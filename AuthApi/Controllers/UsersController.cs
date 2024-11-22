namespace AuthApi.Controllers
{
    using System.Text.Json;
    using AuthApi.Models;
    using AuthApi.Services.Interfaces;
    using AuthApi.Services.Models.Register;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using RollingDiceApi.Models.LogIn;
    using RollingDiceApi.Models.UsersRegister;

    [ApiController]
    [Route("[controller]")]
    public class UsersController(ILogger<UsersController> logger, IMapper mapper, IUserService userService) : ControllerBase
    {
        [HttpPost("Register")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] RegisterUserRequest registerRequest, CancellationToken ct)
        {
            ResponseModel<string> response = new ResponseModel<string>();

            try
            {
                var serviceRequest = mapper.Map<RegisterUserServiceRequest>(registerRequest);
                var result = await userService.RegisterUser(serviceRequest, ct);

                response.Status = true;
                response.Message = "success";
                response.Data = "User created successfully!";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> PostAsync([FromBody] LogInRequest loginRequest, CancellationToken ct)
        {
            ResponseModel<string> response = new ResponseModel<string>();

            try
            {
                var serviceRequest = mapper.Map<LogInServiceRequest>(loginRequest);
                var result = await userService.LogInUser(serviceRequest, ct);

                response.Status = true;
                response.Message = "success";
                response.Data = JsonSerializer.Serialize(result);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
    }
}
