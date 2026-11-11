# Quản lý thiết bị mạng

Web API quản lý thiết bị và địa điểm, xây dựng bằng ASP.NET Core 9, Entity Framework Core 9 và SQL Server.

## Chạy dự án

1. Cấu hình `ConnectionStrings:DefaultConnection` trong `appsettings.Development.json` hoặc User Secrets.
2. Tại thư mục dự án, khôi phục package và áp dụng migration:

   ```powershell
   dotnet restore
   dotnet ef database update
   ```

3. Khởi chạy API:

   ```powershell
   dotnet run
   ```

Ứng dụng cũng gọi `Database.Migrate()` khi khởi động. Swagger được bật trong môi trường Development.

## Điều chuyển thiết bị

Gửi request `POST /api/devices/{id}/transfers` để chuyển thiết bị. Lấy địa điểm đích từ `GET /api/sites`.

Ví dụ với thiết bị ID `28`:

```http
POST http://localhost:5272/api/devices/28/transfers
Content-Type: application/json
```

```json
{
  "toSiteId": 2
}
```

Điều chuyển thành công trả `201 Created`, đồng thời cập nhật `Devices.SiteId` và ghi lịch sử vào `DeviceTransfers`.

## API chính

- `GET /api/devices` — danh sách thiết bị
- `GET /api/devices/{id}` — thiết bị theo mã
- `POST /api/devices` — tạo thiết bị
- `PUT /api/devices/{id}` — cập nhật tên, IP và trạng thái; đổi địa điểm phải dùng API điều chuyển
- `DELETE /api/devices/{id}` — xóa thiết bị nếu chưa có lịch sử điều chuyển
- `POST /api/devices/{id}/transfers` — điều chuyển thiết bị
- `GET /api/devices/{id}/transfers` — lịch sử điều chuyển, mới nhất trước
- `GET /api/sites` — danh sách địa điểm

Thiết bị không tồn tại trả `404`; địa điểm đích không tồn tại trả `400`; địa điểm đích trùng địa điểm hiện tại trả `409`. `PUT` trả `409` nếu request hợp lệ nhưng cố đổi `SiteId`. `DELETE` trả `409` nếu thiết bị đã có lịch sử điều chuyển.

## Tổ chức mã nguồn

```text
Controllers                         API: nhận request và ánh xạ kết quả sang HTTP
Services                            Application: quy tắc nghiệp vụ
Repositories                        Application: interface repository và Unit of Work
Data                                Infrastructure: DbContext và cấu hình EF Core
  Repositories                      Infrastructure: triển khai repository bằng EF Core
  UnitOfWork                        Infrastructure: triển khai Unit of Work
Migrations                          Infrastructure: lịch sử thay đổi schema
Models                              Entity và request/response model dùng chung
```

Luồng điều chuyển: `Controller → DeviceCommandService → Repository / UnitOfWork → AppDbContext → SQL Server`.
Thiết bị và bản ghi lịch sử được cập nhật trên cùng `AppDbContext`; service gọi `SaveChangesAsync` một lần.

## Kiểm chứng SQL

Mở `verify.sql`, chỉnh `@DeviceId` theo thiết bị cần kiểm tra rồi chạy hai câu truy vấn để xem địa điểm hiện tại và lịch sử điều chuyển.
