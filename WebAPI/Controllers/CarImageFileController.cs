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
    public class CarImageFileController : ControllerBase
    {
        ICarImageFileService _carImageFileService;

        public CarImageFileController(ICarImageFileService carImageFileService)
        {
            _carImageFileService = carImageFileService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] IFormFile imageFile, [FromForm] CarImage carImage)
        {
            var result = _carImageFileService.AddImage(carImage, imageFile);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = _carImageFileService.GetAllImages();

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getallbycarid")]
        public async Task<IActionResult> GetAllByCarId(int carId)
        {
            var result = _carImageFileService.GetAllImagesByCarId(carId);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromForm] CarImage carImage)
        {
            var result = _carImageFileService.DeleteImage(carImage);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromForm] IFormFile imageFile, [FromForm] CarImage carImage)
        {
            var result = _carImageFileService.UpdateImage(carImage, imageFile);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
