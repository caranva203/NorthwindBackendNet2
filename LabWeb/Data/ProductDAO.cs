using LabWeb.Conexion;
using LabWeb.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

namespace LabWeb.Data
{
    public class ProductDAO
    {
        ConexionBD conexion;
        private NorthwindEntities db = new NorthwindEntities();

        public ProductDAO()
        {
            conexion = new ConexionBD();
        }

        // Lista de Productos con EntityFramework
        public List<Products> getProductos()
        {
            List<Products> productoBD = new List<Products>();
            List<Products> productoSerial = new List<Products>();
            productoBD = db.Products.OrderBy(a => a.ProductName).ToList();

            foreach (Products producto in productoBD)
            {
                Products serialProducto = new Products();
                serialProducto.ProductID = producto.ProductID;
                serialProducto.ProductName = producto.ProductName;
                serialProducto.UnitPrice = producto.UnitPrice;
                productoSerial.Add(serialProducto);
            }
            return productoSerial;
        }

        ////metodo que retorna la lista de productos de la BD por ADO.NET
        //public List<Products> getProductos()
        //{
        //    Products producto;
        //    List<Products> listaProductos = new List<Products>();

        //    if (conexion.Conectar() == "True")
        //    {
        //        StringBuilder builder = new StringBuilder();
        //        builder.Append(" SELECT ProductID, ProductName, UnitPrice FROM Products ");

        //        using (SqlCommand comando = new SqlCommand(builder.ToString(), conexion.getConn()))
        //        {
        //            using (IDataReader reader = comando.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    producto = new Products();
        //                    producto.ProductID = int.Parse(reader.IsDBNull(0) ? "" : reader.GetValue(0).ToString());
        //                    producto.ProductName = reader.IsDBNull(1) ? "" : reader.GetValue(1).ToString();
        //                    producto.UnitPrice = decimal.Parse(reader.IsDBNull(2) ? "" : reader.GetValue(2).ToString());
        //                    listaProductos.Add(producto);
        //                }
        //            }
        //        }
        //    }

        //    return listaProductos;
        //}
    }
}