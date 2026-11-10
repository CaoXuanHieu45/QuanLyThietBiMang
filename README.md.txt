# Quản lý thiết bị mạng

## 1. Cấu hình kết nối SQL Server
Mở file `appsettings.json` và cấu hình chuỗi kết nối (Connection String) trỏ tới SQL Server:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=NetworkDeviceDb;Trusted_Connection=True;TrustServerCertificate=True"
}

2. Chạy Migration và tạo Database
Chạy lệnh sau tại thư mục gốc của dự án:

Bash
dotnet ef database update

3. Khởi động API
Chạy lệnh sau để khởi động ứng dụng:

Bash
dotnet run

4.Request mẫu (Postman)
-URL:http://localhost:5272/api/devices
-Method:POST
-BODY(JSON):
jSON
{
    "id": 11,
    "name": "Camera nhà cà phê",
    "ipAddress": "192.168.1.40",
    "status": "Online",
    "siteId": 1,
    "site": null
}
