using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSales
{
    public partial class Product : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        public Product()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            LoadProduct();
        }

        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT  p.Year, p.Color, p.Status, " +
                "p.cid, p.bid, p.DateOfLastMaintenance, p.CurrentMileage, p.LicensePlateNumber, " +
                "p.DatePaymentAssurance, p.DateVidange, p.DateTaxe FROM vehicule AS p INNER JOIN " +
                "tbBrand AS b ON b.id = p.bid" +
                " INNER JOIN categories AS c on c.id = p.cid WHERE" +
                " CONCAT(p.Color, b.Name, c.name) LIKE '%" +txtSearch.Text+ "%'",cn);
            cn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[9].ToString(), dr[10].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductModule productModule = new ProductModule(this);
            productModule.ShowDialog();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "edit")
            {
                ProductModule product = new ProductModule(this);
                product.txtYear.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                product.txtmileage.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                product.txtmaintenance.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                product.ldcategorie.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                product.ldbrand.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                product.txtStatus.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();
                product.UDReOrder.Value = int.Parse(dgvProduct.Rows[e.RowIndex].Cells[7].Value.ToString());

                product.txtYear.Enabled = false;
                product.btnSave.Enabled = false;
                product.btnUpdate.Enabled = true;
                product.ShowDialog();
            }
            else if (colName == "delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM vehicule WHERE VechiculeId LIKE '" + dgvProduct[1, e.RowIndex].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Product has been successfully deleted.", "Point Of Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            LoadProduct();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void Product_Load(object sender, EventArgs e)
        {

        }

        private void txtSearch_Click(object sender, EventArgs e)
        {

        }
    }
}
