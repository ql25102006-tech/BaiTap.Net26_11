using System;

namespace Bai6_7_PowerGridMonitor.DTO
{
    public class TramBienApDTO
    {
        public string MaTram { get; set; } = string.Empty;
        public string TenTram { get; set; } = string.Empty;
        public int CapDienAp { get; set; }
        public string KhuVuc { get; set; } = string.Empty;
        public double NguongAnToan { get; set; } = 85.0;
    }

    public class NhatKySuCoDTO
    {
        public int MaSuCo { get; set; }
        public string MaTram { get; set; } = string.Empty;
        public string TenTram { get; set; } = string.Empty;
        public DateTime ThoiGian { get; set; }
        public double DienApKV { get; set; }
        public double MucTaiPhanTram { get; set; }
        public string NoiDungCanhBao { get; set; } = string.Empty;
    }
}
