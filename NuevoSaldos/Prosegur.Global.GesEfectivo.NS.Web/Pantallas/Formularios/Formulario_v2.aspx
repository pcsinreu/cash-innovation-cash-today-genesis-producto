<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="Formulario_v2.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.Formulario_v2" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Import Namespace="Prosegur.Framework.Dicionario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../../App_Themes/Padrao/css/js_color_picker_v2.css" media="screen" />
    <script src="../../js/color_functions.js" type="text/javascript"></script>
    <script src="../../js/js_color_picker_v2.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Literal ID="litDicionario" runat="server"></asp:Literal>
    <div style="width: 98%; padding: 10px;">
        <div style="width: 100%; height: 320px; display: block; position: relative;">
            <div id="dvPainel" style="float: left; width: 100%;">
                <div id="dvAlert" style="width: 100%; height: auto; display: none;">
                    <asp:Literal ID="litAlert" runat="server"></asp:Literal>
                </div>
                <script type="text/javascript">
                    /* Variaveis Paso 0 */
                    var dvAlert = document.getElementById('dvAlert');
                </script>

                <%--Inicio Passo0--%>
                <div id="dvPaso0" style="width: 100%; height: 320px; display: block;">
                    <div class="ui-panel-titlebar2">
                        <%--Titulo Passo0--%>
                        <asp:Label ID="lblPaso0" runat="server" Text="Paso Uno" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
                    </div>
                    <div style="width: 100%; margin-left: 10px; margin-top: 5px;">
                        <div class="dvUsarFloat">
                            <%--Formulário Passo0--%>
                            <div id="dvCodigoFormulario" runat="server" style="margin-right: 5px;">
                                <asp:Label ID="lblCodigoFormulario" runat="server" Text=""></asp:Label><br />
                                <asp:TextBox ID="txtCodigoFormulario" runat="server" MaxLength="15" Enabled="true" Style="width: 100px;" />
                                <img id="codigoValido" src="" alt="Codigo valido" style="visibility: hidden; width: 13px; height: 13px;" />
                            </div>
                            <div id="dvDescripcionFormulario" runat="server">
                                <asp:Label ID="lblDescripcionFormulario" runat="server" Text=""></asp:Label><br />
                                <asp:TextBox ID="txtDescripcionFormulario" runat="server" MaxLength="50" Enabled="true" Style="width: 250px;" />
                            </div>
                            <div id="dvColorFormulario" runat="server">
                                <asp:Label ID="lblColorFormulario" runat="server" Text=""></asp:Label><br />
                                <asp:TextBox ID="txtColorFormulario" runat="server" MaxLength="10" Enabled="true" Style="width: 70px;"></asp:TextBox>
                            </div>
                            <div id="dvTipoDocumento" runat="server">
                                <asp:Label ID="lblTipoDocumento" runat="server" Text=""></asp:Label><br />
                                <asp:DropDownList ID="ddlTipoDocumento" runat="server"></asp:DropDownList>
                            </div>
                            <div class="dvclear"></div>
                            <div id="dvOpciones" runat="server" style="height: auto; width: 350px;">
                                <div style="margin-bottom: 10px;">
                                    <asp:CheckBox ID="chkDocumentoIndividual" runat="server" />
                                </div>
                                <div style="margin-bottom: 10px;">
                                    <asp:CheckBox ID="chkDocumentoGrupo" runat="server" />
                                </div>
                                <div>
                                    <asp:CheckBox ID="chkActivo" runat="server" />
                                </div>
                            </div>
                            <div class="dvclear"></div>
                            <div id="Div1" style="height: 10px;">&nbsp;</div>
                            <div class="dvclear"></div>
                            <div>
                                <asp:Label ID="lblIcono" runat="server" Text="Icone"></asp:Label><br />
                                <div style="position: relative;">
                                    <div style="float: left;">
                                        <asp:TextBox ID="txtUploadFile" runat="server" Enabled="false" Style="line-height: 16px; padding: 2px 9px; font-size: 8pt; color: #666; border: 1px solid #D8D8D8; font-family: inherit; width: 250px"></asp:TextBox>
                                    </div>
                                    <div class="fileUpload btn btn-primary" style="float: left; margin-left: 5px;">
                                        <asp:Label ID="lblCargar" runat="server" Text=""></asp:Label><br />
                                        <asp:FileUpload ID="fupImagem" runat="server" CssClass="upload" />
                                    </div>

                                </div>
                            </div>
                            <div class="dvclear"></div>
                            <div id="dvImagem" runat="server" style="height: 10px; display: none;">
                                <asp:Image ID="imgPhoto" runat="server" Height="100px" Style="margin-left: 0px" />
                            </div>
                            <div id="dvImagemError" runat="server" style="height: 10px; display: none; font-style: italic; color: #c24131;">
                            </div>
                            <div class="dvclear"></div>
                        </div>
                    </div>
                </div>
                <script type="text/javascript">
                    /* Variaveis Paso 0 */
                    //$("#<%=txtCodigoFormulario.ClientID%>")[0]
                    var p0 = document.getElementById('dvPaso0');
                    var txtCodigoFormulario = document.getElementById('<%= txtCodigoFormulario.ClientID%>');
                    var lblCodigoFormulario = document.getElementById('<%= lblCodigoFormulario.ClientID%>');
                    var txtDescripcionFormulario = document.getElementById('<%= txtDescripcionFormulario.ClientID%>');
                    var lblDescripcionFormulario = document.getElementById('<%= lblDescripcionFormulario.ClientID%>');
                    var txtColorFormulario = document.getElementById('<%= txtColorFormulario.ClientID%>');
                    var lblColorFormulario = document.getElementById('<%= lblColorFormulario.ClientID%>');
                    var ddlTipoDocumento = document.getElementById('<%= ddlTipoDocumento.ClientID%>');
                    var lblTipoDocumento = document.getElementById('<%= lblTipoDocumento.ClientID%>');
                    var codigoValido = document.getElementById("codigoValido");
                    var fupImagem = document.getElementById('<%=fupImagem.ClientID%>');
                    var txtUploadFile = document.getElementById('<%=txtUploadFile.ClientID%>');
                    var imgPhoto = document.getElementById('<%=imgPhoto.ClientID%>');
                    var dvImagem = document.getElementById('<%=dvImagem.ClientID%>');
                    var dvImagemError = document.getElementById('<%=dvImagemError.ClientID%>');
                    var chkDocumentoGrupo = document.getElementById('<%=chkDocumentoGrupo.ClientID%>');
                </script>
                <%--Fim Passo0--%>

                <%--Inicio Passo1--%>
                <div id="dvPaso1" style="width: 100%; height: 320px; display: none;">
                    <div class="ui-panel-titlebar2">
                        <%--Titulo Passo1--%>
                        <asp:Label ID="lblPaso1" runat="server" Text="Paso Dos" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
                    </div>
                    <div style="width: 100%; margin-left: 10px; margin-top: 5px;">
                        <div class="dvUsarFloat">
                            <%--Formulário Passo1--%>
                            <div id="dvCaracteristicaPrincipal" runat="server" style="margin-right: 5px;">
                                <asp:Label ID="lblCaracteristicaPrincipal" runat="server" Text="" Style="font-weight: bold; color: #2996e2;"></asp:Label><br />
                                <br />
                                &nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rbCaracteristicaPrincipalGestionRemesa" runat="server" GroupName="CaracteristicaPrincipal" Text="Gestión de Remesas" Checked="True" />
                                <asp:RadioButton ID="rbCaracteristicaPrincipalGestionBultos" runat="server" GroupName="CaracteristicaPrincipal" Text="Gestión de Bultos" />
                                <asp:RadioButton ID="rbCaracteristicaPrincipalGestionFondos" runat="server" GroupName="CaracteristicaPrincipal" Text="Gestión de Fondos" />
                                <asp:RadioButton ID="rbCaracteristicaPrincipalCierres" runat="server" GroupName="CaracteristicaPrincipal" Text="Cierres" />
                                <asp:RadioButton ID="rbCaracteristicaPrincipalOtrosMovimientos" runat="server" GroupName="CaracteristicaPrincipal" Text="Otros Movimientos" />
                            </div>
                            <div class="dvclear"></div>
                            <div style="height: 10px;">&nbsp;</div>
                            <div class="dvclear"></div>
                            <div id="dvSaldoNegativo" style="margin-right: 5px; display: block; height: auto;">
                                &nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="ckbkPermiteLlegarSaldoNegativo" runat="server" Text="¿El movimiento permite llegar a saldo negativo?" />
                            </div>
                            <div class="dvclear"></div>
                            <div id="dvTipoBulto" style="margin-right: 5px; display: block; margin-top: 10px;">
                                <asp:Label ID="lblTipoBulto" runat="server" Text="" Style="font-weight: bold; color: #2996e2;"></asp:Label><br />
                                <asp:CheckBoxList ID="ckbTipoBulto" runat="server" RepeatColumns="5" Style="margin-left: 10px; margin-top: 10px;"></asp:CheckBoxList>
                            </div>
                            <div class="dvclear"></div>
                            <div id="dvGestionElementos" style="margin-right: 5px; display: block; margin-top: 10px;">
                                <asp:Label ID="lblGestionElementos" runat="server" Text="" Style="font-weight: bold; color: #2996e2;"></asp:Label><br />
                                <br />
                                &nbsp;&nbsp;&nbsp;
								<asp:RadioButton ID="rbGestionElementosAltas" runat="server" GroupName="GestionRemesas" Text="Altas" Checked="True" />
                                <asp:RadioButton ID="rbGestionElementosBajas" runat="server" GroupName="GestionRemesas" Text="Bajas" />
                                <asp:RadioButton ID="rbGestionElementosReenvios" runat="server" GroupName="GestionRemesas" Text="Reenvíos" />
                                <asp:RadioButton ID="rbGestionElementosActas" runat="server" GroupName="GestionRemesas" Text="Actas" />
                                <asp:RadioButton ID="rbGestionElementosSustitucion" runat="server" GroupName="GestionRemesas" Text="Sustitución" />
                            </div>

                            <div class="dvclear"></div>
                            <div id="dvBajas" style="margin-right: 5px; display: none; margin-top: 10px;">
                                <asp:Label ID="lblBajas" runat="server" Text="" Style="font-weight: bold; color: #2996e2;"></asp:Label><br />
                                <br />
                                &nbsp;&nbsp;&nbsp;
							    <asp:RadioButton ID="rbGestionElementosBajasBajaElementos" runat="server" GroupName="GestionRemesasBajas" Checked="True" Text="Baja de una Remesa en el Sistema" />
                                <asp:RadioButton ID="rbGestionElementosBajasSalidasRecorrido" runat="server" GroupName="GestionRemesasBajas" Text="Salida de una Remesa en una Ruta" />
                            </div>
                            <div class="dvclear"></div>
                            <div id="dvReenvios" style="margin-right: 5px; display: none; margin-top: 10px;">
                                <asp:Label ID="lblReenvios" runat="server" Text="" Style="font-weight: bold; color: #2996e2;"></asp:Label><br />
                                <br />
                                &nbsp;&nbsp;&nbsp;
							    <asp:RadioButton ID="rbGestionElementosReenviosEntreSectores" runat="server" GroupName="GestionRemesasReenvios" Checked="True" Text="Entre Sectores" />
                                <asp:RadioButton ID="rbGestionElementosReenviosEntrePlantas" runat="server" GroupName="GestionRemesasReenvios" Text="Entre Plantas" />

                                <asp:CheckBox ID="chkGestionElementosReenviosReenvioAutomatico" runat="server" GroupName="GestionRemesasReenvios" Checked="False" Text="¿Utilizar este formulario para Documentos de Reenvío Automatico?" />
                            </div>
                            <div class="dvclear"></div>
                            <div id="dvActas" style="margin-right: 5px; display: none; margin-top: 10px;">
                                <asp:Label ID="lblActas" runat="server" Text="" Style="font-weight: bold; color: #2996e2;"></asp:Label><br />
                                <br />
                                &nbsp;&nbsp;&nbsp;
							    <asp:RadioButton ID="rbGestionElementosActasActaRecuento" runat="server" GroupName="GestionRemesasActas" Text="Acta de Recuento" Checked="True" />
                                <asp:RadioButton ID="rbGestionElementosActasActaClasificado" runat="server" GroupName="GestionRemesasActas" Text="Acta de Clasificado" />
                                <asp:RadioButton ID="rbGestionElementosActasActaEmbolsado" runat="server" GroupName="GestionRemesasActas" Text="Acta de Embolsado" />
                                <asp:RadioButton ID="rbGestionElementosActasActaDesembolsado" runat="server" GroupName="GestionRemesasActas" Text="Acta de Desembolsado" />
                            </div>
                            <div class="dvclear"></div>
                            <div id="dvFiltro" style="margin-right: 5px; display: none; margin-top: 10px;">
                                <asp:Label ID="lblFiltro" runat="server" Text="" Style="font-weight: bold; color: #2996e2;"></asp:Label><br />
                                <br />
                                &nbsp;&nbsp;&nbsp;
							    <asp:DropDownList ID="ddlFiltro" runat="server" SkinID="padrao-dropdownlist"></asp:DropDownList>
                            </div>
                            <div class="dvclear"></div>
                            <div id="dvGestionFondos" style="margin-right: 5px; display: none; margin-top: 10px;">
                                <asp:Label ID="lblGestionFondos" runat="server" Text="" Style="font-weight: bold; color: #2996e2;"></asp:Label><br />
                                <br />
                                &nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rbGestionFondosMovimientoFondos" runat="server" GroupName="GestionFondos" Checked="True" Text="Movimiento de Fondos" />
                                <asp:RadioButton ID="rbGestionFondosAjustes" runat="server" GroupName="GestionFondos" Text="Ajustes" />
                                <asp:RadioButton ID="rbGestionFondosSustitucion" runat="server" GroupName="GestionFondos" Text="Sustitución" />
                                <asp:RadioButton ID="rbGestionFondosSolicitacion" runat="server" GroupName="GestionFondos" Text="Solicitación" />
                            </div>
                            <div class="dvclear"></div>
                            <div id="dvFondosMovimiento" style="margin-right: 5px; display: none; margin-top: 10px;">
                                <asp:Label ID="lblFondosMovimiento" runat="server" Text="" Style="font-weight: bold; color: #2996e2;"></asp:Label><br />
                                <br />
                                &nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rbGestionFondosMovimientoFondosEntreSectores" runat="server" GroupName="GestionFondosMovimientoFondos" Checked="True" Text="Entre Sectores" />
                                <asp:RadioButton ID="rbGestionFondosMovimientoFondosEntreCanales" runat="server" GroupName="GestionFondosMovimientoFondos" Text="Entre Canales" />
                                <asp:RadioButton ID="rbGestionFondosMovimientoFondosEntrePlantas" runat="server" GroupName="GestionFondosMovimientoFondos" Text="Entre Plantas" />
                                <asp:RadioButton ID="rbGestionFondosMovimientoFondosEntreClientes" runat="server" GroupName="GestionFondosMovimientoFondos" Text="Entre Clientes" />
                            </div>
                            <div class="dvclear"></div>
                            <div id="dvCierres" style="margin-right: 5px; display: none; margin-top: 10px;">
                                <asp:Label ID="lblCierres" runat="server" Text="" Style="font-weight: bold; color: #2996e2;"></asp:Label><br />
                                <br />
                                &nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rbCierresTesoro" runat="server" GroupName="Cierres" Text="Cierre de Tesoro" Checked="True" />
                                <asp:RadioButton ID="rbCierresCaja" runat="server" GroupName="Cierres" Text="Cierre de Caja" />
                                <asp:RadioButton ID="rbCierresCuadreFisico" runat="server" GroupName="Cierres" Text="Cuadre Físico" />
                            </div>
                            <div class="dvclear"></div>
                        </div>
                    </div>
                </div>
                <script type="text/javascript">
                    /* Variaveis Paso 1 */
                    var p1 = document.getElementById('dvPaso1');
                    var rbRemesas = document.getElementById('<%= rbCaracteristicaPrincipalGestionRemesa.ClientID%>');
                    var rbBultos = document.getElementById('<%= rbCaracteristicaPrincipalGestionBultos.ClientID%>');
                    var rbFondos = document.getElementById('<%= rbCaracteristicaPrincipalGestionFondos.ClientID%>');
                    var rbCierres = document.getElementById('<%= rbCaracteristicaPrincipalCierres.ClientID%>');
                    var dvGestionElementos = document.getElementById('dvGestionElementos');
                    var dvGestionFondos = document.getElementById('dvGestionFondos');
                    var dvCierres = document.getElementById('dvCierres');
                    var dvTipoBulto = document.getElementById('dvTipoBulto');
                    var dvSaldoNegativo = document.getElementById('dvSaldoNegativo');
                    var dvBajas = document.getElementById('dvBajas');
                    var dvReenvios = document.getElementById('dvReenvios');
                    var dvActas = document.getElementById('dvActas');
                    var dvFiltro = document.getElementById('dvFiltro');
                    var dvFondosMovimiento = document.getElementById('dvFondosMovimiento');
                    var rbAltas = document.getElementById('<%= rbGestionElementosAltas.ClientID%>');
                    var rbBajas = document.getElementById('<%= rbGestionElementosBajas.ClientID%>');
                    var rbReenvios = document.getElementById('<%= rbGestionElementosReenvios.ClientID%>');
                    var rbActas = document.getElementById('<%= rbGestionElementosActas.ClientID%>');
                    var rbSustitucion = document.getElementById('<%= rbGestionElementosSustitucion.ClientID%>');
                    var rbGestionFondosMovimientoFondos = document.getElementById('<%= rbGestionFondosMovimientoFondos.ClientID%>');
                    var rbEntreClientes = document.getElementById('<%= rbGestionFondosMovimientoFondosEntreClientes.ClientID%>');
                    var rbEntreSectores = document.getElementById('<%= rbGestionFondosMovimientoFondosEntreSectores.ClientID%>');
                    var rbEntreCanales = document.getElementById('<%= rbGestionFondosMovimientoFondosEntreCanales.ClientID%>');
                    var rbEntrePlantas = document.getElementById('<%= rbGestionFondosMovimientoFondosEntrePlantas.ClientID%>');


                    var rbSolicitacion = document.getElementById('<%= rbGestionFondosSolicitacion.ClientID%>');
                    var rbCaja = document.getElementById('<%= rbCierresCaja.ClientID%>');
                    
                </script>
                <%--Fim Passo1--%>

                <%--Inicio Passo2--%>
                <div id="dvPaso2" style="width: 100%; height: 320px; display: none;">
                    <div class="ui-panel-titlebar2">
                        <asp:Label ID="lblPaso2" runat="server" Text="Paso Tres" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
                    </div>
                    <div style="width: 100%; margin-left: 5px; margin-top: 5px;">
                        <div class="dvUsarFloat">
                            <div id="dvTipoSectorOrigen" runat="server" style="margin: 15px; height: auto !important;">
                                <asp:Label ID="lblTipoSectorOrigen" runat="server" Text="" Style="font-weight: bold; color: #2996e2;"></asp:Label><br />
                                <div style="width: 200px; height: 150px; overflow: auto; border-radius: 5px !important; padding: 2px; border: solid 1px #A8A8A8;">
                                    <asp:CheckBoxList ID="cklTipoSectorOrigen" runat="server" RepeatColumns="1">
                                    </asp:CheckBoxList>
                                </div>
                                <br />
                                <div style="width: 200px; text-align: right;">
                                    <span id="legTipoSectorOrigen" style="font-style: italic;"></span>
                                    <button type="button" name="btnAgregarTodosTipoSectorOrigen" value="" id="btnAgregarTodosTipoSectorOrigenEntrada"
                                        onclick="javascript: seleccionarTodos('TipoSectorOrigen', true);" style="padding: 0px; border-style: none; background-color: transparent; width: 12px; height: 12px;"
                                        onblur="javascript: cambiarLegenda('TipoSectorOrigen', 0);" onfocus="javascript: cambiarLegenda('TipoSectorOrigen', 1);">
                                        <img src="../../Imagenes/ckeck_true.png" alt="" />
                                    </button>
                                    <button type="button" name="btnDesAgregarTodosTipoSectorOrigen" value="" id="btnDesAgregarTodosTipoSectorOrigen"
                                        onclick="javascript: seleccionarTodos('TipoSectorOrigen', false);" style="padding: 0px; border-style: none; background-color: transparent; width: 12px; height: 12px;"
                                        onkeydown="javascript: HomeEnd();"
                                        onblur="javascript: cambiarLegenda('TipoSectorOrigen', 0);" onfocus="javascript: cambiarLegenda('TipoSectorOrigen', 2);">
                                        <img src="../../Imagenes/ckeck_false.png" alt="" />
                                    </button>
                                </div>
                            </div>
                            <div id="dvTipoSectorDestino" runat="server" style="margin: 15px; height: auto !important;">
                                <asp:Label ID="lblTipoSectorDestino" runat="server" Text="" Style="font-weight: bold; color: #2996e2;"></asp:Label><br />
                                <div style="width: 200px; height: 150px; overflow: auto; border-radius: 5px !important; padding: 2px; border: solid 1px #A8A8A8;">
                                    <asp:CheckBoxList ID="cklTipoSectorDestino" runat="server" RepeatColumns="1">
                                    </asp:CheckBoxList>
                                </div>
                                <br />
                                <div style="width: 200px; text-align: right;">
                                    <span id="legTipoSectorDestino" style="font-style: italic;"></span>
                                    <button type="button" name="btnAgregarTodosTipoSectorDestino" value="" id="btnAgregarTodosTipoSectorDestino"
                                        onclick="javascript: seleccionarTodos('TipoSectorDestino', true);" style="padding: 0px; border-style: none; background-color: transparent; width: 12px; height: 12px;"
                                        onblur="javascript: cambiarLegenda('TipoSectorDestino', 0);" onfocus="javascript: cambiarLegenda('TipoSectorDestino', 1);">
                                        <img src="../../Imagenes/ckeck_true.png" alt="" />
                                    </button>
                                    <button type="button" name="btnDesAgregarTodosTipoSectorDestino" value="" id="btnDesAgregarTodosTipoSectorDestino"
                                        onclick="javascript: seleccionarTodos('TipoSectorDestino', false);" style="padding: 0px; border-style: none; background-color: transparent; width: 12px; height: 12px;"
                                        onkeydown="javascript: HomeEnd();"
                                        onblur="javascript: cambiarLegenda('TipoSectorDestino', 0);" onfocus="javascript: cambiarLegenda('TipoSectorDestino', 2);">
                                        <img src="../../Imagenes/ckeck_false.png" alt="" />
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <script type="text/javascript">
                    /* Variaveis Paso 2 */
                    var p2 = document.getElementById('dvPaso2');
                    var origen = document.getElementById('<%= dvTipoSectorOrigen.ClientID%>');
                    var destino = document.getElementById('<%= dvTipoSectorDestino.ClientID%>');
                </script>
                <%--Fim Passo2--%>

                <%--Inicio Passo3--%>
                <div id="dvPaso3" style="width: 100%; height: 320px; display: none;">
                    <div class="ui-panel-titlebar2">
                        <asp:Label ID="lblPaso3" runat="server" Text="Paso Cuatro" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
                    </div>
                    <div style="width: 100%; margin-left: 5px; margin-top: 5px;">
                        <div class="dvUsarFloat">

                        </div>
                    </div>
                </div>
                <script type="text/javascript">
                    /* Variaveis Paso 3 */
                    var p3 = document.getElementById('dvPaso3');
                </script>
                <%--Fim Passo3--%>

                <%--Inicio Passo4--%>
                <div id="dvPaso4" style="width: 100%; height: 320px; display: none;">
                    <div class="ui-panel-titlebar2">
                        <asp:Label ID="lblPaso4" runat="server" Text="Paso Cinco" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
                    </div>
                    <div style="width: 100%; margin-left: 5px; margin-top: 5px;">
                        <div class="dvUsarFloat">
                            <div id="dvIacIndividual" runat="server" style="margin-right: 5px; margin-top: 10px; height:auto !important;">
                                <asp:Label ID="lblIacIndividual" runat="server" Text="" Style="font-weight: bold; color: #2996e2;"></asp:Label><br />
                                <asp:DropDownList ID="ddlIacIndividual" runat="server" ></asp:DropDownList>
							</div>
                            <div id="dvIacGrupo" runat="server" style="margin-right: 5px; margin-top: 10px; height:auto !important;">
                                <asp:Label ID="lblIacGrupo" runat="server" Text="" Style="font-weight: bold; color: #2996e2;"></asp:Label><br />
                                <asp:DropDownList ID="ddlIacGrupo" runat="server" ></asp:DropDownList>
                            </div>
                            <div class="dvclear"></div>
                            <div id="dvOpciones4" runat="server" style="margin-right: 5px; margin-top: 10px; height:auto !important;">
							    <div id="dvCodigoExternoObligatorio" style="width:100%;"><asp:CheckBox ID="chkCodigoExternoObligatorio" runat="server" Checked="false" Text="" /></div>
								<div id="dvIntegracionSalidas" style="width:100%;"><asp:CheckBox ID="chkIntegracionSalidas" runat="server" Checked="false" Text="" /></div>
								<div id="dvIntegracionRecepcionEnvio" style="width:100%;"><asp:CheckBox ID="chkIntegracionRecepcionEnvio" runat="server" Checked="false" Text="" /></div>
								<div id="dvIntegracionLegado" style="width:100%;"><asp:CheckBox ID="chkIntegracionLegado" runat="server" Checked="false" Text="" /></div>
								<div id="dvIntegracionConteo" style="width:100%;"><asp:CheckBox ID="chkIntegracionConteo" runat="server" Checked="false" Text="" /></div>
                                <div id="dvSolicitacionFondos" style="width:100%;"><asp:CheckBox ID="chkSolicitacionFondos" runat="server" Checked="false" Text="" /></div>
                                <div id="dvContestarSolicitacionFondos" style="width:100%;"><asp:CheckBox ID="chkContestarSolicitacionFondos" runat="server" Checked="false" Text="" /></div>
                            </div>
                            <div class="dvclear"></div>
                            <div id="dvImprime" runat="server" style="margin-right: 5px; margin-top: 10px; height:auto !important;">
							    <asp:CheckBox ID="chkImprime" runat="server" Text="" /><br />
							</div>
                            <div class="dvclear"></div>
                            <div id="dvCopias" runat="server" style="display: none; margin-right: 5px; margin-top: 10px; height:auto !important;">
                                <asp:Label ID="lblImprimeCopias" runat="server" Text=""></asp:Label><br />
                                <asp:TextBox ID="txtImprimeCopia" MaxLength="36" runat="server" ></asp:TextBox>
                                <button type="button" onclick="javascript:adicionarCopia();" class="ui-datepicker-trigger" style="width: auto; height: auto; padding: 0px; margin: 0px;">
                                    <img src="../../Imagenes/Agregar.png" alt="Añadir" /></button>
                            </div>
                            <div class="dvclear"></div>
                            <div id="dvInfValores" runat="server" style="display: none; margin-top: 0px; height: auto;">
                                <asp:Label ID="lblInfValores" runat="server" Text="Valores"></asp:Label><br />
                                <div id="lstItensAdicionados" runat="server" class="BuscarPorvalores"></div>
                                <asp:HiddenField ID="hidItensAdicionados" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <script type="text/javascript">
                    /* Variaveis Paso 4 */
                    var p4 = document.getElementById('dvPaso4');
                    var txtImprimeCopia = document.getElementById('<%= txtImprimeCopia.ClientID%>');
                    var lstItensAdicionados = document.getElementById('<%= lstItensAdicionados.ClientID%>');
                    var hidItensAdicionados = document.getElementById('<%= hidItensAdicionados.ClientID%>');
                    var chkImprime = document.getElementById('<%= chkImprime.ClientID%>');
                    var dvInfValores = document.getElementById('<%= dvInfValores.ClientID%>');
                    var dvCopias = document.getElementById('<%= dvCopias.ClientID%>');
                    var dvIacGrupo = document.getElementById('<%= dvIacGrupo.ClientID%>');
                    var chkSolicitacionFondos = document.getElementById('<%= chkSolicitacionFondos.ClientID%>');
                    var chkIntegracionSalidas = document.getElementById('<%= chkIntegracionSalidas.ClientID%>');
                    var dvSolicitacionFondos = document.getElementById('dvSolicitacionFondos');
                    var dvIntegracionSalidas = document.getElementById('dvIntegracionSalidas');
                </script>
                <%--Fim Passo4--%>

                <%--Inicio Passo5--%>
                <div id="dvPaso5" style="width: 100%; height: 320px; display: none;">
                    <div class="ui-panel-titlebar2">
                        <asp:Label ID="lblPaso5" runat="server" Text="Paso Cinco" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
                    </div>
                    <div style="width: 100%; margin-left: 5px; margin-top: 5px;">
                        <div class="dvUsarFloat">
                            <div id="Div3" runat="server" style="margin-right: 5px; margin-top: 10px;">
                            </div>
                        </div>
                    </div>
                </div>
                <script type="text/javascript">
                    /* Variaveis Paso 5 */
                    var p5 = document.getElementById('dvPaso5');
                </script>
                <%--Fim Passo5--%>
            </div>
        </div>

        <%--Inicio Controles--%>
        <div class="dvUsarFloat" style="width: 100%;">
            <div id="dvbtnCancelar" style="margin: 12px 25px 2px 0px; height: auto; float: right;">
                <input id="btnCancelar" type="button" value="Cancelar" class="ui-button" style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" onclick="javascript: cancelar();" />
            </div>
            <div id="dvbtnGrabar" style="margin: 12px 25px 2px 0px; height: auto; float: right;">
                <asp:Button ID="btnGrabar" runat="server" Text="Grabar" class="ui-button" Style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" />
            </div>
            <div id="dvbtnSeguinte" style="margin: 12px 25px 2px 0px; height: auto; float: right;">
                <input id="btnSeguinte" type="button" value="Seguinte" class="ui-button" style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" onclick="javascript: controlPanel(true);" />
            </div>
            <div id="dvbtnAnterior" style="margin: 12px 25px 2px 0px; height: auto; min-width: 40px; float: right;">
                <input id="btnAnterior" type="button" value="Anterior" class="ui-button" style="height: 20px; padding: 0px !important; width: 100px; margin: 1px;" onclick="javascript: controlPanel(false);" />
            </div>
            <div style="margin: 12px 25px 2px 0px; height: auto; min-width: 40px; float: left;">
                <div class="ballonPainel">
                    <div id="dvPuntoPaso0">1</div>
                    <div id="dvPuntoPaso1">2</div>
                    <div id="dvPuntoPaso2">3</div>
                    <div id="dvPuntoPaso3">4</div>
                    <div id="dvPuntoPaso4">5</div>
                    <div id="dvPuntoPaso5">6</div>
                </div>
            </div>
            <div class="dvclear">
            </div>
        </div>
        <script type="text/javascript">
            /* Variaveis Controles */
            var btnCancelar = document.getElementById('btnCancelar');
            var btnGrabar = document.getElementById('btnGrabar');
            var btnSeguinte = document.getElementById('btnSeguinte');
            var btnAnterior = document.getElementById('btnAnterior');
            var pp0 = document.getElementById('dvPuntoPaso0');
            var pp1 = document.getElementById('dvPuntoPaso1');
            var pp2 = document.getElementById('dvPuntoPaso2');
            var pp3 = document.getElementById('dvPuntoPaso3');
            var pp4 = document.getElementById('dvPuntoPaso4');
            var pp5 = document.getElementById('dvPuntoPaso5');
        </script>
        <%--Fim Controles--%>
    </div>

    <script type="text/javascript">

        /* Indica qual painel está sendo exibido */
        var painelAtual = 0;

        /* Valida o painel atual e se possivel indica qual o proximo painel a ser exibido e chama a funçao para configurar o painel */
        function controlPanel(a) {
            if (validarPanel()) {
                if (a) {
                    // Próximo Painel
                    painelAtual = painelAtual + 1;
                } else {
                    // Painel Anterior
                    painelAtual = painelAtual - 1;
                }
            }
            configurarPanel();
        }

        /* Quando solicitado para alterar o painel, deve validar se os campos do painel atual estão preenchido corretamente */
        function validarPanel() {

            var error = '';
            var errorVacio = '';
            var errorCodigo = '';
            switch (painelAtual) {
                case 0:
                    if (txtCodigoFormulario.value == '') {
                        errorVacio += "&nbsp;&nbsp;&nbsp;- " + lblCodigoFormulario.innerText + '<br />';
                    }
                    if (txtDescripcionFormulario.value == '') {
                        errorVacio += "&nbsp;&nbsp;&nbsp;- " + lblDescripcionFormulario.innerText + '<br />';
                    }
                    if (txtColorFormulario.value == '') {
                        errorVacio += "&nbsp;&nbsp;&nbsp;- " + lblColorFormulario.innerText + '<br />';
                    }
                    if (ddlTipoDocumento.selectedIndex == 0) {
                        errorVacio += "&nbsp;&nbsp;&nbsp;- " + lblTipoDocumento.innerText + '<br />';
                    }
                    if (codigoValido.src.indexOf('error') > 0) {
                        errorCodigo += codigoValido.alt + "<br />";
                    }
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }

            var numError = 1;
            if (errorVacio.length > 0) {
                error += numError + ") " + CamposObrigatorios + "<br />" + errorVacio;
                numError += 1;
            }
            if (errorCodigo.length > 0) {
                error += numError + ") " + errorCodigo;
                numError += 1;
            }

            if (error.length > 0) {
                dvAlert.innerHTML = "<div class='error-box alert'> <div class='msg'>" + error + "</div> <p><a class='toggle-alert' href='#'>Toggle</a></p> </div>";
                jQuery(document).ready(function () { $(".alert .toggle-alert").click(function () { $(this).closest(".alert").slideUp(); return false; }); });
                return false;
            } else {
                dvAlert.innerHTML = '';
                return true;
            }
        }

        /* Configura estado inicial dos paineis */
        function configurarPanel() {

            dvAlert.style.display = "none";
            btnAnterior.setAttribute("disabled", "disabled");
            btnSeguinte.setAttribute("disabled", "disabled");
            p0.style.display = "none";
            p1.style.display = "none";
            p2.style.display = "none";
            p3.style.display = "none";
            p4.style.display = "none";
            p5.style.display = "none";
            pp0.className = "";
            pp1.className = "";
            pp2.className = "";
            pp3.className = "";
            pp4.className = "";
            pp5.className = "";

            if (painelAtual > 0) {
                btnAnterior.removeAttribute("disabled");
            }
            if (painelAtual < 5) {
                btnSeguinte.removeAttribute("disabled");
            }
            switch (painelAtual) {
                case 1:
                    p1.style.display = "block";
                    pp1.className = "selecionado";
                    break;
                case 2:
                    p2.style.display = "block";
                    pp2.className = "selecionado";
                    controlePaso2();
                    break;
                case 3:
                    p3.style.display = "block";
                    pp3.className = "selecionado";
                    break;
                case 4:
                    p4.style.display = "block";
                    pp4.className = "selecionado";
                    controlePaso4();
                    break;
                case 5:
                    p5.style.display = "block";
                    pp5.className = "selecionado";
                    break;
                default:
                    p0.style.display = "block";
                    pp0.className = "selecionado";
                    break;
            }

            if (dvAlert.innerText != "") {
                $("#dvAlert").show("slow");
            } else {
                dvAlert.style.display = "none";
            }
        }

        /* Valida o Código do Formulario - é disparado ao sair do campo txtCodigoFormulario */
        function validarCodigoFormulario() {
            if (Modo == 'Alta') {
                PageMethods.ValidarCodigoFormulario(txtCodigoFormulario.value, onSucess, onError);
            }
        }
        function onSucess(response) {
            if (response == 'OK') {
                codigoValido.src = '../../Imagenes/success.png';
            } else {
                codigoValido.src = '../../Imagenes/error.png';
                codigoValido.alt = response;
            }
            if (txtCodigoFormulario.value == '') {
                codigoValido.style.visibility = 'hidden';
            } else {
                codigoValido.style.visibility = 'visible';
            }
        }
        function onError(e) {
            alert(e);
        }

        /* Controla as opções do Paso 2 */
        function controlePaso1() {
            dvGestionElementos.style.display = 'none';
            dvGestionFondos.style.display = 'none';
            dvCierres.style.display = 'none';
            dvTipoBulto.style.display = 'none';
            dvSaldoNegativo.style.display = 'none';
            dvBajas.style.display = 'none';
            dvReenvios.style.display = 'none';
            dvActas.style.display = 'none';
            dvFiltro.style.display = 'none';
            dvFondosMovimiento.style.display = 'none';

            if (rbRemesas.checked == true) {
                $("#dvGestionElementos").show("fast");
                $("#dvTipoBulto").show("fast");
                $("#dvSaldoNegativo").show("fast");
                controlePaso1Elementos();
            } else if (rbBultos.checked == true) {
                $("#dvGestionElementos").show("fast");
                $("#dvTipoBulto").show("fast");
                $("#dvSaldoNegativo").show("fast");
                controlePaso1Elementos();
            } else if (rbFondos.checked == true) {
                $("#dvGestionFondos").show("fast");
                $("#dvSaldoNegativo").show("fast");
                controlePaso1Fondos();
            } else if (rbCierres.checked == true) {
                $("#dvCierres").show("fast");
            }
        }
        /* Controla as opções do Paso 2 de Elementos */
        function controlePaso1Elementos() {
            dvBajas.style.display = 'none';
            dvReenvios.style.display = 'none';
            dvActas.style.display = 'none';
            dvFiltro.style.display = 'none';

            if (rbAltas.checked == true) {

            } else if (rbBajas.checked == true) {
                $("#dvBajas").show("fast");
                $("#dvFiltro").show("fast");
            } else if (rbReenvios.checked == true) {
                $("#dvReenvios").show("fast");
                $("#dvFiltro").show("fast");
            } else if (rbActas.checked == true) {
                $("#dvActas").show("fast");
                $("#dvFiltro").show("fast");
            } else if (rbSustitucion.checked == true) {

            }
        }
        /* Controla as opções do Paso 2 de Fondos */
        function controlePaso1Fondos() {
            dvFondosMovimiento.style.display = 'none';
            if (rbFondos.checked == true) {
                $("#dvFondosMovimiento").show("fast");
            }
        }

        /* Controle do Paso 3 */
        function controlePaso2() {
            origen.style.display = 'block';
            destino.style.display = 'none';
            if ((rbBultos.checked && (rbReenvios.checked || rbAltas.checked)) ||
                (rbRemesas.checked && (rbReenvios.checked || rbAltas.checked)) ||
                (rbFondos.checked && rbFondos.checked && !rbEntreClientes.checked) ||
                (rbFondos.checked && rbSolicitacion.checked) ||
                (rbCierres.checked && rbCaja.checked)) {
                destino.style.display = 'block';
            }
        }

        /* Controle do Paso 4 */
        function controlePaso4() {

            dvSolicitacionFondos.style.display = 'none';
            chkSolicitacionFondos.Enabled = true;
            dvIntegracionSalidas.style.display = 'none';
            chkIntegracionSalidas.Enabled = true;

            //CheckBox Solicitação de Fundos
            //Gestión de Fondos – Movimiento de Fondos – Entre Sectores
            //Gestión de Fondos – Movimiento de Fondos – Entre Plantas
            if (!chkDocumentoGrupo.checked && ((rbFondos.checked && rbMovimientoFondos.checked) && (rbEntreSectores.checked || rbEntrePlantas.checked))){
                alert('t');
                dvSolicitacionFondos.style.display = 'block';
                dvIntegracionSalidas.style.display = 'block';
                //Se for solicitação fundos ou contestação de fundos é obrigatório ter integração com o salidas
                if (chkSolicitacionFondos.Checked){
                    chkIntegracionSalidas.Checked = true;
                    chkIntegracionSalidas.Enabled = false;
                    chkContestarSolicitacionFondos.Checked = false;
                    chkContestarSolicitacionFondos.Enabled = false;
                }else if(chkContestarSolicitacionFondos.Checked){
                    chkIntegracionSalidas.Checked = true;
                    chkIntegracionSalidas.Enabled = false;
                    chkSolicitacionFondos.Enabled = false;
                    chkSolicitacionFondos.Checked = false;
                }

            } else if ((!chkDocumentoGrupo.checked && ((rbRemesas.checked && rbReenvios.checked) && (rbEntreSectores.checked || rbEntrePlantas.checked))) ||
                       (!chkDocumentoGrupo.checked && ((rbBultos.checked && rbReenvios.checked) && (rbEntreSectores.checked || rbEntrePlantas.checked)))){

                dvIntegracionSalidas.style.display = 'block';
                //'Se for solicitação fundos ou contestação de fundos é obrigatório ter integração com o salidas
                if (chkSolicitacionFondos.Checked){
                    chkIntegracionSalidas.Checked = true;
                    chkIntegracionSalidas.Enabled = false;
                    chkContestarSolicitacionFondos.Checked = false;
                    chkContestarSolicitacionFondos.Enabled = false;
                }else if(chkContestarSolicitacionFondos.Checked){
                    chkIntegracionSalidas.Checked = true;
                    chkIntegracionSalidas.Enabled = false;
                    chkSolicitacionFondos.Enabled = false;
                    chkSolicitacionFondos.Checked = false;
                }
            }

            //Se for Substituição, Solicitação de fundos ou Contetar Fundos não tem terminos de grupo
            if (!(((rbRemesas.checked && rbSustitucion.checked) || (rbBultos.checked && rbSustitucion.checked)) || (rbFondos.checked && rbGestionFondosSustitucion.checked) || ((rbFondos.checked && rbGestionFondosMovimientoFondos.checked) && (rbEntreSectores.checked || rbEntrePlantas.checked) && (chkSolicitacionFondos.checked)) || ((((rbRemesas.checked && rbReenvios.checked) && (rbGestionRemesasReenviosEntreSectores.checked || rbGestionRemesasReenviosEntrePlantas.checked)) || ((rbBultos.checked && rbReenvios.checked) && (rbGestionBultosReenviosEntreSectores.checked || rbGestionBultosReenviosEntrePlantas.checked)) || ((rbFondos.checked && rbGestionFondosMovimientoFondos.checked) && (rbEntreSectores.checked || rbEntrePlantas.checked))) && (chkContestarSolicitacionFondos.checked)))) {
                dvIacGrupo.style.display = 'block';
            } else {
                dvIacGrupo.style.display = 'none';
            }

        }

        /* Evento do botão cancelar */
        function cancelar() {
            if (confirm('Cancelar?')) {
                window.location.assign("ConfiguracionFormularios.aspx");
            }
        }

        /* Funções para controlar os Paineis de Origen e Destino - TipoSectores */
        function seleccionarTodosItens(a, b) {
            inputs = a.getElementsByTagName('input');
            for (x = 0; x < inputs.length; x++) {
                if (inputs[x].type == 'checkbox') {
                    inputs[x].checked = b;
                }
            }
        }
        function seleccionarTodos(a, b) {
            if (a == 'TipoSectorOrigen') {
                seleccionarTodosItens(origen, b);
            } else if (a == 'TipoSectorDestino') {
                seleccionarTodosItens(destino, b);
            }
        }
        function cambiarLegenda(a, b) {
            var leg = document.getElementById('leg' + a);
            if (b == 0) {
                leg.innerText = '';
            } else if (b == 1) {
                leg.innerText = legAgregarTodos;
            } else if (b == 2) {
                leg.innerText = legDesAgregarTodos;
            }
        }
        function HomeEnd(a, b) {
            if (event.which || event.keyCode) {
                if ((event.which == 35) || (event.keyCode == 35)) {
                    inputs = a.getElementsByTagName('input');
                    for (x = 0; x < inputs.length; x++) {
                        if (inputs[x].type == 'checkbox') {
                            inputs[x].focus();
                        }
                    }
                }
                if ((event.which == 36) || (event.keyCode == 36)) {
                    document.getElementById(a.id + '_0').focus();
                }
            }
            return false;
        }
        function atribuirFuncion(a) {
            inputs = a.getElementsByTagName('input');
            for (x = 0; x < inputs.length; x++) {
                if (inputs[x].type == 'checkbox') {
                    inputs[x].onkeydown = function () { HomeEnd(a, inputs.length - 1); };
                }
            }
        }
        atribuirFuncion(origen);
        atribuirFuncion(destino);

        /* Upload de imagem */
        fupImagem.onchange = function () {

            if (checkFileExtension(this.value)) {
                txtUploadFile.value = this.value;
                var file = document.querySelector('input[type=file]').files[0]; //sames as here
                var reader = new FileReader();

                reader.onloadend = function () {
                    imgPhoto.src = reader.result;
                }

                if (file) {
                    reader.readAsDataURL(file); //reads the data as a URL
                    dvImagem.style.display = "block";
                    dvImagemError.style.display = "none";
                } else {
                    imgPhoto.src = "";
                    dvImagem.style.display = "none";
                }
            } else {
                txtUploadFile.value = '';
                imgPhoto.src = "";
                dvImagem.style.display = "none";
                dvImagemError.style.display = "block";
            }

        };
        function checkFileExtension(elem) {

            if (elem.indexOf('.') == -1)
                return false;

            var validExtensions = new Array();
            var ext = elem.substring(elem.lastIndexOf('.') + 1).toLowerCase();

            validExtensions[0] = 'jpg';
            validExtensions[1] = 'jpeg';
            validExtensions[2] = 'bmp';
            validExtensions[3] = 'png';
            validExtensions[4] = 'gif';
            validExtensions[5] = 'ico';

            for (var i = 0; i < validExtensions.length; i++) {
                if (ext == validExtensions[i])
                    return true;
            }

            elem.value = "";
            dvImagemError.innerText = String.format(Extension_Invalido, ext.toUpperCase());
            return false;
        }

        /* Controla Copias de Impressão */
        function adicionarCopia() {
            var valor = txtImprimeCopia.value.replace(';', '');
            if (validarValor(valor)) {
                lstItensAdicionados.innerHTML += '<div id="item_' + valor + '">' + valor + '<button type="button" value="X" onclick="javascript:eliminarCopia(' + "'" + valor + "'" + ');" /></div>';
                hidItensAdicionados.value += valor + ';';
            }
            txtImprimeCopia.value = '';
            txtImprimeCopia.focus();
            exibirCopias();
        }
        function eliminarCopia(a) {
            var olddiv = document.getElementById('item_' + a);
            lstItensAdicionados.removeChild(olddiv);
            hidItensAdicionados.value = hidItensAdicionados.value.replace(a + ';', '');
            txtImprimeCopia.value = '';
            txtImprimeCopia.focus();
            exibirCopias();
        }
        function validarValor(a) {
            if (hidItensAdicionados.value.indexOf(a + ';') > -1) {
                return false
            }
            return true
        }
        function exibirCopias() {
            if (chkImprime.checked == true) {
                dvCopias.style.display = 'block';
            } else {
                dvCopias.style.display = 'none';
            }
            if (hidItensAdicionados.value != '') {
                dvInfValores.style.display = 'block';
            } else {
                dvInfValores.style.display = 'none';
            }
        }

        /* Configura paineis ao iniciar a página */
        configurarPanel();

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
</asp:Content>
