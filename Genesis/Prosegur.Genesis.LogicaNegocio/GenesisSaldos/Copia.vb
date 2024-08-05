Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon
Imports Prosegur.Framework.Dicionario.Tradutor

Namespace GenesisSaldos

    Public Class Copia
        ''' <summary>
        ''' Insere copias para um formulário
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub GuardarCopiasFormulario(identificadorFormulario As String, copias As List(Of Clases.Copia))
            Try
                ValidarObrigatorios(identificadorFormulario, copias)

                AccesoDatos.GenesisSaldos.Copia.GuardarCopiasFormulario(identificadorFormulario, copias)

            Catch ex As Exception
                Throw
            End Try
        End Sub
        ''' <summary>
        ''' Exclui copias de um formulário
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub BorrarCopiasFormulario(identificadorFormulario As String)
            Try

                AccesoDatos.GenesisSaldos.Copia.BorrarCopiasFormulario(identificadorFormulario)

            Catch ex As Exception
                Throw
            End Try
        End Sub
        ''' <summary>
        ''' Recuperar copias para um formulário
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Function ObtenerCopiasFormulario(identificadorFormulario As String) As List(Of Clases.Copia)
            Try
                Dim copias As New List(Of Clases.Copia)

                Return AccesoDatos.GenesisSaldos.Copia.RecuperaCopias(identificadorFormulario)

            Catch ex As Exception
                Throw
            End Try
        End Function

        Public Shared Sub ValidarObrigatorios(identificadorFormulario As String, copias As List(Of Comon.Clases.Copia))

            Dim erros As New System.Text.StringBuilder

            If String.IsNullOrEmpty(identificadorFormulario) Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("050_Identificador_Formulario_Obligatorio"))
            End If

            If copias Is Nothing OrElse copias.Count = 0 Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("050_Copias_Vazia"))
            End If

            For Each copia In copias

                If String.IsNullOrEmpty(copia.Identificador) Then
                    erros.AppendLine(Traduzir("050_Identificador_Copias"))
                End If

                If String.IsNullOrEmpty(copia.Destino) Then
                    erros.AppendLine(Traduzir("050_Destino"))
                End If

                If String.IsNullOrEmpty(copia.FechaHoraCreacion) Then
                    erros.AppendLine(Traduzir("050_Fecha_Creacion_Obligatorio"))
                End If

                If String.IsNullOrEmpty(copia.UsuarioCreacion) Then
                    erros.AppendLine(Traduzir("050_Usuario_Creacion_Obligatorio"))
                End If

                If String.IsNullOrEmpty(copia.FechaHoraModificacion) Then
                    erros.AppendLine(Traduzir("050_Fecha_Modificacion_Obligatorio"))
                End If

                If String.IsNullOrEmpty(copia.UsuarioModificacion) Then
                    erros.AppendLine(Traduzir("050_Usuario_Modificacion_Obligatorio"))
                End If

            Next


            If erros.Length > 0 Then
                Throw New Excepcion.CampoObrigatorioException(erros.ToString)
            End If
        End Sub
    End Class
End Namespace
