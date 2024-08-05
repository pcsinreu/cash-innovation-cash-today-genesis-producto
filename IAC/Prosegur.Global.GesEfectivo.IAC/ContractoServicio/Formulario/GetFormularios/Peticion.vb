Imports System.Xml.Serialization
Imports System.Xml

Namespace Formulario.GetFormularios

    <XmlType(Namespace:="urn:GetFormularios")>
    <XmlRoot(Namespace:="urn:GetFormularios")>
    <Serializable()>
    Public Class Peticion

#Region "[Variáveis]"

        Private _Identificador As List(Of String)
        Private _Codigo As List(Of String)
        Private _Descripcion As List(Of String)

#End Region

#Region "[Propriedades]"

        Public Property Identificador() As List(Of String)
            Get
                Return _Identificador
            End Get
            Set(value As List(Of String))
                _Identificador = value
            End Set
        End Property

        Public Property Codigo() As List(Of String)
            Get
                Return _Codigo
            End Get
            Set(value As List(Of String))
                _Codigo = value
            End Set
        End Property

        Public Property Descripcion() As List(Of String)
            Get
                Return _Descripcion
            End Get
            Set(value As List(Of String))
                _Descripcion = value
            End Set
        End Property

#End Region

    End Class

End Namespace