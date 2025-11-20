// EmployeesController.cs - Basic CRUD operations for employees. Create/Edit/Delete actions require [Authorize]
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using emp_management.Data;
using emp_management.Models;

namespace emp_management.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public IActionResult Index()
        {
            try
            {
                var employees = _context.Employees.ToList();
                return View(employees);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error loading employees: " + ex.Message;
                return View(new List<Employee>());
            }
        }

        // GET: Employees/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            // Get salary information for the employee
            var salaries = _context.Salaries
                .Where(s => s.EmployeeId == id)
                .OrderByDescending(s => s.Year)
                .ThenByDescending(s => s.Month)
                .ToList();

            ViewBag.Salaries = salaries;

            return View(employee);
        }

        // GET: Employees/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Employees.Add(employee);
                    _context.SaveChanges();
                    TempData["Success"] = "Employee created successfully!";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating employee: " + ex.Message);
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    // Detach any existing tracked entity
                    var existingEmployee = _context.Employees.Local.FirstOrDefault(e => e.Id == id);
                    if (existingEmployee != null)
                    {
                        _context.Entry(existingEmployee).State = EntityState.Detached;
                    }

                    _context.Entry(employee).State = EntityState.Modified;
                    _context.SaveChanges();
                    TempData["Success"] = "Employee updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating employee: " + ex.Message);
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var employee = _context.Employees.Find(id);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    _context.SaveChanges();
                    TempData["Success"] = "Employee deleted successfully!";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error deleting employee: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
