using CommonLayer.Users;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        Task AddNote(NotePostModel notepostmodel, int UserID);
        Task<Note> UpdateNote(int userId, int noteId, NoteUpdateModel noteUpdateModel);
        Task DeleteNote(int noteId, int userId);
        Task ChangeColour(int userId, int noteId, string colour);
        Task ArchiveNote(int userId, int noteId);
        Task Remainder(int userId, int noteId, DateTime remainder);
        Task Trash(int userId, int noteId);
        Task Pin(int userId, int noteId);
        Task<List<Note>> GetAllNotes(int userId);

    }
}
