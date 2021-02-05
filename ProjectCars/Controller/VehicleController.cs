using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectCars.BL.Interface;
using ProjectCars.Models.Vehicle;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectCars.Controllers
{

    [ApiController]
    [Route("Vehicle")]

    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehicleController(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }



        [HttpPost("Create")]

        public async Task<IActionResult> Create(VehicleRequest request)
        {
            if (request == null)
            {
                return BadRequest(request);
            }

            var position = _mapper.Map<Vehicle>(request);

            var result = await _vehicleService.Create(position);

            var response = _mapper.Map<VehicleResponse>(result);

            return Ok(response);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] VehicleRequest request)
        {
            var result = await _vehicleService.Update(_mapper.Map<Vehicle>(request));

            if (result == null) return NotFound();

            var Vehicle = _mapper.Map<VehicleResponse>(result);

            return Ok(Vehicle);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _vehicleService.Delete(id);
            return Ok();
        }

        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAll()
        {
            var result = await _vehicleService.GetAll();

            if (result == null)
            {
                return NotFound("Collection is empty");
            }

            var Vehicleresult = _mapper.Map<IEnumerable<VehicleResponse>>(result);

            return Ok(Vehicleresult);
        }
    }
}
