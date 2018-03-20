
//var MainModule = angular.module( "MainModule", [] );

// I act a repository for the remote friend collection.
MainModule.service(
    "OpdRecordService",
    function ($http, $q) {

        // Return public API.
        return ({
            GetPatient: GetPatient,
            GetSummary: GetSummary,
            GetDocscan: GetDocscan,
            GetQuippeScan: GetQuippeScan,
            GetObservationLastEpi: GetObservationLastEpi
        });

        // search member by criterias.
        function GetPatient(hn,DoctorCode) {
            var request = $http({
                method: "post",
                url: ROOT_URL+"OpdRecord/GetPatient",                
                //url:"/OpdRecord/GetPatient",
                data: {
                    "hn": hn
                  //  "DoctorCode": DoctorCode
                }
            });
            return (request.then(handleSuccess, handleError));
        }

        function GetSummary(episodeRowId) {
            var request = $http({
                method: "post",
                 url: ROOT_URL + "OpdRecord/GetMedical",                
                //url: "/OpdRecord/GetMedical",
                data: {
                    "episodeRowId": episodeRowId
                }
            });
            return (request.then(handleSuccess, handleError));
        }

        function GetDocscan(HN) {
            var request = $http({
                method: "post",
                url: ROOT_URL + "OpdRecord/GetDocscan",
                //url: "/OpdRecord/GetMedical",
                data: {
                    "HN": HN
                }
            });
            return (request.then(handleSuccess, handleError));
        }

        function GetQuippeScan(HN) {
            var request = $http({
                method: "post",
                url: ROOT_URL + "OpdRecord/GetQuippeScan",
                data: {
                    "HN": HN
                }
            });
            return (request.then(handleSuccess, handleError));
        }

        function GetObservationLastEpi(HN) {
            var request = $http({
                method: "post",
                url: ROOT_URL + "OpdRecord/GetObservationLastEpi",
                //url: "/OpdRecord/GetMedical",
                data: {
                    "HN": HN
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