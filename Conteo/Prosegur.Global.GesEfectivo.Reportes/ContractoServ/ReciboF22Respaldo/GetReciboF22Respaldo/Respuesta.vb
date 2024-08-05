Imports System.Xml.Serialization
Imports System.Xml

Namespace ReciboF22Respaldo.GetReciboF22Respaldo

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 23/03/2011 Criado
    ''' </history>
    <XmlType(Namespace:="urn:RespaldoCompleto")> _
    <XmlRoot(Namespace:="urn:RespaldoCompleto")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _ReciboF22Respaldo As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22RespaldoColeccion

#End Region

#Region "Propriedades"

        Public Property ReciboF22Respaldo() As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22RespaldoColeccion
            Get
                Return _ReciboF22Respaldo
            End Get
            Set(value As ContractoServ.ReciboF22Respaldo.GetReciboF22Respaldo.ReciboF22RespaldoColeccion)
                _ReciboF22Respaldo = value
            End Set
        End Property

#End Region

    End Class

End Namespace