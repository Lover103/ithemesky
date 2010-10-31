var __SortThemesCache = {};
function BindSortEvent() {
    $('ul.mainColTab a').click(
        function() {
            var sort = $(this).attr('sort');
            if (__SortThemesCache.hasOwnProperty(sort)) {
                $('#divSortThemes').html(__SortThemesCache[sort]);
            }
            else {
                $('#divSortThemes').html('loading...');
                $('#divSortThemes').load('/Service/GetSortThemes/' + sort + ',8', function(response, status, xhr) {
                    if (status == 'success') {
                        __SortThemesCache[sort] = response;
                    }
                });
            }
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
                if ($(this).val() != '') {
                    window.location.href = '/search/new/' + escape($(this).val()) + '/1';
                }
            }
            else {
                if ($(this).val() != '') {
                    $('.searchPannel .searchResult').show('slow');
                    $('.searchPannel .searchResult').load('/Service/GetSuggestThemes/' + $(this).val() + ',3');
                    $('.searchPannel a.btn').show();
                }
                else {
                    $('.searchPannel .searchResult').show();
                }
            }
        }
    );
    $('.searchPannel a.btn').click(
        function() {
            $('.searchPannel .searchResult .list').html(' No Result. Please try another key word.');
            $('.searchPannel .searchResult').hide('slow');
            $('#txtSearchKeyword').val('');
            $(this).hide();
        }
    );
}

function ChangeTags(urlWithoutTags) {
    var tags = '';
    $('.selectTags dl').each(
        function(i) {
            var tmpTags = '';
            $(this).find('li a.selected').each(
                function(j) {
                    if (j > 0) {
                        tmpTags += ',';
                    }
                    tmpTags += $(this).text();
                }
            );
                if (i > 0 && tmpTags != '' && tags != '') {
                tags += '-';
            }
            tags += tmpTags;
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
function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/; domain=ithemesky.com";
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}
var _themeId = 0;
function LoadComments(pageIndex) {
    $("#commentListContainer").html('loading comments ...');
    $("#commentListContainer").load('/Service/GetThemeComments/' + _themeId + ',' + pageIndex + ',5');
}
function PostCommentSuccess() {
    alert('post comment success.');
    $('#Content').html('');
    LoadComments(1);
}
var _softIdentify = '';
function LoadSoftComments(pageIndex) {
    $("#commentListContainer").html('loading comments ...');
    $("#commentListContainer").load('/Service/GetSoftComments/' + escape(_softIdentify) + ',' + pageIndex + ',5');
}
function PostSoftCommentSuccess() {
    alert('post comment success.');
    $('#Content').html('');
    LoadSoftComments(1);
}
function BindRateEvent() {
    $('.detailRate li a').click(
                function() {
                    $.get('/Service/RateTheme/' + _themeId + ',' + $(this).attr('value')
                    , function(data) {
                        if (data == '1') {
                            alert('thanks for your rating');
                        }
                        else if (data == '-1') {
                            alert('you have rated.');
                        }
                        else {
                            alert('ratting error. please try again later.');
                        }
                    });
                }
            );
}
$(document).ready(
    function() {
        BindSuggestEvent();
    }
);