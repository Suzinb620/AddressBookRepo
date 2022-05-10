using System.ComponentModel.DataAnnotations;
using Application.DataTransferObjects;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        //Variables:
        private readonly IAddressService _addressService;
        private readonly ILogger _logger;

        //Constructors:
        public AddressController(IAddressService addressService, ILogger<AddressController> logger)
        {
            _addressService = addressService;
            _logger = logger;
        }

        //Methods:
        [HttpGet]
        public IActionResult Get()
        {
            var address = _addressService.GetLastAdded();
            if (address == null)
            {
                _logger.LogError("Attempting to get the last added address that not exist");
                return NotFound();
            }
            _logger.LogInformation("Got the last added address");
            return Ok(address);
        }

        [HttpGet("{city}")]
        public IActionResult Get(string city)
        {
            var addresses = _addressService.GetByCity(city);
            if (addresses == null)
            {
                _logger.LogError("Attempting to get addresses by entering not existing city name");
                return NotFound();
            }
            _logger.LogInformation("Got the addresses by entering city name");
            return Ok(addresses);
        }

        [HttpPost]
        public IActionResult Post(CreateAddressDto address)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Attempting to add address with invalid data");
                return BadRequest(ModelState);
            }

            var created = _addressService.Add(address);
            if (created == null)
            {
                _logger.LogError("Attempting to add currently existing address");
                return Conflict();
            }

            _logger.LogInformation("Added new address");
            return Created($"api/posts/{created.Id}", created);
        }
    }
}
