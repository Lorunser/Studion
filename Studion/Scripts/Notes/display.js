$(document).ready(function () {
    var script = $('#scriptElement');
    var noteID = script.attr('noteID');
    var username = script.attr('username');

    deleteButton(noteID);
    commentList(noteID, username);
    commentForm(noteID);
})

//delete button
function deleteButton(noteID){
    $("#delete-button").on("click", function () {
        var button = $(this);

        bootbox.confirm("Are you sure you want to delete this set of notes?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/notes/" + noteID,
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
function commentList(noteID, username) {
    //deal with rendering list of comments
    $.ajax({
        url: '/api/comments/' + noteID,
        dataType: 'json',
        method: 'GET',

        success: function (data) {
            var commentListGroup = $("#commentListGroup");
            var length = data.length;

            for (var i = 0; i < length; i++) {
                var comment = data[i]

                //check if can show delete
                var deleteButton = "";
                if (username == comment.commenterUsername) {
                    deleteButton = "<a href='/comment/delete/";
                }

                //append comment
                commentListGroup.append(
                    "<li class='list-group-item'>\
                                <p>\
                                    <a href='/users/" + comment.commenterID + "'>" + comment.commenterUsername + "</a>" +
                                    "<small>  " + moment(comment.postTime).format("MMMM YYYY") + "  </small>" +
                                "</p>\
                                <p>" +
                                comment.commentMessage +
                                "</p>\
                            </li>"
                    );
            }

            var header = $("#header");
            header.text("Comments (" + length + ")");
        }
    });
}

//deal with comment form
function commentForm(noteID) {
    // get comment form
    var commentForm = $("#commentForm");

    // check form rendered
    if(commentForm.length == 1){
        //called on form submit
        commentForm.on("submit", function (event) {
            event.preventDefault();
            var form = $(this);

            var data = form.serializeArray();
            message = data[0].value;

            var commentDto = {
                noteID: noteID,
                commentMessage: message
            };

            $.ajax({
                url: "/api/comments",
                method: "POST",
                contentType: "application/json",
                data: JSON.stringify(commentDto),
                success: function () {
                    var commentListGroup = $('#commentListGroup')

                    commentListGroup.append(
                    "<li class='list-group-item'>\
                                <p>\
                                    <a> Your comment </a>" +
                                    "<small>" + "  just now</small>" +
                                "</p>\
                                <p>" +
                                commentDto.commentMessage +
                                "</p>\
                            </li>"
                    );
                },
                error: function (error) {
                    alert('An error has occured');
                }
            });

            return false;
        });
    }   
}