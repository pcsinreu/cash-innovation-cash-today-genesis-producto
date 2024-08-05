angular.module("ConsLocApp")
 .controller("MaeSalesCtrl", ['$scope', '$http', '$location', '$translate', 'ConsLocConstants', 'ActionsService', '$compile', function ($scope, $http, $location, $translate, ConsLocConstants, ActionsService, $compile) {

     var now = new Date();

     $scope.filtroFechaDesde = new Date(now.toLocaleDateString());
     $scope.filtroFechaHasta = new Date(now.toLocaleDateString());
     $scope.filtroCodigo;
     $scope.filtroDesc;
     $scope.filtroBanco;
     $scope.filtroCli = { codCliente: '', desCliente: '', banks: [] };


     //auxiliares
     $scope.filtroFechaDesdeAux;
     $scope.filtroFechaHastaAux;
     $scope.filtroCodigoAux;
     $scope.filtroDescAux;

     var queryString = $location.search();
     var codigoDelegacion = queryString.codigoDelegacion;
     var usuarioLogueado = queryString.usuarioAD;
     var descripcionDelegacion = queryString.descripcionDelegacion;

     if (usuarioLogueado == undefined) {
         usuarioLogueado = 'USUARIO';
     };

     if (codigoDelegacion == undefined) {
         codigoDelegacion = '01';
         descripcionDelegacion = 'NAZCA';
     };

     $("#usuarioLogueado").text(usuarioLogueado);
     $("#Delegacion").text(descripcionDelegacion);



     var bankAction = ActionsService.ObtenerBancos(0, 10, 1);

     $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, bankAction).then(function (response) {

         var data = angular.fromJson(response.data);

         var pagactual = data.Bancos[0].NUM;
         var cantpaginas = data.Bancos[1].NUM;
         $scope.Bancos = data.Bancos.slice(3);
         $scope.Bancos.page = pagactual;
         $scope.Bancos.Pages = cantpaginas == 0 ? 1 : cantpaginas;

         //var filterPath = data.PathFiltros[0].PATH;

         //var compiledeHTML = $compile("<uc-filters filterPath='" + filterPath + "' add='search(filter)'></uc-filters>")($scope);
         //$("#filter").append(compiledeHTML);
     });

     $scope.search = function (filter) {
         alert(filter.CodCliente + '  ' + filter.Description);
     }

     $scope.GetCollectionDetails = function (banco) {

         var expanded = $("#" + banco.COD_CLIENTE).attr("aria-expanded");

         if (expanded == 'false' || expanded == undefined) {

             var GetCollectionDetailsAction = ActionsService.ObtenerDetallesAcreditaciones(0, 10, 1, banco.COD_CLIENTE, $scope.filtroFechaDesdeAux, $scope.filtroFechaHastaAux, codigoDelegacion);

             $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, GetCollectionDetailsAction).then(function (response) {

                 var data = angular.fromJson(response.data);

                 var pagactual = data.Detalles[2].NUM;
                 var cantpaginas = data.Detalles[1].NUM;
                 banco.DetalleAcreditaciones = data.Detalles.slice(3);
                 banco.DetalleAcreditaciones.page = pagactual;
                 banco.DetalleAcreditaciones.Pages = cantpaginas == 0 ? 1 : cantpaginas;

             });
         };
     };

     $scope.GetAcreditationDetailsxCli = function (acred) {

         var expanded = $("#" + acred.FYH_ACREDITACION.split('/').join('') + acred.COD_CLIENTE).attr("aria-expanded");

         if (expanded == 'false' || expanded == undefined) {

             var GetAcreditationDetailsxCliAction = ActionsService.ObtenerDetallesAcreditacionesxClientes(0, 10, 1, acred.FYH_ACREDITACION, acred.COD_CLIENTE, codigoDelegacion);

             $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, GetAcreditationDetailsxCliAction).then(function (response) {

                 var data = angular.fromJson(response.data);

                 var pagactual = data.Acreditaciones[2].NUM;
                 var cantpaginas = data.Acreditaciones[1].NUM;
                 acred.AcreditationbyCli = data.Acreditaciones.slice(3);
                 acred.AcreditationbyCli.page = pagactual;
                 acred.AcreditationbyCli.Pages = cantpaginas == 0 ? 1 : cantpaginas;
                 acred.expanded = true;
             });
         }
     };

     $scope.GetMaes = function (axcli) {

         var GetAcreditationDetailsxCliAction = ActionsService.ObtenerMaes(0, 10, 1, axcli.FYH_ACREDITACION, axcli.COD_CLIENTE, codigoDelegacion);

         $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, GetAcreditationDetailsxCliAction).then(function (response) {

             var data = angular.fromJson(response.data);

             var pagactual = data.Maes[2].NUM;
             var cantpaginas = data.Maes[1].NUM;
             axcli.Maes = data.Maes.slice(3);
             axcli.Maes.page = pagactual;
             axcli.Maes.Pages = cantpaginas == 0 ? 1 : cantpaginas;

             axcli.expanded = true;
         });
     };

     $scope.ChangeMaesPage = function (AcreditationbyCli, direction) {

         var nextPage = 0;

         switch (direction) {
             case 'previous':
                 nextPage = AcreditationbyCli.Maes.page - 1;
                 break;
             case 'next':
                 nextPage = AcreditationbyCli.Maes.page + 1;
                 break;
         };

         var GetAcreditationDetailsxCliAction = ActionsService.ObtenerMaes(nextPage, 10, 1, AcreditationbyCli.FYH_ACREDITACION, AcreditationbyCli.COD_CLIENTE);

         $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, GetAcreditationDetailsxCliAction).then(function (response) {

             var data = angular.fromJson(response.data);

             var pagactual = data.Maes[2].NUM;
             var cantpaginas = data.Maes[1].NUM;
             AcreditationbyCli.Maes = data.Maes.slice(3);
             AcreditationbyCli.Maes.page = pagactual;
             AcreditationbyCli.Maes.Pages = cantpaginas == 0 ? 1 : cantpaginas;
         });
     };

     $scope.ChangeBanksPage = function (bancos, direction) {
         var nextPage = 0;

         switch (direction) {
             case 'previous':
                 nextPage = bancos.page - 1;
                 break;
             case 'next':
                 nextPage = bancos.page + 1;
                 break;
         };

         var bankAction = ActionsService.ObtenerBancos(nextPage, 10, 1);

         $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, bankAction).then(function (response) {

             var data = angular.fromJson(response.data);

             var pagactual = data.Bancos[0].NUM;
             var cantpaginas = data.Bancos[1].NUM;
             $scope.Bancos = data.Bancos.slice(3);
             $scope.Bancos.page = pagactual;
             $scope.Bancos.Pages = cantpaginas == 0 ? 1 : cantpaginas;
         });

     };

     $scope.ChangeAcreditationPage = function (bank, direction) {

         var nextPage = 0;

         switch (direction) {
             case 'previous':
                 nextPage = bank.DetalleAcreditaciones.page - 1;
                 break;
             case 'next':
                 nextPage = bank.DetalleAcreditaciones.page + 1;
                 break;
         };

         var GetCollectionDetailsAction = ActionsService.ObtenerDetallesAcreditaciones(nextPage, 10, 1, bank.COD_CLIENTE);

         $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, GetCollectionDetailsAction).then(function (response) {

             var data = angular.fromJson(response.data);

             var pagactual = data.Detalles[2].NUM;
             var cantpaginas = data.Detalles[1].NUM;
             bank.DetalleAcreditaciones = data.Detalles.slice(3);
             bank.DetalleAcreditaciones.page = pagactual;
             bank.DetalleAcreditaciones.Pages = cantpaginas == 0 ? 1 : cantpaginas;
         });
     };


     $scope.ChangeAcreditbyClientPage = function (acreds, acredClient, direction) {


         switch (direction) {
             case 'previous':
                 nextPage = acreds.AcreditationbyCli.page - 1;
                 break;
             case 'next':
                 nextPage = acreds.AcreditationbyCli.page + 1;
                 break;
         };

         var GetAcreditationDetailsxCliAction = ActionsService.ObtenerDetallesAcreditacionesxClientes(nextPage, 10, 1, acredClient.FYH_ACREDITACION, acreds.COD_CLIENTE);

         $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, GetAcreditationDetailsxCliAction).then(function (response) {

             var data = angular.fromJson(response.data);

             var pagactual = data.Acreditaciones[2].NUM;
             var cantpaginas = data.Acreditaciones[1].NUM;
             acreds.AcreditationbyCli = data.Acreditaciones.slice(3);
             acreds.AcreditationbyCli.page = pagactual;
             acreds.AcreditationbyCli.Pages = cantpaginas == 0 ? 1 : cantpaginas;
         });

     };


     $scope.Filter = function () {
         var error = false;
         $scope.ShowDifferenceError = false;
         $scope.showFhastaError = false;
         $scope.showFdesdeError = false;


         if ($scope.filtroFechaDesde != undefined && $scope.filtroFechaHasta != undefined) {

             if (new Date($scope.filtroFechaHasta) < new Date($scope.filtroFechaDesde)) {
                 $scope.ShowDifferenceError = true
                 error = true;
             }

         } else {

             if ($scope.filtroFechaDesde != undefined && $scope.filtroFechaHasta == undefined) {
                 $scope.showFhastaError = true;
                 error = true;
             }

             if ($scope.filtroFechaHasta != undefined && $scope.filtroFechaDesde == undefined) {
                 $scope.showFdesdeError = true;
                 error = true;
             }

         }

         if (!error) {

             $scope.filtroFechaDesdeAux = angular.copy($scope.filtroFechaDesde);
             $scope.filtroFechaHastaAux = angular.copy($scope.filtroFechaHasta);
             $scope.filtroCodigoAux = angular.copy($scope.filtroCodigo);
             $scope.filtroDescAux = angular.copy($scope.filtroDesc);

             var bankAction = ActionsService.ObtenerBancos(0, 10, 1, $scope.filtroFechaDesdeAux, $scope.filtroFechaHastaAux, $scope.filtroCodigoAux, $scope.filtroDescAux, codigoDelegacion);

             $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, bankAction).then(function (response) {

                 var data = angular.fromJson(response.data);

                 var pagactual = data.Bancos[0].NUM;
                 var cantpaginas = data.Bancos[1].NUM;
                 $scope.Bancos = data.Bancos.slice(3);
                 $scope.Bancos.page = pagactual;
                 $scope.Bancos.Pages = cantpaginas == 0 ? 1 : cantpaginas;

             });

         }

     }

     $scope.ResetBankSearchPage = function () {
         $scope.filtroCli = { codCliente: '', desCliente: '', banks: [] };

         $scope.ChangeBankSearchPage($scope.filtroCli, '');
     };

     $scope.ChangeBankSearchPage = function (filtroCli, direction) {
         $scope.ChangeBankSearchPageSpecific(filtroCli, direction, '')
     };

     $scope.ChangeBankSearchPageSpecific = function (filtroCli, direction, page) {

         var nextPage = 0;

         if (page != undefined && page != '') {
             nextPage = page
         }


         switch (direction) {
             case 'previous':
                 nextPage = filtroCli.banks.page - 1;
                 break;
             case 'next':
                 nextPage = filtroCli.banks.page + 1;
                 break;
         };

         var getBanks = ActionsService.ChangeBankSearchPage(nextPage, 10, 1, filtroCli.codCliente, filtroCli.desCliente);

         $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, getBanks).then(function (response) {

             var data = angular.fromJson(response.data);

             var pagactual = data.Bancos[2].NUM;
             var cantpaginas = data.Bancos[1].NUM;
             var cantreg = data.Bancos[0].NUM;
             filtroCli.banks = data.Bancos.slice(3);
             filtroCli.banks.page = pagactual;
             filtroCli.banks.Pages = cantpaginas == 0 ? 1 : cantpaginas;
             filtroCli.banks.CantReg = cantreg;
         });
     };

     $scope.setCodeanddeskFilter = function (bank) {
         $scope.filtroDesc = bank.DES_CLIENTE;
         $scope.filtroCodigo = bank.COD_CLIENTE;
         $scope.filtroBanco = bank.COD_CLIENTE;
         $("#modal-container-558944").modal('hide');
     }

     $scope.Clean = function () {
         $scope.filtroDesc = "";
         $scope.filtroCodigo = "";
         $scope.filtroFechaDesde = undefined;
         $scope.filtroFechaHasta = undefined;
     }


     $translate.use(navigator.language + '-maedetails');

 }]);



//Banco
// |___ COD_CLIENTE
// |___ DES_CLIENTE
// |___ DetalleAcreditaciones
//                 |__________ COD_CLIENTE
//                 |__________ DES_CLIENTE
//                 |__________ FYH_ACREDITACION
//                 |__________ REALES
//                 |__________ DOLARES
//                 |__________ EUROS
//                 |__________ AcreditationbyCli
//                                   |____________ COD_CLIENTE
//                                   |____________ DES_CLIENTE
//                                   |____________ FYH_ACREDITACION
//                                   |____________ REALES
//                                   |____________ DOLARES
//                                   |____________ EUROS
//                                   |____________ Maes
//                                                  |___________ COD_IDENTIFICACION
//                                                  |____________ DES_SECTOR
//                                                  |____________ FYH_ACREDITACION
//                                                  |____________ REALES
//                                                  |____________ DOLARES
//                                                  |____________ EUROS