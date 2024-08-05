
Namespace Clases.Legado


    Namespace Constantes
        Public Class DireccionServicioLegado
            Public Const CONST_DOCUMENTO_SERVICE As String = "Documento/DocumentoService"
            Public Const CONST_RUTA_SERVICE As String = "Ruta/RutaService"
            Public Const CONST_PROGRAMACIONSEVICIO_SERVICE As String = "ProgramacionServicio/ProgramacionServicioService"
            Public Const CONST_OT_SERVICE As String = "Ot/OtService"
            Public Const CONST_MODULO_SERVICE As String = "Modulo/ModuloService"
            Public Const CONST_SERVICIO_SERVICE As String = "Servicio/ServicioService"
        End Class
    End Namespace

    Public Class DireccionServicioLegado

        Private Shared _documento_service As String
        Public Shared Property Documento_Service As String
            Get
                If String.IsNullOrEmpty(_documento_service) Then
                    Return Constantes.DireccionServicioLegado.CONST_DOCUMENTO_SERVICE
                End If
                Return _documento_service
            End Get
            Set(value As String)
                _documento_service = value
            End Set
        End Property

        Private Shared _ruta_service As String
        Public Shared Property Ruta_Service As String
            Get
                If String.IsNullOrEmpty(_ruta_service) Then
                    Return Constantes.DireccionServicioLegado.CONST_RUTA_SERVICE
                End If
                Return _ruta_service
            End Get
            Set(value As String)
                _ruta_service = value
            End Set
        End Property

        Private Shared _programacionServicio_service As String
        Public Shared Property ProgramacionServicio_Service As String
            Get
                If String.IsNullOrEmpty(_programacionServicio_service) Then
                    Return Constantes.DireccionServicioLegado.CONST_PROGRAMACIONSEVICIO_SERVICE
                End If
                Return _programacionServicio_service
            End Get
            Set(value As String)
                _programacionServicio_service = value
            End Set
        End Property

        Private Shared _ot_service As String
        Public Shared Property Ot_Service As String
            Get
                If String.IsNullOrEmpty(_ot_service) Then
                    Return Constantes.DireccionServicioLegado.CONST_OT_SERVICE
                End If
                Return _ot_service
            End Get
            Set(value As String)
                _ot_service = value
            End Set
        End Property

        Private Shared _modulo_service As String
        Public Shared Property Modulo_Service As String
            Get
                If String.IsNullOrEmpty(_modulo_service) Then
                    Return Constantes.DireccionServicioLegado.CONST_MODULO_SERVICE
                End If
                Return _modulo_service
            End Get
            Set(value As String)
                _modulo_service = value
            End Set
        End Property

        Private Shared _servicio_service As String
        Public Shared Property Servicio_Service As String
            Get
                If String.IsNullOrEmpty(_servicio_service) Then
                    Return Constantes.DireccionServicioLegado.CONST_SERVICIO_SERVICE
                End If
                Return _servicio_service
            End Get
            Set(value As String)
                _servicio_service = value
            End Set
        End Property

    End Class

    Public Enum EnumDireccionSerivicioLegado
        Documento
        Ruta
        ProgramacionServicio
        Ot
        Modulo
        Servicio
    End Enum

End Namespace