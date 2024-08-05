<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucBusqueda.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucBusqueda" %>

<div id="<%=Me.ID%>" <%= Me.setStyle%> data-bind="with: <%= Me.UtilitarioBusca%>" >
    <asp:HiddenField ID="guidName" runat="server" />
    <div>
        <span <%If String.IsNullOrEmpty(Me.Titulo) Then%> data-bind="text: Titulo" <%End If%>><%= Me.Titulo %></span>
        <br>
        <input type="text" id="txtBusquedaCodigo_<%= Me.TipoString%>_<%= Me.ID%>" maxlength="15" data-bind="value: Codigo" onchange="javascript: obtenerValorPorCodigo('<%= Me.TipoString%>', this, '<%= Me.ID%>', '<%= Me.IDAssociacao%>');" />
        <input type="hidden" id="txtBusquedaMulti_<%= Me.TipoString%>_<%= Me.ID%>" value="<%= Me.EsMultiString %>" />
        <input type="hidden" id="txtBusquedaSeleccionados_<%= Me.TipoString%>_<%= Me.ID%>" data-bind="value: jsonString" />
        <input type="hidden" id="hdPossuiBind_<%= Me.TipoString%>_<%= Me.ID%>" value="<%= IIf(String.IsNullOrEmpty(Me.UtilitarioBusca), 0, 1)%>" />
        <input type="hidden" id="hdAssociacion_<%= Me.ID%>" value="<%= Me.IDAssociacao%>" />
        <input type="hidden" id="hdAssociacionPadre_<%= Me.ID%>" value="<%= Me.IDAssociacaoPadre%>" />
    </div>
    <div>
        <input type="button" class="butonFormPequeno butonFormBusqueda" onclick="javascript: ExibirPopupBusquedaAvanzado('<%= Me.TipoString%>', '<%= Me.ID%>'); " />
    </div>
    <div>
        <br>
        <input type="text" id="txtBusquedaDescripcion_<%= Me.TipoString%>_<%= Me.ID%>" data-bind="value: Descripcion" maxlength="100" style="width: 215px;" onchange="javascript: obtenerValorPorDescription('<%= Me.TipoString%>', this, '<%= Me.ID%>', '<%= Me.IDAssociacao%>>');" />
    </div>
    <div id="dvBusquedaBotonLimpar_<%= Me.TipoString%>_<%= Me.ID%>">
        <input type="button" class="butonFormPequeno butonFormLimpar" onclick="javascript: limparCampo('<%= Me.TipoString%>','<%= Me.ID%>');" />
    </div>
    <div class="dvclear"></div>
    <div id="dvBusquedaValores_<%= Me.TipoString%>_<%= Me.ID%>" class="lista" style="margin: 5px 0px 5px 0px; display: none;" data-bind="style: {display: VisualizacionValoresMulti() ? 'block' : 'none'}"  >
        <select id="txtBusquedaValores_<%= Me.TipoString%>_<%= Me.ID%>" size='4' multiple="multiple" class="valores"  data-bind="options: Propriedad, optionsText: 'CodigoDescripcion', optionsValue: 'Identificador' "
            style="width: 370px !important; height: 45px !important;">
        </select>
        <div id="dvBusquedaAcciones_<%= Me.TipoString%>_<%= Me.ID%>" class="acciones" style="width: 12px;">
            <input type="button" class="butonFormPequeno butonFormLimpar" onclick="javascript: limparCampoMult('<%= Me.TipoString%>','<%=Me.ID%>');" style="margin: 2px 2px 0px 2px;" />
        </div>
    </div>
</div>

