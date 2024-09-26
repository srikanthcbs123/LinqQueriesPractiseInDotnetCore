using LinqQueriesPractiseInDotnetCore.DBCONNECT;
using LinqQueriesPractiseInDotnetCore.Northwind;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace LinqQueriesPractiseInDotnetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NormalLinqQueries : ControllerBase
    {
        Northwind_DBContext _northwind_DBContext;
        NorthwindContext _northwindContext;
        public NormalLinqQueries(Northwind_DBContext northwind_DBContext, NorthwindContext northwindContext)
        {
            _northwind_DBContext = northwind_DBContext;
            _northwindContext=northwindContext;
        }
        [HttpGet]
        [Route("GetEmployeesData")]
        public async Task<IActionResult> GetEmployeesData()
        {//here we are fetchingall employess  data.
            var result = from a in _northwind_DBContext.Employees select a;
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("GetEmployeesDatawith_ITDepartment")]
        public async Task<IActionResult> GetAllOrders()
        {//it will return employee data with it department along with all the columns data
            var result = from a in _northwind_DBContext.Employees where a.Designation == "IT" select a;
            //It converts your data to jsonformat
            var convertedData=JsonConvert.SerializeObject(result);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("GetEmployeesDatawith_ITDepartmentwithonlyname")]
        public async Task<IActionResult> GetOnlyrequiredNames()
        {//here we are fetchingall the data and showing only one column only.
            var result = from a in _northwind_DBContext.Employees select new {FullName=a.Name };
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("GetEmployeesDataWithNamestatswiths")]
        public async Task<IActionResult> GetDataByNamesStartswiths()
        {//here we are fetchingall employess  data.
            var result = from s in _northwind_DBContext.Customers where s.ContactName.StartsWith("A") select s;
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("GetEmployees&DeptDataByUsingJoins")]
        public async Task<IActionResult> GetDataByUsingJoins()
        {
            //here we are fetching employess&DepartMenent with data by using joins and orderby descending with required columns.
            //sqlquery:select * from employee e join Departments d on d.Id=e.EmpId order by e.City desc
            var result = from e in _northwindContext.Employees join d in _northwindContext.Departments on e.EmpId equals d.Id  orderby e.City descending select new { e.FirstName, e.LastName, e.City, d.DeptName };
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("take(number) Usage")]
        public async Task<IActionResult> TakeUsage()
        {
            //if you want to get the only first 5 records in atable use this take(number) method.
            //select top 5 * from customers
            var result = (from lstcustmer in _northwindContext.Customers select lstcustmer).Take(5);
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("Skip(number) Usage")]
        public async Task<IActionResult> SkipUsage()
        {
            //if you want to get the only first 5 records in a table use this take(number) method.
            //after using the take() method you can use skip() method .
            //skip will skip or ignore the given count of records after taking the records.
            //select top 5 * from customers
            var result = (from lstcustmer in _northwindContext.Customers select lstcustmer).Take(5).Skip(4);
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
    }
}



