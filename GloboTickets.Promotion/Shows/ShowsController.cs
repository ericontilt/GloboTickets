using System;
using System.Linq;
using System.Threading.Tasks;
using GloboTickets.Promotion.Acts;
using GloboTickets.Promotion.Venues;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GloboTickets.Promotion.Shows
{
    public class ShowsController : Controller
    {
        private readonly ShowCommands showCommands;
        private readonly ShowQueries showQueries;
        private readonly VenueQueries venueQueries;
        private readonly ActQueries actQueries;

        public ShowsController(ShowCommands showCommands, ShowQueries showQueries, VenueQueries venueQueries, ActQueries actQueries)
        {
            this.showCommands = showCommands;
            this.showQueries = showQueries;
            this.venueQueries = venueQueries;
            this.actQueries = actQueries;
        }

        public async Task<IActionResult> Create(Guid id)
        {
            var act = await actQueries.GetAct(id);
            if (act == null)
            {
                return NotFound();
            }

            var venues = await venueQueries.ListVenues();
            var nextWeek = DateTime.Now.Date.AddDays(7).AddHours(19);
            var viewModel = new CreateShowViewModel
            {
                Act = act,
                Venues = venues.Select(venue => new SelectListItem
                {
                    Value = venue.VenueGuid.ToString(),
                    Text = $"{venue.Name}, {venue.City}"
                }).ToList(),
                StartTime = nextWeek
            };
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(String.Empty, TempData["CustomError"].ToString());
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid id, [Bind("Venue,StartTime")] CreateShowViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var venue = await venueQueries.GetVenue(viewModel.Venue);
                if (venue == null)
                {
                    TempData["CustomError"] = "Venue not found";
                    return RedirectToAction(nameof(Create), new { id });
                }

                if (venue.TimeZone == null)
                {
                    TempData["CustomError"] = "The selected venue does not have a time zone";
                    return RedirectToAction(nameof(Create), new { id });
                }

                var timeZone = TimeZoneInfo.FindSystemTimeZoneById(venue.TimeZone);
                if (timeZone == null)
                {
                    TempData["CustomError"] = $"The selected venue has an invalid time zone: {venue.TimeZone}";
                    return RedirectToAction(nameof(Create), new { id });
                }

                var offset = timeZone.GetUtcOffset(viewModel.StartTime);
                await showCommands.ScheduleShow(id, viewModel.Venue, new DateTimeOffset(viewModel.StartTime, offset));
                return RedirectToAction("Details", "Acts", new { id });
            }
            else
            {
                return View(viewModel);
            }
        }
    }
}