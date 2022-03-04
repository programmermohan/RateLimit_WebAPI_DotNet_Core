using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RateLimit_WebAPI
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }

        [JsonConstructor]
        public User(int userid, string userName, string password, string emailId)
        {
            this.UserId = userid;
            this.UserName = userName;
            this.Password = password;
            this.EmailId = emailId;
        }
    }
}
