using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using DUSAPI.Models;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.AspNetCore.Hosting;

namespace DUSAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrmtAttachmentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly dusContext _dbcontext;
        public static IWebHostEnvironment _webHostEnvironment;

        public PrmtAttachmentController(IConfiguration configuration, dusContext _context, IWebHostEnvironment webHostEnvironment)
        {
            _dbcontext = _context;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var result = _dbcontext.VPrmtAttachments.ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                var results = ex;
                return Ok(results);
            }

        }

        //Insert Attachment , AttachmentPath & CreatedBy API.
        [HttpPost]
        [Route("Inserting/Table")]
        public async Task<IActionResult> InsertData(PrmtAttachment PrmtAttachments)
        {
            try
            {
                string randomname = DateTime.Now.ToString("MMddyyyyhhmmss") + Path.GetRandomFileName() + PrmtAttachments.Filename;
                int user = PrmtAttachments.TransactionId;
                string uploadpath = Path.Combine("C:\\Users\\jrwvillapando\\source\\repos\\project1\\project1\\wwwroot//Permits//" + user + "//");
                string filepath = uploadpath + PrmtAttachments.Filepath;
                PrmtAttachments.Filepath = BCrypt.Net.BCrypt.HashPassword(filepath);
                PrmtAttachments.NewFilename = randomname;

                var result = new PrmtAttachment()
                {
                    TransactionId = PrmtAttachments.TransactionId,
                    Filename = PrmtAttachments.Filename,
                    NewFilename = PrmtAttachments.NewFilename,
                    Filepath = PrmtAttachments.Filepath,
                    CreatedBy = PrmtAttachments.CreatedBy,
                    Branch = PrmtAttachments.Branch,

                };
                await _dbcontext.PrmtAttachments.AddAsync(PrmtAttachments);
                await _dbcontext.SaveChangesAsync();
                return Ok("Data Inserted");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }


        //Fetch user attachment
        [HttpGet]
        [Route("Fetch/TransactionID/{folder}")]
        public async Task<IActionResult> GetFiles([FromRoute] int folder)
        {
            var result = await _dbcontext.PrmtAttachments.Where(c => c.TransactionId == folder).Where( c => c.IsDelete == 0).ToListAsync();
            return Ok(result);
        }


        //DELETE DATA
        [HttpPost]
        [Route("Delete/Attachment/Table")]
        public ActionResult Delete(PrmtAttachment PrmtAttachments)
        {
            try
            {

                string modifieddate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                SqlCommand cmd = new SqlCommand("Update prmt_attachment SET isDelete='" + PrmtAttachments.IsDelete + "', ModifiedDate = '" + modifieddate + "', ModifiedBy = '" + PrmtAttachments.ModifiedBy + "' WHERE ID = '" + PrmtAttachments.Id + "'", con);
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


        //Download files/attachments API.
        [HttpPost]
        [Route("Download/files")]
        public FileResult DownloadFiles(string files, string folder)
        {

            //Build the File Path.
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "C:\\Users\\jrwvillapando\\source\\repos\\DUSAPI\\DUSAPI\\wwwroot//Permits//" + folder + "//") + files;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", files);

        }






    }
}
