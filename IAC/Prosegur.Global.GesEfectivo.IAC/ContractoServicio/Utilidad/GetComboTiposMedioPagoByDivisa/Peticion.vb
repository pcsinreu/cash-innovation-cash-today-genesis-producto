Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboTiposMedioPagoByDivisa

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboTiposMedioPagoByDivisa")> _
    <XmlRoot(Namespace:="urn:GetComboTiposMedioPagoByDivisa")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _CodigoIsoDivisa As String

#End Region

#Region "[Propriedades]"

        Public Property CodigoIsoDivisa() As String
            Get
                Return _CodigoIsoDivisa
            End Get
            Set(value As String)
                _CodigoIsoDivisa = value
            End Set
        End Property

#End Region

    End Class

End Namespace