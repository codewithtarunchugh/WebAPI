using System;
using System.Web.Http;
using System.Net.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using WebApplication1.Models;
using System.Web;

namespace WebApplication1.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = "select EmployeeID, EmployeeName,Department,convert(varchar(10),DateOfJoining, 120) as DateOfJoining, PhotoFileName from Employee";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Employee emp)
        {
            try
            {

                string query = string.Format("insert into Employee values ('{0}','{1}','{2}','{3}')", emp.EmployeeName, emp.Department, emp.DateOfJoining, emp.PhotoFileName);


                DataTable table = new DataTable();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))

                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);

                }
                return "Add Successfully !!";
            }
            catch (Exception) { return "Failed to Add!!"; }

        }
        public string Put(Employee emp)
        {
            try
            {

                string query = string.Format("update Employee set EmployeeName='{0}', Department='{1}',DateOfJoining='{2}',PhotoFileName='{3}' where EmployeeID={4}",
                    emp.EmployeeName, emp.Department, emp.DateOfJoining, emp.PhotoFileName, emp.EmployeeID);


                DataTable table = new DataTable();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))

                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);

                }
                return "Updated Successfully !!";
            }
            catch (Exception) { return "Failed to Update!!"; }

        }
        public string Delete(int ID)
        {
            try
            {
                string query = @"Delete from Employee where EmployeeID='" + ID + "'";

                DataTable table = new DataTable();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))

                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);

                }
                return "Deleted Successfully !!";
            }
            catch (Exception) { return "Failed to Delete !!"; }

        }

        [Route("api/Employee/GetAllDepartmentNames")]
        [HttpGet]
        public HttpResponseMessage GetAllDepartmentNames()
        {
            string query = "select DepartmentName from department";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);

        }
        [Route("api/Employee/SaveFile")]
        public string SaveFile()
        {
            try
            {
                var httprequest = HttpContext.Current.Request;
                var postedFile = httprequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + fileName);
                postedFile.SaveAs(physicalPath);
                return fileName;
            }
            catch (Exception) { return "anonymous.png"; }
        }
    }
}
