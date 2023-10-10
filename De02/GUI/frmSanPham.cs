using BUS;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUI
{
    public partial class frmSanPham : Form
    {
        private readonly SanPhamBUS sanPhamBUS = new SanPhamBUS();
        private readonly LoaiSPBUS loaiSPBUS = new LoaiSPBUS();
        public frmSanPham()
        {
            InitializeComponent();
        }

        private void FillCombobox(List<LoaiSP> listLoaiSP)
        {
            this.cmbLoaiSP.DataSource = listLoaiSP;
            this.cmbLoaiSP.DisplayMember = "TenLoai";
            this.cmbLoaiSP.ValueMember = "MaLoai";

        }

        private void BindListView(List<SanPham> listSP)
        {
            lvSanPham.Items.Clear();
            foreach (var item in listSP)
            {
                ListViewItem lvi = lvSanPham.Items.Add(item.MaSP);
                lvi.SubItems.Add(item.TenSP);
                lvi.SubItems.Add(item.NgayNhap.ToShortDateString());
                lvi.SubItems.Add(item.LoaiSP.TenLoai.ToString());
                
            }
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            try
            {
                var listSP = sanPhamBUS.GetAll();
                var listLoai = loaiSPBUS.GetAll();
                BindListView(listSP);
                FillCombobox(listLoai);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void frmSanPham_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No) 
            {
                e.Cancel = true;
            }
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
            //else return;
        }

        private void lvSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lvSanPham.SelectedItems.Count > 0)
                {
                    ListViewItem lvi = lvSanPham.SelectedItems[0];
                    txtMaSP.Text = lvi.SubItems[0].Text;
                    txtTenSP.Text = lvi.SubItems[1].Text;
                    dtNgayNhap.Text = lvi.SubItems[2].Text;
                    cmbLoaiSP.Text = lvi.SubItems[3].Text;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var listSP = sanPhamBUS.GetAll();
                var listLoai = loaiSPBUS.GetAll();

                string ma = txtMaSP.Text.ToString();
                string ten = txtTenSP.Text.ToString();
                DateTime ngay = dtNgayNhap.Value;
                var loai = listLoai.FirstOrDefault(l=>l.TenLoai == cmbLoaiSP.Text);

                var sp = new SanPham
                {
                    MaSP = ma,
                    TenSP = ten,
                    NgayNhap = ngay,
                    MaLoai = loai.MaLoai
                };

                sanPhamBUS.InsertUpdate(sp);
                BindListView(sanPhamBUS.GetAll());
                MessageBox.Show("Thêm sản phẩm thành công");
            }
            catch ( Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var listSP = sanPhamBUS.GetAll();
                var listLoai = loaiSPBUS.GetAll();

                string ma = txtMaSP.Text.ToString();
                string ten = txtTenSP.Text.ToString();
                DateTime ngay = dtNgayNhap.Value;
                var loai = listLoai.FirstOrDefault(l => l.TenLoai == cmbLoaiSP.Text);

                var sp = new SanPham
                {
                    MaSP = ma,
                    TenSP = ten,
                    NgayNhap = ngay,
                    MaLoai = loai.MaLoai
                };

                sanPhamBUS.InsertUpdate(sp);
                BindListView(sanPhamBUS.GetAll());
                MessageBox.Show("Cập nhật sản phẩm thành công");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void ClearTextBox()
        {
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            dtNgayNhap.Value = DateTime.Now;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var listSP = sanPhamBUS.GetAll();
                var sanPham = listSP.FirstOrDefault(emp => emp.MaSP == txtMaSP.Text.ToString());
                if (sanPham != null)
                {
                    DialogResult dr = MessageBox.Show("Bạn có muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        sanPhamBUS.DeleteSanPham(sanPham);
                        frmSanPham_Load(sender, e);
                        MessageBox.Show("Xóa sản phẩm thành công");
                        ClearTextBox();

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        

        private void btnFind_Click(object sender, EventArgs e)
        {
           
            string data = txtFind.Text.Trim().ToLower();
            foreach (ListViewItem item in lvSanPham.Items)
            {
                if (!item.SubItems[1].ToString().ToLower().Contains(data))
                {
                    lvSanPham.Items.Remove(item);
                }
            }
     
        }

    private void btnLoad_Click(object sender, EventArgs e)
        {
            frmSanPham_Load(sender, e);
        }

    }
}
