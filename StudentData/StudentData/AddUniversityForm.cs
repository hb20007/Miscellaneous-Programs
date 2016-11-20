﻿/// <file>
/// <author>Hanna Sababa</author>
/// <datecreated>2016-11-19</datecreated>
/// <summary>
///  Contains the part of the class AddUniversityForm with the non-autogenerated code
/// </summary>
/// </file>
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace StudentData
{
    partial class AddUniversityForm : Form
    {
        /// <summary>
        /// Is set to <code>true</code> if the user entry shows validition errors and then the user presses the add button
        /// </summary>
        private bool incorrectEntryFlag = false;

        public AddUniversityForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Closes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            this.incorrectEntryFlag = false; // Neccessary in case the user sets the flag to true but then decides to cancel adding a new university
        }

        /// <summary>
        /// Adds the university to the .csv file with the list of all universities
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, System.EventArgs e)
        {
            string errorMessage = string.Empty;
            if (this.universityNameTextBox.Text.Trim() == string.Empty)
                errorMessage = "University name cannot be empty.";
            else if (!Utilities.isUniversityNew(this.universityNameTextBox.Text))
                errorMessage = "University already exists in the database.";
            else if (!Regex.IsMatch(this.universityNameTextBox.Text.Trim(), @"^[A-Za-z]+\s[A-Za-z\s]*[A-Za-z]+$"))
                errorMessage = "University name must only contain letters and must be at least two distinct words long.";
            if (errorMessage != string.Empty)
            {
                MessageBox.Show(errorMessage, "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.incorrectEntryFlag = true;
            }
            else
            {
                this.incorrectEntryFlag = false; // Neccessary in case the user sets the flag to true but then fixes the problem
                //The code below doesn't work because it will replace the text in the file
                //StreamWriter writer = new StreamWriter(StudentDataMainForm.UNIVERSITIES_FILE_NAME);
                //writer.Write(',' + this.universityNameTextBox.Text.Trim());
                //writer.Close();
                Utilities.addUniversityToList(this.universityNameTextBox.Text);
                this.Close();
            }
        }

        /// <summary>
        /// Prevents the form from closing if the text box was left empty and if the user pressed the add button as opposed to the cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddUniversityForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.incorrectEntryFlag)
                e.Cancel = true;
        }
    }
}