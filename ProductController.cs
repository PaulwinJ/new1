using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using crud.DAL;
using crud.Models;
using System.IO;
using OfficeOpenXml;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;


namespace crud.Controllers
{
    public class ProductController : Controller
    {
       
        Product_DAL _productDAL = new Product_DAL();
        // GET: Product
        public ActionResult Index()
        {
            var productList = _productDAL.GetAllProducts();
            if (productList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently products not available in the Database.";
            }
            return View(productList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var product = _productDAL.GetProductByID(id);

                if (product == null)
                {
                    TempData["ErrorMessage"] = "Product details not available with the product Id: " + id;
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }


        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                int id = 0;

                if (ModelState.IsValid)
                {
                    id = _productDAL.InsertProducts(product);

                    if (id > 0)
                    {
                        TempData["SuccessMessage"] = "Product details saved successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to insert the product.";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = _productDAL.GetProductByID(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _productDAL.UpdateProducts(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Product/Delete/5
public ActionResult Delete(int id)
{
    try
    {
        var product = _productDAL.GetProductByID(id);

        if (product == null)
        {
            TempData["ErrorMessage"] = "Product details not available with the product Id: " + id;
            return RedirectToAction("Index");
        }
        return View(product);
    }
    catch (Exception ex)
    {
        TempData["ErrorMessage"] = ex.Message;
        return RedirectToAction("Index");
    }
}


        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                int rowsAffected = _productDAL.DeleteProducts(id);

                if (rowsAffected > 0)
                {
                    TempData["SuccessMessage"] = "Product deleted successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Unable to delete the product.";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult ExportToExcel()
        {
            var productList = _productDAL.GetAllProducts();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; 
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("Products");
                worksheet.Cells["A1"].LoadFromCollection(productList, true);

                byte[] fileContents = excelPackage.GetAsByteArray();
                return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Products.xlsx");
            }
        }

       
    }
}
