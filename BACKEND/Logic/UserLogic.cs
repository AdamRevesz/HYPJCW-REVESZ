using Logic.Interfaces;
using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class UserLogic : IUserLogic
    {
        IRepository<User> userRepo;
        public UserLogic(IRepository<User> userRepo)
        {
            this.userRepo = userRepo;
        }
    }
}
