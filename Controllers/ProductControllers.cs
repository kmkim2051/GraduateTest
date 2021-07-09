using GradTest2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace GradTest2.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        private IWebHostEnvironment webHostEnvironment;

        public ProductController(IWebHostEnvironment _webHostEnvironment)
        {
            webHostEnvironment = _webHostEnvironment;
        }

        [Route("")]
        [Route("index")]
        [Route("~/")]
        public IActionResult Index(Product product, IFormFile excel)
        {
            return View("Index", new Product());
        }

        [Route("save")]
        [HttpPost]
        public IActionResult Save(Product product, IFormFile excel)
        {
            if (excel == null || excel.Length == 0)
            {
                return Content("File not selected");

            }
            var path = Path.Combine(this.webHostEnvironment.WebRootPath, "files", excel.FileName);
            var stream = new FileStream(path, FileMode.Create);
            excel.CopyToAsync(stream);
            product.Excel = excel.FileName;
            
            ViewBag.product = product;
            return Ok(new { name = product.Excel });
            //return View("Success");
        }

    }
}