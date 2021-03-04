using MySql.Data.MySqlClient;
using StadiumStatsWebAPI.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PocketClosetWebServiceAPI.Services
{
    public class MySQLService
    {

        private string connectionString;

        public MySQLService(string connectionString) {

            this.connectionString = connectionString;
        }

        public void executeQuery(string query) {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try{
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = query;
                mySqlCommand.ExecuteNonQuery();
            } catch (Exception e) {
                Debug.WriteLine(e.Message);
            } finally {
                conn.Close();
            }                  
        }

        public int create(string query) {
            int identity = 0;
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = query;
                identity = Convert.ToInt32(mySqlCommand.ExecuteScalar());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return identity;
        }

        public List<T> getResults<T>(string query) {
            List<T> models = new List<T>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = query;
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                while (reader.Read()) {
                    T model = getModelFromReader<T>(reader);
                    models.Add(model);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return models;
        }

        public T getResult<T>(string query) {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = query;
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    T model = getModelFromReader<T>(reader);
                    return model;
                }
                else {
                    return default(T);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return default(T);
        }

        private T getModelFromReader<T>(MySqlDataReader reader) {
            List<string> columnNames = getFieldNames<T>();
            T model = (T)FormatterServices.GetUninitializedObject(typeof(T));
            FieldInfo[] fieldInfos = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            for (int i=0; i<fieldInfos.Length; i++) {
                if (fieldInfos[i].FieldType.Name.Equals("String"))
                {
                    fieldInfos[i].SetValue(model, reader[columnNames[i]].ToString());
                }
                else if (fieldInfos[i].FieldType.Name.Equals("Int32"))
                {
                    fieldInfos[i].SetValue(model, Convert.ToInt32(reader[columnNames[i]].ToString()));
                }
                else {
                    fieldInfos[i].SetValue(model, reader[columnNames[i]].ToString());
                }                
            }
            return model;
        }

        private List<string> getFieldNames<T>()
        {
            List<string> fieldNames = new List<string>();
            FieldInfo[] fields = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            for (int i = 0; i < fields.Length; i++)
            {
                string fieldName = fields[i].Name.ToString();
                fieldName = fieldName.Substring(1, fieldName.IndexOf('>') - 1);
                fieldNames.Add(fieldName);
            }
            return fieldNames;
        }

        public List<Athlete> getAthletesForUser(string query) {
            List<Athlete> athletes = new List<Athlete>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand();
            try
            {
                conn.Open();    //opening DB connection
                mySqlCommand.Connection = conn;
                mySqlCommand.CommandText = query;
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Athlete athlete = getAthleteFromReader(reader);
                    athletes.Add(athlete);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return athletes;
        }

        private Athlete getAthleteFromReader(MySqlDataReader reader) {
            Athlete athlete = new Athlete();
            athlete.firstName = reader["FIRSTNAME"].ToString();
            athlete.lastName = reader["LASTNAME"].ToString();
            athlete.athleteType = reader["ATHLETETYPE"].ToString();
            athlete.id = Int32.Parse(reader["ID"].ToString());
            athlete.userId = Int32.Parse(reader["USERID"].ToString());
            athlete.athletePic = Encoding.UTF8.GetString((byte[])reader["ATHLETEPIC"]);
            return athlete;
        }

    }
}
