Imports System.Xml.Serialization
Imports System.Xml

Namespace GuardarCliente

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 21/09/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GuardarCliente")> _
    <XmlRoot(Namespace:="urn:GuardarCliente")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIAVEIS]"

        Private _ClientesOk As ClienteOkColeccion
        Private _ClientesError As ClienteErrorColeccion
       
#End Region

#Region "[PROPRIEDADES]"

        Public Property ClientesOk() As ClienteOkColeccion
            Get
                Return _ClientesOk
            End Get
            Set(value As ClienteOkColeccion)
                _ClientesOk = value
            End Set
        End Property

        Public Property ClientesError() As ClienteErrorColeccion
            Get
                Return _ClientesError
            End Get
            Set(value As ClienteErrorColeccion)
                _ClientesError = value
            End Set
        End Property

#End Region

    End Class

End Namespace