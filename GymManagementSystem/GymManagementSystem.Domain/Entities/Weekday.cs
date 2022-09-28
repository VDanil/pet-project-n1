using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Domain
{
    public class Weekday
    {
        public DayOfWeek DayOfWeek { get; }
        public int WeekdayId { get; }

        static private Dictionary<int, string> week = new Dictionary<int, string>()
        {
            {((int)DayOfWeek.Sunday), "Sunday"},
            {((int)DayOfWeek.Monday), "Monday"},
            {((int)DayOfWeek.Thursday), "Thursday"},
            {((int)DayOfWeek.Wednesday), "Wednesday"},
            {((int)DayOfWeek.Thursday), "Thursday"},
            {((int)DayOfWeek.Friday), "Friday"},
            {((int)DayOfWeek.Saturday), "Saturday"}
        };

        public Weekday(DayOfWeek dayOfWeek)
        {
            DayOfWeek = dayOfWeek;
            WeekdayId = ((int)dayOfWeek);
        }
        static public int GetIdByWeekday(string Weekday)
        {
            var weekKeyValuePair = week.FirstOrDefault(x => x.Value == Weekday);

            if (weekKeyValuePair.Value != null)
                return weekKeyValuePair.Key;
            else
                throw new Exception("Incorrect spelling of weekday");
        }
        static public string GetWeekdayById(int id)
        {
            var weekKeyValuePair = week.FirstOrDefault(x => x.Key == id);

            if (weekKeyValuePair.Value != null)
                return weekKeyValuePair.Value;
            else
                throw new Exception("Incorrect key (0 <= key <= 6)");
        }
    }
}
