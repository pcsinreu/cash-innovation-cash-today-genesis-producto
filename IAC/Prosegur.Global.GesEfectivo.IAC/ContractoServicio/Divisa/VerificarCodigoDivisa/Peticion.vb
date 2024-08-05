Imports System.Xml.Serialization
Imports System.Xml

Namespace Divisa.VerificarCodigoDivisa

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 27/01/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarCodigoDivisa")> _
    <XmlRoot(Namespace:="urn:VerificarCodigoDivisa")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _CodigoIso As String

#End Region

#Region "[Propriedades]"

        Public Property CodigoIso() As String
            Get
                Return _CodigoIso
            End Get
            Set(value As String)
                _CodigoIso = value
            End Set
        End Property

#End Region

    End Class

End Namespace