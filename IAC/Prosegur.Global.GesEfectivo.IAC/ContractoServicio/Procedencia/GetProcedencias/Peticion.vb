Imports System.Xml.Serialization
Imports System.Xml


Namespace Procedencia.GetProcedencias

    <XmlType(Namespace:="urn:GetProcedencias")> _
    <XmlRoot(Namespace:="urn:GetProcedencias")> _
    <Serializable()> _
    Public Class Peticion

        Private _oidProcedencia As String
        Private _codigoTipoSubCliente As String
        Private _codigoTipoPuntoServicio As String
        Private _codigoTipoProcedencia As String
        Private _activo As Nullable(Of Boolean)

        Public Property OidProcedencia() As String
            Get
                Return _oidProcedencia
            End Get
            Set(value As String)
                _oidProcedencia = value
            End Set
        End Property

        Public Property CodigoTipoSubCliente() As String
            Get
                Return _codigoTipoSubCliente
            End Get
            Set(value As String)
                _codigoTipoSubCliente = value
            End Set
        End Property

        Public Property CodigoTipoPuntoServicio() As String
            Get
                Return _codigoTipoPuntoServicio
            End Get
            Set(value As String)
                _codigoTipoPuntoServicio = value
            End Set
        End Property

        Public Property CodigoTipoProcedencia() As String
            Get
                Return _codigoTipoProcedencia
            End Get
            Set(value As String)
                _codigoTipoProcedencia = value
            End Set
        End Property

        Public Property Activo() As Nullable(Of Boolean)
            Get
                Return _activo
            End Get
            Set(value As Nullable(Of Boolean))
                _activo = value
            End Set
        End Property


    End Class

End Namespace