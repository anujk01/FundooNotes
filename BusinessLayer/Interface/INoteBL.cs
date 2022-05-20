using CommonLayer.Users;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface INoteBL
    {
        Task AddNote(NotePostModel notePostModel, int UserID);
        Task<Note> UpdateNote(int userId, int noteId, NoteUpdateModel noteUpdateModel);
        Task DeleteNote(int userId, int noteId);
        Task ChangeColour(int userId, int noteId, string color);
        Task ArchiveNote(int userId, int noteId);
        Task Remainder(int userId, int noteId, DateTime remainder);
        Task Trash(int userId, int noteId);
        Task Pin(int userId, int noteId);
        Task<List<Note>> GetAllNotes(int userId);
    }
}
