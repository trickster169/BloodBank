using BloodBank.BLL;
using BloodBank.DAL;
using System;
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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        //Create the Object of BLL and DAL
        loginBLL l = new loginBLL();
        loginDAL dal = new loginDAL();

        //Create a Static String method to save the username
        public static string loggedInUser;

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Write the Code to Login our Application
            //1. Get the username and password from login form
            l.username = txtUsername.Text;
            l.password = txtPassword.Text;

            //Check the Login Credentials
            bool isSuccess = dal.loginCheck(l);

            //Check whehter the login is success or not
            //If login is success then isSuccess will be true else it will be false
            if (isSuccess == true)
            {
                //Login Success
                //Display Success Message
                MessageBox.Show("Login Successful.");

                //Save the username in loggedInuser Stattic MEthod
                loggedInUser = l.username;

                //Display home Form
                frmHome home = new frmHome();
                home.ShowDialog();
                this.Close(); //To CLose Login Form
            }
            else
            {
                //Login Failed
                //Display the Error Message
                MessageBox.Show("Login Failed. Try Again.");
            }
        }
    }
}
