(function () {
    'use strict';

    config.$inject = ['$routeProvider', '$locationProvider'];

    angular.module('stallerApp', [
        'ngRoute', 'companyService'
    ]).config(config);


    function config($routeProvider, $locationProvider) {
        $routeProvider
            .when('/', {
                templateUrl: '/Views/list.html',
                controller: 'CompanyListController'
            })
            .when('/index.html', {
                templateUrl: '/Views/list.html',
                controller: 'CompanyListController'
            })
            .when('/company/add/', {
                templateUrl: '/Views/add.html',
                controller: 'CompanyAddController'
            })
            .when('/company/edit/:id', {
                templateUrl: '/Views/edit.html',
                controller: 'CompanyEditController'
            })
            .when('/company/delete/:id', {
                templateUrl: '/Views/delete.html',
                controller: 'CompanyDeleteController'
            })
        ;

        $locationProvider.html5Mode(true);
    }

})();
