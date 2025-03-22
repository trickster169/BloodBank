using BloodBank.BLL;
using BloodBank.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBank.UI
{
    public partial class frmDonors : Form
    {
        public frmDonors()
        {
            InitializeComponent();
        }
        //Create object of Donor BLL and Donor DAL
        donorsBLL d = new donorsBLL();
        donorsDAL dal = new donorsDAL();
        userDAL udal = new userDAL();
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //Close this form
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //We will write the code to Add new Donor
            //Step 1. Get the DAta from Manage Donors Form
            d.first_name = txtFirstName.Text;
            d.last_name = txtLastName.Text;
            d.email = txtEmail.Text;
            d.gender = cmbGender.Text;
            d.blood_group = cmbBloodGroup.Text;
            d.contact = txtContact.Text;
            d.address = txtAddress.Text;
            d.added_date = DateTime.Now;

            //Get The ID of Logged In USer
            string loggedInUser = frmLogin.loggedInUser;
            userBLL usr = udal.GetIDFromUsername(loggedInUser);

            d.added_by = usr.user_id;

            //Step2: Inserting Data into Database
            //Create a Boolean Variable to Isnert DAta into DAtabase and check whether the data inserted successfully of not
            bool isSuccess = dal.Insert(d);

            //if the Data is inserted successfully then the values of isSuccess will be True else it will be false
            if(isSuccess==true)
            {
                //Data Inserted Successfully
                MessageBox.Show("New Donor Added Successfully");

                //Refresh Datagrid View
                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;

                //Clear all the Textboxes
                Clear();
            }
            else
            {
                //FAiled to Insert Data
                MessageBox.Show("Failed to Add new Donor.");
            }
        }

        //Create a Method to Clear all the Textboxes
        public void Clear()
        {
            //Clear all the TExtboxes
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtDonorId.Text = "";
            cmbGender.Text = "";
            cmbBloodGroup.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            
        }

        private void dgvDonors_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //SElect the DAta from DAtagrid View and Display in our Form

            //1. Find the Row Selected
            int RowIndex = e.RowIndex;

            txtDonorId.Text = dgvDonors.Rows[RowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvDonors.Rows[RowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvDonors.Rows[RowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvDonors.Rows[RowIndex].Cells[3].Value.ToString();
            txtContact.Text = dgvDonors.Rows[RowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvDonors.Rows[RowIndex].Cells[5].Value.ToString();
            txtAddress.Text = dgvDonors.Rows[RowIndex].Cells[6].Value.ToString();
            cmbBloodGroup.Text = dgvDonors.Rows[RowIndex].Cells[7].Value.ToString();
        }

        private void frmDonors_Load(object sender, EventArgs e)
        {
            //Display Donors in DataGrid View
            DataTable dt = dal.Select();
            dgvDonors.DataSource = dt;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Add the Functionality to Update the Donors
            //1. Get the Values from Form
            d.donor_id = int.Parse(txtDonorId.Text);
            d.first_name = txtFirstName.Text;
            d.last_name = txtLastName.Text;
            d.email = txtEmail.Text;
            d.gender = cmbGender.Text;
            d.blood_group = cmbBloodGroup.Text;
            d.contact = txtContact.Text;
            d.address = txtAddress.Text;
            //Get The ID of Logged In USer
            string loggedInUser = frmLogin.loggedInUser;
            userBLL usr = udal.GetIDFromUsername(loggedInUser);

            d.added_by = usr.user_id;

            //Create a Boolean Variable to Check whether the data updated successfully or not
            bool isSuccess = dal.Update(d);

            //If the data updated successfully then the value of isSuccess will be true else it will be false
            if (isSuccess == true)
            {
                //Donor Updated Successfully
                MessageBox.Show("Donor updated Successfully.");
                Clear();

                //Refresh Datagridview
                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;

            }
            else
            {
                //Failed to Update
                MessageBox.Show("Failed to update donors.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get the value from form
            d.donor_id = int.Parse(txtDonorId.Text);

            //Create a Boolean Variable to Check whether the donor deleted or not
            bool isSuccess = dal.Delete(d);

            if(isSuccess==true)
            {
                //Donor Deleted Successfully
                MessageBox.Show("Donor Deleted Successfully.");

                Clear();

                //Refresh Datagrid View
                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;
            }
            else
            {
                //Failed to Delete Donor
                MessageBox.Show("Failed to Delete Donor");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Clear the TExtboxes
            Clear();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Let's Add the Dunctionality to Search the Donors

            //1. Get the Keywords Typed on the Search TExt Box
            string keywords = txtSearch.Text;

            // Check Whether the Search TExtBox is Empty or Not
            if(keywords != null)
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
