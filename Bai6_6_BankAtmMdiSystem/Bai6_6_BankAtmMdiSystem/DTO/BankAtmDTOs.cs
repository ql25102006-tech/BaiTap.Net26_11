using System;

namespace Bai6_6_BankAtmMdiSystem.DTO
{
    public class GiaoDichVienDTO
    {
        public int MaGDV { get; set; }
        public string TenDangNhap { get; set; } = string.Empty;
        public string MatKhau { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public string CaTruc { get; set; } = string.Empty;
    }

    public class TramAtmDTO
    {
        public string MaTram { get; set; } = string.Empty;
        public string DiaChiDatMay { get; set; } = string.Empty;
        public decimal SoTienTonQuy { get; set; }
        public string TrangThaiHoatDong { get; set; } = "Hoạt động";
        public string? AnhHienTruong { get; set; }
    }

    public class PhienDoiSoatDTO
    {
        public int MaDoiSoat { get; set; }
        public DateTime NgayDoiSoat { get; set; }
        public int TongGiaoDich { get; set; }
        public int SoGiaoDichKhop { get; set; }
        public int SoSaiLech { get; set; }
        public decimal TongTienDoiSoat { get; set; }
        public string TrangThai { get; set; } = "Hoàn tất";
        public string GhiChu { get; set; } = string.Empty;
    }
}
