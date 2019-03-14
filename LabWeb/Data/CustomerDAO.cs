﻿using LabWeb.Conexion;
using LabWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;


namespace LabWeb.Data
{
    public class CustomerDAO
    {
        ConexionBD conexion = new ConexionBD();
        private NorthwindEntities db = new NorthwindEntities();

        // Lista de Clientes con EntityFramework
        public List<Customers> consultarClientes()
        {
            List<Customers> clientesBD = new List<Customers>();
            List<Customers> clientesSerial = new List<Customers>();
            clientesBD = db.Customers.Where(x => x.b_logiv == 0).OrderBy(a => a.CompanyName).ToList();

            foreach (Customers cliente in clientesBD)
            {
                Customers serialCliente = new Customers();
                serialCliente.CustomerID = cliente.CustomerID;
                serialCliente.CompanyName = cliente.CompanyName;
                serialCliente.ContactName = cliente.ContactName;
                serialCliente.City = cliente.City;
                serialCliente.Phone = cliente.Phone;
                clientesSerial.Add(serialCliente);
            }
            return clientesSerial;
        }

        //Lista de clientes por ID
        public List<Customers> obtenerClientePorID(string id)
        {
            if (conexion.Conectar() == "True")
            {
                List<Customers> listaCustomer = new List<Customers>();
                Customers customer;


                using (SqlCommand comando = new SqlCommand("SELECT CustomerID, CompanyName, ContactName, City, Phone FROM Customers WHERE CustomerID LIKE '%" + id + "%' AND b_logiv = 0", conexion.getConn()))
                {
                    using (IDataReader reader = comando.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            customer = new Customers();
                            customer.CustomerID = reader.IsDBNull(0) ? "" : reader.GetValue(0).ToString();
                            customer.CompanyName = reader.IsDBNull(1) ? "" : reader.GetValue(1).ToString();
                            customer.ContactName = reader.IsDBNull(2) ? "" : reader.GetValue(2).ToString();
                            customer.City = reader.IsDBNull(3) ? "" : reader.GetValue(3).ToString();
                            customer.Phone = reader.IsDBNull(4) ? "" : reader.GetValue(4).ToString();
                            listaCustomer.Add(customer);
                        }
                    }
                }

                return listaCustomer;
            }
            else
            {
                return null;
            }


        }

        public int eliminarCliente(string id)
        {
            int cod = 0;


            if (conexion.Conectar() == "True")
            {
                using (SqlCommand comando = new SqlCommand("UPDATE Customers SET b_logiv = 1 WHERE CustomerID = '" + id + "'", conexion.getConn()))
                {
                    cod = comando.ExecuteNonQuery();
                }
                return cod;
            }
            else
            {
                return cod;
            }
        }
        ///Lista de clientes con ADO.NET
        //public List<Customers> consultarClientes()
        //{
        //    if (conexion.Conectar() == "True")
        //    {
        //        List<Customers> listaCustomer = new List<Customers>();
        //        Customers customer;


        //        using (SqlCommand comando = new SqlCommand("SELECT CustomerID, CompanyName, ContactName, City, Phone FROM Customers WHERE b_logiv = 0 ORDER BY CustomerID DESC", conexion.getConn()))
        //        {
        //            using (IDataReader reader = comando.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    customer = new Customers();
        //                    customer.CustomerID = reader.IsDBNull(0) ? "" : reader.GetValue(0).ToString();
        //                    customer.CompanyName = reader.IsDBNull(1) ? "" : reader.GetValue(1).ToString();
        //                    customer.ContactName = reader.IsDBNull(2) ? "" : reader.GetValue(2).ToString();
        //                    customer.City = reader.IsDBNull(3) ? "" : reader.GetValue(3).ToString();
        //                    customer.Phone = reader.IsDBNull(4) ? "" : reader.GetValue(4).ToString();
        //                    listaCustomer.Add(customer);
        //                }
        //            }
        //        }

        //        return listaCustomer;
        //    }
        //    else
        //    {
        //        string ex = conexion.Conectar();
        //        return null;
        //    }
        //}

    }
}