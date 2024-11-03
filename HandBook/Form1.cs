using HandBook.Classes;

namespace HandBook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Shown += (sender, args) => InitializeHandBook(); // Инициализация данных при показе формы
        }

        private void InitializeHandBook()
        {
            Car car = new Car(
                1,
                "Toyota",
                "Красный",
                "1HGCM82633A123456",
                "AB1234",
                "15.03.2020",
                "Спойлер, легкосплавные диски",
                "10.06.2023");

            handBook.carsList.Add(car);

            PasportsData ownerDetails = new PasportsData(
                "Иванов Иван Иванович",
                "12.03.1985",
                "1234",
                "567890",
                "20.05.2003",
                "Отделом УФМС по г. Москве"
            );
            handBook.pasportsData.Add(ownerDetails);

          

            MessageBox.Show(car.ToString() + Environment.NewLine + ownerDetails.ToString(), "Информация о машине и владельце", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
