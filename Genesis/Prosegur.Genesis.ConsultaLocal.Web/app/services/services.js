var app = angular.module("ConsLocApp");
app.service('QueryService', function () {

  var parameters;
  var data;

  this.addParameters = function (obj) {
    parameters = obj;
  };

  this.getParameters = function () {
    return parameters;
  };

  this.saveData = function (obj) {
    data = obj;
  }

  this.getData = function () {
    return data;
  }

});

app.service('ActionsService', [
  'ConsLocConstants',
  '$http',
  function (ConsLocConstants, $http) {

    this.localizationAction = function (funcionality, culture) {
      return {Accion: ConsLocConstants.CONST_OBTENER_TRADUCCIONES, CodFuncionalidad: funcionality, CodIdioma: culture};
    };

    this.tesoreriaIntegralAction = function () {
      return {Accion: ConsLocConstants.CONST_TESORERIA_INTEGRAL};
    };

    this.ObtenerSectoresClientesAction = function (numPage, maxPerPage, includePage, clientCode) {
      return {Accion: ConsLocConstants.CONST_OBTENER_SECTORES_CLIENTES, NumPagina: numPage, MaxPorPagina: maxPerPage, IncluirPagina: includePage, Cliente: clientCode};
    };

    this.ObtenerSaldosClienteAction = function (numPage, maxPerPage, includePage, clientCode, sectorCode) {
      return {
        Accion: ConsLocConstants.CONST_OBTENER_SALDOS_CLIENTE,
        NumPagina: numPage,
        MaxPorPagina: maxPerPage,
        IncluirPagina: includePage,
        Cliente: clientCode,
        Sector: sectorCode
      };
    };

    //PANTALLA DETALLES MAE

    this.ObtenerBancos = function (numPage, maxPerPage, includePage, fechaDesde, fechaHasta, codCliente, desCliente, codDelegacion) {

      return {
        Accion: ConsLocConstants.CONST_OBTENER_BANCOS,
        NumPagina: numPage,
        MaxPorPagina: maxPerPage,
        IncluirPagina: includePage,
        FechaDesde: fechaDesde,
        FechaHasta: fechaHasta,
        CodCliente: codCliente,
        Descripcion: desCliente,
        CodDelegacion: codDelegacion
      };
    };

    this.ObtenerDetallesAcreditaciones = function (numPage, maxPerPage, includePage, codCliente, fechaDesde, fechaHasta, codDelegacion) {
      return {
        Accion: ConsLocConstants.CONST_OBTENER_ACREDITACIONES,
        NumPagina: numPage,
        MaxPorPagina: maxPerPage,
        IncluirPagina: includePage,
        CodCliente: codCliente,
        FechaDesde: fechaDesde,
        FechaHasta: fechaHasta,
        CodDelegacion: codDelegacion
      };
    };

    this.ObtenerDetallesAcreditacionesxClientes = function (numPage, maxPerPage, includePage, fecha, codCliente, codDelegacion) {
      return {
        Accion: ConsLocConstants.CONST_OBTENER_DETALLESCLIENTES,
        NumPagina: numPage,
        MaxPorPagina: maxPerPage,
        IncluirPagina: includePage,
        Fecha: fecha,
        CodCliente: codCliente,
        CodDelegacion: codDelegacion
      };
    };

    this.ChangeBankSearchPage = function (numPage, maxPerPage, includePage, codCliente, desCliente) {
      return {
        Accion: ConsLocConstants.CONST_OBTENER_BANCOS_FECHAVALOR,
        NumPagina: numPage,
        MaxPorPagina: maxPerPage,
        IncluirPagina: includePage,
        CodCliente: codCliente,
        Descripcion: desCliente
      };
    };

    this.ObtenerMaes = function (numPage, maxPerPage, includePage, fecha, codCliente, codDelegacion) {
      return {
        Accion: ConsLocConstants.CONST_OBTENER_ACREDITACIONES_MAE,
        NumPagina: numPage,
        MaxPorPagina: maxPerPage,
        IncluirPagina: includePage,
        Fecha: fecha,
        CodCliente: codCliente,
        CodDelegacion: codDelegacion
      };
    };

    this.ObtenerTortaMaesxPlanificacion = function (oidPlanificacion, oidBanco) {
      var accion = {
        Accion: ConsLocConstants.CONST_OBTENER_TORTAMAESXCLIENTE,
        OidPlanificacion: oidPlanificacion,
        OidCliente: oidBanco
      };

      return $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, accion)
    };

    //requests

    this.ObtenerbancosMenu = function (numPage, maxPerPage, includePage, fecha, codCliente, codDelegacion) {
      var accion = {
        Accion: ConsLocConstants.CONST_OBTENER_BANCOS,
        NumPagina: numPage,
        MaxPorPagina: maxPerPage,
        IncluirPagina: includePage,
        Fecha: fecha,
        CodCliente: codCliente,
        CodDelegacion: codDelegacion
      };

      return $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, accion);
    };

    this.ObtenerPlanificaciones = function (oidCliente) {
      var accion = {
        Accion: ConsLocConstants.CONST_OBTENER_PLANIFICACIONES,
        OidCliente: oidCliente
      };

      return $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, accion);
    }

    this.ObtenerDetalleMaes = function (numPage, maxPerPage, includePage, oidAcreditacion, oidMaquina) {

      var accion = {
        Accion: ConsLocConstants.CONST_OBTENER_DETALLE_MAES,
        NumPagina: numPage,
        MaxPorPagina: maxPerPage,
        IncluirPagina: includePage,
        OidAcreditacion: oidAcreditacion,
        OidMaquina: oidMaquina
      };

      return $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, accion);
    }

    //modal saldos clientes
    this.ObtenerSaldosDesCli = function (oidClientes, oidPlanificacion) {
      var accion = {

        Accion: ConsLocConstants.CONST_OBTENER_SALDOSDESCLI,
        OidPlanificacion : oidPlanificacion,
        OidClientes: oidClientes
      };

      return $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, accion);
    }

    //modal saldos delegaciones

    this.ObtenerSaldosDespDeleg = function (oidDelegaciones, oidPlanificacion) {
      var accion = {
        Accion: ConsLocConstants.CONST_OBTENER_SALDOSDESDELEG,
        OidPlanificacion : oidPlanificacion,
        OidDelegaciones: oidDelegaciones
      };

      return $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, accion);
    }

    //Cuadro Saldos Desplazados
    this.ObtenerSaldosDesp = function (oidPlanificacion) {
      var accion = {

        Accion: ConsLocConstants.CONST_OBTENER_SALDOSDESP,
        OidPlanificacion: oidPlanificacion
      };

      return $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, accion);
    }

    //Cuadro Saldos a Acreditar
    this.ObtenerSaldosAcred = function (oidPlanificacion) {
      var accion = {
        Accion: ConsLocConstants.CONST_OBTENER_SALDOSACRED,
        OidPlanificacion: oidPlanificacion
      };

      return $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, accion);
    }

    //Modal busqueda de clientes
    this.ObtenerCliPlanificacion = function (numPage, maxPerPage, includePage) {
      var accion = {
        Accion: ConsLocConstants.CONST_OBTENER_CLIPLANIFICACION,
        NumPagina: numPage,
        MaxPorPagina: maxPerPage,
        IncluirPagina: includePage
      };

      return $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, accion);
    };

    //Nivel de Delegaciones x cliente
    this.ObtenerDetalleAcreditacionesxDelegacion = function (numPage, maxPerPage, includePage, oidAcreditacion, codCliente, codDelegacion) {
      var accion = {
        Accion: ConsLocConstants.CONST_OBTENER_DETALLEDELEGACIONES,
        NumPagina: numPage,
        MaxPorPagina: maxPerPage,
        IncluirPagina: includePage,
        OidAcreditacion: oidAcreditacion,
        CodCliente: codCliente,
        CodDelegacion: codDelegacion
      };
      return $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, accion);
    };

    // Nivel de Maes x delegacion
    this.ObtenerMaesxDeleg = function (numPage, maxPerPage, includePage, fecha, codCliente, codDelegacion,oidPlanta) {
      return {
        Accion: ConsLocConstants.CONST_OBTENER_ACREDITACIONES_MAE_DELEG,
        NumPagina: numPage,
        MaxPorPagina: maxPerPage,
        IncluirPagina: includePage,
        Fecha: fecha,
        CodCliente: codCliente,
        CodDelegacion: codDelegacion,
        OidPlanta: oidPlanta
      };
    };
  }
]);

app.factory('asyncLoader', function ($q, $timeout, $http) {

  return function (options) {
    var deferred = $q.defer(),
      translations;

    var action = {
      Accion: 'OBTENER_TRADUCCIONES',
      CodFuncionalidad: options
        .key
        .split('-')[
          options
            .key
            .split('-')
            .length - 1
        ]
        .trim(),
      CodIdioma: options
        .key
        .split('-')[0]
        .trim()
    };

    var objTranslations = new Object();

    $http.post('api/LogApi/EjecutarAccion', action).then(function (response) {

        var translateObject = angular.fromJson(response.data);
        $.each(translateObject.Traducciones, function (key, value) {
          objTranslations[value.COD_EXPRESION] = value.VALOR;
        })

        translations = objTranslations;

        deferred.resolve(translations);

      });

    return deferred.promise;
  };
});

angular
  .module('App.filters', [])
  .filter('placeholder', [function () {
      return function (text, placeholder) {
        // If we're dealing with a function, get the value
        if (angular.isFunction(text)) 
          text = text();
        
        // Trim any whitespace and show placeholder if no content

        if (text == null || text == '') {
          return placeholder
        } else {
          return text
        };

      };
    }
  ]);

app.directive('ucFilters', function () {
  var controller = [
    '$scope',
    function ($scope) {
      function init() {
        $scope.report = angular.copy($scope.reportName);
      };

      init();

      $scope.Search = function (filter) {
        $scope.add({filter: filter});
      }
    }
  ];

  templateUrl = function (elem, attrs) {
    // if (attrs.actionName == 'OBTENER_TESORERIA_INTEGRAL') {    return
    // 'app/Filter/BankFilter.html'; } else if (attrs.actionName ==
    // 'OBTENER_SECTORES') {    return 'app/Filter/SectorFilter.html'; }

    return attrs.filterpath;

  };

  return {
    restrict: 'EA',
    scope: {
      datasource: '=',
      add: '&'
    },
    controller: controller,
    templateUrl: templateUrl
  };
});

// app.directive('myEnter', function () {     return function (scope, element,
// attrs) {         element.bind("keydown keypress", function (event) {    if
// (event.which === 13) {                 scope.$apply(function () {
// scope.$eval(attrs.myEnter);                 }); event.preventDefault();   }
//       });     }; });