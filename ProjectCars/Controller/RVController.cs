using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectCars.BL.Interface;
using ProjectCars.Models.Vehicle;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectCars.Controllers
{

    [ApiController]
    [Route("RV")]

    public class RVController : ControllerBase
    {
        private readonly IRVService _rvService;
        private readonly IMapper _mapper;

        public RVController(IRVService rvService, IMapper mapper)
        {
            _rvService = rvService;
            _mapper = mapper;
        }



        [HttpPost("Create")]

        public async Task<IActionResult> Create(RVRequest request)
        {
            if (request == null)
            {
                return BadRequest(request);
            }

            var position = _mapper.Map<RV>(request);

            var result = await _rvService.Create(position);

            var response = _mapper.Map<RVResponse>(result);

            return Ok(response);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] RVRequest request)
        {
            var result = await _rvService.Update(_mapper.Map<RV>(request));

            if (result == null) return NotFound();

            var rv = _mapper.Map<RVResponse>(result);

            return Ok(rv);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _rvService.Delete(id);
            return Ok();
        }

        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAll()
        {
            var result = await _rvService.GetAll();

            if (result == null)
            {
                return NotFound("Collection is empty");
            }

            var RVresult = _mapper.Map<IEnumerable<RVResponse>>(result);

            return Ok(RVresult);
        }
    }
}
