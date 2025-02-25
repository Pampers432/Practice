using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HandBook.Classes
{
    public class Car
    {
        private string brand = null!;
        private string color = null!;
        private string serialNumber = null!;
        private string sideNumber = null!;
        private string dateOfManufacture = null!;
        private string featuresOfDesignAndColoring = null!;
        private string dateofLastTechnicalInspection = null!;

        [Required]
        public string Brand
        {
            get { return brand; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new FormatException("Поле 'Марка' не должно быть пустым.");
                brand = value;
            }
        }

        [Required]
        public string Color
        {
            get { return color; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new FormatException("Поле 'Цвет' не должно быть пустым.");
                color = value;
            }
        }

        [Required]
        public string SerialNumber
        {
            get { return serialNumber; }
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
            get { return dateOfManufacture; }
            set
            {
                // Если передано пустое значение, установим дефис
                if (string.IsNullOrEmpty(value))
                {
                    dateOfManufacture = "-";
                }
                else if (DateTime.TryParseExact(value, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    dateOfManufacture = parsedDate.ToString("dd.MM.yyyy");
                }
                else
                {
                    throw new FormatException("Некорректный формат даты выпуска, используйте формат DD.MM.YYYY.");
                }
            }
        }


        public string FeaturesOfDesignAndColoring
        {
            get { return featuresOfDesignAndColoring; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new FormatException("Поле 'Особенности конструкции' не должно быть пустым.");
                featuresOfDesignAndColoring = value;
            }
        }

        public string DateofLastTechnicalInspection
        {
            get { return dateofLastTechnicalInspection; }
            set
            {
                if (DateTime.TryParseExact(value, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    dateofLastTechnicalInspection = parsedDate.ToString("dd.MM.yyyy");
                }
                else
                {
                    throw new FormatException("Некорректный формат даты последнего техосмотра, используйте формат DD.MM.YYYY.");
                }
            }
        }

        // Constructors
        public Car(string brand, string color, string serialNumber, string sideNumber, string dateOfManufacture, string featuresOfDesignAndColoring, string dateofLastTechnicalInspection)
        {
            Brand = brand; 
            Color = color;
            SerialNumber = serialNumber;
            SideNumber = sideNumber;
            DateOfManufacture = dateOfManufacture;
            FeaturesOfDesignAndColoring = featuresOfDesignAndColoring;
            DateofLastTechnicalInspection = dateofLastTechnicalInspection;
        }

        public Car() { }

        // Methods
        public override string ToString()
        {
            return $"{"Марка:",-30} {brand}\n" +
                   $"{"Цвет:",-30} {color}\n" +
                   $"{"VIN:",-30} {SerialNumber}\n" +
                   $"{"Бортовой номер:",-30} {SideNumber}\n" +
                   $"{"Дата выпуска:",-30} {DateOfManufacture:dd.MM.yyyy}\n" +
                   $"{"Особенности конструкции:",-30} {featuresOfDesignAndColoring}\n" +
                   $"{"Дата последнего техосмотра:",-30} {DateofLastTechnicalInspection}";
        }
    }
}
