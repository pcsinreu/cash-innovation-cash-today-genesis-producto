Imports Prosegur.Genesis
Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Genesis.ContractoServicio
Imports Newtonsoft.Json

Public Class OrdenesDeServicioNotificacionPopup
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

            Dim OidIntegracion As String = Request.QueryString("OidIntegracion")
            If Not Page.IsPostBack AndAlso Not String.IsNullOrWhiteSpace(OidIntegracion) Then
                Dim codigoPais = String.Empty
                Dim objProxyDelegacion As New Comunicacion.ProxyWS.IAC.ProxyDelegacion
                Dim objPeticionDelegacion As New IAC.ContractoServicio.Delegacion.GetDelegacionDetail.Peticion
                Dim objRespuestaDelegacion As IAC.ContractoServicio.Delegacion.GetDelegacionDetail.Respuesta
                objPeticionDelegacion.CodigoDelegacione = Prosegur.Genesis.Web.Login.Parametros.Permisos.Usuario.Delegacion.Codigo

                objRespuestaDelegacion = objProxyDelegacion.GetDelegacioneDetail(objPeticionDelegacion)
                If objRespuestaDelegacion IsNot Nothing AndAlso objRespuestaDelegacion.Delegacion.Count > 0 Then
                    codigoPais = objRespuestaDelegacion.Delegacion(0).CodPais
                End If

                Dim objPeticion As New Contractos.Job.EnviarNotificacion.Peticion With {
                    .CodigoPais = codigoPais,
                    .Configuracion = New Contractos.Job.EnviarNotificacion.Entrada.Configuracion With {
                        .Usuario = Genesis.Web.Login.Parametros.Permisos.Usuario.Login,
                        .IdentificadorAjeno = String.Empty
                    }
                }

                Dim notificacion = New LogicaNegocio.AccionOrdenServicio().GetNotificacionesApiGlobal("", OidIntegracion, objPeticion)

                Dim objJson = JsonConvert.SerializeObject(notificacion, Formatting.Indented)
                txtNotificacion.InnerText = objJson
            End If


        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()

    End Sub

#End Region
End Class