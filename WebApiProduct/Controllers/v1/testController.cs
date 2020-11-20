﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiProduct.Controllers.v1
{
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class testController : ControllerBase
    {
        [HttpGet]      
        public IEnumerable<string> Get()
        {
            return new string[] { "苹果", "梨子", "石榴","v1" };
        }
    }
}