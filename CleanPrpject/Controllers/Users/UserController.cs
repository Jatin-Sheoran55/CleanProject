using Application.Dto.Address;
using Application.Dto.Adress;
using Application.Dto.User;
using Application.Dto.UserAdress;
using Application.Interfaces.Services.Addresss;
using Application.Interfaces.Services.UserRegisters;
using Application.Interfaces.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRegistrationService _registrationService;
        private readonly IAddressService _addressService;

        public UserController(
            IUserService userService,
            IUserRegistrationService registrationService,
            IAddressService addressService)
        {
            _userService = userService;
            _registrationService = registrationService;
            _addressService = addressService;
        }

      
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserWithAddressDto dto)
        {
            var user = await _registrationService.RegisterAsync(dto);

            var response = new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Addresses = user.Addresses?.Select(a => new AddressResponseDto
                {
                    Id = a.Id,
                    Street = a.Street,
                    City = a.City,
                    State = a.State,
                    Country = a.Country,
                    Pincode = a.Pincode
                }).ToList() ?? new()
            };

            return Ok(response);
        }
        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            var response = new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
               Addresses = user.Addresses.Select(a => new AddressResponseDto
                {
                    Id = a.Id,
                    Street = a.Street,
                    City = a.City,
                    State = a.State,
                    Country = a.Country,
                    Pincode = a.Pincode
                }).ToList()
            };

            return Ok(response);
        }

     
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDto dto)
        {
            var result = await _userService.UpdateAsync(id, dto);

            if (result == null)
                return NotFound();

            var response = new UserResponseDto
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                Addresses = result.Addresses?.Select(a => new AddressResponseDto
                {
                    Id = a.Id,
                    Street = a.Street,
                    City = a.City,
                    State = a.State,
                    Country = a.Country,
                    Pincode = a.Pincode
                }).ToList() ?? new()
            };

            return Ok(response);
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!await _userService.DeleteAsync(id))
                return NotFound();

            return Ok("User deleted");
        }


        [HttpPut("address/{id}")]
        public async Task<IActionResult> UpdateAddress(int id, UpdateAddressDto dto)
        {
            var result = await _addressService.UpdateAsync(id, dto); 

            if (result == null)
                return NotFound();

            return Ok(new AddressResponseDto
            {
                Id = result.Id,
                Street = result.Street,
                City = result.City,
                State = result.State,
                Country = result.Country,
                Pincode = result.Pincode
            });

        }

        
        [HttpDelete("address/{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            if (!await _addressService.DeleteAsync(id))
                return NotFound();

            return Ok("Address deleted");
        }
    }
}
