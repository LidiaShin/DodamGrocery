using DodamGroceryMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DodamGroceryMVC.Controllers
{
    public class GroceryItemController : Controller
    {
        // GET: GroceryItem
        public ActionResult Index()
        {
            IEnumerable<mvcGroceryItemModel> itemList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("GroceryItem").Result;
            itemList = response.Content.ReadAsAsync<IEnumerable<mvcGroceryItemModel>>().Result;
            return View(itemList);
        }

      

       




        public ActionResult AddOrEdit(int id=0)
        {
            if(id==0)
            return View(new mvcGroceryItemModel());

            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("GroceryItem/"+id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcGroceryItemModel>().Result);
            }

        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcGroceryItemModel newItem)
        {
            if (newItem.ID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("GroceryItem", newItem).Result;
                TempData["SuccessMessage"] = "New item is saved successfully! ";
            }

            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("GroceryItem/"+newItem.ID,newItem).Result;
                TempData["SuccessMessage"] = "Item is updated successfully! ";
            }

         
            return RedirectToAction("Index");
        }



    



        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("GroceryItem/" +id.ToString()).Result;
            TempData["SuccessMessage"] = "Selected item record is deleted successfully! ";
            return RedirectToAction("Index");
        }
    }
}