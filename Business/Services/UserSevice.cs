using Business.Abstractions;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Services
{
    public class UserSevice : IUserService
    {
        private static User user;

        private static object lockObj = new object();

        AppMonitoringContext _dbContext;

        private UserSevice(AppMonitoringContext dbContext)
        {
            _dbContext = dbContext;
        }

        public static User CreateUserSingleton()
        {
            lock (lockObj)
            {
                if (user == null)
                {
                    user = new User();
                }

                return user;
            }
            
        }

        public static User CreateUserSingleton(int userId, string userName)
        {
            lock (lockObj)
            {
                if (user == null)
                {
                    user = new User() { UserId = userId, UserName = userName };
                }

                return user;
            }
        }

        
    }
}
