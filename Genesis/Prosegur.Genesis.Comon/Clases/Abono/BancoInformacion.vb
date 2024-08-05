Namespace Clases.Abono
    <Serializable()>
    Public Class BancoInformacion
        Inherits AbonoInformacion
        Public Sub New()
            Me.DatosBancarios = New List(Of DatoBancario)()
        End Sub

        Public Property DatosBancarios As List(Of DatoBancario)

    End Class
End Namespace

