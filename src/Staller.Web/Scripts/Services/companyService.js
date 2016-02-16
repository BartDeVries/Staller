(function () {
    'use strict';

    angular
        .module('companyService', ['ngResource'])
        .factory('Company', Company);

    Company.$inject = ['$resource'];

    function Company($resource) {
        //return $resource('/api/company/:id');

        return $resource(
               "/api/company/:id",
               { id: "@Id" },
               { "update": { method: "PUT" } }
          );
    }

})();