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
        public string serialNumber { get; set; }
        public string sideNumber { get; set; }
        public string dateOfManufacture { get; set; }
        public string featuresOfDesignAndColoring { get; set; }
        public string dateofLastTechnicalInspection { get; set; }
        public string ownersPassportDetails { get; set; }

        public string SerialNumber
        {
            get {  return serialNumber; }
            set
            {
                string vinPattern = @"^[A-HJ-NPR-Z0-9]{17}$";
                while (true)
                {
                    if (Regex.IsMatch(value, vinPattern))
                    {
                        serialNumber = value;
                        break;
                    }
                    else
                    {
                        throw new FormatException("Некорректный заводской номер, попробуйте ещё раз.");
                    }
                }
                
            }
        }

        public string SideNumber
        {
            get { return sideNumber; }
            set
            {
                while (true)
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
        }
        public override string ToString()
        {
            return String.Format($"ID: {Id}, Марка: {brand}, Цвет: {color}, VIN: {serialNumber}, Бортовой номер: {sideNumber}, Дата выпуска: {dateOfManufacture}, Особенности конструкции: {featuresOfDesignAndColoring}, Дата последнего техосмотра: {dateofLastTechnicalInspection}, Паспортные данные владельца: {ownersPassportDetails}");
        }
    }
}
