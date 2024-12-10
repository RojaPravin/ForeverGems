using ForeverGems.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ForeverGems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeedataController : ControllerBase
    {
        string constr = "Data Source = DESKTOP-RNPM1SO ; Initial Catalog = ForeverGems ; Integrated Security = True";
        // GET: api/<EmployeedataController>
        [HttpGet]
        public JsonResult Get()
        {
            List<Employeedata> list = new List<Employeedata>();
            SqlConnection con = new SqlConnection(constr);
            string query = "sp_Employee_Index";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                list.Add(new Employeedata
                {
                    id = Convert.ToInt32(sdr["id"]),
                    name = sdr["name"].ToString(),
                    designation = sdr["designation"].ToString(),
                    salary = Convert.ToInt32(sdr["salary"])

                });
            }
            con.Close();
            return new JsonResult(list);
        }

        // GET api/<EmployeedataController>/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            Employeedata obj = new Employeedata();
            SqlConnection con = new SqlConnection(constr);
            string query = "sp_Employee_Details " + id;
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                obj = new Employeedata
                {
                    id = Convert.ToInt32(sdr["id"]),
                    name = sdr["name"].ToString(),
                    designation = sdr["designation"].ToString(),
                    salary = Convert.ToInt32(sdr["salary"])

                };
            }
            con.Close();
            return new JsonResult(obj);
        }

        // POST api/<EmployeedataController>
        [HttpPost]
        public void Post(Employeedata employee)
        {
            SqlConnection con = new SqlConnection(constr);
            string query = "sp_Employee_Create @name, @designation, @salary";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", employee.name);
            cmd.Parameters.AddWithValue("@designation", employee.designation);
            cmd.Parameters.AddWithValue("@salary", employee.salary);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        // PUT api/<EmployeedataController>/5
        [HttpPut("{id}")]
        public void Put(int id, Employeedata employee)
        {
            SqlConnection con = new SqlConnection(constr);
            string query = "sp_Employee_Edit @id , @name, @designation, @salary";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("@name", employee.name);
            cmd.Parameters.AddWithValue("@designation", employee.designation);
            cmd.Parameters.AddWithValue("@salary", employee.salary);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        // DELETE api/<EmployeedataController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            SqlConnection con = new SqlConnection(constr);
            string query = "sp_Employee_Delete " + id;
            SqlCommand cmd = new SqlCommand(query, con);
            
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
