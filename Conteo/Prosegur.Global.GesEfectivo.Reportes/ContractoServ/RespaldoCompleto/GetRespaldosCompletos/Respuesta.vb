Imports System.Xml.Serialization
Imports System.Xml

Namespace RespaldoCompleto.GetRespaldosCompletos

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 28/07/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:RespaldoCompleto")> _
    <XmlRoot(Namespace:="urn:RespaldoCompleto")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _RespaldosCompletosCSV As RespaldoCompletoCSVColeccion
        Private _RespaldoCompletoPDF As RespaldoCompletoPDF

#End Region

#Region "Propriedades"

        Public Property RespaldosCompletosCSV() As RespaldoCompletoCSVColeccion
            Get
                Return _RespaldosCompletosCSV
            End Get
            Set(value As RespaldoCompletoCSVColeccion)
                _RespaldosCompletosCSV = value
            End Set
        End Property

        Public Property RespaldoCompletoPDF() As RespaldoCompletoPDF
            Get
                Return _RespaldoCompletoPDF
            End Get
            Set(value As RespaldoCompletoPDF)
                _RespaldoCompletoPDF = value
            End Set
        End Property

#End Region

    End Class

End Namespace