$(document).ready(function () {
    $("#noteTable").DataTable({
        ajax: {
            url: "/api/notes",
            dataSrc: ""
        },

        columns: [
            {
                data: "title",
                render: function (data, type, note) {
                    return "<a href='/notes/display/" + note.noteID + "'>" + note.Title + "</a>";
                }
            },
            {
                data: "authorName",
                render: function (data, type, note) {
                    return "<a href='/users/" + note.authorID + "'>" + note.authorName + "</a>";
                }
            },
            {
                data: "subjectName"
            }
        ]
    });
});