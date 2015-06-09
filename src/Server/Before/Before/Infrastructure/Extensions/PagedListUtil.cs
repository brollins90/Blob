using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Blob.Contracts.Models;
using PagedList;
using PagedList.Mvc;

namespace Before.Infrastructure.Extensions
{
    public static class PagedListUtil
    {

        public static IPagedList GetPagedListMetaData<T>(this IPageInfoVm<T> inList)
        {
            return new StaticPagedList<T>(inList.Items, inList.PageNum, inList.PageSize, inList.TotalCount).GetMetaData();
        }

        public static MvcHtmlString BeforePagedListPager<T>(
            this System.Web.Mvc.HtmlHelper html,
            IEnumerable<T> subset,
            IPagedList metaData,
            Func<int, string> generatePageUrl,
            string updateTargetId)
        {
            PagedListRenderOptions options = PagedListRenderOptions.EnableUnobtrusiveAjaxReplacing(
                                                                                                   new PagedListRenderOptions
                                                                                                   {
                                                                                                       LinkToFirstPageFormat = String.Format("<<"),
                                                                                                       LinkToPreviousPageFormat = String.Format("<"),
                                                                                                       LinkToNextPageFormat = String.Format(">"),
                                                                                                       LinkToLastPageFormat = String.Format(">>"),
                                                                                                       MaximumPageNumbersToDisplay = 5,
                                                                                                       DisplayEllipsesWhenNotShowingAllPageNumbers = false
                                                                                                   },
                                                                                                   new AjaxOptions
                                                                                                   {
                                                                                                       UpdateTargetId = updateTargetId,
                                                                                                       HttpMethod = "GET",
                                                                                                       InsertionMode = InsertionMode.Replace,
                                                                                                       LoadingElementId = "progressPnl",
                                                                                                       OnComplete = "PagedListAjaxOnComplete"
                                                                                                   });

            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.MergeAttribute("id", updateTargetId + "Pager");
            tagBuilder.InnerHtml = html.PagedListPager(new StaticPagedList<T>(subset, metaData), generatePageUrl, options).ToHtmlString();
            return new MvcHtmlString(tagBuilder.ToString());
        }
    }
}