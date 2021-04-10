using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Car_accounting.carAccounting.CMD
{
    public class ListOfColors : ComboBox
    {
        public List<string> Colors => new List<string>()
        {
            { "Красный" },
            { "Голубой" },
            { "Черный" },
            { "Синий" },
            { "Желтый" },
            { "Зеленый" },
            { "Серый" },
            { "Фиолетовый" },
            { "Оранжевый" }
        };

        public ListOfColors() : base()
        {
            Font = new Font(Font.Name, 12, FontStyle.Regular);
            DropDownStyle = ComboBoxStyle.DropDownList;
            Items.AddRange(Colors.ToArray());
            SelectedIndex = -1;
        }
    }
}