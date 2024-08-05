Imports System.Xml.Serialization
Imports System.Xml

Namespace CodigoAjeno.GetCodigosAjenos

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 16/04/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetCodigosAjenos")> _
    <XmlRoot(Namespace:="urn:GetCodigosAjenos")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "[Variáveis]"

        Private _EntidadCodigosAjenos As EntidadCodigosAjenoColeccion

#End Region

#Region "[Propriedades]"

        Public Property EntidadCodigosAjenos() As EntidadCodigosAjenoColeccion
            Get
                Return _EntidadCodigosAjenos
            End Get
            Set(value As EntidadCodigosAjenoColeccion)
                _EntidadCodigosAjenos = value
            End Set
        End Property


#End Region


    End Class

End Namespace
