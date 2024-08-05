Imports System.Xml.Serialization

Namespace Canal.GetSubCanalesByCertificado
    <Serializable()> _
    Public Class SubCanal

        Private _oidSubCanal As String
        Private _codigoSubCanal As String
        Private _descripcion As String

        Public Property OidSubCanal() As String
            Get
                Return _oidSubCanal
            End Get
            Set(value As String)
                _oidSubCanal = value
            End Set
        End Property

        Public Property CodigoSubCanal() As String
            Get
                Return _codigoSubCanal
            End Get
            Set(value As String)
                _codigoSubCanal = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
            End Set
        End Property
    End Class
End Namespace
