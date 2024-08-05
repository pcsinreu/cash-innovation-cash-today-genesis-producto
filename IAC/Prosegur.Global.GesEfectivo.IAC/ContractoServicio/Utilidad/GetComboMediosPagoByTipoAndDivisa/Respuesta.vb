Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboMediosPagoByTipoAndDivisa

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboMediosPagoByTipoAndDivisa")> _
    <XmlRoot(Namespace:="urn:GetComboMediosPagoByTipoAndDivisa")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _MediosPago As MedioPagoColeccion

#End Region

#Region "[Propriedades]"

        Public Property MediosPago() As MedioPagoColeccion
            Get
                Return _MediosPago
            End Get
            Set(value As MedioPagoColeccion)
                _MediosPago = value
            End Set
        End Property

#End Region

    End Class

End Namespace