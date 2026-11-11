using Microsoft.EntityFrameworkCore;
using QuanLyThietBiMang.Models;
using QuanLyThietBiMang.Repositories;

namespace QuanLyThietBiMang.Data.Repositories;

public sealed class SiteRepository : ISiteRepository
{
    private readonly AppDbContext _context;

    public SiteRepository(AppDbContext context) => _context = context;

    public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default) =>
        _context.Sites.AnyAsync(site => site.Id == id, cancellationToken);

    public Task<List<Site>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _context.Sites.AsNoTracking().OrderBy(site => site.Name).ToListAsync(cancellationToken);
}
