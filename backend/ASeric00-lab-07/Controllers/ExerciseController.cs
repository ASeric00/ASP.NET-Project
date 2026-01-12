using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASeric00_lab_07.Controllers
{
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        //CREATING 3 ENDPOINTS FROM REQUIREMENTS OF TASK

        // -- ROUTE
        [HttpGet("route/{temperature}/{stationName}")]
        public IActionResult GetInfoFromRoute([FromRoute] int temperature, [FromRoute] string stationName)
        {
            return Ok($"Temperature {temperature} reading at {stationName} :: from route!");
        }

        // -- QUERY
        [HttpGet("query")]
        public IActionResult GetInfoFromQuery([FromQuery] int temperature, [FromQuery] string stationName)
        {
            return Ok($"Temperature {temperature} reading at {stationName} :: from query!");
        }

        // -- BODY
        [HttpGet("body")]
        public IActionResult GetInfoFromBody([FromBody] string stationName)
        {
            return Ok($"Hello from {stationName} Weather Station!");
        }

    }
}
