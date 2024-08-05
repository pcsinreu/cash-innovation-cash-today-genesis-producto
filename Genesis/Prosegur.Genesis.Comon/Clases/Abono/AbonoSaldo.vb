Namespace Clases.Abono
    <Serializable()>
    Public Class AbonoSaldo

        Public Sub New()
            Me.Divisa = New DivisaAbono()
            Me.SubCanal = New AbonoInformacion()
            Me.ListaSaldoCuenta = New List(Of SnapshotSaldo)()
            Me.ListaTerminoIAC = New List(Of TerminoIAC)()
            Me.CanalesDocumento = New List(Of AbonoInformacion)()
            Me.SubCanalesDocumento = New List(Of AbonoInformacion)()
            Me.SectoresDocumento = New List(Of AbonoInformacion)()
            Me.Canal = New AbonoInformacion()
        End Sub

        Public Property Identificador As String
        Public Property UsuarioCreacion As String
        Public Property UsuarioModificacion As String
        Public Property Divisa As DivisaAbono
        Public Property Importe As Double
        Public Property ListaSaldoCuenta As List(Of SnapshotSaldo)
        Public Property SubCanal As AbonoInformacion
        Public Property Canal As AbonoInformacion
        Public Property SubCanalesDocumento As List(Of AbonoInformacion)
        Public Property CanalesDocumento As List(Of AbonoInformacion)
        Public Property SectoresDocumento As List(Of AbonoInformacion)
        Public Property ListaTerminoIAC As List(Of TerminoIAC)
        Public Property IdentificadorSnapshot As String
        Public Property IdentificadorDocumento As String

    End Class
End Namespace
