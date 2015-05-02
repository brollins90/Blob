using System.Web.Mvc;

namespace Before
{
    public partial class Startup
    {
        public void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
