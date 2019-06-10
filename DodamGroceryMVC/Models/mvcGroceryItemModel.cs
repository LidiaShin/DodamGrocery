using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DodamGroceryMVC.Models
{
    public class mvcGroceryItemModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Please enter item type")]
        public string ItemType { get; set; }

        public string ItemName { get; set; }
        public Nullable<decimal> ItemPurchasedPrice { get; set; }
        public Nullable<decimal> ItemRetailPrice { get; set; }
        public Nullable<int> ItemQuantity { get; set; }
     
    }
}