Imports Prosegur.Genesis.Comon

Namespace NumeroSerieBillete.GetNumeroDeSerieBillete

    <Serializable()>
    Public NotInheritable Class GetNumeroDeSerieBilletePeticion
        Inherits BasePeticion

        Public Property CodAplicacionGenesis As Integer
        Public Property IdRemesa As String
        Public Property IdBulto As String
        Public Property CodDelegacion As String

    End Class

End Namespace
