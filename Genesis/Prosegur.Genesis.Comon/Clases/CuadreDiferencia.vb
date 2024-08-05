Imports System.Windows.Media

Namespace Clases
    ''' <summary>
    ''' Classe de Cuadre diferencia.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Class CuadreDiferencia
        Inherits BaseClase

        Public Property CodigoISODivisa As String
        Public Property CodigoDenominacion As String
        Public Property DescripcionDivisa As String
        Public Property DescripcionDenominacion As String
        Public Property Disponible As Boolean
        Public Property SaldoActual As Decimal
        Public Property SaldoContado As Decimal
        Public Property Diferencia As Decimal
        Public Property OperadorSaldo As Enumeradores.Salidas.OperadorSaldo
        Public Property Color As Brush

    End Class

End Namespace

