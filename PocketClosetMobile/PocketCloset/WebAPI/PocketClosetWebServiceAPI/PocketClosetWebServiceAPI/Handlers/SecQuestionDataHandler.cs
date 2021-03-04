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
    public class SecQuestionDataHandler : SecQuestion, ISecQuestionDataHandler
    {
        private readonly IConfiguration config;

        public SecQuestionDataHandler(IConfiguration config)
        {
            this.config = config;
        }

        public bool createSecQuestion()
        {
            return saveSecQuestion("create_sec_question");
        }

        public SecQuestion getSecQuestion(int userId)
        {
            SecQuestion secQuestion = null;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                conn.Open();
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = "get_sec_question";
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", userId));
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    secQuestion = getSecQuestionFromReader(reader);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return secQuestion;
        }

        private SecQuestion getSecQuestionFromReader(MySqlDataReader reader)
        {
            SecQuestion secQuestion = new SecQuestion();
            secQuestion.queId = Int32.Parse(reader["que_id"].ToString());
            secQuestion.userId = Int32.Parse(reader["user_id"].ToString());
            secQuestion.question = reader["question"].ToString();
            secQuestion.answer = reader["answer"].ToString();
            return secQuestion;
        }
        

        private bool saveSecQuestion(string command) {
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
                mySqlCommand.Parameters.Add(new MySqlParameter("_que_id", this.queId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", this.userId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_question", this.question));
                mySqlCommand.Parameters.Add(new MySqlParameter("_answer", this.answer));
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
