using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using PocketCloset.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketClosetWebServiceAPI.Handlers
{
    public class ProfilePictureDataHandler : ProfilePicture, IProfilePictureDataHandler
    {
        private readonly IConfiguration config;

        public ProfilePictureDataHandler(IConfiguration config)
        {
            this.config = config;
        }

        public bool createProfilePicture()
        {
            return saveProfilePicture("create_profile_picture");
        }

        public bool deleteProfilePicture(int userId)
        {
            bool response = false;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = "delete_profile_picture";
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", this.userId));
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

        private ProfilePicture getProfilePictureFromReader(MySqlDataReader reader)
        {
            ProfilePicture profilePicture = new ProfilePicture();
            profilePicture.userId = Int32.Parse(reader["user_id"].ToString());
            profilePicture.profilePicture = Encoding.UTF8.GetString((byte[])reader["profile_picture"]);
            return profilePicture;
        }

        public ProfilePicture getProfilePicture(int userId)
        {
            ProfilePicture profilePicture = null;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                conn.Open();
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = "get_profile_picture";
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", userId));
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    profilePicture = getProfilePictureFromReader(reader);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return profilePicture;
        }

        public bool updateProfilePicture()
        {
            return saveProfilePicture("update_profile_picture");
        }

        private bool saveProfilePicture(string command) {
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
                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", this.userId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_profile_picture", this.profilePicture));
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
