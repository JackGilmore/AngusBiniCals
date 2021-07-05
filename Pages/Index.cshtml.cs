using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
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
        private readonly PostcodeLookupService _postcodeLookupService;
        [RegularExpression("([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\\s?[0-9][A-Za-z]{2})\r\n")]
        [Required]
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
