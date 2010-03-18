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

function BindSuggestEvent() {
    $('#txtSearchKeyword').keyup(
        function(evt) {
            if (evt.keyCode == 13) {

            }
            else {
                $('.searchPannel .searchResult').show('slow');
                $('.searchPannel .searchResult').load('/Service/GetSuggestThemes/' + $(this).val() + ',3');
            }
        }
    );
    $('.searchPannel a.btn').click(
        function() {
            $('.searchPannel .searchResult .list').html(' No Result. Please try another key word.');
            $('.searchPannel .searchResult').hide('slow');
        }
    );
}

$(document).ready(
    function() {
        BindSuggestEvent();
    }
);