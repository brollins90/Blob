$(document).ready(function() {
    beforeBindings();
    //loadDashboard();
});

function beforeBindings() {
    bindAllModals();
    //bindPaginators();
    bindPaginator("#customerPageContentPager a[href]", "#customerPageContent");
    bindPaginator("#customerGroupPageContentPager a[href]", "#customerGroupPageContent"); 
    bindPaginator("#dashDevicesLargeContentPager a[href]", "#dashDevicesLargeContent");
    bindPaginator("#devicePageContentPager a[href]", "#devicePageContent");
    bindPaginator("#performanceRecordPageContentPager a[href]", "#performanceRecordPageContent");
    bindPaginator("#statusRecordPageContentPager a[href]", "#statusRecordPageContent");
};

//var site = site || {};
//site.baseUrl = site.baseUrl || "";
//function loadDashboard() {
//    $(".partialContents").each(function (index, item) {
//        var url = site.baseUrl + $(item).data("url");
//        if (url && url.length > 0) {
//            $(item).load(url, function () { PagedListAjaxOnComplete(); });
//        }
//    });
//};

function PagedListAjaxOnComplete() {
    beforeBindings();
};

function bindPaginator(pager, content) {
    $(document).on("click", pager, function () {
        //history.pushState(null, null, $(this).attr("href"));
        $.ajax({
            url: this.href,
            type: "GET",
            cache: false,
            success: function (result) {
                $(content).html(result);
            }
        });
        return false;
    });
}
//function bindPaginators() {
//    var pager = $(".pagination-container a[href]");
//    console.log(pager);
//    if ($(pager).length) {
//        var content = $(pager).parent().closest(".pageContent");
//        console.log(content);
//        if ($(content).length) {
//            $(document).on("click", pager, function() {
//                history.pushState(null, null, $(this).attr("href"));
//                $.ajax({
//                    url: this.href,
//                    type: "GET",
//                    cache: false,
//                    success: function(result) {
//                        $(content).html(result);
//                    }
//                });
//                return false;
//            });
//        }
//    }
//};

function bindAllModals() {
    $.ajaxSetup({ cache: false });
    $(document).on("click", "a[data-modal]", function (e) {
        //history.pushState(null, null, $(this).attr("href"));
        $("#myModalContent").load(this.href, function () {
            $("#myModal").modal({ keyboard: true}, "show");
            bindForm(this);
        });
        return false;
    });
};

function bindForm(dialog) {
    $("form", dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $("#myModal").modal("hide");
                    $('#replacetarget').load(result.url);
                    //Refresh
                    location.reload();
                } else {
                    $("#myModalContent").html(result);
                    bindForm(dialog);
                }
            },
            error: function (result) {
                $("#myModalContent").html(result.responseText);
                bindForm(dialog);                
            }
        });
        return false;
    });
};
