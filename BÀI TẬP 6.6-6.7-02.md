**BÀI TẬP 6.6: HỆ THỐNG MDI ĐIỀU HÀNH MẠNG LƯỚI CÂY ATM  NGÂN HÀNG (KIẾN TRÚC MDI & ASYNC/AWAIT)** 

**1\. Mục tiêu kiến thức** 

• Xây dựng kiến trúc giao diện đa cửa sổ MDI (IsMdiContainer \= true) quản  lý phân hệ nghiệp vụ ngân hàng. 

• Ngăn chặn hành vi mở trùng lặp (Duplicate) Form con khi nhấn Menu nhiều  lần. 

• Áp dụng async/await và Task.Run để xử lý tác vụ đối soát giao dịch ATM  khối lượng lớn mà không làm treo giao diện (UI Thread). 

**2\. Yêu cầu cấu trúc hệ thống** 

• **Form Cha MDI (frmBankAtmMain):** 

o Thuộc tính IsMdiContainer \= true. 

o MenuStrip: 

▪ **Hệ thống:** *Đăng xuất, Khóa màn hình, Thoát*. 

▪ **Quản lý ATM:** *Danh sách Trạm ATM & Tồn quỹ, Đối soát  Giao dịch Cuối ngày*. 

▪ **Cửa sổ:** *Xếp tầng (Cascade), Xếp ngang (Tile Horizontal), Xếp  dọc (Tile Vertical), Đóng tất cả*. 

o StatusStrip: Hiển thị *Tên Giao dịch viên trực*, *Trạng thái kết nối Core  Banking*, và thanh tiến trình ToolStripProgressBar. 

• **Form Con 1 (frmAtmStations):** Quản lý danh sách các trạm ATM (Mã  trạm, Địa chỉ đặt máy, Số tiền tồn quỹ hiện tại, Trạng thái hoạt động). Có sử dụng OpenFileDialog để đính kèm ảnh chụp hiện trường trạm ATM. 

• **Form Con 2 (frmAtmReconciliation):** Phân hệ đối soát sao kê giao dịch  ATM bất đồng bộ. 

**3\. Yêu cầu nghiệp vụ** 

1\. **Điều hướng MDI chuẩn mực:**  
o Khi chọn một chức năng trên Menu **Quản lý ATM**, hệ thống duyệt  qua this.MdiChildren. Nếu Form con đã tồn tại thì gọi Activate() để đưa lên trên cùng; nếu chưa có thì khởi tạo mới, gán MdiParent \= this  và hiển thị. 

o Các chức năng sắp xếp cửa sổ trong Menu **Cửa sổ** phải hoạt động  đúng với toàn bộ Form con đang mở. 

2\. **Đối soát giao dịch bất đồng bộ (async/await &**  

**CancellationTokenSource):** 

o Tại Form con frmAtmReconciliation, thiết kế nút **\[Chạy Đối Soát Tự Động\]** và nút **\[Dừng Đối Soát\]**. 

o Khi nhấn chạy, giả lập quá trình tải và đối chiếu hàng trăm nghìn giao  dịch rút tiền/chuyển khoản từ máy chủ mất từ **4 \- 6 giây** (dùng await  Task.Run(...) kết hợp Task.Delay). 

o **Yêu cầu UI Thread:** Trong lúc tiến trình đối soát đang chạy và  ToolStripProgressBar trên Form cha đang chạy tiến độ, người dùng  vẫn có thể mở Form con khác, di chuyển hoặc thu phóng cửa sổ mà  ứng dụng **tuyệt đối không bị đơ (Not Responding)**. 

**BÀI TẬP 6.7: TRUNG TÂM ĐIỀU ĐỘ & GIÁM SÁT LƯỚI ĐIỆN THỜI  GIAN THỰC (CROSS-THREAD SAFETY)** 

**1\. Mục tiêu kiến thức** 

• Xây dựng tiến trình chạy ngầm lặp định kỳ (Background Loop) mô phỏng hệ thống SCADA thu thập thông số trạm biến áp. 

• Tuân thủ nghiêm ngặt quy tắc **Thread Safety** trong WinForms khi cập nhật  dữ liệu từ Background Thread lên UI Control thông qua this.Invoke() hoặc  Control.InvokeRequired. 

**2\. Yêu cầu giao diện (frmPowerGridMonitor)** 

• **Khu vực điều khiển:** Nút **\[Bắt đầu Giám sát SCADA\]**, Nút **\[Ngắt Kết  nối\]**, ComboBox chọn Trạm biến áp (ví dụ: *Trạm 110kV Hà Đông, Trạm  220kV Chèm*).  
• **Khu vực bảng thông số trực quan (Real-time Dashboard):** o Label (lblVoltage): Hiển thị Điện áp hiện tại (����). 

o Label (lblLoadPercent): Hiển thị Mức mang tải (%) — Tự động đổi  màu chữ sang **Đỏ** và hiện cảnh báo nếu vượt ngưỡng an toàn (\> 85%), màu **Xanh lá** nếu ổn định. 

o StatusStrip: Hiển thị thời gian nhận gói tin cuối cùng (HH:mm:ss) và  tổng số chu kỳ đã quét. 

• **Khu vực nhật ký sự cố:** DataGridView (dgvGridLogs) cấu hình thủ công  bằng code, tự động ghi nhận các mốc thời gian mà trạm biến áp rơi vào trạng  thái **Quá tải**. 

**3\. Yêu cầu nghiệp vụ & Kỹ thuật Thread Safety** 

1\. **Tiến trình thu thập dữ liệu ngầm:** 

o Khi nhấn **\[Bắt đầu Giám sát SCADA\]**, khởi chạy một Task.Run chạy  vòng lặp ngầm, định kỳ **2 giây/lần** sinh ngẫu nhiên thông số vận hành:  Điện áp (108���� − 115����) và Mức tải (50% − 98%). 

2\. **Quy tắc Thread Safety (Bắt buộc):** 

o Khi nhận thông số mới từ Thread phụ, **nghiêm cấm** mọi hành vi truy  cập hoặc gán giá trị trực tiếp vào lblVoltage, lblLoadPercent,  

StatusStrip hay dgvGridLogs. 

o Phải kiểm tra if (this.InvokeRequired) và sử dụng this.Invoke(new  Action(() \=\> { ... })) để chuyển ngữ cảnh thực thi về Main UI Thread  trước khi cập nhật giao diện hoặc thêm dòng cảnh báo quá tải vào  DataGridView. 

o **Tiêu chí kiểm thử:** Khi chạy ứng dụng ở chế độ Debug (F5) trên  Visual Studio, hệ thống phải cập nhật thông số liên tục mượt mà và  **tuyệt đối không được ném ra ngoại lệ** InvalidOperationException:  Cross-thread operation not valid.  
**YÊU CẦU NỘP BÀI & CẤU TRÚC MÃ NGUỒN**  

Áp dụng quy chuẩn nộp bài trên GitHub với cấu trúc Solution 3-Tier  (Database/ScriptDB.sql, DTO, DAL, BUS, GUI, README.md kèm 5 ảnh chụp  màn hình chức năng) tương tự như hướng dẫn của các bài 6.3 – 6.5.