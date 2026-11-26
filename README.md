# Báo Cáo Thực Hành Bài Tập Ngày 26/11 (Bài 6.6 - 6.7)

## MỤC 1: THÔNG TIN SINH VIÊN
- **Họ và tên sinh viên:** Lê Ngọc Quyền
- **Mã sinh viên:** 24810310259
- **Lớp / Khóa:** D19CNPM3
- **Môn học:** Lập trình C# / Windows Forms (ADO.NET, 3-Tier Architecture, MDI, Async/Await & Cross-Thread Safety)

---

## DANH SÁCH BÀI TẬP ĐÃ HOÀN THÀNH

1. **[Bài 6.6: Hệ thống MDI Điều hành Mạng lưới Cây ATM Ngân hàng (MDI & Async/Await)](./Bai6_6_BankAtmMdiSystem)**
   - Chuẩn 3-Tier (`DTO`, `DAL`, `BUS`, `GUI`), `Database/ScriptDB.sql` (`BankAtmMdiDB`), `App.config`, `Bai6_6_BankAtmMdiSystem.slnx`, 5 ảnh minh họa trong `images/`.
   - Form Cha MDI (`IsMdiContainer = true`), chống mở trùng lặp Form Con qua `this.MdiChildren` + `Activate()`, Menu Cửa sổ (Cascade, Tile Horizontal, Tile Vertical, Đóng tất cả), CRUD Trạm ATM + `OpenFileDialog`, và Đối soát cuối ngày bất đồng bộ (`async/await`, `Task.Run`, `CancellationTokenSource`) cập nhật `ToolStripProgressBar` không gây đơ UI Thread.

2. **[Bài 6.7: Trung tâm Điều độ & Giám sát Lưới điện Thời gian thực (Cross-Thread Safety)](./Bai6_7_PowerGridMonitor)**
   - Chuẩn 3-Tier (`DTO`, `DAL`, `BUS`, `GUI`), `Database/ScriptDB.sql` (`PowerGridDB`), `App.config`, `Bai6_7_PowerGridMonitor.slnx`, 5 ảnh minh họa trong `images/`.
   - Tiến trình SCADA chạy ngầm chu kỳ 2 giây/lần sinh Điện áp (`108kV - 115kV`) và Mức tải (`50% - 98%`), cập nhật an toàn luồng qua `if (this.InvokeRequired)` + `this.Invoke(...)`, tự động đổi màu Đỏ/Xanh lá và ghi log sự cố quá tải (`> 85%`) vào `dgvGridLogs` + SQL Server.
