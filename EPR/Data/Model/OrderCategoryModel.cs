using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.Data.Model
{
    public class OrderCategoryModel
    {
        public String name { get; set; }
        public int position { get; set; }

        public List<OrderDateModel> OrderDateModelList { get; set; }

        public OrderCategoryModel(String name)
        {
            this.name = name;
            this.position = 1;

            OrderDateModelList = new List<OrderDateModel>();
        }

    }

}