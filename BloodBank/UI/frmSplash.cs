﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBank.UI
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        int move = 0;

        private void timerSplash_Tick(object sender, EventArgs e)
        {
            //Write the code to show Loading Animation
            timerSplash.Interval = 20;
            panelMovable.Width += 10;

            move += 10;

            //If the loading is complete then display login form and close this form
            if (move == 700)
            {
                //Stop the Timer and Close this Form
                timerSplash.Stop();
                

                //Display the Login Form
                frmLogin login = new frmLogin();
                login.ShowDialog();
                this.Close();
            }
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {

            //Load the Timer
            timerSplash.Start();
        }
    }
}
