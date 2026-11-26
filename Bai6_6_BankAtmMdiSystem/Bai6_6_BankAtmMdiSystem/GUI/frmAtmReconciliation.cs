using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bai6_6_BankAtmMdiSystem.BUS;
using Bai6_6_BankAtmMdiSystem.DTO;

namespace Bai6_6_BankAtmMdiSystem.GUI
{
    public partial class frmAtmReconciliation : Form
    {
        private readonly BankAtmBUS bus = new BankAtmBUS();
        private CancellationTokenSource? _cts;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AutoLoadDatabase { get; set; } = true;

        public frmAtmReconciliation()
        {
            InitializeComponent();
        }

        private void frmAtmReconciliation_Load(object? sender, EventArgs e)
        {
            SetupGrid();
            if (AutoLoadDatabase)
            {
                LoadHistory();
            }
        }

        private void SetupGrid()
        {
            dgvLichSuDoiSoat.AutoGenerateColumns = false;
            dgvLichSuDoiSoat.Columns.Clear();
            dgvLichSuDoiSoat.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaDoiSoat", HeaderText = "Mã phiên", Width = 75 });
            var colNgay = new DataGridViewTextBoxColumn { DataPropertyName = "NgayDoiSoat", HeaderText = "Ngày đối soát", Width = 135 };
            colNgay.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvLichSuDoiSoat.Columns.Add(colNgay);
            dgvLichSuDoiSoat.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TongGiaoDich", HeaderText = "Tổng GD", Width = 95 });
            dgvLichSuDoiSoat.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoGiaoDichKhop", HeaderText = "Số khớp", Width = 95 });
            dgvLichSuDoiSoat.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoSaiLech", HeaderText = "Sai lệch", Width = 80 });
            var colTien = new DataGridViewTextBoxColumn { DataPropertyName = "TongTienDoiSoat", HeaderText = "Tổng tiền (VNĐ)", Width = 160 };
            colTien.DefaultCellStyle.Format = "N0";
            colTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLichSuDoiSoat.Columns.Add(colTien);
            dgvLichSuDoiSoat.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TrangThai", HeaderText = "Trạng thái", Width = 100 });
            dgvLichSuDoiSoat.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "GhiChu", HeaderText = "Ghi chú", Width = 180 });
        }

        private void LoadHistory()
        {
            try
            {
                dgvLichSuDoiSoat.DataSource = null;
                dgvLichSuDoiSoat.DataSource = bus.GetLichSuDoiSoat();
            }
            catch { }
        }

        private async void btnChayDoiSoat_Click(object? sender, EventArgs e)
        {
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
            CancellationToken token = _cts.Token;

            btnChayDoiSoat.Enabled = false;
            btnDungDoiSoat.Enabled = true;
            prgDoiSoat.Value = 0;
            lstLogDoiSoat.Items.Clear();

            DateTime ngayDoiSoat = dtpNgayDoiSoat.Value;
            frmBankAtmMain? parentMdi = this.MdiParent as frmBankAtmMain;

            var progressReporter = new Progress<(int Percent, string LogLine)>(update =>
            {
                prgDoiSoat.Value = update.Percent;
                lblTrangThai.Text = $"Đang đối soát tự động: {update.Percent}% — {update.LogLine}";
                lstLogDoiSoat.Items.Add($"[{DateTime.Now:HH:mm:ss}] {update.LogLine}");
                lstLogDoiSoat.TopIndex = lstLogDoiSoat.Items.Count - 1;
                parentMdi?.UpdateReconciliationProgress(update.Percent, $"Đang đối soát giao dịch cuối ngày ({update.Percent}%)...");
            });

            try
            {
                PhienDoiSoatDTO ketQua = await Task.Run(async () =>
                {
                    IProgress<(int, string)> reporter = progressReporter;
                    int totalSteps = 10;
                    int totalTransactions = 150000;
                    int matched = 0;
                    int discrepancy = 0;

                    for (int step = 1; step <= totalSteps; step++)
                    {
                        token.ThrowIfCancellationRequested();

                        // Giả lập tải và đối soát 15.000 giao dịch mỗi lô (Tổng 5 giây cho 10 bước)
                        await Task.Delay(500, token);

                        int batchSize = totalTransactions / totalSteps;
                        int stepMismatch = (step == 6 || step == 9) ? 1 : 0;
                        matched += (batchSize - stepMismatch);
                        discrepancy += stepMismatch;

                        int percent = step * 10;
                        string detail = $"Lô {step}/{totalSteps}: Đã đối chiếu {step * batchSize:N0} giao dịch rút tiền/chuyển khoản (Sai lệch: {discrepancy})";
                        reporter.Report((percent, detail));
                    }

                    return new PhienDoiSoatDTO
                    {
                        NgayDoiSoat = ngayDoiSoat,
                        TongGiaoDich = totalTransactions,
                        SoGiaoDichKhop = matched,
                        SoSaiLech = discrepancy,
                        TongTienDoiSoat = 214500000000m,
                        TrangThai = "Hoàn tất",
                        GhiChu = $"Đối soát tự động ngày {ngayDoiSoat:dd/MM/yyyy}"
                    };
                }, token);

                // Lưu kết quả vào SQL Server nếu kết nối sẵn sàng
                if (AutoLoadDatabase)
                {
                    try
                    {
                        bus.SavePhienDoiSoat(ketQua);
                        LoadHistory();
                    }
                    catch (Exception dbEx)
                    {
                        lstLogDoiSoat.Items.Add($"[Cảnh báo CSDL] Không lưu được log vào DB: {dbEx.Message}");
                    }
                }

                lblTrangThai.Text = $"Hoàn tất đối soát {ketQua.TongGiaoDich:N0} giao dịch (Khớp: {ketQua.SoGiaoDichKhop:N0}, Sai lệch: {ketQua.SoSaiLech})!";
                parentMdi?.UpdateReconciliationProgress(100, "Core Banking: Đối soát cuối ngày hoàn tất!");
                MessageBox.Show($"Đối soát tự động hoàn tất!\n- Tổng giao dịch: {ketQua.TongGiaoDich:N0}\n- Giao dịch khớp: {ketQua.SoGiaoDichKhop:N0}\n- Sai lệch: {ketQua.SoSaiLech}",
                    "Hoàn tất Đối soát", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                lblTrangThai.Text = "Đã dừng đối soát theo yêu cầu người dùng (Cancelled).";
                lstLogDoiSoat.Items.Add($"[{DateTime.Now:HH:mm:ss}] Tiến trình đối soát đã bị hủy an toàn qua CancellationTokenSource.");
                prgDoiSoat.Value = 0;
                parentMdi?.UpdateReconciliationProgress(0, "Core Banking: Đã dừng tác vụ đối soát.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi trong quá trình đối soát: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnChayDoiSoat.Enabled = true;
                btnDungDoiSoat.Enabled = false;
            }
        }

        private void btnDungDoiSoat_Click(object? sender, EventArgs e)
        {
            if (_cts != null && !_cts.IsCancellationRequested)
            {
                btnDungDoiSoat.Enabled = false;
                lblTrangThai.Text = "Đang gửi tín hiệu dừng đối soát...";
                _cts.Cancel();
            }
        }

        private void frmAtmReconciliation_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (_cts != null && !_cts.IsCancellationRequested)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
        }
    }
}
