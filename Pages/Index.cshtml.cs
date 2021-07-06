using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AngusBiniCals.Models;
using AngusBiniCals.Services;

namespace AngusBiniCals.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PostcodeLookupService _postcodeLookupService;
        [Required]
        [BindProperty]
        [MaxLength(10)]
        public string Postcode { get; set; }

        public IEnumerable<AddressEntry> AddressEntries { get; set; }
        
        public IndexModel(PostcodeLookupService postcodeLookupService)
        {
            _postcodeLookupService = postcodeLookupService;
        }
        public void OnGet()
        {

        }

        public async Task<PageResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            AddressEntries = await _postcodeLookupService.GetAddressesFromPostCode(Postcode);

            return Page();
        }
    }
}
