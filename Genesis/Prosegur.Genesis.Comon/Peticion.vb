Imports Prosegur.Genesis.Comon

''' <summary>
''' Classe de petição simples com apenas um parâmetro tipado
''' </summary>
<Serializable()>
Public NotInheritable Class Peticion(Of TPeticion)
    Inherits BaseRespuesta
    Implements Paginacion.IPeticionPaginacion

    Public Property Parametro As TPeticion

    Public Property ParametrosPaginacion As Paginacion.ParametrosPeticionPaginacion Implements Paginacion.IPeticionPaginacion.ParametrosPaginacion

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

