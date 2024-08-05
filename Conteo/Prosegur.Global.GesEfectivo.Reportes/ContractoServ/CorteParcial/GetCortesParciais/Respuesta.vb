Imports System.Xml.Serialization
Imports System.Xml

Namespace CorteParcial.GetCortesParciais

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 17/07/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:CorteParcial")> _
    <XmlRoot(Namespace:="urn:CorteParcial")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _CortesParciaisCSV As CorteParcialCSVColeccion
        Private _CorteParcialPDF As CorteParcialPDF

#End Region

#Region "Propriedades"

        Public Property CortesParciaisCSV() As CorteParcialCSVColeccion
            Get
                Return _CortesParciaisCSV
            End Get
            Set(value As CorteParcialCSVColeccion)
                _CortesParciaisCSV = value
            End Set
        End Property

        Public Property CorteParcialPDF() As CorteParcialPDF
            Get
                Return _CorteParcialPDF
            End Get
            Set(value As CorteParcialPDF)
                _CorteParcialPDF = value
            End Set
        End Property


#End Region

    End Class

End Namespace