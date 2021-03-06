﻿using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetProduct(int idp);
        List<Product> Search(string keyWord);
        int GenerateProductId();
        string CreateProduct(Product product);
        string UpdateProduct(Product product);
    }
}
