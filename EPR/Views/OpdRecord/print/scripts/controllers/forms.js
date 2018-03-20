/**
 * Created by tong on 2015-07-07.
 */
'use strict';

/**
 * @ngdoc function
 * @name y001App.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the y001App
 */
angular.module('y001App')
  .controller('formsCtrl', function () {
    this.awesomeThings = [
      'HTML5 Boilerplate',
      'AngularJS',
      'Karma'
    ];
  });

var app= angular.module('y001App');

app.config(function($sceProvider){
  $sceProvider.enabled(false);
});

app.factory('formserve',function(){
  var forms = new formdata();
  return{
    all: function(){
      return forms;
    },
    first: function(){
      return forms[0];
    }
  }
});


app.controller('formList',function($scope,$sce,formserve){
  $scope.forms=formserve.all();
//  $scope.htplt = headData();

//  console.log($scope.forms);
  $scope.loadForm=function($scope){
    console.log($scope);
  };

});

