using GloboTickets.Promotion.DataAccess;
using GloboTickets.Promotion.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GloboTickets.Promotion.Pages
{
    public class ActModel : PageModel
    {
        private readonly ActQueries actQueries;
        private readonly ActCommands actCommands;
        private readonly ContentQueries contentQueries;
        private readonly ContentCommands contentCommands;

        public ActModel(ActQueries actQueries, ActCommands actCommands, ContentQueries contentQueries, ContentCommands contentCommands)
        {
            this.actQueries = actQueries;
            this.actCommands = actCommands;
            this.contentQueries = contentQueries;
            this.contentCommands = contentCommands;
        }

        [BindProperty(SupportsGet=true)]
        public Guid ActGuid { get; set; }

        public bool AddAct { get; set; }

        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public DateTime Date { get; set; } = DateTime.Now;
        [BindProperty]
        public string City { get; set; }
        [BindProperty]
        public string Venue { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }
        [BindProperty]
        public string ImageHash { get; set; }
        [BindProperty]
        public long LastModifiedTicks { get; set; }
        public string ErrorMessage { get; set; }

        public async Task OnGet()
        {
            var act = await actQueries.GetAct(ActGuid);

            if (act == null)
            {
                AddAct = true;
            }
            else
            {
                AddAct = false;
                if (act.Description != null)
                {
                    Title = act.Description.Title;
                    Date = act.Description.Date.ToLocalTime();
                    City = act.Description.City;
                    Venue = act.Description.Venue;
                    ImageHash = act.Description.ImageHash;
                    LastModifiedTicks = act.Description.LastModifiedTicks;
                }
            }
        }

        public async Task<IActionResult> OnPost()
        {
            var imageHash = await GetImageHash();

            try
            {
                await actCommands.SetActDescription(ActGuid, new ActDescriptionModel
                {
                    Title = Title,
                    Date = Date.ToUniversalTime(),
                    City = City,
                    Venue = Venue,
                    ImageHash = imageHash,
                    LastModifiedTicks = LastModifiedTicks
                });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }

            return Redirect("~/");
        }

        private async Task<string> GetImageHash()
        {
            if (Image != null)
            {
                using var imageReadStream = Image.OpenReadStream();
                using var imageMemoryStream = new MemoryStream();
                await imageReadStream.CopyToAsync(imageMemoryStream);
                var imageHash = await contentCommands.SaveContent(imageMemoryStream.ToArray(), Image.ContentType);
                return imageHash;
            }
            else
            {
                return ImageHash;
            }
        }
    }
}