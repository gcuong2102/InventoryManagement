using InventoryManagerment.Common;
using InventoryManagerment.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InventoryManagerment.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cookie = Request.Cookies[Common.CommonConstants.USER_DATA];
            if (cookie == null)
            {
                Session[Common.CommonConstants.URL_DATA] = Request.Url.ToString();
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }
            else if(cookie != null)
            {
                var result = new DataAccess().CheckUserName(cookie[Common.CommonConstants.USER_NAME]);
                if (!result)
                {
                    Session[Common.CommonConstants.URL_DATA] = Request.Url.ToString();
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
                }
                else
                {
                    if (long.Parse(cookie[Common.CommonConstants.ROLE_ID]) != 1)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }
        protected void SetAlert(string messenger, string type)
        {
            TempData["AlertMessenger"] = messenger;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "danger")
            {
                TempData["AlertType"] = "alert-danger";
            }
            else
            {
                TempData["AlertType"] = "alert-warning";
            }
        }
        protected string GetUserName() 
        {
            HttpCookie cookie = Request.Cookies[Common.CommonConstants.USER_DATA];
            string userName = cookie[Common.CommonConstants.USER_NAME];
            return userName;
        }
    }
}