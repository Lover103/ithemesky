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
                if ($(this).val() != '') {
                    $('.searchPannel .searchResult').show('slow');
                    $('.searchPannel .searchResult').load('/Service/GetSuggestThemes/' + $(this).val() + ',3');
                }
                else {
                    $('.searchPannel .searchResult').hide();
                }
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

function ChangeTags(urlWithoutTags) {
    var tags = '';
    $('.selectTags dl').each(
        function(i) {
            if (i > 0 && tags != '') {
                tags += ' and ';
            }
            $(this).find('li a.selected').each(
                function(j) {
                    if (j > 0) {
                        tags += ',';
                    }
                    tags += $(this).text();
                }
            );
        }
    );
    window.location.href = urlWithoutTags + tags;
}

function InitTagsEvent(urlWithoutTags, currentTagNames) {
    $('.selectTags li a').each(
        function(i) {
            $(this).click(
                function() {
                    $(this).toggleClass('selected');
                    ChangeTags(urlWithoutTags);
                }
            );
            if (currentTagNames.indexOf($(this).text()) >= 0) {
                $(this).addClass('selected');
            }
        }
    );
}

$(document).ready(
    function() {
        BindSuggestEvent();
    }
);