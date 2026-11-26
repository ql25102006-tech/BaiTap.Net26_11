using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Bai6_6_BankAtmMdiSystem.DTO;

namespace Bai6_6_BankAtmMdiSystem.DAL
{
    public class BankAtmDAL
    {
        public GiaoDichVienDTO? DangNhap(string username, string password)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = "SELECT MaGDV, TenDangNhap, MatKhau, HoTen, CaTruc FROM GiaoDichVien WHERE TenDangNhap = @User AND MatKhau = @Pass";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@User", username);
                    cmd.Parameters.AddWithValue("@Pass", password);
                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            return new GiaoDichVienDTO
                            {
                                MaGDV = r.GetInt32(0),
                                TenDangNhap = r.GetString(1),
                                MatKhau = r.GetString(2),
                                HoTen = r.GetString(3),
                                CaTruc = r.IsDBNull(4) ? "" : r.GetString(4)
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<TramAtmDTO> GetAllStations(string? keyword = null)
        {
            var list = new List<TramAtmDTO>();
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = @"
                    SELECT MaTram, DiaChiDatMay, SoTienTonQuy, TrangThaiHoatDong, AnhHienTruong
                    FROM TramATM
                    WHERE (@Keyword = '' OR MaTram LIKE @Pattern OR DiaChiDatMay LIKE @Pattern)
                    ORDER BY MaTram";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    string kw = keyword?.Trim() ?? string.Empty;
                    cmd.Parameters.AddWithValue("@Keyword", kw);
                    cmd.Parameters.AddWithValue("@Pattern", "%" + kw + "%");
                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new TramAtmDTO
                            {
                                MaTram = r.GetString(0),
                                DiaChiDatMay = r.GetString(1),
                                SoTienTonQuy = r.GetDecimal(2),
                                TrangThaiHoatDong = r.IsDBNull(3) ? "Hoạt động" : r.GetString(3),
                                AnhHienTruong = r.IsDBNull(4) ? null : r.GetString(4)
                            });
                        }
                    }
                }
            }
            return list;
        }

        public bool InsertStation(TramAtmDTO atm)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = @"
                    INSERT INTO TramATM (MaTram, DiaChiDatMay, SoTienTonQuy, TrangThaiHoatDong, AnhHienTruong)
                    VALUES (@Ma, @DiaChi, @TonQuy, @TrangThai, @Anh)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Ma", atm.MaTram);
                    cmd.Parameters.AddWithValue("@DiaChi", atm.DiaChiDatMay);
                    cmd.Parameters.AddWithValue("@TonQuy", atm.SoTienTonQuy);
                    cmd.Parameters.AddWithValue("@TrangThai", atm.TrangThaiHoatDong);
                    cmd.Parameters.AddWithValue("@Anh", (object?)atm.AnhHienTruong ?? DBNull.Value);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateStation(TramAtmDTO atm)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = @"
                    UPDATE TramATM
                    SET DiaChiDatMay = @DiaChi, SoTienTonQuy = @TonQuy, TrangThaiHoatDong = @TrangThai, AnhHienTruong = @Anh
                    WHERE MaTram = @Ma";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Ma", atm.MaTram);
                    cmd.Parameters.AddWithValue("@DiaChi", atm.DiaChiDatMay);
                    cmd.Parameters.AddWithValue("@TonQuy", atm.SoTienTonQuy);
                    cmd.Parameters.AddWithValue("@TrangThai", atm.TrangThaiHoatDong);
                    cmd.Parameters.AddWithValue("@Anh", (object?)atm.AnhHienTruong ?? DBNull.Value);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteStation(string maTram)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmdGD = new SqlCommand("DELETE FROM GiaoDichATM WHERE MaTram = @Ma", conn, trans))
                        {
                            cmdGD.Parameters.AddWithValue("@Ma", maTram);
                            cmdGD.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM TramATM WHERE MaTram = @Ma", conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@Ma", maTram);
                            int rows = cmd.ExecuteNonQuery();
                            trans.Commit();
                            return rows > 0;
                        }
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<PhienDoiSoatDTO> GetLichSuDoiSoat()
        {
            var list = new List<PhienDoiSoatDTO>();
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = "SELECT MaDoiSoat, NgayDoiSoat, TongGiaoDich, SoGiaoDichKhop, SoSaiLech, TongTienDoiSoat, TrangThai, GhiChu FROM PhienDoiSoat ORDER BY NgayDoiSoat DESC";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new PhienDoiSoatDTO
                            {
                                MaDoiSoat = r.GetInt32(0),
                                NgayDoiSoat = r.GetDateTime(1),
                                TongGiaoDich = r.GetInt32(2),
                                SoGiaoDichKhop = r.GetInt32(3),
                                SoSaiLech = r.GetInt32(4),
                                TongTienDoiSoat = r.GetDecimal(5),
                                TrangThai = r.GetString(6),
                                GhiChu = r.IsDBNull(7) ? "" : r.GetString(7)
                            });
                        }
                    }
                }
            }
            return list;
        }

        public int SavePhienDoiSoat(PhienDoiSoatDTO phien)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        string sqlInsert = @"
                            INSERT INTO PhienDoiSoat (NgayDoiSoat, TongGiaoDich, SoGiaoDichKhop, SoSaiLech, TongTienDoiSoat, TrangThai, GhiChu)
                            OUTPUT INSERTED.MaDoiSoat
                            VALUES (@Ngay, @TongGD, @Khop, @SaiLech, @TongTien, @TrangThai, @GhiChu)";
                        int maDoiSoat;
                        using (SqlCommand cmd = new SqlCommand(sqlInsert, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@Ngay", phien.NgayDoiSoat);
                            cmd.Parameters.AddWithValue("@TongGD", phien.TongGiaoDich);
                            cmd.Parameters.AddWithValue("@Khop", phien.SoGiaoDichKhop);
                            cmd.Parameters.AddWithValue("@SaiLech", phien.SoSaiLech);
                            cmd.Parameters.AddWithValue("@TongTien", phien.TongTienDoiSoat);
                            cmd.Parameters.AddWithValue("@TrangThai", phien.TrangThai);
                            cmd.Parameters.AddWithValue("@GhiChu", phien.GhiChu);
                            maDoiSoat = (int)cmd.ExecuteScalar();
                        }

                        string sqlUpdateGD = "UPDATE GiaoDichATM SET TrangThaiDoiSoat = N'Đã đối soát' WHERE CAST(ThoiGian AS DATE) <= CAST(@Ngay AS DATE) AND TrangThaiDoiSoat = N'Chờ đối soát'";
                        using (SqlCommand cmdUp = new SqlCommand(sqlUpdateGD, conn, trans))
                        {
                            cmdUp.Parameters.AddWithValue("@Ngay", phien.NgayDoiSoat);
                            cmdUp.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return maDoiSoat;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
