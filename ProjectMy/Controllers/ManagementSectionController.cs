using DataLayer.DAC;
using DataLayer.Entities;
using ProjectMy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectMy.Controllers
{
    public class ManagementSectionController : Controller
    {
        public ActionResult ManagementSection()
        {
            return View();
        }

        public ActionResult DataFiles()
        {
            return View();
        }

        public ActionResult MenuItem()
        {
            return View();
        }

        public ActionResult MenuItem2(string category = "Pizza")
        {
            ViewBag.name = category;
            return View("MenuItem");
        }

        public ActionResult MealDeal()
        {
            return View();
        }

        public ActionResult Course()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult MenuItems(int? categoryId = 1)
        {
            return PartialView("_MenuItems", DAC.GetItemsByCategoryId(categoryId));
        }

        [HttpPost]
        public JsonResult AddItem(Item model)
        {
            if (model != null)
            {
                DAC.AddItem(model);
                return Json("item added", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("Please add Information", JsonRequestBehavior.AllowGet);


        }

        [HttpGet]
        public ActionResult DefineCategories()
        {
            return View(DAC.GetAllCategories());
        }

        [HttpPost]
        public ActionResult DefineCategories(Categories category)
        {
            if (category != null)
            {
                DAC.AddCategory(category);
                return Json("Category added", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("Please add Information", JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public PartialViewResult GetCategories()
        {
            return PartialView("_Categories", DAC.GetAllCategories());
        }

        [HttpDelete]
        public JsonResult DeleteItem(int? id)
        {
            DAC.DeleteItemById(id);
            return Json("Item Deleted", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Sizes()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Sizes(Sizes size = null)
        {
            if (size != null)
            {
                DAC.AddSize(size);
                return Json("Item added", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("Operation Failed because no inofrmation added.", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult GetCategorySizes(int categoryId = 1)
        {
            return PartialView("_Sizes", DAC.GetSizesByCategoryId(categoryId));
        }

        [ChildActionOnly]
        public PartialViewResult GetSizeCategories()
        {
            return PartialView("_CategoriesSize", DAC.GetAllCategories());
        }

        [HttpGet]
        public ActionResult Prices()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult GetPriceCategories()
        {
            return PartialView("_PriceCategories", DAC.GetAllCategories());
        }

        [HttpGet]
        public PartialViewResult GetCategoryPrices(int categoryId = 1)
        {
            PriceViewModel prices = new PriceViewModel();
            prices.CategoryName = DAC.GetCategoryById(categoryId).Title;
            prices.Items = DAC.GetItemsByCategoryId(categoryId);
            prices.sizes = DAC.GetSizesByCategoryId(categoryId);
            return PartialView("_CategoryPrices", prices);
        }

        [HttpGet]
        public PartialViewResult DefinePrice(int item, int category)
        {
            return PartialView("_DefinePrice");
        }

        [HttpPost]
        public JsonResult AddPrice()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdatePrices(PriceModel prices)
        {
            if (prices != null)
            {
                Item item = new Item()
                {
                    Id = prices.ItemId,
                    Price1 = prices.price1,
                    Price2 = prices.price2,
                    Price3 = prices.price3
                };
                DAC.UpdatedPrices(item);
                return Json("Updated Successfully", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("Invalid Information Entered", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateExtrasPrices(PriceModel prices)
        {
            if (prices != null)
            {
                Extras item = new Extras()
                {
                    Id = prices.ItemId,
                    Price1 = prices.price1,
                    Price2 = prices.price2,
                    Price3 = prices.price3
                };
                DAC.UpdateExtrasPrices(item);
                return Json("Updated Successfully", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("Invalid Information Entered", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Extras() {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult CategoryExtras()
        {
            var extras = DAC.GetExtrasByCategoryId(1);
            if (extras != null) {
                return PartialView("_CategoryExtras",extras);
            }
            else
            {
                return PartialView("_CategoryExtras",new List<Extras>());
            }
           
        }

        [HttpGet]
        public PartialViewResult ExtrasPrices()
        {
            List<Extras> extras = new List<Extras>();
            extras = DAC.GetExtrasByCategoryId(1);
            return PartialView("_ExtrasPrices", extras);
        }

        [HttpGet]
        public PartialViewResult CategoryExtras(int categoryId=1)
        {
            var extras = DAC.GetExtrasByCategoryId(categoryId);
            if (extras != null)
            {
                return PartialView("_CategoryExtras", extras);
            }
            else
            {
                return PartialView("_CategoryExtras", new List<Extras>());
            }
        }

        [ChildActionOnly]
        public PartialViewResult CategoriesForExtras()
        {
            return PartialView("_CategoriesForExtras", DAC.GetAllCategories());
        }
        
    }
}