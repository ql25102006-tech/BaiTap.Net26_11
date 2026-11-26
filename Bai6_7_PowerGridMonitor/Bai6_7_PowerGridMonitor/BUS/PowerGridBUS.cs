using System;
using System.Collections.Generic;
using Bai6_7_PowerGridMonitor.DAL;
using Bai6_7_PowerGridMonitor.DTO;

namespace Bai6_7_PowerGridMonitor.BUS
{
    public class PowerGridBUS
    {
        private readonly PowerGridDAL dal = new PowerGridDAL();

        public List<TramBienApDTO> GetAllTramBienAp() => dal.GetAllTramBienAp();

        public List<NhatKySuCoDTO> GetNhatKySuCo(string? maTram = null) => dal.GetNhatKySuCo(maTram);

        public void LuuThongSoVaSuCo(string maTram, double dienApKV, double mucTai, bool quaTai)
        {
            if (string.IsNullOrWhiteSpace(maTram))
                throw new ArgumentException("Mã trạm biến áp không hợp lệ!");
            dal.LuuThongSoVaSuCo(maTram, dienApKV, mucTai, quaTai);
        }
    }
}
