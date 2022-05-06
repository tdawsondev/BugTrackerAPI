using BugTrackerAPI.Models;
using BugTrackerAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                string query = " SELECT * FROM public.project ORDER BY name ASC;";
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
                string query = " SELECT * FROM public.project WHERE id = @id";
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
        public IActionResult Create(Project project)
        {
            try
            {
                string query = "CALL public.create_project(@name, @description, @createdBy)";
                DataTable table = new DataTable();

                List<NpgsqlParameter> sqlParams = new List<NpgsqlParameter>();
                sqlParams.Add(new NpgsqlParameter("@name", project.Name));
                sqlParams.Add(new NpgsqlParameter("@description", project.Description));
                sqlParams.Add(new NpgsqlParameter("@createdBy", project.Created_By_Id));


                table = DataAccess.ExecuteQueryAndReturnTable(query, sqlParams);
                return Ok(JsonConvert.SerializeObject(table));
            }
            catch (Exception ex)
            {
                return BadRequest("An error was encountered: " + ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update(Project project)
        {
            try
            {
                string query = "UPDATE public.project SET name=@name, description=@description, created_by_id=@createdBy WHERE id = @id;";
                DataTable table = new DataTable();

                List<NpgsqlParameter> sqlParams = new List<NpgsqlParameter>();
                sqlParams.Add(new NpgsqlParameter("@name", project.Name));
                sqlParams.Add(new NpgsqlParameter("@description", project.Description));
                sqlParams.Add(new NpgsqlParameter("@createdBy", project.Created_By_Id));
                sqlParams.Add(new NpgsqlParameter("@id", project.ID));


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
                string query = @"DELETE FROM public.project WHERE id = @id";
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
        [Route("GetProjectUsers")]
        public IActionResult GetProjectUsers(Guid id)
        {
            try
            {
                string query = "SELECT * FROM public.get_project_users(@id);";
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
        [Route("AddUserToProject")]
        public IActionResult AddUserToProject(ProjectUsers pu)
        {
            try
            {
                string query = "INSERT INTO public.project_users (id, project_id, user_id) VALUES (default, @pId, @uId);";
                DataTable table = new DataTable();
                List<NpgsqlParameter> sqlParams = new List<NpgsqlParameter>();
                sqlParams.Add(new NpgsqlParameter("@pId", pu.Project_id));
                sqlParams.Add(new NpgsqlParameter("@uId", pu.User_id));

                table = (DataAccess.ExecuteQueryAndReturnTable(query, sqlParams));
                return Ok(JsonConvert.SerializeObject(table));
            }
            catch (Exception ex)
            {
                return BadRequest("An error was encountered: " + ex.Message);
            }

        }
        [HttpDelete]
        [Route("RemoveUserFromProject")]
        public IActionResult RemoveUserFromProject(ProjectUsers pu)
        {
            try
            {
                string query = "DELETE FROM public.project_users WHERE project_id = @pId AND user_id = @uId;";
                DataTable table = new DataTable();
                List<NpgsqlParameter> sqlParams = new List<NpgsqlParameter>();
                sqlParams.Add(new NpgsqlParameter("@pId", pu.Project_id));
                sqlParams.Add(new NpgsqlParameter("@uId", pu.User_id));

                table = (DataAccess.ExecuteQueryAndReturnTable(query, sqlParams));
                return Ok(JsonConvert.SerializeObject(table));
            }
            catch (Exception ex)
            {
                return BadRequest("An error was encountered: " + ex.Message);
            }

        }

        [HttpGet]
        [Route("GetSprints/{id}")]
        public IActionResult GetAllSprintsForProject(Guid id)
        {
            try
            {
                string query = " SELECT * FROM public.sprint WHERE project_id = @id";
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



    }
}
