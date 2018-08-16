$(document).ready(function () {
    deleteButton();
    commentList();
    commentForm();
})

//delete button
function deleteButton(){
    $("#delete-button").on("click", function () {
        var button = $(this);

        bootbox.confirm("Are you sure you want to delete this set of notes?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/notes/" + button.attr("data-note-id"),
                    method: "DELETE",
                    success: function () {
                        window.location = "/home";
                    }
                });
            }
        })
    });
}

//deal with rendering list of comments
function commentList() {
    //deal with rendering list of comments
    $.ajax({
        url: 'api/comments/@Model',
        dataType: 'json',
        method: 'GET',

        success: function (data) {
            var commentListGroup = $("#commentListGroup");
            var i = 0;

            for (var comment in data) {
                i++;
                commentListGroup.append(
                    "<li class='list-group-item'>\
                                <p>\
                                    <a href='/users/" + comment.commenterID + "'>" + comment.commenterUsername + "</a>"
                            + comment.commentMessage +
                            "<small>" + moment(comment.postTime).format("MMMM YYYY") + "</small>\
                                </p>\
                            </li>"
                    );
            }

            var header = $("#header");
            header.val("Comments (" + i + ")");
        }
    });
}

//deal with comment form
function commentForm() {
    // get comment form
    var commentForm = $("#commentForm");

    // check form rendered
    if(commentForm.length == 1){
        //called on form submit
        commentForm.on("submit", function (event) {
            event.preventDefault();
            var form = $(this);

            data = form.serialize();
            message = data[0].value;

            $.ajax({
                url: "api/comments",
                method: "POST",
                data: {
                    noteID : $("#title").attr("data-note-id"),
                    commentMessage : message
                },
                success: function () {
                    location.reload(true);
                }
            });

            return false;
        });
    }   
}