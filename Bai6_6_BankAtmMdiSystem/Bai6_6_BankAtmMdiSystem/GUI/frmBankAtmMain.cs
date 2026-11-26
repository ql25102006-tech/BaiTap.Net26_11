using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Bai6_6_BankAtmMdiSystem.BUS;
using Bai6_6_BankAtmMdiSystem.DTO;

namespace Bai6_6_BankAtmMdiSystem.GUI
{
    public partial class frmBankAtmMain : Form
    {
        private readonly BankAtmBUS bus = new BankAtmBUS();
        private GiaoDichVienDTO currentTeller = new GiaoDichVienDTO
        {
            MaGDV = 1,
            TenDangNhap = "gdv01",
            HoTen = "Lê Ngọc Quyền",
            CaTruc = "Ca Sáng (07:30 - 15:30)"
        };

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AutoLoadDatabase { get; set; } = true;

        public frmBankAtmMain()
        {
            InitializeComponent();
        }

        private void frmBankAtmMain_Load(object? sender, EventArgs e)
        {
            UpdateClock();
            UpdateTellerStatus();
            if (AutoLoadDatabase)
            {
                CheckCoreConnection();
            }
        }

        private void CheckCoreConnection()
        {
            try
            {
                bus.GetAllStations();
                sslKetNoiCore.Text = "Core Banking: Đã kết nối SQL Server (BankAtmMdiDB)";
            }
            catch (Exception ex)
            {
                sslKetNoiCore.Text = $"Core Banking: Chưa kết nối ({ex.Message})";
            }
        }

        private void UpdateTellerStatus()
        {
            if (string.IsNullOrEmpty(currentTeller.TenDangNhap))
            {
                sslGiaoDichVien.Text = "Giao dịch viên trực: (Chưa đăng nhập)";
                menuQuanLyAtm.Enabled = false;
                menuCuaSo.Enabled = false;
            }
            else
            {
                sslGiaoDichVien.Text = $"Giao dịch viên trực: {currentTeller.HoTen} ({currentTeller.TenDangNhap})";
                menuQuanLyAtm.Enabled = true;
                menuCuaSo.Enabled = true;
            }
        }

        public void UpdateReconciliationProgress(int percent, string coreStatusText)
        {
            if (IsDisposed || !IsHandleCreated) return;
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => UpdateReconciliationProgress(percent, coreStatusText)));
                return;
            }

            int clamped = Math.Max(tspbTienTrinh.Minimum, Math.Min(tspbTienTrinh.Maximum, percent));
            tspbTienTrinh.Value = clamped;
            if (!string.IsNullOrWhiteSpace(coreStatusText))
            {
                sslKetNoiCore.Text = coreStatusText;
            }
        }

        /// <summary>
        /// Điều hướng MDI chuẩn mực: duyệt qua this.MdiChildren, nếu đã tồn tại thì Activate(), ngược lại tạo mới.
        /// </summary>
        public T OpenOrActivateChildForm<T>(Func<T> factory) where T : Form
        {
            foreach (Form child in this.MdiChildren)
            {
                if (child is T existing)
                {
                    if (existing.WindowState == FormWindowState.Minimized)
                        existing.WindowState = FormWindowState.Normal;
                    existing.Activate();
                    return existing;
                }
            }

            T newForm = factory();
            newForm.MdiParent = this;
            newForm.Show();
            return newForm;
        }

        private void menuDanhSachTram_Click(object? sender, EventArgs e)
        {
            OpenOrActivateChildForm(() => new frmAtmStations { AutoLoadDatabase = this.AutoLoadDatabase });
        }

        private void menuDoiSoat_Click(object? sender, EventArgs e)
        {
            OpenOrActivateChildForm(() => new frmAtmReconciliation { AutoLoadDatabase = this.AutoLoadDatabase });
        }

        private void menuXepTang_Click(object? sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void menuXepNgang_Click(object? sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void menuXepDoc_Click(object? sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void menuDongTatCa_Click(object? sender, EventArgs e)
        {
            foreach (Form child in this.MdiChildren)
            {
                child.Close();
            }
        }

        private void menuDangXuat_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất phiên làm việc hiện tại không?", "Xác nhận Đăng xuất",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            foreach (Form child in this.MdiChildren)
            {
                child.Close();
            }

            currentTeller = new GiaoDichVienDTO();
            UpdateTellerStatus();

            ShowLoginOrUnlockDialog(isLockScreen: false);
        }

        private void menuKhoaManHinh_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentTeller.TenDangNhap)) return;
            ShowLoginOrUnlockDialog(isLockScreen: true);
        }

        private void ShowLoginOrUnlockDialog(bool isLockScreen)
        {
            using (Form dlg = new Form())
            {
                dlg.Text = isLockScreen ? "Khóa màn hình - Nhập mật khẩu mở khóa" : "Đăng nhập Hệ thống Điều hành ATM";
                dlg.ClientSize = new Size(380, 190);
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.MaximizeBox = false;
                dlg.MinimizeBox = false;

                Label lblUser = new Label { Text = "Tên đăng nhập:", Location = new Point(20, 25), AutoSize = true };
                TextBox txtUser = new TextBox
                {
                    Location = new Point(145, 22),
                    Size = new Size(205, 27),
                    Text = isLockScreen ? currentTeller.TenDangNhap : "gdv01",
                    ReadOnly = isLockScreen
                };

                Label lblPass = new Label { Text = "Mật khẩu:", Location = new Point(20, 65), AutoSize = true };
                TextBox txtPass = new TextBox
                {
                    Location = new Point(145, 62),
                    Size = new Size(205, 27),
                    UseSystemPasswordChar = true,
                    Text = "123456"
                };

                Label lblHint = new Label
                {
                    Text = "Tài khoản mẫu: gdv01 / 123456",
                    Location = new Point(145, 96),
                    AutoSize = true
                };

                Button btnOk = new Button
                {
                    Text = isLockScreen ? "Mở khóa" : "Đăng nhập",
                    Location = new Point(145, 130),
                    Size = new Size(100, 34),
                    UseVisualStyleBackColor = true
                };
                Button btnCancel = new Button
                {
                    Text = "Hủy",
                    Location = new Point(255, 130),
                    Size = new Size(95, 34),
                    UseVisualStyleBackColor = true
                };

                btnOk.Click += (s, ev) =>
                {
                    string u = txtUser.Text.Trim();
                    string p = txtPass.Text.Trim();
                    try
                    {
                        GiaoDichVienDTO? found = null;
                        try
                        {
                            found = bus.DangNhap(u, p);
                        }
                        catch
                        {
                            if (u == "gdv01" && p == "123456")
                            {
                                found = new GiaoDichVienDTO { MaGDV = 1, TenDangNhap = "gdv01", HoTen = "Lê Ngọc Quyền", CaTruc = "Ca Sáng" };
                            }
                        }

                        if (found != null)
                        {
                            currentTeller = found;
                            UpdateTellerStatus();
                            dlg.DialogResult = DialogResult.OK;
                            dlg.Close();
                        }
                        else
                        {
                            MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                };

                btnCancel.Click += (s, ev) =>
                {
                    dlg.DialogResult = DialogResult.Cancel;
                    dlg.Close();
                };

                dlg.AcceptButton = btnOk;
                dlg.CancelButton = btnCancel;
                dlg.Controls.AddRange(new Control[] { lblUser, txtUser, lblPass, txtPass, lblHint, btnOk, btnCancel });
                dlg.ShowDialog(this);
            }
        }

        private void menuThoat_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void UpdateClock()
        {
            sslClock.Text = "Thời gian: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void timerClock_Tick(object? sender, EventArgs e)
        {
            UpdateClock();
        }
    }
}
