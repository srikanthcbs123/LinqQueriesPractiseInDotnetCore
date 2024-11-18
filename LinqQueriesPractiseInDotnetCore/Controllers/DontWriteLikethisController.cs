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
    public class DontWriteLikethisController : ControllerBase
    {
        Northwind_DBContext _northwind_DBContext;
        NorthwindContext _northwindContext;
        public DontWriteLikethisController(Northwind_DBContext northwind_DBContext, NorthwindContext northwindContext)
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
            List<StudentData> studentList = new List<StudentData>() {
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        //============================================
        [HttpGet]
        [Route("GetEmployeesData")]
        public async Task<IActionResult> GetEmployeesData1()
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
            var convertedData = JsonConvert.SerializeObject(result);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("GetEmployeesDatawith_ITDepartmentwithonlyname")]
        public async Task<IActionResult> GetOnlyrequiredNames()
        {//here we are fetchingall the data and showing only one column only.
            var result = from a in _northwind_DBContext.Employees select new { FullName = a.Name };
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
            var result = from e in _northwindContext.Employees join d in _northwindContext.Departments on e.EmpId equals d.Id orderby e.City descending select new { e.FirstName, e.LastName, e.City, d.DeptName };
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

        [HttpGet]
        [Route("AgeWithFilter")]
        public async Task<IActionResult> AgeWithFilter()
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
            var filteredResult = from s in lststudentsObj
                                 where s.Age > 15 && s.Age <= 20
                                 select new { FullName = s.StudentName };//giving the alisaname
                                                                         //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(filteredResult);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }

        [HttpGet]
        [Route("OrderByusage")]
        public async Task<IActionResult> OrderbyUsage()
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
            var orderByAscendingResult = from s in lststudentsObj
                                         orderby s.StudentName ascending
                                         select s;

            var orderByDescendingResult = from s in lststudentsObj
                                          orderby s.StudentName descending
                                          select s;
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(orderByDescendingResult);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }

        [HttpGet]
        [Route("GroupByusage")]
        public async Task<IActionResult> GroupByusage()
        {
            //example with dummydata
            List<StudentData> lststudentsObj = new List<StudentData>()
            {
               new StudentData() { StudentID = 1, StudentName = "John", Age = 13} ,
               new StudentData() { StudentID = 2, StudentName = "Moin",  Age = 13 } ,
               new StudentData() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
               new StudentData() { StudentID = 4, StudentName = "Ram" , Age = 20} ,
               new StudentData() { StudentID = 5, StudentName = "Ron" , Age = 15 }
            };
            var groupedStudents = lststudentsObj.GroupBy(s => s.Age)
                                     .Select(g => new { Age = g.Key, Students = g.ToList() });
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(groupedStudents);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }


        [HttpGet]
        [Route("GroupByusageWithCount")]
        public async Task<IActionResult> GroupByusageWithCount()
        {
            //example with dummydata
            // Define a list of fruits
            List<string> fruits = new List<string>
        {
            "apple", "banana", "orange", "apple", "grape", "banana", "apple"
        };

            // Group the fruits using Query syntax(RealTime Usethis one)
            var groupedFruits = fruits.GroupBy(f => f)
                          .Select(g => new { Fruit = g.Key, Count = g.Count() });

            // Group the fruits using method syntax
            var fruitsGrouped1 = fruits.GroupBy(fruit => fruit);

            // Print the grouped fruits
            foreach (var group in fruitsGrouped1)
            {
                Console.WriteLine($"Fruit: {group.Key}, Count: {group.Count()}");
            }
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(groupedFruits);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
    }
}
