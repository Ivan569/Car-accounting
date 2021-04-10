using System;
using System.Drawing;
using System.Windows.Forms;
using Car_accounting.carAccounting.BL;

namespace Car_accounting.carAccounting.CMD
{
    public class AuxiliaryForm : Form
    {
        public Car lastCar = new Car("Не указано", "", DateTime.Now.Year, DateTime.Now, 0);
        public bool ok;
        private readonly TextBox carBrand;
        private readonly ListOfColors listColors;
        private readonly TextBox yearOfRelease;
        private readonly DateTimePicker dateOfSale;
        private readonly TextBox amount;

        public AuxiliaryForm(string title, Car car)
        {
            ok = false;
            Text = title;
            Size = new Size(200, 300);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.Beige;
            Label lbl = new Label();
            lbl.Text = "Марка автомобиля";
            lbl.SetBounds(40, 10, 110, 20);
            Controls.Add(lbl);
            carBrand = new TextBox();
            carBrand.SetBounds(40, 30, 110, 20);
            carBrand.Text += car.CarBrand;
            Controls.Add(carBrand);
            Label lbl2 = new Label();
            lbl2.Text = "Цвет автомобиля";
            lbl2.SetBounds(40, 50, 110, 20);
            Controls.Add(lbl2);
            listColors = new ListOfColors();
            listColors.SetBounds(40, 70, 110, 10);
            listColors.SelectedIndex = Array.IndexOf(listColors.Colors.ToArray(), car.Color);
            Controls.Add(listColors);
            Label lbl3 = new Label();
            lbl3.Text = "Год выпуска";
            lbl3.SetBounds(40, 100, 110, 20);
            Controls.Add(lbl3);
            yearOfRelease = new TextBox();
            yearOfRelease.SetBounds(40, 120, 110, 20);
            yearOfRelease.KeyPress += (x, e) =>
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                    e.Handled = true;
            };
            yearOfRelease.Text = car.YearOfRelease.ToString();
            Controls.Add(yearOfRelease);
            Label lbl4 = new Label();
            lbl4.Text = "Дата продажи";
            lbl4.SetBounds(40, 140, 110, 20);
            Controls.Add(lbl4);
            dateOfSale = new DateTimePicker();
            dateOfSale.Format = DateTimePickerFormat.Custom;
            dateOfSale.SetBounds(40, 160, 110, 20);
            dateOfSale.Text = car.DateOfSale.ToString();
            Controls.Add(dateOfSale);
            Label lbl5 = new Label();
            lbl5.Text = "Сумма";
            lbl5.SetBounds(40, 180, 110, 20);
            Controls.Add(lbl5);
            amount = new TextBox();
            amount.SetBounds(40, 200, 110, 20);
            amount.KeyPress += (x, e) =>
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                    e.Handled = true;
            };
            amount.Text = car.Amount.ToString();
            Controls.Add(amount);
            Button btnOk = new Button();
            btnOk.SetBounds(40, 230, 55, 20);
            btnOk.Text = "ОК";
            btnOk.Click += OnBtnOkClick;
            Controls.Add(btnOk);
            Button btnCancel = new Button();
            btnCancel.SetBounds(95, 230, 55, 20);
            btnCancel.Text = "Отмена";
            btnCancel.Click += (x, y) => Close();
            Controls.Add(btnCancel);
        }

        private void OnBtnOkClick(object obj, EventArgs args)
        {
            lastCar.CarBrand = carBrand.Text;
            lastCar.Color = (string)listColors.SelectedItem;
            if (yearOfRelease.Text == "")
                yearOfRelease.Text = "0";
            lastCar.YearOfRelease = int.Parse(yearOfRelease.Text);
            lastCar.DateOfSale = dateOfSale.Value;
            if (amount.Text == "")
                amount.Text = "0";
            lastCar.Amount = int.Parse(amount.Text);
            ok = true;
            Close();
        }
    }
}