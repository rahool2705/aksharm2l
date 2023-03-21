using Business.Entities.OurProduct;
using Business.Entities.ProductPhotoPath;
using Business.Interface;
using Business.Interface.ProductImages;
using ERP.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
//using PartialViewResult = System.Web.Mvc.PartialViewResult;
//using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using PartialViewResult = Microsoft.AspNetCore.Mvc.PartialViewResult;
using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using Business.Interface.IOurProduct;
using System.Threading.Tasks;
using ERP.Helpers;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using System.Data;
using System.Linq;

namespace ERP.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    [DisplayName("OurProduct")]
    public class OurProductController : SettingsController
    {
        private readonly IMasterService _masterService;
        private readonly IProductImages _productImages;
        private readonly IOurProductService _ourProductService;

        public OurProductController(IMasterService masterService, IProductImages productImages, IOurProductService ourProductService)
        {
            _masterService = masterService;
            _productImages = productImages;
            _ourProductService = ourProductService;
        }
        public IActionResult Index()
        {
            List<ProductPhotoPath> imagePath = _productImages.GetImagePath().Result;
            return View(imagePath);
        }

        [HttpGet]
        public IActionResult Get(int id, string collectionId)/*, string itemName*/
        {
            try
            {
                OurProduct model = new OurProduct();
                // model.ItemText= itemName;
                model.Item = _productImages.GetProductImageDetailAsync(id).Result.ProductImageText.Replace(".jpeg", "").Replace(".jpg", "").Replace(".png", "");
                model.ItemCollectionID = collectionId;
                var listUOMID = _masterService.GetAllUOMID();
                ViewData["UOMID"] = new SelectList(listUOMID, "UOMID", "UOMText");

                if (id > 0)
                {
                    //model = _iMarketingCompanyFinancialYear.GetFinancialYearAsync(id).Result;

                    return PartialView("AddOurProductImages", model);
                }
                else
                {
                    return PartialView("AddOurProductImages", model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddOurProduct(OurProduct ourProduct)
        {
            try
            {
                ourProduct.CreatedOrModifiedBy = USERID;

                //Set value in Session object.
                //HttpContext.Session.SetString("ItemCollectionID", "Dravesh");
                //string test = HttpContext.Session.GetString("ItemCollectionID");
                //Get SessionID inside Controller.
                TempData["SessionID"] = HttpContext.Session.Id;
                //var test = TempData["ItemCollectionID"];
                if (string.IsNullOrEmpty(ourProduct.ItemCollectionID))
                {
                    Guid obj = Guid.NewGuid();
                    ourProduct.ItemCollectionID = Regex.Replace(obj.ToString(), "[^0-9.]", "");
                    HttpContext.Session.SetString("ItemCollectionID", ourProduct.ItemCollectionID);
                    TempData["ItemCollectionID"] = ourProduct.ItemCollectionID;
                }

                /*Random random = new Random();
                ourProduct.CreatedOrModifiedBy = USERID;
                ourProduct.ItemCollectionID = random.Next(999999999);*/

                string ItemForRFQID = await _ourProductService.AddOurProduct(ourProduct);

                {
                    if (!string.IsNullOrEmpty(ItemForRFQID))
                    {
                        return Json(new { status = true, message = MessageHelper.Added });
                    }
                    else
                        return Json(new { status = false, message = MessageHelper.Error });
                    //return RedirectToAction("Index", "OurProduct");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }

        [HttpGet]
        public IActionResult GoForRFQCard(string CollectionID)
        {
            GoForRFQCard goForRFQCard = new GoForRFQCard();
            goForRFQCard.CustomerInquiryDate= DateTime.Today;
            goForRFQCard.InquiryDate= DateTime.Today;

            var FinancialYearList = _masterService.GetAllFinancialYearMasterAsync();
            ViewData["FinYearText"] = new SelectList(FinancialYearList, "FinancialYearID", "FinancialYear");

            var InquiryTypeList = _masterService.GetAllInquiryTypeMasterAsync();
            ViewData["InquiryType"] = new SelectList(InquiryTypeList, "InquiryTypeID", "InquiryTypeText");

            goForRFQCard.Mediator = DISPLAYUSERNAME;

            goForRFQCard.LstGoForRFQCardTable = _ourProductService.GetSessionGoForRFQCardTable(CollectionID).Result;

            return View(goForRFQCard);
        }

        [HttpPost]
        public async Task<ActionResult> GoForRFQItemInactive(int itemForRFQID)
        {
            GoForRFQCardTable goForRFQCard = new GoForRFQCardTable()
            {
                ItemForRFQID = itemForRFQID,
                CreatedOrModifiedBy = USERID
            };
            int GoForRFQCardInActive = await _ourProductService.GoForRFQItemInactive(goForRFQCard);
            if (GoForRFQCardInActive > 0)
            {
                return Json(new { status = true, message = MessageHelper.InactivatedRFQ });
            }
            else
                return Json(new { status = true, message = MessageHelper.Error });
        }

        [HttpPost]
        public IActionResult IGoForRFQCardItem(GoForRFQCard goForRFQCard)
        {
            goForRFQCard.CreatedOrModifiedBy = USERID;
            int inquiryMasterId = _ourProductService.IGoForRFQCardItem(goForRFQCard).Result;
            if (inquiryMasterId > 0)
            {
                return Json(inquiryMasterId);
            }
            /* DataTable shiftTimeDataTable = new DataTable("Beta_RequirementShiftTimingsType");
         //we create column names as per the type in DB 
         shiftTimeDataTable.Columns.Add("RequirementShiftTimingID", typeof(int));
         shiftTimeDataTable.Columns.Add("RequirementShiftTimingFrom", typeof(string));
         shiftTimeDataTable.Columns.Add("RequirementShiftTimingTo", typeof(string));
         shiftTimeDataTable.Columns.Add("RequirementHoursPerWeek", typeof(decimal));
         //and fill in some values 
         if (item.ShiftTimings != null)
         {

             foreach (var shift in item.ShiftTimings)
             {
                 shiftTimeDataTable.Rows.Add(shift.RequirementShiftTimingID
                                 , shift.RequirementShiftTimingFrom
                                 , shift.RequirementShiftTimingTo
                                 , shift.RequirementHoursPerWeek);
             }
         }*/
            return PartialView("Index", goForRFQCard);
        }


        [HttpPost]
        public IActionResult IGoForRFQCardItemList(List<GoForRFQCardTable> goForRFQCardTables, int inquiryMasterId)
        {
            if (goForRFQCardTables.Count() > 0)
            {
                foreach (var item in goForRFQCardTables)
                {
                    item.InquiryID = inquiryMasterId;
                    item.CreatedOrModifiedBy = USERID;
                }
            }

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("InquiryItemID", typeof(int)));
            dataTable.Columns.Add(new DataColumn("InquiryID", typeof(int)));
            dataTable.Columns.Add(new DataColumn("ItemID", typeof(int)));
            dataTable.Columns.Add(new DataColumn("Qty", typeof(decimal)));
            dataTable.Columns.Add(new DataColumn("Remark", typeof(string)));
            dataTable.Columns.Add(new DataColumn("IsInspectionRequired", typeof(bool)));
            dataTable.Columns.Add(new DataColumn("InspectionAgencyID", typeof(int)));
            dataTable.Columns.Add(new DataColumn("Item", typeof(string)));
            dataTable.Columns.Add(new DataColumn("UOMID", typeof(int)));

            foreach (var item in goForRFQCardTables)
            {
                dataTable.Rows.Add(new object[]
                {
                    item.InquiryItemID = 0,
                    item.InquiryID,
                    item.ItemForRFQID,
                    item.Quantity,
                    item.ItemDescription,
                    item.IsInspectionRequired,
                    item.InspectionAgencyID,
                    item.Item,
                    item.UOMID,
                });
            }

            var list = _ourProductService.IGoForRFQCardItemList(dataTable);

            if (list != null)
            {
                //HttpContext.Session.abo("Name");
                //var test1 = HttpContext.Session.Id;
                HttpContext.Session.SetString("ItemCollectionID", "");
                return Json(new { status = true, message = MessageHelper.QuotationRegistered });
            }

            else
                return Json(new { status = false, message = MessageHelper.Error });

            #region
            //DataTable shiftTimeDataTable = new DataTable("Beta_RequirementShiftTimingsType");
            //we create column names as per the type in DB 
            //shiftTimeDataTable.Columns.Add("RequirementShiftTimingID", typeof(int));
            //shiftTimeDataTable.Columns.Add("RequirementShiftTimingFrom", typeof(string));
            //shiftTimeDataTable.Columns.Add("RequirementShiftTimingTo", typeof(string));
            //shiftTimeDataTable.Columns.Add("RequirementHoursPerWeek", typeof(decimal));
            //and fill in some values 
            //if (item.ShiftTimings != null)
            //{
            //    foreach (var shift in item.ShiftTimings)
            //    {
            //        shiftTimeDataTable.Rows.Add(shift.RequirementShiftTimingID
            //                        , shift.RequirementShiftTimingFrom
            //                        , shift.RequirementShiftTimingTo
            //                        , shift.RequirementHoursPerWeek);
            //    }
            //}
            #endregion

            //return PartialView("Index", goForRFQCardTables);
        }
    }
}
