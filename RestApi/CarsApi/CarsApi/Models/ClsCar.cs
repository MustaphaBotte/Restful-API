namespace CarsApi.Models
{
    public class ClsCar
    {
        public  int Id { get; set; }       
        public  string VehicleName { get; set; }       
        public  string MakeName { get; set; }        
        public  int Year { get; set; }        
        public  int Engine_CC { get; set; }      
        public  int NumDoors { get; set; }

        public ClsCar(int id, string vehicleName, string makeName, int year, int engine_CC, int numDoors)
        {
            Id = id;
            VehicleName = vehicleName;
            MakeName = makeName;
            Year = year;
            Engine_CC = engine_CC;
            NumDoors = numDoors;
        }
    }
}
