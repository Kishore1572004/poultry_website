
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YourProjectNamespace.Controllers
{
    public class ToolsController : Controller
    {
        public IActionResult VaccinePlanner()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VaccinePlanner(DateTime hatchDate)
        {
            try
            {
                var schedule = new List<(int dayOffset, string vaccine, string symptom)>
                {
                    (7, "F1 Vaccine", "Poor immunity, viral outbreaks"),
                    (14, "IBD (Gumboro)", "White watery droppings, high mortality"),
                    (21, "Lasota Vaccine", "Weakness, twisted neck, death"),
                    (28, "Fowl Pox", "Skin nodules, scabs, eye swelling"),
                    (35, "Deworming", "Weight loss, poor feed conversion"),
                    (42, "ND R2B", "Breathing issues, poor growth"),
                    (60, "Cholera Vaccine", "Sudden death, diarrhea"),
                    (90, "Booster Combo", "Decline in egg production, infections")
                };

                var result = schedule.Select(v => new
                {
                    date = hatchDate.AddDays(v.dayOffset),
                    vaccine = v.vaccine,
                    symptom = v.symptom
                }).ToList();

                ViewBag.Schedule = result;
                ViewBag.SelectedDate = hatchDate;

                return View();
            }
            catch (Exception ex)
            {
                //  Log the exception or show a user-friendly message
                ViewBag.Error = "An error occurred while generating the vaccine schedule. Please try again.";
                Console.WriteLine("Error in VaccinePlanner POST: " + ex.Message);
                return View();
            }
        }
    }
}
