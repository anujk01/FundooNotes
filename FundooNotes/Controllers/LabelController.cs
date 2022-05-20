using BusinessLayer.Interface;
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
    public class LabelController : ControllerBase
    {
        FundooContext fundooContext;
        ILabelBL labelBL;
        public LabelController(FundooContext fundoo, ILabelBL labelBL)
        {
            this.fundooContext = fundoo;
            this.labelBL = labelBL;
        }

        [Authorize]
        [HttpPost("AddLabel/{noteId}/{labelName}")]
        public async Task<ActionResult> AddLabel(int noteId, string labelName)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userid", StringComparison.InvariantCultureIgnoreCase));
                int userID = Int32.Parse(userid.Value);

                await this.labelBL.AddLabel(userID, noteId, labelName);
                return this.Ok(new { success = true, message = "Label Added Successfully " });

            }
            catch (System.Exception ex)
            {

                throw ex;
            }

        }
        [Authorize]
        [HttpGet("GetByLableId/{noteId}")]
        public List<Label> GetByLabelId(int noteId)
        {
            try
            {
                var result = this.labelBL.GetByLabelId(noteId);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("UpdateLabel/{lableName}/{noteId}")]
        public IActionResult UpdateLabel(string labelName, int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userid").Value);
                var result = this.labelBL.UpdateLabel(labelName, noteId, userId);
                if (result == null)
                {
                    return this.Ok(new { Success = true, message = " Label Updated successfully "});
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Failed to update" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [Authorize]
        [HttpDelete("DeleteLabel/{labelId}")]
        public IActionResult DeleteLabel(int labelId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userid").Value);
                if (this.labelBL.DeleteLabel(labelId, userId))
                {
                    return this.Ok(new { Success = true, message = " Label Deleted Successfully " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Failed " });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
    