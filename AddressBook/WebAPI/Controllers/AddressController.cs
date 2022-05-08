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
        //private readonly ILogger _logger;

        //Constructors:
        public AddressController(IAddressService addressService/*, ILogger logger*/)
        {
            _addressService = addressService;
            //_logger = logger;
        }

        //Methods:
        [HttpGet]
        public IActionResult Get()
        {
            var address = _addressService.GetLastAdded();
            return address == null ? NotFound() : Ok(address);
        }

        [HttpGet("{city}")]
        public IActionResult Get(string city)
        {
            var addresses = _addressService.GetByCity(city);
            return addresses == null ? NotFound() : Ok(addresses);
        }

        [HttpPost]
        public IActionResult Post(CreateAddressDto address)
        {
            var created = _addressService.Add(address);
            return created == null ? Conflict() : Created($"api/posts/{created.Id}", created);
        }
    }
}
