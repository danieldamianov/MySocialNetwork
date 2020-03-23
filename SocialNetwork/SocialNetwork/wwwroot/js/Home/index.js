function userLikedOrUnlikedPost(userId, postId) {
    jQuery.get("/Likes/UserLikedOrUnlikedPost?userWhoLikesIt=" + userId + "&likedPostId=" + postId)
        .done(function success(data) {

            var likeCountElement = document.getElementById(postId + "likesCount");
            var likeImageElement = document.getElementById(postId + "image");

            //var likeOrUnlikeButtonPath = "fas fa-thumbs-down";
            //if (post.HasCurrentUserLikedThePost == false) {
            //    likeOrUnlikeButtonPath = "fas fa-thumbs-up";
            //}
            if (data.usersHasUnLikedThePost) {

                var firstLikes = parseInt(likeCountElement.innerHTML);
                var afterLiking = --firstLikes;
                likeCountElement.innerHTML = afterLiking;
                likeImageElement.className = "fas fa-thumbs-up";

            }
            else if (data.usersHasLikedThePost) {

                var firstLikes = parseInt(likeCountElement.innerHTML);
                var afterLiking = ++firstLikes;
                likeCountElement.innerHTML = afterLiking;
                likeImageElement.className = "fas fa-thumbs-down";
            }
    })

}

function showOrHideAddCommentSection(postId) {
    var element = document.getElementById(postId + "addCommentSection");

    if (element.style.display == "none") {
        element.style.display = "block";
    }
    else {
        element.style.display = "none"
    }
}

function showOrHideCommentsSection(postId){
    var element = document.getElementById(postId + "commentsSection");
    if (element.style.display == "none") {
    element.style.display = "block";
    }
    else {
        element.style.display = "none"
    }
}