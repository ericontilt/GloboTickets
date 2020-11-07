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
    public class ShowModel : PageModel
    {
        private readonly ShowQueries showQueries;
        private readonly ShowCommands showCommands;
        private readonly ContentQueries contentQueries;
        private readonly ContentCommands contentCommands;

        public ShowModel(ShowQueries showQueries, ShowCommands showCommands, ContentQueries contentQueries, ContentCommands contentCommands)
        {
            this.showQueries = showQueries;
            this.showCommands = showCommands;
            this.contentQueries = contentQueries;
            this.contentCommands = contentCommands;
        }

        [BindProperty(SupportsGet=true)]
        public Guid ShowGuid { get; set; }

        public bool AddShow { get; set; }

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
            var show = await showQueries.GetShow(ShowGuid);

            if (show == null)
            {
                AddShow = true;
            }
            else
            {
                AddShow = false;
                if (show.Description != null)
                {
                    Title = show.Description.Title;
                    Date = show.Description.Date.ToLocalTime();
                    City = show.Description.City;
                    Venue = show.Description.Venue;
                    ImageHash = show.Description.ImageHash;
                    LastModifiedTicks = show.Description.LastModifiedTicks;
                }
            }
        }

        public async Task<IActionResult> OnPost()
        {
            var imageHash = await GetImageHash();

            try
            {
                await showCommands.SetShowDescription(ShowGuid, new ShowDescriptionModel
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
