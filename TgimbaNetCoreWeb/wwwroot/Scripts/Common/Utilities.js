﻿function isNullUndefined(object, location, expectedObj) {   
    if (object === undefined) {   
        Error(expectedObj + ' is undefined at ' + location);
    }

    if (object === null) {           
        Error(expectedObj + ' is null  at ' + location);
    }                     
}

function booleanIsTrue(booleanValue) {
    if (booleanValue 
        && (booleanValue === "true" || booleanValue === true || booleanValue === "True"))
    {
        return true;
    }

    return false;
}

function setSearchButtonClass() {
    var isMobile = SessionGetIsMobile(SESSION_CLIENT_IS_MOBILE);
    
    if (!booleanIsTrue(isMobile)) {
        document.getElementById("USER_CONTROL_SEARCH_BUTTON").classList.add('button');
        document.getElementById("USER_CONTROL_SEARCH_BUTTON").value = "Search";
    } else {
        document.getElementById("USER_CONTROL_SEARCH_BUTTON").classList.add('buttonSearch');
    }
}
          
function HasValue(ctrlId, type, file) {
    var ctrl = document.getElementById(ctrlId);
   
    isNullUndefined(ctrl, file, 'ctrl-HasValue');
    
    var value = ctrl.value;

    if (value === null || value === undefined || value.length < 1) {
        return alert('Please enter a value for ' + type);
    }

    return true;
}

function SetCategory(id) {
    //3 hot, 2 warm, 1 cold, 0 cool
    if (id === 3) {
        return 'Hot';           		//hot
    } else if (id === 2) {
        return 'Warm';          		//warm
    } else {
        return 'Cool'; 	                //cool/cold
    }
}

function RenderCompletedCheckBox(isCompleted) {
    if (isCompleted && isCompleted === true) {
        return '<input type="checkbox" checked disabled="disabled" >';
    }

    return '<input type="checkbox" disabled="disabled" >';
}

function SetElementValue(ctrlName, value) {
	ctrlObj = document.getElementById(ctrlName);

	if (ctrlObj.type === 'checkbox') {
		if (value === 'true' || value === true) {
			ctrlObj.checked = true;
		} else {
			ctrlObj.checked = false;
		}
	} else if (ctrlObj.type === 'select-one') {
		//3 hot, 2 warm, 1 cold, 0 cool
		if (value === '3') {
			ctrlObj.selectedIndex = 0;		//hot
		} else if (value === '2') {
			ctrlObj.selectedIndex = 1;		//warm
		} else {
			ctrlObj.selectedIndex = 2;		//cool/cold
		}								 
	} else {
		ctrlObj.value = value;
	}

	return value;
}

function GetElementValue(ctrlName) {
	var ctrlObj = GetElement(ctrlName);

	if (ctrlObj.type === 'checkbox') {
		value = ctrlObj.checked;
	} else if (ctrlObj.type === 'select-one') {
		value = ctrlObj.options[ctrlObj.selectedIndex].value;
	} else {
		value = ctrlObj.value;
	}

	return value;  
}

function GetElement(ctrlName) {
	var ctrlObj = document.getElementById(ctrlName);

	isNullUndefined(ctrlObj, 'Utilities.js', 'ctrl exists');

	return ctrlObj
}

function GetHost() {
    var host = window.location.origin;

    return host;
}