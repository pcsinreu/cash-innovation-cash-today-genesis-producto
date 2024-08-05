<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="Certificados.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.Certificados" %>
<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



 <fieldset class="ui-fieldset ui-fieldset-toggleable" style="margin:0 5px 0 10px;">
                    <legend class="ui-fieldset-legend ui-corner-all ui-state-default"><span class="ui-fieldset-toggler ui-icon ui-icon-minusthick">
                    </span>
                        <asp:Label ID="Label1" runat="server" Text="Datos de Entrada" />
                    </legend>
                    <div class="ui-fieldset-content">

                     <fieldset class="ui-fieldset ui-fieldset-toggleable certificados">
            <legend class="ui-fieldset-legend ui-corner-all ui-state-default">
                <span class="ui-fieldset-toggler ui-icon ui-icon-minusthick"></span>
                <asp:Label ID="Label2" runat="server" Text="Delegaciones" />
            </legend>
            <div class="ui-fieldset-content">
                 <table class="ui-picklist" cellpadding="0" cellspacing="0" border="0" style="margin:10px 10px 20px">
            <tbody>
                <tr valign="top">
                    <td>
                        <ul class="ui-widget-content ui-picklist-list ui-picklist-source ui-corner-all ui-sortable">
                            <li class="ui-picklist-item ui-corner-all"><span>Nazca</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>La Plata</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>Mar del Plata</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>Mendoza</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>Bariloche</span></li>
                        </ul>
                        <select id="formGrid:permiso_source" name="formGrid:permiso_source" multiple="true"
                            class="ui-helper-hidden">
                            <option value="C6ADC1496A901187E040007F01005A3F" selected="selected">C6ADC1496A901187E040007F01005A3F</option>
                        </select>
                    </td>
                    <td>
                        <ul class="btnPick">
                        <li><button type="button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-icon-only ui-picklist-button-add"
                            title="Agregar">
                            <span class="ui-button-icon-left ui-icon ui-icon ui-icon-arrow-1-e"></span><span
                                class="ui-button-text">ui-button</span>
                        </button></li>
                        <li><button type="button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-icon-only ui-picklist-button-add-all"
                            title="Agregar Todos">
                            <span class="ui-button-icon-left ui-icon ui-icon ui-icon-arrowstop-1-e"></span><span
                                class="ui-button-text">ui-button</span>
                        </button></li>
                        <li> <button type="button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-icon-only ui-picklist-button-remove"
                            title="Eliminar">
                            <span class="ui-button-icon-left ui-icon ui-icon ui-icon-arrow-1-w"></span><span
                                class="ui-button-text">ui-button</span></button></li>
                        <li>
                        
                        <button type="button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-icon-only ui-picklist-button-remove-all"
                            title="Eliminar Todos">
                            <span class="ui-button-icon-left ui-icon ui-icon ui-icon-arrowstop-1-w"></span><span
                                class="ui-button-text">ui-button</span></button></li>
                        
                        </ul>
                        
                    </td>
                    <td>
                       <ul class="ui-widget-content ui-picklist-list ui-picklist-source ui-corner-all ui-sortable">
                            <li class="ui-picklist-item ui-corner-all"><span>Nazca</span></li>
                        </ul>
                        <select id="formGrid:permiso_target" name="formGrid:permiso_target" multiple="true"
                            class="ui-helper-hidden">
                        </select>
                    </td>
                </tr>
            </tbody>
        </table>
            </div>
        </fieldset>

         <fieldset class="ui-fieldset ui-fieldset-toggleable certificados">
            <legend class="ui-fieldset-legend ui-corner-all ui-state-default">
                <span class="ui-fieldset-toggler ui-icon ui-icon-minusthick"></span>
                <asp:Label ID="Label7" runat="server" Text="Canales" />
            </legend>
            <div class="ui-fieldset-content">
                 <table class="ui-picklist" cellpadding="0" cellspacing="0" border="0" style="margin:10px 10px 20px">
            <tbody>
                <tr valign="top">
                    <td>
                        <ul class="ui-widget-content ui-picklist-list ui-picklist-source ui-corner-all ui-sortable">
                            <li class="ui-picklist-item ui-corner-all"><span>0-Saldos</span></li>
                           
                        </ul>
                        <select id="Select3" name="formGrid:permiso_source" multiple="true"
                            class="ui-helper-hidden">
                            <option value="C6ADC1496A901187E040007F01005A3F" selected="selected">C6ADC1496A901187E040007F01005A3F</option>
                        </select>
                    </td>
                    <td>
                        <ul class="btnPick">
                        <li><button type="button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-icon-only ui-picklist-button-add"
                            title="Agregar">
                            <span class="ui-button-icon-left ui-icon ui-icon ui-icon-arrow-1-e"></span><span
                                class="ui-button-text">ui-button</span>
                        </button></li>
                        <li><button type="button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-icon-only ui-picklist-button-add-all"
                            title="Agregar Todos">
                            <span class="ui-button-icon-left ui-icon ui-icon ui-icon-arrowstop-1-e"></span><span
                                class="ui-button-text">ui-button</span>
                        </button></li>
                        <li> <button type="button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-icon-only ui-picklist-button-remove"
                            title="Eliminar">
                            <span class="ui-button-icon-left ui-icon ui-icon ui-icon-arrow-1-w"></span><span
                                class="ui-button-text">ui-button</span></button></li>
                        <li>
                        
                        <button type="button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-icon-only ui-picklist-button-remove-all"
                            title="Eliminar Todos">
                            <span class="ui-button-icon-left ui-icon ui-icon ui-icon-arrowstop-1-w"></span><span
                                class="ui-button-text">ui-button</span></button></li>
                        
                        </ul>
                        
                    </td>
                    <td>
                       <ul class="ui-widget-content ui-picklist-list ui-picklist-source ui-corner-all ui-sortable">
                            <li class="ui-picklist-item ui-corner-all"><span>1-Cambios</span></li>
                        </ul>
                        <select id="Select4" name="formGrid:permiso_target" multiple="true"
                            class="ui-helper-hidden">
                        </select>
                    </td>
                </tr>
            </tbody>
        </table>
            </div>
        </fieldset>

        <fieldset class="ui-fieldset ui-fieldset-toggleable certificados">
            <legend class="ui-fieldset-legend ui-corner-all ui-state-default">
                <span class="ui-fieldset-toggler ui-icon ui-icon-minusthick"></span>
                <asp:Label ID="Label3" runat="server" Text="Sectores" />
            </legend>
            <div class="ui-fieldset-content">
                 <table class="ui-picklist" cellpadding="0" cellspacing="0" border="0" style="margin:10px 10px 20px">
            <tbody>
                <tr valign="top">
                    <td>
                        <ul class="ui-widget-content ui-picklist-list ui-picklist-source ui-corner-all ui-sortable">
                            <li class="ui-picklist-item ui-corner-all"><span>Nazca-Recuento Tickets (*)</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>Nazca-Armado de Salidas (*)</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>Nazca-Recuento Mecanizado(*)</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>La Plata-Tesoro Bultos (*)</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>La Plata-Tesoreria Saldos (*)</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>La Plata-Caja Externa DIURNA (*)</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>Atl-Mar del Plata-Caja  Monedas(*)</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>Atl-Mar del Plata-Tesoro Bultos (*)</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>Atl-Mar del Plata-Recuento ATM (*)</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>Cuy-Mendoza-Tesoro Bultos (*)</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>Cuy-Mendoza-Caja ATM(*)</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>Cuy-Mendoza-Tesoreria Saldos(*)</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>Cuy-Mendoza-Caja MONEDAS(*)</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>Sur-Bariloche-Tesoreria Saldos (*)</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>Sur-Bariloche-Caja Externa Saldos (*)</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>Sur-Bariloche-Tesoro Bultos (*)</span></li>
                        </ul>
                        <select id="Select1" name="formGrid:permiso_source" multiple="true"
                            class="ui-helper-hidden">
                            <option value="C6ADC1496A901187E040007F01005A3F" selected="selected">C6ADC1496A901187E040007F01005A3F</option>
                        </select>
                    </td>
                    <td>
                        <ul class="btnPick">
                        <li><button type="button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-icon-only ui-picklist-button-add"
                            title="Agregar">
                            <span class="ui-button-icon-left ui-icon ui-icon ui-icon-arrow-1-e"></span><span
                                class="ui-button-text">ui-button</span>
                        </button></li>
                        <li><button type="button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-icon-only ui-picklist-button-add-all"
                            title="Agregar Todos">
                            <span class="ui-button-icon-left ui-icon ui-icon ui-icon-arrowstop-1-e"></span><span
                                class="ui-button-text">ui-button</span>
                        </button></li>
                        <li> <button type="button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-icon-only ui-picklist-button-remove"
                            title="Eliminar">
                            <span class="ui-button-icon-left ui-icon ui-icon ui-icon-arrow-1-w"></span><span
                                class="ui-button-text">ui-button</span></button></li>
                        <li>
                        
                        <button type="button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-icon-only ui-picklist-button-remove-all"
                            title="Eliminar Todos">
                            <span class="ui-button-icon-left ui-icon ui-icon ui-icon-arrowstop-1-w"></span><span
                                class="ui-button-text">ui-button</span></button></li>
                        
                        </ul>
                        
                    </td>
                    <td>
                       <ul class="ui-widget-content ui-picklist-list ui-picklist-source ui-corner-all ui-sortable">
                            <li class="ui-picklist-item ui-corner-all"><span>Cuy-Mendoza-Tesoro Bultos (*)</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>Cuy-Mendoza-Caja ATM(*)</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>Cuy-Mendoza-Tesoreria Saldos(*)</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>Cuy-Mendoza-Caja MONEDAS(*)</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>Sur-Bariloche-Tesoreria Saldos (*)</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>Sur-Bariloche-Caja Externa Saldos (*)</span></li>
                            <li class="ui-picklist-item ui-corner-all"><span>Sur-Bariloche-Tesoro Bultos (*)</span></li>
                        </ul>
                        <select id="Select2" name="formGrid:permiso_target" multiple="true"
                            class="ui-helper-hidden">
                        </select>
                    </td>
                </tr>
            </tbody>
        </table>
            </div>
        </fieldset>

                    </div>
                </fieldset>







           <fieldset class="ui-fieldset ui-fieldset-toggleable certificados" style="margin: 10px 5px 0 10px">
            <legend class="ui-fieldset-legend ui-corner-all ui-state-default">
                <span class="ui-fieldset-toggler ui-icon ui-icon-minusthick"></span>
                <asp:Label ID="Label4" runat="server" Text="Datos de Salida" />
            </legend>
            <div class="ui-fieldset-content">
                <fieldset class="ui-fieldset ui-fieldset-toggleable certificados">
                    <legend class="ui-fieldset-legend ui-corner-all ui-state-default"><span class="ui-fieldset-toggler ui-icon ui-icon-minusthick">
                    </span>
                        <asp:Label ID="Label5" runat="server" Text="Ultima Ejecución para este Cliente" />
                    </legend>
                    <div class="ui-fieldset-content">
                    </div>
                </fieldset>


                <fieldset class="ui-fieldset ui-fieldset-toggleable certificados">
                    <legend class="ui-fieldset-legend ui-corner-all ui-state-default"><span class="ui-fieldset-toggler ui-icon ui-icon-minusthick">
                    </span>
                        <asp:Label ID="Label6" runat="server" Text="Identificador Generado" />
                    </legend>
                    <div class="ui-fieldset-content">
                    </div>
                </fieldset>
            </div>
        </fieldset>



        <ul class="certificados-btns">
            <li><asp:Button ID="Button1" SkinID="filter-button" runat="server" Text="Consulta Ejecuciones Anteriores" /></li>
            <li><asp:Button ID="Button2" SkinID="filter-button" runat="server" Text="Consulta Configuración Saldo" /></li>
            <li><asp:Button ID="Button3" SkinID="filter-button" runat="server" Text="Ejecutar Processo" /></li>
        </ul>
</asp:Content>
