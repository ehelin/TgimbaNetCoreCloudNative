﻿var ServerCall = {}	;

ServerCall.Post = function (subUrl, formData)
{
    var promise = new Promise(
        function (resolve, reject) {
            var request = new XMLHttpRequest();			   
            
            request.onreadystatechange = function (err) {
                if (request.readyState === 4) {
                    successCallback();
                }
            };

            var base_url = GetHost();
            request.open("POST", base_url + subUrl);
            request.send(formData);

            successCallback = function () {
                return resolve(request.responseText);
            }
        }
    );

    return promise;
};

ServerCall.Delete = function (subUrl, encodedUserName, encodedToken) {
	var promise = new Promise(
		function (resolve, reject) {
			var request = new XMLHttpRequest();

			request.onreadystatechange = function (err) {
				if (request.readyState === 4) {
					successCallback();
				}
			};

			var base_url = GetHost();
            request.open("DELETE", base_url + subUrl);

            request.setRequestHeader("EncodedUserName", encodedUserName);
            request.setRequestHeader("EncodedToken", encodedToken);

			request.send('');

			successCallback = function () {
				return resolve(request.responseText);
			}
		}
	);

	return promise;
};

ServerCall.PostBody = function (subUrl, body)
{
    var promise = new Promise(
        function (resolve, reject) {
            var request = new XMLHttpRequest();		   
            
            request.onreadystatechange = function (err) {
                if (request.readyState === 4) {
                    successCallback();
                }
            };

            var base_url = GetHost();
            request.open("POST", base_url + subUrl);	
			request.setRequestHeader('Content-Type', 'application/json');	
            request.send(body);

            successCallback = function () {
                return resolve(request.responseText);
            }
        }
    );

    return promise;
};

ServerCall.Get = function (subUrl) {
    var promise = new Promise(
        function (resolve, reject) {
            var request = new XMLHttpRequest();

            request.onreadystatechange = function (err) {
                if (request.readyState === 4) {
                    successCallback();
                }
            };

            var base_url = GetHost();
			var url = base_url + subUrl;
            request.open('GET', url, true);

            request.send('');

            successCallback = function () {
                return resolve(request.responseText);
            }
        }
    );

    return promise;
};