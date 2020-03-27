using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SocialNetwork.Controllers.Extensions;
using SocialNetwork.Models;
using SocialNetwork.Models.Comments;
using SocialNetwork.Services.CommentsManagement;

namespace SocialNetwork.Controllers
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// Render a partial view to string.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="controller"></param>
        /// <param name="viewNamePath"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static async Task<string> RenderViewToStringAsync<TModel>(this Controller controller, string viewNamePath, TModel model)
        {
            if (string.IsNullOrEmpty(viewNamePath))
                viewNamePath = controller.ControllerContext.ActionDescriptor.ActionName;

            controller.ViewData.Model = model;

            using (StringWriter writer = new StringWriter())
            {
                try
                {
                    IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;

                    ViewEngineResult viewResult = null;

                    if (viewNamePath.EndsWith(".cshtml"))
                        viewResult = viewEngine.GetView(viewNamePath, viewNamePath, false);
                    else
                        viewResult = viewEngine.FindView(controller.ControllerContext, viewNamePath, false);

                    if (!viewResult.Success)
                        return $"A view with the name '{viewNamePath}' could not be found";

                    ViewContext viewContext = new ViewContext(
                        controller.ControllerContext,
                        viewResult.View,
                        controller.ViewData,
                        controller.TempData,
                        writer,
                        new HtmlHelperOptions()
                    );

                    await viewResult.View.RenderAsync(viewContext);

                    return writer.GetStringBuilder().ToString();
                }
                catch (Exception exc)
                {
                    return $"Failed - {exc.Message}";
                }
            }
        }
    }

    public class CommentsController : Controller
    {
        private readonly ICommentsFunctionalityService commentsFunctionalityService;

        private readonly IControllerAdditionalFunctionality controllerAdditionalFunctionality;

        public CommentsController(
            ICommentsFunctionalityService commentsFunctionalityService,
            IControllerAdditionalFunctionality controllerAdditionalFunctionality)
        {
            this.commentsFunctionalityService = commentsFunctionalityService;
            this.controllerAdditionalFunctionality = controllerAdditionalFunctionality;
        }

        [Authorize]
        public async Task<IActionResult> NewComment(string creatorId, string postId, string commentContent)
        {
            this.commentsFunctionalityService.AddCommentToPost(creatorId, postId, commentContent);

            var result = new StringBuilder();

            var commentsofPost = this.commentsFunctionalityService.GetAllUsersWhoHaveCommentedPost(postId);

            var postsCount = 0;

            foreach (var comment in commentsofPost)
            {
                postsCount++;
                string viewContent = await this.RenderViewToStringAsync<LinkToProfileViewModel>("_LinkToProfile", new LinkToProfileViewModel(
                    comment.UserId,
                    this.controllerAdditionalFunctionality.GetProfilePicture(comment.UserId),
                    comment.Username,
                    40));
                result.Append(viewContent);

                result.Append($"<p style=\"word-wrap: break-word; \">{comment.Comment}</p>");
            }

            return this.Json(new CommentSectionViewModel(result.ToString(), postsCount));
        }
    }
}