using System.ComponentModel.DataAnnotations;

namespace QuanLyThietBiMang.Models;

public sealed class TransferRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "ToSiteId phải lớn hơn 0.")]
    public int ToSiteId { get; set; }
}
