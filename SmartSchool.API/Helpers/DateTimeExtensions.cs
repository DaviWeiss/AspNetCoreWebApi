using System;

namespace SmartSchool.API.Helpers
{
    public static class DateTimeExtensions
    {
        public static int GetCurrentAge(this DateTime dateTime)
        {
            var currenteAge = DateTime.UtcNow;
            int age = currenteAge.Year - dateTime.Year;

            if (currenteAge < dateTime.AddYears(age))
                age--;

            return age;
        }
    }
}