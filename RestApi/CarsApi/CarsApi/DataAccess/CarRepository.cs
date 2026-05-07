using CarsApi.Models;
using Microsoft.Data.SqlClient;

namespace CarsApi.DataAccess
{

    public class CarRepository
    {
        private static readonly string _ConnectionString = @"Server=.\MSSQLSERVER1;Database=Cars; User=sa;Password=123456;TrustServerCertificate=True;";
        public static async Task<List<ClsCar>?> GetAllCarsAsync(int LastId)
        {
            const string Query = @"select top 10 ID, Vehicle_Display_Name, Makes.Make, Year, Engine_CC, NumDoors 
                                  from VehicleDetails inner join Makes on Makes.MakeID = VehicleDetails.MakeID  
                                  where ID > @id                                  
                                  order by ID asc";

            using (SqlConnection connection = new SqlConnection(connectionString: _ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@id", LastId);

                    try
                    {
                        await connection.OpenAsync();
                        SqlDataReader CarsReader =await command.ExecuteReaderAsync();
                        if (CarsReader.HasRows)
                        {
                            List<ClsCar> Cars = new List<ClsCar>();

                            while (CarsReader.Read())
                            {
                                Cars.Add(new ClsCar(
                                    int.TryParse(CarsReader["ID"].ToString() ?? "0", out int id) ? id : 0,
                                    CarsReader["Vehicle_Display_Name"].ToString() ?? "N/A",
                                    CarsReader["Make"].ToString() ?? "N/A",
                                    int.TryParse(CarsReader["year"].ToString() ?? "0", out int year) ? year : 0,
                                    int.TryParse(CarsReader["engine_cc"].ToString() ?? "0", out int engine_cc) ? engine_cc : 0,
                                    int.TryParse(CarsReader["numdoors"].ToString() ?? "0", out int numdoors) ? numdoors : 0));
                            }
                            return Cars;
                        }
                        return null;
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                        // log it later
                        throw;
                    }

                }

            }

        }

        public static async Task<ClsCar?> GetCarById(int CarId)
        {
            const string Query = @"select top 1 ID, Vehicle_Display_Name, Makes.Make, Year, Engine_CC, NumDoors 
                                  from VehicleDetails inner join Makes on Makes.MakeID = VehicleDetails.MakeID  where ID = @id";

            using (SqlConnection connection = new SqlConnection(connectionString: _ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@id", CarId);
                    try
                    {
                        await connection.OpenAsync();
                        SqlDataReader CarsReader = await command.ExecuteReaderAsync();
                        if (CarsReader.HasRows)
                        {

                            if(CarsReader.Read())
                            {
                               return new ClsCar(
                                    int.TryParse(CarsReader["ID"].ToString() ?? "0", out int id) ? id : 0,
                                    CarsReader["Vehicle_Display_Name"].ToString() ?? "N/A",
                                    CarsReader["Make"].ToString() ?? "N/A",
                                    int.TryParse(CarsReader["year"].ToString() ?? "0", out int year) ? year : 0,
                                    int.TryParse(CarsReader["engine_cc"].ToString() ?? "0", out int engine_cc) ? engine_cc : 0,
                                    int.TryParse(CarsReader["numdoors"].ToString() ?? "0", out int numdoors) ? numdoors : 0);
                            }
                            return null;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        // log it later
                        throw;
                    }

                }

            }
            return null;
        }

        public static async Task<int> Count()
        {        
                const string Query = @"select count(*) from VehicleDetails";

                using (SqlConnection connection = new SqlConnection(connectionString: _ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        await connection.OpenAsync();
                        var result = await command.ExecuteScalarAsync();

                        if (result is int Count)
                            return Count;

                        else
                            throw new Exception("An Error Occured while counting the total cars");
                    }

                }
                   
        }

        public static async Task<bool> Delete(int Id)
        {
            const string Query = @"delete from VehicleDetails where Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString: _ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("id", Id);
                    await connection.OpenAsync();
                    var result = await command.ExecuteNonQueryAsync();

                    if (result is int affectedRows)
                        return affectedRows>0;

                    else
                        throw new Exception("An Error Occured while deleting the car");
                }

            }

        }

    }
}
