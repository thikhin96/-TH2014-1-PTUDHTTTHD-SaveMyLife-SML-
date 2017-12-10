﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using DataModel;
using DataService.Interfaces;
using DataService.Dtos;
using SML_QLNPP.Models;

namespace SML_QLNPP.Controllers
{
    public class DistributorController : Controller
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        IDistributorService service;

        public DistributorController(IDistributorService service)
        {
            this.service = service;
        }

        // GET: Distributor
        public ActionResult Distributor()
        {
            logger.Info("Start controller....");
            DistributorViewModel model = new DistributorViewModel();
            model.listDis = service.GetAll();
            return View(model);
        }
    }
}