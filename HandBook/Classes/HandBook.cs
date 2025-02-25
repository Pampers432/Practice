using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandBook.Classes
{
    public class handBook
    {
        [Key] // Указываем, что это ключевое поле
        public int Id { get; set; }

        [Required] // Поле обязательно
        public Car car { get; set; } = null!;

        [Required]
        public PasportsData data { get; set; } = null!;

        public handBook(int Id, Car car, PasportsData data)
        {
            this.Id = Id;
            this.car = car;
            this.data = data;
        }

        public handBook() { }
    }
}
