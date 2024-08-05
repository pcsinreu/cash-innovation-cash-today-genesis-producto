angular.module("ConsLocApp")
 .controller("aboutCtrl", ['QueryService', '$scope', '$http', '$location', 'ConsLocConstants', 'ActionsService', '$compile', function (QueryService, $scope, $http, $location, ConsLocConstants, ActionsService, $compile) {

     $scope.clientes = [];



     var action = ActionsService.tesoreriaIntegralAction();

     $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, action).then(function (response) {

         var data = angular.fromJson(response.data);

         $scope.clientes = data.Clientes;
     });




     $scope.GoToClient = function (data) {

         QueryService.addParameters(ActionsService.ObtenerSectoresClientesAction(0, 6, 1, data.COD_CLIENTE));

         $location.path(ConsLocConstants.CONST_ACCIONHANDLER_PATH);
     }





 }]);