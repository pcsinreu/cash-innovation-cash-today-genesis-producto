Imports Prosegur.Genesis.Comon

Namespace Contractos.Genesis.TipoContenedor

    ''' <summary>
    ''' Classe TipoContenedorRespuesta
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TipoContenedorRespuesta
        Inherits BaseRespuestaPaginacion

        Sub New()
            MyBase.New()
        End Sub

        Public Property TiposContenedor As List(Of Clases.TipoContenedor)

    End Class

End Namespace