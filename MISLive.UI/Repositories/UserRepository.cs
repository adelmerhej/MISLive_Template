using MISLive.UI.Models.Users;
using System.Collections.Generic;

namespace MISLive.UI.Repositories
{
    public class UserRepository
    {
        public UserModel SelectUser(int id)
        {
            UserModel model = new UserModel
            {
                Id = id,
                UserName = "Serge",
                Password = "serge",
                Email = "serge@xolog.com",
                Active = true,
            };

            return model;
        }

        public List<UserModel> GetUsers()
        {
            List<UserModel> users = new List<UserModel>();


            users.Add(new UserModel
            {
                Id = 1,
                UserName = "Serge",
                Password = "serge",
                Email = "serge@xolog.com",
                Active = true,
            });


            users.Add(new UserModel
            {
                Id = 2,
                UserName = "Adel",
                Password = "adel",
                Email = "adel@xolog.com",
                Active = true,
            });


            users.Add(new UserModel
            {
                Id = 3,
                UserName = "Rachelle",
                Password = "rachelle",
                Email = "rachelle@xolog.com",
                Active = true,
            });


            users.Add(new UserModel
            {
                Id = 4,
                UserName = "Luciana",
                Password = "luciana",
                Email = "luciana@xolog.com",
                Active = false,
            });

            return users;
        }


    }
}
