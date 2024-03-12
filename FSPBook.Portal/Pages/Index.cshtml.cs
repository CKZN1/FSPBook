using FSPBook.Data;
using FSPBook.Data.Entities;
using FSPBook.Portal.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using FSPBook.Portal.Services;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;

namespace FSPBook.Pages;
public class IndexModel : PageModel
{
    public IDbContext DbContext { get; set; }
    public IConfiguration Configuration { get; set; }
    public INewsService NewsService { get; set; }
    public PaginatedList<Post> Posts { get; set; }
    public List<Datum> NewsItems { get; set; }

    public IndexModel(IDbContext context, IConfiguration configuration, INewsService newsService)
    {
        DbContext = context;
        Configuration = configuration;
        NewsService = newsService;
    }

    public async Task OnGet(int? pageIndex)
    {
        var posts = DbContext.GetLatestPosts();        

        short pageSize = Convert.ToInt16(Configuration.GetSection("PageSize").Value);
        Posts = PaginatedList<Post>.Create(
            posts, pageIndex ?? 1, pageSize);

        NewsItems = await NewsService.GetNews();
    }
}