using InAndOutProject.Data;
using InAndOutProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOutProject.Controllers
{
    public class ItemController : Controller
    {

        private readonly ApplicationDbContext _db;

        public ItemController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {

            IEnumerable<ItemModel> objList = _db.Items;
            return View(objList);
        }
        
        //GET-CREATE

        public IActionResult Create()
        {

            return View();
        }

        //POST-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ItemModel obj)
        {
            _db.Items.Add(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
