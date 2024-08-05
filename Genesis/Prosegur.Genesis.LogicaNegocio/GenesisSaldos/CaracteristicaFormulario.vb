Imports Prosegur.Genesis.Comon
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon.Extenciones

Namespace GenesisSaldos
    ''' <summary>
    ''' Classe responsável pelo Caracteristicads do Formulário.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CaracteristicaFormulario

        Public Shared Function RecuperarCaracteristicasFormulario(identificadorFormulario As String) As List(Of Enumeradores.CaracteristicaFormulario)
            Return AccesoDatos.GenesisSaldos.CaracteristicaFormulario.RecuperarCaracteristicasFormulario(identificadorFormulario)
        End Function

        Public Shared Sub GuardarCaracteristicasFormulario(formulario As Clases.Formulario)
            Dim listaCaracteristicas As List(Of Enumeradores.CaracteristicaFormulario) = Nothing
            Try
                If formulario Is Nothing OrElse formulario.Caracteristicas Is Nothing OrElse formulario.Caracteristicas.Count = 0 Then
                    Throw New Excepcion.CampoObrigatorioException(Traduzir("045_Caracteristicas_Obligatorio"))
                End If

                ValidarObrigatorios(formulario)

                For Each carac In formulario.Caracteristicas
                    Dim caracFormulario As New Clases.CaracteristicaFormulario
                    caracFormulario.Identificador = System.Guid.NewGuid.ToString
                    caracFormulario.IdentificadorFormulario = formulario.Identificador
                    'Recupera o id da característica pelo código
                    caracFormulario.IdentificadorCaracteristica = RecuperaIdentificadorCaracteristica(carac.RecuperarValor())
                    If String.IsNullOrEmpty(caracFormulario.IdentificadorCaracteristica) Then
                        'Se não achar o id da característica pelo código
                        Throw New Excepcion.CampoObrigatorioException(Traduzir("045_Caracteristicas_Obligatorio"))
                    End If
                    caracFormulario.FechaHoraCreacion = formulario.FechaHoraCreacion
                    caracFormulario.UsuarioCreacion = formulario.UsuarioCreacion
                    caracFormulario.FechaHoraModificacion = formulario.FechaHoraModificacion
                    caracFormulario.UsuarioModificacion = formulario.UsuarioModificacion

                    'Insere Característica Formulário
                    AccesoDatos.GenesisSaldos.CaracteristicaFormulario.GuardarCaracteristicaFormulario(caracFormulario)

                Next

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Shared Sub BorrarCaracteristicaFormularioPorFormulario(identificadorFormulario As String)

            Try
                'Insere Característica Formulário
                AccesoDatos.GenesisSaldos.CaracteristicaFormulario.BorrarCaracteristicaFormularioPorFormulario(identificadorFormulario)

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub
        ''' <summary>
        ''' Valida os campos obrigatórios
        ''' </summary>
        ''' <param name="formulario">Formulário</param>
        ''' <remarks></remarks>
        Public Shared Sub ValidarObrigatorios(formulario As Clases.Formulario)

            Dim erros As New System.Text.StringBuilder

            If String.IsNullOrEmpty(formulario.Identificador) Then
                erros.AppendLine(Traduzir("045_Identificador_Obligatorio"))
            End If

            If String.IsNullOrEmpty(formulario.FechaHoraCreacion) Then
                erros.AppendLine(Traduzir("045_Fecha_Creacion_Obligatorio"))
            End If

            If String.IsNullOrEmpty(formulario.UsuarioCreacion) Then
                erros.AppendLine(Traduzir("045_Fecha_Creacion_Obligatorio"))
            End If

            If String.IsNullOrEmpty(formulario.FechaHoraModificacion) Then
                erros.AppendLine(Traduzir("045_Fecha_Modificacion_Obligatorio"))
            End If

            If String.IsNullOrEmpty(formulario.UsuarioModificacion) Then
                erros.AppendLine(Traduzir("045_Usuario_Modificacion_Obligatorio"))
            End If

            If erros.Length > 0 Then
                Throw New Excepcion.CampoObrigatorioException(erros.ToString)
            End If
        End Sub

        ''' <summary>
        ''' Recupera o id da característica a partir do codigo
        ''' </summary>
        ''' <param name="codigoCaracteristica">Código da Caracteristica</param>
        ''' <remarks></remarks>
        Private Shared Function RecuperaIdentificadorCaracteristica(codigoCaracteristica As String) As String

            If Not String.IsNullOrEmpty(codigoCaracteristica) Then
                Return AccesoDatos.GenesisSaldos.CaracteristicaFormulario.RecuperarIdentificadorCaracteristica(codigoCaracteristica)
            Else
                Return String.Empty
            End If

        End Function

    End Class

End Namespace
