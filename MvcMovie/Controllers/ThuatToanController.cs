using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models;
using System.Threading;
using System.Diagnostics;

namespace MvcMovie.Controllers
{
    public class ThuatToanController : Controller
    {

        #region VIEW
        // GET: ThuatToan --> 5 PP
        public ActionResult Index()
        {            
            ViewBag.xuat = "Xin chao";
            return View();
        }

        // GET: SelectionSort
        public ActionResult SelectionSort()
        {            
            return View();
        }

        // GET: InsertSort
        public ActionResult InsertSort()
        {           
            return View();
        }

        // GET: BubbleSort
        public ActionResult BubbleSort()
        {
            return View();
        }

        // GET: MergeSort
        public ActionResult MergeSort()
        {
            return View();
        }

        // GET: QuickSort
        public ActionResult QuickSort()
        {
            return View();
        }

        // GET: KhuDQQS
        public ActionResult KhuDQQS()
        {
            return View();
        }

        //        
        public ActionResult GetResults()
        {
            
            string type = Request.QueryString["sxt"];
            string pp = Request.QueryString["pp"];

            //ViewBag.PhuongPhap = Request.QueryString["pp"];
            if(pp.ToLower()=="shs")
                ViewBag.SapXepTheo = "Số hồ sơ";
            if (pp.ToLower() == "stt")
                ViewBag.SapXepTheo = "Số thứ tự";
            if (pp.ToLower() == "mssv")
                ViewBag.SapXepTheo = "Mã số sinh viên";
            if (pp.ToLower() == "ngaysinh")
                ViewBag.SapXepTheo = "Ngày Sinh";
            if (pp.ToLower() == "hoten")
                ViewBag.SapXepTheo = "Họ Tên";

            var data = GetData();
            ThuatToan tt = new ThuatToan();
            if (pp == "SelectionSort")
            {
                ViewBag.PhuongPhap = "Chọn Trực Tiếp - Selection Sort";
                tt.SelectionSort(data, type);
            }
            if (pp == "InsertSort")
            {
                ViewBag.PhuongPhap = "Chèn Trực Tiếp - Insert Sort";
                tt.InsertionSort(data, type);
            }
            if (pp == "BubbleSort")
            {
                ViewBag.PhuongPhap = "Nổi bọt - Bubble Sort";
                tt.BubbleSort(data, type);
            }
            if (pp == "MergeSort")
            {
                ViewBag.PhuongPhap = "Trộn Trực Tiếp - Merge Sort";
                tt.MergeSort(data, type);
            }
            if (pp == "QuickSort")
            {
                ViewBag.PhuongPhap = "PP Nhanh - Selection Sort";
                tt.QuickSort(data, type);
            }
            var model = data.ToList();
            return View(model);
        }
        #endregion

        #region ACTION
        [HttpPost]
        public ActionResult AjaxSelectionSort(string type)
        {
            var res = Json(new { status = 0, sErr = "" });
            try
            {
                //TODO - 
                string sTime = "";
                int totRec = 0;
                fncSelectionSort(type , out sTime, out  totRec);
                res = Json(new { status = 1, totalTimeStr = sTime, sErr = "", totalRecord = totRec.ToString("0,0") });
            }
            catch (Exception ex)
            {
                res = Json(new { status = 0, sErr = "Lỗi xảy ra ! " + ex.Message });
            }
            return res;
        }


        [HttpPost]
        public ActionResult AjaxInsertSort(string type)
        {
            var res = Json(new { status = 0, sErr = "" });
            try
            {
                //TODO - 
                string sTime = "";
                int totRec = 0;
                fncInsertSort(type, out sTime, out totRec);
                res = Json(new { status = 1, totalTimeStr = sTime, sErr = "", totalRecord = totRec.ToString("0,0") });
            }
            catch (Exception ex)
            {
                res = Json(new { status = 0, sErr = "Lỗi xảy ra ! " + ex.Message });
            }
            return res;
        }

        [HttpPost]
        public ActionResult AjaxBubbleSort(string type)
        {
            var res = Json(new { status = 0, sErr = "" });
            try
            {
                //TODO - 
                string sTime = "";
                int totRec = 0;
                fncBubbleSort(type, out sTime, out totRec);
                res = Json(new { status = 1, totalTimeStr = sTime, sErr = "", totalRecord = totRec.ToString("0,0") });
            }
            catch (Exception ex)
            {
                res = Json(new { status = 0, sErr = "Lỗi xảy ra ! " + ex.Message });
            }
            return res;
        }

        [HttpPost]
        public ActionResult AjaxMergeSort(string type)
        {
            var res = Json(new { status = 0, sErr = "" });
            try
            {
                //TODO - 
                string sTime = "";
                int totRec = 0;
                fncMergeSort(type, out sTime, out totRec);
                res = Json(new { status = 1, totalTimeStr = sTime, sErr = "", totalRecord = totRec.ToString("0,0") });
            }
            catch (Exception ex)
            {
                res = Json(new { status = 0, sErr = "Lỗi xảy ra ! " + ex.Message });
            }
            return res;
        }

        [HttpPost]
        public ActionResult AjaxQuickSort(string type)
        {
            var res = Json(new { status = 0, sErr = "" });
            try
            {
                //TODO - 
                string sTime = "";
                int totRec = 0;
                fncQuickSort(type, out sTime, out totRec);
                res = Json(new { status = 1, totalTimeStr = sTime, sErr = "", totalRecord = totRec.ToString("0,0") });
            }
            catch (Exception ex)
            {
                res = Json(new { status = 0, sErr = "Lỗi xảy ra ! " + ex.Message });
            }
            return res;
        }
        #endregion

        #region PRIVATE FUNCTION
        private SinhVien[] GetData()
        {            
            List<SinhVien> lst = new List<SinhVien>();
            using (var db = new Models.TestSortEntities())
            {
                var ls = db.SVs.ToList();
                foreach(var item in ls)
                {
                    SinhVien sv = new SinhVien();
                    sv.HoTen = item.HoTen;
                    sv.MSSV = item.SoBaoDanh;
                    if (item.NgaySinh.HasValue) sv.NgaySinh = item.NgaySinh.Value;
                    if (item.STT.HasValue) sv.STT = item.STT.Value;
                    if (item.SHS.HasValue) sv.SHS = item.SHS.Value;
                    lst.Add(sv);
                }
            }
            return lst.ToArray();
        }

        private void fncSelectionSort(string type, out string  stime, out int totRec)
        {
            stime = "";
            totRec = 0;
            //List<SinhVien> lst = new List<SinhVien>();
            ThuatToan tt = new ThuatToan();
            var data = GetData();
            totRec = data.Count();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            tt.SelectionSort(data, type);
           
            //Thread.Sleep(10000);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            stime = elapsedTime;
            //return lst;
        }

        private void fncInsertSort(string type, out string stime, out int totRec)
        {
            stime = "";
            totRec = 0;
            //List<SinhVien> lst = new List<SinhVien>();
            ThuatToan tt = new ThuatToan();
            var data = GetData();
            totRec = data.Count();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            tt.InsertionSort(data, type);

            //Thread.Sleep(10000);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            stime = elapsedTime;
            //return lst;
        }

        private void fncBubbleSort(string type, out string stime, out int totRec)
        {
            stime = "";
            totRec = 0;
            //List<SinhVien> lst = new List<SinhVien>();
            ThuatToan tt = new ThuatToan();
            var data = GetData();
            totRec = data.Count();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            tt.BubbleSort(data, type);

            //Thread.Sleep(10000);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            stime = elapsedTime;
            //return lst;
        }

        private void fncMergeSort(string type, out string stime, out int totRec)
        {
            stime = "";
            totRec = 0;
            //List<SinhVien> lst = new List<SinhVien>();
            ThuatToan tt = new ThuatToan();
            var data = GetData();
            totRec = data.Count();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            tt.MergeSort(data, type);

            //Thread.Sleep(10000);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            stime = elapsedTime;
            //return lst;
        }

        private void fncQuickSort(string type, out string stime, out int totRec)
        {
            stime = "";
            totRec = 0;
            //List<SinhVien> lst = new List<SinhVien>();
            ThuatToan tt = new ThuatToan();
            var data = GetData();
            totRec = data.Count();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            tt.QuickSort(data, type);

            //Thread.Sleep(10000);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            stime = elapsedTime;
            //return lst;
        }

        #endregion
    }
}