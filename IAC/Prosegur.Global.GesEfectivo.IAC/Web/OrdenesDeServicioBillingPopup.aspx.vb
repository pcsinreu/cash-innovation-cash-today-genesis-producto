Imports Prosegur.Genesis
Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Genesis.ContractoServicio

Public Class OrdenesDeServicioBillingPopup
    Inherits Base

#Region "[OVERRIDES]"
    Protected Overrides Sub DefinirParametrosBase()
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.ORDENES_SERVICIO
        MyBase.ValidarAcao = False
        MyBase.ValidarPemissaoAD = False
    End Sub
    Protected Overrides Sub Inicializar()

        Try

            ASPxGridView.RegisterBaseScript(Page)

            Dim OidSaldoAcuerdoRef As String = Request.QueryString("OidSaldoAcuerdoRef")
            If Not Page.IsPostBack AndAlso Not String.IsNullOrWhiteSpace(OidSaldoAcuerdoRef) Then
                Dim codigoPais = String.Empty
                Dim objProxyDelegacion As New Comunicacion.ProxyWS.IAC.ProxyDelegacion
                Dim objPeticionDelegacion As New IAC.ContractoServicio.Delegacion.GetDelegacionDetail.Peticion
                Dim objRespuestaDelegacion As IAC.ContractoServicio.Delegacion.GetDelegacionDetail.Respuesta
                objPeticionDelegacion.CodigoDelegacione = Prosegur.Genesis.Web.Login.Parametros.Permisos.Usuario.Delegacion.Codigo

                objRespuestaDelegacion = objProxyDelegacion.GetDelegacioneDetail(objPeticionDelegacion)
                If objRespuestaDelegacion IsNot Nothing AndAlso objRespuestaDelegacion.Delegacion.Count > 0 Then
                    codigoPais = objRespuestaDelegacion.Delegacion(0).CodPais
                End If
                Dim UrlApi = Genesis.LogicaNegocio.Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "urlApiNotificacionOS")(0).Valores(0)
                Dim url = $"{UrlApi}/{codigoPais}/{OidSaldoAcuerdoRef}"

                ifNotificacion.Src = url
            End If


        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()

    End Sub

#End Region
End Class