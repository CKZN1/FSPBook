using FSPBook.Data.Entities;
using FSPBook.Data;
using FSPBook.Portal.Models;
using FSPBook.Portal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace FSPBook.Portal.Pages
{
    public class ProfileModel : PageModel
    {
        public IDbContext DbContext { get; set; }
        public PaginatedList<Post> Posts { get; set; }
        public Profile Profile { get; set; }
        public List<Datum> NewsItems { get; set; }
        public IConfiguration Configuration { get; set; }

        public ProfileModel(IDbContext context, IConfiguration configuration)
        {
            DbContext = context;
            Configuration = configuration;
        }

        public void OnGet(int? pageIndex, int authorId)
        {
            var posts = DbContext.GetPostsByAuthorId(authorId);

            short pageSize = Convert.ToInt16(Configuration.GetSection("PageSize").Value);
            Posts = PaginatedList<Post>.Create(
                posts, pageIndex ?? 1, pageSize);

            Profile = DbContext.GetProfileById(authorId);
        }
    }
}
