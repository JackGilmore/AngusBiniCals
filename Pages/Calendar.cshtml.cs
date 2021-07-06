using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AngusBiniCals.Models;
using AngusBiniCals.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AngusBiniCals.Pages
{
    public class CalendarModel : PageModel
    {
        private readonly AddressLookupService _addressLookupService;
        private readonly CalendarService _calendarService;
        [BindProperty(SupportsGet = true)]
        [Required]
        public string UPRN { get; set; }
        public string Address { get; set; }
        public string CalURL { get; set; }
        public IEnumerable<CalendarEntry> CalendarEntries { get; set; }
        public CalendarModel(AddressLookupService addressLookupService, CalendarService calendarService)
        {
            _addressLookupService = addressLookupService;
            _calendarService = calendarService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(UPRN))
            {
                return NotFound("No UPRN supplied");
            }

            Address = await _addressLookupService.GetAddressFromUPRN(UPRN);

            if (string.IsNullOrEmpty(Address))
            {
                return NotFound("No address found for UPRN");
            }

            CalendarEntries = await _calendarService.GetDatesForUPRN(UPRN);

            CalURL = Url.ActionLink("ical", "API", new {uprn = UPRN},"webcal");

            return Page();
        }
    }
}
