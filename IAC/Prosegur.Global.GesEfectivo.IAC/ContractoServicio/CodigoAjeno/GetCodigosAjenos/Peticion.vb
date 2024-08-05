Imports System.Xml.Serialization
Imports System.Xml

Namespace CodigoAjeno.GetCodigosAjenos

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 16/04/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetCodigosAjenos")> _
    <XmlRoot(Namespace:="urn:GetCodigosAjenos")> _
    <Serializable()> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

#Region "[Variáveis]"

        Private _CodigosAjenos As CodigoAjeno

#End Region

#Region "[Propriedades]"

        Public Property CodigosAjeno() As CodigoAjeno
            Get
                Return _CodigosAjenos
            End Get
            Set(value As CodigoAjeno)
                _CodigosAjenos = value
            End Set
        End Property


#End Region

    End Class

End Namespace