using CarsApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarsApi.Controller
{
    [Route("api/Cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        [HttpGet("page/{LastId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<List<ClsCar>>> GetAllCars(int LastId)
        {
            if(LastId<=0)
            {
                return BadRequest($"The LastID {LastId} is not valid!");
            }
           
            List<ClsCar>? Cars = await BusinessLayer.CarsService.GetAllCarsAsync(LastId);

            if (Cars == null || Cars.Count == 0)
                      return NotFound("No cars in the system right now please try again :(");

            return Ok(Cars);
            
        }


        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClsCar>> GetCarById(int Id)
        {
            if (Id <= 0)  
                return BadRequest($"The Id {Id} is not valid!");
            
            ClsCar? Car = await BusinessLayer.CarsService.GetCarById(Id);

            if (Car == null )          
                return NotFound($"Car with id {Id} not found :(");

            return Ok(Car);
      
        }


        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<object>> TotalCars()
        {
            try
            {
                int Count = await BusinessLayer.CarsService.Count();
                return Ok(new { message = "success", count = Count });
            }
            catch (Exception ex) 
            {
                return Problem(detail: ex.Message, statusCode:500);
            }
        }


    }
}
