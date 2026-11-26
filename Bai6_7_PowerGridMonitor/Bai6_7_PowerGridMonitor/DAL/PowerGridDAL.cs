using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Bai6_7_PowerGridMonitor.DTO;

namespace Bai6_7_PowerGridMonitor.DAL
{
    public class PowerGridDAL
    {
        public List<TramBienApDTO> GetAllTramBienAp()
        {
            var list = new List<TramBienApDTO>();
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = "SELECT MaTram, TenTram, CapDienAp, KhuVuc, NguongAnToan FROM TramBienAp ORDER BY MaTram";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new TramBienApDTO
                            {
                                MaTram = r.GetString(0),
                                TenTram = r.GetString(1),
                                CapDienAp = r.GetInt32(2),
                                KhuVuc = r.IsDBNull(3) ? "" : r.GetString(3),
                                NguongAnToan = r.GetDouble(4)
                            });
                        }
                    }
                }
            }
            return list;
        }

        public List<NhatKySuCoDTO> GetNhatKySuCo(string? maTram = null)
        {
            var list = new List<NhatKySuCoDTO>();
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                string sql = @"
                    SELECT TOP 100 nk.MaSuCo, nk.MaTram, t.TenTram, nk.ThoiGian, nk.DienApKV, nk.MucTaiPhanTram, nk.NoiDungCanhBao
                    FROM NhatKySuCo nk
                    INNER JOIN TramBienAp t ON nk.MaTram = t.MaTram
                    WHERE (@MaTram = '' OR nk.MaTram = @MaTram)
                    ORDER BY nk.ThoiGian DESC";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaTram", maTram ?? string.Empty);
                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new NhatKySuCoDTO
                            {
                                MaSuCo = r.GetInt32(0),
                                MaTram = r.GetString(1),
                                TenTram = r.GetString(2),
                                ThoiGian = r.GetDateTime(3),
                                DienApKV = r.GetDouble(4),
                                MucTaiPhanTram = r.GetDouble(5),
                                NoiDungCanhBao = r.GetString(6)
                            });
                        }
                    }
                }
            }
            return list;
        }

        public void LuuThongSoVaSuCo(string maTram, double dienApKV, double mucTai, bool quaTai)
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        string trangThai = quaTai ? "Quá tải" : "Ổn định";
                        string sqlTS = @"
                            INSERT INTO ThongSoVanHanh (MaTram, ThoiGianQuet, DienApKV, MucTaiPhanTram, TrangThai)
                            VALUES (@MaTram, GETDATE(), @DienAp, @MucTai, @TrangThai)";
                        using (SqlCommand cmdTS = new SqlCommand(sqlTS, conn, trans))
                        {
                            cmdTS.Parameters.AddWithValue("@MaTram", maTram);
                            cmdTS.Parameters.AddWithValue("@DienAp", Math.Round(dienApKV, 2));
                            cmdTS.Parameters.AddWithValue("@MucTai", Math.Round(mucTai, 2));
                            cmdTS.Parameters.AddWithValue("@TrangThai", trangThai);
                            cmdTS.ExecuteNonQuery();
                        }

                        if (quaTai)
                        {
                            string sqlSC = @"
                                INSERT INTO NhatKySuCo (MaTram, ThoiGian, DienApKV, MucTaiPhanTram, NoiDungCanhBao)
                                VALUES (@MaTram, GETDATE(), @DienAp, @MucTai, @NoiDung)";
                            using (SqlCommand cmdSC = new SqlCommand(sqlSC, conn, trans))
                            {
                                cmdSC.Parameters.AddWithValue("@MaTram", maTram);
                                cmdSC.Parameters.AddWithValue("@DienAp", Math.Round(dienApKV, 2));
                                cmdSC.Parameters.AddWithValue("@MucTai", Math.Round(mucTai, 2));
                                cmdSC.Parameters.AddWithValue("@NoiDung", $"Quá tải lưới điện ({mucTai:F1}% > 85% ngưỡng an toàn)");
                                cmdSC.ExecuteNonQuery();
                            }
                        }

                        trans.Commit();
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
