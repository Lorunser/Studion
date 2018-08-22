using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Studion.Models;
using Studion.Dtos;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace Studion.Controllers.Api
{
    public class RatingController : ApiController
    {
        #region Database
        private ApplicationDbContext _context;

        public RatingController()
        {
            _context = new ApplicationDbContext();
        }
        #endregion

        #region CREATE
        [HttpPost]
        [Route("api/ratings")]
        [Authorize]
        public IHttpActionResult CreateRating(RatingDto ratingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var userID = User.Identity.GetUserId();
            Rating ratingInDb = _context.Ratings
                .SingleOrDefault(r => r.RaterID == userID && r.NoteID == ratingDto.NoteID);

            // rating not made yet
            if (ratingInDb == null) 
            {
                ratingInDb = ToNewRatingInDb(ratingDto);

                _context.Ratings.Add(ratingInDb);
                _context.SaveChanges();

                return Created(new Uri(Request.RequestUri + "/" + ratingDto.NoteID + "/" + userID), ratingDto);
            }

            //rating already exists >> modify
            if(ratingInDb.RaterID == userID)
            {
                ModifyRatingInDb(ratingInDb, ratingDto);
                _context.SaveChanges();

                return Ok(ratingDto);
            }

            //not authorised
            return Unauthorized();
        }
        #endregion

        #region RETRIEVE
        [HttpGet]
        [Route("api/ratings/{noteID}/{raterID}")]
        public IHttpActionResult GetParticularRating(int noteID, string raterID)
        {
            var userID = User.Identity.GetUserId();

            if (userID != raterID)
                return Unauthorized();

            Rating ratingInDb = _context.Ratings
                .SingleOrDefault(r => r.NoteID == noteID && r.RaterID == raterID);

            if (ratingInDb == null)
                return NotFound();

            RatingDto ratingDto = ToRatingDto(ratingInDb);
            return Ok(ratingDto);
        }

        [HttpGet]
        [Route("api/ratings/{noteID}")]
        public IHttpActionResult GetAverageRating(int noteID)
        {
            var note = _context.Notes
                .Include(n => n.ratings)
                .SingleOrDefault(n => n.NoteID == noteID);

            if (note == null)
                return NotFound();

            double averageRating = note.GetAvRating();
            return Ok(averageRating);
        }
        #endregion

        #region UPDATE
        [HttpPut]
        [Route("api/ratings")]
        [Authorize]
        public IHttpActionResult UpdateRating(RatingDto ratingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var userID = User.Identity.GetUserId();

            Rating ratingInDb = _context.Ratings
                .SingleOrDefault(r => r.RaterID == userID && r.NoteID == ratingDto.NoteID);

            if (ratingInDb == null)
                return NotFound();

            if (ratingInDb.RaterID == userID)
            {
                ModifyRatingInDb(ratingInDb, ratingDto);
                _context.SaveChanges();
                return Ok(ratingDto);
            }

            //not authorised
            return Unauthorized();
        }
        #endregion

        #region DELETE
        [HttpDelete]
        [Route("api/ratings/{noteID}/{raterID}")]
        public IHttpActionResult DeleteRating(int noteID, string raterID)
        {
            Rating ratingInDb = _context.Ratings
                .SingleOrDefault(r =>r.NoteID == noteID && r.RaterID == raterID);

            if (ratingInDb == null)
                return NotFound();

            if (User.Identity.GetUserId() == raterID)
            {
                _context.Ratings.Remove(ratingInDb);
                _context.SaveChanges();
                return Ok();
            }

            return Unauthorized();
        }
        #endregion

        #region Helpers
        public Rating ToNewRatingInDb(RatingDto ratingDto)
        {
            Rating ratingInDb = new Rating();

            //from Dto
            ratingInDb.NoteID = ratingDto.NoteID;
            ratingInDb.Stars = ratingDto.Stars;

            //anti-spoof
            ratingInDb.RaterID = User.Identity.GetUserId();

            return ratingInDb;
        }

        public void ModifyRatingInDb(Rating ratingInDb, RatingDto ratingDto)
        {
            ratingInDb.Stars = ratingDto.Stars;
        }

        public RatingDto ToRatingDto(Rating ratingInDb)
        {
            RatingDto ratingDto = new RatingDto();

            ratingDto.NoteID = ratingInDb.NoteID;
            ratingDto.RaterID = ratingInDb.RaterID;
            ratingDto.Stars = ratingInDb.Stars;

            return ratingDto;
        }
        #endregion
    }
}
