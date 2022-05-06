using BugTrackerAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BugTrackerAPI.Models;

namespace BugTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                string query = " SELECT * FROM public.login ORDER BY user_name ASC;";
                DataTable table = new DataTable();
                table = (DataAccess.ExecuteQueryAndReturnTable(query));
                return Ok(JsonConvert.SerializeObject(table));
            }
            catch (Exception ex)
            {
                return BadRequest("An error was encountered: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("Get/{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                string query = " SELECT * FROM public.login WHERE id = @id";
                DataTable table = new DataTable();

                List<NpgsqlParameter> sqlParams = new List<NpgsqlParameter>();
                sqlParams.Add(new NpgsqlParameter("@id", id));

                table = (DataAccess.ExecuteQueryAndReturnTable(query, sqlParams));
                return Ok(JsonConvert.SerializeObject(table));
            }
            catch (Exception ex)
            {
                return BadRequest("An error was encountered: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(Login login)
        {
            try
            {
                string query = "INSERT INTO public.login (id, user_name, password) VALUES (DEFAULT, @userName, @password);";
                DataTable table = new DataTable();

                List<NpgsqlParameter> sqlParams = new List<NpgsqlParameter>();
                sqlParams.Add(new NpgsqlParameter("@userName", login.User_Name));
                sqlParams.Add(new NpgsqlParameter("@password", login.Password));


                table = DataAccess.ExecuteQueryAndReturnTable(query, sqlParams);
                return Ok(JsonConvert.SerializeObject(table));
            }
            catch (Exception ex)
            {
                return BadRequest("An error was encountered: " + ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update(Login login)
        {
            try
            {
                string query = "UPDATE public.login SET user_name=@userName, password=@password WHERE id = @id;";
                DataTable table = new DataTable();

                List<NpgsqlParameter> sqlParams = new List<NpgsqlParameter>();
                sqlParams.Add(new NpgsqlParameter("@userName", login.User_Name));
                sqlParams.Add(new NpgsqlParameter("@password", login.Password));
                sqlParams.Add(new NpgsqlParameter("@id", login.User_Id));


                table = DataAccess.ExecuteQueryAndReturnTable(query, sqlParams);
                return Ok(JsonConvert.SerializeObject(table));
            }
            catch (Exception ex)
            {
                return BadRequest("An error was encountered: " + ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            try
            {
                string query = @"DELETE FROM public.login WHERE id = @id";
                DataTable table = new DataTable();
                List<NpgsqlParameter> sqlParams = new List<NpgsqlParameter>();
                sqlParams.Add(new NpgsqlParameter("@id", id));

                return Ok(JsonConvert.SerializeObject(DataAccess.ExecuteQueryAndReturnTable(query, sqlParams)));
            }
            catch (Exception ex)
            {
                return BadRequest("An error was encountered: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("GetActiveProjects/{id}")]
        public IActionResult GetActiveProjects(Guid id)
        {
            try
            {
                string query = "SELECT * FROM public.get_user_projects(@id);";
                DataTable table = new DataTable();
                List<NpgsqlParameter> sqlParams = new List<NpgsqlParameter>();
                sqlParams.Add(new NpgsqlParameter("@id", id));

                table = (DataAccess.ExecuteQueryAndReturnTable(query, sqlParams));
                return Ok(JsonConvert.SerializeObject(table));
            }
            catch (Exception ex)
            {
                return BadRequest("An error was encountered: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("TryLogin")]
        public IActionResult TryLogin(Login login)
        {
            try
            {
                string query = " SELECT id, user_name FROM public.login WHERE user_name = @userName AND password = @password";
                DataTable table = new DataTable();

                List<NpgsqlParameter> sqlParams = new List<NpgsqlParameter>();
                sqlParams.Add(new NpgsqlParameter("@userName", login.User_Name));
                sqlParams.Add(new NpgsqlParameter("@password", login.Password));

                table = (DataAccess.ExecuteQueryAndReturnTable(query, sqlParams));
                JArray result = JArray.Parse(JsonConvert.SerializeObject(table));
                string returnValue = "Invaild Login";
                if (result.HasValues)
                {
                    return Ok(result[0].ToString());
                }
                result.Add(JObject.Parse("{ error: \'Invalid Login\'}"));
                return Ok(result[0].ToString());
            }
            catch (Exception ex)
            {
                return BadRequest("An error was encountered: " + ex.Message);
            }
        }




    }
}
