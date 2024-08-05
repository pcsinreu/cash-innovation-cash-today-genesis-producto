Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports System.Data

Namespace Genesis

    ''' <summary>
    ''' Clase Bulto.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 12/09/2013 - Criado
    ''' </history>
    Public Class Bulto

#Region "[CONSULTAS]"

        ''' <summary>
        ''' Verifica si hay Bulto cerrado en la Remesa
        ''' </summary>
        ''' <param name="identificadorRemesa">identificadorRemesa</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function HayBultoCerradoPorRemesa(identificadorRemesa As String) As Boolean
            Return AccesoDatos.Genesis.Bulto.HayBultoCerradoPorRemesa(identificadorRemesa)
        End Function

        ''' <summary>
        ''' Conteo de bultos en un determiado estado
        ''' </summary>
        ''' <param name="estado">Estado del bulto</param>
        ''' <param name="identificadorRemesa"></param>
        ''' <param name="identificadorBulto"></param>
        ''' <param name="contarDiferencia">Si queire saber cuantos bultos distindos del estado</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function HayBultoEnEstado(estado As Enumeradores.EstadoBulto, _
                                       Optional identificadorRemesa As List(Of String) = Nothing, _
                                       Optional identificadorBulto As List(Of String) = Nothing, _
                                       Optional contarDiferencia As Boolean = False) As Boolean
            Return AccesoDatos.Genesis.Bulto.HayBultoEnEstado(estado, identificadorRemesa, identificadorBulto, contarDiferencia)
        End Function

        ''' <summary>
        ''' Verifica se existe bultos quem não estão cuadrados
        ''' </summary>
        ''' <param name="identificadorBulto"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Shared Function HayBultoSinCuadrar(identificadorBulto As List(Of String)) As Boolean
            Return AccesoDatos.Genesis.Bulto.HayBultoSinCuadrar(identificadorBulto)
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="identificadores"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerIdentificadorDocumento(identificadores As List(Of String), Optional obtenerOrigen As Boolean = True) As List(Of String)

            Dim _identificadoresDocumento As New List(Of String)

            Try

                If identificadores IsNot Nothing AndAlso identificadores.Count > 0 Then
                    _identificadoresDocumento = AccesoDatos.Genesis.Bulto.ObtenerIdentificadorDocumento(identificadores, obtenerOrigen)
                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            Return _identificadoresDocumento

        End Function

        Shared Function ObtenerUltimoDocumento(identificadorBulto As String, identificadorDocumento As String, estadoDocumentoElemento As Enumeradores.EstadoDocumentoElemento, usuario As String) As Clases.Documento
            Dim objDocumento As New Clases.Documento

            Try
                Dim identificador As String = AccesoDatos.Genesis.Bulto.ObtenerUltimoDocumento(identificadorBulto, identificadorDocumento, estadoDocumentoElemento)

                If Not String.IsNullOrEmpty(identificador) Then
                    objDocumento = LogicaNegocio.GenesisSaldos.Documento.recuperarDocumentoPorIdentificador(identificador, usuario, Nothing)
                Else
                    Return Nothing
                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            Return objDocumento
        End Function

#End Region

#Region "[INSERIR]"

        Public Shared Sub grabarBulto_v2(ByRef objBulto As Clases.Bulto, identificadorRemessa As String)

            Try

                'Altera o estado do bulto para pendente
                AccesoDatos.Genesis.Bulto.grabarBulto(objBulto, identificadorRemessa)

                'Insere o formato do bulto
                TipoFormato.InserirTipoFormatoPorBulto(identificadorRemessa, objBulto.Identificador, If(objBulto.TipoFormato IsNot Nothing, objBulto.TipoFormato.Identificador, String.Empty), objBulto.UsuarioModificacion)

                'Insere o servicio do bulto
                AccesoDatos.Genesis.TipoServicio.InserirTipoServicioPorBulto(identificadorRemessa, objBulto.Identificador, If(objBulto.TipoServicio IsNot Nothing, objBulto.TipoServicio.Identificador, String.Empty), objBulto.UsuarioModificacion)

                'Depois de inserir o bulto, verifica se esse bulto possui parcial
                'Se possuir então insere as parciais.
                If objBulto.Parciales IsNot Nothing AndAlso objBulto.Parciales.Count > 0 Then
                    For Each objParcial In objBulto.Parciales
                        objParcial.UsuarioModificacion = objBulto.UsuarioModificacion
                        LogicaNegocio.Genesis.Parcial.GrabarParcial(objParcial, identificadorRemessa, objBulto.Identificador)
                    Next
                End If
            Catch ex As Excepcion.CampoObrigatorioException
                Throw
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' Grabar un nuevo bulto
        ''' </summary>
        ''' <param name="objBulto"></param>
        ''' <param name="identificadorRemessa"></param>
        ''' <remarks></remarks>
        Public Shared Sub grabarBulto(ByRef objBulto As Clases.Bulto, identificadorRemessa As String,
                             Optional ByRef _transacion As DbHelper.Transacao = Nothing)

            'Valida os campos obrigatórios do bulto antes de inserir.
            ValidarBulto(objBulto)

            Try
                'Se não deu erro na validação então insere.
                If String.IsNullOrEmpty(objBulto.Identificador) Then
                    objBulto.Identificador = System.Guid.NewGuid.ToString
                End If

                'Altera o estado do bulto para pendente
                objBulto.Estado = Enumeradores.EstadoBulto.Cerrado
                AccesoDatos.Genesis.Bulto.grabarBulto(objBulto, identificadorRemessa, _transacion)

                'Insere o formato do bulto
                TipoFormato.InserirTipoFormatoPorBulto(identificadorRemessa, objBulto.Identificador, If(objBulto.TipoFormato IsNot Nothing, objBulto.TipoFormato.Identificador, String.Empty), objBulto.UsuarioModificacion, _transacion)

                'Insere o servicio do bulto
                AccesoDatos.Genesis.TipoServicio.InserirTipoServicioPorBulto(identificadorRemessa, objBulto.Identificador, If(objBulto.TipoServicio IsNot Nothing, objBulto.TipoServicio.Identificador, String.Empty), objBulto.UsuarioModificacion, _transacion)

                'Depois de inserir o bulto, verifica se esse bulto possui parcial
                'Se possuir então insere as parciais.
                If objBulto.Parciales IsNot Nothing AndAlso objBulto.Parciales.Count > 0 Then
                    For Each objParcial In objBulto.Parciales
                        objParcial.UsuarioModificacion = objBulto.UsuarioModificacion
                        LogicaNegocio.Genesis.Parcial.GrabarParcial(objParcial, identificadorRemessa, objBulto.Identificador, _transacion)
                    Next
                End If
            Catch ex As Excepcion.CampoObrigatorioException
                Throw
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try
        End Sub

#End Region

#Region "[ACTUALIZAR]"

        ''' <summary>
        ''' Actualizar Bulto.
        ''' </summary>
        ''' <param name="objBulto"></param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarBulto(objBulto As Clases.Bulto, identificadorRemesa As String)

            'Se existe atualiza, senão insere.
            If Util.RegistroExiste(Enumeradores.Tabela.Bulto, "OID_BULTO", objBulto.Identificador) Then
                AccesoDatos.Genesis.Bulto.ActualizarBulto(objBulto)

                'Exclui e inclui o Formato para atualizar
                If objBulto.TipoFormato IsNot Nothing Then
                    TipoFormato.ExcluirTipoFormatoPorBulto(objBulto.Identificador, objBulto.TipoFormato.Identificador)
                    TipoFormato.InserirTipoFormatoPorBulto(identificadorRemesa, objBulto.Identificador, objBulto.TipoFormato.Identificador, objBulto.UsuarioModificacion)
                End If

                'Exclui e inclui o Servicio para atualizar
                If objBulto.TipoServicio IsNot Nothing Then
                    AccesoDatos.Genesis.TipoServicio.ExcluirTipoServicioPorBulto(objBulto.Identificador, objBulto.TipoServicio.Identificador)
                    AccesoDatos.Genesis.TipoServicio.InserirTipoServicioPorBulto(identificadorRemesa, objBulto.Identificador, objBulto.TipoServicio.Identificador, objBulto.UsuarioModificacion)
                End If

                'Atualiza as parciais 
                If objBulto.Parciales IsNot Nothing AndAlso objBulto.Parciales.Count > 0 Then
                    For Each objParcial In objBulto.Parciales
                        objParcial.UsuarioModificacion = objBulto.UsuarioModificacion
                        LogicaNegocio.Genesis.Parcial.ActualizarParcial(objParcial, identificadorRemesa, objBulto.Identificador)
                    Next
                End If
            Else
                grabarBulto(objBulto, identificadorRemesa)
            End If

        End Sub

        ''' <summary>
        ''' Actualizar el estado del bulto.
        ''' </summary>
        ''' <param name="objBulto">Objeto Bulto.</param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarEstadoBulto(objBulto As Clases.Bulto)
            AccesoDatos.Genesis.Bulto.ActualizarEstadoBulto(objBulto)
            If objBulto.Parciales IsNot Nothing AndAlso objBulto.Parciales.Count > 0 Then
                For Each objParcial In objBulto.Parciales
                    objParcial.UsuarioModificacion = objBulto.UsuarioModificacion
                    LogicaNegocio.Genesis.Parcial.ActualizarEstadoParcial(objParcial)
                Next
            End If
        End Sub

        ''' <summary>
        ''' Actualizar el estado abono del bulto.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarEstadoAbonoBulto(identificadorRemesa As String, codigoEstadoAbono As String, usuario As String,
                                                     soloCuandoNoEsNoAbonados As Boolean,
                                                     ByRef transaccion As DataBaseHelper.Transaccion)
            AccesoDatos.Genesis.Bulto.ActualizarEstadoAbonoBulto(identificadorRemesa, codigoEstadoAbono, usuario, soloCuandoNoEsNoAbonados, transaccion)
        End Sub

        ''' <summary>
        ''' Actualiza IdentificadorDocumento del Bulto.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarIdentificadorDocumentoDelBulto(objBulto As Clases.Bulto)
            AccesoDatos.Genesis.Bulto.ActualizarIdentificadorDocumentoDelBulto(objBulto)
        End Sub

        ''' <summary>
        ''' Actualizar BOL_CUADRADO en Saldos.
        ''' </summary>
        ''' <param name="precintosBultos"></param>
        ''' <param name="codigoUsuario"></param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarCuadrado(precintosBultos As List(Of String), codigoUsuario As String)

            AccesoDatos.Genesis.Bulto.ActualizarCuadrado(precintosBultos, codigoUsuario)

        End Sub

#End Region

#Region "[ELIMINAR]"

        ''' <summary>
        ''' Eliminar Bulto.
        ''' </summary>
        ''' <param name="objBulto"></param>
        ''' <remarks></remarks>
        Public Shared Sub EliminarBulto(objBulto As Clases.Bulto)

            'Exclui o bulto do documento
            AccesoDatos.GenesisSaldos.DocumentoElemento.Eliminar(objBulto.IdentificadorDocumento, String.Empty, objBulto.Identificador)

            'Excluir as parciais
            If objBulto.Parciales IsNot Nothing AndAlso objBulto.Parciales.Count > 0 Then
                For Each objParcial In objBulto.Parciales
                    LogicaNegocio.Genesis.Parcial.EliminarParcial(objParcial)
                Next
            End If

            'Exclui o formato do bulto
            If objBulto.TipoFormato IsNot Nothing Then
                TipoFormato.ExcluirTipoFormatoPorBulto(objBulto.Identificador, objBulto.TipoFormato.Identificador)
            End If

            'Exclui o Servicio do bulto
            If objBulto.TipoServicio IsNot Nothing Then
                AccesoDatos.Genesis.TipoServicio.ExcluirTipoServicioPorBulto(objBulto.Identificador, objBulto.TipoServicio.Identificador)
            End If

            'Exclui o bulto
            AccesoDatos.Genesis.Bulto.EliminarBulto(objBulto.Identificador)
        End Sub

#End Region

#Region "[MEDOTOS]"

        ''' <summary>
        ''' Valida os campos obrigatórios do bulto.
        ''' </summary>
        ''' <param name="objBulto">Objeto bulto preenchido.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValidarBulto(objBulto As Clases.Bulto)

            Dim erros As New System.Text.StringBuilder
            If objBulto Is Nothing Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("028_bulto_vazio"))
            End If

            If objBulto.Cuenta Is Nothing OrElse String.IsNullOrEmpty(objBulto.Cuenta.Identificador) Then
                erros.AppendLine(Traduzir("028_cuenta_obrigatorio"))
            End If

            'If objBulto.Cuenta.Sector Is Nothing OrElse String.IsNullOrEmpty(objBulto.Cuenta.Identificador) Then
            '    erros.AppendLine(Traduzir("028_sector_obrigatorio"))
            'End If

            If objBulto.Precintos Is Nothing OrElse objBulto.Precintos.Count = 0 OrElse String.IsNullOrEmpty(objBulto.Precintos(0)) Then
                erros.AppendLine(Traduzir("028_precinto_obrigatorio"))
            End If

            If String.IsNullOrEmpty(objBulto.UsuarioCreacion) Then
                erros.AppendLine(Traduzir("028_usuario_creacion_obrigatorio"))
            End If

            If String.IsNullOrEmpty(objBulto.UsuarioModificacion) Then
                erros.AppendLine(Traduzir("028_usuario_modification_obrigatorio"))
            End If

            If erros.Length > 0 Then
                Throw New Excepcion.CampoObrigatorioException(erros.ToString)
            End If
        End Sub

        Public Shared Function RomperPrecintosSaldosSalidas(Peticion As Contractos.Genesis.Bulto.RomperPrecintosSaldosSalidas.Peticion) As Contractos.Genesis.Bulto.RomperPrecintosSaldosSalidas.Respuesta

            Dim respuesta As New ContractoServicio.Contractos.Genesis.Bulto.RomperPrecintosSaldosSalidas.Respuesta

            Try

                AccesoDatos.Genesis.Bulto.RomperPrecintosSaldosSalidas(Peticion.Bultos, Peticion.ReenvioCambioPrecintoLegado, Peticion.ReenvioCambioPrecintoSol, Peticion.TrabajaPorBulto, Peticion.CodigoPlanta, Peticion.CodigoDelegacion, Peticion.CodigoUsuario)

            Catch ex As Excepcion.NegocioExcepcion
                respuesta.Mensajes.Add(ex.Message)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.Excepciones.Add(ex.Message)
            End Try

            Return respuesta

        End Function

#End Region

    End Class

End Namespace


