using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bai6_7_PowerGridMonitor.BUS;
using Bai6_7_PowerGridMonitor.DTO;

namespace Bai6_7_PowerGridMonitor.GUI
{
    public partial class frmPowerGridMonitor : Form
    {
        private readonly PowerGridBUS bus = new PowerGridBUS();
        private BindingList<NhatKySuCoDTO> gridLogs = new BindingList<NhatKySuCoDTO>();
        private CancellationTokenSource? _cts;
        private int _totalCycles = 0;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AutoLoadDatabase { get; set; } = true;

        public frmPowerGridMonitor()
        {
            InitializeComponent();
        }

        private void frmPowerGridMonitor_Load(object? sender, EventArgs e)
        {
            SetupGrid();
            if (AutoLoadDatabase)
            {
                LoadSubstationsAndHistory();
            }
            else
            {
                LoadFallbackSubstations();
            }
        }

        private void SetupGrid()
        {
            dgvGridLogs.AutoGenerateColumns = false;
            dgvGridLogs.Columns.Clear();
            dgvGridLogs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaSuCo", HeaderText = "STT", Width = 65 });
            var colTime = new DataGridViewTextBoxColumn { DataPropertyName = "ThoiGian", HeaderText = "Thời gian sự cố", Width = 145 };
            colTime.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            dgvGridLogs.Columns.Add(colTime);
            dgvGridLogs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenTram", HeaderText = "Trạm biến áp", Width = 210 });
            var colVolt = new DataGridViewTextBoxColumn { DataPropertyName = "DienApKV", HeaderText = "Điện áp (kV)", Width = 115 };
            colVolt.DefaultCellStyle.Format = "N1";
            colVolt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGridLogs.Columns.Add(colVolt);
            var colLoad = new DataGridViewTextBoxColumn { DataPropertyName = "MucTaiPhanTram", HeaderText = "Mức tải (%)", Width = 110 };
            colLoad.DefaultCellStyle.Format = "N1";
            colLoad.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGridLogs.Columns.Add(colLoad);
            dgvGridLogs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NoiDungCanhBao", HeaderText = "Nội dung cảnh báo", Width = 290 });

            dgvGridLogs.DataSource = gridLogs;
        }

        private void LoadFallbackSubstations()
        {
            var list = new List<TramBienApDTO>
            {
                new() { MaTram = "TBA_HD", TenTram = "Trạm 110kV Hà Đông", CapDienAp = 110, NguongAnToan = 85.0 },
                new() { MaTram = "TBA_CHEM", TenTram = "Trạm 220kV Chèm", CapDienAp = 220, NguongAnToan = 85.0 },
                new() { MaTram = "TBA_TD", TenTram = "Trạm 110kV Thượng Đình", CapDienAp = 110, NguongAnToan = 85.0 },
                new() { MaTram = "TBA_ST", TenTram = "Trạm 220kV Sơn Tây", CapDienAp = 220, NguongAnToan = 85.0 }
            };
            cboTramBienAp.DataSource = list;
            cboTramBienAp.DisplayMember = "TenTram";
            cboTramBienAp.ValueMember = "MaTram";
        }

        private void LoadSubstationsAndHistory()
        {
            try
            {
                var trams = bus.GetAllTramBienAp();
                cboTramBienAp.DataSource = trams;
                cboTramBienAp.DisplayMember = "TenTram";
                cboTramBienAp.ValueMember = "MaTram";

                ReloadLogsFromDatabase();
                sslScadaStatus.Text = "Trạng thái SCADA: Sẵn sàng (Đã kết nối PowerGridDB)";
            }
            catch (Exception ex)
            {
                LoadFallbackSubstations();
                sslScadaStatus.Text = $"Trạng thái SCADA: Chế độ mô phỏng cục bộ ({ex.Message})";
            }
        }

        private void ReloadLogsFromDatabase()
        {
            var logs = bus.GetNhatKySuCo();
            gridLogs.Clear();
            foreach (var item in logs)
            {
                gridLogs.Add(item);
            }
        }

        private void btnBatDau_Click(object? sender, EventArgs e)
        {
            if (cboTramBienAp.SelectedItem is not TramBienApDTO selectedTram)
            {
                MessageBox.Show("Vui lòng chọn Trạm biến áp cần giám sát!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _cts?.Dispose();
            _cts = new CancellationTokenSource();
            CancellationToken token = _cts.Token;

            btnBatDau.Enabled = false;
            btnNgatKetNoi.Enabled = true;
            cboTramBienAp.Enabled = false;
            sslScadaStatus.Text = $"Trạng thái SCADA: Đang thu thập dữ liệu thời gian thực [{selectedTram.TenTram}]...";

            string maTram = selectedTram.MaTram;
            string tenTram = selectedTram.TenTram;
            double nguongAnToan = selectedTram.NguongAnToan;

            // Tiến trình chạy ngầm trên Background Thread (chu kỳ 2 giây/lần)
            Task.Run(async () =>
            {
                Random rng = new Random();
                while (!token.IsCancellationRequested)
                {
                    // Sinh ngẫu nhiên Điện áp (108kV - 115kV) và Mức tải (50% - 98%)
                    double dienAp = Math.Round(108.0 + rng.NextDouble() * 7.0, 1);
                    double mucTai = Math.Round(50.0 + rng.NextDouble() * 48.0, 1);
                    DateTime thoiGianQuet = DateTime.Now;
                    bool isOverload = mucTai > nguongAnToan;

                    // Lưu thông số vào CSDL từ background thread nếu khả dụng
                    if (AutoLoadDatabase)
                    {
                        try
                        {
                            bus.LuuThongSoVaSuCo(maTram, dienAp, mucTai, isOverload);
                        }
                        catch { }
                    }

                    // BẮT BUỘC: Chuyển ngữ cảnh thực thi về Main UI Thread trước khi đụng vào bất kỳ Control nào
                    CapNhatThongSoAnToanThread(maTram, tenTram, dienAp, mucTai, isOverload, thoiGianQuet);

                    try
                    {
                        await Task.Delay(2000, token);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                }
            }, token);
        }

        /// <summary>
        /// Tuân thủ tuyệt đối Thread Safety bằng InvokeRequired + this.Invoke(...)
        /// </summary>
        public void CapNhatThongSoAnToanThread(string maTram, string tenTram, double dienAp, double mucTai, bool isOverload, DateTime thoiGianQuet)
        {
            if (IsDisposed || !IsHandleCreated) return;

            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke(new Action(() => CapNhatThongSoAnToanThread(maTram, tenTram, dienAp, mucTai, isOverload, thoiGianQuet)));
                }
                catch (ObjectDisposedException) { }
                return;
            }

            // Đang thực thi an toàn trên Main UI Thread
            _totalCycles++;
            lblVoltage.Text = $"{dienAp:N1} kV";
            lblLoadPercent.Text = $"{mucTai:N1} %";

            if (isOverload)
            {
                lblLoadPercent.ForeColor = Color.Red;
                lblCanhBao.ForeColor = Color.Red;
                lblCanhBao.Text = $"CẢNH BÁO QUÁ TẢI (> 85%) tại {tenTram}!";

                gridLogs.Insert(0, new NhatKySuCoDTO
                {
                    MaSuCo = gridLogs.Count + 1,
                    MaTram = maTram,
                    TenTram = tenTram,
                    ThoiGian = thoiGianQuet,
                    DienApKV = dienAp,
                    MucTaiPhanTram = mucTai,
                    NoiDungCanhBao = $"Quá tải lưới điện ({mucTai:N1}% > 85% ngưỡng an toàn)"
                });
            }
            else
            {
                lblLoadPercent.ForeColor = Color.Green;
                lblCanhBao.ForeColor = Color.Green;
                lblCanhBao.Text = "Ổn định - Thông số trong ngưỡng an toàn";
            }

            sslLastPacket.Text = $"Gói tin cuối: {thoiGianQuet:HH:mm:ss}";
            sslTotalCycles.Text = $"Tổng chu kỳ đã quét: {_totalCycles}";
        }

        private void btnNgatKetNoi_Click(object? sender, EventArgs e)
        {
            StopScadaLoop();
            sslScadaStatus.Text = "Trạng thái SCADA: Đã ngắt kết nối theo yêu cầu người dùng.";
        }

        private void StopScadaLoop()
        {
            if (_cts != null && !_cts.IsCancellationRequested)
            {
                _cts.Cancel();
            }
            btnBatDau.Enabled = true;
            btnNgatKetNoi.Enabled = false;
            cboTramBienAp.Enabled = true;
        }

        private void btnTaiLichSu_Click(object? sender, EventArgs e)
        {
            try
            {
                ReloadLogsFromDatabase();
                MessageBox.Show("Đã tải lại nhật ký sự cố từ SQL Server!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không tải được nhật ký từ CSDL: {ex.Message}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmPowerGridMonitor_FormClosing(object? sender, FormClosingEventArgs e)
        {
            StopScadaLoop();
            _cts?.Dispose();
        }
    }
}
