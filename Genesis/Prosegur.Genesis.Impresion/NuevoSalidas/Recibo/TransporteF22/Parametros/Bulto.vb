Namespace NuevoSalidas.Recibo.TransporteF22.Parametros

    ''' <summary>
    ''' Entidad Bulto
    ''' </summary>
    ''' <history>[jviana] 23/08/2010 Creado</history>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class Bulto

#Region "[PROPIEDADES]"

        Public Property CodPrecintoBulto() As String
        Public Property CodBolsa() As String
        Public Property CodIsoDivisa As String
        Public Property TipoCaja As String
        Public Property Cantidad As Integer
        Public Property EfectivoDetalle As EfectivoDetalleColeccion
        Public Property ValorTotalBulto As Decimal
        Public Property ValorTotalEfectivo As Decimal
        Public Property ValorTotalCheque As Decimal
        Public Property ValorTotalOtros As Decimal

#End Region

    End Class

End Namespace


