# Quản lý thiết bị mạng

1.Cấu hình kết nối SQL Server
Mở file `appsettings.json` và cấu hình chuỗi kết nối (Connection String) trỏ tới SQL Server:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=NetworkDeviceDb;Trusted_Connection=True;TrustServerCertificate=True"
}

2.Chạy Migration và tạo Database
Chạy lệnh sau tại thư mục gốc của dự án:

Bash
dotnet ef database update

3.Khởi động API
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


Đề bài demo Chuyển thiết bị mạng giữa các địa điểm

1.Cách chạy Migration:
dotnet ef database update

2.Request mẫu để chuyển thiết bị:(chạy trên postman)
+Method: POST

+URL: http://localhost:5272/api/devices/{id}/transfers (Ví dụ: http://localhost:5272/api/devices/22/transfers)

+Headers: Content-Type: application/json

+Body (JSON):
	{
  	"toSiteId": 2
	}

3.Sơ đồ kiến trúc luồng xử lý:
Controller -> Service -> Repository / Unit Work -> SQL Server