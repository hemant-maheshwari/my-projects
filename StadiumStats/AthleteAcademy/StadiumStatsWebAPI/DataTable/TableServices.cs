using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace PocketClosetWebServiceAPI.Services
{
    public class TableServices
    {
        private MySQLService mySQLService;

        public TableServices(string connString) {
            mySQLService = new MySQLService(connString);
        }

        public void printFields<T>() {
            List<string>  fieldNames = getFieldNames<T>();
            List<string> fieldTypes = getFieldTypes<T>();
            foreach (string fieldName in fieldNames) {
                Debug.WriteLine(fieldName);
            }
            foreach (string fieldType in fieldTypes)
            {
                Debug.WriteLine(fieldType);
            }
        }

        public void create<T>() {
            string tableName = getTableNameFromClass<T>();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("CREATE TABLE ");
            stringBuilder.Append(tableName);
            stringBuilder.Append("(ID INT AUTO_INCREMENT, ");
            stringBuilder.Append(getCommaSeperatedColumnString<T>());
            stringBuilder.Append("PRIMARY KEY (ID));");
            string createTableQuery = stringBuilder.ToString();
            Debug.WriteLine(createTableQuery);
            mySQLService.executeQuery(createTableQuery);
        }

        private string getCommaSeperatedColumnString<T>() {
            StringBuilder stringBuilder = new StringBuilder();
            List<string> fieldNames = getFieldNames<T>();
            List<string> fieldTypes = getFieldTypes<T>();
            for (int i=1; i<fieldNames.Count; i++) {
                stringBuilder.Append(fieldNames[i].ToUpper());
                stringBuilder.Append(" ");
                stringBuilder.Append(getColumnType(fieldTypes[i]));
                stringBuilder.Append(", ");
            }
            return stringBuilder.ToString();
        }

        private string getColumnType(string fieldType) {
            if (fieldType.Equals("String"))
            {
                return "VARCHAR(100)";
            }
            else if (fieldType.Equals("Int32"))
            {
                return "INT";
            }
            else if (fieldType.Equals("Double") || fieldType.Equals("Float"))
            {
                return "DOUBLE";
            }
            else
            {
                return "VARCHAR(100)";
            }
        }

        private string getTableNameFromClass<T>() {
            return typeof(T).Name;
        }

        private List<string> getFieldNames<T>() {
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

        private List<string> getFieldTypes<T>()
        {
            List<string> fieldTypes = new List<string>();
            FieldInfo[] fields = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            for (int i = 0; i < fields.Length; i++)
            {
                string fieldType = fields[i].FieldType.Name.ToString();
                fieldTypes.Add(fieldType);
            }
            return fieldTypes;
        }


    }
}
