using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;
using Newtonsoft.Json;
using Npgsql;
using BugTrackerAPI.Models;
using BugTrackerAPI.Utilities;
using System.Data;

namespace BugTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        [Route("Get/{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                string query = " SELECT * FROM public.task WHERE id = @id";
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
        public IActionResult Create(Task task)
        {
            try
            {
                string query = "INSERT INTO public.task (id, name, description, assigned_userid, status, estimated_time, remaining_time, parent_featureid) " +
                    "VALUES (default, @name, @description, @userId, @status, @estTime, @remTime, @featureId);";
                DataTable table = new DataTable();

                List<NpgsqlParameter> sqlParams = new List<NpgsqlParameter>();
                sqlParams.Add(new NpgsqlParameter("@name", task.Name));
                sqlParams.Add(new NpgsqlParameter("@description", task.Description));
                sqlParams.Add(new NpgsqlParameter("@userId", task.AssignedUser));
                sqlParams.Add(new NpgsqlParameter("@status", task.Status));
                sqlParams.Add(new NpgsqlParameter("@estTime", task.EstimatedTime));
                sqlParams.Add(new NpgsqlParameter("@remTime", task.RemainingTime));
                sqlParams.Add(new NpgsqlParameter("@featureId", task.ParentFeature));

                table = DataAccess.ExecuteQueryAndReturnTable(query, sqlParams);
                return Ok(JsonConvert.SerializeObject(table));
            }
            catch (Exception ex)
            {
                return BadRequest("An error was encountered: " + ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update(Task task)
        {
            try
            {
                string query = "UPDATE public.task SET name=@name, description=@description, assigned_userid=@userId, status=@status, estimated_time=@estTime, remaining_time=@remTime, parent_featureid=@featureId WHERE id = @id;";
                DataTable table = new DataTable();

                List<NpgsqlParameter> sqlParams = new List<NpgsqlParameter>();
                sqlParams.Add(new NpgsqlParameter("@id", task.Id));
                sqlParams.Add(new NpgsqlParameter("@name", task.Name));
                sqlParams.Add(new NpgsqlParameter("@description", task.Description));
                sqlParams.Add(new NpgsqlParameter("@userId", task.AssignedUser));
                sqlParams.Add(new NpgsqlParameter("@status", task.Status));
                sqlParams.Add(new NpgsqlParameter("@estTime", task.EstimatedTime));
                sqlParams.Add(new NpgsqlParameter("@remTime", task.RemainingTime));
                sqlParams.Add(new NpgsqlParameter("@featureId", task.ParentFeature));


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
                string query = @"DELETE FROM public.task WHERE id = @id";
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
        [Route("GetSubtasks/{id}")]
        public IActionResult GetAllSubtasksForTask(Guid id)
        {
            try
            {
                string query = " SELECT * FROM public.subtask WHERE parent_taskid = @id";
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
