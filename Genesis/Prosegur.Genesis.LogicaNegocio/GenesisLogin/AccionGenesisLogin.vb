Imports System.DirectoryServices
Imports Prosegur.Framework.Dicionario
Imports System.Configuration
Imports Prosegur.Genesis.ContractoServicio

Namespace LoginGenesis
    Public Class AccionGenesisLogin

        Public Shared Function Ejecutar(ByVal Peticion As Entidades.Login.Peticion) As Entidades.Login.Respuesta
            Dim respuesta As New Entidades.Login.Respuesta()
            Dim ruta As String = String.Empty

            Try ' Try-Catch especifico para verificar se o usuario/senha informados são válidos ou está bloqueado/expirado.
                respuesta.Codigo = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                UtilAD.VerificarLoginImac(Peticion.Login, Peticion.Password)
            Catch ex As Exception
                respuesta.Codigo = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                respuesta.Descripcion = Tradutor.Traduzir("Gen_msg_Login_LoginIncorrecto")
                Return respuesta
            End Try
            Return respuesta
        End Function
    End Class
End Namespace

