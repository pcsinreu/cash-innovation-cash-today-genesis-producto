angular.module("ConsLocApp")
    .register
    .controller("SectorsCtrl", ['QueryService', 'ActionsService', 'ConsLocConstants', '$scope', '$http', '$location', '$translate', function (QueryService, ActionsService, ConsLocConstants, $scope, $http, $location, $translate) {

        $scope.Sectors = [];
        $scope.Sales = [];
        $scope.PageNumber;
        $scope.CantPerPage;
        $scope.IncludexPage;
        $scope.TotalPag;
        $scope.Cliente;

        var dat = QueryService.getData();
        var parameters = QueryService.getParameters();

        if (dat != undefined) {
            $scope.Sectors = dat.Sectores.slice(3);
            $scope.Cliente = dat.Sectores[0].CLIENTE;
            $scope.TotalPag = dat.Sectores[1].NUM + 1;
        }

        if (parameters != undefined) {
            $scope.PageNumber = parameters.NumPagina;
            $scope.CantPerPage = parameters.MaxPorPagina;
            $scope.IncludexPage = parameters.IncluirPagina;
        }


        $scope.NextPageSale = function (parent) {
            var sector = parent.sector;
            var codSector = sector.DES_SECTOR.split('-')[0].trim();

            var OpenAccordionAction = ActionsService.ObtenerSaldosClienteAction(sector.Page + 1, 2, $scope.IncludexPage, sector.CLIENTE, codSector);

            $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, OpenAccordionAction).then(function (response) {

                var data = angular.fromJson(response.data);

                sector.expanded = true;
                sector.Sales = data.Saldos.slice(3);
                sector.Page++;

            });
        }

        $scope.PreviousPageSale = function (parent) {

            var sector = parent.sector;
            var codSector = sector.DES_SECTOR.split('-')[0].trim();

            var OpenAccordionAction = ActionsService.ObtenerSaldosClienteAction(sector.Page - 1, 2, $scope.IncludexPage, sector.CLIENTE, codSector);

            $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, OpenAccordionAction).then(function (response) {

                var data = angular.fromJson(response.data);

                sector.expanded = true;
                sector.Sales = data.Saldos.slice(3);
                sector.Page--;
            });
        }

        $scope.Nextpage = function (type) {

            var action = ActionsService.ObtenerSectoresClientesAction($scope.PageNumber + 1, $scope.CantPerPage, $scope.IncludexPage, $scope.Cliente);
            var nextPageAction = action;

            $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, nextPageAction).then(function (response) {

                var data = angular.fromJson(response.data);
                $scope.Sectors = data.Sectores.slice(3);;
                $scope.PageNumber++;
            });
        };

        $scope.Previous = function (type) {

            var previousPageAction = ActionsService.ObtenerSectoresClientesAction($scope.PageNumber - 1, $scope.CantPerPage, $scope.IncludexPage, $scope.Cliente);

            $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, previousPageAction).then(function (response) {

                var data = angular.fromJson(response.data);
                $scope.Sectors = data.Sectores.slice(3);;
                $scope.PageNumber--;
            });
        };

        $scope.OpenAccordion = function (sector) {

            var codSector = sector.DES_SECTOR.split('-')[0].trim();

            var OpenAccordionAction = ActionsService.ObtenerSaldosClienteAction(0, 2, $scope.IncludexPage, sector.CLIENTE, codSector);

            $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, OpenAccordionAction).then(function (response) {

                var data = angular.fromJson(response.data);

                sector.expanded = true;
                sector.Page = data.Saldos[0].NUM;
                sector.Pages = data.Saldos[1].NUM;
                if (sector.Pages == 0) {
                    sector.Pages++;
                }
                sector.Sales = data.Saldos.slice(3);
            });
        };

        $scope.GoBack = function () {
            $location.path(ConsLocConstants.CONST_ROOT_PATH);
        };

        $scope.changeLanguage = function (translationrequest) {
            $translate.use(translationrequest);
        };

        $translate.use(navigator.language + '-sectores');

    }]);

function Back() {
    var scope = angular.element(document.getElementById("SectorPage")).scope();

    scope.$apply(function () {
        scope.GoBack();
    });
}