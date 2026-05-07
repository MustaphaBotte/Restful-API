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
        Random random = new Random();

        [HttpGet("page/{LastId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<List<ClsCar>>> GetAllCars(int LastId)
        {
            if (LastId <= 0)
            {
                return BadRequest($"The LastID {LastId} is not valid!");
            }

            List<ClsCar>? Cars = await BusinessLayer.CarsService.GetAllCarsAsync(LastId);

            if (Cars == null || Cars.Count == 0)
                return NotFound("No cars in the system right now please try again :(");

            return Ok(Cars);

        }



        [HttpGet("{Id}", Name = "GetCarById")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClsCar>> GetCarById(int Id)
        {
            if (Id <= 0)
                return BadRequest($"The Id {Id} is not valid!");

            ClsCar? Car = await BusinessLayer.CarsService.GetCarById(Id);

            if (Car == null)
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
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }


        [HttpPost(Name = "AddNewCar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<object>> AddNewCar(ClsCar Car)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Car.MakeName) 
                    || string.IsNullOrWhiteSpace(Car.VehicleName)
                    || Car.Year > DateTime.Now.Year + 1 || Car.Year <= 0
                    || Car.NumDoors < 0)
                {
                    return BadRequest("Invalid Car Data.");
                }
                Car.Id = random.Next(1, 100); // simulate the insertion because the cars database is more complicated
                // Fk,Constraints ,checks,and a lot of required columns ,
                return CreatedAtRoute("GetCarById", new { id = Car.Id }, Car);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }



        [HttpDelete("{Id}", Name = "DeleteCar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> DeleteCar(int Id)
        {
            if(Id<=0)
            {
                return BadRequest($"Id with {Id} is not valid");
            }      
            try
            {
                if (await BusinessLayer.CarsService.GetCarById(Id) == null)
                {
                    return NotFound($"Car with Id {Id} Not Found!");
                }

                await BusinessLayer.CarsService.Delete(Id);          
                return Ok($"Car with Id {Id} deleted successfully");
                
            }
            catch (Exception ex) {
              return  Problem(ex.Message);
            }

        }


        [HttpPut(Name = "UpdateCar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCar(ClsCar updatedCar)
        {
            if (updatedCar == null 
                || updatedCar.Id<=0
                || string.IsNullOrWhiteSpace(updatedCar.MakeName) 
                || string.IsNullOrWhiteSpace(updatedCar.VehicleName) 
                || updatedCar.Year > DateTime.Now.Year + 1 || updatedCar.Year<=0
                || updatedCar.NumDoors < 0) { 
           
                return BadRequest($"invalid data was sent");
            }
            try
            {
                int Id = updatedCar.Id;

                if (await BusinessLayer.CarsService.GetCarById(Id) == null)
                {
                    return NotFound($"Car with Id {Id} Not Found!");
                }

                // simulate the update
                return Ok(new { message = $"Car with Id {Id} updated successfully", data = updatedCar });

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

    }
}
