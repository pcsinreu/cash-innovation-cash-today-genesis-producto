Imports Prosegur.Genesis.Comon
Imports Prosegur.Framework.Dicionario.Tradutor

Namespace Genesis

    ''' <summary>
    ''' Classe TipoBulto
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TipoBulto

        ''' <summary>
        ''' Recupera todos os tipos de bultos ativos
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerTiposBultos() As List(Of Clases.TipoBulto)

            Dim tiposBultos As New List(Of Clases.TipoBulto)

            Try

                tiposBultos = AccesoDatos.Genesis.TiposBultos.RecuperaTiposBultos

            Catch ex As Exception
                Throw
            End Try

            Return tiposBultos
        End Function

        ''' <summary>
        ''' Recupera todos os tipos de bultos ativos de um formulário
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerTiposBultos(identificadorFormulario) As List(Of Clases.TipoBulto)

            Dim tiposBultos As New List(Of Clases.TipoBulto)

            Try

                tiposBultos = AccesoDatos.Genesis.TiposBultos.RecuperaTiposBultos(identificadorFormulario)

            Catch ex As Exception
                Throw
            End Try

            Return tiposBultos
        End Function

        ''' <summary>
        ''' Insere tipos de bultos para um formulário
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub GuardaTiposBultosFormulario(identificadorFormulario As String, tiposBultos As List(Of Clases.TipoBulto))
            Try
                ValidarObrigatorios(identificadorFormulario, tiposBultos)

                AccesoDatos.Genesis.TiposBultos.GuardarTiposBultosFormulario(identificadorFormulario, tiposBultos)

            Catch ex As Exception
                Throw
            End Try
        End Sub
        ''' <summary>
        ''' Exclui tipos de bultos de um formulário
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub BorrarTiposBultosFormulario(identificadorFormulario As String)
            Try

                AccesoDatos.Genesis.TiposBultos.BorrarTiposBultosFormulario(identificadorFormulario)

            Catch ex As Exception
                Throw
            End Try
        End Sub
        Public Shared Sub ValidarObrigatorios(identificadorFormulario As String, tiposBultos As List(Of Comon.Clases.TipoBulto))

            Dim erros As New System.Text.StringBuilder

            If String.IsNullOrEmpty(identificadorFormulario) Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("049_Identificador_Formulario_Obligatorio"))
            End If

            If tiposBultos Is Nothing OrElse tiposBultos.Count = 0 Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("049_Tipo_Bulto_Vazia"))
            End If

            For Each tipobulto In tiposBultos

                If String.IsNullOrEmpty(tipobulto.Identificador) Then
                    erros.AppendLine(Traduzir("049_Identificador_Tipo_Bulto"))
                End If

                If String.IsNullOrEmpty(tipobulto.FechaHoraCreacion) Then
                    erros.AppendLine(Traduzir("049_Fecha_Creacion_Obligatorio"))
                End If

                If String.IsNullOrEmpty(tipobulto.UsuarioCreacion) Then
                    erros.AppendLine(Traduzir("049_Usuario_Creacion_Obligatorio"))
                End If

                If String.IsNullOrEmpty(tipobulto.FechaHoraModificacion) Then
                    erros.AppendLine(Traduzir("049_Fecha_Modificacion_Obligatorio"))
                End If

                If String.IsNullOrEmpty(tipobulto.UsuarioModificacion) Then
                    erros.AppendLine(Traduzir("049_Usuario_Modificacion_Obligatorio"))
                End If

            Next


            If erros.Length > 0 Then
                Throw New Excepcion.CampoObrigatorioException(erros.ToString)
            End If
        End Sub
    End Class

End Namespace