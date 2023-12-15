using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using System.Web.UI.WebControls;


namespace WebApplication2.Models
{


    public class cart_item
    {
        public Product products { get; set; }
        public decimal? quanlity { get; set; }
       
    }

    public class Cart
    {
        List<cart_item> items = new List<cart_item>();
        public List<cart_item> Items
        {
            get { return items; }
        }



        public void Add(Product product, int ql = 1)
        {
            var item = items.FirstOrDefault(s => s.products.ID_product == product.ID_product);
            if (item == null)
            {
                items.Add(item = new cart_item
                {
                    products = product,
                    quanlity = ql
                });
            }
            else { item.quanlity += ql; }

        }
        public void Update_Quanlity_Shopping(int id, int quantity)
        {
            var item = items.Find(s => s.products.ID_product == id);
            if (item != null)
            {
                item.quanlity = quantity;
            }
        }

        public decimal? Total_money()
        {

            var money = items.Sum(s => s.quanlity * s.products.Price);
            
            return (decimal)money;
        }
        public void clearCart()
        {
            items.Clear();

        }

        //public List<CartView> lala()

        //{
        //    EdithTourEntities db = new EdithTourEntities();
        //    var model = (from a in db.Tour_Inside
        //                 join b in db.Tickets on a.ID_tour_inside equals b.ID_tour_inside
        //                 where a.ID_tour_inside < 100
        //                 select new CartView()
        //                 {
        //                     Id = a.ID_tour_inside,
        //                     Name = a.Name,
        //                     Description = a.Description,
        //                     Image = a.Image,
        //                     Name_Ticket = b.Name,
        //                     Price = b.Price,
        //                     amount = b.Numberof_ticket,
        //                 }).ToList();
        //    return model;
        //}
        //public List<CartView> lalo()

        //{
        //    EdithTourEntities db = new EdithTourEntities();

        //    var model = (from a in db.Tour_Outside
        //                 join b in db.Tickets on a.ID_tour_outside equals b.ID_tour_inside
        //                 where a.ID_tour_outside < 100
        //                 select new CartView()
        //                 {
        //                     Id = a.ID_tour_outside,
        //                     Name = a.Name,
        //                     Description = a.Description,
        //                     Image = a.Image,
        //                     Name_Ticket = b.Name,
        //                     Price = b.Price,
        //                     amount = b.Numberof_ticket,
        //                 }).ToList();
        //    return model;
        //}
    }


    //public List<Ticket> select_price(string name)
    //{
    //    var model = db.Ticket.Where(s=>s.Name==name);
    //   if(model != null)
    //    {
    //        decimal? price = model.FirstOrDefault().Price;
    //    }
    //    return select_price(name);
    //}



}



