SELECT 
    d.Name AS TenThietBi, 
    d.IpAddress, 
    s.Name AS TenDiaDiem
FROM Devices d
JOIN Sites s ON d.SiteId = s.Id;