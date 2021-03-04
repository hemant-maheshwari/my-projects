using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using PocketCloset.Models;
using PocketClosetWebServiceAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PocketClosetWebServiceAPI.Handlers
{
    public class FollowerDataHandler : Follower, IFollowerDataHandler
    {

        private readonly IConfiguration config;

        public FollowerDataHandler(IConfiguration config)
        {
            this.config = config;
        }
        public bool createFollower()
        {
            return saveFollower("create_follower");
        }

        public bool deleteFollower(int followId)
        {
            bool response = false;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = "delete_follower";
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("_follow_id", this.followId));
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

        public List<FollowViewModel> getFollowers(int userId)
        {
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            List<FollowViewModel> followViewModels = new List<FollowViewModel>();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = "get_all_followers";
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", userId));
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    FollowViewModel followViewModel = getFollowViewModelFromReader(reader);
                    followViewModels.Add(followViewModel);
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
            return followViewModels;
        }

        public List<FollowViewModel> getFollowing(int userId)
        {
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            List<FollowViewModel> followViewModels = new List<FollowViewModel>();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = "get_all_following";
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", userId));
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    FollowViewModel followViewModel = getFollowViewModelFromReader(reader);
                    followViewModels.Add(followViewModel);
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
            return followViewModels;
        }

        private bool saveFollower(string command) {
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
                mySqlCommand.Parameters.Add(new MySqlParameter("_follower_user_id", this.followerUserId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_followed_user_id", this.followedUserId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_response", 0));
                mySqlCommand.Parameters["_response"].Direction = ParameterDirection.Output;
                mySqlCommand.ExecuteNonQuery();
                var result = mySqlCommand.Parameters["_response"].Value;
                //if result is 1, it means stored procedure ran successfully without any error 
                if (Convert.ToInt32(result) == 1){
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

        private FollowViewModel getFollowViewModelFromReader(MySqlDataReader reader) {
            FollowViewModel followViewModel = new FollowViewModel();
            followViewModel.userId = Int32.Parse(reader["user_id"].ToString());
            followViewModel.username = reader["username"].ToString();
            followViewModel.firstName = reader["first_name"].ToString();
            followViewModel.lastName = reader["last_name"].ToString();
            return followViewModel;
        }

    }
}
