angular.module("ConsLocApp")
    .controller("ActionHandlerCtrl", ['QueryService', '$http', '$location', '$routeParams', '$httpParamSerializerJQLike', 'ConsLocConstants', function (QueryService, $http, $location, $routeParams, $httpParamSerializerJQLike, ConsLocConstants) {

        var actions = QueryService.getParameters();

        $http.post(ConsLocConstants.CONST_CONSLOC_SERVICE_PATH, actions).then(function (response) {

            var data = angular.fromJson(response.data);

            QueryService.saveData(data);

            if (data.HtmlJs != undefined) {

                var HtmlPath = data.HtmlJs[0].HTML;
                var JsPath = data.HtmlJs[0].JS;

                var html = $httpParamSerializerJQLike({ html: HtmlPath });
                var js = $httpParamSerializerJQLike({ js: JsPath });

                $location.path(ConsLocConstants.CONST_INFORMATIONREQUESTED_PATH + html + '/' + js);
            } else {
                $("#divGeprModContenido").html(data.HTML[0].innerHtml);
            }

        });

    }]);
