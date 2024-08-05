Imports Prosegur.Genesis.Comon

Namespace NumeroSerieBillete.GetNumeroDeSerieBillete

    <Serializable()>
    Public Class GetNumeroDeSerieBilleteRespuesta
        Inherits BaseRespuesta

        Sub New()
            MyBase.New()
        End Sub

        Sub New(mensaje As String)
            MyBase.New(mensaje)
        End Sub

        Sub New(exception As Exception)
            MyBase.New(exception)
        End Sub

        Property DenominacionBilletes As List(Of Clases.DenominacionBillete)

    End Class

End Namespace
