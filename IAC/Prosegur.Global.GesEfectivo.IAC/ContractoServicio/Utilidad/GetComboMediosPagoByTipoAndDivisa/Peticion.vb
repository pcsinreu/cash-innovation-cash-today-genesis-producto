Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboMediosPagoByTipoAndDivisa

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboMediosPagoByTipoAndDivisa")> _
    <XmlRoot(Namespace:="urn:GetComboMediosPagoByTipoAndDivisa")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _CodigoTipoMedioPago As String
        Private _CodigoIsoDivisa As String

#End Region

#Region "[Propriedades]"

        Public Property CodigoTipoMedioPago() As String
            Get
                Return _CodigoTipoMedioPago
            End Get
            Set(value As String)
                _CodigoTipoMedioPago = value
            End Set
        End Property

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