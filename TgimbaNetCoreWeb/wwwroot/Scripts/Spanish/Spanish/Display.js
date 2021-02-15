﻿function GetSelectedList() {
    var availablelistTarget = $("#listOptionsHtml");

    return availablelistTarget.val();
}

function ResetDropDownName(listName) {
    $("#listOptionsHtml").text = listName;
}

function CreateDropDownList(list, id) {
    var headerPrefix = '<select id="' + id + '" name="' + id + '" class="yellowBlue">';
    var headerSuffix = '</select>';
    var elementPrefix = '<option class="yellowBlue" >';
    var elementSuffix = '</option>';
    var listOptionsHtml = null;
    list = list.split(',');

    listOptionsHtml = headerPrefix;

    for (i = 0; i < list.length; i++) {
        listOptionsHtml = listOptionsHtml + '<option class="yellowBlue" >' + list[i] + elementSuffix;
    }

    listOptionsHtml = listOptionsHtml + headerPrefix;

    $(document).on('change', "#" + id, function () {
        listSelectIndexChange();
    });

    return listOptionsHtml;
}

function ResetPage() {
    $("#createEditList").html('');
    $("#listTarget").html('');
}

function ClearAlert() {
    $("#MessageHolder").html('');
}

function Alert(msg) {
    ClearAlert();
    $("#MessageHolder").append('<p>' + msg + '</p>');
    BuildMnuButton();
}

function SetupDisplayList() {
    var selectedList = GetSelectedList();
    var list = GetSelectedListContents(selectedList);

    if (list == null) {
        return;
    }

    SetLocalStorageList(list);
    list = list.split(";");
    var firstListItem = list[0].split(",");
    var firstListItemId = firstListItem[0];
    SetLocalStorageCurrentItemId(firstListItemId);

    //if (selectedList === 'CurrentVerbList') {
    //    LoadVerb(SPANISH_VERB_PATH + firstListItem[1] + '.json');
    //} else {
    //    var firstListItemId = firstListItem[0];
    //    SetLocalStorageCurrentItemId(firstListItemId);
    //}
}

function BuildMnuButton() {
    var mnuButtons = $("#mnuButtons");
    var mnuButton = '<input type="button" value="Menu" class="yellowBlue" id="mnuButton" name="mnuButton" />';

    mnuButtons.html('');
    mnuButtons.append(mnuButton);

    $("#mnuButton").click(mnuButtonClick);//createListButtonClick);
}

function BuildMnuButtons() {
    var mnuButtons = $("#mnuButtons");
    var btnCreateList = '<input type="button" value="Create" class="yellowBlue" id="createListButton" name="createListButton" />';
    var btnManageList = null;
    var btnDeleteList = null;
    var btnCancel = '<input type="button" value="Cancel" class="yellowBlue" id="cancelListItemButton" name="cancelListItemButton" />';

    if (IsUserListSelected() == true) {
        btnManageList = '<input type="button" value="Add" class="yellowBlue" id="addListItemButton" name="addListItemButton" />'
        btnDeleteList = '<input type="button" value="Delete" class="yellowBlue" id="deleteListItemButton" name="deleteListItemButton" />'
    }
    else {
        btnManageList = '<input type="button" value="Add" class="yellowBlue" id="addListItemButton" name="addListItemButton"  disabled/>'
        btnDeleteList = '<input type="button" value="Delete" class="yellowBlue" id="deleteListItemButton" name="deleteListItemButton"  disabled/>'
    }

    mnuButtons.html('');
    mnuButtons.append(btnCreateList + btnManageList + btnDeleteList + btnCancel + '<br/><br/>');

    $("#createListButton").click(createListButtonClick);
    $("#addListItemButton").click(addListItemButtonClick);
    $("#deleteListItemButton").click(deleteListItemButtonClick);
    $("#cancelListItemButton").click(cancelListItemButtonClick);
}

function BuildCreateEditListDisplay(create) {
    var mgnList = $("#createEditList");
    mgnList.html('');

    if (create == 'true')
        BuildCreateListDisplay(mgnList);
    else
        BuildEditListDisplay(mgnList);
}

function BuildCreateListDisplay(mgnList) {
    var listName = '<input type="text" class="yellowBlue" id="listName" name="listName" />';
    var btnSubmit = '<input type="button" value="Submit" class="yellowBlue" id="submitNewListButton" name="submitNewListButton" />'
    var btnCancel = '<input type="button" value="Cancel" class="yellowBlue" id="cancelNewListButton" name="cancelNewListButton" />'

    mgnList.append(listName + '<br/>' + btnSubmit + btnCancel + '<br/><br/>');

    $("#submitNewListButton").click(submitNewListButtonClick);
    $("#cancelNewListButton").click(createListButtonCancelClick);
}

function BuildEditListDisplay(mgnList) {
    var listItemEnglish = '<label>English</label><input type="text" class="yellowBlue" id="listItemEnglish" name="listItemEnglish" />';
    var listItemSpanish = '<label>Spanish</label><input type="text" class="yellowBlue" id="listItemSpanish" name="listItemSpanish" />';
    var btnSubmit = '<input type="button" value="Submit" class="yellowBlue" id="submitNewListItemButton" name="submitNewListItemButton" />'

    mgnList.append(listItemEnglish + '<br/>' + listItemSpanish + '<br/>' + btnSubmit + '<br/><br/>');

    $("#submitNewListItemButton").click(submitNewListItemButtonClick);
}

function LanguageChange(visible) {
    var cbLanguage = $("#cbLanguageContainer");

    if (visible == true) {
        $("#cbLanguage").checked = false;
        cbLanguage.hide();
    }
    else
        cbLanguage.show();
}

function BuildListDisplay(availableLists) {
    var availablelistTarget = $("#availablelistTarget");
    var dropDownList = CreateDropDownList(availableLists, 'listOptionsHtml')
    var submitButton = '<input type="button" value="Submit" class="yellowBlue" id="submitListButton" name="submitListButton" />';
    var cbLanguage = '<div id="cbLanguageContainer" name="cbLanguageContainer"><input type="checkbox" id="cbLanguage" name="cbLanguage">English (off)/Spanish (on)</input></div>';

    availablelistTarget.html('');

    //availablelistTarget = LanguageChange(true, availablelistTarget);

    availablelistTarget.append(cbLanguage);
    availablelistTarget.append('<br/>');
    availablelistTarget.append('<br/>');
    availablelistTarget.append(dropDownList);
    availablelistTarget.append(submitButton);

    $("#submitListButton").click(submitListButtonClick);
}

function GetLanguage() {
    var cbLanguage = $("#cbLanguage");
    var language = null;

    if (cbLanguage.is(":checked"))
        language = 'Spanish';
    else
        language = 'English';

    return language;
}

function GetSpanishAnswerSection(spanishContent) {
    var splitSpanish = spanishContent.split("<br/>");
    var conjuncationSplit = '';
    var showConjuncationButton = '';
    var spanishContent = "<div class='masterConjuncationHide' id='SpanishContentsMaster' name='SpanishContentsMaster'>";
    var verbFrontDivPrefix = "<div class='conjuncationHide' id='";
    var verbFrontDivMiddle = "' name='";
    var verbFrontDivSuffix = "'>";
    var verbDivSuffix = "</div>";
    var curVerbDiv = '';

    spanishContent = splitSpanish[0] + '<br/>';

    for (var o = 1; o < splitSpanish.length; o++) {
        conjuncationSplit = splitSpanish[o].split(" ");
        conjuncationSplit[0] = conjuncationSplit[0].replace(":", "");
        var compareValue = conjuncationSplit[0];

        if (compareValue != "Indicative_Present"
            && compareValue != "Indicative_Preterite"
            && compareValue != "Indicative_Future")
            continue;

        curVerbDiv = verbFrontDivPrefix + 'Conjuncation' + o + verbFrontDivMiddle + 'Conjuncation' + o + verbFrontDivSuffix;

        for (var i = 1; i < conjuncationSplit.length; i++) {
            curVerbDiv = curVerbDiv + conjuncationSplit[i] + '<br/>';
            curVerbDiv = curVerbDiv.trim();
        }

        var continueLoop = 1;
        while (continueLoop) {
            if (curVerbDiv.substring(curVerbDiv.length - 5, curVerbDiv.length) == '<br/>')
                curVerbDiv = curVerbDiv.substring(0, curVerbDiv.length - 5);
            else
                continueLoop = 0;
        }


        showConjuncationButton = "<input type='button' class='yellowBlue' value='" + conjuncationSplit[0] + "' onclick='showConjuncationButton(" + o + ")' />";

        curVerbDiv = curVerbDiv + '<br/>' + verbDivSuffix + '<br/>';

        spanishContent = spanishContent + showConjuncationButton + curVerbDiv;
    }

    spanishContent = spanishContent + verbDivSuffix;

    return spanishContent;
}

function showSelectedConjuncation(clickedConjuncation) {
    var conjuncation = null;

    for (var i = 1; i <= TOTAL_SPANISH_CONJUNCATIONS; i++) {
        conjuncation = $(JQUERY_CTL_CONJUNCTION + i);
        if (conjuncation) {
            conjuncation.removeClass(CSS_CONJUNCTION_SHOW);
            conjuncation.addClass(CSS_CONJUNCTION_HIDE);
        }
    }

    conjuncation = $(JQUERY_CTL_CONJUNCTION + clickedConjuncation);
    conjuncation.removeClass(CSS_CONJUNCTION_HIDE);
    conjuncation.addClass(CSS_CONJUNCTION_SHOW);
}

function ClearTermDisplay() {
    var listDisplay = $("#listTarget");
    listDisplay.html('');
    var mgnList = $("#createEditList");
    mgnList.html('');
}

function BuildTermDisplay() {
    var selectedList = GetSelectedList();
    var curItem = GetListItemById();
    var listDisplay = $("#listTarget");

    if (curItem == null || curItem == 'null' || curItem.length <= 1) {
        Alert(NO_MORE_ITEMS);
        return;
    }

    listDisplay.html('');

    if (selectedList === 'CurrentVerbList' || selectedList === 'CompleteVerbList') {
        var verbUrl = SPANISH_VERB_PATH + curItem[1] + '.json'; 
        LoadVerb(verbUrl, listDisplay);
    } else {
        buildRegularListTermDisplay(curItem, listDisplay);
    }
}

function buildRegularListTermDisplay(curItem, listDisplay) {
    var nextButton = '<input type="button" class="yellowBlue" value="Next" id="nextTargetClick" name="nextTargetClick" />';
    var showAnswerButton = '<input type="button" class="yellowBlue" value="Show Answer" id="showAnswerClick" name="showAnswerClick" />';
    var answerPrefix = '<div class="answerHide  yellowBlue" id="answerDiv" name="answerDiv">';
    var answerSuffix = '</div>';
    var questionPrefix = '<div class="yellowBlue" >';
    var questionSuffix = '</div>';
    var answer = null;
    var language = GetLanguage();

    var contents = '';

    contents = contents + nextButton;
    contents = contents + showAnswerButton;
    contents = contents + '<br/>';

    if (language == 'English') {
        contents = contents + questionPrefix + curItem[1] + questionSuffix;
        answer = answerPrefix + GetSpanishAnswerSection(curItem[2]) + answerSuffix;
    }
    else {
        contents = contents + questionPrefix + curItem[2] + questionSuffix;
        answer = answerPrefix + curItem[1] + answerSuffix;
    }

    contents = contents + answer;

    listDisplay.append(contents);

    $("#nextTargetClick").click(nextTargetClick);
    $("#showAnswerClick").click(showAnswerClick);
}

function LoadVerb(verbUrl, listDisplay) {
    ServerCalls.GetJson(verbUrl, listDisplay);
};

function SetJson(verbJson, listDisplay) {
    var nextButton = '<input type="button" class="yellowBlue" value="Next" id="nextTargetClick" name="nextTargetClick" />';
    var showAnswerButton = '<input type="button" class="yellowBlue" value="Show Answer" id="showAnswerClick" name="showAnswerClick" />';
    var answerPrefix = '<div class="answerHide  yellowBlue" id="answerDiv" name="answerDiv">';
    var answerSuffix = '</div>';
    var questionPrefix = '<div class="yellowBlue" >';
    var questionSuffix = '</div>';
    var answer = null;

    var contents = '';

    contents = contents + nextButton;
    contents = contents + showAnswerButton;
    contents = contents + '<br/>';

    contents = contents + questionPrefix + verbJson.Name + questionSuffix;
    var jsonAnswerSection = GetSpanishVerbAnswerSectionJson(verbJson);
    answer = answerPrefix + jsonAnswerSection + answerSuffix;

    contents = contents + answer;

    listDisplay.append(contents);

    $("#nextTargetClick").click(nextTargetClick);
    $("#showAnswerClick").click(showAnswerClick);
};

function GetSpanishVerbAnswerSectionJson(verbJson) {
    spanishContent = verbJson.EnglishMeaning + '<br/>';
    
    spanishContent = InsertParticiple(spanishContent, "Present Participle", verbJson.PresentParticiple, 1);
    spanishContent = InsertParticiple(spanishContent, "Past Participle", verbJson.PastParticiple, 2);
    spanishContent = InsertTense(spanishContent, "Indicative - Present Tense", verbJson.Indicative.PresentTense, 3);
    spanishContent = InsertTense(spanishContent, "Indicative - Pretérito", verbJson.Indicative.Pretérito, 4);
    spanishContent = InsertTense(spanishContent, "Indicative - Conditional", verbJson.Indicative.Conditional, 5);
    spanishContent = InsertTense(spanishContent, "Indicative - Future", verbJson.Indicative.Future, 6);
    spanishContent = InsertTense(spanishContent, "Subjunctive - PresentTense", verbJson.Subjunctive.PresentTense, 7);
    spanishContent = InsertTense(spanishContent, "Subjunctive - Imperfect", verbJson.Subjunctive.Imperfect, 8);
    spanishContent = InsertTense(spanishContent, "Subjunctive - PresentTense", verbJson.Subjunctive.Future, 9);

    spanishContent = spanishContent + "<br/></div>";

    return spanishContent;
}

function InsertParticiple(spanishContent, title, participle, conjunctionIndex) {
    spanishContent = spanishContent + "<input type='button' class='yellowBlue' value='" + title + "' onclick='showConjuncationButton(" + conjunctionIndex + ")' />";
    spanishContent = spanishContent + "<div class='conjuncationHide' id='Conjuncation" + conjunctionIndex + "' name='Conjuncation" + conjunctionIndex + "'>";
    spanishContent = spanishContent + title + " - " +  participle + " <br/>";
    spanishContent = spanishContent + "</div><br/>";

    return spanishContent;
}
function InsertTense(spanishContent, title, verbJson, conjunctionIndex) {
    if (verbJson) {
        spanishContent = spanishContent + "<input type='button' class='yellowBlue' value='" + title + "' onclick='showConjuncationButton(" + conjunctionIndex + ")' />";
        spanishContent = spanishContent + "<div class='conjuncationHide' id='Conjuncation" + conjunctionIndex + "' name='Conjuncation" + conjunctionIndex + "'>";
        spanishContent = spanishContent + title + "<br/>";
        spanishContent = spanishContent + "Yo: " + verbJson.Yo + "<br/>";
        spanishContent = spanishContent + "Tu: " + verbJson.Tu + "<br/>";
        spanishContent = spanishContent + "ElEllaUsted: " + verbJson.ElEllaUsted + "<br/>";
        spanishContent = spanishContent + "Nosotros: " + verbJson.Nosotros + "<br/>";
        spanishContent = spanishContent + "Vosotros: " + verbJson.Vosotros + "<br/>";
        spanishContent = spanishContent + "EllosEllasUstedes: " + verbJson.EllosEllasUstedes + "<br/>";
        spanishContent = spanishContent + "</div><br/>";
    }

    return spanishContent;
}