﻿/// <file>
/// <author>Hanna Sababa</author>
/// <datecreated>2016-11-19</datecreated>
/// <summary>
///  Contains the part of the class AllStudentsForm with the non-autogenerated code
/// </summary>
/// </file>
using System;
using System.Windows.Forms;
using System.IO;

namespace StudentData
{
    public partial class AllStudentsForm : Form
    {
        public AllStudentsForm()
        {
            this.InitializeComponent();
            this.loadData();
        }

        /// <summary>
        /// Closes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Loads the students' data into the DataGridView
        /// </summary>
        private void loadData()
        {
            try
            {
                StreamReader reader = new StreamReader(File.OpenRead(Utilities.STUDENTS_FILE_NAME));
                reader.ReadLine(); // The first line is done away with because it has the column headings
                while (!reader.EndOfStream)
                {
                    int rowIndex = this.studentsDataGridView.Rows.Add();
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');
                    for (int i = 0; i < values.Length; i++) // Assigning the CheckBox column simply to the "True" or "False" in the .csv file works
                        this.studentsDataGridView.Rows[rowIndex].Cells[i].Value = values[i];
                }
                reader.Close();
            }
            catch
            {
                MessageBox.Show($"The file {Utilities.STUDENTS_FILE_NAME} was not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1); // Application.Exit() and this.Close() do not work here (because this code runs before this.InitializeComponent() probably)
            }
        }
    }
}