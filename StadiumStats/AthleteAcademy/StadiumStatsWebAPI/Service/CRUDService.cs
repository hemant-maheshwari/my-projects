using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PocketClosetWebServiceAPI.Services
{
    public class CRUDService
    {

        private MySQLService mySQLService;

        public CRUDService(string connString) {
            mySQLService = new MySQLService(connString);
        }

        public int create<T>(T model) {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("INSERT INTO ");
            stringBuilder.Append(getTableNameFromClass<T>());
            stringBuilder.Append("(");
            stringBuilder.Append(getCommaSeperatedString(getFieldNames<T>()));
            stringBuilder.Append(") VALUES(");
            stringBuilder.Append(getCommaSeperatedString(getFieldValues(model)));
            stringBuilder.Append(");SELECT MAX(ID) FROM ");
            stringBuilder.Append(getTableNameFromClass<T>());
            string createString = stringBuilder.ToString();
            Debug.WriteLine(createString);
            int identity = mySQLService.create(createString);
            Debug.WriteLine("Record created with identity "+identity);
            return identity;
        }

        public List<T> getAll<T>() {
            string getAllQuery = "select * from " + getTableNameFromClass<T>();
            Debug.WriteLine(getAllQuery);
            return mySQLService.getResults<T>(getAllQuery);
        }

        public T get<T>(int searchId)
        {
            string getQuery = "select * from " + getTableNameFromClass<T>() + " where id = "+searchId;
            Debug.WriteLine(getQuery);
            return mySQLService.getResult<T>(getQuery);
        }

        public void update<T>(T model) {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("update ");
            stringBuilder.Append(getTableNameFromClass<T>());
            stringBuilder.Append(" set ");
            stringBuilder.Append(getColumnValuePair(model));
            stringBuilder.Append(" where id = ");
            stringBuilder.Append(getModelId(model));
            stringBuilder.Append(";");
            string updateQuery = stringBuilder.ToString();
            Debug.WriteLine(updateQuery);
            mySQLService.executeQuery(updateQuery);
        }

        public void delete<T>(int searchId) {
            string deleteQuery = "delete from " + getTableNameFromClass<T>() + " where id = " + searchId;
            Debug.WriteLine(deleteQuery);
            mySQLService.executeQuery(deleteQuery);
        }

        public void deleteAll<T>() {
            string deleteQuery = "delete from " + getTableNameFromClass<T>();
            Debug.WriteLine(deleteQuery);
            mySQLService.executeQuery(deleteQuery);
        }


        private string getColumnValuePair<T>(T model) {
            StringBuilder stringBuilder = new StringBuilder();
            List<string> columnNames = getFieldNames<T>();
            List<string> columnValues = getFieldValues<T>(model);
            for (int i= 0; i<columnNames.Count; i++) {
                stringBuilder.Append(columnNames[i]);
                stringBuilder.Append("=");
                stringBuilder.Append(columnValues[i]);
                stringBuilder.Append(",");
            }
            string columnValuePairString = stringBuilder.ToString();
            return columnValuePairString.Substring(0, columnValuePairString.Length - 1);           
        }

        private int getModelId<T>(T model) {
            FieldInfo[] fields = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            int fieldValue = Convert.ToInt32(fields[0].GetValue(model).ToString());
            return fieldValue;
        }

        private string getTableNameFromClass<T>()
        {
            return typeof(T).Name;
        }

        private string getCommaSeperatedString(List<string> stringList) {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string element in stringList) {
                stringBuilder.Append(element);
                stringBuilder.Append(",");
            }
            string convertedString = stringBuilder.ToString();
            return convertedString.Substring(0, convertedString.Length - 1);
        }

        private List<string> getFieldNames<T>()
        {
            List<string> fieldNames = new List<string>();
            FieldInfo[] fields = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            for (int i = 1; i < fields.Length; i++)
            {
                string fieldName = fields[i].Name.ToString();
                fieldName = fieldName.Substring(1, fieldName.IndexOf('>') - 1);
                fieldNames.Add(fieldName.ToUpper());
            }
            return fieldNames;
        }

        private List<string> getFieldValues<T>(T model) {
            List<string> fieldValues = new List<string>();
            FieldInfo[] fields = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            for (int i = 1; i < fields.Length; i++)
            {
                object fieldValue = fields[i].GetValue(model);
                if (fieldValue != null) {
                    fieldValues.Add("'"+fieldValue.ToString()+"'");
                }
                else {
                    fieldValues.Add("''");
                }
            }
            return fieldValues;
        }

    }
}
