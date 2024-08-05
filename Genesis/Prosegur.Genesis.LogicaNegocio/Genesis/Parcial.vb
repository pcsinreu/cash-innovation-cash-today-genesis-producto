Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports System.Data
Imports Prosegur.Genesis.Comon.Enumeradores

Namespace Genesis

    ''' <summary>
    ''' Clase Parcial
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 12/09/2013 - Criado
    ''' </history>
    Public Class Parcial

#Region "[CONSULTAS]"

#End Region

#Region "[INSERIR]"

        ''' <summary>
        ''' Grabar una nueva parcial
        ''' </summary>
        ''' <param name="objParcial"></param>
        ''' <param name="identificadorRemessa"></param>
        ''' <param name="identificadorBulto"></param>
        ''' <remarks></remarks>
        Public Shared Sub GrabarParcial(ByRef objParcial As Clases.Parcial, identificadorRemessa As String, identificadorBulto As String,
                             Optional ByRef _transacion As DbHelper.Transacao = Nothing)

            'Valida os campos obrigatórios da parcial antes de inserir.
            ValidarParcial(objParcial)

            Try

                If String.IsNullOrEmpty(objParcial.Identificador) Then objParcial.Identificador = System.Guid.NewGuid.ToString

                'altera o estado da parcial para pendente
                objParcial.Estado = Enumeradores.EstadoParcial.Pendiente

                'Se não deu erro na validação então insere.
                AccesoDatos.Genesis.Parcial.GrabarParcial(objParcial, identificadorBulto, _transacion)

                'Insere o formato da parcial
                If objParcial.TipoFormato IsNot Nothing Then
                    TipoFormato.InserirTipoFormatoPorParcial(identificadorRemessa, identificadorBulto, objParcial.Identificador, objParcial.TipoFormato.Identificador, objParcial.UsuarioModificacion, _transacion)
                End If

            Catch ex As Exception
                Throw
            End Try

        End Sub

#End Region

#Region "[ACTUALIZAR]"

        ''' <summary>
        ''' Actualizar Parcial.
        ''' </summary>
        ''' <param name="objParcial"></param>
        ''' <param name="identificadorRemessa"></param>
        ''' <param name="identificadorBulto"></param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarParcial(objParcial As Clases.Parcial, identificadorRemessa As String, identificadorBulto As String)
            'Se existe atualiza, senão insere.
            If Util.RegistroExiste(Enumeradores.Tabela.Parcial, "OID_PARCIAL", objParcial.Identificador) Then

                AccesoDatos.Genesis.Parcial.ActualizarParcial(objParcial)

                'Exclui e inclui o Formato para atualizar
                If objParcial.TipoFormato IsNot Nothing Then
                    TipoFormato.ExcluirTipoFormatoPorParcial(objParcial.Identificador, objParcial.TipoFormato.Identificador)
                    TipoFormato.InserirTipoFormatoPorParcial(identificadorRemessa, identificadorBulto, objParcial.Identificador, objParcial.TipoFormato.Identificador, objParcial.UsuarioModificacion)
                End If

            Else
                GrabarParcial(objParcial, identificadorRemessa, identificadorBulto)
            End If
        End Sub

        ''' <summary>
        ''' Actualizar estado de la parcial.
        ''' </summary>
        ''' <param name="objParcial"></param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarEstadoParcial(objParcial As Clases.Parcial)
            AccesoDatos.Genesis.Parcial.ActualizarEstadoParcial(objParcial)
        End Sub

#End Region

#Region "[ELIMINAR]"

        ''' <summary>
        ''' Eliminar Parcial
        ''' </summary>
        ''' <param name="parcial"></param>
        ''' <remarks></remarks>
        Public Shared Sub EliminarParcial(parcial As Clases.Parcial)
            If parcial.TipoFormato IsNot Nothing Then
                TipoFormato.ExcluirTipoFormatoPorParcial(parcial.Identificador, parcial.TipoFormato.Identificador)
            End If
            AccesoDatos.Genesis.Parcial.EliminarParcial(parcial.Identificador)
        End Sub

        ''' <summary>
        ''' Exclui as parciais.
        ''' </summary>
        ''' <param name="documento"></param>
        ''' <remarks></remarks>
        Public Shared Sub EliminarParcialActa(documento As Clases.Documento)
            If documento.Elemento IsNot Nothing Then
                Dim remesa = CType(documento.Elemento, Clases.Remesa)

                'Verifica se a remesa tem bultos se tiver anular os bultos
                If remesa.Bultos IsNot Nothing Then
                    'Verifica se a a parcial será eliminada
                    For Each bulto In remesa.Bultos.Where(Function(b) b.Parciales IsNot Nothing)
                        For Each parcical In bulto.Parciales.Where(Function(p) p.Estado = EstadoParcial.Eliminado)
                            LogicaNegocio.Genesis.Parcial.EliminarParcial(parcical)
                        Next

                        'Exclui as parciais do bulto.
                        bulto.Parciales.RemoveAll(Function(p) p.Estado = EstadoParcial.Eliminado)
                    Next
                End If
            End If
        End Sub

#End Region

#Region "[MEDOTOS]"

        ''' <summary>
        ''' Valida os campos obrigatórios da parcial.
        ''' </summary>
        ''' <param name="objParcial">Objeto parcial preenchido.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValidarParcial(objParcial As Clases.Parcial)
            Dim erros As New System.Text.StringBuilder
            If objParcial Is Nothing Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("028_parcial_vazio"))
            End If

            If String.IsNullOrEmpty(objParcial.Estado) Then
                erros.AppendLine(Traduzir("028_estado_obrigatorio"))
            End If

            If String.IsNullOrEmpty(objParcial.UsuarioCreacion) Then
                erros.AppendLine(Traduzir("028_usuario_creacion_obrigatorio"))
            End If

            If String.IsNullOrEmpty(objParcial.UsuarioModificacion) Then
                erros.AppendLine(Traduzir("028_usuario_modification_obrigatorio"))
            End If

            If erros.Length > 0 Then
                Throw New Excepcion.CampoObrigatorioException(erros.ToString)
            End If

        End Sub

#End Region


        ' ''' <summary>
        ' ''' Verifica se existe historico de valores para a parcial
        ' ''' </summary>
        ' ''' <param name="IdentificadorParcial">Identificador da Parcial</param>
        ' ''' <returns></returns>
        ' ''' <remarks></remarks>
        'Public Shared Function HayHistoricoValores(IdentificadorParcial As String) As Boolean

        '    Try

        '        Return AccesoDatos.Genesis.Parcial.ParcialHayHistoricoValores(IdentificadorParcial)

        '    Catch ex As Exception
        '        Throw
        '    End Try

        '    Return False

        'End Function
    End Class

End Namespace
