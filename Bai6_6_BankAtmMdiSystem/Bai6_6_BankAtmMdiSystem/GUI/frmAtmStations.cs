using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Bai6_6_BankAtmMdiSystem.BUS;
using Bai6_6_BankAtmMdiSystem.DTO;

namespace Bai6_6_BankAtmMdiSystem.GUI
{
    public partial class frmAtmStations : Form
    {
        private readonly BankAtmBUS bus = new BankAtmBUS();
        private string? currentImagePath;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AutoLoadDatabase { get; set; } = true;

        public frmAtmStations()
        {
            InitializeComponent();
        }

        private void frmAtmStations_Load(object? sender, EventArgs e)
        {
            cboTrangThai.Items.AddRange(new object[] { "Hoạt động", "Hết tiền", "Bảo trì", "Mất kết nối" });
            cboTrangThai.SelectedIndex = 0;

            SetupGrid();
            if (AutoLoadDatabase)
            {
                LoadStations();
            }
        }

        private void SetupGrid()
        {
            dgvTramAtm.AutoGenerateColumns = false;
            dgvTramAtm.Columns.Clear();
            dgvTramAtm.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaTram", HeaderText = "Mã trạm", Width = 110 });
            dgvTramAtm.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DiaChiDatMay", HeaderText = "Địa chỉ đặt máy", Width = 310 });
            var colTonQuy = new DataGridViewTextBoxColumn { DataPropertyName = "SoTienTonQuy", HeaderText = "Số tiền tồn quỹ (VNĐ)", Width = 170 };
            colTonQuy.DefaultCellStyle.Format = "N0";
            colTonQuy.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvTramAtm.Columns.Add(colTonQuy);
            dgvTramAtm.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TrangThaiHoatDong", HeaderText = "Trạng thái hoạt động", Width = 140 });
            dgvTramAtm.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "AnhHienTruong", HeaderText = "Ảnh hiện trường", Width = 190 });
        }

        private void LoadStations(string? keyword = null)
        {
            try
            {
                dgvTramAtm.DataSource = null;
                dgvTramAtm.DataSource = bus.GetAllStations(keyword);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách trạm ATM: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvTramAtm_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvTramAtm.SelectedRows.Count > 0 && dgvTramAtm.SelectedRows[0].DataBoundItem is TramAtmDTO atm)
            {
                txtMaTram.Text = atm.MaTram;
                txtDiaChi.Text = atm.DiaChiDatMay;
                txtTonQuy.Text = atm.SoTienTonQuy.ToString("G0");
                cboTrangThai.SelectedItem = atm.TrangThaiHoatDong;
                currentImagePath = atm.AnhHienTruong;
                DisplayImageSafely(currentImagePath);
            }
        }

        private void DisplayImageSafely(string? path)
        {
            if (picAnhTram.Image != null)
            {
                Image old = picAnhTram.Image;
                picAnhTram.Image = null;
                old.Dispose();
            }

            if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
            {
                try
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    using (Image temp = Image.FromStream(fs))
                    {
                        picAnhTram.Image = new Bitmap(temp);
                    }
                }
                catch
                {
                    picAnhTram.Image = null;
                }
            }
        }

        private bool ReadAndValidate(out TramAtmDTO atm)
        {
            errorProvider1.Clear();
            atm = new TramAtmDTO();
            bool valid = true;

            if (string.IsNullOrWhiteSpace(txtMaTram.Text))
            {
                errorProvider1.SetError(txtMaTram, "Mã trạm ATM không được để trống!");
                valid = false;
            }
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                errorProvider1.SetError(txtDiaChi, "Địa chỉ đặt máy không được để trống!");
                valid = false;
            }
            if (!decimal.TryParse(txtTonQuy.Text.Trim().Replace(",", ""), out decimal tonQuy) || tonQuy < 0)
            {
                errorProvider1.SetError(txtTonQuy, "Số tiền tồn quỹ phải là số không âm!");
                valid = false;
            }

            if (valid)
            {
                atm.MaTram = txtMaTram.Text.Trim().ToUpper();
                atm.DiaChiDatMay = txtDiaChi.Text.Trim();
                atm.SoTienTonQuy = tonQuy;
                atm.TrangThaiHoatDong = cboTrangThai.SelectedItem?.ToString() ?? "Hoạt động";
                atm.AnhHienTruong = currentImagePath;
            }
            return valid;
        }

        private void btnChonAnh_Click(object? sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn ảnh chụp hiện trường trạm ATM";
                ofd.Filter = "Hình ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|Tất cả (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    currentImagePath = ofd.FileName;
                    DisplayImageSafely(currentImagePath);
                }
            }
        }

        private void btnXoaAnh_Click(object? sender, EventArgs e)
        {
            currentImagePath = null;
            DisplayImageSafely(null);
        }

        private void btnAdd_Click(object? sender, EventArgs e)
        {
            if (!ReadAndValidate(out TramAtmDTO atm)) return;
            try
            {
                if (bus.InsertStation(atm))
                {
                    LoadStations();
                    MessageBox.Show($"Thêm mới trạm [{atm.MaTram}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi thêm trạm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnUpdate_Click(object? sender, EventArgs e)
        {
            if (!ReadAndValidate(out TramAtmDTO atm)) return;
            try
            {
                if (bus.UpdateStation(atm))
                {
                    LoadStations();
                    MessageBox.Show($"Cập nhật trạm [{atm.MaTram}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy mã trạm cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi cập nhật: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            string ma = txtMaTram.Text.Trim();
            if (string.IsNullOrEmpty(ma)) return;
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa trạm ATM [{ma}]?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (bus.DeleteStation(ma))
                    {
                        LoadStations();
                        ClearInputs();
                        MessageBox.Show("Đã xóa trạm ATM thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex) { MessageBox.Show($"Lỗi xóa trạm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnClear_Click(object? sender, EventArgs e) => ClearInputs();

        private void ClearInputs()
        {
            errorProvider1.Clear();
            txtMaTram.Clear();
            txtDiaChi.Clear();
            txtTonQuy.Clear();
            if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;
            currentImagePath = null;
            DisplayImageSafely(null);
            txtMaTram.Focus();
        }

        private void btnSearch_Click(object? sender, EventArgs e) => LoadStations(txtSearch.Text);
        private void btnReload_Click(object? sender, EventArgs e) { txtSearch.Clear(); LoadStations(); }
    }
}
