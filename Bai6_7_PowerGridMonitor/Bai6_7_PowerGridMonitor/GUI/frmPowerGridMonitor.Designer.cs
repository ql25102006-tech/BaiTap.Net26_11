namespace Bai6_7_PowerGridMonitor.GUI
{
    partial class frmPowerGridMonitor
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
            grpDieuKhien = new GroupBox();
            lblTram = new Label();
            cboTramBienAp = new ComboBox();
            btnBatDau = new Button();
            btnNgatKetNoi = new Button();
            btnTaiLichSu = new Button();
            grpThongSo = new GroupBox();
            lblVoltageTitle = new Label();
            lblVoltage = new Label();
            lblLoadTitle = new Label();
            lblLoadPercent = new Label();
            lblStatusTitle = new Label();
            lblCanhBao = new Label();
            grpNhatKy = new GroupBox();
            dgvGridLogs = new DataGridView();
            statusStrip1 = new StatusStrip();
            sslLastPacket = new ToolStripStatusLabel();
            sslTotalCycles = new ToolStripStatusLabel();
            sslScadaStatus = new ToolStripStatusLabel();
            grpDieuKhien.SuspendLayout();
            grpThongSo.SuspendLayout();
            grpNhatKy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGridLogs).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();

            // grpDieuKhien
            grpDieuKhien.Controls.AddRange(new Control[] { lblTram, cboTramBienAp, btnBatDau, btnNgatKetNoi, btnTaiLichSu });
            grpDieuKhien.Dock = DockStyle.Top;
            grpDieuKhien.Location = new Point(8, 8);
            grpDieuKhien.Name = "grpDieuKhien";
            grpDieuKhien.Size = new Size(1004, 80);
            grpDieuKhien.TabIndex = 0;
            grpDieuKhien.TabStop = false;
            grpDieuKhien.Text = "Khu vực Điều khiển SCADA";

            lblTram.AutoSize = true;
            lblTram.Location = new Point(15, 34);
            lblTram.Text = "Chọn Trạm biến áp:";

            cboTramBienAp.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTramBienAp.Location = new Point(155, 30);
            cboTramBienAp.Size = new Size(290, 28);

            btnBatDau.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnBatDau.Location = new Point(465, 26);
            btnBatDau.Size = new Size(215, 36);
            btnBatDau.Text = "Bắt đầu Giám sát SCADA";
            btnBatDau.UseVisualStyleBackColor = true;
            btnBatDau.Click += btnBatDau_Click;

            btnNgatKetNoi.Enabled = false;
            btnNgatKetNoi.Location = new Point(695, 26);
            btnNgatKetNoi.Size = new Size(150, 36);
            btnNgatKetNoi.Text = "Ngắt Kết nối";
            btnNgatKetNoi.UseVisualStyleBackColor = true;
            btnNgatKetNoi.Click += btnNgatKetNoi_Click;

            btnTaiLichSu.Location = new Point(860, 26);
            btnTaiLichSu.Size = new Size(130, 36);
            btnTaiLichSu.Text = "Tải lịch sử CSDL";
            btnTaiLichSu.UseVisualStyleBackColor = true;
            btnTaiLichSu.Click += btnTaiLichSu_Click;

            // grpThongSo
            grpThongSo.Controls.AddRange(new Control[] { lblVoltageTitle, lblVoltage, lblLoadTitle, lblLoadPercent, lblStatusTitle, lblCanhBao });
            grpThongSo.Dock = DockStyle.Top;
            grpThongSo.Location = new Point(8, 88);
            grpThongSo.Name = "grpThongSo";
            grpThongSo.Size = new Size(1004, 125);
            grpThongSo.TabIndex = 1;
            grpThongSo.TabStop = false;
            grpThongSo.Text = "Bảng thông số vận hành trực quan (Real-time Telemetry - Cross-Thread Safe via Invoke)";

            lblVoltageTitle.AutoSize = true;
            lblVoltageTitle.Location = new Point(20, 35);
            lblVoltageTitle.Text = "Điện áp hiện tại (kV):";

            lblVoltage.AutoSize = true;
            lblVoltage.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblVoltage.Location = new Point(20, 62);
            lblVoltage.Name = "lblVoltage";
            lblVoltage.Text = "---.- kV";

            lblLoadTitle.AutoSize = true;
            lblLoadTitle.Location = new Point(310, 35);
            lblLoadTitle.Text = "Mức mang tải (% - Ngưỡng an toàn <= 85%):";

            lblLoadPercent.AutoSize = true;
            lblLoadPercent.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblLoadPercent.Location = new Point(310, 62);
            lblLoadPercent.Name = "lblLoadPercent";
            lblLoadPercent.Text = "--.- %";

            lblStatusTitle.AutoSize = true;
            lblStatusTitle.Location = new Point(660, 35);
            lblStatusTitle.Text = "Cảnh báo hệ thống:";

            lblCanhBao.AutoSize = true;
            lblCanhBao.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCanhBao.Location = new Point(660, 68);
            lblCanhBao.Name = "lblCanhBao";
            lblCanhBao.Text = "Chưa kích hoạt giám sát";

            // grpNhatKy
            grpNhatKy.Controls.Add(dgvGridLogs);
            grpNhatKy.Dock = DockStyle.Fill;
            grpNhatKy.Location = new Point(8, 213);
            grpNhatKy.Name = "grpNhatKy";
            grpNhatKy.Padding = new Padding(8);
            grpNhatKy.Size = new Size(1004, 370);
            grpNhatKy.TabIndex = 2;
            grpNhatKy.TabStop = false;
            grpNhatKy.Text = "Nhật ký Sự cố Quá tải Lưới điện (dgvGridLogs - Tự động ghi nhận khi Mức tải > 85%)";

            dgvGridLogs.AllowUserToAddRows = false;
            dgvGridLogs.AllowUserToDeleteRows = false;
            dgvGridLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvGridLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGridLogs.Dock = DockStyle.Fill;
            dgvGridLogs.Location = new Point(8, 28);
            dgvGridLogs.MultiSelect = false;
            dgvGridLogs.Name = "dgvGridLogs";
            dgvGridLogs.ReadOnly = true;
            dgvGridLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGridLogs.Size = new Size(988, 334);

            // statusStrip1
            statusStrip1.Items.AddRange(new ToolStripItem[] { sslLastPacket, sslTotalCycles, sslScadaStatus });
            statusStrip1.Location = new Point(0, 594);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1020, 26);

            sslLastPacket.BorderSides = ToolStripStatusLabelBorderSides.Right;
            sslLastPacket.Name = "sslLastPacket";
            sslLastPacket.Text = "Gói tin cuối: --:--:--";

            sslTotalCycles.BorderSides = ToolStripStatusLabelBorderSides.Right;
            sslTotalCycles.Name = "sslTotalCycles";
            sslTotalCycles.Text = "Tổng chu kỳ đã quét: 0";

            sslScadaStatus.Name = "sslScadaStatus";
            sslScadaStatus.Spring = true;
            sslScadaStatus.Text = "Trạng thái SCADA: Đã ngắt kết nối";
            sslScadaStatus.TextAlign = ContentAlignment.MiddleLeft;

            // Form
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1020, 620);
            Controls.Add(grpNhatKy);
            Controls.Add(grpThongSo);
            Controls.Add(grpDieuKhien);
            Controls.Add(statusStrip1);
            MinimumSize = new Size(920, 520);
            Name = "frmPowerGridMonitor";
            Padding = new Padding(8);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bài 6.7: Trung tâm Điều độ & Giám sát Lưới điện Thời gian thực (Cross-Thread Safety)";
            Load += frmPowerGridMonitor_Load;
            FormClosing += frmPowerGridMonitor_FormClosing;
            grpDieuKhien.ResumeLayout(false); grpDieuKhien.PerformLayout();
            grpThongSo.ResumeLayout(false); grpThongSo.PerformLayout();
            grpNhatKy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvGridLogs).EndInit();
            statusStrip1.ResumeLayout(false); statusStrip1.PerformLayout();
            ResumeLayout(false); PerformLayout();
        }

        #endregion

        private GroupBox grpDieuKhien, grpThongSo, grpNhatKy;
        private Label lblTram, lblVoltageTitle, lblVoltage, lblLoadTitle, lblLoadPercent, lblStatusTitle, lblCanhBao;
        private ComboBox cboTramBienAp;
        private Button btnBatDau, btnNgatKetNoi, btnTaiLichSu;
        private DataGridView dgvGridLogs;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel sslLastPacket, sslTotalCycles, sslScadaStatus;
    }
}
