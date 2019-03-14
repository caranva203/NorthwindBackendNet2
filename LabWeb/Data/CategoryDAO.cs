using LabWeb.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace LabWeb.Data
{
    public class CategoryDAO
    {

        private NorthwindEntities db = new NorthwindEntities();

        //Lista de categorias con sus productos
            public  List<Categories> getCategorias()
            {
                List<Categories> categoriaBD = new List<Categories>();
                List<Categories> categoriaSerial = new List<Categories>();
                categoriaBD = db.Categories.OrderBy(a => a.CategoryName).ToList();

            foreach (var obj in categoriaBD)
            {
                Categories objdto = new Categories();
                objdto.CategoryID = obj.CategoryID;
                objdto.CategoryName = obj.CategoryName;
                objdto.Description = obj.Description;

                objdto.Products = new Collection<Products>();
                foreach (var o in obj.Products)
                {
                    objdto.Products.Add(
                        new Products()
                        {
                            ProductID = o.ProductID,
                            ProductName = o.ProductName,
                            CategoryID = o.CategoryID,
                            SupplierID = o.SupplierID,
                            UnitPrice = o.UnitPrice
                        });
                }
                categoriaSerial.Add(objdto);
            }
            return categoriaSerial;
        }
    }
}