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
using System.Text.Json;
using System.Threading.Tasks;

namespace Logic
{
    public class UserLogic
    {
        DtoProvider dtoProvider;
        IRepository<User> userRepo;
        PictureLogic pictureLogic;
        public UserLogic(IRepository<User> userRepo, DtoProvider dtoProvider,PictureLogic pictureLogic)
        {
            this.userRepo = userRepo;
            this.dtoProvider = dtoProvider;
            this.pictureLogic = pictureLogic;
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
            if (string.IsNullOrWhiteSpace(dto.Username))
            {
                dto.Username = oldUser.UserName;
            }
            if (string.IsNullOrWhiteSpace(dto.EmailAddress))
            {
                dto.EmailAddress = oldUser.EmailAddress;
            }
            if (string.IsNullOrWhiteSpace(dto.Password))
            {
                dto.Password = oldUser.Password;
            }
            dtoProvider.Mapper.Map(dto, oldUser);
            userRepo.Update(oldUser);
        }

        public void AddUserFromJson(string json)
        {
            var users = JsonSerializer.Deserialize<List<UserCreateDto>>(json);
            foreach (var user in users)
            {
                userRepo.Create(dtoProvider.Mapper.Map<User>(user));
            }
        }

        public void DeleteUsersWithoutEmail()
        {
            var users = userRepo.ReadAll().Where(u => string.IsNullOrWhiteSpace(u.EmailAddress)).ToList();
            foreach (var user in users)
            {
                userRepo.Remove(user.Id);
            }
        }



    }
}
