using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using DUSAPI.Models;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.CodeAnalysis.Scripting;
using System.Linq;
using System;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using static System.Net.WebRequestMethods;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DUSAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly dusContext _dbcontext;



        public UserController(IConfiguration configuration, dusContext _context)
        {

            _dbcontext = _context;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var result = _dbcontext.VUserlists.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                var results = ex;
                return Ok(results);
            }

        }
/*        [HttpGet]
        [Route("a/a")]
        public IActionResult Gets()
        {
            try
            {
                var result = PasswordEncryptor.HashPassword("heheehe");
                return Ok(result);
            }
            catch (Exception ex)
            {
                var results = ex;
                return Ok(results);
            }

        }*/
        [HttpGet]
        [Route("Count/User")]
        public ActionResult Count()
        {
            try
            {
                var result = _dbcontext.VUserlists.Where(c => c.IsDelete == 0).Count();
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
        [Route("isDelete/Userlist")]
        public ActionResult IsDelete()
        {
            try
            {
                var result = _dbcontext.VUserlists.Where(c => c.IsDelete == 0).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                var results = ex;
                return Ok(results);
            }

        }

        //Fetch user/email ID
        [HttpGet]
        [Route("Fetch/Email/{email}")]
        public async Task<IActionResult> GetFiles([FromRoute] string email)
        {
            var result = await _dbcontext.VUserlists.Where(c => c.Email == email || c.Username == email).ToListAsync();
            return Ok(result);
        }


        //Register Account in Userlist
        [HttpPost]
        [Route("Register/userlist/User")]
        public string InsertData(UserList UserLists)
        {
            try
            {
                if (_dbcontext.UserLists.Any(x => x.Email == UserLists.Email))
                {
                    return "Email Already Registered";
                }
                else if (_dbcontext.UserLists.Any(x => x.Username == UserLists.Username))
                {
                    return "Username Already Registered";
                }
                /*else if (UserPersons.Password.Length == 8)
                {
                 return "Minimum 8 password.";
                }*/
                else
                {
                    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                    SqlCommand cmd = new SqlCommand("Insert into user_list( Username, Password , Email , Lastname , Firstname , Middlename , ContactNumber , Address , Status, CreatedBy ,UserType, AccountType) values ('" + UserLists.Username + "','" + BCrypt.Net.BCrypt.HashPassword(UserLists.Password) + "','" + UserLists.Email + "', '" + UserLists.Lastname + "','" + UserLists.Firstname + "','" + UserLists.Middlename + "','" + UserLists.ContactNumber + "','" + UserLists.Address + "','" + UserLists.Status + "','" + UserLists.CreatedBy + "','" + UserLists.UserType + "','" + UserLists.AccountType + "')", con);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        return "Successfully registered";
                    }
                    else
                    {
                        return "Not Successfully registered";
                    }
                }
            }

            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        //Update  Account Settings User
        [HttpPost]
        [Route("Update/AccountSetting/User")]
        public string AccountSetting(UserList UserLists)
        {
            try
            {
                if(UserLists.Password != UserLists.ConfirmPassword)
                {
                    return "Password Doesn't Match";
                }
                else
                {
                    string modifieddate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                    SqlCommand cmd = new SqlCommand("Update user_list SET Password='" + BCrypt.Net.BCrypt.HashPassword(UserLists.Password) + "',Status = '" + UserLists.Status + "', ModifiedDate = '" + modifieddate + "', ModifiedBy = '" + UserLists.ModifiedBy + "' WHERE ID = '" + UserLists.Id + "'", con);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        return "Successfully Updated";
                    }
                    else
                    {
                        return "Not Successfully Updated";
                    }
                }
            }

            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        //Login Account
        [HttpPost]
        [Route("Login/User")]
        public ActionResult login(UserList UserLists)
        {
            try
            {
                var username = _dbcontext.UserLists.SingleOrDefault(x => x.Username == UserLists.Username && x.IsDelete == 0 );
                var email = _dbcontext.UserLists.SingleOrDefault(x => x.Email == UserLists.Email && x.IsDelete == 0 );
     

                if (email == null && username == null)
                {
                    var resultsss = "No existing users";
                    return Ok(resultsss);
                }


                else if (email != null)
                {
                    var active = _dbcontext.UserLists.SingleOrDefault(x => x.Email == UserLists.Email && x.Status == "I");
                
                    bool isValidPassword = BCrypt.Net.BCrypt.Verify(UserLists?.Password, email?.Password);


                    if (isValidPassword)
                    {
                      if(active != null)
                        {
                            var response = "Inactive Account";
                            return Ok(response);



                        }
                        else
                        {
                            var response = "Correct Details";
                            return Ok(response);
                        }

             
                    }
                    else if(!isValidPassword)
                    {
                        var response = "Incorrect Password";
                        return Ok(response);
                    }
                    else
                    {
                        var response = "Incorrect details";
                        return Ok(response);
                    }
                }

                else if (username != null)
                {
                    var inactive = _dbcontext.UserLists.SingleOrDefault(x => x.Username == UserLists.Email && x.Status == "I");
                    bool isValidPasswords = BCrypt.Net.BCrypt.Verify(UserLists?.Password, username?.Password);

                    if (isValidPasswords)
                    {
                        if (inactive != null)
                        {
                            var response = "Inactive Account";
                            return Ok(response);


                        }
                        else
                        {
                            var response = "Correct Details";
                            return Ok(response);

                        }

                    }
                    else if(!isValidPasswords)
                    {
                        var response = "Incorrect Password";
                        return Ok(response);
                    }
                    else
                    {
                        var response = "Incorrect details";
                        return Ok(response);
                    }
                }

            }
            catch (Exception ex)
            {
                var response = ex.Message;
                return Ok(response);
            }

            return null;
        }




        //DELETE DATA
        [HttpPost]
        [Route("Delete/Userlist/Table")]
        public ActionResult Delete(UserList UserLists)
        {
            try
            {

                string modifieddate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                SqlCommand cmd = new SqlCommand("Update user_list SET Status = '" + UserLists.Status + "',  isDelete='" + UserLists.IsDelete + "', ModifiedDate = '" + modifieddate + "', ModifiedBy = '" + UserLists.ModifiedBy + "' WHERE ID = '" + UserLists.Id + "'", con);
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



        //UPDATE DATA
        [HttpPost]
        [Route("Update/Userlist/Table")]
        public ActionResult Update(UserList UserLists)
        {

            try
            {
                string modifieddate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sql-con").ToString());
                SqlCommand cmd = new SqlCommand("Update user_list SET Username= '" + UserLists.Username + "', Email = '" + UserLists.Email + "', Lastname = '" + UserLists.Lastname + "', Firstname = '" + UserLists.Firstname + "', Middlename = '" + UserLists.Middlename + "', ContactNumber = '" + UserLists.ContactNumber + "', Address = '" + UserLists.Address + "', UserType = '" + UserLists.UserType + "', AccountType = '" + UserLists.AccountType + "', Status = '" + UserLists.Status + "', ModifiedDate = '" + modifieddate + "', ModifiedBy = '" + UserLists.ModifiedBy + "' WHERE ID = '" + UserLists.Id + "'", con);
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
        


        //EMAIL NOTIFICATION
        [HttpPost("EmailNotification")]
        public async Task<IActionResult> SendEmaiNotification([FromForm] string usermail)
        {
            Random _rdm = new Random();
            var otp = Convert.ToInt16(_rdm.Next(1000, 9999)).ToString();

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("fscdontreply@gmail.com"));
            email.To.Add(MailboxAddress.Parse(usermail));
            email.Subject = otp + " is your FSC-Accreditation code ";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = "<p>Good day,<br>" + "<br><p style = \"font-size: 15px;\"> Your One-Time PIN (OTP) for FSC-Accreditation is " + "<b>" + otp + ". </b> This is valid for 3 minutes.</p>" +
                "<p style= \"text-align: left;\"\"color: darkgray;\">" +
                "----<BR> Thank you, <BR> VASO System <BR><I style=\"font-size: 9px;\">Fast Services - IT</I></p>"
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("fscdontreply@gmail.com", "xdyheubvngtspxhh");
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok("Email sent");
        }

    }
}

