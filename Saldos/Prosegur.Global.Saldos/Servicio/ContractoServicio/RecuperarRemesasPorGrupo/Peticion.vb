Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperarRemesasPorGrupo

    <XmlType(Namespace:="urn:RecuperarRemesasPorGrupo")> _
    <XmlRoot(Namespace:="urn:RecuperarRemesasPorGrupo")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIAVEIS]"

        Private _Usuario As GuardarDatosDocumento.Usuario
        Private _Filtro As Filtro

#End Region

#Region "[PROPRIEDADES]"

        Public Property Usuario() As GuardarDatosDocumento.Usuario
            Get
                Return _Usuario
            End Get
            Set(value As GuardarDatosDocumento.Usuario)
                _Usuario = value
            End Set
        End Property
        Public Property Filtro() As Filtro
            Get
                Return _Filtro
            End Get
            Set(value As Filtro)
                _Filtro = value
            End Set
        End Property

#End Region

    End Class

End Namespace