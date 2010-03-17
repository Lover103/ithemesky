function BindSortEvent() {
    $('ul.mainColTab a').click(
        function() {
            $('#divSortThemes').html('loading...');
            $('#divSortThemes').load('/Service/GetSortThemes/' + $(this).attr('sort') + ',8');
            $('ul.mainColTab li').removeClass();
            $(this).parent('li').addClass('selected');
            $(this).blur();
        }
    );
}