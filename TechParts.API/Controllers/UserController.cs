using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TechParts.API.Data;
using TechParts.API.Models;
using TechParts.API.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace TechParts.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private IUserRepositroy _userRepo { get; }
        
        private IMapper _mapper { get; }

        private IPartRepository _partRepo { get; }
        public IWebHostEnvironment _hostingEnv { get; }

        public UserController(IUserRepositroy userRepo, IMapper mapper, IPartRepository partRepo, IWebHostEnvironment hostingEnv)
        {
            this._userRepo = userRepo;
            this._mapper = mapper;
            this._partRepo = partRepo;
            this._hostingEnv = hostingEnv;
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            System.Console.WriteLine("Getting user " + id);
            
            var userFromRepo = await _userRepo.GetUser(id);
            
            if(userFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserToReturnDto>(userFromRepo));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var usersFromRepo = await _userRepo.GetAllUsers();

            return Ok( _mapper.Map<IEnumerable<UserToReturnDto>>(usersFromRepo));
        }

        [Authorize]
        [HttpPost("{id}/wishlist/{partId}")]
        public async Task<IActionResult> AddToWishList(int id, int partId)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            
            if(await _userRepo.WishlistItemExists(id, partId))
            {
                return BadRequest("This item is already in your wishlist");
            }

            if(await _userRepo.CreateUserPart(id, partId, true))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost("{id}/cart/{partId}")]
        public async Task<IActionResult> AddToCart(int id, int partId)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            
            var userPart = await _userRepo.GetCartItem(id, partId);

            if(userPart != null)
            {
                var part = await _partRepo.GetPart(partId);

                if(part != null && userPart.Count < part.CountAvailable)
                {
                    userPart.Count++;
                    
                    if(await _partRepo.SaveDatabase())
                    {
                        return Ok();
                    }
                }
                else
                {
                    return BadRequest("Not enough in stock");
                }
            }

            if(await _userRepo.CreateUserPart(id, partId, false))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet("{id}/wishlist")]
        public async Task<IActionResult> GetWishList(int id)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            return Ok(await _userRepo.GetWishlist(id));
        }

        [Authorize]
        [HttpGet("{id}/cart")]
        public async Task<IActionResult> GetCartt(int id)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            return Ok(await _userRepo.GetCart(id));
        }

        [Authorize]
        [HttpGet("{id}/like/{recieverId}")]
        public async Task<IActionResult> AddLike([FromRoute] int id, [FromRoute] int recieverId)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            if(await _userRepo.AddLikes(id, recieverId))
            {
                return Ok("Sucessfully Liked");
            }

            return BadRequest();
        }

        [Authorize]
        [HttpGet("{id}/getlikees")]
        public async Task<IActionResult> GetLikees([FromRoute] int id)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var likess = await _userRepo.GetLikees(id);

            return Ok(likess);
        }

        [Authorize]
        [HttpGet("{id}/getlikers")]
        public async Task<IActionResult> GetLikers([FromRoute] int id)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var likers = await _userRepo.GetLikers(id);

            return Ok(likers);
        }

        [Authorize]
        [HttpPost("{id}/UploadFile")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, [FromRoute] int id)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            System.Console.WriteLine("Recieved file: " + file.FileName + file.ContentType);

            if(await _userRepo.AddPhotoRecord(id, file))
            {
                return Ok("File upload successful");
            }

            return BadRequest("File upload failed");
        }

        [Authorize]
        [HttpGet("{id}/SetMainPhoto/{photoId}")]
        public async Task<IActionResult> SetMainPhoto([FromRoute] int id, [FromRoute]int photoId)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            if(await _userRepo.SetMainPhoto(id, photoId))
            {
                return Ok("Main photo sucessfully changed!");
            }

            return BadRequest("Something went wrong...");
        }
    }
}