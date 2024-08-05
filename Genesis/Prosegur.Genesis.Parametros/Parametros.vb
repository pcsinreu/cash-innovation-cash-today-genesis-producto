Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ValorPosible

Public Class Parametros

    Private Shared _InformacionUsuario As ContractoServicio.Login.EjecutarLogin.Usuario
    Public Shared Property InformacionUsuario As ContractoServicio.Login.EjecutarLogin.Usuario
        Get
            Return _InformacionUsuario
        End Get
        Set(value As ContractoServicio.Login.EjecutarLogin.Usuario)
            _InformacionUsuario = value
        End Set
    End Property

    Private Shared _InformacionUsuarioCompleta As ContractoServicio.Login.EjecutarLogin.Usuario
    Public Shared Property InformacionUsuarioCompleta As ContractoServicio.Login.EjecutarLogin.Usuario
        Get
            Return _InformacionUsuarioCompleta
        End Get
        Set(value As ContractoServicio.Login.EjecutarLogin.Usuario)
            _InformacionUsuarioCompleta = value
        End Set
    End Property

    Public Shared Property Aplicaciones As ContractoServicio.Login.EjecutarLogin.AplicacionVersionColeccion = Nothing
    Public Shared Property AgenteComunicacion As Prosegur.Genesis.Comunicacion.Agente = Nothing

    Public Shared Property URLServicio As String = String.Empty
    Public Shared Property URLServicioTokenLogin As String = String.Empty
    Public Shared Property URLServicioTokenSenha As String = String.Empty
    Public Shared Property CodigoAplicacion As String = String.Empty
    Public Shared Property CodigoPuesto As String = String.Empty
    Public Shared Property PathXmlError As String = String.Empty
    Public Shared Property forzarServicio As Boolean = False
    Public Shared Property IntegracionNuevoSaldosUrlWeb As String = String.Empty
    Public Shared Property CaminhoArquivoLogATM As String = String.Empty

    Public Shared Function DescripcionSeleccionado(valoresPosibles As List(Of ValorPosible)) As String

        If valoresPosibles IsNot Nothing Then

            For Each vp As ValorPosible In valoresPosibles
                If vp.esValorDefecto Then
                    Return vp.Descripcion
                End If
            Next

        End If

        Return String.Empty

    End Function

    Public Shared Function ValorSeleccionado(valoresPosibles As List(Of ValorPosible)) As String

        If valoresPosibles IsNot Nothing Then

            For Each vp As ValorPosible In valoresPosibles
                If vp.esValorDefecto Then
                    Return vp.Codigo
                End If
            Next

        End If

        Return String.Empty

    End Function

End Class
