Imports System.Xml.Serialization
Imports System.Xml

Namespace Agrupacion.SetAgrupaciones

    ''' <summary>
    ''' Classe RespuestaAgrupacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 05/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetAgrupaciones")> _
    <XmlRoot(Namespace:="urn:SetAgrupaciones")> _
    <Serializable()> _
    Public Class RespuestaAgrupacion
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _Codigo As String
        Private _Descripcion As String

#End Region

#Region "[Propriedades]"

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
            End Set
        End Property

#End Region

    End Class

End Namespace