using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Tasky.Web.Pages;

public class IndexModel : TaskyPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
