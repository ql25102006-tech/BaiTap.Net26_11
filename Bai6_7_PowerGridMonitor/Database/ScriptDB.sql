-- =============================================
-- Script khởi tạo CSDL: PowerGridDB
-- Bài 6.7: Trung tâm Điều độ & Giám sát Lưới điện Thời gian thực (SCADA)
-- =============================================

IF DB_ID('PowerGridDB') IS NULL
BEGIN
    CREATE DATABASE PowerGridDB;
END
GO

USE PowerGridDB;
GO

IF OBJECT_ID('NhatKySuCo', 'U') IS NOT NULL DROP TABLE NhatKySuCo;
IF OBJECT_ID('ThongSoVanHanh', 'U') IS NOT NULL DROP TABLE ThongSoVanHanh;
IF OBJECT_ID('TramBienAp', 'U') IS NOT NULL DROP TABLE TramBienAp;
GO

-- 1. Bảng TramBienAp
CREATE TABLE TramBienAp (
    MaTram VARCHAR(20) PRIMARY KEY,
    TenTram NVARCHAR(150) NOT NULL,
    CapDienAp INT NOT NULL,
    KhuVuc NVARCHAR(100),
    NguongAnToan FLOAT DEFAULT 85.0
);
GO

-- 2. Bảng ThongSoVanHanh
CREATE TABLE ThongSoVanHanh (
    MaBanGhi INT IDENTITY(1,1) PRIMARY KEY,
    MaTram VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES TramBienAp(MaTram),
    ThoiGianQuet DATETIME DEFAULT GETDATE(),
    DienApKV FLOAT NOT NULL,
    MucTaiPhanTram FLOAT NOT NULL,
    TrangThai NVARCHAR(50) NOT NULL
);
GO

-- 3. Bảng NhatKySuCo
CREATE TABLE NhatKySuCo (
    MaSuCo INT IDENTITY(1,1) PRIMARY KEY,
    MaTram VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES TramBienAp(MaTram),
    ThoiGian DATETIME DEFAULT GETDATE(),
    DienApKV FLOAT NOT NULL,
    MucTaiPhanTram FLOAT NOT NULL,
    NoiDungCanhBao NVARCHAR(255) NOT NULL
);
GO

-- =============================================
-- DỮ LIỆU MẪU (SAMPLE DATA)
-- =============================================

INSERT INTO TramBienAp (MaTram, TenTram, CapDienAp, KhuVuc, NguongAnToan) VALUES
('TBA_HD', N'Trạm 110kV Hà Đông', 110, N'Hà Đông - Hà Nội', 85.0),
('TBA_CHEM', N'Trạm 220kV Chèm', 220, N'Bắc Từ Liêm - Hà Nội', 85.0),
('TBA_TD', N'Trạm 110kV Thượng Đình', 110, N'Thanh Xuân - Hà Nội', 85.0),
('TBA_ST', N'Trạm 220kV Sơn Tây', 220, N'Sơn Tây - Hà Nội', 85.0);
GO

INSERT INTO NhatKySuCo (MaTram, ThoiGian, DienApKV, MucTaiPhanTram, NoiDungCanhBao) VALUES
('TBA_HD', DATEADD(MINUTE, -45, GETDATE()), 108.6, 89.4, N'Quá tải lưới điện (> 85% ngưỡng an toàn)'),
('TBA_CHEM', DATEADD(MINUTE, -20, GETDATE()), 109.2, 92.1, N'Quá tải lưới điện (> 85% ngưỡng an toàn)');
GO
