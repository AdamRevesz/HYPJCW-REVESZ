using Logic;
using Logic.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Dtos.Content;
using Models.Dtos.UserDto;

namespace Backend_Feleves.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        DtoProvider dtoProvider;
        FilterLogics filterLogic;
        ContentLogic contentLogic;
        UserLogic userLogic;
        public FilterController(DtoProvider dtoProvider, FilterLogics logic, ContentLogic contentLogic, UserLogic userLogic)
        {
            this.dtoProvider = dtoProvider;
            this.filterLogic = logic;
            this.contentLogic = contentLogic;
            this.userLogic = userLogic;
        }

        [HttpGet("filterContentByCategory")]
        public IEnumerable<ContentShortViewDto> GetContentByCategory(string category)
        {
            if (category.IsNullOrEmpty())
            {
                return contentLogic.GetAllContent();
            }
            try
            {
                return filterLogic.GetContentByCategory(category);
            }
            catch (ArgumentException ex)
            {
                BadRequest(ex.Message);
            }
            return contentLogic.GetAllContent();
        }

        [HttpGet("highestapprovedContents")]
        public IEnumerable<ContentShortViewDto> GetHighestApprovedContents()
        {
            try
            {
                return filterLogic.GetHighestApprovedContents();
            }
            catch (ArgumentException ex)
            {
                BadRequest(ex.Message);
            }
            return contentLogic.GetAllContent();
        }

        [HttpGet("getContentHighestRatedContentOfUser")]
        public ContentShortViewDto GetContentHighestRatedContentOfUser([FromQuery] string username)
        {
            if (username.IsNullOrEmpty())
            {
                throw new ArgumentException("User ID cannot be null or empty.");
            }
            try
            {
                return filterLogic.GetContentHighestRatedContentOfUser(username);
            }
            catch (ArgumentException ex)
            {
                BadRequest(ex.Message);
            }
            throw new ArgumentException("User not found or has no content.");
        }

        [HttpGet("getHighestApprovedCreator")]
        public Dictionary<string, ContentShortViewDto> GetHighestApprovedCreator()
        {
            try
            {
                return filterLogic.GetHighestApprovedCreator();
            }
            catch (ArgumentException ex)
            {
                BadRequest(ex.Message);
            }
            return contentLogic.GetAllContent().ToDictionary(c => c.Id, c => c);
        }

        [HttpGet("UserContentCount")]
        public List<UserContentCountDto> GetUserContentCount()
        {
            var users = userLogic.GetAllUsers();
            if (users is null)
            {
                throw new ArgumentException("No users found.");
            }
            return filterLogic.GetUserContentcount();
        }

        [HttpGet("getContentCategoryCount")]
        public List<ContentCategoryCountDto> GetContentCategoryCount()
        {
            var content = contentLogic.GetAllContent();
            if (content is null)
            {
                throw new ArgumentException("No content found.");
            }
            return filterLogic.GetContentCountByCategory();
        }
    }
}
