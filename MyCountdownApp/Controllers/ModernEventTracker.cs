using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyCountdownApp.Controllers
{
    [Route("api/Countdown")]
    [ApiController]
    public class ModernEventTracker : ControllerBase
    {
        private static DateTime? TargetDate = null;

        [HttpPost("set-target")]
        public IActionResult SetTarget([FromBody] DateTime date)
        {
            TargetDate = date;

            return Ok(new { Message = "The date has been successfully saved to the server!" });
        }

        [HttpGet("time-left")]
        public IActionResult GetTimeLeft()
        {
            if (TargetDate == null)
            {
                var now = DateTime.Now;
                return Ok(new
                {
                    isSet = false,
                    hours = now.Hour,
                    minutes = now.Minute,
                    seconds = now.Second
                });
            }

            TimeSpan timeDiff;
            bool isPast = false;

            if (TargetDate.Value > DateTime.Now)
            {
                timeDiff = TargetDate.Value - DateTime.Now;
                isPast = false;
            }
            else
            {
                timeDiff = DateTime.Now - TargetDate.Value;
                isPast = true;
            }

            return Ok(new
            {
                isSet = true,
                isPast = isPast,
                days = timeDiff.Days,
                hours = timeDiff.Hours,
                minutes = timeDiff.Minutes,
                seconds = timeDiff.Seconds
            });
        }
        [HttpPost("clear")]
        public IActionResult ClearTarget()
        {
            TargetDate = null;
            return Ok(new { Message = "time cleared" });
        }
    }
}