Imports System.Xml.Serialization
Imports System.Xml

Namespace Agrupacion.SetAgrupaciones

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetAgrupaciones")> _
    <XmlRoot(Namespace:="urn:SetAgrupaciones")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _Agrupaciones As AgrupacionColeccion
        Private _CodigoUsuario As String

#End Region

#Region "[Propriedades]"

        Public Property Agrupaciones() As AgrupacionColeccion
            Get
                Return _Agrupaciones
            End Get
            Set(value As AgrupacionColeccion)
                _Agrupaciones = value
            End Set
        End Property

        Public Property CodigoUsuario() As String
            Get
                Return _CodigoUsuario
            End Get
            Set(value As String)
                _CodigoUsuario = value
            End Set
        End Property

#End Region

    End Class

End Namespace