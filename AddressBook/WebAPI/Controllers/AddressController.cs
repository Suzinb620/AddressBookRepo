using System.Security.Cryptography;
using Application.DataTransferObjects;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        //Methods:
        [HttpGet]
        public IActionResult Get()
        {
            var address = _addressService.GetLastAdded();
            if (address == null) return NotFound();
            return Ok(address);
        }

        [HttpGet("{city}")]
        public IActionResult Get(string city)
        {
            var addresses = _addressService.GetByCity(city);
            if(addresses == null) return NotFound();
            return Ok(addresses);
        }

        [HttpPost]
        public IActionResult Post(CreateAddressDto address)
        {
            var created = _addressService.Add(address);
            if (created == null) return Conflict();
            return Created($"api/posts/{created.Id}", created);
        }
    }
}
