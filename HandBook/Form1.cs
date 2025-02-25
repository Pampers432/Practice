using HandBook.Classes;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HandBook
{
    public partial class Form1 : Form
    {
        private int currentId = 0; // ��� �������� �������� ������������� ID

        public Form1()
        {
            InitializeComponent();
            this.Shown += (sender, args) =>
            {
                InitializeHandBook(); // ������������� ������
                LoadDataFromDatabase(); // �������� ������ �� ��
                UpdateCurrentId(); // ���������� currentId ����� �������� ������
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
            //            Color = "�������",
            //            SerialNumber = "1HGCM82633A123456",
            //            SideNumber = "AB1234",
            //            DateOfManufacture = "15.03.2020",
            //            FeaturesOfDesignAndColoring = "Metallic",
            //            DateofLastTechnicalInspection = "10.06.2023"
            //        },
            //        data = new PasportsData
            //        {
            //            FIO = "������ ���� ��������",
            //            BirthDate = "12.03.1985",
            //            Series = "1234",
            //            Number = "567890",
            //            IssueDate = "20.05.2003",
            //            IssuedBy = "�� ���"
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
                    .Include(h => h.car) // �������� ��������� ������ Car
                    .Include(h => h.data) // �������� ��������� ������ PasportsData
                    .ToList();

                dataGridView1.Rows.Clear(); // ������� DataGridView ����� ����������� ������

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
                // �������� ������������ ID �� ���� ������
                var maxId = context.HandBooks.Max(h => (int?)h.Id) ?? 0;
                currentId = maxId; // ���� ������� ���, currentId ����� 0
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // ��������� currentId �� ������ ������ �� ����
                UpdateCurrentId();
                currentId++; // ����������� ID �� 1

                // �������� �������� �� ��������� �����
                DateTime dateOfManufacture = DateTime.Parse(textBox5.Text.Trim());
                DateTime dateOfLastTechnicalInspection = DateTime.Parse(textBox7.Text.Trim());

                // ��������, ��� ���� ���������� ������ ���� ������������
                if (dateOfLastTechnicalInspection < dateOfManufacture)
                {
                    MessageBox.Show("���� ���������� ���������� ������ ���� �� ������ ���� ������� ����������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ������ ������� ��� ������ � ���� ������
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

                // ��������� ����� ������ � ���� ������
                using (var context = new AppDbContext())
                {
                    context.HandBooks.Add(handbook);
                    context.SaveChanges();
                }

                // ��������� ������ � DataGridView
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
                // ����� ��������� � ������������ ������
                MessageBox.Show($"������ �����: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // ����� ���������� �� ������ ������ ������
                MessageBox.Show($"��������� ������: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    // �������� ������ ��������� ������
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    // �������� ID ������ �� ��������� ������
                    int id = Convert.ToInt32(selectedRow.Cells[0].Value);

                    // �������� �������� �� ��������� �����
                    DateTime dateOfManufacture = DateTime.Parse(textBox5.Text.Trim());
                    DateTime dateOfLastTechnicalInspection = DateTime.Parse(textBox7.Text.Trim());

                    // ��������, ��� ���� ���������� ������ ���� ������������
                    if (dateOfLastTechnicalInspection < dateOfManufacture)
                    {
                        MessageBox.Show("���� ���������� ���������� ������ ���� �� ������ ���� ������� ����������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // ������ ������� ��� ���������� ������
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

                    // ��������� ������ � ���� ������
                    using (var context = new AppDbContext())
                    {
                        // ������� ������ � �� �� ID
                        var handBook = context.HandBooks
                            .Include(h => h.car)  // �������� ��������� ������ Car
                            .Include(h => h.data) // �������� ��������� ������ PasportsData
                            .FirstOrDefault(h => h.Id == id);

                        if (handBook != null)
                        {
                            // ��������� ������ � ������� �� ��
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

                            // ��������� ��������� � ����
                            context.SaveChanges();
                        }
                        else
                        {
                            MessageBox.Show("������ ��� ���������� �� �������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // ��������� ������ � DataGridView
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

                    // ���������� ������������ �� �������� ����������
                    MessageBox.Show("������ ������� ���������.", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (FormatException ex)
                {
                    // ������� ��������� �� ������ � ������ ������������ ������
                    MessageBox.Show($"������ �����: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    // ����� ���������� �� ������ ������ ������
                    MessageBox.Show($"��������� ������: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // ���� �� ���� ������ �� �������, ������� ��������������
                MessageBox.Show("�������� ������ ��� ����������.", "��������������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // ���������, ���� �� ��������� ������
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // ���� ������ ��������� ������
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // ��������� ������� ������������ ���������� �������� � ������� ������ � TextBox
                textBox1.Text = selectedRow.Cells[1].Value?.ToString() ?? ""; // ������� 1
                textBox2.Text = selectedRow.Cells[2].Value?.ToString() ?? ""; // ������� 2
                textBox3.Text = selectedRow.Cells[3].Value?.ToString() ?? ""; // ������� 3
                textBox4.Text = selectedRow.Cells[4].Value?.ToString() ?? ""; // ������� 4
                textBox5.Text = selectedRow.Cells[5].Value?.ToString() ?? ""; // ������� 5
                textBox6.Text = selectedRow.Cells[6].Value?.ToString() ?? ""; // ������� 6
                textBox7.Text = selectedRow.Cells[7].Value?.ToString() ?? ""; // ������� 7
                textBox8.Text = selectedRow.Cells[8].Value?.ToString() ?? ""; // ������� 8
                textBox9.Text = selectedRow.Cells[9].Value?.ToString() ?? ""; // ������� 9
                textBox10.Text = selectedRow.Cells[10].Value?.ToString() ?? ""; // ������� 10
                textBox11.Text = selectedRow.Cells[11].Value?.ToString() ?? ""; // ������� 11
                textBox12.Text = selectedRow.Cells[12].Value?.ToString() ?? ""; // ������� 12
                textBox13.Text = selectedRow.Cells[13].Value?.ToString() ?? ""; // ������� 13
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    // �������� ������ ��������� ������
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    // �������� ID ������ �� ��������� ������
                    int id = Convert.ToInt32(selectedRow.Cells[0].Value);

                    // ������� ������ �� ���� ������
                    using (var context = new AppDbContext())
                    {
                        // ������� ������ �� ID
                        var handBook = context.HandBooks
                            .Include(h => h.car)
                            .Include(h => h.data)
                            .FirstOrDefault(h => h.Id == id);

                        if (handBook != null)
                        {
                            context.HandBooks.Remove(handBook); // ������� ������
                            context.SaveChanges(); // ��������� ��������� � ��
                        }
                        else
                        {
                            MessageBox.Show("������ �� ������� � ���� ������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // ������� ������ �� DataGridView
                    dataGridView1.Rows.Remove(selectedRow);

                    // ���������� ������������ �� �������� ��������
                    MessageBox.Show("������ ������� ������� �� ���� ������ � ����������.", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // ����� ���������� �� ������ ������
                    MessageBox.Show($"��������� ������: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // ���� �� ���� ������ �� �������, ������� ��������������
                MessageBox.Show("�������� ������ ��� ��������.", "��������������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // ��������� DataGridView, �������� ������ ������ � ������� ������ ����������
                Book.Classes.TechnicalInspection.FilterExpiredInspections(dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // ���������, ��� ������ � DataGridView �������
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // ��������� ��� �� ��������� ������ (������� � �������� 8)
                    string fullName = dataGridView1.SelectedRows[0].Cells[8].Value?.ToString();
                    if (string.IsNullOrWhiteSpace(fullName))
                    {
                        MessageBox.Show("��� �� ������� � ��������� ������.");
                        return;
                    }

                    // ������ ����� �����������
                    string invitationText = $"��������� {fullName},\n\n" +
                        "� ����� � �������� ������ ����������� ������������ �������, " +
                        "���������� ��� ������ ������������ �������� ������������ ��������� ������ ������������� ��������. " +
                        "��� ���������� ��� ����������� ������������ ������������ � ���������� ���� ����������� ����������.";

                    // �������� ���������� Word ����� ������� ����������
                    Type wordType = Type.GetTypeFromProgID("Word.Application");
                    dynamic wordApp = Activator.CreateInstance(wordType);

                    // �������� ������ ���������
                    dynamic document = wordApp.Documents.Add();
                    document.Content.Text = invitationText;

                    // ���������� ���������
                    string folderPath = @"D:\TRPO\Practice\HandBook\Invations";
                    string fileName = System.IO.Path.Combine(folderPath, $"{fullName}_�����������_��_���������.docx");

                    // ���������, ���������� �� �����. ���� ��� � ������.
                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        System.IO.Directory.CreateDirectory(folderPath);
                    }

                    // ��������� ����
                    document.SaveAs2(fileName);

                    // �������� ��������� � ����������
                    document.Close();
                    wordApp.Quit();

                    MessageBox.Show($"�������� ������� ������ � ������� �:\n{fileName}");
                }
                else
                {
                    MessageBox.Show("�������� ������ � ������� ��� �������� ���������.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������: {ex.Message}");
            }
        }


        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // ������� ��� ������ � DataGridView
                dataGridView1.Rows.Clear();

                // �������� ����� ��� �������� ������ �� ���� ������
                LoadDataFromDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������ ��� ������� ��� �������� ������: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
