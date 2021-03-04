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
    public class OutfitDataHandler : Outfit, IOutfitDataHandler
    {
        private readonly IConfiguration config;

        public OutfitDataHandler(IConfiguration config)
        {
            this.config = config;
        }
        public bool createOutfit()
        {
            return saveOutfit("create_outfit");
        }

        public bool deleteOutfit(int outfitId)
        {
            bool response = false;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                //string clothListString = getCommaSepearatedStringFromList(this.clothList);
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = "delete_outfit";
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("_outfit_id", this.outfitId));
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

        public List<Outfit> getAllOutfits(int userId)
        {
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            List<Outfit> outfits = new List<Outfit>();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = "get_all_outfits";
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", userId));
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Outfit outfit = getOutfitFromReader(reader);
                    outfits.Add(outfit);
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
            return outfits;
        }

        private Outfit getOutfitFromReader(MySqlDataReader reader)
        {
            Outfit outfit = new Outfit();
            outfit.outfitId = Int32.Parse(reader["outfit_id"].ToString());
            outfit.userId = Int32.Parse(reader["user_id"].ToString());
            outfit.clothId = Int32.Parse(reader["cloth_list"].ToString());
            outfit.outfitName = reader["outfit_name"].ToString();
            return outfit;
        }

        private OutfitViewModel getOutfitViewModelFromReader(MySqlDataReader reader) {
            OutfitViewModel outfitViewModel = new OutfitViewModel();
            outfitViewModel.outfitName = reader["outfit_name"].ToString();
            outfitViewModel.clothPicString = Encoding.UTF8.GetString((byte[])reader["cloth_picture"]);
            return outfitViewModel;
        }

        public Outfit getOutfit(int outfitId)
        {
            Outfit outfit = null;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = "get_outfit";
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("_outfit_id", outfitId));
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    outfit = getOutfitFromReader(reader);
                    
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
            return outfit;
        }

        public bool updateOutfit()
        {
            return saveOutfit("update_outfit");
        }

        private bool saveOutfit(string command) {
            bool response = false;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try{
                //string clothListString = getCommaSepearatedStringFromList(this.clothList);
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = command;
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("_outfit_id", this.outfitId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", this.userId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_outfit_name", this.outfitName));
                mySqlCommand.Parameters.Add(new MySqlParameter("_cloth_id", this.clothId));
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

        private string getCommaSepearatedStringFromList(List<string> listString) {
            string commaSepearatedString = "";
            for (int i=0; i<listString.Count; i++) {
                commaSepearatedString = commaSepearatedString + listString[i] + ","; 
            }
            return commaSepearatedString.Substring(0, commaSepearatedString.Length-1);
        }

        private List<string> getListFromCommaSeperatedString(string str) {
            List<string> stringList = str.Split(",").ToList<string>();
            return stringList;
        }

        public List<OutfitViewModel> getOutfits(int userId)
        {
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            List<OutfitViewModel> outfits = new List<OutfitViewModel>();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = "get_outfits";
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", userId));
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    OutfitViewModel outfit = getOutfitViewModelFromReader(reader);
                    outfits.Add(outfit);
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
            return outfits;
        }
    }
}
