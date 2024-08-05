Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel

Namespace Genesis

    ''' <summary>
    ''' Clase AccionTiposector
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [henrique.ribeiro] 11/10/2013 - Criado
    ''' </history>
    Public Class TipoSector
        ''' <summary>
        ''' Método que recupera as delegações pelos seus respectivos codigos.
        ''' </summary>
        ''' <param name="codigos">Codigos a serem pesquisados</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerPorCodigos(codigoDelegacion As String, codigoPlanta As String, ParamArray codigos As String()) As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.TipoSector)
            Return Prosegur.Genesis.AccesoDatos.Genesis.TipoSector.ObtenerPorCodigos(codigoDelegacion, codigoPlanta, codigos)
        End Function
        ''' <summary>
        ''' Método que recupera os todos tipos sectores ativos
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerTiposSectores() As List(Of Prosegur.Genesis.Comon.Clases.TipoSector)
            Return Prosegur.Genesis.AccesoDatos.Genesis.TipoSector.RecuperarTiposSectores
        End Function

        ''' <summary>
        ''' Método que recupera os todos tipos sectores de um formulário
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerTiposSectores(identificadorFormulario As String, codigoRelacionConFormulario As String) As List(Of Prosegur.Genesis.Comon.Clases.TipoSector)
            Return Prosegur.Genesis.AccesoDatos.Genesis.TipoSector.RecuperarTiposSectores(identificadorFormulario, codigoRelacionConFormulario)
        End Function

        ''' <summary>
        ''' Método que insere tipos sectores para formulário
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub GuardarTiposSectoresFormulario(identificadorFormulario As String, relacionConFormulario As String, tiposSectores As List(Of Comon.Clases.TipoSector))

            Try
                ValidarObrigatorios(identificadorFormulario, relacionConFormulario, tiposSectores)

                AccesoDatos.Genesis.TipoSector.GuardarTiposSectoresFormulario(identificadorFormulario, relacionConFormulario, tiposSectores)

            Catch ex As Excepcion.CampoObrigatorioException
                Throw
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try

        End Sub
        ''' <summary>
        ''' Método que exclui tipos sectores de formulário
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub BorrarTiposSectoresFormulario(identificadorFormulario As String)

            Try
                AccesoDatos.Genesis.TipoSector.BorrarTiposSectoresFormulario(identificadorFormulario)

            Catch ex As Excepcion.CampoObrigatorioException
                Throw
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try

        End Sub

        ''' <summary>
        ''' Recupera o tipo sector por identificador.
        ''' </summary>
        ''' <param name="identificador"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarTipoSectorPorIdentificador(identificador As String) As Comon.Clases.TipoSector
            Return Prosegur.Genesis.AccesoDatos.Genesis.TipoSector.RecuperarTipoSectorPorIdentificador(identificador)
        End Function

        Public Shared Function RecuperarTipoSectorPorIdentificadorSector(identificador As String) As Comon.Clases.TipoSector
            Return Prosegur.Genesis.AccesoDatos.Genesis.TipoSector.RecuperarTipoSectorPorIdentificadorSector(identificador)
        End Function

        Public Shared Function RecuperarTipoSectorPorCodigos(codigoDelegacion As String, codigoPlanta As String, codigoSector As String) As Comon.Clases.TipoSector
            Return Prosegur.Genesis.AccesoDatos.Genesis.TipoSector.RecuperarTipoSectorPorCodigos(codigoDelegacion, codigoPlanta, codigoSector)
        End Function

        Public Shared Sub ValidarObrigatorios(identificadorFormulario As String, relacionConFormulario As String, tiposSectores As List(Of Comon.Clases.TipoSector))

            Dim erros As New System.Text.StringBuilder

            If String.IsNullOrEmpty(identificadorFormulario) Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("048_Identificador_Formulario_Obligatorio"))
            End If

            If String.IsNullOrEmpty(relacionConFormulario) Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("048_Relacion_formulario_Obligatorio"))
            End If

            If tiposSectores Is Nothing OrElse tiposSectores.Count = 0 Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("048_Tipo_Sector_Vazia"))
            End If

            For Each tipoSector In tiposSectores

                If String.IsNullOrEmpty(tipoSector.Identificador) Then
                    erros.AppendLine(Traduzir("048_Tipo_Sector_Obligatorio"))
                End If

                If String.IsNullOrEmpty(tipoSector.FechaHoraCreacion) Then
                    erros.AppendLine(Traduzir("048_Fecha_Creacion_Obligatorio"))
                End If

                If String.IsNullOrEmpty(tipoSector.UsuarioCreacion) Then
                    erros.AppendLine(Traduzir("048_Usuario_Creacion_Obligatorio"))
                End If

                If String.IsNullOrEmpty(tipoSector.FechaHoraModificacion) Then
                    erros.AppendLine(Traduzir("048_Fecha_Modificacion_Obligatorio"))
                End If

                If String.IsNullOrEmpty(tipoSector.UsuarioModificacion) Then
                    erros.AppendLine(Traduzir("048_Usuario_Modificacion_Obligatorio"))
                End If

            Next


            If erros.Length > 0 Then
                Throw New Excepcion.CampoObrigatorioException(erros.ToString)
            End If
        End Sub

        Public Shared Function ObtenerPorIdentificadores(identificadoresDelegaciones As List(Of String), identificadoresPlantas As List(Of String)) As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.TipoSector)
            Return Prosegur.Genesis.AccesoDatos.Genesis.TipoSector.ObtenerPorIdentificadores(identificadoresDelegaciones, identificadoresPlantas)
        End Function
    End Class
End Namespace
