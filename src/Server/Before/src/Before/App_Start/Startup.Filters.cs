namespace Before
{
    using System.Web.Mvc;
    using Filters;

    public partial class Startup
    {
        public void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new BeforeAuthorizeAttribute());
        }
    }
}