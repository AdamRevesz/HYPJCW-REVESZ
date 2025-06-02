using Logic.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Dtos.User;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Models.Dtos.UserDto;
using Logic;
using Models.Dtos.Content;

namespace Backend_Feleves.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserManager<User> userManager;
        RoleManager<IdentityRole> roleManager;
        DtoProvider dtoProvider;
        UserLogic logic;
        ContentLogic contentLogic;

        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, DtoProvider dtoProvider, SignInManager<User> signInManager, UserLogic logic, ContentLogic contentLogic)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.dtoProvider = dtoProvider;
            _signInManager = signInManager;
            this.logic = logic;
            this.contentLogic = contentLogic;

            Task.Run(async () =>
            {
                if (!await roleManager.RoleExistsAsync("User"))
                {
                    await roleManager.CreateAsync(new IdentityRole("User"));
                }
            }).Wait();
        }

        [HttpGet("/grantadmin/{userid}")]
        //[Authorize(Roles = "Admin")]
        public async Task GrantAdmin(string userid)
        {
            var user = await userManager.FindByIdAsync(userid);
            if (user == null)
                throw new ArgumentException("User not found");
            await userManager.AddToRoleAsync(user, "Admin");
        }

        [HttpGet("/revokeadmin/{userid}")]
        [Authorize(Roles = "Admin")]
        public async Task RevokeAdmin(string userid)
        {
            var user = await userManager.FindByIdAsync(userid);
            if (user == null)
                throw new ArgumentException("User not found");
            await userManager.RemoveFromRoleAsync(user, "Admin");
        }
        [HttpPut("uploadpicture/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadPicture(string id, [FromForm] IFormFile? uploadedFile)
        {
            if (uploadedFile == null || uploadedFile.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            string folderPath = Path.Combine("..", "..", "FrontEnd_Feleves", "src", "assets", "UploadedPictures");
            Directory.CreateDirectory(folderPath);

            var fileName = Path.GetFileName(uploadedFile.FileName);
            var fullPath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await uploadedFile.CopyToAsync(stream);

            var dto = new UserUpdatePictureDto
            {
                FilePath = $"assets/UploadedPictures/{fileName}"
            };

            // Update the user's picture
            logic.UpdatePicture(id, dto);

            return Ok(new { filePath = dto.FilePath });
        }

        [HttpPut("updateuser/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromForm] UserUpdateDto dto)
        {
            logic.UpdateUser(id, dto);
            return Ok();
        }

        [HttpGet("getuser{id}")]
        public async Task<IEnumerable<UserViewDto>> GetUser(string id)
        {
            var user = userManager.Users.FirstOrDefault(u => u.Id == id);
            var contents = contentLogic.GetContentByOwner(id);

            var userDto = dtoProvider.Mapper.Map<UserViewDto>(user);
            userDto.CreatedContents = contents.ToList();

            return new List<UserViewDto> { userDto };
        }

        [HttpGet]
        public async Task<IEnumerable<UserViewDto>> GetUsers()
        {
            var users = userManager.Users.ToList();
            foreach (var user in users)
            {
                await user.UpdateIsAdminStatusAsync(userManager);
            }
            return users.Select(t => dtoProvider.Mapper.Map<UserViewDto>(t));
        }

        [HttpGet("/isadmin/{userid}")]
        public async Task<IActionResult> IsAdmin(string userid)
        {
            var user = await userManager.FindByIdAsync(userid);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var isAdmin = await userManager.IsInRoleAsync(user, "Admin");
            return Ok(new { IsAdmin = isAdmin });
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(UserInputDto dto)
        {
            var user = new User(dto.Username);
            user.EmailAddress = dto.EmailAddress;
            user.Password = dto.Password;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.IsProfessional = dto.IsProfessional;
            var result = await userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            if (userManager.Users.Count() == 1)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await userManager.AddToRoleAsync(user, "Admin");
            }

            await user.UpdateIsAdminStatusAsync(userManager);
            return Ok(new { UserId = user.Id, IsAdmin = user.IsAdmin });
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var user = await userManager.FindByNameAsync(dto.Username);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            else
            {
                var result = await userManager.CheckPasswordAsync(user, dto.Password);
                if (!result)
                {
                    throw new ArgumentException("Incorrect password");
                }
                else
                {
                    var claim = new List<Claim>();
                    claim.Add(new Claim(ClaimTypes.Name, user.UserName!));
                    claim.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

                    foreach (var role in await userManager.GetRolesAsync(user))
                    {
                        claim.Add(new Claim(ClaimTypes.Role, role));
                    }

                    int expiryInMinutes = 24 * 60;
                    var token = GenerateAccessToken(claim, expiryInMinutes);

                    return Ok(new LoginResultDto()
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = DateTime.Now.AddMinutes(expiryInMinutes)
                    });

                }
            }



        }

        private JwtSecurityToken GenerateAccessToken(IEnumerable<Claim>? claims, int expiryInMinutes)
        {
            var signinKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes("MegszencségeleníthetetlenségeskedéseitekértMegszencségeleníthetetlenségeskedéseitekértMegszencségeleníthetetlenségeskedéseitekért"));

            return new JwtSecurityToken(
                  issuer: "artlounge.com",
                  audience: "artlounge.com",
                  claims: claims?.ToArray(),
                  expires: DateTime.Now.AddMinutes(expiryInMinutes),
                  signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                );
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(string json)
        {
            logic.AddUserFromJson(json);
            return Ok();
        }

        [HttpDelete("/deleteusers")]
        public void DeleteUsersWithoutEmail()
        {
            logic.DeleteUsersWithoutEmail();
        }
    }
}