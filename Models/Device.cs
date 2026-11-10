using System.ComponentModel.DataAnnotations;

namespace QuanLyThietBiMang.Models
{
    public class Device
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên thiết bị là bắt buộc.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Địa chỉ IP là bắt buộc.")]
        public string IpAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Trạng thái là bắt buộc.")]
        public string Status { get; set; } = string.Empty; // "Online" hoặc "Offline"

        [Required(ErrorMessage = "SiteId là bắt buộc.")]
        public int SiteId { get; set; }

        // Navigation property
        public Site? Site { get; set; }
    }
}