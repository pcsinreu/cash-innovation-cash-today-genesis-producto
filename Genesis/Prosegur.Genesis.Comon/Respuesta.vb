Imports Prosegur.Genesis.Comon

''' <summary>
''' Classe de resposta simples com apenas um objeto de resposta
''' </summary>
<Serializable()>
Public NotInheritable Class Respuesta(Of TRespuesta)
    Inherits BaseRespuesta
    Implements Paginacion.IRespuestaPaginacion

    Public Property Retorno As TRespuesta

    Public Property ParametrosPaginacion As Paginacion.ParametrosRespuestaPaginacion Implements Paginacion.IRespuestaPaginacion.ParametrosPaginacion

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
