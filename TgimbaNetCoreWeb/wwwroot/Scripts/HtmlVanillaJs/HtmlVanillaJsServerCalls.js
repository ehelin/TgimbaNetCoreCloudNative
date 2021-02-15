﻿var HtmlVanillaJsServerCalls = {};

HtmlVanillaJsServerCalls.Initialize = function (userAgent) {
    try {
        return ServerCall.Get(BUCKET_LIST_INITIALIZE + userAgent)
                .then(
                function (response) {
                    return response;
                });
    } catch (ex) {
        return Error_Handler('HtmlVanillaJsServerCalls.IsMobile(arg) - Error: ' + ex);
    }
};
	  
HtmlVanillaJsServerCalls.GetBucketListItems = function (url, params, sortColumn, sortAlgorithm) {
	var formData = new FormData();
	var userName = params[0];	 
	var token = params[1];
	var sortColumn = sortColumn && sortColumn.length > 0 ? sortColumn : '';
	var srchTerm = params[2] && params[2].length > 0 ? params[2] : '';
	var srchType = params[3] && params[3].length > 0 ? params[3] : '';
										
	var queryUrl = BUCKET_LIST_PROCESS_GET
				+ "?encodedUserName=" + btoa(userName)
						+ "&encoderedSortString=" + btoa(sortColumn)
							+ "&encodedToken=" + btoa(token)
								+ "&encodedSrchTerm=" + btoa(srchTerm)
								    + "&encodedSortType=" + btoa(sortAlgorithm)
								        + "&encodedSearchType=" + btoa(srchType);

	return ServerCall.Get(queryUrl)
			.then(
				function(response) {										 
					isNullUndefined(response); 

                    if (response === "")
                    {
                        response = "[]";
                    }
					var bucketListItems = JSON.parse(response);
					Display.LoadView(VIEW_MAIN, bucketListItems);
				});
};

HtmlVanillaJsServerCalls.EditBucketListItem = function (url, params) {
	var formData = new FormData();
	var user = SessionGetUsername(SESSION_USERNAME);

	formData.append("Name", params[0]);
	formData.append("DateCreated", params[1]);
	formData.append("BucketListItemType", params[2]);
	formData.append("Completed", params[3]);
	formData.append("Latitude", params[4]);
	formData.append("Longitude", params[5]);
	formData.append("DatabaseId", params[6]);
	formData.append("UserName", params[7]);
	formData.append("encodedUser", btoa(user));
	formData.append("encodedToken", btoa(SessionGetToken(SESSION_TOKEN)));

	return ServerCall.Post(url, formData)
		.then(
			function (response) {
				// TODO - convert response to boolean
				if (response && response === "true") {
					MainController.Index();
				} else {
					// TODO - handle error
					alert('Edit failed');
				}
			});
};

HtmlVanillaJsServerCalls.AddBucketListItem = function (url, params) {
	var formData = new FormData();	 
	var user = SessionGetUsername(SESSION_USERNAME);

	formData.append("Name", params[0]);
	formData.append("DateCreated", params[1]);	
	formData.append("BucketListItemType", params[2]);
	formData.append("Completed", params[3]);
	formData.append("Latitude", params[4]);
	formData.append("Longitude", params[5]);
	formData.append("DatabaseId", '');		
	formData.append("UserName", user);	  
	formData.append("encodedUser", btoa(user)); 
	formData.append("encodedToken", btoa(SessionGetToken(SESSION_TOKEN)));			  

	return ServerCall.Post(url, formData)
		.then(
			function (response) {
				// TODO - convert response to boolean
				if (response && response === "true") {	  
					MainController.Index();		           
				} else {
					// TODO - handle error
					alert('Add failed');
				}
			});
};

HtmlVanillaJsServerCalls.DeleteBucketListItem = function (url, dbId) {
    var user = SessionGetUsername(SESSION_USERNAME);
    var token = SessionGetToken(SESSION_TOKEN);

    return ServerCall.Delete(url + '/?id=' + dbId, btoa(user), btoa(token))
		.then(
			function (response) {
				// TODO - convert response to boolean
				if (response && response === "true") {
					MainController.Index();
				} else {
					// TODO - handle error
					alert('Delete failed');
				}
			});
};

HtmlVanillaJsServerCalls.ProcessLogin = function (view, params) {
	var formData = new FormData();
	var userName = params[0];	 
	var passWord = params[1];

	formData.append("user", btoa(userName));
	formData.append("pass", btoa(passWord));

    return ServerCall.Post(view, formData)
        .then(
        function (token) {
            if (token !== null && token !== undefined && token.length > 0) {
				SessionSetToken(SESSION_TOKEN, token);
				SessionSetUsername(SESSION_USERNAME, userName);
				MainController.Index();		           
            } else {
                // TODO - reset user and pass text boxes to empty
                alert('Username and/or password is incorrect');                
            }
        });
};
						  
HtmlVanillaJsServerCalls.ProcessRegistration = function (view, params) {
	var formData = new FormData();

	formData.append("user", btoa(params[0]));
	formData.append("email", btoa(params[1]));
	formData.append("pass", btoa(params[2]));

	return ServerCall.Post(view, formData)
		.then(
			function (goodRegistration) {
				if (goodRegistration !== null
					&& goodRegistration !== undefined
					&& goodRegistration !== ''
					&& goodRegistration === 'true')  // TODO - convert boolean from string
				{
					Display.LoadView(VIEW_LOGIN, null);
				} else {
					alert('User is not registered');
				}
			});
};

