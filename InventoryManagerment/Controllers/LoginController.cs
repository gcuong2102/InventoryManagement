﻿using InventoryManagerment.Common;
using InventoryManagerment.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagerment.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {           
            return View();
        }
        public ActionResult Login(InventoryManagerment.Models.LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var functions = new DataAccess();
                int result = functions.CheckUser(model);
                if (result == 1)
                {
                    HttpCookie userCookie = new HttpCookie(Common.CommonConstants.USER_DATA);
                    var user = functions.GetUser(model.UserName);
                    userCookie[Common.CommonConstants.USER_NAME] = user.UserName;
                    userCookie[Common.CommonConstants.ROLE_ID] = user.RoleID.ToString();
                    userCookie[CommonConstants.User_ID] = user.ID.ToString();
                    userCookie.Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies.Add(userCookie);
                    if (Session[Common.CommonConstants.URL_DATA] != null)
                    {
                        string url = Session[Common.CommonConstants.URL_DATA].ToString();
                        Session[Common.CommonConstants.URL_DATA] = null;
                        return Redirect(url);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Sai mật khẩu, vui lòng nhập lại mật khẩu");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại, vui nhập lại tài khoản");
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản bạn đang bị khóa, vui lòng thử lại sau");
                }
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            HttpCookie userCookie = new HttpCookie(Common.CommonConstants.USER_DATA);
            userCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(userCookie);
            return View("Index");
        }
    }
}