Imports Prosegur.Genesis.Comon

Namespace NumeroSerieBillete.SetNumeroDeSerieBillete

    <Serializable()>
    Public NotInheritable Class SetNumeroDeSerieBilletePeticion
        Inherits BasePeticion

        Public Property CodAplicacionGenesis As Integer
        Public Property idRemesa As String
        Public Property idBulto As String
        Property DenominacionBilletes As List(Of Clases.DenominacionBillete)

    End Class

End Namespace
