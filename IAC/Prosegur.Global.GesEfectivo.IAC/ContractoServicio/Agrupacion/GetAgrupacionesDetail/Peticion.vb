Imports System.Xml.Serialization
Imports System.Xml

Namespace Agrupacion.GetAgrupacionesDetail

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetAgrupacionesDetail")> _
    <XmlRoot(Namespace:="urn:GetAgrupacionesDetail")> _
    <Serializable()> _
    Public Class Peticion

        Private _CodigoAgrupacion As List(Of String)

        Public Property CodigoAgrupacion() As List(Of String)
            Get
                Return _CodigoAgrupacion
            End Get
            Set(value As List(Of String))
                _CodigoAgrupacion = value
            End Set
        End Property

    End Class

End Namespace