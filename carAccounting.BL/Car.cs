using System;

namespace Car_accounting.carAccounting.BL
{
    public class Car
    {
        public Car(string carBrand, string color, int yearOfRelease, DateTime dateOfSale, double amount)
        {
            CarBrand = carBrand;
            Color = color;
            YearOfRelease = yearOfRelease;
            DateOfSale = dateOfSale;
            Amount = amount;
        }

        public string CarBrand { get; set; }

        public string Color { get; set; }

        public int YearOfRelease { get; set; }

        public DateTime DateOfSale { get; set; }

        public double Amount { get; set; }

        public override string ToString() => CarBrand + "-" + Amount;
    }
}