using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers.TimeSinceCreationFunctionality
{
    public class TimeConvertingService
    {
        public string ConvertDateTime(DateTime dateTime)
        {
            TimeSpan createdBefore = DateTime.UtcNow.Subtract(dateTime);

            string time = string.Empty;

            if (createdBefore.TotalMinutes < 1)
            {
                time = $"{(int)createdBefore.TotalSeconds} seconds";
            }
            else if (createdBefore.TotalHours < 1)
            {
                time = $"{(int)createdBefore.TotalMinutes} minutes";
            }
            else if (createdBefore.TotalDays < 1)
            {
                time = $"{(int)createdBefore.TotalHours} hours";
            }
            else if (createdBefore.TotalDays < 30)
            {
                time = $"{(int)createdBefore.TotalDays} days";
            }
            else if (createdBefore.TotalDays / 30 < 12)
            {
                time = $"{(int)createdBefore.TotalDays / 30} months";
            }
            else
            {
                time = $"{(int)createdBefore.TotalDays / 365} years";

            }

            return time + " ago";

        }
    }
}
