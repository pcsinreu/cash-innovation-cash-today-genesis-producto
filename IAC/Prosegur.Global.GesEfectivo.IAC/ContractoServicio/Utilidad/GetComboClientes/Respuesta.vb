Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboClientes

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboClientes")> _
    <XmlRoot(Namespace:="urn:GetComboClientes")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "[Variáveis]"

        Private _Clientes As ClienteColeccion

#End Region

#Region "[Propriedades]"

        Public Property Clientes() As ClienteColeccion
            Get
                Return _Clientes
            End Get
            Set(value As ClienteColeccion)
                _Clientes = value
            End Set
        End Property

#End Region

    End Class

End Namespace