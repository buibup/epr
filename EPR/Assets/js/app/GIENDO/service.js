﻿
//var MainModule = angular.module( "MainModule", [] );

// I act a repository for the remote friend collection.
MainModule.service(
    "GIENDOService",
    function ($http, $q) {
        // Return public API.
        return ({
            FindGiendoJSON: FindGiendoJSON
            
        });

        // search member by criterias.
        function FindGiendoJSON(hn) {
            var request = $http({
                method: "post",
                url: ROOT_URL + "GIENDO/FindGiendoJSON",
                //url:"/GIENDO/FindGiendo",
                data: {
                    "hn": hn
                    //  "DoctorCode": DoctorCode
                }
            });
            return (request.then(handleSuccess, handleError));
        }

        


        // PRIVATE METHODS =============================================================================================

        // I transform the successful response, unwrapping the application data from the API response payload.
        function handleSuccess(response) {
            return (response.data);
        }

        // I transform the error response, unwrapping the application dta from the API response payload.
        function handleError(response) {
            // The API response from the server should be returned in a
            // nomralized format. However, if the request was not handled by the
            // server (or what not handles properly - ex. server error), then we
            // may have to normalize it on our end, as best we can.
            if (!angular.isObject(response.data) || !response.data.message) {
                return ($q.reject("An unknown error occurred."));
            }
            // Otherwise, use expected error message.
            return ($q.reject(response.data.message));
        }

    }
);