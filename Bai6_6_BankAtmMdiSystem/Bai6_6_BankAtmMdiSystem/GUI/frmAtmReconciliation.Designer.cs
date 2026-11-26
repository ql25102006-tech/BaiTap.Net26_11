namespace Bai6_6_BankAtmMdiSystem.GUI
{
    partial class frmAtmReconciliation
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
            grpControl = new GroupBox();
            lblNgay = new Label();
            dtpNgayDoiSoat = new DateTimePicker();
            btnChayDoiSoat = new Button();
            btnDungDoiSoat = new Button();
            lblTrangThai = new Label();
            prgDoiSoat = new ProgressBar();
            splitBottom = new SplitContainer();
            grpChiTiet = new GroupBox();
            lstLogDoiSoat = new ListBox();
            grpLichSu = new GroupBox();
            dgvLichSuDoiSoat = new DataGridView();
            grpControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitBottom).BeginInit();
            splitBottom.Panel1.SuspendLayout();
            splitBottom.Panel2.SuspendLayout();
            splitBottom.SuspendLayout();
            grpChiTiet.SuspendLayout();
            grpLichSu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLichSuDoiSoat).BeginInit();
            SuspendLayout();

            // grpControl
            grpControl.Controls.AddRange(new Control[] { lblNgay, dtpNgayDoiSoat, btnChayDoiSoat, btnDungDoiSoat, lblTrangThai, prgDoiSoat });
            grpControl.Dock = DockStyle.Top;
            grpControl.Location = new Point(8, 8);
            grpControl.Name = "grpControl";
            grpControl.Size = new Size(944, 125);
            grpControl.Text = "Đối soát Sao kê Giao dịch ATM Cuối ngày (Async/Await & CancellationTokenSource)";

            lblNgay.AutoSize = true; lblNgay.Location = new Point(15, 32); lblNgay.Text = "Ngày đối soát:";
            dtpNgayDoiSoat.Format = DateTimePickerFormat.Custom; dtpNgayDoiSoat.CustomFormat = "dd/MM/yyyy"; dtpNgayDoiSoat.Location = new Point(125, 29); dtpNgayDoiSoat.Size = new Size(140, 27);

            btnChayDoiSoat.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnChayDoiSoat.Location = new Point(285, 25);
            btnChayDoiSoat.Size = new Size(215, 35);
            btnChayDoiSoat.Text = "Chạy Đối Soát Tự Động";
            btnChayDoiSoat.UseVisualStyleBackColor = true;
            btnChayDoiSoat.Click += btnChayDoiSoat_Click;

            btnDungDoiSoat.Enabled = false;
            btnDungDoiSoat.Location = new Point(515, 25);
            btnDungDoiSoat.Size = new Size(160, 35);
            btnDungDoiSoat.Text = "Dừng Đối Soát";
            btnDungDoiSoat.UseVisualStyleBackColor = true;
            btnDungDoiSoat.Click += btnDungDoiSoat_Click;

            lblTrangThai.AutoSize = true;
            lblTrangThai.Location = new Point(15, 72);
            lblTrangThai.Text = "Trạng thái: Sẵn sàng chạy đối soát cuối ngày (Không gây đơ giao diện MDI)";

            prgDoiSoat.Location = new Point(15, 96);
            prgDoiSoat.Size = new Size(910, 20);
            prgDoiSoat.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // splitBottom
            splitBottom.Dock = DockStyle.Fill;
            splitBottom.Location = new Point(8, 133);
            splitBottom.Orientation = Orientation.Horizontal;
            splitBottom.SplitterDistance = 190;
            splitBottom.Panel1.Controls.Add(grpChiTiet);
            splitBottom.Panel2.Controls.Add(grpLichSu);

            grpChiTiet.Controls.Add(lstLogDoiSoat);
            grpChiTiet.Dock = DockStyle.Fill;
            grpChiTiet.Padding = new Padding(6);
            grpChiTiet.Text = "Nhật ký xử lý bất đồng bộ theo từng lô giao dịch";
            lstLogDoiSoat.Dock = DockStyle.Fill;
            lstLogDoiSoat.HorizontalScrollbar = true;

            grpLichSu.Controls.Add(dgvLichSuDoiSoat);
            grpLichSu.Dock = DockStyle.Fill;
            grpLichSu.Padding = new Padding(6);
            grpLichSu.Text = "Lịch sử các Phiên Đối Soát đã hoàn tất (CSDL BankAtmMdiDB)";

            dgvLichSuDoiSoat.AllowUserToAddRows = false;
            dgvLichSuDoiSoat.AllowUserToDeleteRows = false;
            dgvLichSuDoiSoat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLichSuDoiSoat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLichSuDoiSoat.Dock = DockStyle.Fill;
            dgvLichSuDoiSoat.ReadOnly = true;
            dgvLichSuDoiSoat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(960, 560);
            Controls.Add(splitBottom);
            Controls.Add(grpControl);
            Name = "frmAtmReconciliation";
            Padding = new Padding(8);
            Text = "Đối soát Giao dịch Cuối ngày (Form Con 2 - Async/Await)";
            Load += frmAtmReconciliation_Load;
            FormClosing += frmAtmReconciliation_FormClosing;
            grpControl.ResumeLayout(false);
            grpControl.PerformLayout();
            splitBottom.Panel1.ResumeLayout(false);
            splitBottom.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitBottom).EndInit();
            splitBottom.ResumeLayout(false);
            grpChiTiet.ResumeLayout(false);
            grpLichSu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLichSuDoiSoat).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpControl, grpChiTiet, grpLichSu;
        private Label lblNgay, lblTrangThai;
        private DateTimePicker dtpNgayDoiSoat;
        private Button btnChayDoiSoat, btnDungDoiSoat;
        private ProgressBar prgDoiSoat;
        private SplitContainer splitBottom;
        private ListBox lstLogDoiSoat;
        private DataGridView dgvLichSuDoiSoat;
    }
}
