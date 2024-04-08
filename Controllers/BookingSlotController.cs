using AppointmentForm.DAL;
using AppointmentForm.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AppointmentForm.Controllers
{
    public class BookingSlotController : Controller
    {
        Booking_Dal dal = new Booking_Dal();
        // GET: BookingSlot
        public ActionResult Index()
        {
            try
            {

                var list = dal.GetList();

                if (list.Count == 0)
                {
                    TempData["InfoMsg"] = "No Records Available";
                }
                ViewBag.DrList = dal.GetDoctorList();
                //ViewBag.drpSlotTime = dal.GetSlotTime() ;

                return View(list);
            }
            catch (Exception ex)
            {

                TempData["ErrorMsg"] = ex.Message;
                ViewBag.DrList = dal.GetDoctorList();

                return View();
            }

        }

        // GET: BookingSlot/Create
        public ActionResult Create()
        {
            try
            {

                ViewBag.DrList = dal.GetDoctorList();
                //ViewBag.drpSlotTime = dal.GetSlotTime();

            }
            catch (Exception ex)
            {

                TempData["ErrorMsg"] = ex.Message;
                ViewBag.DrList = dal.GetDoctorList();

                return View();
            }

            return View();

        }
        public List<string> GetDrpList()
        {
            var doctors = new List<string>() { "Patil","Laad"};
            return doctors;
        }
        public ActionResult GetSlotTime(int id)
        {
            ViewBag.drpSlotTime = dal.GetSlotTime(id);
            return PartialView("DisplaySlots");
        }

        // POST: BookingSlot/Create
        [HttpPost]
        public ActionResult Create(Booking bk)
        {
            DataSet status = new DataSet();
            try
            {

                if (ModelState.IsValid)
                {
                     status = dal.InsertData(bk) ;

                    if (status.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        TempData["SuccessMsg"] = "Data Inserted Successfully";
                    }
                    else 
                    {
                        TempData["ErrorMsg"] = "Duplicate Record";
                        ViewBag.DrList = dal.GetDoctorList();

                        return View();

                    }
                }
                else 
                {
                        TempData["ErrorMsg"] = "Contact Adminstrator";

                }


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.Message;
                ViewBag.DrList = dal.GetDoctorList();

                return View();
            }
        }

        // GET: BookingSlot/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var list = dal.GetListById(id).FirstOrDefault();
                if (list == null)
                {
                    TempData["InfoMsg"] = "No Records Available With Id : " +id;
                    return RedirectToAction("Index");
                }
                return View(list);

            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.Message;
                ViewBag.DrList = dal.GetDoctorList();

                return View();
            }
        }

        // POST: BookingSlot/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Booking bk)
        {
            try
            {
                DataSet ds = dal.DeleteById(id);
                if (ds.Tables[0].Rows[0][0].ToString().Contains("Success") ) 
                {
                    TempData["SuccessMsg"] = "Data Deleted Successfully";

                }

                return RedirectToAction("Index");
            }
            catch(Exception ex) 
            {
                TempData["ErrorMsg"] = ex.Message;
                ViewBag.DrList = dal.GetDoctorList();

                return View();
            }
        }
    }
}
