using FSPBook.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace FSPBook.Data;
public class Context : DbContext, IDbContext
{
    public virtual DbSet<Profile> Profile { get; set; }
    public virtual DbSet<Post> Post { get; set; }

    public Context(DbContextOptions<Context> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
        .HasOne(e => e.Author)
        .WithMany(e => e.Posts)
        .HasForeignKey(e => e.AuthorId)
        .IsRequired();
    }

    public IQueryable<Post> GetLatestPosts()
    {
        return Post
        .Include(e => e.Author)
        .OrderByDescending(e => e.DateTimePosted)
        .AsQueryable();
    }

    public IQueryable<Post> GetPostsByAuthorId(int authorId)
    {
        return Post
        .Where(p => p.AuthorId == authorId)
        .Include(e => e.Author)
        .OrderByDescending(e => e.DateTimePosted)
        .AsQueryable();
    }

    public Profile GetProfileById(int profileId)
    {
        return Profile
        .Find(profileId);
    }

    public async Task<List<Profile>> GetAllProfiles()
    {
        return await Profile.ToListAsync();
    }

    public async Task<int> AddPost(int authorId, string content)
    {
        Post.Add(new Post { AuthorId = authorId, Content = content, DateTimePosted = DateTimeOffset.Now });
        return await SaveChangesAsync();
    }
}
