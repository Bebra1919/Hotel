using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    internal class User
    {
        private int userID;
        private string surname;
        private string name;
        private string login;
        private string password;
        private string role;
        private string isBlocked;
        private DateTime LastEnterDate;

        public User(string surname,
            string name,
            string login,
            string password,
            string role)
        {
            this.surname = surname;
            this.name = name;
            this.login = login;
            this.password = password;
            this.role = role;
        }

        public string Surname
        {
            get { return this.surname; }
            set { this.surname = value; }
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string Login
        {
            get { return this.login; }
            set { this.login = value; }
        }
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        public string Role
        {
            get { return this.role; }
        }

        public string IsBlocked
        {
            get { return isBlocked; }
            set { isBlocked = value; }
        }

        public int GetId()
        {
            return userID;
        }

        public string GetLogin()
        {
            return login;
        }
        public string GetPassword()
        {
            return password;
        }
    }
}