using Final.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WinnersController : ControllerBase
    {   

        private readonly SqlConnection cnn = new SqlConnection("Data Source=DESKTOP-IECCAGD;Initial Catalog=fia;Integrated Security=True");
        // GET: api/<WinnersController>
        [HttpGet]
        public IEnumerable<Winners> Get()
        {
            
            
                List<Winners> winners = new List<Winners>();
                cnn.Open();
                SqlCommand cmd = new SqlCommand("Winners_GetAll", cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Drivers driver = new Drivers
                    {
                        DriverID = Convert.ToInt32(reader["DriverID"]),
                        Name = Convert.ToString(reader["Name"]),
                        Lastname = Convert.ToString(reader["Lastname"]),
                        Age = Convert.ToInt32(reader["Age"]),
                        Country = Convert.ToString(reader["Country"]),
                        Titles = Convert.ToInt32(reader["Titles"])
                    };
                    Cars car = new Cars
                    {
                        CarID = Convert.ToInt32(reader["CarID"]),
                        Model = Convert.ToString(reader["Model"]),
                        Manufacturer = Convert.ToString(reader["Manufacturer"]),
                        Year = Convert.ToInt32(reader["Year"])

                    };

                    Tracks track = new Tracks()
                    {
                        TrackID = Convert.ToInt32(reader["TrackID"]),
                        TrackName = Convert.ToString(reader["TrackName"]),
                        Location = Convert.ToString(reader["Location"]),
                        Length = Convert.ToInt32(reader["Length"]),
                        Capacity = Convert.ToInt32(reader["Capacity"]),
                        Turns = Convert.ToInt32(reader["Turns"])

                    };
                    Winners winner = new Winners()
                    {
                        WinnerID = Convert.ToInt32(reader["WinnerID"]),
                        Driver = driver,
                        Car = car,
                        Track = track,
                        Date = Convert.ToDateTime(reader["Date"])
                    };
                    winners.Add(winner);
                }

                cnn.Close();
                return winners;
            
           
        }

        // GET api/<WinnersController>/5
        [HttpGet("{id}")]
        public Winners Get(int id)
        {

            Winners winner = new Winners();
            cnn.Open();
            SqlCommand cmd = new SqlCommand("Winners_GetByID", cnn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Drivers driver = new Drivers
                {
                    DriverID = Convert.ToInt32(reader["DriverID"]),
                    Name = Convert.ToString(reader["Name"]),
                    Lastname = Convert.ToString(reader["Lastname"]),
                    Age = Convert.ToInt32(reader["Age"]),
                    Country = Convert.ToString(reader["Country"]),
                    Titles = Convert.ToInt32(reader["Titles"])
                };
                Cars car = new Cars
                {
                    CarID = Convert.ToInt32(reader["CarID"]),
                    Model = Convert.ToString(reader["Model"]),
                    Manufacturer = Convert.ToString(reader["Manufacturer"]),
                    Year = Convert.ToInt32(reader["Year"])

                };

                Tracks track = new Tracks()
                {
                    TrackID = Convert.ToInt32(reader["TrackID"]),
                    TrackName = Convert.ToString(reader["TrackID"]),
                    Location = Convert.ToString(reader["Location"]),
                    Length = Convert.ToInt32(reader["Length"]),
                    Capacity = Convert.ToInt32(reader["Capacity"]),
                    Turns = Convert.ToInt32(reader["Turns"])

                };

                winner.WinnerID = Convert.ToInt32(reader["WinnerID"]);
                winner.Driver = driver;
                winner.Car = car;
                winner.Track = track;
                winner.Date = Convert.ToDateTime(reader["Date"]);




            }
            cnn.Close();
            return winner;
        }

        // POST api/<WinnersController>
        [HttpPost]
        public ActionResult Post(Winners winner)
        {
            try 
            {

               
                cnn.Open();
                SqlCommand cmd = new SqlCommand("Drivers_Add", cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", winner.Driver.Name);
                cmd.Parameters.AddWithValue("@Lastname", winner.Driver.Lastname);
                cmd.Parameters.AddWithValue("@Age", winner.Driver.Age);
                cmd.Parameters.AddWithValue("@Country", winner.Driver.Country);
                cmd.Parameters.AddWithValue("@Titles", winner.Driver.Titles);
                cmd.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("Cars_Add", cnn);
                cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@Model", winner.Car.Model);
                cmd2.Parameters.AddWithValue("@Manufacturer", winner.Car.Manufacturer);
                cmd2.Parameters.AddWithValue("@Year", winner.Car.Year);
                cmd2.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand("Tracks_Add", cnn);
                cmd3.CommandType = System.Data.CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@TrackName", winner.Track.TrackName);
                cmd3.Parameters.AddWithValue("@Location", winner.Track.Location);
                cmd3.Parameters.AddWithValue("@Length", winner.Track.Length);
                cmd3.Parameters.AddWithValue("@Capacity", winner.Track.Capacity);
                cmd3.Parameters.AddWithValue("@Turns", winner.Track.Turns);
                cmd3.ExecuteNonQuery();

                cnn.Close();
                int id1 = LastDriverID();
                int id2 = LastCarID();
                int id3 = LastTrackID();

                cnn.Open();
                SqlCommand cmd4 = new SqlCommand("Winners_Add", cnn);
                cmd4.CommandType = System.Data.CommandType.StoredProcedure;
                cmd4.Parameters.AddWithValue("@DriverID",id1 );
                cmd4.Parameters.AddWithValue("@CarID",id2 );
                cmd4.Parameters.AddWithValue("@TrackID",id3 );
                cmd4.Parameters.AddWithValue("@Date", winner.Date);
                cmd4.ExecuteNonQuery();
                cnn.Close();
                return Ok(LastWinnerID());
            }

            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<WinnersController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id,Winners winner)
        {
            try
            {
                if (winner.WinnerID == 0) winner.WinnerID = id;
                if (winner.WinnerID != id) return BadRequest();
                
                cnn.Open();
                SqlCommand cmd = new SqlCommand("UpdateAllAtOnce",cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@WinnerID", id);
                cmd.Parameters.AddWithValue("@Date", winner.Date);
                cmd.Parameters.AddWithValue("@DriverID", winner.Driver.DriverID);
                cmd.Parameters.AddWithValue("@Name", winner.Driver.Name);
                cmd.Parameters.AddWithValue("@Lastname", winner.Driver.Lastname);
                cmd.Parameters.AddWithValue("@Age", winner.Driver.Age);
                cmd.Parameters.AddWithValue("@Country", winner.Driver.Country);
                cmd.Parameters.AddWithValue("@Titles", winner.Driver.Titles);
                cmd.Parameters.AddWithValue("@CarID", winner.Car.CarID);
                cmd.Parameters.AddWithValue("@Model", winner.Car.Model);
                cmd.Parameters.AddWithValue("@Manufacturer", winner.Car.Manufacturer);
                cmd.Parameters.AddWithValue("@Year", winner.Car.Year);
                cmd.Parameters.AddWithValue("@TrackID",winner.Track.TrackID);
                cmd.Parameters.AddWithValue("@TrackName", winner.Track.TrackName);
                cmd.Parameters.AddWithValue("@Location", winner.Track.Location);
                cmd.Parameters.AddWithValue("@Length", winner.Track.Length);
                cmd.Parameters.AddWithValue("@Capacity", winner.Track.Capacity);
                cmd.Parameters.AddWithValue("@Turns", winner.Track.Turns);
                cmd.ExecuteNonQuery();
                cnn.Close();
                return Ok(winner.WinnerID);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }



        }

        // DELETE api/<WinnersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            int driver = 0;
            int car = 0;
            int track = 0;
            cnn.Open();
            SqlCommand cmd = new SqlCommand("Winners_GetByID", cnn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                driver = Convert.ToInt32(reader["DriverID"]);
                car = Convert.ToInt32(reader["CarID"]);
                track = Convert.ToInt32(reader["TrackID"]);
            }

            cnn.Close();
            cnn.Open();
            SqlCommand cmd2 = new SqlCommand("Winners_Delete", cnn);
            cmd2.CommandType = System.Data.CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@id", id);
            cmd2.ExecuteNonQuery();
            cnn.Close();
            cnn.Open();
            SqlCommand cmd3 = new SqlCommand("Drivers_Delete", cnn);
            cmd3.CommandType = System.Data.CommandType.StoredProcedure;
            cmd3.Parameters.AddWithValue("@id", driver);
            cmd3.ExecuteNonQuery();
            cnn.Close();
            cnn.Open();
            SqlCommand cmd4 = new SqlCommand("Cars_Delete", cnn);
            cmd4.CommandType = System.Data.CommandType.StoredProcedure;
            cmd4.Parameters.AddWithValue("@id", car);
            cmd4.ExecuteNonQuery();
            cnn.Close();
            cnn.Open();
            SqlCommand cmd5 = new SqlCommand("Tracks_Delete", cnn);
            cmd5.CommandType = System.Data.CommandType.StoredProcedure;
            cmd5.Parameters.AddWithValue("@id", track);
            cmd5.ExecuteNonQuery();
            cnn.Close();

        }

        private int LastDriverID()
        {
            int id = 0;
            cnn.Open();
            string query = "select top 1 DriverID from Drivers order by DriverID desc";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                id = Convert.ToInt32(reader["DriverID"]);
            }
            cnn.Close();
            return id;

        }

        private int LastCarID()
        {
            int id = 0;
            cnn.Open();
            string query = "select top 1 CarID from Cars order by CarID desc";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                id = Convert.ToInt32(reader["CarID"]);
            }
            cnn.Close();
            return id;

        }

        private int LastTrackID()
        {
            int id = 0;
            cnn.Open();
            string query = "select top 1 TrackID from Tracks order by TrackID desc";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                id = Convert.ToInt32(reader["TrackID"]);
            }
            cnn.Close();
            return id;

        }

        private int LastWinnerID()
        {
            int id = 0;
            cnn.Open();
            string query = "select top 1 WinnerID from Winners order by WinnerID desc";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                id = Convert.ToInt32(reader["WinnerID"]);
            }
            cnn.Close();
            return id;
        }
       
    }
}
