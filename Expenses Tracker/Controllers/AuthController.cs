using Amazon.S3;
using ExpensesTracker.Application.DTOs;
using ExpensesTracker.Application.Helpers;
using ExpensesTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAuthService _authService;
        readonly IAwsService _awsService;   
        readonly IConfiguration _configuration;
        public AuthController(IAuthService authService, IAwsService awsService , IConfiguration configuration)
        {
            _authService = authService;
            _awsService = awsService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto RegisterUser)
        {
        

            var token = await _authService.Register(RegisterUser, "Customer");
            
            if (token != null)
            {
                return Ok(new { Messsage = "success", token = token });
            }
            else if (token == "exsited")
            {
                return BadRequest(new { Messsage = "user existed" });
            }
            else
            {
                return BadRequest(new { Messsage = "Invalid Data" });
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto LoginUser)
        {
            var token = await _authService.Login(LoginUser);

            if (token == null)
            {
                return ApiResponse<string>.FailureResponse($"Invalid Email or Password !");

            }
            return ApiResponse<string>.SuccessResponse(token, "You have been logged in Successfully !");
           

        }
    }
}
