
$(function () {
    bindAllModals();
    $('#devicePageContentPager').on('click', 'a', function () {
        $.ajax({
            url: this.href,
            type: 'GET',
            cache: false,
            success: function (result) {
                $('#devicePageContent').html(result);
            }
        });
        return false;
    });
    //bindPaginator($("performanceRecordPageContentPager a[href]"), $("performanceRecordPageContent"));
});

//function bindPaginator(pager, content) {
//    $(pager).on("click", function () {
//        $.ajax({
//            url: $(this).attr("href"),
//            type: 'GET',
//            cache: false,
//            success: function (result) {
//                $(content).html(result);
//                bindAllModals();
//            }
//        });
//    });
//}

function bindAllModals() {
    $.ajaxSetup({ cache: false });
    $("a[data-modal]").on("click", function (e) {
        $('#myModalContent').load(this.href, function () {
            $('#myModal').modal({
                keyboard: true
            }, 'show');
            bindForm(this);
        });
        return false;
    });
};

function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    //Refresh
                    location.reload();
                } else {
                    $('#myModalContent').html(result);
                    bindForm(dialog);
                }
            }
        });
        return false;
    });
}


//(function() {
//    $(document).ready(function() {

//        $(document).on("click", "#performanceRecordPageContentPager a[href]", function() {
//            $.ajax({
//                url: $(this).attr("href"),
//                type: 'GET',
//                cache: false,
//                success: function(result) {
//                    $('#performanceRecordPageContent').html(result);
//                    bindAllModals();
//                }
//            });
//        });

//        $(document).on("click", "#statusRecordPageContentPager a[href]", function() {
//            $.ajax({
//                url: $(this).attr("href"),
//                type: 'GET',
//                cache: false,
//                success: function(result) {
//                    $('#statuRecordPageContent').html(result);
//                    bindAllModals();
//                }
//            });
//        });

//        $(document).ready(function() {
//            $(document).on("click", "#deviceageContentPager a[href]", function() {
//                $.ajax({
//                    url: $(this).attr("href"),
//                    type: 'GET',
//                    cache: false,
//                    success: function(result) {
//                        $('#devicePageContent').html(result);
//                        bindAllModals();
//                    }
//                });
//            });
//        });

//        return false;

//    });
//})();

