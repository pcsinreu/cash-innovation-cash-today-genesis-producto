Namespace GetATM

    <Serializable()> _
    Public Class ATM
        Inherits Comum.ATM

        Public Property IdCajero As String
        Public Property CodigoClienteFaturacion As String
        Public Property DescripcionClienteFaturacion As String
        Public Property CodigoCajero As String
        Public Property DescripcionModeloCajero As String
        Public Property DescripcionRed As String
        Public Property Morfologias As List(Of GetATM.Morfologia)

    End Class

End Namespace



