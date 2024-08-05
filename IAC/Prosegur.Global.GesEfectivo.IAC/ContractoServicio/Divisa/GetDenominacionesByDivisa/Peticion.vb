Imports System.Xml.Serialization
Imports System.Xml

Namespace Divisa.GetDenominacionesByDivisa

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 27/01/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetDenominacionesByDivisa")> _
    <XmlRoot(Namespace:="urn:GetDenominacionesByDivisa")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _CodigoIso As List(Of String)

#End Region

#Region "[Propriedades]"

        Public Property CodigoIso() As List(Of String)
            Get
                Return _CodigoIso
            End Get
            Set(value As List(Of String))
                _CodigoIso = value
            End Set
        End Property

#End Region

    End Class

End Namespace