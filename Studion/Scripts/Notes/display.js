$(document).ready(function () {
    var script = $('#scriptElement');
    var noteID = script.attr('noteID');
    var username = script.attr('username');

    deleteButton(noteID);
    commentList(noteID, username);
    commentForm(noteID, username);
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
                    deleteButton = "<button class='btn btn-danger js-comment-delete' commentID='" + comment.commentID + "'>Delete</button>";
                }

                //append comment
                commentListGroup.append(
                    "<li class='list-group-item'>\
                                <p>\
                                    <a href='/users/" + comment.commenterID + "'>" + comment.commenterUsername + "</a>" +
                                    "<small>  " + moment(comment.postTime).format("MMMM YYYY") + "  </small>" +
                                "</p>\
                                <div class='row'>\
                                <p class='col-md-11'>" +
                                htmlEncode(comment.commentMessage) +
                                "</p>" +
                                deleteButton +
                                "</div>\
                            </li>"
                    );
            }

            var header = $("#header");
            header.text("Comments (" + length + ")");

            //register event listeners
            commentDeleteButtons();
        }
    });
}

//deal with comment form
function commentForm(noteID, username) {
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
                success: function (data) {
                    var commentListGroup = $('#commentListGroup')
                    var commentID = data.commentID;
                    var deleteButton = "<button class='btn btn-danger js-comment-delete' commentID='" + commentID + "'>Delete</button>";

                    commentListGroup.append(
                    "<li class='list-group-item'>\
                                <p>\
                                    <a>" + username + " </a>" +
                                    "<small>" + "  just now</small>" +
                                "</p>\
                                <div class='row'>\
                                <p class='col-md-11'>" +
                                htmlEncode(commentDto.commentMessage) +
                                "</p>" +
                                deleteButton +
                                "</div>\
                            </li>"
                    );

                    form.trigger('reset');
                    commentDeleteButtons();
                },
                error: function (error) {
                    bootbox.alert('An error has occured');
                }
            });

            return false;
        });
    }   
}

//register click events for comment delete
function commentDeleteButtons() {
    //register click events
    $('.js-comment-delete').on('click', function () {
        var button = $(this);
        var commentID = button.attr('commentID');

        bootbox.confirm("Are you sure you want to delete this comment", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/comments/" + commentID,
                    method: "DELETE",
                    success: function () {
                        //button.closest('li').remove(); // not working
                        location.reload();
                    },
                    error: function () {
                        bootbox.alert('An error has occured');
                    }
                });
            }
        })
    });
}

//xss prevention functions
function htmlEncode(str) {
    // This must be done in a proper order to achive desired results.
    str = this.replaceAll(str, "&", "&amp;");
    str = this.replaceAll(str, "<", "&lt;");
    str = this.replaceAll(str, ">", "&gt;");
    str = this.replaceAll(str, " ", "&nbsp;");
    return str;
}

function replaceAll(str, subStr, newStr) {
    var offset = 0;
    var index = str.indexOf(subStr);
    while (index != -1) {
        str = str.substr(0, index) + newStr + str.substr(index + subStr.length);
        offset = index + newStr.length;
        index = str.indexOf(subStr, offset);
    }
    return str;
}