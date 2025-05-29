using Data.ClassRepo;
using Data.Repo;
using Logic.Helper;
using Logic.Interfaces;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.Dtos.Picture;
using Models.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class UserLogic
    {
        DtoProvider dtoProvider;
        IRepository<User> userRepo;
        public UserLogic(IRepository<User> userRepo, DtoProvider dtoProvider)
        {
            this.userRepo = userRepo;
            this.dtoProvider = dtoProvider;
        }
         public void UpdatePicture(string id, UserUpdatePictureDto dto)
        {
            var oldUser = userRepo.Read(id);
            dtoProvider.Mapper.Map(dto, oldUser);
            userRepo.Update(oldUser);
        }

        public void UpdateUser(string id, UserUpdateDto dto)
        {
            var oldUser = userRepo.Read(id);
            dtoProvider.Mapper.Map(dto, oldUser);
            userRepo.Update(oldUser);
        }




    }
}
