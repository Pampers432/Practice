using HandBook.Classes;

namespace HandBook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Shown += (sender, args) => InitializeHandBook(); // ������������� ������ ��� ������ �����
        }

        private void InitializeHandBook()
        {
            Car car = new Car(
                1,
                "Toyota",
                "�������",
                "1HGCM82633A123456",
                "AB1234",
                "15.03.2020",
                "�������, ������������� �����",
                "10.06.2023");

            handBook.carsList.Add(car);

            PasportsData ownerDetails = new PasportsData(
                "������ ���� ��������",
                "12.03.1985",
                "1234",
                "567890",
                "20.05.2003",
                "������� ���� �� �. ������"
            );
            handBook.pasportsData.Add(ownerDetails);

          

            MessageBox.Show(car.ToString() + Environment.NewLine + ownerDetails.ToString(), "���������� � ������ � ���������", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
