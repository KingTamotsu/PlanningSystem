﻿using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.ApplicationInsights;
using PlanningSystem.Models;

namespace PlanningSystem.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Overview()
        {
            return View();
        }

        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        // POST: Account/Create
        public ActionResult CreateAccount(Account account)
        {
            //var context = new PlanningSystemEntities();
            //var user = new User
            //{
            //    userId = account.userId,
            //    username = account.username,
            //    password = account.password,
            //    firstLogin = account.firstLogin,
            //    createdAt = account.createdAt,
            //    //role = account.role.name
            //};
            //context.User.Add(user);
            //context.SaveChanges();
            return RedirectToAction("Overview", "Account");
        }

        // PUT: Account/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult EditAccount(Account account)
        {
            return RedirectToAction("Overview", "Account");
        }

        // DEL: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View("Overview");
        }


        //// POST: Account/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


        //// POST: Account/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


        //// POST: Account/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}