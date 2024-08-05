Imports System.Xml.Serialization
Imports System.Xml

Namespace CodigoAjeno.SetCodigosAjenos

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 16/04/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetCodigosAjenos")> _
    <XmlRoot(Namespace:="urn:SetCodigosAjenos")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _CodigosAjenos As CodigoAjenoRespuestaColeccion

#End Region

#Region "[Propriedades]"

        Public Property CodigosAjenos() As CodigoAjenoRespuestaColeccion
            Get
                Return _CodigosAjenos
            End Get
            Set(value As CodigoAjenoRespuestaColeccion)
                _CodigosAjenos = value
            End Set
        End Property


#End Region


    End Class

End Namespace
