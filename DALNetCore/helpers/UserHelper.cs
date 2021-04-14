using System;
using DALNetCore.interfaces;
using Shared.dto;
using models = DALNetCore.Models;

namespace DALNetCore.helpers
{
    public class UserHelper : IUserHelper
    {
        public User ConvertDbUserToUser(models.User dbUser)
        {
            var user = new User()
            {
                UserId = Convert.ToInt32(dbUser.UserId),  //TODO - fix conversion (temp hack)
                UserName = dbUser.UserName,
                Salt = dbUser.Salt,
                Password = dbUser.Password,
                Email = dbUser.Email,
                Token = dbUser.Token
            };

            return user;
        }
    }
}
