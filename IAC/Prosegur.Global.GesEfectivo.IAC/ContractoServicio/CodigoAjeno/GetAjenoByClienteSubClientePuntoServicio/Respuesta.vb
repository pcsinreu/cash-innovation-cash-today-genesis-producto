Imports System.Xml.Serialization
Imports System.Xml

Namespace CodigoAjeno.GetAjenoByClienteSubClientePuntoServicio

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 19/07/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetAjenoByClienteSubClientePuntoServicio")> _
    <XmlRoot(Namespace:="urn:GetAjenoByClienteSubClientePuntoServicio")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "[Variáveis]"

        Private _Ajenos As AjenoColeccion

#End Region

#Region "[Propriedades]"

        Public Property Ajenos() As AjenoColeccion
            Get
                Return _Ajenos
            End Get
            Set(value As AjenoColeccion)
                _Ajenos = value
            End Set
        End Property


#End Region


    End Class

End Namespace
