using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        Task AddLabel(int userId, int noteId, string labelName);
        public List<Label> GetByLabelId(int noteId);
        public Label UpdateLabel(string labelName, int noteId, int userId);
        public bool DeleteLabel(int noteId, int userid);
    }
}
