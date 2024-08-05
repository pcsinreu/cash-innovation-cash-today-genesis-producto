Imports Prosegur.Genesis.Comon

Namespace NumeroSerieBillete.SetNumeroDeSerieBillete

    <Serializable()>
    Public Class SetNumeroDeSerieBilleteRespuesta
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

    End Class

End Namespace
