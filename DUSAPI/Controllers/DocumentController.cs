using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using DUSAPI.Models;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.CodeAnalysis;

namespace DUSAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly dusContext _dbcontext;

        public DocumentController(IConfiguration configuration, dusContext _context)
        {
            _dbcontext = _context;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var result = _dbcontext.VDocuments.Where(c => c.IsDelete == 0).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                var results = ex;
                return Ok(results);
            }

        }

        [HttpGet]
        [Route("Count/Document")]
        public ActionResult Count()
        {
            try
            {
                var result = _dbcontext.VDocuments.Count();
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
        [Route("isDelete/Document")]
        public ActionResult IsDelete()
        {
            try
            {
                var result = _dbcontext.VDocuments.Where(c => c.IsDelete == 0).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                var results = ex;
                return Ok(results);
            }

        }


        [HttpPost]
        [Route("Inserting/Branch")]
        public async Task<IActionResult> InsertData(Documents Documentss)
        {
            try
            {

                var result = new Documents()
                {
                    Name = Documentss.Name,
                    Status = Documentss.Status,
                    CreatedBy = Documentss.CreatedBy,
                };
                await _dbcontext.Documentss.AddAsync(Documentss);
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
        [Route("Update/Document/Table")]
        public ActionResult Update(Documents Documentss)
        {

            try
            {
                string modifieddate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                SqlCommand cmd = new SqlCommand("Update documents SET Name= '" + Documentss.Name +  "', Status = '" + Documentss.Status + "', ModifiedDate = '" + modifieddate + "', ModifiedBy = '" + Documentss.ModifiedBy + "' WHERE ID = '" + Documentss.Id + "'", con);
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
        [Route("Delete/Document/Table")]
        public ActionResult Delete(Documents Documentss)
        {
            try
            {

                string modifieddate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                SqlCommand cmd = new SqlCommand("Update documents SET Status = '" + Documentss.Status + "',  isDelete='" + Documentss.IsDelete + "', ModifiedDate = '" + modifieddate + "', ModifiedBy = '" + Documentss.ModifiedBy + "' WHERE ID = '" + Documentss.Id + "'", con);
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
