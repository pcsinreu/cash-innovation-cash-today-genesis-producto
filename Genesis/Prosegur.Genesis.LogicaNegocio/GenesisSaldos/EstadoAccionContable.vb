Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon
Imports Prosegur.Framework.Dicionario.Tradutor

Namespace GenesisSaldos

    ''' <summary>
    ''' Clase AccionEstadoAccionContable
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 05/09/2013 - Criado
    ''' </history>
    Public Class EstadoAccionContable

        Public Shared Function ObtenerEstadosAccionContable(identificadorAccionContable As String) As List(Of Clases.EstadoAccionContable)
            Dim listaEstadoAccionContable As List(Of Clases.EstadoAccionContable)
            Try
                listaEstadoAccionContable = AccesoDatos.GenesisSaldos.EstadoAccionContable.ObtenerEstadosAccionContable(identificadorAccionContable)

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            Return listaEstadoAccionContable

        End Function

        Public Shared Sub GuardarEstadosAccionesContables(estadosAccionesCotables As List(Of Clases.EstadoAccionContable))

            Try
                If estadosAccionesCotables Is Nothing OrElse estadosAccionesCotables.Count = 0 Then
                    Throw New Excepcion.CampoObrigatorioException(Traduzir("047_Estados_Vazio"))
                End If

                For Each estado In estadosAccionesCotables
                    ValidarObrigatorios(estado)
                Next

                For Each estado In estadosAccionesCotables
                    AccesoDatos.GenesisSaldos.EstadoAccionContable.GuardarEstadoAccionContable(estado)
                Next

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Sub
        Public Shared Sub BorrarEstadosAccionesContablesPorAccionContable(identificadorAccionContable As String)

            Try
                AccesoDatos.GenesisSaldos.EstadoAccionContable.BorrarEstadoAccionContablePorAccionContable(identificadorAccionContable)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Sub

        ''' <summary>
        ''' Valida os campos obrigatórios
        ''' </summary>
        ''' <param name="estadoAccionContable">Estado Ação Contábil</param>
        ''' <remarks></remarks>
        Public Shared Sub ValidarObrigatorios(estadoAccionContable As Clases.EstadoAccionContable)

            Dim erros As New System.Text.StringBuilder

            If String.IsNullOrEmpty(estadoAccionContable.Identificador) Then
                erros.AppendLine(Traduzir("047_Identificador_Vazio"))
            End If

            If String.IsNullOrEmpty(estadoAccionContable.IdentificadorAccionContable) Then
                erros.AppendLine(Traduzir("047_Identificador_Acao_Contabil_Vazio"))
            End If

            If String.IsNullOrEmpty(estadoAccionContable.Codigo) Then
                erros.AppendLine(Traduzir("047_Codigo_Estado"))
            End If

            If String.IsNullOrEmpty(estadoAccionContable.OrigemDisponible) Then
                erros.AppendLine(Traduzir("047_Origem_Disponivel"))
            End If

            If String.IsNullOrEmpty(estadoAccionContable.OrigemNoDisponible) Then
                erros.AppendLine(Traduzir("047_Origem_No_Disponivel"))
            End If

            If String.IsNullOrEmpty(estadoAccionContable.DestinoDisponible) Then
                erros.AppendLine(Traduzir("047_Destino_Disponivel"))
            End If

            If String.IsNullOrEmpty(estadoAccionContable.DestinoNoDisponible) Then
                erros.AppendLine(Traduzir("047_Destino_No_Disponivel"))
            End If

            If String.IsNullOrEmpty(estadoAccionContable.FechaHoraCreacion) Then
                erros.AppendLine(Traduzir("047_Fecha_Creacion_Vazia"))
            End If

            If String.IsNullOrEmpty(estadoAccionContable.FechaHoraModificacion) Then
                erros.AppendLine(Traduzir("047_Fecha_Modificacion_Vazia"))
            End If

            If String.IsNullOrEmpty(estadoAccionContable.UsuarioCreacion) Then
                erros.AppendLine(Traduzir("047_Usuario_Creacion_Vazia"))
            End If

            If String.IsNullOrEmpty(estadoAccionContable.UsuarioModificacion) Then
                erros.AppendLine(Traduzir("047_Usuario_Modificacion_Vazia"))
            End If


            If erros.Length > 0 Then
                Throw New Excepcion.CampoObrigatorioException(erros.ToString)
            End If
        End Sub

    End Class

End Namespace



