// Define our AngularJS application module.
var MainModule = angular.module("MainModule", []);

function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return sParameterName[1];
        }
    }
}

MainModule.controller(
    "GIENDOController",
    function ($scope, $rootScope, $http, $location, $timeout) {
        $scope.closeAllGITabList = function () {
            $scope.GITabList = [];
        }
        $scope.GIENDO = $.Deferred();
       
        $scope.GITabList = [];
        $scope.loadGiendo = function (index) {
           // console.log(index);
        };
        $scope.LastOpenedRow = null;
        $scope.activeLastOpenedRow = function (rowIndex) {
            $scope.LastOpenedRow = rowIndex;
        }
        $scope.closeAllGI = function () {
            $scope.GITabList = [];
        }        
        $scope.closeGITabMedical = function (GITab) {
            //ensure the code will be called in a single $apply block.
            var temp = [];
        //    $timeout(function () {

                GITab.marked = false;
                var GiList;
                var len = $scope.GITabList.length;

                for (var i = 0; i < len; i++) {
                    GiList = $scope.GITabList[i];
                    GiList.active = false;
                    if (GiList.head === GITab.head) {
                        $scope.GITabList.splice(i, 1);
                        i--; //reduce index to current element.
                    }
                    len = $scope.GITabList.length;
                }

                //active lasted tab.
                if ($scope.GITabList.length > 0) {
                    GiList = $scope.GITabList[$scope.GITabList.length - 1];
                    GiList.active = true;
                }

                //force update 'active' attribute on ui by clear object and reder in next $scope.$apply();
                temp = $scope.GITabList;
                $scope.GITabList = [];

         //   });

            //force update 'active' attribute on ui by clear object and reder in next $scope.$apply();
            $timeout(function () {
                $scope.GITabList = temp;
            });

        };
        $scope.activeLastOpenedRow = function (rowIndex) {
            $scope.LastOpenedRow = rowIndex;
        }
       
        $scope.MarkGITab = function (GITabList) { //SET Mark
            if (GITabList.marked === true) {
                GITabList.marked = false;

            }
            else {
                GITabList.marked = true;
            }
        }
        $scope.closeGiendoTab = function (GITab){
            //ensure the code will be called in a single $apply block.
            var temp = [];
            $timeout(function () {

                GITab.marked = false;
                var GIList;
                var len = $scope.GITabList.length;

                for (var i = 0; i < len; i++) {
                    GIList = $scope.GITabList[i];
                    GIList.active = false;
                    if (GIList.ROWID === GITab.ROWID) {
                        $scope.GITabList.splice(i, 1);
                        i--; //reduce index to current element.
                    }
                    len = $scope.GITabList.length;
                }

                //active lasted tab.
                if ($scope.GITabList.length > 0) {
                    GIList = $scope.GITabList[$scope.GITabList.length - 1];
                    GIList.active = true;
                }

                //force update 'active' attribute on ui by clear object and reder in next $scope.$apply();
                temp = $scope.GITabList;
                $scope.GITabList = [];

            });

            //force update 'active' attribute on ui by clear object and reder in next $scope.$apply();
            $timeout(function () {
                $scope.GITabList = temp;
            });

        };
        $scope.loadGiendoMedical = function (GIAdd,Rowid) {
            var temp = [];
            //ensure the code will be called in a single $apply block.
            //$timeout(function () { 
                //CASE 1: If episode in list then, only active episode tab only.
                //CASE 2: If loading episodeMedical, then waiting for response.
                var found = false;
                var GiList;
                var len = $scope.GITabList.length;
                for (var i = 0; i < len; i++) {
                    GiList = $scope.GITabList[i];
                    if (GiList.ROWID === GIAdd.ROWID) {
                        GiList.active = true;
                        found = true;
                    }
                    else {
                        GiList.active = false;
                    }
                }
                if (found) {
                    temp = $scope.GITabList;
                    return;
                }

                //CASE 3: If is not in list, then load episode and append to episodeMedical listing with active tab.

                GIAdd.active = true;
                removeUnmarkTab();

                $scope.GITabList.push(GIAdd); // open new Tab

                //console.log(episodeModel);
                //$scope.episodeTabList = [episodeModel];     // load Old tab

                //force update 'active' attribute on ui by clear object and reder in next $scope.$apply();
                temp = $scope.GITabList;
                $scope.GITabList = [];
        //        $("#DocScan").value(GiendoModel.EpisodeNo);
           // });

            //force update 'active' attribute on ui by clear object and reder in next $scope.$apply();
            //$timeout(function () {
                $scope.GITabList = temp;
           // });

            //Load episode's summary data.


        };
        function removeUnmarkTab() {
            if ($scope.GITabList.length < 1)
                return;

            for (var i = 0; i < $scope.GITabList.length; i++) {
                GI = $scope.GITabList[i];
                if (GI.marked === false) {
                    $scope.GITabList.splice(i, 1);
                    i--; //reduce index to current element.
                }
            }
        }
        

        var hn = getUrlParameter('hn');
         //$http.post('http://localhost:3858/GIENDO/FindGiendoJSON',{hn:hn}).
        //$http.post('http://10.105.10.7/epr2/GIENDO/FindGiendoJSON', { hn: hn }).
        $http.post('http://10.104.10.45/eprsnh/GIENDO/FindGiendoJSON', { hn: hn }).        
         success(function (data, status, header, config) {
             //$scope.patient=data;
             //console.log(data);
             $scope.GIENDO = JSON.parse(JSON.stringify(data));
                      
         }).
         error(function (data, status, header, config) {
             //console.log(status);
         });

        
    });
    

MainModule.filter('cut', function () {
    return function (value, wordwise, max, tail) {
        if (!value) return '';

        max = parseInt(max, 10);
        if (!max) return value;
        if (value.length <= max) return value;

        value = value.substr(0, max);
        if (wordwise) {
            var lastspace = value.lastIndexOf(' ');
            if (lastspace != -1) {
                value = value.substr(0, lastspace);
            }
        }

        return value + (tail || '…');
    };
});
MainModule.filter('ConvertDate', function () {
    return function (value) {
        var date = new Date(value);        
        var month=["ม.ค.","ก.พ.","มี.ค.","เม.ย.","พ.ค.","มิ.ย.","ก.ค.","ส.ค.","ก.ย.","ต.ค.","พ.ย.","ธ.ค."];
        var formattedDate = date.getDate() + "/" + (month[date.getMonth()]) + "/" + date.getFullYear();
        //var formattedDate = date.getFullYear()+"/"+ (date.getMonth() + 1) +"/"+ date.getDate()  ;
        return formattedDate;
    }
});
MainModule.filter('jcidate', function () {
    return function (input) {
        return input.replace(/[\/a-zA-Z()]/gi, '');
    };
});
