using BusinessLayer.Interface;
using CommonLayer.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.DBContext;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [ApiController]

    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        FundooContext fundooContext;
        INoteBL noteBL;
        public NoteController(FundooContext fundoo, INoteBL noteBL)
        {
            this.fundooContext = fundoo;
            this.noteBL = noteBL;
        }

        [Authorize]
        [HttpPost("AddNote")]
        public async Task<IActionResult> AddNote(NotePostModel notePostModel)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userid", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                await this.noteBL.AddNote(notePostModel, userId);

                return this.Ok(new { success = true, message = $"Note Added Successfully" });

            }
            catch (SystemException ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("Update/{noteId}")]
        public async Task<ActionResult> UpdateNote(int noteId, NoteUpdateModel noteUpdateModel)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userid", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);

                var note = fundooContext.Note.FirstOrDefault(u => u.userid == UserId && u.NoteID == noteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = " Sorry!!! Failed to Update note" });
                }
                if (note.IsTrash == true)
                {
                    return this.BadRequest(new { success = false, message = " Sorry !!! Note has been Deleted,Please Create New Note" });
                }
                await this.noteBL.UpdateNote(UserId, noteId, noteUpdateModel);
                return this.Ok(new { success = true, message = "Note Updated successfully!!!" });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("Delete/{noteId}")]
        public async Task<ActionResult> DeleteNote(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userid", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);
                var note = fundooContext.Note.FirstOrDefault(u => u.userid == UserId && u.NoteID == noteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Oops!! This note is not available " });

                }
                if (note.IsTrash == true)
                {
                    return this.BadRequest(new { success = false, message = " Sorry !!! Note has been Deleted,Please Create New Note" });
                }
                await this.noteBL.DeleteNote(noteId, UserId);
                return this.Ok(new { success = true, message = "Note Deleted Successfully" });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize]
        [HttpPut("ChangeColour/{noteId}/{colour}")]
        public async Task<ActionResult> ChangeColour(int noteId, string colour)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userid", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);

                var note = fundooContext.Note.FirstOrDefault(u => u.userid == UserId && u.NoteID == noteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry!!! Note doesn't exist" });
                }
                if (note.IsTrash == true)
                {
                    return this.BadRequest(new { success = false, message = " Sorry !!! Note has been Deleted,Please Create New Note" });
                }

                await this.noteBL.ChangeColour(UserId, noteId, colour);
                return this.Ok(new { success = true, message = "Note Colour Changed Successfully " });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize]
        [HttpPut("ArchiveNote/{noteId}")]
        public async Task<ActionResult> ArchiveNote(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userid", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);

                var note = fundooContext.Note.FirstOrDefault(u => u.userid == userId && u.NoteID == noteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = " Sorry !!! Failed to Archive Notes" });
                }
                if (note.IsTrash == true)
                {
                    return this.BadRequest(new { success = false, message = " Sorry !!! Note has been Deleted,Please Create New Note" });
                }
                await this.noteBL.ArchiveNote(userId, noteId);
                return this.Ok(new { success = true, message = "Note Archived successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("remainderNote/{noteId}/{remainder}")]
        public async Task<ActionResult> RemainderNote(int noteId, DateTime remainder)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userid", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);

                var note = fundooContext.Note.FirstOrDefault(u => u.userid == userId && u.NoteID == noteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry !! Note doesn't Exist" });
                }
                if (note.IsTrash == true)
                {
                    return this.BadRequest(new { success = false, message = " Sorry !!! Note has been Deleted,Please Create New Note" });
                }
                await this.noteBL.Remainder(userId, noteId, remainder);
                return this.Ok(new { success = true, message = "Remainder Sets Successfully!!!" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("Trash/{noteId}")]
        public async Task<ActionResult> IsTrash(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userid", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);

                var note = fundooContext.Note.FirstOrDefault(u => u.userid == userId && u.NoteID == noteId);
                if(note == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry !! Failed to Trash Note" });
                }
                if (note.IsTrash == true)
                {
                    return this.BadRequest(new { success = false, message = " Sorry !!! Note has been Deleted,Please Create New Note" });
                }
                await this.noteBL.Trash(userId, noteId);
                return this.Ok(new { success = true, message = "Trash Added Successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("Pin/{noteId}")]

        public async Task<ActionResult> IsPin(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userid", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);

                var note = fundooContext.Note.FirstOrDefault(u => u.userid == userId && u.NoteID == noteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry !! Failed to Trash Note" });
                }
                if (note.IsTrash == true)
                {
                    return this.BadRequest(new { success = false, message = " Sorry !!! Note has been Deleted,Please Create New Note" });
                }
                await this.noteBL.Pin(userId, noteId);
                return this.Ok(new { success = true, message = "Trash Added Successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetAllNotes")]
        public async Task<ActionResult> GetAllNotes()
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("user", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                List<Note> result = new List<Note>();
                result = await this.noteBL.GetAllNotes(userId);
                return this.Ok(new { success = true, message = $"All Notes", data = result });
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    
}
