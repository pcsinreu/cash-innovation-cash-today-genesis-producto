Imports Parametros = Prosegur.Genesis.Web.Login.Parametros

Public Class ucReloj
    Inherits System.Web.UI.UserControl

    Public Property CodigoDelegacion() As String
    Public Property HabilitarHistorico() As Boolean
    Public Property EsModal() As Boolean
    Public Property Historico() As List(Of KeyValuePair(Of String, String))

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender


        'If Base.InformacionUsuario.SectorSeleccionado.Delegacion IsNot Nothing AndAlso _
        '    Not String.IsNullOrEmpty(Base.InformacionUsuario.SectorSeleccionado.Delegacion.CodigoPais) Then
        '    lblPaisDelegacion.Text = Genesis.LogicaNegocio.Genesis.Pais.ObtenerNombrePais(Base.InformacionUsuario.SectorSeleccionado.Delegacion.CodigoPais)
        'End If

        'If Base.InformacionUsuario.SectorSeleccionado.Delegacion IsNot Nothing AndAlso Not String.IsNullOrEmpty(Base.InformacionUsuario.SectorSeleccionado.Delegacion.Descripcion) Then
        '    If Not String.IsNullOrEmpty(lblPaisDelegacion.Text) Then
        '        lblPaisDelegacion.Text &= ", "
        '    End If
        '    lblPaisDelegacion.Text &= Base.InformacionUsuario.SectorSeleccionado.Delegacion.Descripcion

        '    Dim fechaHora As DateTime = Now

        '    Try
        '        fechaHora = Genesis.LogicaNegocio.Util.RecuperarFechaHoraServidorGMT(Base.InformacionUsuario.SectorSeleccionado.Delegacion.Codigo)
        '    Catch ex As Exception
        '        Genesis.LogicaNegocio.Genesis.Log.GuardarLogExecucao(ex.Message, Parametros.Permisos.Usuario.Login, Base.InformacionUsuario.SectorSeleccionado.Delegacion.Codigo, Genesis.Comon.Enumeradores.Aplicacion.GenesisNuevoSaldos)
        '    End Try

        '    litReloj.Text = "<script> var _fechaGMTDelegacion = new Date('" & fechaHora.ToString("yyyy-MM-dd HH:mm:ss") & "'); </script>"

        'End If


        Dim fechaHora As DateTime = Now

        Try
            'fechaHora = Genesis.LogicaNegocio.Util.RecuperarFechaHoraServidorGMT(Base.InformacionUsuario.SectorSeleccionado.Delegacion.Codigo)

            fechaHora = DateTime.Now.ToString
        Catch ex As Exception
            ' Genesis.LogicaNegocio.Genesis.Log.GuardarLogExecucao(ex.Message, Parametros.Permisos.Usuario.Login, Base.InformacionUsuario.SectorSeleccionado.Delegacion.Codigo, Genesis.Comon.Enumeradores.Aplicacion.GenesisNuevoSaldos)
        End Try

        litReloj.Text = "<script> var _fechaGMTDelegacion = new Date('" & fechaHora.ToString("yyyy-MM-dd HH:mm:ss") & "');</script>"

        '  End If




    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

End Class