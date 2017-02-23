using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExpenseReimbursment.Startup))]
namespace ExpenseReimbursment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
