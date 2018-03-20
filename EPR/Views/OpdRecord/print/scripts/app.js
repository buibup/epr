'use strict';

/**
 * @ngdoc overview
 * @name y001App
 * @description
 * # y001App
 *
 * Main module of the application.
 */
angular
  .module('y001App', [
    'ngRoute',
    'ngTouch',
    'ngSanitize'

  ])
  .config(function ($routeProvider) {
    $routeProvider
      .when('/', {
        templateUrl: 'views/main.html',
        controller: 'MainCtrl',
        controllerAs: 'main'
      })
      .when('/about', {
        templateUrl: 'views/about.html',
        controller: 'AboutCtrl',
        controllerAs: 'about'
      })
      .when('/forms',{
        templateUrl: 'views/forms.html',
        controller: 'formsCtrl',
        controllerAs: 'forms'
      })
      .when('/test',{
        templateUrl: 'views/test.html',
        controller: 'test2Ctrl',
        controllerAs: 'test2'
      })
      .otherwise({
        redirectTo: '/'
      });
  });
