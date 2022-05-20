using BusinessLayer.Interface;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class LabelBL : ILabelBL
    {
        ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        public async Task AddLabel(int userId, int noteId, string labelName)
        {
            try
            {
                await this.labelRL.AddLabel(userId, noteId, labelName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Label UpdateLabel(string LabelName, int noteId, int userId)
        {
            try
            {
                return this.labelRL.UpdateLabel(LabelName, noteId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Label> GetByLabelId(int noteId)
        {
            try
            {
                return this.labelRL.GetByLabelId(noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteLabel(int labelId, int userId)
        {
            try
            {
                return this.labelRL.DeleteLabel(labelId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}