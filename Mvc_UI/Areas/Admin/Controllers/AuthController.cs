﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}