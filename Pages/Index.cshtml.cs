using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AngusBiniCals.Models;
using AngusBiniCals.Services;

namespace AngusBiniCals.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AddressLookupService _addressLookupService;
        [Required]
        [BindProperty]
        [MaxLength(10)]
        public string Postcode { get; set; }

        public IEnumerable<AddressEntry> AddressEntries { get; set; }

        public IndexModel(AddressLookupService addressLookupService)
        {
            _addressLookupService = addressLookupService;
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

            AddressEntries = await _addressLookupService.GetAddressesFromPostCode(Postcode);

            AddressEntries = AddressEntries.OrderBy(x => x, new AddressComparer());

            return Page();
        }
    }
}
