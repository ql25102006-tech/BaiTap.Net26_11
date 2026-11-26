namespace Bai6_6_BankAtmMdiSystem.GUI
{
    partial class frmBankAtmMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            menuStrip1 = new MenuStrip();
            menuHeThong = new ToolStripMenuItem();
            menuDangXuat = new ToolStripMenuItem();
            menuKhoaManHinh = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            menuThoat = new ToolStripMenuItem();
            menuQuanLyAtm = new ToolStripMenuItem();
            menuDanhSachTram = new ToolStripMenuItem();
            menuDoiSoat = new ToolStripMenuItem();
            menuCuaSo = new ToolStripMenuItem();
            menuXepTang = new ToolStripMenuItem();
            menuXepNgang = new ToolStripMenuItem();
            menuXepDoc = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            menuDongTatCa = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            sslGiaoDichVien = new ToolStripStatusLabel();
            sslKetNoiCore = new ToolStripStatusLabel();
            tspbTienTrinh = new ToolStripProgressBar();
            sslClock = new ToolStripStatusLabel();
            timerClock = new System.Windows.Forms.Timer(components);
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();

            // menuStrip1
            menuStrip1.Items.AddRange(new ToolStripItem[] { menuHeThong, menuQuanLyAtm, menuCuaSo });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1180, 28);
            menuStrip1.TabIndex = 0;

            // Hệ thống
            menuHeThong.DropDownItems.AddRange(new ToolStripItem[] { menuDangXuat, menuKhoaManHinh, toolStripSeparator1, menuThoat });
            menuHeThong.Name = "menuHeThong";
            menuHeThong.Text = "Hệ thống";

            menuDangXuat.Name = "menuDangXuat";
            menuDangXuat.Text = "Đăng xuất";
            menuDangXuat.Click += menuDangXuat_Click;

            menuKhoaManHinh.Name = "menuKhoaManHinh";
            menuKhoaManHinh.ShortcutKeys = Keys.Control | Keys.L;
            menuKhoaManHinh.Text = "Khóa màn hình";
            menuKhoaManHinh.Click += menuKhoaManHinh_Click;

            menuThoat.Name = "menuThoat";
            menuThoat.ShortcutKeys = Keys.Alt | Keys.F4;
            menuThoat.Text = "Thoát";
            menuThoat.Click += menuThoat_Click;

            // Quản lý ATM
            menuQuanLyAtm.DropDownItems.AddRange(new ToolStripItem[] { menuDanhSachTram, menuDoiSoat });
            menuQuanLyAtm.Name = "menuQuanLyAtm";
            menuQuanLyAtm.Text = "Quản lý ATM";

            menuDanhSachTram.Name = "menuDanhSachTram";
            menuDanhSachTram.ShortcutKeys = Keys.F2;
            menuDanhSachTram.Text = "Danh sách Trạm ATM & Tồn quỹ";
            menuDanhSachTram.Click += menuDanhSachTram_Click;

            menuDoiSoat.Name = "menuDoiSoat";
            menuDoiSoat.ShortcutKeys = Keys.F3;
            menuDoiSoat.Text = "Đối soát Giao dịch Cuối ngày";
            menuDoiSoat.Click += menuDoiSoat_Click;

            // Cửa sổ
            menuCuaSo.DropDownItems.AddRange(new ToolStripItem[] { menuXepTang, menuXepNgang, menuXepDoc, toolStripSeparator2, menuDongTatCa });
            menuCuaSo.Name = "menuCuaSo";
            menuCuaSo.Text = "Cửa sổ";

            menuXepTang.Name = "menuXepTang";
            menuXepTang.Text = "Xếp tầng (Cascade)";
            menuXepTang.Click += menuXepTang_Click;

            menuXepNgang.Name = "menuXepNgang";
            menuXepNgang.Text = "Xếp ngang (Tile Horizontal)";
            menuXepNgang.Click += menuXepNgang_Click;

            menuXepDoc.Name = "menuXepDoc";
            menuXepDoc.Text = "Xếp dọc (Tile Vertical)";
            menuXepDoc.Click += menuXepDoc_Click;

            menuDongTatCa.Name = "menuDongTatCa";
            menuDongTatCa.Text = "Đóng tất cả";
            menuDongTatCa.Click += menuDongTatCa_Click;

            // statusStrip1
            statusStrip1.Items.AddRange(new ToolStripItem[] { sslGiaoDichVien, sslKetNoiCore, tspbTienTrinh, sslClock });
            statusStrip1.Location = new Point(0, 694);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1180, 26);
            statusStrip1.TabIndex = 1;

            sslGiaoDichVien.BorderSides = ToolStripStatusLabelBorderSides.Right;
            sslGiaoDichVien.Name = "sslGiaoDichVien";
            sslGiaoDichVien.Text = "Giao dịch viên trực: Lê Ngọc Quyền (gdv01)";

            sslKetNoiCore.Name = "sslKetNoiCore";
            sslKetNoiCore.Spring = true;
            sslKetNoiCore.Text = "Core Banking: Sẵn sàng kết nối";
            sslKetNoiCore.TextAlign = ContentAlignment.MiddleLeft;

            tspbTienTrinh.Name = "tspbTienTrinh";
            tspbTienTrinh.Size = new Size(180, 18);
            tspbTienTrinh.Minimum = 0;
            tspbTienTrinh.Maximum = 100;
            tspbTienTrinh.Value = 0;

            sslClock.BorderSides = ToolStripStatusLabelBorderSides.Left;
            sslClock.Name = "sslClock";
            sslClock.Text = "Thời gian: 00:00:00";

            timerClock.Enabled = true;
            timerClock.Interval = 1000;
            timerClock.Tick += timerClock_Tick;

            // frmBankAtmMain
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1180, 720);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(1000, 620);
            Name = "frmBankAtmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bài 6.6: Hệ thống MDI Điều hành Mạng lưới Cây ATM Ngân hàng";
            Load += frmBankAtmMain_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuHeThong;
        private ToolStripMenuItem menuDangXuat;
        private ToolStripMenuItem menuKhoaManHinh;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem menuThoat;
        private ToolStripMenuItem menuQuanLyAtm;
        private ToolStripMenuItem menuDanhSachTram;
        private ToolStripMenuItem menuDoiSoat;
        private ToolStripMenuItem menuCuaSo;
        private ToolStripMenuItem menuXepTang;
        private ToolStripMenuItem menuXepNgang;
        private ToolStripMenuItem menuXepDoc;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem menuDongTatCa;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel sslGiaoDichVien;
        private ToolStripStatusLabel sslKetNoiCore;
        private ToolStripProgressBar tspbTienTrinh;
        private ToolStripStatusLabel sslClock;
        private System.Windows.Forms.Timer timerClock;
    }
}
