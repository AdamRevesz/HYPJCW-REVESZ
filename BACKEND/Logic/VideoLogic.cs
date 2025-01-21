using Logic.Helper;
using Logic.Interfaces;
using Models;
using Repository;
using Models.Dtos.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class VideoLogic
    {
        private readonly Repository<Video> videoRepo;
        private readonly DtoProvider dtoProvider;

        public VideoLogic(Repository<Video> videoRepo, DtoProvider dtoProvider)
        {
            this.videoRepo = videoRepo;
            this.dtoProvider = dtoProvider;
        }

        public void AddVideo(VideoCreateUpdateDto dto)
        {
            Video video = dtoProvider.Mapper.Map<Video>(dto);

            if (videoRepo.GetAll().FirstOrDefault(x => x.Title == video.Title) == null)
            {
                videoRepo.Create(video);
            }
            else
            {
                throw new ArgumentException("Video with the same name already exists");
            }
        }

        public IEnumerable<VideoShortViewDto> GetAllVideos()
        {
            return videoRepo.GetAll().Select(x => dtoProvider.Mapper.Map<VideoShortViewDto>(x));
        }

        public void DeleteVideo(string id)
        {
            videoRepo.DeleteById(id);
        }

        public void DeleteOwnerVideo(string id, string userId)
        {
            var video = videoRepo.FindById(id);
            if (video.OwnerId != userId)
            {
                throw new UnauthorizedAccessException("You are not the owner of this video.");
            }
            videoRepo.DeleteById(id);
        }

        public void UpdateVideo(string id, VideoCreateUpdateDto dto, string userId)
        {
            var oldVideo = videoRepo.FindById(id);
            if (oldVideo.OwnerId != userId)
            {
                throw new UnauthorizedAccessException("You are not the owner of this video.");
            }
            dtoProvider.Mapper.Map(dto, oldVideo);
            videoRepo.Update(oldVideo);
        }

        public VideoViewDto GetVideo(string id)
        {
            var video = videoRepo.FindById(id);
            return dtoProvider.Mapper.Map<VideoViewDto>(video);
        }
    }
}


