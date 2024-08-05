var app = angular.module('ConsLocApp', [
  'ngRoute',
  'ngFileSaver',
  'pascalprecht.translate',
  'App.filters',
  'chart.js',
  'ngAnimate',
  'counter'  
]);
app.config([
  '$routeProvider',
  '$controllerProvider',
  '$translateProvider',
  '$qProvider',
  'ChartJsProvider',
  function ($routeProvider, $controllerProvider, $translateProvider, $qProvider, ChartJsProvider) {
    $qProvider.errorOnUnhandledRejections(false);

    app.register = {
      controller: $controllerProvider.register
    };

    //// Configure all charts
    ChartJsProvider.setOptions({
      chartColors: [
        // '#3366CC',
        // '#DC3912',
        // '#FF9900',
        // '#109618',
        // '#990099',
        // '#3B3EAC',
        // '#0099C6',
        // '#DD4477',
        // '#66AA00',
        // '#B82E2E',
        // '#316395',
        // '#994499',
        // '#22AA99',
        // '#AAAA11',
        // '#6633CC',
        // '#E67300',
        // '#8B0707',
        // '#329262',
        // '#5574A6',
        // '#3B3EAC'
        '#ffd300',
        '#d0d0d0',
        '#808080',
        '#4c4c4c',
        '#e1e1e1',
        '#fff187',
        '#FFF058'     
      ]
    });
    // // Configure all line charts ChartJsProvider.setOptions('line', {
    // showLines: false }); $translateProvider.preferredLanguage('en');
    $translateProvider.useLoader('asyncLoader');

    $routeProvider
      .when('/', {
      templateUrl: 'app/Dashboard/Dashboard.html',
      controller: 'MaeSalesCtrl'
    })
      .when('/maes', {
        templateUrl: 'app/MaeSales/MaeSales.html',
        controller: 'MaeSalesCtrl'
      })
      .when('/about', {
        templateUrl: 'app/about/about.html',
        controller: 'aboutCtrl'
      })
      .when('/ActionHandler', {
        templateUrl: '/app/ActionHandler/ActionHandler.html',
        controller: 'ActionHandlerCtrl'
      })
      .when('/InformationRequested/:html/:js', {
        templateUrl: function (qr) {

          var html = qr.html;

          var htmlDecoded = decodeURIComponent(html).split("=")[1];

          return htmlDecoded;
        },
        resolve: {
          load: function ($q, $route, $rootScope) {
            var deferred = $q.defer();

            var urlencoded = $route.current.params.js;

            var urlDecoded = decodeURIComponent(urlencoded).split("=")[1];

            var dependencies = [urlDecoded];

            $script(dependencies, function () {
              $rootScope
                .$apply(function () {
                  deferred.resolve();
                });
            })

            return deferred.promise;
          }
        }
      });

  }
]).filter('trimhyphen',function()
{
    return function(value)
    {
        return (!value) ? '' : value.replace(/[-:]/g, '');
    }
});
