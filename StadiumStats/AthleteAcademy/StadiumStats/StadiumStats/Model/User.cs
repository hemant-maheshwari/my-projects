using System;
using System.Collections.Generic;
using System.Text;

namespace StadiumStats.Model
{
    public class User
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string userType { get; set; }

        public User()
        {

        }

        public User(string firstName, string lastName, string email, string username, string password, string userType)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.username = username;
            this.password = password;
            this.userType = userType; 
        }

        public User(int id, string firstName, string lastName, string email, string username, string password, string userType)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.username = username;
            this.password = password;
            this.userType = userType;
        }

        public User(string username, string password) {
            this.username = username;
            this.password = password;
        }

    }

   
}
