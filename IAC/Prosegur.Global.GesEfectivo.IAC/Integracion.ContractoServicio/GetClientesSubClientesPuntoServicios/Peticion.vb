Imports System.Xml.Serialization
Imports System.Xml

Namespace GetClientesSubClientesPuntoServicios

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 21/09/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GuardarCliente")> _
    <XmlRoot(Namespace:="urn:GuardarCliente")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIAVEIS]"

        Private _Clientes As ClienteColeccion

#End Region

#Region "[PROPRIEDADES]"

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