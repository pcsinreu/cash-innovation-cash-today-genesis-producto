Imports System.Xml.Serialization
Imports System.Xml

Namespace CodigoAjeno.SetCodigosAjenos

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 16/04/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetCodigosAjenos")> _
    <XmlRoot(Namespace:="urn:SetCodigosAjenos")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _CodigosAjeno As CodigoAjenoColeccion

#End Region

#Region "[Propriedades]"

        Public Property CodigosAjenos() As CodigoAjenoColeccion
            Get
                Return _CodigosAjeno
            End Get
            Set(value As CodigoAjenoColeccion)
                _CodigosAjeno = value
            End Set
        End Property


#End Region

    End Class

End Namespace