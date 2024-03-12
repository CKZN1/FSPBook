using FSPBook.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSPBook.Data
{
    public interface IDbContext
    {
        DbSet<Post> Post { get; set; }
        DbSet<Profile> Profile { get; set; }
        IQueryable<Post> GetLatestPosts();
        IQueryable<Post> GetPostsByAuthorId(int authorId);
        Profile GetProfileById(int profileId);
        Task<int> AddPost(int authorId, string content);
        Task<List<Profile>> GetAllProfiles();
    }
}