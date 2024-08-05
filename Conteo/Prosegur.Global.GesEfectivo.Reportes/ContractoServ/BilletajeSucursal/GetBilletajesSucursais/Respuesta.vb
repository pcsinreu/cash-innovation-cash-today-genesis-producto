Imports System.Xml.Serialization
Imports System.Xml

Namespace BilletajeSucursal.GetBilletajesSucursais

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 17/07/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:BilletajeSucursal")> _
    <XmlRoot(Namespace:="urn:BilletajeSucursal")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _billetajesSucursaisCSV As BilletajeSucursalCSVColeccion
        Private _billetajesSucursaisPDF As BilletajeSucursalPDFColeccion

#End Region

#Region "Propriedades"

        Public Property BilletajesSucursaisCSV() As BilletajeSucursalCSVColeccion
            Get
                Return _billetajesSucursaisCSV
            End Get
            Set(value As BilletajeSucursalCSVColeccion)
                _billetajesSucursaisCSV = value
            End Set
        End Property

        Public Property BilletajesSucursaisPDF() As BilletajeSucursalPDFColeccion
            Get
                Return _billetajesSucursaisPDF
            End Get
            Set(value As BilletajeSucursalPDFColeccion)
                _billetajesSucursaisPDF = value
            End Set
        End Property

#End Region

    End Class

End Namespace