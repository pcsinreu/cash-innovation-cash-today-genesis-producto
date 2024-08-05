Imports System.Xml.Serialization
Imports System.Xml

Namespace ContadoPuesto.ListarContadoPuesto

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 13/08/2010 Criado
    ''' </history>
    <XmlType(Namespace:="urn:ContadoPuesto")> _
    <XmlRoot(Namespace:="urn:ContadoPuesto")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _ContadoPuestoCSV As List(Of ContadoPuesto)

        Private _ContadoPuestoPDF As List(Of ContadoPuesto)

#End Region

#Region "Propriedades"

        Public Property ContadoPuestoCSV() As List(Of ContadoPuesto)
            Get
                Return _ContadoPuestoCSV
            End Get
            Set(value As List(Of ContadoPuesto))
                _ContadoPuestoCSV = value
            End Set
        End Property

        Public Property ContadoPuestoPDF() As List(Of ContadoPuesto)
            Get
                Return _ContadoPuestoPDF
            End Get
            Set(value As List(Of ContadoPuesto))
                _ContadoPuestoPDF = value
            End Set
        End Property

#End Region

    End Class

End Namespace