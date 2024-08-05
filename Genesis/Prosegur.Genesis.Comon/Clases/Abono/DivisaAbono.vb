Namespace Clases.Abono
    <Serializable()>
    Public Class DivisaAbono
        Public Sub New()
            Me.ListaEfectivo = New List(Of EfectivoAbono)()
            Me.ListaMedioPago = New List(Of MedioPagoAbono)()
            Me.Totales = New TotalesAbono
        End Sub

        Public Property Identificador As String
        Public Property CodigoISO As String
        Public Property Descripcion As String
        Private Property _Color As Drawing.Color
        Public Property Color As Drawing.Color
            Get
                Return _Color
            End Get
            Set(value As Drawing.Color)
                _Color = value
                Me.ColorHTML = Drawing.ColorTranslator.ToHtml(value)
            End Set
        End Property
        Public Property ColorHTML As String

        Public Property ListaEfectivo As List(Of EfectivoAbono)
        Public Property ListaMedioPago As List(Of MedioPagoAbono)
        Public Property Totales As TotalesAbono
    End Class

End Namespace

