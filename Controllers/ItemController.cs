using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.ViewModel;

namespace WebApplication2.Controllers
{
    public class ItemController : Controller
    {
        private ECartDBEntities objECartDbEntities;

        public ItemController ()
        {
            objECartDbEntities = new ECartDBEntities();
        }

        // GET: Item
        public ActionResult Index ()
        {
           
            ECartDBEntities myCartDBEntitis = new ECartDBEntities();
            var getcatlist = myCartDBEntitis.Categories.ToList();

            SelectList list = new SelectList(getcatlist, "categoryId", "CategoryName");
            ViewBag.categoryList = list;

            return View();

     
        }

        [HttpPost]
        public JsonResult Index ( ItemViewModel objItemViewModel )
        {
            string NewImage = Guid.NewGuid() + Path.GetExtension(objItemViewModel.ImagePath.FileName);
            objItemViewModel.ImagePath.SaveAs(Server.MapPath("~/Images/" + NewImage));

            Item objItem = new Item();
            objItem.ImagePath = "~/Images/" + NewImage;
            objItem.CategoryId = objItemViewModel.CategoryId;
            objItem.Description = objItemViewModel.Description;
            objItem.ItemCode = objItemViewModel.ItemCode;
            objItem.ItemId = Guid.NewGuid();
            objItem.ItemName = objItemViewModel.ItemName;
            objItem.ItemPrice = objItemViewModel.ItemPrice;
            objECartDbEntities.Items.Add(objItem);
            objECartDbEntities.SaveChanges();

            return Json(new { Success = true, Message = "Item is added Successfully." }, JsonRequestBehavior.AllowGet);
        }
    }
}