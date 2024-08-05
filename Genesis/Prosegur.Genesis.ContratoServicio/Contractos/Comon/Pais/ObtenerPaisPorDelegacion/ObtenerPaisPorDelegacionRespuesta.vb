Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.Pais.ObtenerPaisPorDelegacion

    <Serializable()>
    Public NotInheritable Class ObtenerPaisPorDelegacionRespuesta
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

        Public Property Pais As Clases.Pais

    End Class

End Namespace