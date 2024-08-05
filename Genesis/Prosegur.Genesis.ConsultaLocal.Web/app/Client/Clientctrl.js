angular.module("ConsLocApp").register.controller("Clientctrl", ['QueryService', '$scope', '$http', '$location', function (QueryService, $scope, $http, $location) {

    $location.search('Url', null);

    $scope.Saldos = [];

    $scope.Totales = [];

    var response = QueryService.getData();

    var data = angular.fromJson(response);
    var count = 0;
    $scope.Saldos = data.Saldos;

    angular.forEach($scope.Saldos, function (key, value) {
        if (value != $scope.Saldos.length - 1 && $scope.Saldos[value].DES_SECTOR == $scope.Saldos[value + 1].DES_SECTOR) {

            count++;
        } else {

            $scope.Saldos[value - count]["cuantity"] = count + 1;
            count = 0;
        }
    });

    $scope.Totales = data.Total;

}]);