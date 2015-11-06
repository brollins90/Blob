using System.Web.Mvc;
using Before.Filters;

namespace Before
{
    public partial class Startup
    {
        public void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new BeforeAuthorizeAttribute());
        }
    }
}
