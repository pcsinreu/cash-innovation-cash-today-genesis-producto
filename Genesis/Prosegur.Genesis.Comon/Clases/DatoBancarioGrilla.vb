Namespace Clases
    Public Class DatoBancarioGrilla
        Inherits BindableBase

#Region "fields"
        Private _oidDatoBancario As String
        Private _cliente As String
        Private _subcliente As String
        Private _puntoServicio As String
        Private _divisa As String
        Private _loginUsuario As String
        Private _detalle As List(Of DatoBancarioGrillaDetalle)
        Private _cantAprobados As Integer
        Private _cantPendientes As Integer
        Private _cantRechazados As Integer


#End Region

#Region "properties"
        Public Property OidDatoBancario As String
            Get
                Return _oidDatoBancario
            End Get
            Set(value As String)
                SetProperty(_oidDatoBancario, value, "OidDatoBancario")
            End Set
        End Property
        Public Property Cliente As String
            Get
                Return _cliente
            End Get
            Set(value As String)
                SetProperty(_cliente, value, "Cliente")
            End Set
        End Property

        Public Property SubCliente As String
            Get
                Return _subcliente
            End Get
            Set(value As String)
                SetProperty(_subcliente, value, "SubCliente")
            End Set
        End Property

        Public Property PuntoServicio As String
            Get
                Return _puntoServicio
            End Get
            Set(value As String)
                SetProperty(_puntoServicio, value, "PuntoServicio")
            End Set
        End Property

        Public Property Divisa As String
            Get
                Return _divisa
            End Get
            Set(value As String)
                SetProperty(_divisa, value, "Divisa")
            End Set
        End Property

        Public Property LoginUsuario As String
            Get
                Return _loginUsuario
            End Get
            Set(value As String)
                SetProperty(_loginUsuario, value, "LoginUsuario")
            End Set
        End Property

        Public ReadOnly Property CamposModificados As String
            Get
                Dim result = String.Empty
                For Each det In Detalle
                    result += det.CampoModificado
                    If det IsNot Detalle.Last Then
                        result += " - "
                    End If
                Next
                Return result
            End Get

        End Property

        Public ReadOnly Property NumeroCampos As Integer
            Get
                Return Detalle.Count
            End Get
        End Property
        Public Property CantAprobados As Integer
            Get
                Return _cantAprobados
            End Get
            Set(value As Integer)
                _cantAprobados = value
            End Set
        End Property
        Public Property CantPendientes As Integer
            Get
                Return _cantPendientes
            End Get
            Set(value As Integer)
                _cantPendientes = value
            End Set
        End Property
        Public Property CantRechazados As Integer
            Get
                Return _cantRechazados
            End Get
            Set(value As Integer)
                _cantRechazados = value
            End Set

        End Property
        Public ReadOnly Property Estado As String
            Get
                Return Detalle.First().Estado
            End Get
        End Property

        Public ReadOnly Property Detalle As List(Of DatoBancarioGrillaDetalle)
            Get
                If _detalle Is Nothing Then
                    _detalle = New List(Of DatoBancarioGrillaDetalle)
                End If
                Return _detalle
            End Get
        End Property
#End Region
    End Class

    Public Class DatoBancarioGrillaDetalle
        Inherits BindableBase

#Region "fields"
        Private _oidDatoBancarioCambio As String
        Private _campoModificado As String
        Private _valorActual As String
        Private _valorModificado As String
        Private _loginUsuario As String
        Private _usuarioModificacion As String
        Private _fechaCambio As DateTime
        Private _estado As String
        Private _aprobacionesNecesarias As Integer
        Private _aprobaciones As AprobacionesGrillaDetalle

#End Region

#Region "properties"
        Public Property OidDatoBancarioCambio As String
            Get
                Return _oidDatoBancarioCambio
            End Get
            Set(value As String)
                SetProperty(_oidDatoBancarioCambio, value, "OidDatoBancarioCambio")
            End Set
        End Property
        Public Property CampoModificado As String
            Get
                Return _campoModificado
            End Get
            Set(value As String)
                SetProperty(_campoModificado, value, "CampoModificado")
            End Set
        End Property

        Public Property ValorActual As String
            Get
                Return _valorActual
            End Get
            Set(value As String)
                SetProperty(_valorActual, value, "ValorActual")
            End Set
        End Property

        Public Property ValorModificado As String
            Get
                Return _valorModificado
            End Get
            Set(value As String)
                SetProperty(_valorModificado, value, "ValorModificado")
            End Set
        End Property

        Public Property LoginUsuario As String
            Get
                Return _loginUsuario
            End Get
            Set(value As String)
                SetProperty(_loginUsuario, value, "LoginUsuario")
            End Set
        End Property

        Public Property UsuarioModificacion As String
            Get
                Return _usuarioModificacion
            End Get
            Set(value As String)
                SetProperty(_usuarioModificacion, value, "UsuarioModificacion")
            End Set
        End Property

        Public Property FechaCambio As DateTime
            Get
                Return _fechaCambio
            End Get
            Set(value As DateTime)
                SetProperty(_fechaCambio, value, "FechaCambio")
            End Set
        End Property

        Public Property Estado As String
            Get
                Return _estado
            End Get
            Set(value As String)
                SetProperty(_estado, value, "Estado")
            End Set
        End Property

        Public ReadOnly Property CantidadAprobaciones As Integer
            Get
                Return Aprobaciones.CantidadAprobadores
            End Get
        End Property
        Public Property AprobacionesNecesarias As Integer
            Get
                Return _aprobacionesNecesarias
            End Get
            Set(value As Integer)
                SetProperty(_aprobacionesNecesarias, value, "AprobacionesNecesarias")
            End Set
        End Property

        Public ReadOnly Property Aprobadores As String
            Get
                Dim resultado As String = String.Empty
                For Each usuario In Aprobaciones.UsuariosAprobadores
                    If Not String.IsNullOrWhiteSpace(resultado) Then
                        resultado += vbNewLine
                    End If
                    resultado += String.Format("- {0}{1}{2}", usuario.Usuario, vbNewLine, usuario.FechaAprobacion)
                Next
                Return resultado
            End Get
        End Property

        Public Property Aprobaciones As AprobacionesGrillaDetalle
            Get
                If _aprobaciones Is Nothing Then
                    _aprobaciones = New AprobacionesGrillaDetalle
                End If
                Return _aprobaciones
            End Get
            Set(value As AprobacionesGrillaDetalle)
                SetProperty(_aprobaciones, value, "Aprobaciones")
            End Set
        End Property

#End Region

    End Class

    Public Class AprobacionesGrillaDetalle
        Inherits BindableBase

#Region "fields"

        Private _usuariosAprobadores As List(Of DatoBancarioAprobador)
        Private _aprobacionesNecesarias As Integer

#End Region

#Region "properties"

        Public ReadOnly Property UsuariosAprobadores As List(Of DatoBancarioAprobador)
            Get
                If _usuariosAprobadores Is Nothing Then
                    _usuariosAprobadores = New List(Of DatoBancarioAprobador)
                End If
                Return _usuariosAprobadores
            End Get

        End Property

        Public ReadOnly Property CantidadAprobadores As Integer
            Get
                Return UsuariosAprobadores.Count
            End Get

        End Property

        Public Property AprobacionesNecesarias As Integer
            Get
                Return _aprobacionesNecesarias
            End Get
            Set(value As Integer)
                SetProperty(_aprobacionesNecesarias, value, "AprobacionesNecesarias")
            End Set
        End Property


#End Region

    End Class
    Public Class DatoBancarioAprobador
        Inherits BindableBase
#Region "fields"

        Private _usuario As String
        Private _fechaAprobacion As DateTime
        Private _login As String

#End Region

#Region "properties"

        Public Property Login As String
            Get
                Return _login
            End Get
            Set(value As String)
                SetProperty(_login, value, "Login")
            End Set
        End Property
        Public Property Usuario As String
            Get
                Return _usuario
            End Get
            Set(value As String)
                SetProperty(_usuario, value, "Usuario")
            End Set
        End Property

        Public Property FechaAprobacion As DateTime
            Get
                Return _fechaAprobacion
            End Get
            Set(value As DateTime)
                SetProperty(_fechaAprobacion, value, "FechaAprobacion")
            End Set
        End Property

#End Region
    End Class


End Namespace