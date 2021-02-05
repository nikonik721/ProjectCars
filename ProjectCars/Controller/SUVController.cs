using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectCars.BL.Interface;
using ProjectCars.Models.Vehicle;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectCars.Controllers
{

    [ApiController]
    [Route("SUV")]

    public class SUVController : ControllerBase
    {
        private readonly ISUVService _suvService;
        private readonly IMapper _mapper;

        public SUVController(ISUVService suvService, IMapper mapper)
        {
            _suvService = suvService;
            _mapper = mapper;
        }



        [HttpPost("Create")]

        public async Task<IActionResult> Create(SUVRequest request)
        {
            if (request == null)
            {
                return BadRequest(request);
            }

            var position = _mapper.Map<SUV>(request);

            var result = await _suvService.Create(position);

            var response = _mapper.Map<SUVResponse>(result);

            return Ok(response);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] SUVRequest request)
        {
            var result = await _suvService.Update(_mapper.Map<SUV>(request));

            if (result == null) return NotFound();

            var suv = _mapper.Map<SUVResponse>(result);

            return Ok(suv);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _suvService.Delete(id);
            return Ok();
        }

        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAll()
        {
            var result = await _suvService.GetAll();

            if (result == null)
            {
                return NotFound("Collection is empty");
            }

            var suvresult = _mapper.Map<IEnumerable<SUVResponse>>(result);

            return Ok(suvresult);
        }
    }
}
