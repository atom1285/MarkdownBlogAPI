using Microsoft.EntityFrameworkCore;

namespace MarkdownBlogAPI.DataTransferObjects;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Blog> Blogs { get; set; }
}