Imports Prosegur.Genesis.Comon
Namespace Integracion
    Public Class AccionGrupoTerminoIAC
        Public Shared Function RecuperarGrupoTerminosIACPorCodigo(pCodigo As String) As Clases.GrupoTerminosIAC
            Return AccesoDatos.Genesis.GrupoTerminosIAC.RecuperarGrupoTerminosIACPorCodigo(pCodigo)
        End Function
        Public Shared Function RecuperarIACs() As List(Of Clases.GrupoTerminosIAC)
            Return AccesoDatos.Genesis.GrupoTerminosIAC.RecuperarIACs()
        End Function
    End Class
End Namespace

