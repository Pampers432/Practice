using HandBook.Classes;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HandBook
{
    public partial class Form1 : Form
    {
        private int currentId = 0; // Для хранения текущего максимального ID

        public Form1()
        {
            InitializeComponent();
            this.Shown += (sender, args) =>
            {
                InitializeHandBook(); // Инициализация данных
                LoadDataFromDatabase(); // Загрузка данных из БД
                UpdateCurrentId(); // Обновление currentId после загрузки данных
            };
        }

        private void InitializeHandBook()
        {
            //using (var context = new AppDbContext())
            //{
            //    var newHandBook = new handBook
            //    {
            //        car = new Car
            //        {
            //            Brand = "Toyota",
            //            Color = "Красный",
            //            SerialNumber = "1HGCM82633A123456",
            //            SideNumber = "AB1234",
            //            DateOfManufacture = "15.03.2020",
            //            FeaturesOfDesignAndColoring = "Metallic",
            //            DateofLastTechnicalInspection = "10.06.2023"
            //        },
            //        data = new PasportsData
            //        {
            //            FIO = "Иванов Иван Иванович",
            //            BirthDate = "12.03.1985",
            //            Series = "1234",
            //            Number = "567890",
            //            IssueDate = "20.05.2003",
            //            IssuedBy = "ГУ МВД"
            //        }
            //    };

            //    context.HandBooks.Add(newHandBook);
            //    context.SaveChanges();
            //}
        }

        private void LoadDataFromDatabase()
        {
            using (var context = new AppDbContext())
            {
                var handBooks = context.HandBooks
                    .Include(h => h.car) // Загрузка связанных данных Car
                    .Include(h => h.data) // Загрузка связанных данных PasportsData
                    .ToList();

                dataGridView1.Rows.Clear(); // Очищаем DataGridView перед добавлением данных

                foreach (var handBook in handBooks)
                {
                    dataGridView1.Rows.Add(
                        handBook.Id,
                        handBook.car.Brand,
                        handBook.car.Color,
                        handBook.car.SerialNumber,
                        handBook.car.SideNumber,
                        handBook.car.DateOfManufacture,
                        handBook.car.FeaturesOfDesignAndColoring,
                        handBook.car.DateofLastTechnicalInspection,
                        handBook.data.FIO,
                        handBook.data.BirthDate,
                        handBook.data.Series,
                        handBook.data.Number,
                        handBook.data.IssueDate,
                        handBook.data.IssuedBy
                    );
                }
            }
        }

        private void UpdateCurrentId()
        {
            using (var context = new AppDbContext())
            {
                // Получаем максимальный ID из базы данных
                var maxId = context.HandBooks.Max(h => (int?)h.Id) ?? 0;
                currentId = maxId; // Если записей нет, currentId будет 0
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Обновляем currentId на основе данных из базы
                UpdateCurrentId();
                currentId++; // Увеличиваем ID на 1

                // Получаем значения из текстовых полей
                DateTime dateOfManufacture = DateTime.Parse(textBox5.Text.Trim());
                DateTime dateOfLastTechnicalInspection = DateTime.Parse(textBox7.Text.Trim());

                // Проверка, что дата техосмотра больше даты производства
                if (dateOfLastTechnicalInspection < dateOfManufacture)
                {
                    MessageBox.Show("Дата последнего техосмотра должна быть не меньше даты выпуска автомобиля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Создаём объекты для записи в базу данных
                Car car = new Car(
                    textBox1.Text.Trim(),
                    textBox2.Text.Trim(),
                    textBox3.Text.Trim(),
                    textBox4.Text.Trim(),
                    textBox5.Text.Trim(),
                    textBox6.Text.Trim(),
                    textBox7.Text.Trim()
                );

                PasportsData pasportsData = new PasportsData(
                    textBox8.Text.Trim(),
                    textBox9.Text.Trim(),
                    textBox10.Text.Trim(),
                    textBox11.Text.Trim(),
                    textBox12.Text.Trim(),
                    textBox13.Text.Trim()
                );

                var handbook = new handBook(currentId, car, pasportsData);

                // Добавляем новую запись в базу данных
                using (var context = new AppDbContext())
                {
                    context.HandBooks.Add(handbook);
                    context.SaveChanges();
                }

                // Добавляем данные в DataGridView
                dataGridView1.Rows.Add(
                    handbook.Id,
                    car.Brand,
                    car.Color,
                    car.SerialNumber,
                    car.SideNumber,
                    car.DateOfManufacture,
                    car.FeaturesOfDesignAndColoring,
                    car.DateofLastTechnicalInspection,
                    pasportsData.FIO,
                    pasportsData.BirthDate,
                    pasportsData.Series,
                    pasportsData.Number,
                    pasportsData.IssueDate,
                    pasportsData.IssuedBy
                );
            }
            catch (FormatException ex)
            {
                // Вывод сообщения о некорректных данных
                MessageBox.Show($"Ошибка ввода: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Общий обработчик на случай других ошибок
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    // Получаем первую выбранную строку
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    // Получаем ID записи из выбранной строки
                    int id = Convert.ToInt32(selectedRow.Cells[0].Value);

                    // Получаем значения из текстовых полей
                    DateTime dateOfManufacture = DateTime.Parse(textBox5.Text.Trim());
                    DateTime dateOfLastTechnicalInspection = DateTime.Parse(textBox7.Text.Trim());

                    // Проверка, что дата техосмотра больше даты производства
                    if (dateOfLastTechnicalInspection < dateOfManufacture)
                    {
                        MessageBox.Show("Дата последнего техосмотра должна быть не меньше даты выпуска автомобиля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Создаём объекты для обновления данных
                    Car car = new Car(
                        textBox1.Text.Trim(),
                        textBox2.Text.Trim(),
                        textBox3.Text.Trim(),
                        textBox4.Text.Trim(),
                        textBox5.Text.Trim(),
                        textBox6.Text.Trim(),
                        textBox7.Text.Trim()
                    );

                    PasportsData pasportsData = new PasportsData(
                        textBox8.Text.Trim(),
                        textBox9.Text.Trim(),
                        textBox10.Text.Trim(),
                        textBox11.Text.Trim(),
                        textBox12.Text.Trim(),
                        textBox13.Text.Trim()
                    );

                    // Обновляем данные в базе данных
                    using (var context = new AppDbContext())
                    {
                        // Находим запись в БД по ID
                        var handBook = context.HandBooks
                            .Include(h => h.car)  // Включаем связанные данные Car
                            .Include(h => h.data) // Включаем связанные данные PasportsData
                            .FirstOrDefault(h => h.Id == id);

                        if (handBook != null)
                        {
                            // Обновляем данные в объекте из БД
                            handBook.car.Brand = car.Brand;
                            handBook.car.Color = car.Color;
                            handBook.car.SerialNumber = car.SerialNumber;
                            handBook.car.SideNumber = car.SideNumber;
                            handBook.car.DateOfManufacture = car.DateOfManufacture;
                            handBook.car.FeaturesOfDesignAndColoring = car.FeaturesOfDesignAndColoring;
                            handBook.car.DateofLastTechnicalInspection = car.DateofLastTechnicalInspection;

                            handBook.data.FIO = pasportsData.FIO;
                            handBook.data.BirthDate = pasportsData.BirthDate;
                            handBook.data.Series = pasportsData.Series;
                            handBook.data.Number = pasportsData.Number;
                            handBook.data.IssueDate = pasportsData.IssueDate;
                            handBook.data.IssuedBy = pasportsData.IssuedBy;

                            // Сохраняем изменения в базе
                            context.SaveChanges();
                        }
                        else
                        {
                            MessageBox.Show("Запись для обновления не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Обновляем данные в DataGridView
                    selectedRow.Cells[1].Value = car.Brand;
                    selectedRow.Cells[2].Value = car.Color;
                    selectedRow.Cells[3].Value = car.SerialNumber;
                    selectedRow.Cells[4].Value = car.SideNumber;
                    selectedRow.Cells[5].Value = car.DateOfManufacture;
                    selectedRow.Cells[6].Value = car.FeaturesOfDesignAndColoring;
                    selectedRow.Cells[7].Value = car.DateofLastTechnicalInspection;
                    selectedRow.Cells[8].Value = pasportsData.FIO;
                    selectedRow.Cells[9].Value = pasportsData.BirthDate;
                    selectedRow.Cells[10].Value = pasportsData.Series;
                    selectedRow.Cells[11].Value = pasportsData.Number;
                    selectedRow.Cells[12].Value = pasportsData.IssueDate;
                    selectedRow.Cells[13].Value = pasportsData.IssuedBy;

                    // Уведомляем пользователя об успешном обновлении
                    MessageBox.Show("Данные успешно обновлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (FormatException ex)
                {
                    // Выводим сообщение об ошибке в случае некорректных данных
                    MessageBox.Show($"Ошибка ввода: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    // Общий обработчик на случай других ошибок
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Если ни одна строка не выбрана, выводим предупреждение
                MessageBox.Show("Выберите строку для обновления.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Проверяем, есть ли выбранные строки
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Берём первую выбранную строку
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Проверяем наличие достаточного количества столбцов и заносим данные в TextBox
                textBox1.Text = selectedRow.Cells[1].Value?.ToString() ?? ""; // Столбец 1
                textBox2.Text = selectedRow.Cells[2].Value?.ToString() ?? ""; // Столбец 2
                textBox3.Text = selectedRow.Cells[3].Value?.ToString() ?? ""; // Столбец 3
                textBox4.Text = selectedRow.Cells[4].Value?.ToString() ?? ""; // Столбец 4
                textBox5.Text = selectedRow.Cells[5].Value?.ToString() ?? ""; // Столбец 5
                textBox6.Text = selectedRow.Cells[6].Value?.ToString() ?? ""; // Столбец 6
                textBox7.Text = selectedRow.Cells[7].Value?.ToString() ?? ""; // Столбец 7
                textBox8.Text = selectedRow.Cells[8].Value?.ToString() ?? ""; // Столбец 8
                textBox9.Text = selectedRow.Cells[9].Value?.ToString() ?? ""; // Столбец 9
                textBox10.Text = selectedRow.Cells[10].Value?.ToString() ?? ""; // Столбец 10
                textBox11.Text = selectedRow.Cells[11].Value?.ToString() ?? ""; // Столбец 11
                textBox12.Text = selectedRow.Cells[12].Value?.ToString() ?? ""; // Столбец 12
                textBox13.Text = selectedRow.Cells[13].Value?.ToString() ?? ""; // Столбец 13
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    // Получаем первую выбранную строку
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    // Получаем ID записи из выбранной строки
                    int id = Convert.ToInt32(selectedRow.Cells[0].Value);

                    // Удаляем запись из базы данных
                    using (var context = new AppDbContext())
                    {
                        // Находим объект по ID
                        var handBook = context.HandBooks
                            .Include(h => h.car)
                            .Include(h => h.data)
                            .FirstOrDefault(h => h.Id == id);

                        if (handBook != null)
                        {
                            context.HandBooks.Remove(handBook); // Удаляем запись
                            context.SaveChanges(); // Сохраняем изменения в БД
                        }
                        else
                        {
                            MessageBox.Show("Запись не найдена в базе данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Удаляем строку из DataGridView
                    dataGridView1.Rows.Remove(selectedRow);

                    // Уведомляем пользователя об успешном удалении
                    MessageBox.Show("Запись успешно удалена из базы данных и интерфейса.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Общий обработчик на случай ошибок
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Если ни одна строка не выбрана, выводим предупреждение
                MessageBox.Show("Выберите строку для удаления.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // Фильтруем DataGridView, оставляя только строки с истёкшим сроком техосмотра
                Book.Classes.TechnicalInspection.FilterExpiredInspections(dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверяем, что строка в DataGridView выбрана
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Извлекаем ФИО из выбранной строки (столбец с индексом 8)
                    string fullName = dataGridView1.SelectedRows[0].Cells[8].Value?.ToString();
                    if (string.IsNullOrWhiteSpace(fullName))
                    {
                        MessageBox.Show("ФИО не найдено в выбранной строке.");
                        return;
                    }

                    // Создаём текст приглашения
                    string invitationText = $"Уважаемый {fullName},\n\n" +
                        "В связи с вышедшим сроком прохождения технического осмотра, " +
                        "приглашаем вас пройти обязательную проверку технического состояния вашего транспортного средства. " +
                        "Это необходимо для обеспечения безопасности эксплуатации и соблюдения всех нормативных требований.";

                    // Создание приложения Word через позднее связывание
                    Type wordType = Type.GetTypeFromProgID("Word.Application");
                    dynamic wordApp = Activator.CreateInstance(wordType);

                    // Создание нового документа
                    dynamic document = wordApp.Documents.Add();
                    document.Content.Text = invitationText;

                    // Сохранение документа
                    string folderPath = @"D:\TRPO\Practice\HandBook\Invations";
                    string fileName = System.IO.Path.Combine(folderPath, $"{fullName}_Приглашение_на_техосмотр.docx");

                    // Проверяем, существует ли папка. Если нет — создаём.
                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        System.IO.Directory.CreateDirectory(folderPath);
                    }

                    // Сохраняем файл
                    document.SaveAs2(fileName);

                    // Закрытие документа и приложения
                    document.Close();
                    wordApp.Quit();

                    MessageBox.Show($"Документ успешно создан и сохранён в:\n{fileName}");
                }
                else
                {
                    MessageBox.Show("Выберите строку в таблице для создания документа.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }


        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // Очищаем все строки в DataGridView
                dataGridView1.Rows.Clear();

                // Вызываем метод для загрузки данных из базы данных
                LoadDataFromDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при очистке или загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 13; i++)
            {
                var textBox = this.Controls["textBox" + i.ToString()] as TextBox;
                if (textBox != null)
                {
                    textBox.Clear();
                }
            }
        }

    }
}
