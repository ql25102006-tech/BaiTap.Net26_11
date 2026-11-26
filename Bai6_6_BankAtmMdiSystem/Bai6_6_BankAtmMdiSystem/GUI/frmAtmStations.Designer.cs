namespace Bai6_6_BankAtmMdiSystem.GUI
{
    partial class frmAtmStations
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
            grpInput = new GroupBox();
            lblMaTram = new Label();
            txtMaTram = new TextBox();
            lblDiaChi = new Label();
            txtDiaChi = new TextBox();
            lblTonQuy = new Label();
            txtTonQuy = new TextBox();
            lblTrangThai = new Label();
            cboTrangThai = new ComboBox();
            lblAnh = new Label();
            picAnhTram = new PictureBox();
            btnChonAnh = new Button();
            btnXoaAnh = new Button();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            grpSearch = new GroupBox();
            lblSearch = new Label();
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnReload = new Button();
            dgvTramAtm = new DataGridView();
            errorProvider1 = new ErrorProvider(components);
            grpInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAnhTram).BeginInit();
            grpSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTramAtm).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();

            // grpInput
            grpInput.Controls.AddRange(new Control[]
            {
                lblMaTram, txtMaTram, lblDiaChi, txtDiaChi, lblTonQuy, txtTonQuy,
                lblTrangThai, cboTrangThai, lblAnh, picAnhTram, btnChonAnh, btnXoaAnh,
                btnAdd, btnUpdate, btnDelete, btnClear
            });
            grpInput.Dock = DockStyle.Top;
            grpInput.Location = new Point(8, 8);
            grpInput.Name = "grpInput";
            grpInput.Size = new Size(964, 185);
            grpInput.TabIndex = 0;
            grpInput.TabStop = false;
            grpInput.Text = "Thông tin Trạm ATM & Ảnh chụp hiện trường";

            lblMaTram.AutoSize = true; lblMaTram.Location = new Point(15, 32); lblMaTram.Text = "Mã trạm ATM:";
            txtMaTram.Location = new Point(130, 29); txtMaTram.Size = new Size(170, 27);

            lblTrangThai.AutoSize = true; lblTrangThai.Location = new Point(320, 32); lblTrangThai.Text = "Trạng thái:";
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList; cboTrangThai.Location = new Point(410, 29); cboTrangThai.Size = new Size(170, 28);

            lblDiaChi.AutoSize = true; lblDiaChi.Location = new Point(15, 72); lblDiaChi.Text = "Địa chỉ đặt máy:";
            txtDiaChi.Location = new Point(130, 69); txtDiaChi.Size = new Size(450, 27);

            lblTonQuy.AutoSize = true; lblTonQuy.Location = new Point(15, 112); lblTonQuy.Text = "Tồn quỹ (VNĐ):";
            txtTonQuy.Location = new Point(130, 109); txtTonQuy.Size = new Size(210, 27);

            btnAdd.Location = new Point(130, 145); btnAdd.Size = new Size(105, 30); btnAdd.Text = "Thêm trạm"; btnAdd.UseVisualStyleBackColor = true; btnAdd.Click += btnAdd_Click;
            btnUpdate.Location = new Point(245, 145); btnUpdate.Size = new Size(105, 30); btnUpdate.Text = "Cập nhật"; btnUpdate.UseVisualStyleBackColor = true; btnUpdate.Click += btnUpdate_Click;
            btnDelete.Location = new Point(360, 145); btnDelete.Size = new Size(105, 30); btnDelete.Text = "Xóa trạm"; btnDelete.UseVisualStyleBackColor = true; btnDelete.Click += btnDelete_Click;
            btnClear.Location = new Point(475, 145); btnClear.Size = new Size(105, 30); btnClear.Text = "Làm mới"; btnClear.UseVisualStyleBackColor = true; btnClear.Click += btnClear_Click;

            lblAnh.AutoSize = true; lblAnh.Location = new Point(605, 25); lblAnh.Text = "Ảnh hiện trường:";
            picAnhTram.BorderStyle = BorderStyle.FixedSingle;
            picAnhTram.Location = new Point(605, 50);
            picAnhTram.Name = "picAnhTram";
            picAnhTram.Size = new Size(185, 120);
            picAnhTram.SizeMode = PictureBoxSizeMode.Zoom;

            btnChonAnh.Location = new Point(805, 60); btnChonAnh.Size = new Size(140, 32); btnChonAnh.Text = "Chọn ảnh (File)..."; btnChonAnh.UseVisualStyleBackColor = true; btnChonAnh.Click += btnChonAnh_Click;
            btnXoaAnh.Location = new Point(805, 102); btnXoaAnh.Size = new Size(140, 32); btnXoaAnh.Text = "Xóa ảnh"; btnXoaAnh.UseVisualStyleBackColor = true; btnXoaAnh.Click += btnXoaAnh_Click;

            // grpSearch
            grpSearch.Controls.AddRange(new Control[] { lblSearch, txtSearch, btnSearch, btnReload });
            grpSearch.Dock = DockStyle.Top;
            grpSearch.Location = new Point(8, 193);
            grpSearch.Name = "grpSearch";
            grpSearch.Size = new Size(964, 65);
            grpSearch.TabIndex = 1;
            grpSearch.TabStop = false;
            grpSearch.Text = "Tìm kiếm Trạm ATM (SqlParameter LIKE %)";

            lblSearch.AutoSize = true; lblSearch.Location = new Point(15, 28); lblSearch.Text = "Từ khóa (Mã / Địa chỉ):";
            txtSearch.Location = new Point(180, 25); txtSearch.Size = new Size(370, 27);
            btnSearch.Location = new Point(565, 23); btnSearch.Size = new Size(115, 30); btnSearch.Text = "Tìm kiếm"; btnSearch.UseVisualStyleBackColor = true; btnSearch.Click += btnSearch_Click;
            btnReload.Location = new Point(690, 23); btnReload.Size = new Size(115, 30); btnReload.Text = "Tải lại tất cả"; btnReload.UseVisualStyleBackColor = true; btnReload.Click += btnReload_Click;

            // dgvTramAtm
            dgvTramAtm.AllowUserToAddRows = false;
            dgvTramAtm.AllowUserToDeleteRows = false;
            dgvTramAtm.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTramAtm.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTramAtm.Dock = DockStyle.Fill;
            dgvTramAtm.Location = new Point(8, 258);
            dgvTramAtm.MultiSelect = false;
            dgvTramAtm.Name = "dgvTramAtm";
            dgvTramAtm.ReadOnly = true;
            dgvTramAtm.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTramAtm.Size = new Size(964, 300);
            dgvTramAtm.TabIndex = 2;
            dgvTramAtm.SelectionChanged += dgvTramAtm_SelectionChanged;

            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider1.ContainerControl = this;

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(980, 566);
            Controls.Add(dgvTramAtm);
            Controls.Add(grpSearch);
            Controls.Add(grpInput);
            Name = "frmAtmStations";
            Padding = new Padding(8);
            Text = "Danh sách Trạm ATM & Tồn quỹ (Form Con 1)";
            Load += frmAtmStations_Load;
            grpInput.ResumeLayout(false);
            grpInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picAnhTram).EndInit();
            grpSearch.ResumeLayout(false);
            grpSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTramAtm).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpInput, grpSearch;
        private Label lblMaTram, lblDiaChi, lblTonQuy, lblTrangThai, lblAnh, lblSearch;
        private TextBox txtMaTram, txtDiaChi, txtTonQuy, txtSearch;
        private ComboBox cboTrangThai;
        private PictureBox picAnhTram;
        private Button btnChonAnh, btnXoaAnh, btnAdd, btnUpdate, btnDelete, btnClear, btnSearch, btnReload;
        private DataGridView dgvTramAtm;
        private ErrorProvider errorProvider1;
    }
}
