using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AngusBiniCals.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AngusBiniCals.Pages
{
    public class CalendarModel : PageModel
    {
        private readonly AddressLookupService _addressLookupService;
        [BindProperty(SupportsGet = true)]
        [Required]
        public string UPRN { get; set; }

        public string Address { get; set; }
        
        public CalendarModel(AddressLookupService addressLookupService)
        {
            _addressLookupService = addressLookupService;
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

            return Page();
        }
    }
}
