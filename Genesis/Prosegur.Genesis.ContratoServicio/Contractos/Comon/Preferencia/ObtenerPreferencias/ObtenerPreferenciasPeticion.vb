Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.Preferencia.ObtenerPreferencias
    <Serializable()>
    Public Class ObtenerPreferenciasPeticion
        Inherits BasePeticion

        Public Property CodigoUsuario As String
        Public Property codigoAplicacion As Prosegur.Genesis.Comon.Enumeradores.CodigoAplicacion
        Public Property CodigoFuncionalidad As String


    End Class
End Namespace