using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DeveloperTest.Controllers
{
    [ApiController, Route("[controller]")]
    public class TypesController : ControllerBase
    {
        private static readonly string[] Types = { "Small", "Large" };

        private readonly ILogger<EngineerController> _logger;

        public TypesController(ILogger<EngineerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Types;
        }
    }
}