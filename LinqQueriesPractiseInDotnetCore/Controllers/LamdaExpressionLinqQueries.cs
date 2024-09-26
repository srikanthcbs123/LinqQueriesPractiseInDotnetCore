using LinqQueriesPractiseInDotnetCore.DBCONNECT;
using LinqQueriesPractiseInDotnetCore.Northwind;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LinqQueriesPractiseInDotnetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LamdaExpressionLinqQueries : ControllerBase
    {
        Northwind_DBContext _northwind_DBContext;
        NorthwindContext _northwindContext;
        public LamdaExpressionLinqQueries(Northwind_DBContext northwind_DBContext, NorthwindContext northwindContext)
        {
            _northwind_DBContext = northwind_DBContext;
            _northwindContext = northwindContext;
        }

        [HttpGet]
        [Route("LamdaTest")]
        public async Task<IActionResult> GetEmployeesData()
        {
            int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //caluclate the count of even numbers
            //*)In general Lamda expression syntax is
                                          //Variable => Expression
            int evenvaluescount = array.Count(a => a % 2 == 0);//a%2==0 is caluclating the even numbers.
            Console.WriteLine("even values count" + evenvaluescount);
            int oddvaluescount1 = array.Count(a => a % 2 != 0);//odd values finding.
            Console.WriteLine("even values count" + oddvaluescount1);
            //to caluclate the greater then  5 count values
            int countgreatherthefive = array.Count(a => a > 5);
            Console.WriteLine("print the greater the five value" + countgreatherthefive);

            return StatusCode(StatusCodes.Status200OK, 0);

        }

    }
}
