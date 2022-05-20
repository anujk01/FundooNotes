using CommonLayer.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.DBContext;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        FundooContext fundoo;
        public IConfiguration Configuration { get; set; }
        public NoteRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundoo = fundooContext;
            this.Configuration = configuration;
        }

        public async Task AddNote(NotePostModel notepostmodel, int Userid)
        {
            try
            {
                Note note = new Note();
                note.userid = Userid;
                note.Title = notepostmodel.Title;
                note.Description = notepostmodel.Description;
                note.Color = notepostmodel.Color;
                note.IsArchive = false;
                note.IsRemainder = false;
                note.IsPin = false;
                note.IsTrash = false;
                note.CreatedDate = DateTime.Now;
                note.ModifedDate = DateTime.Now;
                fundoo.Add(note);
                await fundoo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Note> UpdateNote(int userId, int noteId, NoteUpdateModel noteUpdateModel)
        {
            try
            {
                var note = fundoo.Note.FirstOrDefault(u => u.userid == userId && u.NoteID == noteId);
                if (note != null)
                {
                    note.Title = noteUpdateModel.Title;
                    note.Description = noteUpdateModel.Description;
                    note.IsArchive = noteUpdateModel.IsArchive;
                    note.Color = noteUpdateModel.Color;
                    note.IsPin = noteUpdateModel.IsPin;
                    note.IsRemainder = noteUpdateModel.IsRemainder;
                    note.IsTrash = noteUpdateModel.IsTrash;
                    await fundoo.SaveChangesAsync();
                }
                return await fundoo.Note.Where(u => u.userid == u.userid && u.NoteID == noteId).Include(u => u.user).FirstAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeleteNote(int noteId, int userId)
        {
            try
            {
                var note = fundoo.Note.FirstOrDefault(u => u.NoteID == noteId && u.userid == userId);
                if (note != null)
                {
                    fundoo.Note.Remove(note);
                    await fundoo.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task ChangeColour(int userId, int noteId, string color)
        {
            try
            {
                var note = fundoo.Note.FirstOrDefault(u => u.userid == userId && u.NoteID == noteId);
                if (note != null)
                {
                    note.Color = color;
                    await fundoo.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task ArchiveNote(int userId, int noteId)
        {
            try
            {
                var note = fundoo.Note.FirstOrDefault(u => u.userid == userId && u.NoteID == noteId);
                if (note != null)
                {
                    if (note.IsArchive == true)
                    {
                        note.IsArchive = false;
                    }
                    if (note.IsArchive == false)
                    {
                        note.IsArchive = true;
                    }
                }
                await fundoo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Remainder(int userId, int noteId, DateTime remainder)
        {
            try
            {
                var note = fundoo.Note.FirstOrDefault(u => u.userid == userId && u.NoteID == noteId);
                if (note != null)
                {
                    note.IsRemainder = true;
                    note.Remainder = remainder;
                }
                await fundoo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Trash(int userId, int noteId)
        {
            try
            {
                var note = fundoo.Note.FirstOrDefault(u => u.userid == userId && u.NoteID == noteId);
                if (note != null)
                {
                    if (note.IsTrash == true)
                    {
                        note.IsTrash = false;
                    }
                    if (note.IsTrash == false)
                    {
                        note.IsTrash = true;
                    }
                }
                await fundoo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Pin(int userId, int noteId)
        {
            try
            {
                var note = fundoo.Note.FirstOrDefault(u => u.userid == userId && u.NoteID == noteId);
                if (note != null)
                {
                    if (note.IsPin == true)
                    {
                        note.IsPin = false;
                    }
                    if (note.IsPin == false)
                    {
                        note.IsPin = true;
                    }
                }
                await fundoo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Note>> GetAllNotes(int userId)
        {
            try
            {
                return await fundoo.Note.Where(u => u.userid == userId).Include(u => u.user).ToListAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
