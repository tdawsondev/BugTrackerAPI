using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BugTrackerAPI.Utilities
{
    public class DataAccess
    {
        public static string GetConnectionString()
        {
            return Startup.StaticConfig.GetConnectionString("LocalConnection"); // String can be "LocalConnection" for on your machine, and "ExternalConnection" for project database
        }

        public static DataTable ExecuteQueryAndReturnTable(string query, List<NpgsqlParameter> sqlParams, int CommandTimeOut = 30)
        {


            DataTable table = new DataTable();
            string sqlDataSource = GetConnectionString();
            NpgsqlDataReader myReader;
            
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.CommandTimeout = CommandTimeOut;
                    foreach (NpgsqlParameter param in sqlParams)
                    {
                        myCommand.Parameters.Add(param);
                    }

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
                return table;
            }

        }
        public static DataTable ExecuteQueryAndReturnTable(string query, int CommandTimeOut = 30)
        {
            return ExecuteQueryAndReturnTable(query, new List<NpgsqlParameter>(), CommandTimeOut);
        }



    }
}
