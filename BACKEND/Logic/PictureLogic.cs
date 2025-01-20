using Logic.Helper;
using Logic.Interfaces;
using Models;
using Repository;
using Models.Dtos.Picture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class PictureLogic
    {
        private readonly Repository<Picture> pictureRepo;
        private readonly DtoProvider dtoProvider;

        public PictureLogic(Repository<Picture> pictureRepo, DtoProvider dtoProvider)
        {
            this.pictureRepo = pictureRepo;
            this.dtoProvider = dtoProvider;
        }

        public void AddPicture(PictureCreateUpdateDto dto)
        {
            Picture picture = dtoProvider.Mapper.Map<Picture>(dto);

            if (pictureRepo.GetAll().FirstOrDefault(x => x.Title == picture.Title) == null)
            {
                pictureRepo.Create(picture);
            }
            else
            {
                throw new ArgumentException("Picture with the same name already exists");
            }
        }

        public IEnumerable<PictureShortViewDto> GetAllPictures()
        {
            return pictureRepo.GetAll().Select(x => dtoProvider.Mapper.Map<PictureShortViewDto>(x));
        }

        public void DeletePicture(string id)
        {
            pictureRepo.DeleteById(id);
        }

        public void UpdatePicture(string id, PictureCreateUpdateDto dto)
        {
            var oldPicture = pictureRepo.FindById(id);
            dtoProvider.Mapper.Map(dto, oldPicture);
            pictureRepo.Update(oldPicture);
        }

        public PictureViewDto GetPicture(string id)
        {
            var picture = pictureRepo.FindById(id);
            return dtoProvider.Mapper.Map<PictureViewDto>(picture);
        }
    }
}
