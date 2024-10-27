using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DateTimee
{
    internal class Program
    {
        static void Main(string[] args)
        {

            DateTime dateTime = DateTime.Now;
            Console.WriteLine();
            Console.WriteLine("{0}/{1}/{2}  {3}:{4}:{5}", dateTime.Year, dateTime.Month,
                dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);

            bool flag = true;
            while(flag)
            {
                Console.WriteLine("Write Date And Time to increase or dicrease: ");
                string input = Console.ReadLine();

                Console.WriteLine();

                DateTimeData dateTimeData = new DateTimeData();
                dateTimeData.ProcessInput(input);

                if (input.Equals("q"))
                {
                    flag = false;
                }
            }
            

            Console.ReadKey();
        }

    }

    // -------------------------------------------------------------------------------------------- Struct DateTimeData ---------------------------------------------------------------------------------------
    public struct DateTimeData
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }


        // -------------------------------------------------------------------------------------------- Method ProcessInput ---------------------------------------------------------------------------------------

        public void ProcessInput(string input)
        {

            Year = DateTime.Now.Year;
            Month = DateTime.Now.Month;
            Day = DateTime.Now.Day;
            Hour = DateTime.Now.Hour;
            Minute = DateTime.Now.Minute;
            Second = DateTime.Now.Second;
            
            Dictionary<char, int> results = ParseInputValues(input);

            if (results.TryGetValue('y', out int yearValue))
                Year += yearValue;

            if (results.TryGetValue('m', out int monthValue))
                AddMonths(monthValue);

            if (results.TryGetValue('d', out int dayValue))
                AddDays(dayValue);

            if (results.TryGetValue('h', out int hourValue))
                AddHours(hourValue);

            if (results.TryGetValue('n', out int minuteValue))
                AddMinutes(minuteValue);

            if (results.TryGetValue('s', out int secondValue))
                AddSeconds(secondValue);

            Console.WriteLine($"{Year}/{Month}/{Day} {Hour}:{Minute}:{Second}");
        }

        // -------------------------------------------------------------------------------------------- Finish Method ProcessInput ---------------------------------------------------------------------------------------

       
        // -------------------------------------------------------------------------------------------------- Method AddMonths ------------------------------------------------------------------------------------------------
        private void AddMonths(int monthsToAdd)
        {

            Month += monthsToAdd;
            if (Month > 12)
            {
                Year += Month / 12;
                Month %= 12;
            }
            if(Month < 1)
            {
                int minusYears = Month / 12;
                Year--;
                Month = 12 + Month % 12;
                while (minusYears < 0)
                {
                    Year--;
                    minusYears++;
                }
            }

        }

        // -------------------------------------------------------------------------------------------------- Method AddMonths ------------------------------------------------------------------------------------------------

        // -------------------------------------------------------------------------------------------------- Method AddDays ------------------------------------------------------------------------------------------------
        private void AddDays(int daysToAdd)
        {

            Day += daysToAdd;
            if (daysToAdd > 0)
            {
                while (true)
                {
                    int daysInCurrentMonth = GetDaysInMonth(Month, Year);
                    if (Day > daysInCurrentMonth)
                    {
                        Day -= daysInCurrentMonth;
                        AddMonths(1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else if (daysToAdd < 0)
            {
                while (Day < 1)
                {
                    Month--; 
                    if (Month < 1)
                    {
                        Month = 12; 
                        Year--; 
                    }
                    int daysInPreviousMonth = GetDaysInMonth(Month, Year);
                    Day += daysInPreviousMonth; 
                }
            }
        }

        // -------------------------------------------------------------------------------------------------- Finish Method AddDays ------------------------------------------------------------------------------------------------

        // -------------------------------------------------------------------------------------------------- Method GetDaysInMonth ------------------------------------------------------------------------------------------------
        private int GetDaysInMonth(int month, int year)
        {
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 31;
                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;
                case 2:
                    return IsLeap(year) ? 29 : 28;
                default:
                    return 0; 
            }
        }

        // -------------------------------------------------------------------------------------------------- Finish Method GetDaysInMonth ------------------------------------------------------------------------------------------------


        // -------------------------------------------------------------------------------------------------- Method AddHours ------------------------------------------------------------------------------------------------

        private void AddHours(int hoursToAdd)
        {
            Hour += hoursToAdd;
            if (Hour >= 24)
            {
                int daysToAdd = Hour / 24;
                Hour %= 24;
                AddDays(daysToAdd); 
            }
        }


        // -------------------------------------------------------------------------------------------------- Finish Method AddHours ------------------------------------------------------------------------------------------------

        // -------------------------------------------------------------------------------------------------- Method addMinutes ------------------------------------------------------------------------------------------------

        private void AddMinutes(int minutesToAdd)
        {
            Minute += minutesToAdd;
            if (Minute > 60){

                AddHours(Minute / 60);
                Minute %= 60;
            }
        }

        // -------------------------------------------------------------------------------------------------- Finish Method addMinutes ------------------------------------------------------------------------------------------------


        // -------------------------------------------------------------------------------------------------- Method addMinutes ------------------------------------------------------------------------------------------------

        private void AddSeconds(int SecondsToAdd)
        {
            Second += SecondsToAdd;
            if (Second > 60)
            {
                AddMinutes(Second / 60);
                Second %= 60;
            }
        }

        // -------------------------------------------------------------------------------------------------- Finish Method addMinutes ------------------------------------------------------------------------------------------------

        // -------------------------------------------------------------------------------------------- Method ParseInputValues ---------------------------------------------------------------------------------------
        private Dictionary<char, int> ParseInputValues(string input)
        {
            Dictionary<char, int> values = new Dictionary<char, int>();
            char[] characters = { 'y', 'm', 'd', 'h', 'n', 's' };

            foreach (char c in characters)
            {
                string value = GetNumberFromInput(input, c);
                if (!string.IsNullOrEmpty(value))
                {
                    values[c] = int.Parse(value);
                }
            }

            return values;
        }

        // -------------------------------------------------------------------------------------------- Finish Method ParseInputValues ---------------------------------------------------------------------------------------

        // -------------------------------------------------------------------------------------------------- Method GetNumberFromInput ------------------------------------------------------------------------------------------------
        private string GetNumberFromInput(string input, char c)
        {
            string number = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == c)
                {
                    int j = i - 1;
                    while (j >= 0 && (char.IsDigit(input[j]) || input[j] == '-'))
                    {
                        number = input[j] + number;
                        j--;
                    }
                    break;
                }
            }
            return number;
        }

        // -------------------------------------------------------------------------------------------------- Finish Method GetNumberFromInput ------------------------------------------------------------------------------------------------

       
        // -------------------------------------------------------------------------------------------------- Method IsLeap ------------------------------------------------------------------------------------------------

        private bool IsLeap(int year)
        {
            if (year % 4 == 0)
            {
                if (year % 100 == 0)
                {
                    if (year % 400 == 0)
                    {
                        return true;
                    }
                    return false;
                }
                return true;
            }
            return false;
        }

        // -------------------------------------------------------------------------------------------------- Finish Method IsLeap ------------------------------------------------------------------------------------------------

    }

    // -------------------------------------------------------------------------------------------- Finish Struct DateTimeData ---------------------------------------------------------------------------------------

}



