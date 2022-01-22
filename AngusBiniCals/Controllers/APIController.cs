using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;
using AngusBiniCals.Services;
using Ical.Net.Serialization;

namespace AngusBiniCals.Controllers
{
    [Route("[action]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly CalendarService _calendarService;

        public APIController(CalendarService calendarService)
        {
            _calendarService = calendarService;
        }
        public async Task<IActionResult> Ical(string uprn, int? reminder = null)
        {
            // TODO: Cache this
            var calendar = await _calendarService.GetCalendarForUPRN(uprn, reminder);

            var serializer = new CalendarSerializer();
            var serializedCalendar = serializer.SerializeToString(calendar);

            var calendarAsBytes = Encoding.UTF8.GetBytes(serializedCalendar);

            return File(calendarAsBytes, "text/calendar");
        }
    }
}
