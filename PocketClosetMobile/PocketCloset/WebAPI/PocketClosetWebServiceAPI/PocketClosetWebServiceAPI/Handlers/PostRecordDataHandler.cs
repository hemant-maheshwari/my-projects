using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using PocketCloset.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PocketClosetWebServiceAPI.Handlers
{
    public class PostRecordDataHandler : PostRecord, IPostRecordDataHandler
    {
        private readonly IConfiguration config;

        public PostRecordDataHandler(IConfiguration config)
        {
            this.config = config;
        }
        public bool createPostRecord()
        {
            return savePostRecord("create_post_record");
        }

        public bool deletePostRecord(int postRecordId)
        {
            bool response = false;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = "delete_post_record";
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("_post_record_id", postRecordId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_response", 0));
                mySqlCommand.Parameters["_response"].Direction = ParameterDirection.Output;
                mySqlCommand.ExecuteNonQuery();
                var result = mySqlCommand.Parameters["_response"].Value;
                //if result is 1, it means stored procedure ran successfully without any error 
                if (Convert.ToInt32(result) == 1)
                {
                    response = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return response;
            }
            finally
            {
                conn.Close();           //closing DB connection
            }
            return response;
        }

        public List<PostRecord> getAllPostRecords(int userId)
        {
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            List<PostRecord> postRecords = new List<PostRecord>();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = "get_all_post_records";
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", userId));
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    PostRecord postRecord = getPostRecordFromReader(reader);
                    postRecords.Add(postRecord);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();           //closing DB connection
            }
            return postRecords;
        }

        private PostRecord getPostRecordFromReader(MySqlDataReader reader)
        {
            PostRecord postRecord = new PostRecord();
            postRecord.postRecordId = Int32.Parse(reader["post_record_id"].ToString());
            postRecord.userId = Int32.Parse(reader["user_id"].ToString());
            postRecord.postId = Int32.Parse(reader["post_id"].ToString());
            postRecord.datePosted = reader["date_posted"].ToString();
            return postRecord;
        }

        public bool savePostRecord(string command) {
            bool response = false;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = command;
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("_post_record_id", this.postRecordId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", this.userId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_post_id", this.postId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_date_posted", this.datePosted));
                mySqlCommand.Parameters.Add(new MySqlParameter("_response", 0));
                mySqlCommand.Parameters["_response"].Direction = ParameterDirection.Output;
                mySqlCommand.ExecuteNonQuery();
                var result = mySqlCommand.Parameters["_response"].Value;
                //if result is 1, it means stored procedure ran successfully without any error 
                if (Convert.ToInt32(result) == 1)
                {
                    response = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return response;
            }
            finally
            {
                conn.Close();           //closing DB connection
            }
            return response;
        }
    }
}
