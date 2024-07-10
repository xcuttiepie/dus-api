using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using DUSAPI.Models;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using System.Linq;
using Microsoft.CodeAnalysis.Operations;

namespace DUSAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly dusContext _dbcontext;

        public BranchController(IConfiguration configuration, dusContext _context)
        {
            _dbcontext = _context;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var result = _dbcontext.VBranches.Where(c => c.IsDelete == 0).Count();
                return Ok(result);
            }
            catch (Exception ex)
            {
                var results = ex;
                return Ok(results);
            }
        }

        [HttpGet]
        [Route("GETS/Branches")]
        public ActionResult Gets()
        {
            try
            {
                var result = _dbcontext.VBranchings.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                var results = ex;
                return Ok(results);
            }

        }

        [HttpGet]
        [Route("isDelete/Branches")]
        public ActionResult IsDelete()
        {
            try
            {
                var result = _dbcontext.VBranches.Where(c => c.IsDelete == 0).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                var results = ex;
                return Ok(results);
            }

        }


        [HttpGet]
        [Route("isDeleteS/BranchesS/{USERNAME}")]
        public ActionResult IsDeleteS()
        {
            try
            {
                var result = _dbcontext.VBranchings.Where(c => c.IsDelete == 0).ToList();
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
        public async Task<IActionResult> InsertData(Branch Branchs)
        {
            try
            {
                if (_dbcontext.Branches.Any(x => x.Branchcode == Branchs.Branchcode))
                {
                    return Ok("Existing Branch Code");
                }
                else if (_dbcontext.Branches.Any(x => x.Branchname == Branchs.Branchname))
                {
                    return Ok("Existing Branch Name");
                }
                else
                {
                    var result = new Branch()
                    {
                        Branchcode = Branchs.Branchcode,
                        Branchname = Branchs.Branchname,
                        BranchAddress = Branchs.BranchAddress,
                        Status = Branchs.Status,
                        CreatedBy = Branchs.CreatedBy,

                    };
                    await _dbcontext.Branches.AddAsync(Branchs);
                    await _dbcontext.SaveChangesAsync();
                    return Ok("Data Inserted");
                }

            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }




        //UPDATE DATA
        [HttpPost]
        [Route("Update/Branch/Table")]
        public ActionResult Update(Branch Branchs)
        {

            try
            {
                string modifieddate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                SqlCommand cmd = new SqlCommand("Update branch SET Branchcode= '" + Branchs.Branchcode + "', Branchname = '" + Branchs.Branchname + "', BranchAddress = '" + Branchs.BranchAddress + "', Status = '" + Branchs.Status + "', ModifiedDate = '" + modifieddate + "', ModifiedBy = '" + Branchs.ModifiedBy + "' WHERE ID = '" + Branchs.Id + "'", con);
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
        [Route("Delete/Branch/Table")]
        public ActionResult Delete(Branch Branchs)
        {
            try
            {

                string modifieddate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                SqlCommand cmd = new SqlCommand("Update branch SET Status='" + Branchs.Status + "',  isDelete='" + Branchs.IsDelete + "', ModifiedDate = '" + modifieddate + "', ModifiedBy = '" + Branchs.ModifiedBy + "'  WHERE Branchcode = '" + Branchs.Branchcode + "'", con);
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


        //Fetch email branch/user
        [HttpGet]
        [Route("Fetch/Email/{email}")]
        public async Task<IActionResult> Getemail([FromRoute]string email)
        {
            try
            {
                if (_dbcontext.VBranchings.Any(x => x.Email == email))
                {
                    if (_dbcontext.UserLists.Any(x => x.Email == email && x.IsDelete != 0))
                    {
                    }
                   else if (_dbcontext.UserLists.Any(x => x.Email == email && x.Status == "I"))
                    {
                        var result = await _dbcontext.VBranchings.Where(c => c.Email == email && c.IsDelete == 0).ToListAsync();
                        return Ok(result);
                    }

                    else
                    {
                        var result = await _dbcontext.VBranchings.Where(c => c.Email == email && c.IsDelete == 0).ToListAsync();
                        return Ok(result);
                    }

                }
                else if (_dbcontext.VBranchings.Any(x => x.UserName == email))
                {
                    if (_dbcontext.UserLists.Any(x => x.Username == email && x.IsDelete != 0))
                    {
                      
                    }
                    else if (_dbcontext.UserLists.Any(x => x.Username == email && x.Status == "I"))
                    {
                        var result = await _dbcontext.VBranchings.Where(c => c.UserName == email && c.IsDelete == 0).ToListAsync();
                        return Ok(result);
                    }
                    else
                    {
                        var result = await _dbcontext.VBranchings.Where(c => c.UserName == email && c.IsDelete == 0).ToListAsync();
                        return Ok(result);
                    }

                }
                return Ok();
            }
            catch (Exception ex)
            {
                var results = ex;
                return Ok(results);
            }

        }

        //Fetch BranchID branch
        [HttpGet]
        [Route("Fetch/BranchID/{Branchname}")]
        public async Task<IActionResult> GetBranch([FromRoute] string Branchname)
        {
            var result = await _dbcontext.Branches.Where(c => c.Branchname == Branchname).ToListAsync();
            return Ok(result);
        }

        //Fetch Branch that notexisting on that user.
        [HttpGet]
        [Route("Fetch/NotExistingbranch/{username}")]
        public ActionResult fetchbranch(string username)
        {
            try
            {
                    var Branches = _dbcontext.Branches.Where(b => b.IsDelete == 0 && b.Status == "A" 
                    && !_dbcontext.VBranchings.Any(vb => vb.BranchName == b.Branchname && vb.Status != "I" && vb.IsDelete != 1 && vb.UserName == username)).ToList();
                    return Ok(Branches);
              
            }
            catch (Exception ex)
            {
                var results = ex;
                return Ok(results);
            }
        }



        [HttpPost]
        [Route("UpdatingExisting/User/Branch")]
        public ActionResult UpdatingExistingBranch(Branching Branchings)
        {
            try
            {

                string modifieddate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                SqlCommand cmd = new SqlCommand("Update branching SET Status='" + Branchings.Status + "',  isDelete='" + Branchings.IsDelete + "', ModifiedDate = '" + modifieddate + "', ModifiedBy = '" + Branchings.ModifiedBy + "'  WHERE BranchCode = '" + Branchings.BranchCode + "'", con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    var results = "Data Existing";
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

        //Inserting Branch User
        [HttpPost]
        [Route("Inserting/Branch/User")]
        public async Task<IActionResult> InsertData(Branching Branchings)
        {
            try
            {
                if(_dbcontext.Branchings.Any(x => x.BranchCode == Branchings.BranchCode && x.EmailId == Branchings.EmailId))
                {
                    return Ok("Data Exisiting");
                }
                else
                {
                    var result = new Branching()
                    {
                        BranchCode = Branchings.BranchCode,
                        BranchId = Branchings.BranchId,
                        UserId = Branchings.UserId,
                        EmailId = Branchings.EmailId,
                        Status = Branchings.Status,
                        CreatedBy = Branchings.CreatedBy,
                    };
                    await _dbcontext.Branchings.AddAsync(Branchings);
                    await _dbcontext.SaveChangesAsync();
                    return Ok("Data Inserted");
                }
                return Ok();

            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }


        //DELETE(update) EXISTING UserBranch
        [HttpPost]
        [Route("Delete/User/Branch")]
        public ActionResult DeleteUserBranch(Branching Branchings)
        {
            try
            {

                string modifieddate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                SqlCommand cmd = new SqlCommand("Update branching SET Status='" + Branchings.Status + "',  isDelete='" + Branchings.IsDelete + "', ModifiedDate = '" + modifieddate + "', ModifiedBy = '" + Branchings.ModifiedBy + "'  WHERE BranchCode = '" + Branchings.BranchCode + "'  AND EmailID = '" + Branchings.EmailId + "'", con);
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

        //DELETE UserBranch 
        [HttpPost]
        [Route("Delete/Users/Branches")]
        public ActionResult DeleteUserBranchs(Branching Branchings)
        {
            try
            {

                string modifieddate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                SqlCommand cmd = new SqlCommand("Update branching SET Status='" + Branchings.Status + "',  isDelete='" + Branchings.IsDelete + "', ModifiedDate = '" + modifieddate + "', ModifiedBy = '" + Branchings.ModifiedBy + "'  WHERE BranchCode = '" + Branchings.BranchCode + "'", con);
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


        //DELETE UserBranch rights only 
        [HttpPost]
        [Route("Delete/UserBranch/rights")]
        public ActionResult DeleteUserBranchRights(Branching Branchings)
        {
            try
            {

                string modifieddate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                SqlCommand cmd = new SqlCommand("Update branching SET Status='" + Branchings.Status + "',  isDelete='" + Branchings.IsDelete + "', ModifiedDate = '" + modifieddate + "', ModifiedBy = '" + Branchings.ModifiedBy + "'  WHERE Id = '" + Branchings.Id + "'", con);
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
