function userLikedOrUnlikedPost(userId, postId) {
    jQuery.get("/Likes/UserLikedOrUnlikedPost?userWhoLikesIt=" + userId + "&likedPostId=" + postId)
        .done(function success(data) {

            var likeCountElement = document.getElementById(postId + "likesCount");
            var likeImageElement = document.getElementById(postId + "image");
            var usersLikedThePostExtendedViewElement = document.getElementById(postId + "usersLikedThePostExtendetView");
            usersLikedThePostExtendedViewElement.innerHTML = "";

            if (data.usersHasUnLikedThePost) {
                likeImageElement.className = "fas fa-thumbs-up";
            }
            else if (data.usersHasLikedThePost) {
                likeImageElement.className = "fas fa-thumbs-down";
            }
            data.usersLikedThePost.forEach(
                element =>
                    usersLikedThePostExtendedViewElement.innerHTML +=
                    `<a href="/Users/Profile/?userId=${element.userId}" style="margin: 1px">
                        <div>
                            <img style="object-fit: cover; border-radius: 50%; width : 40px ; height: 40px" src="${element.profilePicturePath}" alt="Alternate Text" /> 
                            <b>${element.username}</b>
                        </div > 
                     </a >`
            );
            likeCountElement.innerHTML = data.likesCount;
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

function showOrHideCommentsSection(postId) {
    var element = document.getElementById(postId + "commentsSection");
    if (element.style.display == "none") {
        element.style.display = "block";
    }
    else {
        element.style.display = "none"
    }
}