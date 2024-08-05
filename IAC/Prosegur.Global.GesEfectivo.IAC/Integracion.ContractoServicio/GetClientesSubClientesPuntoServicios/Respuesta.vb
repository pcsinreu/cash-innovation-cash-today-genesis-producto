Imports System.Xml.Serialization
Imports System.Xml

Namespace GetClientesSubClientesPuntoServicios

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
        Private _SubClientesOk As SubClienteOkColeccion
        Private _PuntoServiciosOk As PuntoServicioOkColeccion
        Private _ClientesError As ClienteErrorColeccion
        Private _SubClientesError As SubClienteErrorColeccion
        Private _PuntoServiciosError As PuntoServicioErrorColeccion

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

        Public Property SubClientesOk() As SubClienteOkColeccion
            Get
                Return _SubClientesOk
            End Get
            Set(value As SubClienteOkColeccion)
                _SubClientesOk = value
            End Set
        End Property

        Public Property PuntoServiciosOk() As PuntoServicioOkColeccion
            Get
                Return _PuntoServiciosOk
            End Get
            Set(value As PuntoServicioOkColeccion)
                _PuntoServiciosOk = value
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

        Public Property SubClientesError() As SubClienteErrorColeccion
            Get
                Return _SubClientesError
            End Get
            Set(value As SubClienteErrorColeccion)
                _SubClientesError = value
            End Set
        End Property

        Public Property PuntoServiciosError() As PuntoServicioErrorColeccion
            Get
                Return _PuntoServiciosError
            End Get
            Set(value As PuntoServicioErrorColeccion)
                _PuntoServiciosError = value
            End Set
        End Property

#End Region

    End Class

End Namespace