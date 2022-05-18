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
    public class FeatureController : ControllerBase
    {

        [HttpGet]
        [Route("Get/{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                string query = " SELECT * FROM public.feature WHERE id = @id";
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
        public IActionResult Create(Feature feature)
        {
            try
            {
                string query = "INSERT INTO public.feature (id, name, description, status, sprint_id) VALUES (default, @name, @description, @status, @sprintId);";
                DataTable table = new DataTable();

                List<NpgsqlParameter> sqlParams = new List<NpgsqlParameter>();
                sqlParams.Add(new NpgsqlParameter("@name", feature.Name));
                sqlParams.Add(new NpgsqlParameter("@description", feature.Description));
                sqlParams.Add(new NpgsqlParameter("@status", feature.Status));
                sqlParams.Add(new NpgsqlParameter("@sprintId", feature.ParentSprint));

                table = DataAccess.ExecuteQueryAndReturnTable(query, sqlParams);
                return Ok(JsonConvert.SerializeObject(table));
            }
            catch (Exception ex)
            {
                return BadRequest("An error was encountered: " + ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update(Feature feature)
        {
            try
            {
                string query = "UPDATE public.feature SET name=@name, description=@description, status=@status, sprint_id=@sprintId WHERE id = @id;";
                DataTable table = new DataTable();

                List<NpgsqlParameter> sqlParams = new List<NpgsqlParameter>();
                sqlParams.Add(new NpgsqlParameter("@id", feature.Id));
                sqlParams.Add(new NpgsqlParameter("@name", feature.Name));
                sqlParams.Add(new NpgsqlParameter("@description", feature.Description));
                sqlParams.Add(new NpgsqlParameter("@status", feature.Status));
                sqlParams.Add(new NpgsqlParameter("@sprintId", feature.ParentSprint));


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
                string query = @"DELETE FROM public.feature WHERE id = @id";
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
        [Route("GetTasks")]
        public IActionResult GetAllTasksForFeature(Guid id)
        {
            try
            {
                string query = " SELECT * FROM public.task WHERE parent_featureid = @id";
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
        [HttpGet]
        [Route("GetWithParents/{id}")]
        public IActionResult GetWithParents(Guid id) /// not finished
        {
            try
            {
                string query = @"SELECT feature.* , sprint.name as sprint_name, project.id as projectid, project.name as project_name
                                        FROM ((feature INNER JOIN sprint ON feature.sprint_id = sprint.id)
	                                         INNER JOIN project on project.id = sprint.project_id)
	                                         WHERE feature.id = @id";
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
