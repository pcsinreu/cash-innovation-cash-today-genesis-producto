Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboTiposProcedencia

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 25/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboTiposProcedencia")> _
    <XmlRoot(Namespace:="urn:GetComboTiposProcedencia")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _TiposProcedencia As TipoProcedenciaColeccion

#End Region

#Region "[Propriedades]"

        Public Property TiposProcedencia() As TipoProcedenciaColeccion
            Get
                Return _TiposProcedencia
            End Get
            Set(value As TipoProcedenciaColeccion)
                _TiposProcedencia = value
            End Set
        End Property

#End Region

    End Class

End Namespace