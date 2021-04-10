using System;
using System.IO;
using System.Drawing;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Collections.Generic;
using Car_accounting.carAccounting.BL;

namespace Car_accounting.carAccounting.CMD
{
    public class MainForm : Form
    {
        private readonly ListBox carsSold;
        private readonly List<Car> listCarsSold;
        private DataBase dataBase;

        public MainForm(string title)
        {
            Text = title;
            Size = new Size(500, 200);            
            FormClosed += MainForm_FormClosed;
            MainMenu menu = new MainMenu();
            MenuItem files = new MenuItem("Файлы");
            files.MenuItems.Add("Загрузить", OnButtonOpenClick);
            files.MenuItems.Add("Сохранить", OnButtonSaveClick);
            MenuItem edit = new MenuItem("Редактировать");
            edit.MenuItems.Add("Создать", OnButtonCreateClick);
            edit.MenuItems.Add("Редактировать", OnButtonEditClick);
            edit.MenuItems.Add("Удалить", OnButtonDeleteClick);
            menu.MenuItems.Add(files);
            menu.MenuItems.Add(edit);
            Menu = menu;
            carsSold = new ListBox();
            carsSold.BackColor = Color.Beige;
            carsSold.BorderStyle = BorderStyle.Fixed3D;
            carsSold.Size = new Size(1920, 1080);
            Controls.Add(carsSold);
            listCarsSold = new List<Car>();
        }

        private void MainForm_FormClosed(object obj, FormClosedEventArgs args)
        {
            if (dataBase != null)
                dataBase.myConnection.Close();
        }

        private void OnButtonOpenClick(object obj, EventArgs args)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "(*.mdb)|*.mdb";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fileInf = new FileInfo(openFileDialog.FileName);
                if (fileInf.Exists)
                {
                    fileInf.CopyTo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Cars.mdb"), true);
                    dataBase = new DataBase();
                    dataBase.myConnection = new OleDbConnection(dataBase.connectString);
                    dataBase.myConnection.Open();
                    OleDbCommand dbCommand = new OleDbCommand("SELECT w_CarBrand, w_Color, w_YearOfRelease, w_DateOfSale, w_Amount FROM Cars ORDER BY w_id", dataBase.myConnection);
                    OleDbDataReader reader = dbCommand.ExecuteReader();
                    carsSold.Items.Clear();
                    listCarsSold.Clear();

                    while(reader.Read())
                    {
                        carsSold.Items.Add(reader[0].ToString() + "-" + reader[4].ToString());
                        listCarsSold.Add(new Car(reader[0].ToString(), reader[1].ToString(), int.Parse(reader[2].ToString()), DateTime.Parse(reader[3].ToString()), double.Parse(reader[4].ToString())));
                    }

                    reader.Close();
                    dataBase.myConnection.Close();
                }
            }
        }

        private void OnButtonSaveClick(object obj, EventArgs args)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "(*.mdb)|*.mdb";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string dbFileName = saveFileDialog.FileName;
                saveFileDialog.Reset();
                dataBase = new DataBase();
                dataBase.myConnection = new OleDbConnection(dataBase.connectString);
                dataBase.myConnection.Open();

                for (int i = 0; i < listCarsSold.Count; i++)
                {
                    string query = "INSERT INTO Cars (w_CarBrand, w_Color, w_YearOfRelease, w_DateOfSale, w_Amount) VALUES (";
                    query += @"'" + listCarsSold[i].CarBrand + @"', '" + listCarsSold[i].Color + @"', " + listCarsSold[i].YearOfRelease + @",'" + listCarsSold[i].DateOfSale.ToString().Split(' ')[0] + @"', " + listCarsSold[i].Amount + ")";
                    OleDbCommand dbCommand = new OleDbCommand(query, dataBase.myConnection);
                    if (i == 0)
                        new OleDbCommand("DELETE FROM Cars WHERE w_id", dataBase.myConnection).ExecuteNonQuery();
                    dbCommand.ExecuteNonQuery();
                }

                dataBase.myConnection.Close();
                FileInfo fileInf = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Cars.mdb"));
                if (fileInf.Exists)
                    fileInf.CopyTo(dbFileName, true);
            }          
        }

        private void OnButtonCreateClick(object obj, EventArgs args)
        {
            AuxiliaryForm auxiliaryForm = new AuxiliaryForm("Create", new Car("", "", DateTime.Now.Year, DateTime.Now, 0));
            auxiliaryForm.ShowDialog();
            if (auxiliaryForm.ok)
            {
                listCarsSold.Add(auxiliaryForm.lastCar);
                carsSold.Items.Add(auxiliaryForm.lastCar.ToString());
                carsSold.SelectedIndex = listCarsSold.Count - 1;
            }
        }

        private void OnButtonEditClick(object obj, EventArgs args)
        {
            if (listCarsSold.Count != 0)
            {
                AuxiliaryForm auxiliaryForm = new AuxiliaryForm("Edit", listCarsSold[carsSold.SelectedIndex]);
                auxiliaryForm.ShowDialog();
                if (auxiliaryForm.ok)
                {
                    listCarsSold[carsSold.SelectedIndex] = auxiliaryForm.lastCar;
                    carsSold.Items[carsSold.SelectedIndex] = auxiliaryForm.lastCar;
                }
            }
        }

        private void OnButtonDeleteClick(object obj, EventArgs args)
        {
            if (carsSold.SelectedIndex != -1)
            {
                listCarsSold.RemoveAt(carsSold.SelectedIndex);
                carsSold.Items.RemoveAt(carsSold.SelectedIndex);
            }
        }
    }
}