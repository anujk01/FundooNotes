using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        Task AddLabel(int userId, int noteId, string labelName);
        public List<Label> GetByLabelId(int noteId);
        public Label UpdateLabel(string labelName, int noteId, int userId);
        public bool DeleteLabel(int labelId, int userId);
    }
}
