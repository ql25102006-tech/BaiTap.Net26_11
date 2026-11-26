-- =============================================
-- Script khởi tạo CSDL: BankAtmMdiDB
-- Bài 6.6: Hệ thống MDI Điều hành Mạng lưới Cây ATM Ngân hàng
-- =============================================

IF DB_ID('BankAtmMdiDB') IS NULL
BEGIN
    CREATE DATABASE BankAtmMdiDB;
END
GO

USE BankAtmMdiDB;
GO

IF OBJECT_ID('GiaoDichATM', 'U') IS NOT NULL DROP TABLE GiaoDichATM;
IF OBJECT_ID('PhienDoiSoat', 'U') IS NOT NULL DROP TABLE PhienDoiSoat;
IF OBJECT_ID('TramATM', 'U') IS NOT NULL DROP TABLE TramATM;
IF OBJECT_ID('GiaoDichVien', 'U') IS NOT NULL DROP TABLE GiaoDichVien;
GO

-- 1. Bảng GiaoDichVien
CREATE TABLE GiaoDichVien (
    MaGDV INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap VARCHAR(50) UNIQUE NOT NULL,
    MatKhau VARCHAR(100) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    CaTruc NVARCHAR(50)
);
GO

-- 2. Bảng TramATM
CREATE TABLE TramATM (
    MaTram VARCHAR(20) PRIMARY KEY,
    DiaChiDatMay NVARCHAR(200) NOT NULL,
    SoTienTonQuy DECIMAL(18,2) NOT NULL CHECK (SoTienTonQuy >= 0),
    TrangThaiHoatDong NVARCHAR(50) DEFAULT N'Hoạt động',
    AnhHienTruong NVARCHAR(500) NULL
);
GO

-- 3. Bảng GiaoDichATM
CREATE TABLE GiaoDichATM (
    MaGD INT IDENTITY(1,1) PRIMARY KEY,
    MaTram VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES TramATM(MaTram),
    SoThe VARCHAR(20) NOT NULL,
    LoaiGD NVARCHAR(50) NOT NULL,
    SoTien DECIMAL(18,2) NOT NULL,
    ThoiGian DATETIME DEFAULT GETDATE(),
    TrangThaiDoiSoat NVARCHAR(30) DEFAULT N'Chờ đối soát'
);
GO

-- 4. Bảng PhienDoiSoat
CREATE TABLE PhienDoiSoat (
    MaDoiSoat INT IDENTITY(1,1) PRIMARY KEY,
    NgayDoiSoat DATETIME DEFAULT GETDATE(),
    TongGiaoDich INT NOT NULL,
    SoGiaoDichKhop INT NOT NULL,
    SoSaiLech INT NOT NULL,
    TongTienDoiSoat DECIMAL(18,2) NOT NULL,
    TrangThai NVARCHAR(50) NOT NULL,
    GhiChu NVARCHAR(255)
);
GO

-- =============================================
-- DỮ LIỆU MẪU (SAMPLE DATA)
-- =============================================

INSERT INTO GiaoDichVien (TenDangNhap, MatKhau, HoTen, CaTruc) VALUES
('gdv01', '123456', N'Lê Ngọc Quyền', N'Ca Sáng (07:30 - 15:30)'),
('gdv02', '123456', N'Nguyễn Thị Minh', N'Ca Chiều (15:30 - 23:30)');
GO

INSERT INTO TramATM (MaTram, DiaChiDatMay, SoTienTonQuy, TrangThaiHoatDong, AnhHienTruong) VALUES
('ATM_EPU_01', N'235 Hoàng Quốc Việt, Bắc Từ Liêm, Hà Nội', 450000000, N'Hoạt động', NULL),
('ATM_CG_02', N'18 Phạm Hùng, Cầu Giấy, Hà Nội', 320000000, N'Hoạt động', NULL),
('ATM_DD_03', N'100 Tây Sơn, Đống Đa, Hà Nội', 85000000, N'Hoạt động', NULL),
('ATM_HD_04', N'10 Trần Phú, Hà Đông, Hà Nội', 0, N'Hết tiền', NULL),
('ATM_TX_05', N'312 Nguyễn Trãi, Thanh Xuân, Hà Nội', 150000000, N'Bảo trì', NULL);
GO

INSERT INTO GiaoDichATM (MaTram, SoThe, LoaiGD, SoTien, ThoiGian, TrangThaiDoiSoat) VALUES
('ATM_EPU_01', '9704000000001001', N'Rút tiền', 2000000, DATEADD(HOUR, -3, GETDATE()), N'Chờ đối soát'),
('ATM_EPU_01', '9704000000001002', N'Chuyển khoản', 5000000, DATEADD(HOUR, -2, GETDATE()), N'Chờ đối soát'),
('ATM_CG_02', '9704000000001003', N'Rút tiền', 1500000, DATEADD(HOUR, -1, GETDATE()), N'Chờ đối soát'),
('ATM_DD_03', '9704000000001004', N'Rút tiền', 3000000, DATEADD(MINUTE, -30, GETDATE()), N'Chờ đối soát');
GO

INSERT INTO PhienDoiSoat (NgayDoiSoat, TongGiaoDich, SoGiaoDichKhop, SoSaiLech, TongTienDoiSoat, TrangThai, GhiChu) VALUES
(DATEADD(DAY, -1, GETDATE()), 124500, 124498, 2, 185450000000, N'Hoàn tất', N'Đối soát cuối ngày hôm qua'),
(DATEADD(DAY, -2, GETDATE()), 118200, 118200, 0, 162100000000, N'Hoàn tất', N'Khớp 100% số liệu Core Banking');
GO
