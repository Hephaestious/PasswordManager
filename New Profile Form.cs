﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordManager
{
    public partial class NewProfileForm : Form
    {
        private string Phrase = "";
        private string ProfileName = "";
        private string ProfilePassword = "";
        private Database.DBProfile newProfile;
        public Profile NewProfile { get { return (newProfile == null) ? null : new Profile(newProfile); } }
        public NewProfileForm()
        {
            InitializeComponent();
            newProfile = null;
            Phrase = Database.GeneratePhrase();
            phraseTextBox.Text = Phrase;
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(profileNameInput.Text) && !string.IsNullOrEmpty(profilePasswordInput.Text))
            {
                if (!Database.IsProfile(profileNameInput.Text))
                {
                    ProfileName = profileNameInput.Text;
                    ProfilePassword = profilePasswordInput.Text;
                    tabControl1.SelectTab(tabPage4);
                }
                else
                {
                    MessageBox.Show("A profile by that name already exists.");
                }
            }
            else
            {
                MessageBox.Show("You must enter a profile name and password. You won't need these to recover your passwords.");
            }
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(phraseConfirmTextBox.Text))
            {
                if (phraseConfirmTextBox.Text == Phrase)
                {
                    newProfile = Database.CreateProfile(ProfileName, ProfilePassword, Phrase);
                    MessageBox.Show("Created profile");
                    Close();
                }
                else
                {
                    MessageBox.Show("You entered an invalid phrase");
                    tabControl1.SelectTab(tabPage3);
                }
            }
            else
            {
                MessageBox.Show("You must input a phrase");
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage3);
        }
    }
}
