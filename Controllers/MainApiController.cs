﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelSaber.Main.Controllers
{
    public class MainApiController : Controller
    {
        [Route("api/")]
        public IActionResult Index()
        {
            return Ok($"Build: {Program.Version}, Build Time: {Program.CompiledTime.ToUniversalTime():O}");
        }
    }
}
