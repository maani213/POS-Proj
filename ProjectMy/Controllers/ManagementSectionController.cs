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

        [HttpGet]
        public ActionResult MenuItem()
        {
            return View(new Item());
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
            List<Item> Items = DAC.GetItemsByCategoryId(categoryId);
            List<ItemViewModel> ViewItems = new List<ItemViewModel>();
            foreach (var item in Items)
            {
                ItemViewModel viewitem = new ItemViewModel();
                viewitem.BackgroundColor = item.BackgroundColor;
                viewitem.TextColor = item.TextColor;
                viewitem.TextStyle = item.TextStyle;
                viewitem.Id = item.Id;
                viewitem.Title = item.Title;
                viewitem.FontSize = item.FontSize;

                viewitem.Toppings = item.Toppings;
                if (item.IsBold)
                {
                    viewitem.fontWeight = "Bold";
                }
                else
                {
                    viewitem.fontWeight = "normal";
                }
                if (item.IsItalic)
                {
                    viewitem.fontStyle = "italic";
                }
                else
                {
                    viewitem.fontStyle = "normal";
                }

                ViewItems.Add(viewitem);
            }

            return PartialView("_MenuItems", ViewItems);
        }

        [HttpPost]
        public JsonResult AddItem(Item model)
        {
            if (model != null)
            {
                var item = DAC.AddItem(model);

                ItemViewModel viewitem = new ItemViewModel();
                viewitem.BackgroundColor = item.BackgroundColor;
                viewitem.TextColor = item.TextColor;
                viewitem.TextStyle = item.TextStyle;
                viewitem.Id = item.Id;
                viewitem.Title = item.Title;
                viewitem.Toppings = item.Toppings;
                viewitem.FontSize = item.FontSize;

                if (item.IsBold)
                {
                    viewitem.fontWeight = "Bold";
                }
                else
                {
                    viewitem.fontWeight = "normal";
                }
                if (item.IsItalic)
                {
                    viewitem.fontStyle = "italic";
                }
                else
                {
                    viewitem.fontStyle = "normal";
                }

                return Json(viewitem, JsonRequestBehavior.AllowGet);
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
        public ActionResult DefineCategories(Category category)
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

        [HttpPost]
        public JsonResult DeleteItem(int? id)
        {
            if (id != null)
            {
                DAC.DeleteItemById(id);
                return Json("Item Deleted", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("No Value Passed", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteExtra(int? id)
        {
            if (id != null)
            {
                DAC.DeleteExtraById(id);
                return Json("Item Deleted", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("No Value Passed", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteSize(int? id)
        {
            if (id != null)
            {
                DAC.DeleteSizeById(id);
                return Json("Item Deleted", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("No Value Passed", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteCategory(int? id)
        {
            if (id != null)
            {
                DAC.DeleteCategoryItemById(id);
                return Json("Item Deleted", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("No Value Passed", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteMultipleItemsItem(int[] ids)
        {
            string message = Convert.ToString(ids.Count()) + " Deleted";
            if (ids.Count() > 0)
            {
                DAC.DeleteMultipleItems(ids);
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Invalid data passed.", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult EditItem(Item Newitem)
        {
            if (Newitem != null)
            {
                Item item = DAC.UpdateItem(Newitem);

                ItemViewModel viewitem = new ItemViewModel();
                viewitem.BackgroundColor = item.BackgroundColor;
                viewitem.TextColor = item.TextColor;
                viewitem.TextStyle = item.TextStyle;
                viewitem.Id = item.Id;
                viewitem.Title = item.Title;
                viewitem.Toppings = item.Toppings;
                viewitem.FontSize = item.FontSize;

                if (item.IsBold)
                {
                    viewitem.fontWeight = "Bold";
                }
                else
                {
                    viewitem.fontWeight = "normal";
                }
                if (item.IsItalic)
                {
                    viewitem.fontStyle = "italic";
                }
                else
                {
                    viewitem.fontStyle = "normal";
                }

                return Json(viewitem, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("No Value Passed", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult EditExtra(ExtrasAndTopping extraItem)
        {
            if (extraItem != null)
            {
                DAC.UpdateExtrasItem(extraItem);
                return Json("Item Updated", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("No Value Passed", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult EditSize(Size sizeItem)
        {
            if (sizeItem != null)
            {
                DAC.UpdateSize(sizeItem);
                return Json("Item Updated", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("No Value Passed", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult EditCategory(Category category)
        {
            if (category != null)
            {
                DAC.UpdateCategory(category);
                return Json("Item Updated", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("No Value Passed", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Sizes()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Sizes(Size size = null)
        {
            if (size != null)
            {
                var Item = DAC.AddSize(size);
                return Json(Item, JsonRequestBehavior.AllowGet);
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
                ExtrasAndTopping item = new ExtrasAndTopping()
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
        public ActionResult Extras()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult CategoryExtras()
        {
            var extras = DAC.GetExtrasByCategoryId(1);
            if (extras != null)
            {
                return PartialView("_CategoryExtras", extras);
            }
            else
            {
                return PartialView("_CategoryExtras", new List<ExtrasAndTopping>());
            }

        }

        [HttpGet]
        public PartialViewResult ExtrasPrices(int cat)
        {
            List<ExtrasAndTopping> extras = new List<ExtrasAndTopping>();
            if (cat == 1)
            {
                extras = DAC.GetAllToppings();
                return PartialView("_ToppingsPrices", extras);
            }
            else
            {
                extras = DAC.GetAllExtras();
                return PartialView("_ExtrasPrices", extras);
            }

        }

        [HttpGet]
        public PartialViewResult CategoryExtras(int categoryId = 1)
        {
            var extras = DAC.GetExtrasByCategoryId(categoryId);
            if (extras != null)
            {
                return PartialView("_CategoryExtras", extras);
            }
            else
            {
                return PartialView("_CategoryExtras", new List<ExtrasAndTopping>());
            }
        }

        [ChildActionOnly]
        public PartialViewResult CategoriesForExtras()
        {
            return PartialView("_CategoriesForExtras", DAC.GetAllCategories());
        }

        [HttpPost]
        public JsonResult AddExrtaItem(ExtrasAndTopping extra)
        {
            if (extra == null)
            {
                return Json("Erro : Invalid Data Added", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var Item = DAC.AddExtra(extra);
                return Json(Item, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult CookingInsrtuctions()
        {
            return View();
        }

        public JsonResult DeleteAllItems(int CategoryId = -1)
        {
            var responce = DAC.DeleteAllItemsByCategory(CategoryId);
            if (responce)
            {
                return Json("All Items are Deleted.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error : something went wrong.", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult CookingInstructions()
        {
            return View(new CookingInstruction());
        }

        [ChildActionOnly]
        public ActionResult CookingInstrItems()
        {
            List<CookingInstruction> Items = DAC.GetAllCookInst();
            List<CookingInstrViewModel> ViewItems = new List<CookingInstrViewModel>();
            foreach (var item in Items)
            {
                CookingInstrViewModel viewitem = new CookingInstrViewModel();
                viewitem.BackgroundColor = item.BackgroundColor;
                viewitem.TextColor = item.TextColor;
                viewitem.TextStyle = item.TextStyle;
                viewitem.Id = item.Id;
                viewitem.Title = item.Title;
                viewitem.FontSize = item.FontSize;

                if (item.IsBold)
                {
                    viewitem.fontWeight = "Bold";
                }
                else
                {
                    viewitem.fontWeight = "normal";
                }
                if (item.IsItalic)
                {
                    viewitem.fontStyle = "italic";
                }
                else
                {
                    viewitem.fontStyle = "normal";
                }

                ViewItems.Add(viewitem);
            }
            return PartialView("_CookingInstrItems", ViewItems);
        }

        [HttpPost]
        public ActionResult AddInsrtruction(CookingInstruction model)
        {
            if (model != null)
            {
                var item = DAC.AddCookInstr(model);

                CookingInstrViewModel viewitem = new CookingInstrViewModel();
                viewitem.BackgroundColor = item.BackgroundColor;
                viewitem.TextColor = item.TextColor;
                viewitem.TextStyle = item.TextStyle;
                viewitem.Id = item.Id;
                viewitem.Title = item.Title;

                viewitem.FontSize = item.FontSize;

                if (item.IsBold)
                {
                    viewitem.fontWeight = "Bold";
                }
                else
                {
                    viewitem.fontWeight = "normal";
                }
                if (item.IsItalic)
                {
                    viewitem.fontStyle = "italic";
                }
                else
                {
                    viewitem.fontStyle = "normal";
                }

                return Json(viewitem, JsonRequestBehavior.AllowGet);
            }
            else
                return Json("Please add Information", JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult UpdateCookingInstr(CookingInstruction model)
        {
            if (model != null)
            {
                var item = DAC.UpdateCookingInstr(model);

                CookingInstrViewModel viewitem = new CookingInstrViewModel();
                viewitem.BackgroundColor = item.BackgroundColor;
                viewitem.TextColor = item.TextColor;
                viewitem.TextStyle = item.TextStyle;
                viewitem.Id = item.Id;
                viewitem.Title = item.Title;

                viewitem.FontSize = item.FontSize;

                if (item.IsBold)
                {
                    viewitem.fontWeight = "Bold";
                }
                else
                {
                    viewitem.fontWeight = "normal";
                }
                if (item.IsItalic)
                {
                    viewitem.fontStyle = "italic";
                }
                else
                {
                    viewitem.fontStyle = "normal";
                }

                return Json(viewitem, JsonRequestBehavior.AllowGet);
            }
            else
                return Json("Please add Information", JsonRequestBehavior.AllowGet);

        }

        public JsonResult DeleteCookingInstr(int value)
        {
            return Json(DAC.DeleteCookingInstr(value),JsonRequestBehavior.AllowGet);
        }
    }
}