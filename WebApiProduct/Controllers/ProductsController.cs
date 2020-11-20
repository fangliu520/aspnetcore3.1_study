using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApiProduct.Utility.Filter;

namespace WebApiProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
   // [CrossDomainFilter]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/Products
        [HttpGet]
        //[Authorize] //jwt鉴权授权访问Microsoft.AspNetCore.Authorization;
        //[AllowAnonymous]//鉴权过程中允许不用验证
        
        public IEnumerable<string> Get()
        {
            return new string[] { "苹果", "梨子", "石榴", _configuration["Port"] };
        }

        // GET: api/Products/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return $"value:{id}";
        }

        // POST: api/Products
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
