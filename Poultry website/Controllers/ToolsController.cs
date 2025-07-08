using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YourProjectNamespace.Controllers
{
    public class ToolsController : Controller
    {
        // Shows the vaccine planner form
        public IActionResult VaccinePlanner()
        {
            return View();
        }

        // Runs when the user submits a hatch date
        [HttpPost]
        public IActionResult VaccinePlanner(DateTime hatchDate)
        {
            try
            {
                // List of vaccines with when to give and related symptoms
                var schedule = new List<(int dayOffset, string vaccine, string symptom)>
                {
                    (7, "F1 Vaccine", "Low immunity, virus spread"),
                    (14, "IBD Gumboro", "White droppings, more deaths"),
                    (21, "Lasota Vaccine", "Weak body, twisted neck"),
                    (28, "Fowl Pox", "Skin bumps, eye swelling"),
                    (35, "Deworming", "Weight drop, poor eating"),
                    (42, "ND R2B", "Breathing trouble, slow growth"),
                    (60, "Cholera Vaccine", "Sudden death, loose motion"),
                    (90, "Booster Combo", "Low egg count, illness")
                };

                // Add days to hatch date to get final vaccine dates
                var result = schedule.Select(v => new
                {
                    date = hatchDate.AddDays(v.dayOffset),
                    vaccine = v.vaccine,
                    symptom = v.symptom
                }).ToList();

                // Send schedule and selected date to the view
                ViewBag.Schedule = result;
                ViewBag.SelectedDate = hatchDate;

                return View();
            }
            catch (Exception ex)
            {
                // Show error if something goes wrong
                ViewBag.Error = "Something went wrong. Please try again.";
                Console.WriteLine("Error in VaccinePlanner POST: " + ex.Message);
                return View();
            }
        }
    }
}
