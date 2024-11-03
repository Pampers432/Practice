using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HandBook.Classes
{
    internal class PasportsData
    {
        private string fio { get; set; }
        private string birthDate { get; set; }
        private string series { get; set; }
        private string number { get; set; }
        private string issueDate { get; set; }
        public string issuedBy { get; set; }

        //descriptors
        public string FIO
        {
            get { return fio; }
            set
            {
                string fioPattern = @"^[А-ЯЁ][а-яё]+\s[А-ЯЁ][а-яё]+\s[А-ЯЁ][а-яё]+$";
                if (Regex.IsMatch(value, fioPattern))
                {
                    fio = value;
                }
                else
                {
                    throw new FormatException("Некорректный формат ФИО, используйте формат 'Фамилия Имя Отчество' с заглавными буквами.");
                }
            }
        }

        public string BirthDate
        {
            get { return birthDate; }
            set
            {
                if (DateTime.TryParseExact(value, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    birthDate = parsedDate.ToString("dd.MM.yyyy");
                }
                else
                {
                    throw new FormatException("Некорректный формат даты рождения, используйте формат DD.MM.YYYY.");
                }
            }
        }

        public string Series
        {
            get { return series; }
            set
            {
                string seriesPattern = @"^\d{4}$";
                if (Regex.IsMatch(value, seriesPattern))
                {
                    series = value;
                }
                else
                {
                    throw new FormatException("Некорректная серия паспорта, используйте формат NNNN.");
                }
            }
        }

        public string Number
        {
            get { return number; }
            set
            {
                string numberPattern = @"^\d{6}$";
                if (Regex.IsMatch(value, numberPattern))
                {
                    number = value;
                }
                else
                {
                    throw new FormatException("Некорректный номер паспорта, используйте формат NNNNNN.");
                }
            }
        }

        public string IssueDate
        {
            get { return issueDate; }
            set
            {
                if (DateTime.TryParseExact(value, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    issueDate = parsedDate.ToString("dd.MM.yyyy");
                }
                else
                {
                    throw new FormatException("Некорректный формат даты выдачи, используйте формат DD.MM.YYYY.");
                }
            }
        }

        // Constructors
        public PasportsData(string fio, string birthDate, string series, string number, string issueDate, string issuedBy)
        {
            FIO = fio;
            BirthDate = birthDate;
            Series = series;
            Number = number;
            IssueDate = issueDate;
            this.issuedBy = issuedBy;
        }

        // Methods
        public override string ToString()
        {
            return $"{"ФИО:",-30} {FIO}\n" +
                   $"{"Дата рождения:",-30} {BirthDate}\n" +
                   $"{"Серия:",-30} {Series}\n" +
                   $"{"Номер:",-30} {Number}\n" +
                   $"{"Дата выдачи:",-30} {IssueDate}\n" +
                   $"{"Кем выдан:",-30} {issuedBy}";
        }

    }
}
