using InAndOutProject.Data;
using InAndOutProject.Models;
using InAndOutProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOutProject.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ExpenseController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {

            IEnumerable<Expense> objList = _db.Expenses;
            foreach (var obj in objList)
            {
                obj.ExpenseType = _db.ExpenseTypes.FirstOrDefault(u => u.Id == obj.ExpenseTypeId);
            }
            return View(objList);
        }

        //GET-CREATE

        public IActionResult Create()
        {
            /* IEnumerable<SelectListItem> TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
             {
                 Text = i.Name,
                 Value = i.Id.ToString()
             });

             ViewBag.TypeDropDown = TypeDropDown;*/

            ExpenseViewModel expenseViewModels = new ExpenseViewModel()
            {

                Expense = new Expense(),
                TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };



            return View(expenseViewModels);
        }

        //POST-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseViewModel obj)
        {
            if (ModelState.IsValid)
            {
               // obj.ExpenseTypeId = 1;
            _db.Expenses.Add(obj.Expense);
            _db.SaveChanges();

            return RedirectToAction("Index");

            }

            return View(obj);
        }

        //POST-GET
       
        public IActionResult Delete(int? id)
        {
            if (id==null || id==0)
            {
                return NotFound();
            }
            var obj = _db.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        //POST-DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            
                _db.Expenses.Remove(obj);
                _db.SaveChanges();

                return RedirectToAction("Index");

        }

        
        //GET UPDATE
        public IActionResult Update(int? id)
        {

            ExpenseViewModel expenseViewModels = new ExpenseViewModel()
            {

                Expense = new Expense(),
                TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            if (id == null || id == 0)
            {
                return NotFound();
            }
            expenseViewModels.Expense = _db.Expenses.Find(id);
            if (expenseViewModels.Expense == null)
            {
                return NotFound();
            }
            return View(expenseViewModels);

        }

        //POST-UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExpenseViewModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.Expenses.Update(obj.Expense);
                _db.SaveChanges();

                return RedirectToAction("Index");

            }

            return View(obj);
        }
    }
}
