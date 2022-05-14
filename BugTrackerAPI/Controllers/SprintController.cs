using BugTrackerAPI.Models;
using BugTrackerAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class SprintController : ControllerBase
    {

        [HttpGet]
        [Route("Get/{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                string query = " SELECT * FROM public.sprint WHERE id = @id";
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
        [Route("GetWithProjectData/{id}")]
        public IActionResult GetWithProjectData(Guid id)
        {
            try
            {
                string query = "SELECT sprint.id, sprint.name, sprint.description, sprint.status, sprint.project_id, sprint.start_date, sprint.end_date, project.name AS project_name, project.description AS project_description" +
                    " FROM public.sprint INNER JOIN public.project ON public.sprint.project_id = public.project.id WHERE public.sprint.id = @id";
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
        public IActionResult Create(Sprint sprint)
        {
            try
            {
                string query = "INSERT INTO public.sprint(id, name, description, status, project_id, start_date, end_date) VALUES (default, @name, @description, @status, @projectId, @startDate, @endDate);";
                DataTable table = new DataTable();

                List<NpgsqlParameter> sqlParams = new List<NpgsqlParameter>();
                sqlParams.Add(new NpgsqlParameter("@name", sprint.Name));
                sqlParams.Add(new NpgsqlParameter("@description", sprint.Description));
                sqlParams.Add(new NpgsqlParameter("@status", sprint.Status));
                sqlParams.Add(new NpgsqlParameter("@projectId", sprint.ParentProject));
                sqlParams.Add(new NpgsqlParameter("@startDate", sprint.StartDate));
                sqlParams.Add(new NpgsqlParameter("@endDate", sprint.EndDate));


                table = DataAccess.ExecuteQueryAndReturnTable(query, sqlParams);
                return Ok(JsonConvert.SerializeObject(table));
            }
            catch (Exception ex)
            {
                return BadRequest("An error was encountered: " + ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update(Sprint sprint)
        {
            try
            {
                string query = "UPDATE public.sprint SET name=@name, description=@description, status=@status, project_id=@projectId, start_date=@startDate, end_date=@endDate WHERE id = @id;";
                DataTable table = new DataTable();

                List<NpgsqlParameter> sqlParams = new List<NpgsqlParameter>();
                sqlParams.Add(new NpgsqlParameter("@id", sprint.Id));
                sqlParams.Add(new NpgsqlParameter("@name", sprint.Name));
                sqlParams.Add(new NpgsqlParameter("@description", sprint.Description));
                sqlParams.Add(new NpgsqlParameter("@status", sprint.Status));
                sqlParams.Add(new NpgsqlParameter("@projectId", sprint.ParentProject));
                sqlParams.Add(new NpgsqlParameter("@startDate", sprint.StartDate));
                sqlParams.Add(new NpgsqlParameter("@endDate", sprint.EndDate));


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
                string query = @"DELETE FROM public.sprint WHERE id = @id";
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
        [Route("GetFeatures/{id}")]
        public IActionResult GetAllFeaturesForSprint(Guid id)
        {
            try
            {
                string query = " SELECT * FROM public.feature WHERE sprint_id = @id";
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
        [Route("GetSprintTree/{id}")]
        public IActionResult GetTreeforSprint(Guid id)
        {
            try
            {

                JArray sprint = GetFeaturesforTree(id);
                Console.WriteLine(sprint.ToString());
                foreach(JObject j in sprint)
                {
                    Guid featureId = Guid.Parse(j["id"].ToString());
                    JArray tasks = GetTasksforTree(featureId);
                    j.Add("Tasks", tasks);
                    foreach(JObject t in tasks)
                    {
                        Guid taskId = Guid.Parse(t["id"].ToString());
                        JArray subtasks = GetSubtasksforTree(taskId);
                        t.Add("Subtasks", subtasks);
                    }
                }
                return Ok(sprint.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest("An error was encountered: " + ex.Message);
            }
        }



        private JArray GetFeaturesforTree(Guid id)
        {
            try
            {
                string query = "SELECT * FROM public.feature WHERE sprint_id = @id";
                DataTable table = new DataTable();

                List<NpgsqlParameter> sqlParams = new List<NpgsqlParameter>();
                sqlParams.Add(new NpgsqlParameter("@id", id));

                table = (DataAccess.ExecuteQueryAndReturnTable(query, sqlParams));


                return  JArray.Parse(JsonConvert.SerializeObject(table));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private JArray GetTasksforTree(Guid id)
        {
            try
            {
                string query = "SELECT public.task.*, public.login.user_name FROM public.task INNER JOIN public.login ON public.task.assigned_userid = public.login.id WHERE parent_featureid = @id";
                DataTable table = new DataTable();

                List<NpgsqlParameter> sqlParams = new List<NpgsqlParameter>();
                sqlParams.Add(new NpgsqlParameter("@id", id));

                table = (DataAccess.ExecuteQueryAndReturnTable(query, sqlParams));


                return JArray.Parse(JsonConvert.SerializeObject(table));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private JArray GetSubtasksforTree(Guid id)
        {
            try
            {
                string query = "SELECT public.subtask.*, public.login.user_name FROM public.subtask INNER JOIN public.login ON public.subtask.assigned_userid = public.login.id WHERE parent_taskid = @id";
                DataTable table = new DataTable();

                List<NpgsqlParameter> sqlParams = new List<NpgsqlParameter>();
                sqlParams.Add(new NpgsqlParameter("@id", id));

                table = (DataAccess.ExecuteQueryAndReturnTable(query, sqlParams));


                return JArray.Parse(JsonConvert.SerializeObject(table));
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        








    }
}
