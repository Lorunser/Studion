using Studion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Studion.Dtos;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace Studion.Controllers.Api
{
    public class CommentController : ApiController
    {
        #region Database
        private ApplicationDbContext _context;

        //constructor
        public CommentController()
        {
            _context = new ApplicationDbContext();
        }
        #endregion

        #region CREATE
        [HttpPost]
        [Route("api/comments")]
        public IHttpActionResult CreateComment(CommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (User.Identity.IsAuthenticated)
            {
                Comment commentInDb = ToNewCommentInDb(commentDto);

                //save to db
                _context.Comments.Add(commentInDb);
                _context.SaveChanges();

                //extract id
                commentDto.CommentID = commentInDb.CommentID;

                return Created(new Uri(Request.RequestUri + "/" + commentDto.NoteID), commentDto);
            }

            return Unauthorized();

        }
        #endregion

        #region RETRIEVE
        [HttpGet]
        [Route("api/comments/{noteID}")]
        public IHttpActionResult GetCommentsForNote(int noteID)
        {
            //extract all comments for said note
            IEnumerable<Comment> commentsInDb = _context.Comments
                .Include(c => c.commenter.UserName)
                .Where(c => c.NoteID == noteID)
                .ToList();

            //convert to dtos
            IEnumerable<CommentDto> commentDtos = ToCommentDtoList(commentsInDb);

            return Ok(commentDtos);
        }
        #endregion

        #region UPDATE
        [HttpPut]
        [Route("api/comments")]
        public IHttpActionResult UpdateComment(CommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Comment commentInDb = _context.Comments
                .Single(c => c.CommentID == commentDto.CommentID);

            if (commentInDb == null)
                return NotFound();

            if(User.Identity.GetUserId() == commentInDb.CommenterID)
            {
                //make changes
                ModifyCommentInDb(commentInDb, commentDto);
                //save
                _context.SaveChanges();
                //success
                return Ok(commentDto);
            }

            return Unauthorized();
        }
        #endregion

        #region DELETE
        [HttpDelete]
        [Route("api/comments/{commentID}")]
        public IHttpActionResult DeleteComment(int commentID)
        {
            Comment commentInDb = _context.Comments
                .Single(c => c.CommentID == commentID);

            if (commentInDb == null)
                return NotFound();

            if(User.Identity.GetUserId() == commentInDb.CommenterID)
            {
                _context.Comments.Remove(commentInDb);
                _context.SaveChanges();
                return Ok();
            }

            return Unauthorized();
        }
        #endregion

        #region Helpers
        private Comment ToNewCommentInDb(CommentDto commentDto)
        {
            Comment commentInDb = new Comment();

            //assign all props from Dto
            commentInDb.NoteID = commentDto.NoteID;
            commentInDb.CommentMessage = commentDto.CommentMessage;

            //assign props that cannot be forged
            commentInDb.CommenterID = User.Identity.GetUserId();
            commentInDb.PostTime = DateTime.Now;

            return commentInDb;
        }

        private void ModifyCommentInDb(Comment commentInDb, CommentDto commentDto)
        {
            //assign props from Dto
            commentInDb.CommentMessage = commentDto.CommentMessage;
            //alter post time
            commentInDb.PostTime = DateTime.Now;
        }

        private IEnumerable<CommentDto> ToCommentDtoList(IEnumerable<Comment> commentsInDb)
        {
            List<CommentDto> commentDtos = new List<CommentDto>();

            foreach (var commentInDb in commentsInDb)
            {
                commentDtos.Add(ToCommentDto(commentInDb));
            }

            return commentDtos;
        }

        private CommentDto ToCommentDto(Comment commentInDb)
        {
            CommentDto commentDto = new CommentDto();

            //assign props
            commentDto.CommentID = commentInDb.CommentID;

            commentDto.CommenterID = commentInDb.CommenterID;
            commentDto.CommenterUsername = commentInDb.commenter.UserName;

            commentDto.CommentMessage = commentInDb.CommentMessage;
            commentDto.PostTime = commentInDb.PostTime;

            return commentDto;
        }
        #endregion
    }
}
