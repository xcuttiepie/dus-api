using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using DUSAPI.Models;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;

namespace DUSAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrmtDetailController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly dusContext _dbcontext;

        public PrmtDetailController(IConfiguration configuration, dusContext _context)
        {
            _dbcontext = _context;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("Count/Document")]
        public ActionResult Count()
        {
            try
            {
                var result = _dbcontext.VDocumenEntries.Where(c => c.IsDelete == 0).Count();
                if (result == 0)
                {
                    var results = "No existing data";
                    return Ok(results);
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                var results = ex;
                return Ok(results);
            }
        }


        [HttpGet]
        [Route("isDelete/PermitEntry/{sites}")]
        public ActionResult IsDelete(string sites)
        {
            try
            {
                var result = _dbcontext.VDocumenEntries.Where(c =>  c.Branch == sites  &&  c.IsDelete == 0).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                var results = ex;
                return Ok(results);
            }

        }

        //MultipleUpload files into database api.
        [HttpPost]
        [Route("MultipleUpload/files/Table")]
        public async Task<IActionResult> UploadMultipleFile(List<IFormFile>? uploadAttachments, string folder)
        {
            try
            {
                if (uploadAttachments.Count > 0)
                {
                    foreach (var PrmtAttachment in uploadAttachments)
                    {
                        string folders = folder + "//";
                        string filename = PrmtAttachment.FileName;
                        string filenames = Path.GetFileName(filename);
                        string uploadpath = Path.Combine("C:\\Users\\jrwvillapando\\source\\repos\\DUSAPI\\DUSAPI\\wwwroot//Permits//" + folders);


                        if (!Directory.Exists(uploadpath))
                        {
                            Directory.CreateDirectory(uploadpath);
                        }
                        using (FileStream fs = System.IO.File.Create(uploadpath + filenames))
                        {
                            await PrmtAttachment.CopyToAsync(fs);
                            await fs.FlushAsync();
                        }

                    }
                    return Ok("Uploaded");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Ok("Not Uploaded");
        }


        //Inserting Document, Date_Uploaded, Full_Details, CreatedBy into First Table
        [HttpPost]
        [Route("Inserting/Data/Table")]
        public async Task<IActionResult> InsertData(PrmtDetail PrmtDetails)
        {
            try
            {

                var result = new PrmtDetail()
                {
                    TransactionId = PrmtDetails.TransactionId,
                    Document = PrmtDetails.Document,
                    FullDetails = PrmtDetails.FullDetails,
                    Status = PrmtDetails.Status,
                    CreatedBy = PrmtDetails.CreatedBy,
                    Branch = PrmtDetails.Branch,

                };
                await _dbcontext.PrmtDetails.AddAsync(PrmtDetails);
                await _dbcontext.SaveChangesAsync();
                return Ok("Data Inserted");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //UPDATE DATA
        [HttpPost]
        [Route("Update/Data/Table")]
        public ActionResult Update(PrmtDetail PrmtDetails)
        {

            try
            {
                string modifieddate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                SqlCommand cmd = new SqlCommand("Update prmt_details SET Document= '" + PrmtDetails.Document + "', FullDetails = '" + PrmtDetails.FullDetails + "', Status = '" + PrmtDetails.Status + "', ModifiedDate = '" + modifieddate + "', ModifiedBy = '" + PrmtDetails.ModifiedBy + "' WHERE ID = '" + PrmtDetails.Id + "'", con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    var results = "Successfully Updated";
                    return Ok(results);
                }
                else
                {
                    var results = "Not Updated";
                    return Ok(results);
                }
            }
            catch (Exception ex)
            {
                var results = ex;
                return Ok(results);
            }
        }

        //DELETE DATA
        [HttpPost]
        [Route("Delete/Data/Table")]
        public ActionResult Delete(PrmtDetail PrmtDetails)
        {
            try
            {

                string modifieddate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                SqlCommand cmd = new SqlCommand("Update prmt_details SET Status='" + PrmtDetails.Status + "',  isDelete='" + PrmtDetails.IsDelete + "', ModifiedDate = '" + modifieddate + "', ModifiedBy = '" + PrmtDetails.ModifiedBy + "'  WHERE ID = '" + PrmtDetails.Id + "'", con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    var results = "Successfully Deleted";
                    return Ok(results);
                }
                else
                {
                    var results = "Not Deleted";
                    return Ok(results);
                }
            }
            catch (Exception ex)
            {
                var results = "ERROR";
                return Ok(results);
            }
        }
    }
}
