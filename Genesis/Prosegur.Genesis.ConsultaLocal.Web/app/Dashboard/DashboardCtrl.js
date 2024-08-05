angular
  .module("ConsLocApp")
  .controller("DashboardCtrl", [
    '$scope',
    '$timeout',
    '$http',
    '$location',
    '$translate',
    'ConsLocConstants',
    'ActionsService',
    'FileSaver',
    'Blob',
    function ($scope, $timeout, $http, $location, $translate, ConsLocConstants, ActionsService, FileSaver, Blob) {

      //PieChartInfoModalDeleg
      $scope.ShowModal = function (points, evt) {

        var codigosclientes = ',' + $scope
          .SaldosCliente
          .codigoCli
          .toString() + ',';

        $scope.ObtenerSaldosDesCli(codigosclientes);

        //  $("#PieChartInfoModal").attr("class", "modal bounceIn animated")
        $("#PieChartInfoModal").attr("class", "modal bounceIn animated")
        $("#PieChartInfoModal").modal("show");
      };

      $scope.ShowModalDelegaciones = function (points, evt) {

        var codigosDelegaciones = ',' + $scope
          .SaldosDelegacion
          .codigoDeleg
          .toString() + ',';

        $scope.ObtenerSaldosDespDel(codigosDelegaciones);

        $("#PieChartInfoModalDeleg").attr("class", "modal bounceIn animated")
        $("#PieChartInfoModalDeleg")         
          .modal("show");
      };

      $('.filtroFecha').datetimepicker({format: 'DD/MM/YYYY hh:mm A', locale: 'es'});

      var now = new Date();
      $scope.delegexpanded = false;
      $scope.filtroFechaDesde;
      $scope.filtroFechaHasta;
      $scope.filtroCodigo;
      $scope.filtroDesc;
      $scope.filtroBanco;
      $scope.filtroCli = {
        codCliente: '',
        desCliente: '',
        banks: []
      };
      $scope.bancosConfig = [];
      $scope.ListaConfiguraciones = [];
      $scope.ConfiguracionSeleccionada;
      $scope.Bancos = [];
      $scope.logoBancoSeleccionado;
      $scope.descripcionBancoSeleccionado;
      $scope.oidBancoSeleccionado;
      $scope.labels = [];
      $scope.data = [];
      $scope.labelsDeleg = [];
      $scope.dataDeleg = [];
      $scope.SaldosCliente = {
        data: [],
        codigoCli: [],
        labels: [],
        elements: []
      };
      $scope.SaldosDelegacion = {
        data: [],
        codigoDeleg: [],
        labels: [],
        elements: []
      };
      $scope.options = {
        responsive: true
      };
      $scope.PlanificacionSeleccionada = "Seleccione una planificacion";
      $scope.SaldosDespClientes = [];
      $scope.SaldosDespDelegaciones = [];
      $scope.SaldosDesp = [];
      $scope.SaldosAcred = [];
      $scope.ClientesPla = [];
      //auxiliares
      $scope.filtroFechaDesdeAux;
      $scope.filtroFechaHastaAux;
      $scope.filtroCodigoAux;
      $scope.filtroDescAux;
      $scope.bank = {
        COD_CLIENTE: '',
        DES_CLIENTE: ''
      };

      $scope.ExcelDivisaLocal;
      $scope.ExcelUsd;
      $scope.ExcelEur;

      //var queryString = $location.search();
      var codigoDelegacion = "01"
      $scope.bank.COD_CLIENTE = "237"; //queryString.CodigoCliente;
      $scope.bank.DES_CLIENTE = "Bradesco"; //queryString.DesCliente;
      $scope.Agrupamiento = {};
      $scope.Agrupamiento.value = "Cliente";
      $scope.items = ['Cliente', 'Delegacion'];
      $scope.selection = $scope.items[0];
      $scope.OidPlanificacionSeleccionada;

      ObtenerBancos();

      // DetalleAcreditaciones($scope.bank);
      // ObtenerTortaMaesxCliente("55EEE08F4AD6301EE0535A00530AEF7E");

      function ObtenerTortaMaesxCliente(oidPlanificacion) {
        var ObtenerDatosTortaMaesAction = ActionsService.ObtenerTortaMaesxCliente(oidPlanificacion);

        $http
          .post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, ObtenerDatosTortaMaesAction)
          .then(function (response) {

            var data = angular.fromJson(response.data);
            var i = 0;
            $.each(data.Maes, function (index, value) {

              i++;

              if (i <= 5) {
                $scope
                  .labels
                  .push(value.DESCRIPCION);
                $scope
                  .data
                  .push(value.CANTIDAD_MAES);
              };
            });
          });
      }

      function DetalleAcreditaciones(banco) {

        var expanded = $("#" + banco.COD_CLIENTE).attr("aria-expanded");

        if (expanded == 'false' || expanded == undefined) {

          var GetCollectionDetailsAction = ActionsService.ObtenerDetallesAcreditaciones(0, 10, 1, banco.COD_CLIENTE, $scope.filtroFechaDesdeAux, $scope.filtroFechaHastaAux, codigoDelegacion);

          $http
            .post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, GetCollectionDetailsAction)
            .then(function (response) {

              var data = angular.fromJson(response.data);

              var pagactual = data.Detalles[2].NUM;
              var cantpaginas = data.Detalles[1].NUM;
              banco.DetalleAcreditaciones = data
                .Detalles
                .slice(3);
              banco.DetalleAcreditaciones.page = pagactual;
              banco.DetalleAcreditaciones.Pages = cantpaginas == 0
                ? 1
                : cantpaginas;

            });
        };
      };

      $scope.GetAcreditationDetailsxCli = function (acred) {

        var expanded = $("#" + acred.FYH_ACREDITACION.split('/').join('') + acred.COD_CLIENTE).attr("aria-expanded");

        if (expanded == 'false' || expanded == undefined) {

          var GetAcreditationDetailsxCliAction = ActionsService.ObtenerDetallesAcreditacionesxClientes(0, 10, 1, acred.FYH_ACREDITACION, acred.COD_CLIENTE, codigoDelegacion);

          $http
            .post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, GetAcreditationDetailsxCliAction)
            .then(function (response) {

              var data = angular.fromJson(response.data);

              var pagactual = data.Acreditaciones[2].NUM;
              var cantpaginas = data.Acreditaciones[1].NUM;
              acred.AcreditationbyCli = data
                .Acreditaciones
                .slice(3);
              acred.AcreditationbyCli.page = pagactual;
              acred.AcreditationbyCli.Pages = cantpaginas == 0
                ? 1
                : cantpaginas;
              acred.expanded = true;
            });
        }
      };

      $scope.GetMaes = function (axcli) {

        var GetAcreditationDetailsxCliAction = ActionsService.ObtenerMaes(0, 10, 1, axcli.FYH_ACREDITACION, axcli.COD_CLIENTE, codigoDelegacion);

        $http
          .post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, GetAcreditationDetailsxCliAction)
          .then(function (response) {

            var data = angular.fromJson(response.data);

            var pagactual = data.Maes[2].NUM;
            var cantpaginas = data.Maes[1].NUM;
            axcli.Maes = data
              .Maes
              .slice(3);
            axcli.Maes.page = pagactual;
            axcli.Maes.Pages = cantpaginas == 0
              ? 1
              : cantpaginas;

            axcli.expanded = true;
          });
      };

      $scope.GetMaesxDeleg = function(axdeleg)
      {
        // var GetAcreditationDetailsxDelegAction = ActionsService.ObtenerMaes(0, 10, 1, "2017-12-04T10:15:00", axdeleg.COD_CLIENTE, axdeleg.COD_DELEGACION);
        var GetAcreditationDetailsxDelegAction = ActionsService.ObtenerMaesxDeleg(0, 10, 1, axdeleg.FYH_ACREDITACION, axdeleg.COD_DELEGACION, codigoDelegacion,axdeleg.OID_PLANTA);

        $http
          .post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, GetAcreditationDetailsxDelegAction)
          .then(function (response) {

            var data = angular.fromJson(response.data);

            var pagactual = data.Maes[2].NUM;
            var cantpaginas = data.Maes[1].NUM;
            axdeleg.Maes = data
              .Maes
              .slice(3);
            axdeleg.Maes.page = pagactual;
            axdeleg.Maes.Pages = cantpaginas == 0
              ? 1
              : cantpaginas;

            axdeleg.expanded = true;
          });
      }

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

        $http
          .post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, GetAcreditationDetailsxCliAction)
          .then(function (response) {

            var data = angular.fromJson(response.data);

            var pagactual = data.Maes[2].NUM;
            var cantpaginas = data.Maes[1].NUM;
            AcreditationbyCli.Maes = data
              .Maes
              .slice(3);
            AcreditationbyCli.Maes.page = pagactual;
            AcreditationbyCli.Maes.Pages = cantpaginas == 0
              ? 1
              : cantpaginas;
          });
      };

      $scope.ChangeDetalleMaes = function (mae, direction) {
        var nextPage = 0;

        switch (direction) {
          case 'previous':
            nextPage = mae.Detalles.page - 1;
            break;
          case 'next':
            nextPage = mae.Detalles.page + 1;
            break;
        };

        ActionsService
          .ObtenerDetalleMaes(nextPage, 10, 1, mae.OID_ACREDITACION, mae.OID_MAQUINA)
          .then(function (response) {

            var data = angular.fromJson(response.data);

            var pagactual = data.AcredMaes[0].NUM;
            var cantpaginas = data.AcredMaes[1].NUM;
            mae.Detalles = data
              .AcredMaes
              .slice(3);
            mae.Detalles.page = pagactual;
            mae.Detalles.Pages = cantpaginas == 0
              ? 1
              : cantpaginas;

            mae.expanded = true;
          });
      }

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

        $http
          .post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, bankAction)
          .then(function (response) {

            var data = angular.fromJson(response.data);

            var pagactual = data.Bancos[0].NUM;
            var cantpaginas = data.Bancos[1].NUM;
            $scope.Bancos = data
              .Bancos
              .slice(3);
            $scope.Bancos.page = pagactual;
            $scope.Bancos.Pages = cantpaginas == 0
              ? 1
              : cantpaginas;
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

        $http
          .post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, GetCollectionDetailsAction)
          .then(function (response) {

            var data = angular.fromJson(response.data);

            var pagactual = data.Detalles[2].NUM;
            var cantpaginas = data.Detalles[1].NUM;
            bank.DetalleAcreditaciones = data
              .Detalles
              .slice(3);
            bank.DetalleAcreditaciones.page = pagactual;
            bank.DetalleAcreditaciones.Pages = cantpaginas == 0
              ? 1
              : cantpaginas;
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
    }

        $scope.ChangeAcreditbyDelegPage = function (acreds, acredClient, direction) {

            switch (direction) {
              case 'previous':
                nextPage = acreds.AcreditationbyDeleg.page - 1;
                break;
              case 'next':
                nextPage = acreds.AcreditationbyDeleg.page + 1;
                break;
            };

        var GetAcreditationDetailsxCliAction = ActionsService.ObtenerDetallesAcreditacionesxClientes(nextPage, 10, 1, acredClient.FYH_ACREDITACION, acreds.COD_CLIENTE);

        $http
          .post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, GetAcreditationDetailsxCliAction)
          .then(function (response) {

            var data = angular.fromJson(response.data);

            var pagactual = data.Acreditaciones[2].NUM;
            var cantpaginas = data.Acreditaciones[1].NUM;
            acreds.AcreditationbyCli = data
              .Acreditaciones
              .slice(3);
            acreds.AcreditationbyCli.page = pagactual;
            acreds.AcreditationbyCli.Pages = cantpaginas == 0
              ? 1
              : cantpaginas;
          });

      };

      $scope.ChangeClientesPlanificaion = function (Clientes, direction) {

        var nextPage = 0;

        switch (direction) {
          case 'previous':
            nextPage = Clientes.page - 1;
            break;
          case 'next':
            nextPage = Clientes.page + 1;
            break;
        };

        ActionsService
          .ObtenerCliPlanificacion(nextPage, 5, 1)
          .then(function (response) {
            var data = angular.fromJson(response.data);
            var pagactual = data.Clientes[0].NUM;
            var cantpaginas = data.Clientes[1].NUM;
            $scope.ClientesPla = data
              .Clientes
              .slice(3);
            $scope.ClientesPla.page = pagactual;
            $scope.ClientesPla.Pages = cantpaginas == 0
              ? 1
              : cantpaginas;
          });

      };

      $scope.Filter = function () {
        var error = false;
        var currentFormat = "DD/MM/YYYY"; //moment.localeData().longDateFormat('L');

        $scope.ShowDifferenceError = false;
        $scope.showFhastaError = false;
        $scope.showFdesdeError = false;

        var fechaHasta = moment($("#FechaHasta").val(), currentFormat + "HH:mm A").toDate();
        var fechaDesde = moment($("#FechaDesde").val(), currentFormat + "HH:mm A").toDate();

        if ($("#FechaHasta").val() == "" && $("#FechaDesde").val() == "") {
          fechaDesde = "";
          fechaHasta = "";
        }

        if (FechaDesde != undefined && FechaHasta != undefined) {

          if (new Date(FechaHasta) < new Date(FechaDesde)) {
            $scope.ShowDifferenceError = true
            error = true;
          }

        } else {

          if (FechaDesde != undefined && FechaHasta == undefined) {
            $scope.showFhastaError = true;
            error = true;
          }

          if (FechaHasta != undefined && FechaDesde == undefined) {
            $scope.showFdesdeError = true;
            error = true;
          }

        }

        if (!error) {

          $scope.filtroFechaDesdeAux = fechaDesde;
          $scope.filtroFechaHastaAux = fechaHasta;
          $scope.filtroCodigoAux = angular.copy($scope.filtroCodigo);
          $scope.filtroDescAux = angular.copy($scope.filtroDesc);

          DetalleAcreditaciones($scope.bank)

        }

      }

      $scope.ResetBankSearchPage = function () {
        $scope.filtroCli = {
          codCliente: '',
          desCliente: '',
          banks: []
        };

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

        $http
          .post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, getBanks)
          .then(function (response) {

            var data = angular.fromJson(response.data);

            var pagactual = data.Bancos[2].NUM;
            var cantpaginas = data.Bancos[1].NUM;
            var cantreg = data.Bancos[0].NUM;
            filtroCli.banks = data
              .Bancos
              .slice(3);
            filtroCli.banks.page = pagactual;
            filtroCli.banks.Pages = cantpaginas == 0
              ? 1
              : cantpaginas;
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

      $scope.ObtenerPlanificaciones = function (bank, openSidebar = 'toggle') {
        $scope.bank = null;
        $scope.bank = bank;
        var encontrado = false;

        ActionsService
          .ObtenerPlanificaciones(bank.OID_CLIENTE)
          .then(function (response) {
            var data = angular.fromJson(response.data);
            $scope.ListaConfiguraciones = data.Planificaciones;
            $scope.logoBancoSeleccionado = bank.DES_LOGO_PATH;
            $scope.descripcionBancoSeleccionado = bank.DES_CLIENTE;
            $scope.oidBancoSeleccionado = bank.OID_CLIENTE;

            //info para grafico
            $.each(data.Planificaciones, function (index, value) {

              if (Boolean(value.ESDEFAULT)) {
                $scope.ObtenerGraficoMaesxClientes(value.OID_PLANIFICACION, value.DES_PLANIFICACION);
                encontrado = true;
                return false;
              };
            });

            if (!encontrado) {
              $scope.ObtenerGraficoMaesxClientes(data.Planificaciones[0].OID_PLANIFICACION, data.Planificaciones[0].DES_PLANIFICACION);
              //  //info para cuadro de saldo desplazado
              // $scope.ObtenerSaldosDesp(data.Planificaciones[0].OID_PLANIFICACION);
            }
          });

        DetalleAcreditaciones($scope.bank);

        $("#sidebar").collapse(openSidebar);

      };

      $scope.ObtenerGraficoMaesxClientes = function (oidPlanificacion, desPlanificacion) {
        ActionsService
          .ObtenerTortaMaesxPlanificacion(oidPlanificacion, $scope.oidBancoSeleccionado)
          .then(function (response) {

            var data = angular.fromJson(response.data);

            $scope.SaldosCliente.labels = [];
            $scope.SaldosCliente.data = [];
            $scope.SaldosCliente.codigoCli = [];
            $scope.SaldosCliente.elements = data.Maes;

            $scope.SaldosDelegacion.labels = [];
            $scope.SaldosDelegacion.data = [];
            $scope.SaldosDelegacion.codigoDeleg = [];
            $scope.SaldosDelegacion.elements = data.MaesDeleg;
            //maes x cliente

            $.each(data.Maes, function (index, value) {

              if (value.DESCRIPCION == "OTROS" && value.CANTIDAD_MAES == null) {
                return;
              }

              $scope
                .SaldosCliente
                .labels
                .push(value.DESCRIPCION);
              $scope
                .SaldosCliente
                .data
                .push(value.CANTIDAD_MAES);
              $scope
                .SaldosCliente
                .codigoCli
                .push(value.CODIGO_CLIENTE);
            });

            //--------------------------------------- maes x delegacion
            $.each(data.MaesDeleg, function (index, value) {

              if (value.DESCRIPCION == "OTROS" && value.CANTIDAD_MAES == null) {
                return;
              }

              $scope
                .SaldosDelegacion
                .labels
                .push(value.DESCRIPCION);
              $scope
                .SaldosDelegacion
                .data
                .push(value.CANTIDAD_MAES);
              $scope
                .SaldosDelegacion
                .codigoDeleg
                .push(value.CODIGO_DELEGACION);
            });

            $scope.PlanificacionSeleccionada = desPlanificacion;
            $scope.OidPlanificacionSeleccionada = oidPlanificacion;

          });

        //info para cuadro de saldo desplazado
        $scope.ObtenerSaldosDesp(oidPlanificacion);

        //info para cuadro de saldos a acreditar
        $scope.ObtenerSaldosAcred(oidPlanificacion);
      }

      $scope.ObtenerDetalleMaes = function (mae) {
        ActionsService
          .ObtenerDetalleMaes(0, 10, 1, mae.OID_ACREDITACION, mae.OID_MAQUINA)
          .then(function (response) {

            var data = angular.fromJson(response.data);

            var pagactual = data.AcredMaes[0].NUM;
            var cantpaginas = data.AcredMaes[1].NUM;
            mae.Detalles = data
              .AcredMaes
              .slice(3);
            mae.Detalles.page = pagactual;
            mae.Detalles.Pages = cantpaginas == 0
              ? 1
              : cantpaginas;

            mae.expanded = true;
          });
      }

      //modal maes x cliente
      $scope.ObtenerSaldosDesCli = function (oidClientes) {
        ActionsService
          .ObtenerSaldosDesCli(oidClientes, $scope.OidPlanificacionSeleccionada)
          .then(function (response) {

            var data = angular.fromJson(response.data);
            $scope.SaldosDespClientes = data.SaldosDesCli;

            $.each($scope.SaldosCliente.elements, function (i, datoGrafico) {

              var encontrado = false;
              var contador = 0;

              while (!encontrado && contador <= $scope.SaldosDespClientes.length) {

                if ($scope.SaldosDespClientes[contador].CODIGO_CLIENTE == datoGrafico.CODIGO_CLIENTE) {
                  $scope.SaldosDespClientes[contador].CANTIDAD_MAES = datoGrafico.CANTIDAD_MAES;
                  encontrado = true;
                }
                contador++;
              };
            });
          });
      };

      //modal maes x delegacion
      $scope.ObtenerSaldosDespDel = function (oidDelegaciones) {

        ActionsService
          .ObtenerSaldosDespDeleg(oidDelegaciones, $scope.OidPlanificacionSeleccionada)
          .then(function (response) {

            var data = angular.fromJson(response.data);
            $scope.SaldosDespDelegaciones = data.SaldosDeleg;

            $.each($scope.SaldosDelegacion.elements, function (i, datoGrafico) {

              var encontrado = false;
              var contador = 0;

              while (!encontrado) {

                if ($scope.SaldosDespDelegaciones[contador].CODIGO_DELEGACION == datoGrafico.CODIGO_DELEGACION) {
                  $scope.SaldosDespDelegaciones[contador].CANTIDAD_MAES = datoGrafico.CANTIDAD_MAES;
                  encontrado = true;
                }
                contador++;
              };
            });
          });

      };

      //cuadro saldos desplazados
      $scope.ObtenerSaldosDesp = function (oidPlanificacion) {
        ActionsService
          .ObtenerSaldosDesp(oidPlanificacion)
          .then(function (response) {
            var data = angular.fromJson(response.data);
            $scope.SaldosDesp = data.SaldosDesp;
          });

      };

      //Cuadro saldos a acreditar
      $scope.ObtenerSaldosAcred = function (oidPlanificacion) {
        ActionsService
          .ObtenerSaldosAcred(oidPlanificacion)
          .then(function (response) {
            var data = angular.fromJson(response.data);
            $scope.SaldosAcred = data.SaldosAcred;
          });
      };

      //modal busqueda de clientes
      $scope.ObtenerCliPlanificacion = function () {
        ActionsService
          .ObtenerCliPlanificacion(0, 5, 1)
          .then(function (response) {
            var data = angular.fromJson(response.data);
            var pagactual = data.Clientes[0].NUM;
            var cantpaginas = data.Clientes[1].NUM;
            $scope.ClientesPla = data
              .Clientes
              .slice(3);
            $scope.ClientesPla.page = pagactual;
            $scope.ClientesPla.Pages = cantpaginas == 0
              ? 1
              : cantpaginas;
          });

        $("#BusquedaDeClientes").attr("class", "modal bounceIn animated")
        $("#BusquedaDeClientes").modal("show");

      };

      //modal busqueda de Clientes seleccion
      $scope.SeleccionarBanco = function (banco) {
        $scope.ObtenerPlanificaciones(banco);
        $("#BusquedaDeClientes").modal('hide');
      };

      $scope.$on('chart-create', function (evt, chart) {

        if (chart.canvas.id == "maeschart") {
          document
            .getElementById("maesClienteLegened")
            .innerHTML = chart.generateLegend();
        }

        if (chart.canvas.id == "maesdelegacion") {
          document
            .getElementById("maesDelegacionLegend")
            .innerHTML = chart.generateLegend();
        }
      });

      $scope.SaveAndExport = function (acred) {
        $scope.Save(acred)
      }

      $scope.Save = function (acred) {

        $scope.ExcelDivisaLocal = acred.REALES;
        $scope.ExcelUsd = acred.DOLARES;
        $scope.ExcelEur = acred.EUROS;

      };

      $scope.ExportData = function () {

        $timeout(function () {
          var ha = $("#exportable").html();

          var blob = new Blob([ha], {type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"});
          FileSaver.saveAs(blob, "Report.xls");
        }, 100);

      };

      $scope.safeApply = function (fn) {
        var phase = this.$root.$$phase;
        if (phase == '$apply' || phase == '$digest') {
          if (fn && (typeof(fn) === 'function')) {
            fn();
          }
        } else {
          this.$apply(fn);
        }
      };

      $scope.GroupByDelegacion = function (identificador) {

        if (!$('#GroupingDeleg' + identificador).transition('is visible')) {
          $('#GroupingCli' + identificador)
            .transition('scale', function () {
              $('#GroupingDeleg' + identificador).transition('scale');
            });
        };

        $('#btnCli' + identificador).removeClass("yellow");
        $('#btnDeleg' + identificador).addClass("yellow");
      };

      $scope.GroupByCliente = function (identificador) {
        if (!$('#GroupingCli' + identificador).transition('is visible')) {
          $('#GroupingDeleg' + identificador)
            .transition('scale', function () {
              $('#GroupingCli' + identificador).transition('scale');
            });
        };

        $('#btnDeleg' + identificador).removeClass("yellow");
        $('#btnCli' + identificador).addClass("yellow");
      };

      $scope.GetMaesxDelegacion = function () {
        $scope.delegexpanded = true;
      }

      $scope.CloseaesxDelegacion = function () {
        $scope.delegexpanded = false;
      }

      function ObtenerBancos() {
        ActionsService
          .ObtenerbancosMenu(0, 10, 1)
          .then(function (response) {
            var data = angular.fromJson(response.data);

            var pagactual = data.Bancos[0].NUM;
            var cantpaginas = data.Bancos[1].NUM;
            $scope.Bancos = data
              .Bancos
              .slice(3);
            $scope.Bancos.page = pagactual;
            $scope.Bancos.Pages = cantpaginas == 0
              ? 1
              : cantpaginas;

            if ($scope.Bancos.length > 0) {
              $scope.ObtenerPlanificaciones($scope.Bancos[0], 'hide');
            }

          });
      }

      $scope.DispatchDetalleDeAcreditaciones = function(acred)
      {
         switch ($scope.selection) {
             case "Delegacion":
                 $scope.ObtenerDetalleAcreditacionesxDelegacion(acred);
                 break;
             case "Cliente":
                $scope.GetAcreditationDetailsxCli(acred); 
                break;
         }

      };

      $scope.ObtenerDetalleAcreditacionesxDelegacion = function (acred) {
        ActionsService
          .ObtenerDetalleAcreditacionesxDelegacion(0, 10, 1, acred.OID_ACREDITACION, acred.COD_CLIENTE, codigoDelegacion)
          .then(function (response) {
            var data = angular.fromJson(response.data);

            var cantpaginas = data.Acreditaciones[1].NUM;
            var pagactual = data.Acreditaciones[2].NUM;

            acred.AcreditationbyDeleg = data
              .Acreditaciones
              .slice(3);
            acred.AcreditationbyDeleg.page = pagactual;
            acred.AcreditationbyDeleg.Pages = cantpaginas == 0
              ? 1
              : cantpaginas;
            acred.expanded = true;

          });
      };

      $scope.CloseAll = function () {
        $.each($scope.bank.DetalleAcreditaciones, function (index,element) {
            element.expanded = false;
        });
      };

      $translate.use(navigator.language + '-maedetails');
    
}]);