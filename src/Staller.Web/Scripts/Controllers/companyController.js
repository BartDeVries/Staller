(function () {
    'use strict';

    angular
        .module('stallerApp')
        .controller('CompanyListController', CompanyListController)
        .controller('CompanyAddController', CompanyAddController)
        .controller('CompanyEditController', CompanyEditController)
        .controller('CompanyDeleteController', CompanyDeleteController);

    CompanyListController.$inject = ['$scope', 'Company'];

    function CompanyListController($scope, Company) {
        $scope.companies = Company.query();
    }

    /* Company Create Controller */
    CompanyAddController.$inject = ['$scope', '$location', 'Company'];

    function CompanyAddController($scope, $location, Company) {
        $scope.company = new Company();
        $scope.add = function () {
            $scope.company.$save(function () {
                $location.path('/');
            });
        };
    }




    /* Company Edit Controller */
    CompanyEditController.$inject = ['$scope', '$routeParams', '$location', 'Company'];

    function CompanyEditController($scope, $routeParams, $location, Company) {
        $scope.company = Company.get({ id: $routeParams.id });
        $scope.edit = function () {
            $scope.company.$update(function () {
                $location.path('/');
            });
        };
    }

    /* Company Delete Controller  */
    CompanyDeleteController.$inject = ['$scope', '$routeParams', '$location', 'Company'];

    function CompanyDeleteController($scope, $routeParams, $location, Company) {
        $scope.company = Company.get({ id: $routeParams.id });
        $scope.remove = function () {
            $scope.company.$remove({ id: $scope.company.Id }, function () {
                $location.path('/');
            });
        };
    }


})();
