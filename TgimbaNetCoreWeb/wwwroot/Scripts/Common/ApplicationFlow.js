﻿var ApplicationFlow = {}; 

ApplicationFlow.SetView = function(view) { 	
    return ApplicationFlow.SetLayout()
    .then(
        function () {
            if (LoginController.IsLoggedIn() === true) {
                MainController.Index();
            }
            else {
                return LoginController.Index();
            }
        }
    )
};

ApplicationFlow.SetLayout = function () 
{
    return HtmlVanillaJsServerCalls.Initialize(window.navigator.userAgent)
        .then(
            function (response) {
                isNullUndefined(response);
                response = JSON.parse(response);

                ApplicationFlow.SetupScreenTypeCss(response.isMobile);
                SessionSet(SESSION_AVAILABLE_SORTING_ALGORITHMS, response.availableSortingAlgorithms);
                SessionSet(SESSION_AVAILABLE_SEARCHING_ALGORITHMS, response.availableSearchingAlgorithms);
            }
        );
};

ApplicationFlow.SetupScreenTypeCss = function(isMobile) 
{
    if (isMobile === true || isMobile === "true") {
        SessionSetIsMobile(SESSION_CLIENT_IS_MOBILE, true);
        cssFileName = CSS_FILE_MOBILE;
    } else {
        SessionSetIsMobile(SESSION_CLIENT_IS_MOBILE, false);
        cssFileName = CSS_FILE_DESKTOP;
    }
    
    var fileref = document.createElement("link");
    fileref.rel = "stylesheet";
    fileref.type = "text/css";
    fileref.href = cssFileName;
    document.getElementsByTagName("head")[0].appendChild(fileref)
}
