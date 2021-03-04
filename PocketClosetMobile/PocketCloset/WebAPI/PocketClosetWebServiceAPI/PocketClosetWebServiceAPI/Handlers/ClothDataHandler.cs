using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using PocketCloset.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PocketClosetWebServiceAPI.Handlers
{
    public class ClothDataHandler : Cloth, IClothDataHandler
    {

        private readonly IConfiguration config;

        public ClothDataHandler(IConfiguration config)
        {
            this.config = config;
        }
        public bool createCloth()
        {
            return saveCloth("create_cloth");
        }

        public List<Cloth> getAllClothes(int userId)
        {
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            List<Cloth> clothes = new List<Cloth>();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = "get_all_clothes";
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", userId));
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Cloth cloth = getClothFromReader(reader);
                    clothes.Add(cloth);
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
            return clothes;
        }

        private Cloth getClothFromReader(MySqlDataReader reader) {
            Cloth cloth = new Cloth();
            cloth.clothId = Int32.Parse(reader["cloth_id"].ToString());
            cloth.userId = Int32.Parse(reader["user_id"].ToString());
            cloth.clothType = reader["cloth_type"].ToString();
            cloth.color = reader["color"].ToString();
            cloth.season = reader["season"].ToString();
            cloth.material = reader["material"].ToString();
            cloth.clothPicture = Encoding.UTF8.GetString((byte[])reader["cloth_picture"]);            
            return cloth;
        }

        public Cloth getCloth(int clothId)
        {
            Cloth cloth = null;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                conn.Open();
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = "get_cloth";
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("_cloth_id", clothId));
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                while (reader.Read()){
                    cloth = getClothFromReader(reader);
                }
            }
            catch (Exception ex){
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return cloth;
        }

        public bool updateCloth(){
            return saveCloth("update_cloth");
        }

        private bool saveCloth(string command) {
            bool response = false;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try{
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = command;
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("_cloth_id", this.clothId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", this.userId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_cloth_type", this.clothType));
                mySqlCommand.Parameters.Add(new MySqlParameter("_cloth_picture", this.clothPicture));
                mySqlCommand.Parameters.Add(new MySqlParameter("_season", this.season));
                mySqlCommand.Parameters.Add(new MySqlParameter("_material", this.material));
                mySqlCommand.Parameters.Add(new MySqlParameter("_color", this.color));
                mySqlCommand.Parameters.Add(new MySqlParameter("_response", 0));
                mySqlCommand.Parameters["_response"].Direction = ParameterDirection.Output;
                mySqlCommand.ExecuteNonQuery();
                var result = mySqlCommand.Parameters["_response"].Value;
                //if result is 1, it means stored procedure ran successfully without any error 
                if (Convert.ToInt32(result) == 1){
                    response = true;
                }
            }
            catch (Exception ex){
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return response;
            }
            finally{
                conn.Close();           //closing DB connection
            }
            return response;
        }

        public Cloth createNewCloth()
        {
            Cloth cloth = null;
            string connectionString = config.GetConnectionString("DefaultConnection");
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = "create_new_cloth";
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(new MySqlParameter("_user_id", this.userId));
                mySqlCommand.Parameters.Add(new MySqlParameter("_cloth_type", this.clothType));
                mySqlCommand.Parameters.Add(new MySqlParameter("_cloth_picture", this.clothPicture));
                mySqlCommand.Parameters.Add(new MySqlParameter("_season", this.season));
                mySqlCommand.Parameters.Add(new MySqlParameter("_material", this.material));
                mySqlCommand.Parameters.Add(new MySqlParameter("_color", this.color));
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    cloth = getClothFromReader(reader);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return default(Cloth);
            }
            finally
            {
                conn.Close();           //closing DB connection
            }
            return cloth;
        }
    }
}
