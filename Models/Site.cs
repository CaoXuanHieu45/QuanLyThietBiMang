using System.ComponentModel.DataAnnotations;

namespace QuanLyThietBiMang.Models
{
    public class Site
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên địa điểm là bắt buộc.")]
        public string Name { get; set; } = string.Empty;

        // Navigation property
        public ICollection<Device> Devices { get; set; } = new List<Device>();
    }
}