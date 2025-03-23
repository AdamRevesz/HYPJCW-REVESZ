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
        Repository<User> userRepo;
        public UserLogic(Repository<User> userRepo)
        {
            this.userRepo = userRepo;
        }

       
    }
}
