Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboTiposPuntoServicio

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboTiposPuntoServicio")> _
    <XmlRoot(Namespace:="urn:GetComboTiposPuntoServicio")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _TiposPuntoServicio As TipoPuntoServicioColeccion

#End Region

#Region "[Propriedades]"

        Public Property TiposPuntoServicio() As TipoPuntoServicioColeccion
            Get
                Return _TiposPuntoServicio
            End Get
            Set(value As TipoPuntoServicioColeccion)
                _TiposPuntoServicio = value
            End Set
        End Property

#End Region

    End Class

End Namespace