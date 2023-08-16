using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public async Task OnGetAsync(CancellationToken token)
    {
        _logger.LogInformation("Welcome the index page");
    }

    public void OnPost(int sum)
    {
    }
}
