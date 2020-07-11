using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TechJobsPersistent.Data;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsPersistent.Controllers
{
    public class EmployerController : Controller
    {
        private JobDbContext context;

        public EmployerController(JobDbContext jobDbContext)
        {
            context = jobDbContext;
        }

        // GET: /Employer/
        public IActionResult Index()
        {
            List<Employer> employers = context.Employers.ToList();
            return View(employers);
        }

        public IActionResult Add()
        {
            AddEmployerViewModel add = new AddEmployerViewModel();
            return View(add);
        }

        [HttpPost]
        public IActionResult Add(AddEmployerViewModel add)
        {
            if (ModelState.IsValid)
            {
                var toAdd = new Employer
                {
                    Name = add.Name,
                    Location = add.Location
                };

                context.Employers.Add(toAdd);
                context.SaveChanges();

                return Redirect("Index");
            }
            return View(add);
        }

        public IActionResult About(int id)
        {
            AddEmployerViewModel model;
            
            foreach (var employer in context.Employers.ToList())
            {
                if (id == employer.Id)
                {
                    model = new AddEmployerViewModel
                    {
                        Name = employer.Name,
                        Location = employer.Location
                    };

                    return View(model);
                }
            }
            
            return View();
        }
    }
}
