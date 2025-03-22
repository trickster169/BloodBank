using BloodBank.DAL;
using BloodBank.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBank
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        //Create the Object of Donor Dal
        donorsDAL dal = new donorsDAL();

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            //Load all the Blood Donors Count When Form is Loaded
            //Call allDonorCountMethod
            allDonorCount();

            //Display all the Donors
            DataTable dt = dal.Select();
            dgvDonors.DataSource = dt;

            //Display the username of Logged In user
            lblUser.Text = frmLogin.loggedInUser;
        }

        public void allDonorCount()
        {
            //Get the Donor Count from DAtabase and SEt in respective label
            lblOpositiveCount.Text = dal.countDonors("O+");
            lblOnegativeCount.Text = dal.countDonors("O-");
            lblApositiveCount.Text = dal.countDonors("A+");
            lblAnegativeCount.Text = dal.countDonors("A-");
            lblBpositiveCount.Text = dal.countDonors("B+");
            lblBnegativeCount.Text = dal.countDonors("B-");
            lblABpositiveCount.Text = dal.countDonors("AB+");
            lblABnegativeCount.Text = dal.countDonors("AB-");
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsers users = new frmUsers();
            users.ShowDialog();
        }

        private void donorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDonors donors = new frmDonors();
            donors.ShowDialog();
        }

        private void frmHome_Activated(object sender, EventArgs e)
        {
            //Call allDonorCount Method
            allDonorCount();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Let's Add the Dunctionality to Search the Donors

            //1. Get the Keywords Typed on the Search TExt Box
            string keywords = txtSearch.Text;

            // Check Whether the Search TExtBox is Empty or Not
            if (keywords != null)
            {
                //Display the information of Donors Based on Keywords
                DataTable dt = dal.Search(keywords);
                dgvDonors.DataSource = dt;
            }
            else
            {
                //DIsplay all the Donors
                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;
            }
        }
    }
}
