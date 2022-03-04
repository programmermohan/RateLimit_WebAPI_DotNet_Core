using RateLimit_WebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateLimit_WebAPI
{
    public partial class User
    {
        private readonly DatabaseContext _databaseContext;

        public User(DatabaseContext databaseContext)
        {
            this._databaseContext = databaseContext;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------//
        public List<User> GetUsers()
        {
            try
            {
                return _databaseContext.Users.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------//
        public User GetUserById(int id)
        {
            try
            {
                User user = _databaseContext.Users.Find(id);
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------//
        public int AddUser(User user)
        {
            try
            {
                _databaseContext.Users.Add(user);
                _databaseContext.SaveChanges();
                return user.UserId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------//
        public int UpdateUser(User user, int Id)
        {
            try
            {
                User Finduser = _databaseContext.Users.FirstOrDefault(a => a.UserId == Id);
                if (Finduser == null)
                    return 0;

                Finduser.UserName = user.UserName;
                Finduser.Password = user.Password;
                Finduser.EmailId = user.EmailId;

                // _databaseContext.Update(user);
                _databaseContext.SaveChanges();
                return this.UserId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------//
        public bool DeleteUser(int id)
        {
            try
            {
                bool isDeleted = false;
                User user = _databaseContext.Users.Find(id);
                if (user != null)
                {
                    _databaseContext.Remove(user);
                    _databaseContext.SaveChanges();
                    isDeleted = true;
                }

                return isDeleted;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------//
        public User GetUser(User user)
        {
            try
            {
                User findUser = _databaseContext.Users.FirstOrDefault(a => a.UserName == user.UserName);
                if (findUser != null)
                    return findUser;
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------//
    }
}
