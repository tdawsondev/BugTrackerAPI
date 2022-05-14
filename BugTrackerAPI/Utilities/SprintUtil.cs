﻿using BugTrackerAPI.Models;
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
using Newtonsoft.Json.Linq;

namespace BugTrackerAPI.Utilities
{
    public class SprintUtil
    {

        public static JObject GetParent(Guid sprintID)
        {
            try
            {
                string query = "SELECT * FROM public.project WHERE id = (SELECT public.sprint.project_id FROM public.sprint WHERE id = @id);";
                DataTable table = new DataTable();

                List<NpgsqlParameter> sqlParams = new List<NpgsqlParameter>();
                sqlParams.Add(new NpgsqlParameter("@id", sprintID));

                table = (DataAccess.ExecuteQueryAndReturnTable(query, sqlParams));


                return JObject.Parse(JsonConvert.SerializeObject(table));
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
