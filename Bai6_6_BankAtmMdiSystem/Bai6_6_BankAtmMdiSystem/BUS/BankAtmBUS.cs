using System;
using System.Collections.Generic;
using Bai6_6_BankAtmMdiSystem.DAL;
using Bai6_6_BankAtmMdiSystem.DTO;

namespace Bai6_6_BankAtmMdiSystem.BUS
{
    public class BankAtmBUS
    {
        private readonly BankAtmDAL dal = new BankAtmDAL();

        public GiaoDichVienDTO? DangNhap(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu!");
            return dal.DangNhap(username.Trim(), password.Trim());
        }

        public List<TramAtmDTO> GetAllStations(string? keyword = null) => dal.GetAllStations(keyword);

        public bool InsertStation(TramAtmDTO atm)
        {
            ValidateStation(atm);
            return dal.InsertStation(atm);
        }

        public bool UpdateStation(TramAtmDTO atm)
        {
            ValidateStation(atm);
            return dal.UpdateStation(atm);
        }

        public bool DeleteStation(string maTram)
        {
            if (string.IsNullOrWhiteSpace(maTram)) throw new ArgumentException("Mã trạm ATM không hợp lệ!");
            return dal.DeleteStation(maTram);
        }

        public List<PhienDoiSoatDTO> GetLichSuDoiSoat() => dal.GetLichSuDoiSoat();

        public int SavePhienDoiSoat(PhienDoiSoatDTO phien) => dal.SavePhienDoiSoat(phien);

        private void ValidateStation(TramAtmDTO atm)
        {
            if (string.IsNullOrWhiteSpace(atm.MaTram))
                throw new ArgumentException("Mã trạm ATM không được để trống!");
            if (string.IsNullOrWhiteSpace(atm.DiaChiDatMay))
                throw new ArgumentException("Địa chỉ đặt máy ATM không được để trống!");
            if (atm.SoTienTonQuy < 0)
                throw new ArgumentException("Số tiền tồn quỹ không được âm!");
        }
    }
}
