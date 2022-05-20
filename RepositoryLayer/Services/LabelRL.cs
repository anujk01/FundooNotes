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
    public class LabelRL : ILabelRL
    {
        FundooContext fundoo;
        public IConfiguration Configuration { get; set; }
        public LabelRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundoo = fundooContext;
            this.Configuration = configuration;
        }

        public async Task AddLabel(int userId, int noteId, string labelName)
        {
            try
            {
                Label label = new Label();
                label.userid = userId;
                label.LabelName = labelName;
                label.NoteID = noteId;
                fundoo.Add(label);
                await fundoo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }     
        }
        public Label UpdateLabel(string labelName, int noteId, int userId)
        {
            try
            {
                var result = this.fundoo.Label.FirstOrDefault(u => u.NoteID == noteId && u.userid == userId);
                if (result != null)
                {
                    result.LabelName = labelName;
                    this.fundoo.Label.Update(result);
                    this.fundoo.SaveChanges();
                    return null;

                }
                else
                {
                    return null;
                }
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
                var data = this.fundoo.Label.Where(d => d.NoteID == noteId).ToList();
                if (data != null)
                {
                    return data;
                }
                else
                {
                    return null;
                }
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
                var result = this.fundoo.Label.FirstOrDefault(u => u.LabelId == labelId && u.userid == userId);
                if (result != null)
                {
                    this.fundoo.Label.Remove(result);
                    this.fundoo.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
