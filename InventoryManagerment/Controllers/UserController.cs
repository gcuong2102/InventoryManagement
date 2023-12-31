﻿using InventoryManagerment.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagerment.Controllers
{
    public class UserController : AdminController
    {
        // GET: User
        public ActionResult Index(string searchString,int page=1,int pageSize=10)
        {
            TempData[Common.CommonConstants.PAGE_NAME] = "Người dùng";
            ViewBag.Title = "Tuấn Hoan - Người Dùng";
            var model = new DataAccess().ListAllUserOnPagedlist(searchString,page,pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            TempData[Common.CommonConstants.PAGE_NAME] = "Thêm người dùng";
            ViewBag.Title = "Tuấn Hoan - Thêm Người Dùng";
            SetViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult Create(User model)
        {
            var dao = new DataAccess();
            TempData[Common.CommonConstants.PAGE_NAME] = "Thêm người dùng";
            ViewBag.Title = "Tuấn Hoan - Thêm Người Dùng";
            SetViewBag();
            model.Status = true;
            bool result = dao.InsertUser(model,GetUserName());
            if (result)
            {
                ModelState.Clear();
                ModelState.AddModelError("","Thêm người dùng thành công");               
            }
            else
            {
                ModelState.AddModelError("","Thêm người dùng thất bại");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            TempData[Common.CommonConstants.PAGE_NAME] = "Chỉnh sửa người dùng";
            ViewBag.Title = "Tuấn Hoan - Chỉnh Sửa Người Dùng";
            var dao = new DataAccess();
            var model = dao.GetUser("",id);
            SetViewBag(model.RoleID);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(User model)
        {
            TempData[Common.CommonConstants.PAGE_NAME] = "Chỉnh sửa người dùng";
            ViewBag.Title = "Tuấn Hoan - Chỉnh Sửa Người Dùng";
            var dao = new DataAccess();
            var result = dao.UpdateUser(model,GetUserName());
            SetViewBag(model.RoleID);
            if (result)
            {

                HttpCookie httpcookie = Request.Cookies[Common.CommonConstants.USER_DATA];
                if (model.ID.ToString() == httpcookie[Common.CommonConstants.User_ID])
                {
                    httpcookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(httpcookie);
                    RedirectToAction("Index", "Login");
                }
                else
                {
                    return View();
                }
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ModelState.AddModelError("", "Tên người dùng này đã tồn tại, vui lòng thử lại");
                return View();
            }

        }
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new DataAccess();
            ViewBag.RoleID = new SelectList(dao.ListAllRoleToViewBag(),"ID","Name",selectedId);
        }
        [HttpPost]
        public ActionResult Delete(long id)
        {
            var dao = new DataAccess();
            bool result = dao.DeleteUser(id,GetUserName());
            return RedirectToAction("Index");
        }
    }
}