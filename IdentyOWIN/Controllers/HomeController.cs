﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentyOWIN.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Ключ", "Значение");
            return View(data);
        }

    } // end controller

} // end namespace