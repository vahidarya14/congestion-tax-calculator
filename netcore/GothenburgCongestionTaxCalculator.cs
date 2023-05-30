using System;
using System.Collections.Generic;
using System.Linq;
using congestion.calculator;
public class GothenburgCongestionTaxCalculator : ICongestionTaxCalculator
{
    /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that day
         */
    public int GetTax(Vehicle vehicle, DateTime[] dates)
    {
        if (IsTollFreeVehicle(vehicle)) return 0;

        dates = dates.Where(x => !IsTollFreeDate(x)).OrderBy(x => x).ToArray();

        List<(DateTime DateTime, int TollFee)> Temp = new List<(DateTime, int)>();
        DateTime intervalStart = dates[0];
        int totalFee = 0;
        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee( vehicle, date);
            Temp.Add((date, nextFee));
            //int tempFee = GetTollFee(intervalStart, vehicle);

            //long diffInMillies = date.Millisecond - intervalStart.Millisecond;
            //long minutes = diffInMillies / 1000 / 60;


            //if (minutes <= 60)
            //{
            //    if (totalFee > 0) totalFee -= tempFee;
            //    if (nextFee >= tempFee) tempFee = nextFee;
            //    totalFee += tempFee;
            //}
            //else
            //{
            //    totalFee += nextFee;
            //}
        }
        //if (totalFee > 60) totalFee = 60;

        while (Temp.Count > 0)
        {
            var date = Temp[0];
            var tollsIn60Min = Temp
              .Where(x => x.DateTime >= date.DateTime && x.DateTime <= date.DateTime.AddMinutes(60))
              .ToList();
            var maxTollIn60Min = tollsIn60Min.Max(x => x.TollFee);

            totalFee += Math.Min(maxTollIn60Min, 60);


            Temp.RemoveAll(x => x.DateTime >= date.DateTime && x.DateTime <= date.DateTime.AddMinutes(60));
        }
        return totalFee;
    }

    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        if (vehicle == null) return false;

        var vehicleType = vehicle.GetVehicleType();
        return vehicleType.Equals(TollFreeVehicles.Motorcycle.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Bus.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Military.ToString());
    }

    int GetTollFee(Vehicle vehicle, DateTime date)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

        int hour = date.Hour;
        int minute = date.Minute;

        if (hour == 6 && minute >= 0 && minute <= 29) return 8;
        else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
        else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
        else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
        else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
        else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
        else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
        else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
        else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
        else return 0;
    }

    private bool IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (year == 2013)
        {
            if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }
        }
        return false;
    }

    public enum TollFreeVehicles
    {
        Emergency,
        Bus,
        Diplomat,
        Motorcycle,
        Military,
        Foreign
    }
}

