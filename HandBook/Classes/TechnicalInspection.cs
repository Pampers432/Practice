using System;
using System.Windows.Forms;

namespace Book.Classes
{
    public class TechnicalInspection
    {
        /// <summary>
        /// Проверяет, требуется ли прохождение техосмотра.
        /// </summary>
        public static void FilterExpiredInspections(DataGridView dgv)
        {
            // Проверяем, что DataGridView не пустой
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Таблица пуста.");
                return;
            }

            // Индексы столбцов "Техосмотр" и "Дата выпуска" (столбцы под номерами 8 и 6)
            int inspectionColumnIndex = 7;  // Столбец "Техосмотр" (по индексу 7)
            int manufactureColumnIndex = 5; // Столбец "Дата выпуска" (по индексу 5)

            // Список строк, у которых срок техосмотра вышел
            var rowsToKeep = dgv.Rows.Cast<DataGridViewRow>()
                .Where(row =>
                {
                    DateTime inspectionDate;
                    DateTime manufactureDate;

                    // Получаем значение даты последнего техосмотра из столбца "Техосмотр"
                    bool isInspectionDateValid = DateTime.TryParse(row.Cells[inspectionColumnIndex].Value?.ToString(), out inspectionDate);

                    // Если дата техосмотра некорректна, пытаемся использовать дату выпуска
                    if (!isInspectionDateValid)
                    {
                        // Получаем значение даты выпуска из столбца "Дата выпуска"
                        if (DateTime.TryParse(row.Cells[manufactureColumnIndex].Value?.ToString(), out manufactureDate))
                        {
                            inspectionDate = manufactureDate.AddYears(2); // Первый техосмотр через 2 года после выпуска
                        }
                        else
                        {
                            // Если обе даты некорректны, строка не подходит
                            return false;
                        }
                    }

                    // Рассчитываем дату следующего техосмотра
                    DateTime nextInspectionDate = inspectionDate.AddYears(2);

                    // Возвращаем true, если срок техосмотра вышел
                    return nextInspectionDate < DateTime.Now;
                })
                .ToList();

            // Очищаем DataGridView
            dgv.Rows.Clear();

            // Добавляем только строки с истёкшим сроком техосмотра
            foreach (var row in rowsToKeep)
            {
                int index = dgv.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dgv.Rows[index].Cells[cell.ColumnIndex].Value = cell.Value;
                }
            }

            // Выводим сообщение, если ни одной строки не осталось
            if (rowsToKeep.Count == 0)
            {
                MessageBox.Show("Нет записей с истёкшим сроком техосмотра.");
            }
        }        
    }
}
