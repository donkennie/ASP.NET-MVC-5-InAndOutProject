using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOutProject.Models.ViewModels
{
    public class ExpenseViewModel
    {

        public Expense Expense { get; set; }

        public IEnumerable<SelectListItem> TypeDropDown { get; set; }
    }
}
