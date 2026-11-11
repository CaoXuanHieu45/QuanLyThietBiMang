-- Đổi giá trị này thành ID thiết bị cần kiểm tra.
DECLARE @DeviceId int = 28;

-- 1. Địa điểm hiện tại của thiết bị.
SELECT
    d.Id AS DeviceId,
    d.Name AS DeviceName,
    d.SiteId,
    s.Name AS CurrentSiteName
FROM dbo.Devices AS d
JOIN dbo.Sites AS s ON s.Id = d.SiteId
WHERE d.Id = @DeviceId;

-- 2. Lịch sử điều chuyển của cùng thiết bị, mới nhất trước.
SELECT
    t.Id AS TransferId,
    t.DeviceId,
    fs.Name AS FromSiteName,
    ts.Name AS ToSiteName,
    t.TransferredAtUtc
FROM dbo.DeviceTransfers AS t
JOIN dbo.Sites AS fs ON fs.Id = t.FromSiteId
JOIN dbo.Sites AS ts ON ts.Id = t.ToSiteId
WHERE t.DeviceId = @DeviceId
ORDER BY t.TransferredAtUtc DESC;
