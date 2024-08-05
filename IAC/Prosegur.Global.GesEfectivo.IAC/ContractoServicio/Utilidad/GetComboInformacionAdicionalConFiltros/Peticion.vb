Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboInformacionAdicionalConFiltros

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [victor.ramos] 27/05/2014 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboInformacionAdicionalConFiltros")> _
    <XmlRoot(Namespace:="urn:GetComboInformacionAdicionalConFiltros")> _
    <Serializable()> _
    Public Class Peticion

#Region " Variáveis "

        Private _BolEspecificoSaldos As Boolean

#End Region

#Region " Propriedades "

        Public Property BolEspecificoSaldos() As Boolean
            Get
                Return _BolEspecificoSaldos
            End Get
            Set(value As Boolean)
                _BolEspecificoSaldos = value
            End Set
        End Property

#End Region

    End Class

End Namespace