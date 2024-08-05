Imports System.Xml.Serialization
Imports System.Xml

Namespace Divisa.VerificarDescripcionDivisa

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 27/01/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarDescripcionDivisa")> _
    <XmlRoot(Namespace:="urn:VerificarDescripcionDivisa")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _DescripcionDivisa As String

#End Region

#Region "[Propriedades]"

        Public Property DescripcionDivisa() As String
            Get
                Return _DescripcionDivisa
            End Get
            Set(value As String)
                _DescripcionDivisa = value
            End Set
        End Property

#End Region

    End Class

End Namespace