Imports System.Xml.Serialization
Imports System.Xml

Namespace GuardarDatosDocumento

    <XmlType(Namespace:="urn:GuardarDatosDocumento")> _
    <XmlRoot(Namespace:="urn:GuardarDatosDocumento")> _
    <Serializable()> _
    Public Class Peticion

        Private _Accion As Enumeradores.eAccion
        Private _Usuario As New Usuario
        Private _Documento As New Documento

        Public Property Accion() As Enumeradores.eAccion
            Get
                Return _Accion
            End Get
            Set(value As Enumeradores.eAccion)
                _Accion = value
            End Set
        End Property

        Public Property Usuario() As Usuario
            Get
                Return _Usuario
            End Get
            Set(value As Usuario)
                _Usuario = value
            End Set
        End Property

        Public Property Documento() As Documento
            Get
                Return _Documento
            End Get
            Set(value As Documento)
                _Documento = value
            End Set
        End Property

    End Class

End Namespace