Public Class QSConexionABDD

    '#Region "[VARIÁVEIS]"

    '    Private _Conexion As ADODB.Connection
    '    Private _CadenaDeConexion As String

    '#End Region

    '#Region "[PROPRIEDADES]"

    '    Public Property conexion() As ADODB.Connection

    '        Get
    '            If _Conexion Is Nothing Then
    '                _Conexion = New Connection()
    '            End If
    '            conexion = _Conexion
    '        End Get
    '        Set(Value As ADODB.Connection)
    '            _Conexion = Value
    '        End Set

    '    End Property

    '    Public Property CadenaDeConexion() As String

    '        Get
    '            CadenaDeConexion = _CadenaDeConexion
    '        End Get
    '        Set(Value As String)
    '            _CadenaDeConexion = Value
    '        End Set
    '    End Property

    '#End Region

    '#Region "[MÉTODOS]"

    '    Public Function Abrir() As Short

    '        On Error GoTo Abrir_Error

    '        With Me.conexion
    '            .ConnectionString = _CadenaDeConexion
    '            .Open()
    '        End With
    'Abrir_Salida:
    '        Exit Function
    'Abrir_Error:
    '        Abrir = -1
    '        Err.Raise(Err.Number, "QSConexionABDD.Abrir", Err.Description)
    '        Resume Abrir_Salida

    '    End Function

    '    Public Function Cerrar() As Short

    '        On Error GoTo Cerrar_Error

    '        Me.conexion.Close()

    'Cerrar_Salida:
    '        Exit Function
    'Cerrar_Error:
    '        Cerrar = -1
    '        Err.Raise(Err.Number, "QSConexionABDD.Cerrar", Err.Description)
    '        Resume Cerrar_Salida

    '    End Function

    '#End Region

End Class