using HW2Sem.Entities;
using HW2Sem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages;

public class Feed2 : PageModel
{
    private readonly IPostRepository _postRepository;
    public IReadOnlyList<Post> Posts { get; set; } 

    public Feed2(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async void OnGetAsync()
    {
        var posts = await _postRepository.GetAllPosts();
        Posts = posts;
    }
}