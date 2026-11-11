SELECT 
    d.Name AS TenThietBi, 
    d.IpAddress, 
    s.Name AS TenDiaDiem
FROM Devices d
JOIN Sites s ON d.SiteId = s.Id;


-- Xem tất cả thông tin (bao gồm cột Id) của bảng Sites
SELECT * FROM Sites;

-- Xem tất cả thông tin (bao gồm cột Id) của bảng Devices
SELECT * FROM Devices;

-- Kiểm tra xem site của thiết bị thay đổi chưa:
SELECT Id, Name, SiteId FROM Devices WHERE Id = 22;

--Kiểm tra xem bản ghi lịch sử mới đã được tạo chưa:
SELECT * FROM DeviceTransfers WHERE DeviceId = 22;

--Để thử xóa thiết bị chưa có lịch sử, chạy câu SQL này trong SSMS:
SELECT d.Id, d.Name
FROM Devices d
WHERE NOT EXISTS (
    SELECT 1 FROM DeviceTransfers t
    WHERE t.DeviceId = d.Id
);

--Để xem toàn bộ lịch sử chuyển trong SSMS, chạy:
SELECT *
FROM DeviceTransfers
ORDER BY TransferredAtUtc DESC;


--Muốn xem danh sách tất cả thiết bị đã từng chuyển, chạy truy vấn này trong SSMS:
SELECT
    DeviceId,
    FromSiteId,
    ToSiteId,
    TransferredAtUtc
FROM DeviceTransfers
ORDER BY TransferredAtUtc DESC;