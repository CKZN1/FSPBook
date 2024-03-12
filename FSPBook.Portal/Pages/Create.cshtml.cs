using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FSPBook.Data;
using FSPBook.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FSPBook.Pages;
public class CreateModel : PageModel
{
    public IDbContext DbContext { get; set; }

    public List<Profile> Profiles { get; set; }

    public bool Success { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Choose a person to post on behalf of")]
    [Range(1, 10000, ErrorMessage = "Choose a person to post on behalf of")]
    public int ProfileId { get; set; }
    [BindProperty]
    [Required(ErrorMessage = "Write a post")]
    [MinLength(1, ErrorMessage = "Post needs some content")]
    public string ContentInput { get; set; }

    public CreateModel(IDbContext context)
    {
        DbContext = context;
    }

    public async Task OnGetAsync()
    {
        await LoadProfiles();
    }

    public async Task OnPostAsync()
    {
        if (ProfileId > 0)
        {
            await DbContext.AddPost(ProfileId, ContentInput);
            Success = true;
        }

        await LoadProfiles();
    }

    private async Task LoadProfiles()
    {
        Profiles = await DbContext.GetAllProfiles();
    }
}
