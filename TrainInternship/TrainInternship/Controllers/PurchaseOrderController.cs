using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainInternship.Models;
using TrainInternship.Context;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;

namespace TrainInternship.Controllers
{
    public class PurchaseOrderController : Controller
    {
        ShopContext _myContext = new ShopContext();
        // Index
        public ActionResult Index()
        {
            return View(_myContext.PurchaseOrder.ToList());
        }
        // Details
        [HttpGet("PurchaseOrder/Details/{id}")]
        public ActionResult Details(int id)
        {
            ViewData["PO"] = _myContext.PurchaseOrder.Find(id);
            return View(_myContext.PurchaseOrderLine.ToList());
        }
        // Resend mail page
        public ActionResult ResendMail()
        {
            return View();
        }
        // Cancel po
        [HttpPost]
        public JsonResult CancelPO(int orderNo)
        {
            PurchaseOrder poEntity = _myContext.PurchaseOrder.Find(orderNo);
            // check poEntity
            if (poEntity != null)
            {
                if(poEntity.Status == false)
                    return Json("Purchase order has been canceled!");
                poEntity.Status = false;
                _myContext.SaveChanges();
                return Json("Success");
            }
            else
                return Json("Something went wrong!");
        }
        // get po status
        [HttpGet]    
        public JsonResult GetPOStatus(int orderNo)
        {
            PurchaseOrder poEntity = _myContext.PurchaseOrder.Find(orderNo);
            if (poEntity != null)
            {
                return Json(new {poStatus = poEntity.Status});
            }
            else
                return Json("Something went wrong!");
        }
        //create po line
        [HttpPost]
        public JsonResult AddPOLine(PurchaseOrderLine pol)
        {
            if (pol != null)
            {
                _myContext.Add(pol);
                _myContext.SaveChanges();
                return Json("Success");
            }
            else
                return Json("Something went wrong!");
        }
        //delete po line
        [HttpDelete]
        public JsonResult DeletePOLine(int orderNo, int partNo)
        {
            int checkGreaterThan1 = _myContext.PurchaseOrderLine.Where(n => n.OrderNo == orderNo).Count();
            if (checkGreaterThan1 > 1)
            {
                PurchaseOrderLine polEntity = _myContext.PurchaseOrderLine.Where(n => n.OrderNo == orderNo && n.PartNo == partNo).FirstOrDefault();
                if (polEntity != null)
                {
                    _myContext.Remove(polEntity);
                    _myContext.SaveChanges();
                    return Json("Success");
                }
                else
                {
                    return Json("Something went wrong, can not find the line to delete!");
                }
            }
            else
                return Json("You must have at least 1 po line in purchase order!");
        }
        //update po 
        [HttpPost]
        public JsonResult UpdatePO(PurchaseOrder poEntity, List<PurchaseOrderLine> polList)
        {
            //check po and pol list
            if (poEntity != null && polList != null)
            {
                PurchaseOrder po = _myContext.PurchaseOrder.Find(poEntity.OrderNo);
                po.Note = poEntity.Note.Trim();
                po.Address = poEntity.Address.Trim();
                po.County = poEntity.County.Trim();
                po.PostCode = poEntity.PostCode.Trim();
                //
                foreach (var item in polList)
                {
                    PurchaseOrderLine pol = _myContext.PurchaseOrderLine.Find(item.PartNo);
                    pol.QuantityOrder = item.QuantityOrder;
                    pol.BuyPrice = item.BuyPrice;
                    pol.MeMo = item.MeMo.Trim();
                    _myContext.Entry(pol).State = EntityState.Modified;
                }
                _myContext.SaveChanges();
                return Json("Success");
            }
            else
                return Json("Something went wrong!");
        } 
    }
}
