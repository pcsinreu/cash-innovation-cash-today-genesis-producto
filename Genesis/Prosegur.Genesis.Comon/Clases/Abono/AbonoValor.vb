Namespace Clases.Abono
    <Serializable()>
    Public Class AbonoValor
        Public Sub New()
            Me.Cliente = New AbonoInformacion()
            Me.SubCliente = New AbonoInformacion()
            Me.PtoServicio = New AbonoInformacion()
            Me.Divisa = New DivisaAbono()
            Me.CuentasDisponibles = New List(Of BancoInformacion)()
            Me.Cuenta = New DatoBancario()
            Me.AbonoElemento = New AbonoElemento()
            Me.AbonoSaldo = New AbonoSaldo()
            Me.BancoCuenta = New BancoInformacion()
        End Sub
        Public Sub New(abono As Abono)
            Me.New()
            Me.Abono = abono
            abono.AbonosValor.Add(Me)
        End Sub

        Public Property Identificador As String
        Public Property Cliente As AbonoInformacion
        Public Property SubCliente As AbonoInformacion
        Public Property PtoServicio As AbonoInformacion
        Public Property Divisa As DivisaAbono
        Public Property BancoCuenta As BancoInformacion
        Public Property CuentasDisponibles As List(Of BancoInformacion)
        Public Property Cuenta As DatoBancario
        Public Property Observaciones As String
        Public Property Importe As Decimal
        Public Property Abono As Abono
        Public Property AbonoElemento As AbonoElemento
        Public Property AbonoSaldo As AbonoSaldo
        Public Property UsuarioCreacion As String
        Public Property UsuarioModificacion As String
        Public Property MultiplesCuentas As Boolean
        Public Property DivisaContieneResto As Boolean

        Public Function AbonoPorTipo(tipoAbono As Enumeradores.TipoAbono)
            If tipoAbono = Enumeradores.TipoAbono.Elemento Then
                Return Me.AbonoElemento
            ElseIf tipoAbono = Enumeradores.TipoAbono.Saldos OrElse tipoAbono = Enumeradores.TipoAbono.Pedido Then
                Return Me.AbonoSaldo
            Else
                Return Nothing
            End If
        End Function
    End Class
End Namespace
