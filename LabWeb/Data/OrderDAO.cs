using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using LabWeb.Conexion;
using LabWeb.Models;

namespace LabWeb.Data
{
    public class OrderDAO
    {
        ConexionBD conexion;
        NorthwindEntities db;

        public OrderDAO()
        {
            conexion = new ConexionBD();
            db = new NorthwindEntities();
        }

        //Consultar Pedidos
        public List<Orders> consultarPedidos()
        {
            List<Orders> listaPedidos = new List<Orders>();
            Orders pedido;

            if (conexion.Conectar() == "True")
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(" SELECT TOP(50) od.OrderID, od.OrderDate, od.RequiredDate, od.ShippedDate, od.Freight, c.CompanyName ");
                builder.Append(" FROM Orders od, Customers c WHERE estado = 0 AND od.CustomerID = c.CustomerID ORDER BY OrderID DESC");

                using (SqlCommand comando = new SqlCommand(builder.ToString(), conexion.getConn()))
                {
                    using (IDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pedido = new Orders();
                            pedido.OrderID = int.Parse(reader.IsDBNull(0) ? "" : reader.GetValue(0).ToString());
                            pedido.OrderDate = reader.IsDBNull(1) ? new DateTime() : DateTime.Parse(reader.GetValue(1).ToString());
                            pedido.RequiredDate = reader.IsDBNull(2) ? new DateTime() : DateTime.Parse(reader.GetValue(2).ToString());
                            pedido.ShippedDate = reader.IsDBNull(3) ? new DateTime() : DateTime.Parse(reader.GetValue(3).ToString());
                            pedido.Freight = decimal.Parse(reader.IsDBNull(4) ? "0" : reader.GetValue(4).ToString());
                            pedido.CustomerCompany = reader.IsDBNull(5) ? "" : reader.GetValue(5).ToString();
                            listaPedidos.Add(pedido);
                        }
                    }
                }

                foreach (Orders order in listaPedidos)
                {
                    StringBuilder builder2 = new StringBuilder();
                    builder2.Append(" SELECT od.OrderID, od.ProductID, od.UnitPrice, od.Quantity, p.ProductName FROM[Order Details] od, Products p ");
                    builder2.Append(" WHERE od.OrderID = @id AND od.ProductID = p.ProductID ");

                    using (SqlCommand comando2 = new SqlCommand(builder2.ToString(), conexion.getConn()))
                    {
                        Order_Details lineas;
                        order.Order_Details = new List<Order_Details>();
                        comando2.Parameters.AddWithValue("@id", order.OrderID);

                        using (IDataReader reader2 = comando2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                lineas = new Order_Details();
                                lineas.OrderID = int.Parse(reader2.IsDBNull(0) ? "" : reader2.GetValue(0).ToString());
                                lineas.ProductID = int.Parse(reader2.IsDBNull(1) ? "" : reader2.GetValue(1).ToString());
                                lineas.UnitPrice = decimal.Parse(reader2.IsDBNull(2) ? "": reader2.GetValue(2).ToString());
                                lineas.Quantity = short.Parse(reader2.IsDBNull(3) ? "" : reader2.GetValue(3).ToString());
                                lineas.ProductName = reader2.IsDBNull(4) ? "" : reader2.GetValue(4).ToString();
                                order.Order_Details.Add(lineas);
                            }
                        }
                    }
                }
            }

            return listaPedidos;
        }

        //Consultar pedidos por ID
        public List<Orders> obtenerPedidosPorID(int id)
        {
            List<Orders> listaPedidos = new List<Orders>();
            Orders pedido;

            if (conexion.Conectar() == "True")
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(" SELECT od.OrderID, od.OrderDate, od.RequiredDate, od.ShippedDate, od.Freight, c.CompanyName ");
                builder.Append(" FROM Orders od, Customers c WHERE OrderID LIKE '" + id + "%' AND od.CustomerID = c.CustomerID AND estado = 0 ");

                using (SqlCommand comando = new SqlCommand(builder.ToString(), conexion.getConn()))
                {
                    using (IDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pedido = new Orders();
                            pedido.OrderID = int.Parse(reader.IsDBNull(0) ? "" : reader.GetValue(0).ToString());
                            pedido.OrderDate = reader.IsDBNull(1) ? new DateTime() : DateTime.Parse(reader.GetValue(1).ToString());
                            pedido.RequiredDate = reader.IsDBNull(2) ? new DateTime() : DateTime.Parse(reader.GetValue(2).ToString());
                            pedido.ShippedDate = reader.IsDBNull(3) ? new DateTime() : DateTime.Parse(reader.GetValue(3).ToString());
                            pedido.Freight = decimal.Parse(reader.IsDBNull(4) ? "0" : reader.GetValue(4).ToString());
                            pedido.CustomerCompany = reader.IsDBNull(5) ? "" : reader.GetValue(5).ToString();
                            listaPedidos.Add(pedido);
                        }
                    }
                }

                foreach (Orders order in listaPedidos)
                {
                    StringBuilder builder2 = new StringBuilder();
                    builder2.Append(" SELECT od.OrderID, od.ProductID, od.UnitPrice, od.Quantity, p.ProductName FROM[Order Details] od, Products p ");
                    builder2.Append(" WHERE od.OrderID = @id AND od.ProductID = p.ProductID ");

                    using (SqlCommand comando2 = new SqlCommand(builder2.ToString(), conexion.getConn()))
                    {
                        Order_Details lineas;
                        order.Order_Details = new List<Order_Details>();
                        comando2.Parameters.AddWithValue("@id", order.OrderID);

                        using (IDataReader reader2 = comando2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                lineas = new Order_Details();
                                lineas.OrderID = int.Parse(reader2.IsDBNull(0) ? "" : reader2.GetValue(0).ToString());
                                lineas.ProductID = int.Parse(reader2.IsDBNull(1) ? "" : reader2.GetValue(1).ToString());
                                lineas.UnitPrice = decimal.Parse(reader2.IsDBNull(2) ? "" : reader2.GetValue(2).ToString());
                                lineas.Quantity = short.Parse(reader2.IsDBNull(3) ? "" : reader2.GetValue(3).ToString());
                                lineas.ProductName = reader2.IsDBNull(4) ? "" : reader2.GetValue(4).ToString();
                                order.Order_Details.Add(lineas);
                            }
                        }
                    }
                }
            }
            return listaPedidos;
        }

        //Insertar pedido de forma transaccional utilizando ADO.NET
        public bool insertarPedido(Orders obj)
        {
            using (SqlConnection conexion = new SqlConnection())
            {
                SqlTransaction transaction = null;
                try
                {
                    conexion.ConnectionString = "Data Source=ANDRÉS\\SQLEXPRESS;Initial Catalog=Northwind3;User ID=sa;Password=123";
                    conexion.Open();
                    transaction = conexion.BeginTransaction();

                    using (SqlCommand command = conexion.CreateCommand())
                    {
                        string format = "yyyy-MM-dd HH:mm:ss";
                        command.Connection = conexion;
                        command.Transaction = transaction;
                        command.CommandText = string.Format(
                            "INSERT INTO Orders (CustomerID, OrderDate, RequiredDate, ShippedDate, Freight, estado)" +
                            "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}' )",
                             obj.CustomerID, 
                             obj.OrderDate.ToString(format), 
                             obj.RequiredDate.ToString(format), 
                             obj.ShippedDate.ToString(format), 
                             obj.Freight.ToString().Replace(",", "."), 
                             obj.estado);

                        command.ExecuteNonQuery();
                    }

                    if (obj.Order_Details != null)
                    {
                        foreach (var o in obj.Order_Details)
                        {
                            using (SqlCommand command = conexion.CreateCommand())
                            {
                                command.Connection = conexion;
                                command.CommandText = String.Format(
                                    "INSERT INTO [Order Details] (OrderID, ProductID, UnitPrice, Quantity)" +
                                    "VALUES ((SELECT MAX(OrderID) FROM Orders), {0}, {1}, {2})",
                                    o.ProductID, o.UnitPrice.ToString().Replace(",", "."), o.Quantity);
                                command.Transaction = transaction;
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                    transaction.Commit();
                    return true;
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
            throw new NotImplementedException();
        }

        //Eliminar pedido
        public int eliminarPedido(int id)
        {
            int cod = 0;
            if (conexion.Conectar() == "True")
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(" UPDATE Orders SET estado = 1 ");
                builder.Append(" WHERE OrderID = @id ");

                using (SqlCommand comando = new SqlCommand(builder.ToString(), conexion.getConn()))
                {
                    comando.Parameters.AddWithValue("@id", id);

                    return cod = comando.ExecuteNonQuery();
                }
            }
            else
            {
                return cod;
            }
        }

        ////Insertar pedido EntityFramework
        //internal object insertarPedido(Orders order)
        //{
        //    Orders newOrder;
        //    Order_Details detalles;
        //    int cod = 0;

        //    if (order != null)
        //    {
        //        newOrder = new Orders();
        //        newOrder.CustomerID = order.CustomerID;
        //        newOrder.OrderDate = order.OrderDate;
        //        newOrder.RequiredDate = order.RequiredDate;
        //        newOrder.ShippedDate = order.ShippedDate;
        //        newOrder.Freight = order.Freight;
        //        newOrder.estado = 0;

        //        foreach (Order_Details detail in order.Order_Details)
        //        {
        //            detalles = new Order_Details();
        //            detalles.OrderID = detail.OrderID;
        //            detalles.ProductID = detail.ProductID;
        //            detalles.UnitPrice = detail.UnitPrice;
        //            detalles.Quantity = detail.Quantity;
        //            newOrder.Order_Details.Add(detalles);
        //        }

        //        db.Orders.Add(newOrder);
        //        db.SaveChanges();
        //        return cod = 1;
        //    }
        //    else
        //    {
        //        return cod;
        //    }
        //}


    }
}