Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedorFIFO

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:ConsultarContenedorFIFO")> _
    <XmlRoot(Namespace:="urn:ConsultarContenedorFIFO")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Prosegur.Genesis.Comon.BaseRespuesta

#Region "[PROPRIEDADES]"

        Public Property Contenedores As List(Of Comon.Contenedor)

#End Region

        Public ReadOnly Property TotalImporte As Decimal
            Get
                Return Me.Contenedores.Sum(Function(contenedor)
                                               Return contenedor.CuentaContenedor.Divisas.Sum(Function(divisa)
                                                                                                  Return divisa.TotalImporte
                                                                                              End Function)
                                           End Function)
            End Get
        End Property

    End Class
End Namespace