using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Asp.Net_MvcWeb_Pj3.Aptech.Models;
using PagedList;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Asp.Net_MvcWeb_Pj3.Aptech.Controllers
{
    public class AdminEmployController : Controller
    {
        private readonly ILogger<AdminEmployController> _logger;
        private readonly DataContext _context;

        public AdminEmployController(ILogger<AdminEmployController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
            _context.Database.EnsureCreated();
        }

        public IActionResult Index()
        {
            return RedirectToAction("EmployList");
        }

        [HttpGet]
        [Route("/admin/Employ/list")]
        public IActionResult EmployList(int? page)
        {
            int pageSize = 3;
            int pageNumber = page ?? 1;

            var pagedList = _context.Employee.Include(e => e.Department).ToPagedList(pageNumber, pageSize);
            if (pagedList == null || !pagedList.Any())
            {
                return NotFound("Không tìm thấy nhân viên");
            }

            return View(pagedList);
        }
        [HttpGet]
        [Route("/admin/Employ/add")]
        public IActionResult EmployAdd()
        {
            ViewData["DepartmentList"] = _context.Department.ToList();

        // Kiểm tra xem danh sách có chứa dữ liệu không
            var departments = ViewData["DepartmentList"] as List<Department>;
            Console.WriteLine(departments.Count > 0 ? "Department list loaded." : "Department list is empty.");

            return View();
        }


       [HttpPost]
[Route("/admin/Employ/add")]
public IActionResult EmployAdd(Employee employee)
{
    Console.WriteLine($"DepartmentId received: {employee.DepartmentId}");

    // Kiểm tra nếu DepartmentId là giá trị hợp lệ (không phải rỗng hoặc 0)
    if (employee.DepartmentId <= 0)
    {
        ModelState.AddModelError("DepartmentId", "Please select a valid Department.");
    }


    // Log all model state errors
    foreach (var modelState in ModelState)
    {
        foreach (var error in modelState.Value.Errors)
        {
            Console.WriteLine($"Error in {modelState.Key}: {error.ErrorMessage}");
        }
    }

    if (ModelState.IsValid)
    {
        employee.Id = 0; 

        _context.Employee.Add(employee);
        _context.SaveChanges();
        return RedirectToAction("EmployList");
    }

    ViewData["DepartmentList"] = _context.Department.ToList();
    return View(employee);
}








        [HttpGet]
        [Route("/admin/Employ/edit")]
        public IActionResult EmployEdit(int id)
        {
            var employee = _context.Employee.Include(e => e.Department).FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound("Không tìm thấy nhân viên để chỉnh sửa");
            }

            ViewData["DepartmentList"] = _context.Department.ToList();
            return View(employee);
        }

        [HttpPost]
        [Route("/admin/Employ/edit")]
        public IActionResult EmployEdit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employee.Update(employee);
                _context.SaveChanges();

                return RedirectToAction("EmployList");
            }

            ViewData["DepartmentList"] = _context.Department.ToList();
            return View(employee);
        }

        [HttpGet]
        [Route("/admin/Employ/delete")]
        public IActionResult EmployDelete(int id)
        {
            var employee = _context.Employee.Include(e => e.Department).FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound("Không tìm thấy nhân viên để xóa");
            }
            return View(employee);
        }

        [HttpPost]
        [Route("/admin/Employ/delete")]
        public IActionResult EmployDeleteConfirmed(int id)
        {
            var employee = _context.Employee.Find(id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
                _context.SaveChanges();
                HttpContext.Session.SetString("SUCCESS_MSG", "Nhân viên đã được xóa thành công!");
            }
            return RedirectToAction("EmployList");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
