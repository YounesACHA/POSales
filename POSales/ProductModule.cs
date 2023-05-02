using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace POSales
{
    public partial class ProductModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        string stitle = "Point Of Sales";
        Product product;
        public ProductModule(Product pd)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            product = pd;
            LoadBrand();
            LoadCategory();
        }

        public void LoadCategory()
        {
            ldbrand.Items.Clear();
            ldbrand.DataSource = dbcon.getTable("SELECT * FROM categories");
            ldbrand.DisplayMember = "name";
            ldbrand.ValueMember = "id";
        }

        public void LoadBrand()
        {
            ldcategorie.Items.Clear();
            ldcategorie.DataSource = dbcon.getTable("SELECT * FROM tbBrand");
            ldcategorie.DisplayMember = "Name";
            ldcategorie.ValueMember = "id";
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void Clear()
        {
            txtYear.Clear();
            txtmileage.Clear();
            txtmaintenance.Clear();
            txtStatus.Clear();
            ldcategorie.SelectedIndex = 0;
            ldbrand.SelectedIndex = 0;
            UDReOrder.Value = 1;

            txtYear.Enabled = true;
            txtYear.Focus();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                if (MessageBox.Show("Are you sure want to save this product?", "Save Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO vehicule" +
                        " (Year, Color, Status, cid, bid, " +
                        "DateOfLastMaintenance, CurrentMileage, LicensePlateNumber," +
                        " DatePaymentAssurance, DateVidange, DateTaxe)VALUES " +
                        "(@Year,@barcode,@pdesc,@bid" +
                        ",@cid,@price, @reorder)", cn);
                    cm.Parameters.AddWithValue("@Year", txtYear.Text);
                    cm.Parameters.AddWithValue("@mileage", txtmileage.Text);
                    cm.Parameters.AddWithValue("@maintenance", txtmaintenance.Text);
                    cm.Parameters.AddWithValue("@bid", ldcategorie.SelectedValue);
                    cm.Parameters.AddWithValue("@cid", ldbrand.SelectedValue);
                    cm.Parameters.AddWithValue("@price", double.Parse(txtStatus.Text));
                    cm.Parameters.AddWithValue("@reorder", UDReOrder.Value);
                    cn.Open();
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Product has been successfully saved.", stitle);
                    Clear();
                    product.LoadProduct();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to update this product?", "Update Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbProduct SET barcode=@barcode,pdesc=@pdesc,bid=@bid,cid=@cid,price=@price, reorder=@reorder WHERE Year LIKE @Year", cn);
                    cm.Parameters.AddWithValue("@Year", txtYear.Text);
                    cm.Parameters.AddWithValue("@barcode", txtmileage.Text);
                    cm.Parameters.AddWithValue("@pdesc", txtmaintenance.Text);
                    cm.Parameters.AddWithValue("@bid", ldcategorie.SelectedValue);
                    cm.Parameters.AddWithValue("@cid", ldbrand.SelectedValue);
                    cm.Parameters.AddWithValue("@price", double.Parse(txtStatus.Text));
                    cm.Parameters.AddWithValue("@reorder", UDReOrder.Value);
                    cn.Open();
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Product has been successfully updated.", stitle);
                    Clear();
                    this.Dispose();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void ProductModule_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void ProductModule_Load(object sender, EventArgs e)
        {

        }

        private void txtPcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboBrand_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
