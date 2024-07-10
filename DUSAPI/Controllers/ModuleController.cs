using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using DUSAPI.Models;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using System.Linq;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.AspNetCore.Http;
using System;


namespace DUSAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModuleController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly dusContext _dbcontext;

        public ModuleController(IConfiguration configuration, dusContext _context)
        {
            _dbcontext = _context;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var result = _dbcontext.Menus.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                var results = ex;
                return Ok(results);
            }

        }



        [HttpGet]
        [Route("Menus/Parent")]
        public ActionResult MenusParent()
        {
            try
            {
                var result = _dbcontext.Menus.Where(c => c.Type == "Parent").ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                var results = ex;
                return Ok(results);
            }
        }



        [HttpGet]
        [Route("Menus/Child")]
        public ActionResult MenusChild()
        {
            try
            {
                var result = _dbcontext.Menus.Where(c => c.Type == "Child").ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                var results = ex;
                return Ok(results);
            }

        }


        [HttpGet]
        [Route("Menus")]
        public ActionResult menus()
        {
            try
            {
                var result = _dbcontext.VMenus.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                var results = ex;
                return Ok(results);
            }

        }


        [HttpPost]
        [Route("Inserting/Menus")]
        public async Task<IActionResult> InsertData(Menu1 Menus1)
        {
            try
            {
                if (_dbcontext.Menus1.Any(x => x.Menus == Menus1.Menus && x.EmailId == Menus1.EmailId))
                {
                    return Ok("Data Exisiting");
                }

               else if (Menus1.Menus == 2 || Menus1.Menus == 3 || Menus1.Menus == 4 || Menus1.Menus == 5)
                {
              
                    var result = new Menu1()
                    {
                        Menus = Menus1.Menus,
                        Type = Menus1.Type,
                        AssignSubmenu = Menus1.AssignSubmenu,
                        UserId = Menus1.UserId,
                        EmailId = Menus1.EmailId,
                        Status = Menus1.Status,
                        CreatedBy = Menus1.CreatedBy,

                    };
                    await _dbcontext.Menus1.AddAsync(Menus1);
                    await _dbcontext.SaveChangesAsync();
                    return Ok("Data Inserted into Maintenance");
                }

                else
                {
                    var results = new Menu1()
                    {
                        Menus = Menus1.Menus,
                        Type = Menus1.Type,
                        AssignSubmenu = Menus1.AssignSubmenu,
                        UserId = Menus1.UserId,
                        EmailId = Menus1.EmailId,
                        Status = Menus1.Status,
                        CreatedBy = Menus1.CreatedBy,

                    };
                    await _dbcontext.Menus1.AddAsync(Menus1);
                    await _dbcontext.SaveChangesAsync();
                    return Ok("Data Inserted into Transactions");
                }

            
                

            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //DELETE(update) EXISTING UserMenu
        [HttpPut]
        [Route("Update/User/Menu")]
        public ActionResult UpdateUserMenu(Menu1 Menus1)
        {
            try
            {

                string modifieddate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                SqlCommand cmd = new SqlCommand("Update menus SET Status='" + Menus1.Status + "',  isDelete='" + Menus1.IsDelete + "', ModifiedDate = '" + modifieddate + "', ModifiedBy = '" + Menus1.ModifiedBy + "'  WHERE Menus = '" + Menus1.Menus + "'  AND EmailId = '" + Menus1.EmailId + "'", con);
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










        //Fetch user/submenus
        [HttpGet]
        [Route("Fetch/Email/{email}")]
        public async Task<IActionResult> GetFiles([FromRoute] string email)
        {
            try
            {
                var result = await _dbcontext.VMenus.Where(c => c.Email == email || c.Username == email && c.IsDelete == 0).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            var error = "ERROR";
            return View(error);
        }


        //Fetch User that existing parent menus
        [HttpGet]
        [Route("Fetch/ExistingParentMenus/{username}")]
        public ActionResult fetchParentMenu(string username)
        {
            try
            {
                var menu = _dbcontext.Menus.Where(b =>_dbcontext.VMenus.Any(vb => vb.AssignParent == b.Menus && vb.IsDelete == 0 && vb.Username == username)).ToList();
                return Ok(menu);

            }
            catch (Exception ex)
            {
                var results = ex;
                return Ok(results);
            }
        }

        //Fetch BranchID branch
        [HttpGet]
        [Route("Fetch/MenusID/{Menuname}")]
        public async Task<IActionResult> GetBranch([FromRoute] string Menuname)
        {
            var result = await _dbcontext.Menus.Where(c => c.Menus == Menuname).ToListAsync();
            return Ok(result);
        }



        //Fetch Branch that notexisting on that user.
        [HttpGet]
        [Route("Fetch/NotExistingmenus/{username}")]
        public ActionResult fetchbranch(string username)
        {
            try
            {
                var Branches = _dbcontext.Menus.Where(b => b.IsDelete == 0 && b.Status == "A" && b.Type == "Child"
                && !_dbcontext.VMenus.Any(vb => vb.SubMenu == b.Menus && vb.Status != "I" && vb.IsDelete != 1 && vb.Username == username))
                    .ToList();
                return Ok(Branches);

            }
            catch (Exception ex)
            {
                var results = ex;
                return Ok(results);
            }
        }



        //Fetch email branch/user
        [HttpGet]
        [Route("Fetch/Email/Menu/{email}")]
        public async Task<IActionResult> Getmenu([FromRoute] string email)
        {
            try
            {
                if (_dbcontext.VMenus.Any(x => x.Email == email))
                {
                    if (_dbcontext.UserLists.Any(x => x.Email == email && x.IsDelete != 0))
                    {

                    }
                    else if (_dbcontext.UserLists.Any(x => x.Email == email && x.Status == "I"))
                    {
                        var result = await _dbcontext.VMenus.Where(c => c.Email == email && c.IsDelete == 0).ToListAsync();
                        return Ok(result);
                    }

                    else
                    {
                        var result = await _dbcontext.VMenus.Where(c => c.Email == email && c.IsDelete == 0).ToListAsync();
                        return Ok(result);
                    }

                }
                else if (_dbcontext.VMenus.Any(x => x.Username == email))
                {
                    if (_dbcontext.UserLists.Any(x => x.Username == email && x.IsDelete != 0))
                    {

                    }
                    else if (_dbcontext.UserLists.Any(x => x.Username == email && x.Status == "I"))
                    {
                        var result = await _dbcontext.VMenus.Where(c => c.Username == email && c.IsDelete == 0).ToListAsync();
                        return Ok(result);
                    }
                    else
                    {
                        var result = await _dbcontext.VMenus.Where(c => c.Username == email && c.IsDelete == 0).ToListAsync();
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

        //DELETE UserMenus
        [HttpDelete]
        [Route("Delete/Users/Menus")]
        public ActionResult DeleteUserMenus(Menu1 menu1)
        {
            try
            {

                string modifieddate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                SqlCommand cmd = new SqlCommand("Update menus SET Status='" + menu1.Status + "',  isDelete='" + menu1.IsDelete + "', ModifiedDate = '" + modifieddate + "', ModifiedBy = '" + menu1.ModifiedBy + "'  WHERE Id = '" + menu1.Id + "'", con);
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

        [HttpPut]
        [Route("UpdatingExisting/User/Menus")]
        public ActionResult UpdatingExistingBranch(Menu1 menu1)
        {
            try
            {

                string modifieddate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                SqlCommand cmd = new SqlCommand("Update menus SET Status='" + menu1.Status + "',  isDelete='" + menu1.IsDelete + "', ModifiedDate = '" + modifieddate + "', ModifiedBy = '" + menu1.ModifiedBy + "'  WHERE Id = '" + menu1.Id + "'", con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    var results = "Data Updated";
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
                var results = "ERROR";
                return Ok(results);
            }
        }


        //Inserting Buttons User
        [HttpPost]
        [Route("Inserting/Button/User")]
        public async Task<IActionResult> InsertButton(Button Buttons)
        {
            try
            {
                if(_dbcontext.Buttons.Any(x => x.Menu == Buttons.Menu && x.EmailId == Buttons.EmailId))
                {
                    return Ok("Data Existing");
                }
                else
                {
                    var result = new Button()
                    {
                        Menu = Buttons.Menu,
                        UserId = Buttons.UserId,
                        EmailId = Buttons.EmailId,
                        CreatedBy = Buttons.CreatedBy,

                    };
                    await _dbcontext.Buttons.AddAsync(Buttons);
                    await _dbcontext.SaveChangesAsync();
                    return Ok("Data Inserted");
                }
         

                


            
            }

            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //FETCH BUTTON USER
        [HttpGet]
        [Route("Fetch/Button/User/{username}")]
        public ActionResult FetchButton(string username)
        {
            try
            {
                var result = _dbcontext.VButtons.Where(x => x.Username == username && x.Status == "A").ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                var results = ex;
                return Ok(results);
            }

        }

        //UPDATE/DELETE BUTTON USER
        [HttpPut]
        [Route("Update/Delete/ButtonUser")]
        public ActionResult UpdateDeleteButtonUser(Button Buttons)
        {
            try
            {

                string modifieddate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                SqlCommand cmd = new SqlCommand("Update button SET ModifiedDate = '" + modifieddate + "', ModifiedBy = '" + Buttons.ModifiedBy + "', Status = '" + Buttons.Status + "'  WHERE Id = '" + Buttons.Id + "'  AND EmailID = '" + Buttons.EmailId + "'", con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    var results = "Data Updated";
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
                var results = "ERROR";
                return Ok(results);
            }
        }


        //UPDATE/DELETED BUTTON USER
        [HttpPut]
        [Route("Update/Deleted/ButtonUser")]
        public ActionResult UpdateDeletedButtonUser(Button Buttons)
        {
            try
            {

                string modifieddate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                SqlCommand cmd = new SqlCommand("Update button SET ModifiedDate = '" + modifieddate + "', ModifiedBy = '" + Buttons.ModifiedBy + "', Status = '" + Buttons.Status + "'  WHERE Menu = '" + Buttons.Menu + "'  AND EmailID = '" + Buttons.EmailId + "'", con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    var results = "Data Updated";
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
                var results = "ERROR";
                return Ok(results);
            }
        }









        //INSERT ISADD BUTTON
        [HttpPut]
        [Route("Insert/Button/IsAdd")]
        public ActionResult InsertButtonIsAdd(Button Buttons)
        {
            try
            {

                string modifieddate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                SqlCommand cmd = new SqlCommand("Update button SET IsAdd='" + Buttons.IsAdd + "', ModifiedDate = '" + modifieddate + "', ModifiedBy = '" + Buttons.ModifiedBy + "'  WHERE Id = '" + Buttons.Id + "'  AND EmailID = '" + Buttons.EmailId + "'", con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    var results = "Data Updated";
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
                var results = "ERROR";
                return Ok(results);
            }
        }



        //INSERT ISEDIT BUTTON
        [HttpPut]
        [Route("Insert/Button/IsEdit")]
        public ActionResult InsertButtonIsEdit(Button Buttons)
        {
            try
            {

                string modifieddate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                SqlCommand cmd = new SqlCommand("Update button SET IsEdit='" + Buttons.IsEdit + "', ModifiedDate = '" + modifieddate + "', ModifiedBy = '" + Buttons.ModifiedBy + "'  WHERE Id = '" + Buttons.Id + "'  AND EmailID = '" + Buttons.EmailId + "'", con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    var results = "Data Updated";
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
                var results = "ERROR";
                return Ok(results);
            }
        }



        //INSERT ISDELETE BUTTON
        [HttpPut]
        [Route("Insert/Button/IsDelete")]
        public ActionResult InsertButtonIsDelete(Button Buttons)
        {
            try
            {

                string modifieddate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                SqlCommand cmd = new SqlCommand("Update button SET IsDelete='" + Buttons.IsDelete + "', ModifiedDate = '" + modifieddate + "', ModifiedBy = '" + Buttons.ModifiedBy + "'  WHERE Id = '" + Buttons.Id + "'  AND EmailID = '" + Buttons.EmailId + "'", con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    var results = "Data Updated";
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
                var results = "ERROR";
                return Ok(results);
            }
        }



        //Inserting Buttons Userole
        [HttpPost]
        [Route("Insert/Button/Userole")]
        public ActionResult InsertButtonUserrole(Button Buttons)
        {
            try
            {
                if (_dbcontext.Buttons.Any(x => x.Id == Buttons.Id && x.EmailId == Buttons.EmailId))
                {
                    if (_dbcontext.Buttons.Any(x => x.IsAdd == 0))
                    {
                        return Ok("IsAdd");

                    }
                    else if (_dbcontext.Buttons.Any(x => x.IsEdit == 0))
                    {
                        return Ok("IsEdit");
                    }
                    else if (_dbcontext.Buttons.Any(x => x.IsDelete == 0))
                    {

                        return Ok("IsDelete");
                    }
                    else
                    {
                        return Ok("Completed");
                    }

                }
                return Ok("error");
            }

            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }


        //FETCH BUTTON USER
        [HttpGet]
        [Route("Fetch/Button/User/{username}/{menu}")]
        public ActionResult FetchButtons(string username , string menu)
        {
            try
            {
                var result = _dbcontext.VButtons.Where(x => x.Username == username && x.Menu == menu).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                var results = ex;
                return Ok(results);
            }

        }




    }
}
