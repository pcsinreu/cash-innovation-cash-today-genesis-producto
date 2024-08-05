Namespace GenesisSaldos.Pantallas.Limites
    Public Class Periodos
        Public Shared Function Ejecutar(peticion As Comon.Pantallas.Limites.Peticion) As Comon.Pantallas.Limites.Respuesta
            Dim objRespuesta As Comon.Pantallas.Limites.Respuesta = Nothing
            Try
                If String.IsNullOrWhiteSpace(peticion.CodUsuario) Then
                    peticion.CodUsuario = "RECUPERAR_PERIODOS"
                End If

                objRespuesta = AccesoDatos.GenesisSaldos.Periodos.RecuperarPeriodos(peticion)
            Catch ex As Exception
                Throw ex
            End Try
            Return objRespuesta
        End Function
    End Class
End Namespace
