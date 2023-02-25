// script pour l'interface de gestion de sélection avec deux <select...>
// Il faut utiliser le fichier de styles css/flashButtons.css les <div> de
// classe MoveLeft et MoveRight
//
// auteur : Nicolas Chourot

$(document).ready(initUI);

function initUI() {
    $('#MoveLeft').hide();
    $('#MoveRight').hide();
    sortAllSelect();
    $('#UnselectedItems').change(function () {
        $('#UnselectedItems option:selected').each(function () {
            $("#SelectedItems option:selected").prop("selected", false);
            $('#MoveLeft').show();
            $('#MoveRight').hide();
        });
    });

    $('#SelectedItems').change(function () {
        $('#SelectedItems option:selected').each(function () {
            $("#UnselectedItems option:selected").prop("selected", false);
            $('#MoveLeft').hide();
            $('#MoveRight').show();
        });
    });

    $(document).on('submit', 'form', function () {
        $('#SelectedItems option').prop('selected', true);
    });

    ///////////////////////////////////////////////////////////////////
    // On the click event on the image id="MoveLeft"
    ///////////////////////////////////////////////////////////////////
    $("#MoveLeft").on('click', function () {
        $('#UnselectedItems option:selected').each(function () {
            $(this).remove();
            $(this).prop("selected", false);
            $('#SelectedItems').append($(this));
            sortSelect("SelectedItems");
            scrollTo("#SelectedItems", $(this).offset().top);
            $('#MoveLeft').hide();
        });
    });

    ///////////////////////////////////////////////////////////////////
    // On the click event on the image id="MoveRight"
    ///////////////////////////////////////////////////////////////////
    $("#MoveRight").on('click', function () {
        $('#SelectedItems option:selected').each(function () {
            $(this).remove();
            $(this).prop("selected", false);
            $('#UnselectedItems').append($(this));
            sortSelect("UnselectedItems");
            scrollTo("#UnselectedItems", $(this).offset().top);
            $('#MoveRight').hide();
        });
    });
}

///////////////////////////////////////////////////////////////////
// Sort text items of a listbox
///////////////////////////////////////////////////////////////////
function normalize(str) {
    return str.normalize("NFD").replace(/[\u0300-\u036f]/g, "");
}
function sortSelect(selectId) {
    $("#" + selectId).html($("#" + selectId + " option").sort(function (a, b) {
        return a.text === b.text ? 0 : normalize(a.text) < normalize(b.text) ? -1 : 1;
    }))
}

function sortAllSelect() {
    sortSelect("SelectedItems");
    sortSelect("UnselectedItems");
}

function scrollTo(selectId, optionTop) {
    var selectObj = $(selectId);
    var selectTop = selectObj.offset().top;
    selectObj.scrollTop(selectObj.scrollTop() + (optionTop - selectTop));
}