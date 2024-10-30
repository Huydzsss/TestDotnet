using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Asp.Net_MvcWeb_Pj3.Aptech.Models;
using PagedList;
using BCrypt.Net;

namespace Asp.Net_MvcWeb_Pj3.Aptech.Controllers
{
    public class AdminDepartmentController : Controller
    {
        private readonly ILogger<AdminDepartmentController> _logger;
        private readonly DataContext context = new DataContext();

        public AdminDepartmentController(ILogger<AdminDepartmentController> logger)
        {
            _logger = logger;
            // Sync database structure with Model Classes (if not created)
            context.Database.EnsureCreated();
        }

        // Redirect to the Department list by default
        public IActionResult Index()
        {
            return RedirectToAction("DepartmentList");
        }

        // Display the list of Departments
        [HttpGet]
        [Route("/admin/Department/list")]
        public IActionResult DepartmentList(int? page)
        {
            int pageSize = 10; // Số bản ghi trên mỗi trang
            int pageNumber = (page ?? 1); // Trang hiện tại, mặc định là 1

            // Fetch the list of departments from the database
            var departments = context.Department.ToList();

            // Chuyển danh sách sang PagedList
            var pagedDepartments = departments.ToPagedList(pageNumber, pageSize);

            return View(pagedDepartments);
        }

        // Show the Add Department form
        [HttpGet]
        [Route("/admin/Department/add")]
        public IActionResult DepartmentAdd()
        {
            return View();
        }

        // Handle the POST request to add a new department
        [HttpPost]
        [Route("/admin/Department/add")]
        public IActionResult DepartmentAdd(Department department)
        {
           

            // Save the new department to the database
            context.Add(department);
            context.SaveChanges();

            // Set a success message in the session
            HttpContext.Session.SetString("SUCCESS_MSG", "Department added successfully.");

            return RedirectToAction("DepartmentList");
        }
        [HttpGet]
[Route("/admin/Department/edit")]
public IActionResult DepartmentEdit(int id)
{
    var department = context.Department.FirstOrDefault(d => d.Id == id);
    if (department == null)
    {
        return NotFound();
    }

    return View(department);
}

// Handle the POST request to edit a department
[HttpPost]
[Route("/admin/Department/edit")]
public IActionResult DepartmentEdit(Department department)
{
    var existingDepartment = context.Department.FirstOrDefault(d => d.Id == department.Id);
    if (existingDepartment == null)
    {
        return NotFound();
    }

    existingDepartment.Name = department.Name; // Cập nhật tên hoặc các thông tin khác
    context.SaveChanges();

    return RedirectToAction("DepartmentList");
}

// Handle the Delete Department
[HttpGet]
[Route("/admin/Department/delete")]
public IActionResult DepartmentDelete(int id)
{
    var department = context.Department.FirstOrDefault(d => d.Id == id);
    if (department == null)
    {
        return NotFound();
    }

    context.Department.Remove(department);
    context.SaveChanges();

    return RedirectToAction("DepartmentList");
}

        // Error handling
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
