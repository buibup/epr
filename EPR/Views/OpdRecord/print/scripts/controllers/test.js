/**
 * Created by tong on 2015-07-08.
 */

var app=angular.module("y001App");

app.controller('test1',function($scope){
  $scope.x="This is x";
});

app.controller('test2Ctrl', function ($scope) {
  var scope=this;
  $scope.name = 'hey';
  $scope.items = [1, 2, 3];
  $scope.complex = { a: { b: { c: 'yo' }}};
  var thing = 'document';
  $scope.form= new formdata();
  console.log($scope.form.length);
});

app.controller('drawpage',function($scope){
  $scope.pages=new formdata();
})
