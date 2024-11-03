using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HandBook.Classes
{
    internal class Car
    {
        public int Id { get; set; }
        public string brand { get; set; }
        public string color { get; set; }
        private string serialNumber { get; set; }
        private string sideNumber { get; set; }
        private DateTime dateOfManufacture { get; set; }
        public string featuresOfDesignAndColoring { get; set; }
        private DateTime dateofLastTechnicalInspection { get; set; }

        // Descriptors
        public string SerialNumber
        {
            get {  return serialNumber; }
            set
            {
                string vinPattern = @"^[A-HJ-NPR-Z0-9]{17}$";
                if (Regex.IsMatch(value, vinPattern))
                {
                    serialNumber = value;
                }
                else
                {
                    throw new FormatException("Некорректный заводской номер, попробуйте ещё раз.");
                }
            }
        }

        public string SideNumber
        {
            get { return sideNumber; }
            set
            {
                string sideNumberPattern = @"^[A-Za-z0-9]+$";
                if (Regex.IsMatch(value, sideNumberPattern))
                {
                    sideNumber = value;
                }
                else
                {
                    throw new FormatException("Некорректный бортовой номер, попробуйте ещё раз.");
                }
            }
        }

        public string DateOfManufacture
        {
            get { return dateOfManufacture.ToString("dd.MM.yyyy"); }
            set
            {
                if (DateTime.TryParseExact(value, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    dateOfManufacture = parsedDate;
                }
                else
                {
                    throw new FormatException("Некорректный формат даты выпуска, используйте формат DD.MM.YYYY");
                }
            }
        }

        public string DateofLastTechnicalInspection
        {
            get { return dateofLastTechnicalInspection.ToString("dd.MM.yyyy"); }
            set
            {
                if (DateTime.TryParseExact(value, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    dateofLastTechnicalInspection = parsedDate;
                }
                else
                {
                    throw new FormatException("Некорректный формат даты выпуска, используйте формат DD.MM.YYYY");
                }
            }
        }

        // Constructors
        public Car(int id, string brand, string color, string serialNumber, string sideNumber, string dateOfManufacture, string featuresOfDesignAndColoring, string dateofLastTechnicalInspection)
        {
            Id = id;
            this.brand = brand;
            this.color = color;
            SerialNumber = serialNumber;
            SideNumber = sideNumber;
            DateOfManufacture = dateOfManufacture;
            this.featuresOfDesignAndColoring = featuresOfDesignAndColoring;
            DateofLastTechnicalInspection = dateofLastTechnicalInspection;
        }

        // Methods
        public override string ToString()
        {
            return $"{"ID:",-30} {Id}\n" +
                   $"{"Марка:",-30} {brand}\n" +
                   $"{"Цвет:",-30} {color}\n" +
                   $"{"VIN:",-30} {SerialNumber}\n" +
                   $"{"Бортовой номер:",-30} {SideNumber}\n" +
                   $"{"Дата выпуска:",-30} {DateOfManufacture:dd.MM.yyyy}\n" +
                   $"{"Особенности конструкции:",-30} {featuresOfDesignAndColoring}\n" +
                   $"{"Дата последнего техосмотра:",-30} {DateofLastTechnicalInspection}";
        }


    }
}
