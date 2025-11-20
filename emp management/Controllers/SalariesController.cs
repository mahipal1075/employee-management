// SalariesController.cs - Manages employee salary records with CRUD operations
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using emp_management.Data;
using emp_management.Models;

namespace emp_management.Controllers
{
    public class SalariesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Salaries/ByEmployee/5
        public IActionResult ByEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            var salaries = _context.Salaries
                .Where(s => s.EmployeeId == id)
                .OrderByDescending(s => s.Year)
                .ThenByDescending(s => s.Month)
                .ToList();

            ViewBag.Employee = employee;
            return View(salaries);
        }

        // GET: Salaries/Create/5
        [Authorize]
        public IActionResult Create(int employeeId)
        {
            var employee = _context.Employees.Find(employeeId);
            if (employee == null)
            {
                return NotFound();
            }

            ViewBag.Employee = employee;
            var salary = new Salary
            {
                EmployeeId = employeeId,
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year
            };

            return View(salary);
        }

        // POST: Salaries/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Salary salary)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Calculate net salary
                    salary.NetSalary = salary.Basic + salary.Allowances - salary.Deductions;

                    // Check if salary already exists for this month/year
                    var existing = _context.Salaries
                        .FirstOrDefault(s => s.EmployeeId == salary.EmployeeId 
                                          && s.Month == salary.Month 
                                          && s.Year == salary.Year);

                    if (existing != null)
                    {
                        ModelState.AddModelError("", "Salary for this month and year already exists.");
                    }
                    else
                    {
                        _context.Salaries.Add(salary);
                        _context.SaveChanges();
                        TempData["Success"] = "Salary record added successfully!";
                        return RedirectToAction("ByEmployee", new { id = salary.EmployeeId });
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating salary: " + ex.Message);
            }

            var employee = _context.Employees.Find(salary.EmployeeId);
            ViewBag.Employee = employee;
            return View(salary);
        }

        // GET: Salaries/Edit/5
        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = _context.Salaries.Find(id);
            if (salary == null)
            {
                return NotFound();
            }

            var employee = _context.Employees.Find(salary.EmployeeId);
            ViewBag.Employee = employee;

            return View(salary);
        }

        // POST: Salaries/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Salary salary)
        {
            if (id != salary.Id)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    // Calculate net salary
                    salary.NetSalary = salary.Basic + salary.Allowances - salary.Deductions;

                    var existingSalary = _context.Salaries.Local.FirstOrDefault(e => e.Id == id);
                    if (existingSalary != null)
                    {
                        _context.Entry(existingSalary).State = EntityState.Detached;
                    }

                    _context.Entry(salary).State = EntityState.Modified;
                    _context.SaveChanges();
                    TempData["Success"] = "Salary record updated successfully!";
                    return RedirectToAction("ByEmployee", new { id = salary.EmployeeId });
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalaryExists(salary.Id))
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
                ModelState.AddModelError("", "Error updating salary: " + ex.Message);
            }

            var employee = _context.Employees.Find(salary.EmployeeId);
            ViewBag.Employee = employee;
            return View(salary);
        }

        // GET: Salaries/Delete/5
        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = _context.Salaries.Find(id);
            if (salary == null)
            {
                return NotFound();
            }

            var employee = _context.Employees.Find(salary.EmployeeId);
            ViewBag.Employee = employee;

            return View(salary);
        }

        // POST: Salaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var salary = _context.Salaries.Find(id);
                if (salary != null)
                {
                    var employeeId = salary.EmployeeId;
                    _context.Salaries.Remove(salary);
                    _context.SaveChanges();
                    TempData["Success"] = "Salary record deleted successfully!";
                    return RedirectToAction("ByEmployee", new { id = employeeId });
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error deleting salary: " + ex.Message;
            }
            return RedirectToAction("Index", "Employees");
        }

        private bool SalaryExists(int id)
        {
            return _context.Salaries.Any(e => e.Id == id);
        }
    }
}
