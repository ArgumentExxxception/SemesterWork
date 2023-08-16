using Hangfire.Dashboard;

namespace App;

public class MyAuthorizationFilter: IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var ctx = context.GetHttpContext();
        return ctx.User.Identity.IsAuthenticated && ctx.User.IsInRole("admin");
    }
}