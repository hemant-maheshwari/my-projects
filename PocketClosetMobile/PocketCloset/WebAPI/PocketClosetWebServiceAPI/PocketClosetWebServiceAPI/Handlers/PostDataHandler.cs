using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using PocketCloset.Models;
using PocketClosetWebServiceAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketClosetWebServiceAPI.Handlers
{
    public class PostDataHandler : Post, IPostDataHandler
    {

        private readonly IConfiguration config;

        public PostDataHandler(IConfiguration config)
        {
            this.config = config;
        }
        public bool createPost()
        {
            return savePost("create_post");
        }

        public List<Post> getAllPosts(int userId)
        {
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            List<Post> posts = new List<Post>();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = "get_all_posts";
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", userId));
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Post post = getPostFromReader(reader);
                    posts.Add(post);
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
            return posts;
        }

        private Post getPostFromReader(MySqlDataReader reader)
        {
            Post post = new Post();
            post.postId = Int32.Parse(reader["post_id"].ToString());
            post.userId = Int32.Parse(reader["user_id"].ToString());
            post.clothId = Int32.Parse(reader["cloth_id"].ToString());
            post.isModelPresent = getBoolFromInt(Int32.Parse(reader["model_present"].ToString()));
            post.price = Double.Parse(reader["price"].ToString());
            post.url = reader["url"].ToString();
            return post;
        }

        public Post getPost(int postId)
        {
            Post post = null;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                conn.Open();
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = "get_post";
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("_post_id", postId));
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    post = getPostFromReader(reader);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return post;
        }

        private bool savePost(string command)
        {
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
                mySqlCommand.Parameters.Add(new MySqlParameter("_cloth_id", this.clothId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", this.userId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_post_id", this.postId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_model_present", getIntFromBool(this.isModelPresent)));
                mySqlCommand.Parameters.Add(new MySqlParameter("_price", this.price));
                mySqlCommand.Parameters.Add(new MySqlParameter("_url", this.url));
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

        private bool getBoolFromInt(int value) {
            return value == 1;
        }

        private int getIntFromBool(bool flag) {
            if (flag) return 1;
            else return 0;
        }

        public Post createNewPost()
        {
            Post post = null;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = "create_new_post";
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("_cloth_id", this.clothId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", this.userId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_post_id", this.postId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_model_present", getIntFromBool(this.isModelPresent)));
                mySqlCommand.Parameters.Add(new MySqlParameter("_price", this.price));
                mySqlCommand.Parameters.Add(new MySqlParameter("_url", this.url));
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    post = getPostFromReader(reader);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return default(Post);
            }
            finally
            {
                conn.Close();           //closing DB connection
            }
            return post;
        }

        public List<FeedViewModel> getAllFeeds(int userId)
        {
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            List<FeedViewModel> feeds = new List<FeedViewModel>();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = "get_all_feeds";
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", userId));
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    FeedViewModel feed = getFeedViewModelFromReader(reader);
                    feeds.Add(feed);
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
            return feeds;
        }

        private FeedViewModel getFeedViewModelFromReader(MySqlDataReader reader) {
            FeedViewModel feedViewModel = new FeedViewModel();
            feedViewModel.username = reader["username"].ToString();
            feedViewModel.userProfiePicture = Encoding.UTF8.GetString((byte[])reader["profile_picture"]);
            feedViewModel.clothPicture = Encoding.UTF8.GetString((byte[])reader["cloth_picture"]);
            feedViewModel.datePosted = reader["date_posted"].ToString();
            return feedViewModel;
        }

    }
}
