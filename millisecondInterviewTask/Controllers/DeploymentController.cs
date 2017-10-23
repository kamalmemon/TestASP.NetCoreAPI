using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using millisecondInterviewTask.Models;
using millisecondInterviewTask.Interfaces;

namespace millisecondInterviewTask.Controllers
{
    [Route("api/[controller]")]
    public class DeploymentController : Controller
    {
        private readonly IDataAccessLayer _dataAccessLayer;
        public DeploymentController(IDataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }

        // GET api/values
        [HttpGet("allData")]
        [Produces(typeof(List<DeploymentRoot>))]
        public IActionResult Get()
        {
            try
            {
                return Ok(_dataAccessLayer.GetAllDocumentsAsync());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/values
        [HttpPost("setData")]
        public IActionResult Post([FromBody]DeploymentRoot bodyObj)
        {
            try
            {
                _dataAccessLayer.CreateDocumentAsync(bodyObj);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

     
    }
}
