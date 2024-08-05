Namespace Prosegur.Genesis.GenerarReporte.Classes
    Public Class Util

        Public Shared Sub GravarArquivo(str As String, GenerarLog As Boolean,
                                        DireccionArchivos As String, DireccionArchivoLog As String)

            ' verifica se o parâmetro GENERAR_ARCHIVO_LOG foi informado corretamente
            If GenerarLog Then

                ' Valida existencia do diretorio
                If Not System.IO.Directory.Exists(DireccionArchivos) Then
                    ' se não existe, cria o diretorio
                    Dim _Dir As System.IO.DirectoryInfo = System.IO.Directory.CreateDirectory(DireccionArchivos)
                    DireccionArchivos = _Dir.FullName

                End If

                ' grava o log em disco
                Dim fil As New System.IO.StreamWriter(DireccionArchivoLog, True)
                fil.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff tt") & "-" & str)
                fil.Close()


            End If

        End Sub

        ''' <summary>
        ''' Recupera o valor dos parametros
        ''' </summary>
        ''' <param name="objParametros"></param>
        ''' <param name="NombreParametro"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarValorParametro(objParametros As List(Of String), NombreParametro As String) As String

            If objParametros IsNot Nothing AndAlso objParametros.Count > 0 Then

                Dim objTipoReporte As String = (From p In objParametros Where p.Split("=").FirstOrDefault = NombreParametro Select p).FirstOrDefault

                If Not String.IsNullOrEmpty(objTipoReporte) Then
                    Return objTipoReporte.Split("=").Last
                End If

            End If

            Return String.Empty
        End Function

    End Class

End Namespace