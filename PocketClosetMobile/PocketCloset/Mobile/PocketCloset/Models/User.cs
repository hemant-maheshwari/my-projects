using System;
namespace PocketCloset.Models
{
    public class User
    {
        public int userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string userType { get; set; } //user type: store or user
        public string gender { get; set; }
        public string dob { get; set; } //date of birth

        public User() { }

        public User(string userType, string gender, string firstName, string lastName, string dob, string email, string username, string password)
        {
            this.userType = userType;
            this.gender = gender;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dob = dob;
            this.email = email;
            this.username = username;
            this.password = password;
        }

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public User(string email)
        {
            this.email = email;
        }

        public void updateUser(string firstName, string lastName, string email,  string password)
        {
            this.email = email;
            this.firstName = firstName;
            this.lastName = lastName;
            this.password = password;
        }

    }
}
