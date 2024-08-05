Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboDivisasByTipoMedioPago

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboDivisasByTipoMedioPago")> _
    <XmlRoot(Namespace:="urn:GetComboDivisasByTipoMedioPago")> _
    <Serializable()> _
    Public Class Peticion

#Region " Variáveis "

        Private _CodigoTipoMedioPago As String

#End Region

#Region " Propriedades "

        Public Property CodigoTipoMedioPago() As String
            Get
                Return _CodigoTipoMedioPago
            End Get
            Set(value As String)
                _CodigoTipoMedioPago = value
            End Set
        End Property

#End Region

    End Class

End Namespace