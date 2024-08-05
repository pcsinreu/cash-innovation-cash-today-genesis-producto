Imports System.Xml.Serialization
Imports System.Xml

Namespace Procedencia.VerificarExisteProcedencia
    <XmlType(Namespace:="urn:VerificaExisteProcedencia")> _
    <XmlRoot(Namespace:="urn:VerificaExisteProcedencia")> _
    <Serializable()> _
    Public Class Peticion

        Private _oidProcedencia As String
        Private _oidTipoSubCliente As String
        Private _oidTipoPuntoServicio As String
        Private _oidTipoProcedencia As String

        Public Property OidProcedencia() As String
            Get
                Return _oidProcedencia
            End Get
            Set(value As String)
                _oidProcedencia = value
            End Set
        End Property

        Public Property OidTipoSubCliente() As String
            Get
                Return _oidTipoSubCliente
            End Get
            Set(value As String)
                _oidTipoSubCliente = value
            End Set
        End Property

        Public Property OidTipoPuntoServicio() As String
            Get
                Return _oidTipoPuntoServicio
            End Get
            Set(value As String)
                _oidTipoPuntoServicio = value
            End Set
        End Property

        Public Property OidTipoProcedencia() As String
            Get
                Return _oidTipoProcedencia
            End Get
            Set(value As String)
                _oidTipoProcedencia = value
            End Set
        End Property

    End Class

End Namespace

