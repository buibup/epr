var MainModule = angular.module("MainModule", []);

// Main controller for the application.
MainModule.controller(
     "LoginController",
    function ($scope, $rootScope, LoginService,$location,$window) {
        $scope.Login = function () {
            var username = $scope.username;
            var password = $scope.password;
            $scope.data = {};
            LoginService
            //.GetPatient($scope.patientModel.PAPMI_No, $scope.patientModel.DoctorCode)
            .Login(username, password)
            .then(function (data) {                
                if (data.success) {
                    //Redirect MainPage Whit session
                    $scope.data = data.model;
                    if ($scope.data.session !== null) {
                        $window.location.href = 'http://10.104.10.45/eprsnh/OpdRecord';
                    } else {
                        alert('Something wrong Please try again....')
                    }

                    //console.log("After Patient have data" + "loading =" + $scope.loading + "Searching =" + $scope.searching)
                }
                else {

                    alert('User Wrong Please Login again');


                }

            });
        }
        
    }
 )