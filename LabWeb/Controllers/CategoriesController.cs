using LabWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LabWeb.Controllers
{
    public class CategoriesController : ApiController
    {
        CategoryDAO categoriaDAO;

        public CategoriesController()
        {
            categoriaDAO = new CategoryDAO();
        }

        // GET: api/Product/5
        [HttpGet]
        public IHttpActionResult getProducts()
        {
            return Ok(categoriaDAO.getCategorias());
        }
       
    }
}
