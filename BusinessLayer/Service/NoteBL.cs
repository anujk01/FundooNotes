using BusinessLayer.Interface;
using CommonLayer.Users;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class NoteBL : INoteBL
    {
        INoteRL noteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }
        public async Task AddNote(NotePostModel notePostModel, int UserId)
        {
            try
            {
                await this.noteRL.AddNote(notePostModel, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Note> UpdateNote(int UserId, int noteId, NoteUpdateModel noteUpdateModel)
        {
            try
            {
                return await this.noteRL.UpdateNote(UserId, noteId, noteUpdateModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteNote(int UserId, int noteId)
        {
            try
            {
                await this.noteRL.DeleteNote(UserId, noteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ChangeColour(int UserId, int noteId, string color)
        {
            try
            {
                await this.noteRL.ChangeColour(UserId, noteId, color);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ArchiveNote(int UserId, int noteId)
        {
            try
            {
                await this.noteRL.ArchiveNote(UserId, noteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Remainder(int UserId, int noteId, DateTime remainder)
        {
            try
            {
                await this.noteRL.Remainder(UserId, noteId, remainder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Trash(int UserId, int noteId)
        {
            try
            {
                await this.noteRL.Trash(UserId, noteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Pin(int UserId, int noteId)
        {
            try
            {
                await this.noteRL.Pin(UserId, noteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<List<Note>> GetAllNotes(int userId)
        {
            try
            {
                return this.noteRL.GetAllNotes(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}