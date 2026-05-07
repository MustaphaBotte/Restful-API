using CarsApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarsApi.BusinessLayer
{
    public class CarsService
    {
        public static async Task<List<ClsCar>?> GetAllCarsAsync(int LastId)
        {
            try
            {
               return await DataAccess.CarRepository.GetAllCarsAsync(LastId);
            }
            catch (Exception) {
                throw new Exception("Internal server error please try again later");
            }
        }
        public static async Task<ClsCar?> GetCarById(int CarId)
        {
            try
            {
               return await DataAccess.CarRepository.GetCarById(CarId);
            }
            catch (Exception)
            {
                throw new Exception("Internal server error please try again later");
            }
        }
        public static async Task<int> Count( )
        {
            try
            {
                int Count =  await DataAccess.CarRepository.Count();
                return Count;
            }
            catch(Exception)
            {
                throw new Exception("An error occurred while processing your request.");
            }
        }

        public static async Task<bool> Delete(int Id)
        {
            try
            {
                return await DataAccess.CarRepository.Delete(Id);
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while processing your request.");
            }
        }
    }
}
