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
            if (createdBefore.TotalMinutes < 1)
            {
                return $"{(int)createdBefore.TotalSeconds} seconds";
            }
            if (createdBefore.TotalHours < 1)
            {
                return $"{(int)createdBefore.TotalMinutes} minutes";
            }
            if (createdBefore.TotalDays < 1)
            {
                return $"{(int)createdBefore.TotalHours} hours";
            }
            if (createdBefore.TotalDays < 30)
            {
                return $"{(int)createdBefore.TotalDays / 30} months";
            }
            return $"{(int)createdBefore.TotalDays / 365} years";

        }
    }
}
