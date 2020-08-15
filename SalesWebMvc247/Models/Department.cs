﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc247.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> sellers { get; set; } = new List<Seller>();

        public Department()
        {
        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }


        public void AddSeller(Seller seller)
        {
            sellers.Add(seller);
        }

       

        public double TotalSales(DateTime initial, DateTime final)
        {
                               //para cada vendedor na minha lista
            return sellers.Sum(Seller => Seller.TotalSales(initial, final));
        }
    }
}
