function addComment(creatorId, postId, comment) {
    jQuery.get("/Comments/NewComment?creatorId=" + creatorId + "&postId=" + postId + "&commentContent=" + comment)
        .done(function success(data) {
            var commentSection = document.getElementById(postId + "commentsSection");
            commentSection.innerHTML = data.htmlGeneratedForComments;
            var element = document.getElementById(postId + "buttonForShowingOrHidingTheAddCommentSection");
            var commentsCountElement = document.getElementById(postId + "commentsCount");
            commentsCountElement.innerHTML = data.commentsCount;
            element.click();
        })
}