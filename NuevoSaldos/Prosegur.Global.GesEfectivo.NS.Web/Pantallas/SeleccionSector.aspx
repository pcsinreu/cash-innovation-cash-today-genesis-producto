<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master"
    CodeBehind="SeleccionSector.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.SeleccionSector" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<asp:Content ID="Content5" ContentPlaceHolderID="head" runat="server">
    <!--/-->
    <!-- Bootstrap 2 or Bootstrap 3 ?-->
    <!---->
    <link rel="stylesheet" href="../js/Bootstrap/bootstrap-combined.min.css" />
    <!---->
    <!-- Angular 1.1.5 or 1.2.12 ?-->
    <!---->
    <script src="../js/Bootstrap/angular.js"></script>
    <!-- Font Awesome (optional)-->
    <!-- Live Reload ( for development )-->
    <!--/-->
    <!-- abn-tree ( the thing we are testing )-->
    <!---->
    <script src="../js/Bootstrap/abn_tree_directive.js"></script>
    <link rel="stylesheet" href="../js/Bootstrap/abn_tree.css" />

    <script>

        (function () {
            var app, deps;

            deps = ['angularBootstrapNavTree'];

            if (angular.version.full.indexOf("1.2") >= 0) {
                deps.push('ngAnimate');
            }

            app = angular.module('AbnTest', deps);

            app.controller('AbnTestController', function ($scope, $timeout) {
                var apple_selected, tree, treedata_avm, treedata_geography;
                $scope.my_tree_handler = function (branch) {
                    var _ref;
                    $scope.output = "You selected: " + branch.label;
                    if ((_ref = branch.data) != null ? _ref.description : void 0) {
                        return $scope.output += '(' + branch.data.description + ')';
                    }
                };
                apple_selected = function (branch) {
                    return $scope.output = "APPLE! : " + branch.label;
                };


                //Responsavel por carregar os dados do treeview
                <asp:Literal ID="litSectores" runat="server"></asp:Literal>
                
                
                $scope.my_data = treedata_avm;
                $scope.try_changing_the_tree_data = function () {
                    return $scope.my_data = treedata_avm;
                };
                $scope.my_tree = tree = {};
                $scope.try_async_load = function () {
                    $scope.my_data = [];
                    $scope.doing_async = true;
                    return $timeout(function () {
                        $scope.my_data = treedata_avm;
                        $scope.doing_async = false;
                        return tree.expand_all();
                    }, 1000);
                };
                return $scope.try_adding_a_branch = function () {
                    var b;
                    b = tree.get_selected_branch();
                    return tree.add_branch(b, {
                        label: 'New Branch',
                        data: {
                            something: 42,
                            "else": 43
                        }
                    });
                };
            });

        }).call(this);

        var msg_loading = 'msg_loading';
        var msg_CargandoSectores = 'CargandoSectores';
        var msg_redirecionando = 'msg_redirecionando';
        var msg_producidoError = 'msg_producidoError';
        <asp:Literal ID="litDicionario" runat="server"></asp:Literal>


        function seleccionarSector(identificador) {
            console.log("Seleccionar Sector: " + identificador);

            genesisAlertLoading(msg_loading + msg_CargandoSectores);
            jQuery.ajax({
                url: 'SeleccionSector.aspx/SeleccionSector',
                type: "POST",
                dataType: "json",
                data: "{identificadorSector: '" + identificador + "'}",
                contentType: "application/json; charset=utf-8",
                success: function (data, text) {
                    var json_x = JSON.parse(data.d);
                    if (json_x.CodigoError == "0" && json_x.Respuesta != null) {
                        genesisAlertLoading(msg_loading + msg_redirecionando);
                        window.location.href = json_x.Respuesta;
                    } else {
                        genesisAlertError(json_x.MensajeError, json_x.MensajeErrorDescriptiva);
                    }
                },
                error: function (request, status, error) {
                    genesisAlertError(msg_producidoError + error, request.responseText);
                }
            });
        }


    </script>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
<div ng-app="AbnTest">
    <div  ng-controller="AbnTestController">
        <div style="width:80%;margin-left:10%;margin-right:10%;background:whitesmoke;border:1px solid lightgray;border-radius:5px;">
            <span ng-if="doing_async">...cargando...</span>
            <abn-tree tree-data="my_data" tree-control="my_tree" on-select="my_tree_handler(branch)" expand-level="3"></abn-tree>
        </div>
    <div/>
</div>

<%-- Responsavel por amarzenar o tamanho da tela, para ser utilizado em calculos de outras telas --%>
<asp:HiddenField ID="hidHeightPantalla" runat="server" />
<script type="text/javascript"> document.getElementById('<%= hidHeightPantalla.ClientID  %>').value = $(window).height(); </script>

    <script>
        $(document).ready(function() {
            $('form:first *:input[type!=hidden]:first').focus();
        });
    </script>
</asp:Content>
