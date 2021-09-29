using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImageController : ControllerBase
    {
        ICarImageService _carImageService;

        public CarImageController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _carImageService.GetAll();
            
            if(result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] IFormFile imageFile, [FromForm] CarImage carImage)
        {
            var result = _carImageService.AddImage(carImage, imageFile);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromForm] CarImage carImage)
        {
            var result = _carImageService.DeleteImage(carImage);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromForm] IFormFile imageFile, [FromForm] CarImage carImage)
        {
            var result = _carImageService.UpdateImage(carImage, imageFile);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
