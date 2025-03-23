using Data.Repo;
using Logic.Interfaces;
using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class UserLogic
    {
        IRepository<User> userRepo;
        public UserLogic(IRepository<User> userRepo)
        {
            this.userRepo = userRepo;
        }

       
    }
}
