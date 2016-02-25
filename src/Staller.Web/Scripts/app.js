(function () {
    'use strict';

    config.$inject = ['$routeProvider', '$locationProvider'];

    angular.module('stallerApp', [
        'ngRoute', 'companyService'
    ]).config(config);


    function config($routeProvider, $locationProvider) {
        $routeProvider
            .when('/company/', {
                templateUrl: '/Views/Company/list.html',
                controller: 'CompanyListController'
            })
            .when('/company/index.html', {
                templateUrl: '/Views/Company/list.html',
                controller: 'CompanyListController'
            })
            .when('/company/add/', {
                templateUrl: '/Views/Company/add.html',
                controller: 'CompanyAddController'
            })
            .when('/company/edit/:id', {
                templateUrl: '/Views/Company/edit.html',
                controller: 'CompanyEditController'
            })
            .when('/company/delete/:id', {
                templateUrl: '/Views/Company/delete.html',
                controller: 'CompanyDeleteController'
            })
        ;

        $locationProvider.html5Mode(true);
    }

})();
