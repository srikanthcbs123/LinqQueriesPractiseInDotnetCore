using LinqQueriesPractiseInDotnetCore.DBCONNECT;
using LinqQueriesPractiseInDotnetCore.Models;
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
        [HttpGet]
        [Route("LamdatWithOrderByusage")]
        public async Task<IActionResult> LamdatWithOrderByusage()
        {
            //example with dummydata
            List<StudentData> lststudentsObj = new List<StudentData>()
            {
               new StudentData() { StudentID = 1, StudentName = "John", Age = 13} ,
               new StudentData() { StudentID = 2, StudentName = "Moin",  Age = 21 } ,
               new StudentData() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
               new StudentData() { StudentID = 4, StudentName = "Ram" , Age = 20} ,
               new StudentData() { StudentID = 5, StudentName = "Ron" , Age = 15 }
            };
            var thenByResult = lststudentsObj.OrderBy(s => s.StudentName).ThenBy(s => s.Age);

            var thenByDescResult = lststudentsObj.OrderBy(s => s.StudentName).ThenByDescending(s => s.Age);
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(thenByDescResult);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }

        [HttpGet]
        [Route("InnerJoinsusage")]
        public async Task<IActionResult> Joinsusage()
        {
            //example with dummydata
            IList<StudentData> studentList = new List<StudentData>() {
            new StudentData() { StudentID = 1, StudentName = "John", StandardID =1 },
            new StudentData() { StudentID = 2, StudentName = "Moin", StandardID =1 },
            new StudentData() { StudentID = 3, StudentName = "Bill", StandardID =2 },
            new StudentData() { StudentID = 4, StudentName = "Ram" , StandardID =2 },
            new StudentData() { StudentID = 5, StudentName = "Ron"  }
            };

            IList<Standard> standardList = new List<Standard>() {
            new Standard(){ StandardID = 1, StandardName="Standard 1"},
            new Standard(){ StandardID = 2, StandardName="Standard 2"},
            new Standard(){ StandardID = 3, StandardName="Standard 3"}
            };
            //INNER JOIN USED TO FETCH THE COMMON DATA BETWEEN TWO CLASES.
            //Note:one common column is required to perform any join in table.
            var innerJoinResult = studentList.Join(// outer sequence 
                                  standardList,  // inner sequence 
                                  student => student.StandardID,    // outerKeySelector
                                  standard => standard.StandardID,  // innerKeySelector
                                  (student, standard) => new  // result selector
                                  {
                                      StudentName = student.StudentName,
                                      StandardName = standard.StandardName
                                  });
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(innerJoinResult);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("Max() example1usage")]
        public async Task<IActionResult> MaxExampleusage1()
        {
            //example with dummydata
            List<int> intList = new List<int>() { 10, 21, 30, 45, 50, 87 };
            //CHECK THE MAX VALUE BY USING BELOW QUERY
            var Maxvalue = intList.Max();

            //CHECK THE Count VALUE BY USING BELOW QUERY
            var CountOfRecords = intList.Count();

            //sum of the list
            var Sumofthelist = intList.Sum();


            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(Maxvalue);
            return StatusCode(StatusCodes.Status200OK, convertedData);
        }
        [HttpGet]
        [Route("Max() example2usage")]
        public async Task<IActionResult> MaxExampleusage2()
        {
            List<StudentData> studentList = new List<StudentData> () {
            new StudentData() { StudentID = 1, StudentName = "John", Age = 13 } ,
            new StudentData() { StudentID = 2, StudentName = "Moin", Age = 21 } ,
            new StudentData() { StudentID = 3, StudentName = "Bill", Age = 18 } ,
            new StudentData() { StudentID = 4, StudentName = "Ram", Age = 20 } ,
            new StudentData() { StudentID = 5, StudentName = "Ron", Age = 15 }
            };

            var MaxAge = studentList.Max(s => s.Age);

            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(MaxAge);
            return StatusCode(StatusCodes.Status200OK, convertedData);
        }
        [HttpGet]
        [Route("First()usage")]
        public async Task<IActionResult> DistinctUsage()
        {
            try
            {
                //Fetching the First Element from the Data Source using First Method
                List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                int MethodSyntax = numbers.First();
                int MethodSyntax2 = numbers.FirstOrDefault();

                List<int> numbersEmpty = new List<int>();
                List<string> stringEmpty = new List<string>();
                //If the value of list is empty,if you apply first() method it will throw the error.
                //error is:sequence contains no elements.
                //int MethodFirst = numbersEmpty.First();
                //====================================================================
                //FirstOrdefault if the value is not exist it will not throw any error and it will show the default value.
                //it means in our example our list<int> used.due to that it will return deafulat value of int is "0"

                int IntMethodFirstOrDefault = numbersEmpty.FirstOrDefault();
                //it means in our example our list<string> used.due to that it will return deafulat value of string is  "null"
                string strMethodFirstOrDefault = stringEmpty.FirstOrDefault();

                //It converts your data to jsonformat
                var convertedData = JsonConvert.SerializeObject(MethodSyntax);
                return StatusCode(StatusCodes.Status200OK, convertedData);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
