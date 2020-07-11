using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index(List<Job> jobs)
        {
            jobs = context.Jobs.Include(j => j.Employer).ToList();
            //For some reason this isn't running when I save a new job and come back to index, 
            //it shows that the list is empty. Had to initialize another list in the ProcessAddForm and pass it here

            return View(jobs);
        }

        [HttpGet]
        public IActionResult AddJob(AddJobViewModel model)
        {
            //***Need to figure out how to get the web app to stop displaying input error message on FIRST load, even before
            //user submits input

            model = new AddJobViewModel(context.Employers.ToList());
            
            return View(model);
        }

        [HttpPost("/Home/AddJob")]
        public IActionResult ProcessAddJobForm(AddJobViewModel model)
        {
            if (ModelState.IsValid)
            {
                context.Jobs.Add(new Job
                {
                    Name = model.Name,
                    Employer = context.Employers.Find(model.EmployerId)
                });

                context.SaveChanges();

                var returnList = context.Jobs.Include(j => j.Employer).ToList();
                return View("Index", returnList);
            }

            var returnModel = new AddJobViewModel(context.Employers.ToList());
            return View("AddJob", returnModel);
        }

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
