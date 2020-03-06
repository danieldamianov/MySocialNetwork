function userLikedPost(userId, postId) {
    jQuery.get("Likes/LikePost?userWhoLikesIt=" + userId + "&likedPostId=" + postId)
        .done(function success(data) {
            if (data) {
                alert('dsa')
                var firstLikes = parseInt(document.getElementById(postId + 'likesCount').innerHTML);
                var afterLiking = ++firstLikes;
                document.getElementById(postId + 'likesCount').innerHTML = afterLiking;
        }
    })

}