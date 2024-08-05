Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports System.Data

Namespace Genesis

    ''' <summary>
    ''' Clase Remessa.
    ''' </summary>
    Public Class Remesa

#Region " Procedure - Recuperar"

        Public Shared Function recuperarRemesas(identificadoresRemesas As List(Of String),
                                                identificadoresDocumentos As List(Of String),
                                                usuario As String) As ObservableCollection(Of Clases.Remesa)

            Return AccesoDatos.Genesis.Remesa.recuperarRemesas(identificadoresRemesas, identificadoresDocumentos, usuario)

        End Function

#End Region





#Region "[CONSULTAS]"

        ''' <summary>
        ''' Obtener las remesas
        ''' </summary>
        ''' <param name="Filtro"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerRemesas(filtro As Clases.Transferencias.Filtro) As ObservableCollection(Of Clases.Remesa)
            Return AccesoDatos.Genesis.Remesa.ObtenerRemesas(filtro)
        End Function

        Shared Function ObtenerUltimoDocumento(identificadorRemesa As String, identificadorDocumento As String, estadoDocumentoElemento As Enumeradores.EstadoDocumentoElemento, usuario As String) As Clases.Documento
            Dim objDocumento As New Clases.Documento

            Try

                If String.IsNullOrEmpty(identificadorRemesa) AndAlso String.IsNullOrEmpty(identificadorDocumento) Then
                    Throw New Exception(Traduzir("gen_msg_registroenproceso_invalido"))
                End If

                Dim identificador As String = AccesoDatos.Genesis.Remesa.ObtenerUltimoDocumento(identificadorRemesa, identificadorDocumento, estadoDocumentoElemento)

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

        Public Shared Function RecuperarRemesasPorIdentificadorCodigoExternos(Peticion As Contractos.Genesis.Remesa.RecuperarRemesasPorIdentificadorCodigoExternos.Peticion) As Contractos.Genesis.Remesa.RecuperarRemesasPorIdentificadorCodigoExternos.Respuesta

            Dim respuesta As New Contractos.Genesis.Remesa.RecuperarRemesasPorIdentificadorCodigoExternos.Respuesta

            Try

                respuesta.IdentificadoresRemesas = AccesoDatos.Genesis.Remesa.RecuperarRemesasPorIdentificadorCodigoExternos(Peticion.IdentificadoresExternos, Peticion.CodigosExternos)

            Catch ex As Excepcion.NegocioExcepcion
                respuesta.Mensajes.Add(ex.Message)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.Excepciones.Add(ex.Message)
            End Try

            Return respuesta

        End Function

#End Region

#Region "[INSERIR]"

        ''' <summary>
        ''' Grabar una nueva Remesa.
        ''' </summary>
        ''' <param name="objRemesa">Objeto Remesa preenchido.</param>
        ''' <remarks></remarks>
        Public Shared Sub GrabarRemesa_v2(ByRef objRemesa As Clases.Remesa)

            AccesoDatos.Genesis.Remesa.GrabarRemesa(objRemesa)
            For Each objBulto In objRemesa.Bultos
                objBulto.TrabajaPorBulto = objRemesa.TrabajaPorBulto
                LogicaNegocio.Genesis.Bulto.grabarBulto_v2(objBulto, objRemesa.Identificador)
            Next

        End Sub

        ''' <summary>
        ''' Grabar una nueva Remesa.
        ''' </summary>
        ''' <param name="objRemesa">Objeto Remesa preenchido.</param>
        ''' <remarks></remarks>
        Public Shared Sub GrabarRemesa(ByRef objRemesa As Clases.Remesa,
                             Optional ByRef _transacion As DbHelper.Transacao = Nothing)

            'Valida os campos obrigatórios do remessa antes de inserir.
            ValidarRemesa(objRemesa)

            ' Se não deu erro na validação então insere.
            If String.IsNullOrEmpty(objRemesa.Identificador) Then
                objRemesa.Identificador = System.Guid.NewGuid.ToString
            End If

            'Altera o estado da remesa para pendente
            objRemesa.Estado = Enumeradores.EstadoRemesa.Pendiente

            AccesoDatos.Genesis.Remesa.GrabarRemesa(objRemesa, _transacion)

            'Depois de inserir a remessa, verifica se essa remessa possui bultos
            'Se possuir então insere os bultos.
            If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then

                For Each objBulto In objRemesa.Bultos
                    objBulto.UsuarioModificacion = objRemesa.UsuarioModificacion
                    objBulto.IdentificadorDocumento = objRemesa.IdentificadorDocumento
                    If objBulto.CuentaSaldo Is Nothing Then
                        objBulto.CuentaSaldo = objRemesa.CuentaSaldo
                    End If
                    objBulto.TrabajaPorBulto = objRemesa.TrabajaPorBulto
                    LogicaNegocio.Genesis.Bulto.grabarBulto(objBulto, objRemesa.Identificador, _transacion)
                Next

            End If

        End Sub

        ''' <summary>
        ''' Grava a remesa o bulto e as parciais do documento
        ''' </summary>
        ''' <param name="objDocumento"></param>
        ''' <remarks></remarks>
        Public Shared Sub InserirRemesaBultoParcial(objDocumento As Clases.Documento)
            If objDocumento.Elemento IsNot Nothing Then
                Dim objRemesa = CType(objDocumento.Elemento, Clases.Remesa)
                objRemesa.IdentificadorDocumento = objDocumento.Identificador
                objRemesa.UsuarioModificacion = objDocumento.UsuarioModificacion
                If objRemesa.CuentaSaldo Is Nothing Then
                    objRemesa.CuentaSaldo = objDocumento.CuentaSaldoDestino
                End If
                LogicaNegocio.Genesis.Remesa.GrabarRemesa(objRemesa)
            End If
        End Sub

        Public Shared Sub InserirRemesaBultoParcialActa(objDocumento As Clases.Documento)
            If objDocumento.Elemento IsNot Nothing Then
                Dim objRemesa = CType(objDocumento.Elemento, Clases.Remesa)
                objRemesa.UsuarioModificacion = objDocumento.UsuarioModificacion

                If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
                    For Each objBulto In objRemesa.Bultos
                        If objBulto IsNot Nothing Then
                            If Not Util.RegistroExiste(Enumeradores.Tabela.Bulto, "OID_BULTO", objBulto.Identificador) Then
                                objBulto.UsuarioModificacion = objDocumento.UsuarioModificacion
                                objBulto.IdentificadorDocumento = objDocumento.Identificador
                                If objBulto.CuentaSaldo Is Nothing Then
                                    objBulto.CuentaSaldo = objDocumento.CuentaSaldoDestino
                                End If
                                LogicaNegocio.Genesis.Bulto.grabarBulto(objBulto, objRemesa.Identificador)

                            ElseIf objBulto.Parciales IsNot Nothing Then
                                For Each objParcial In objBulto.Parciales
                                    If Not Util.RegistroExiste(Enumeradores.Tabela.Parcial, "OID_PARCIAL", objParcial.Identificador) Then
                                        objParcial.UsuarioModificacion = objDocumento.UsuarioModificacion
                                        LogicaNegocio.Genesis.Parcial.GrabarParcial(objParcial, objRemesa.Identificador, objBulto.Identificador)
                                    End If
                                Next
                            End If
                        End If
                    Next
                End If
            End If
        End Sub

#End Region

#Region "[ACTUALIZAR]"

        ''' <summary>
        ''' Actualizar remesa.
        ''' </summary>
        ''' <param name="objRemesa"></param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarRemesa(objRemesa As Clases.Remesa)
            If Util.RegistroExiste(Enumeradores.Tabela.Remesa, "OID_REMESA", objRemesa.Identificador) Then

                AccesoDatos.Genesis.Remesa.ActualizarRemesa(objRemesa)

                'Se a remesa possui bultos, então atualiza os bultos
                If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
                    For Each objBulto In objRemesa.Bultos
                        objBulto.Cuenta = objRemesa.Cuenta
                        If objBulto.CuentaSaldo Is Nothing Then
                            objBulto.CuentaSaldo = objRemesa.CuentaSaldo
                        End If
                        objBulto.UsuarioModificacion = objRemesa.UsuarioModificacion
                        objBulto.IdentificadorDocumento = objRemesa.IdentificadorDocumento
                        LogicaNegocio.Genesis.Bulto.ActualizarBulto(objBulto, objRemesa.Identificador)
                    Next
                End If
            Else
                GrabarRemesa(objRemesa)
            End If
        End Sub

        ''' <summary>
        ''' Atualiza a remesa o bulto e paracial do documento
        ''' </summary>
        ''' <param name="documento"></param>
        ''' <remarks></remarks>
        Public Shared Sub AtualizarRemesaBultoParcial(ByRef documento As Clases.Documento)
            If documento.Elemento IsNot Nothing Then
                Dim remesa = CType(documento.Elemento, Clases.Remesa)
                'Atualiza a remesa/bulto e parcial
                remesa.IdentificadorDocumento = documento.Identificador
                remesa.UsuarioModificacion = documento.UsuarioModificacion
                If remesa.CuentaSaldo Is Nothing Then
                    remesa.CuentaSaldo = documento.CuentaSaldoDestino
                End If
                LogicaNegocio.Genesis.Remesa.ActualizarRemesa(remesa)
            End If
        End Sub

        ''' <summary>
        ''' Actualizar estado de la remesa.
        ''' </summary>
        ''' <param name="objRemesa"></param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarEstadoRemesa(objRemesa As Clases.Remesa)
            AccesoDatos.Genesis.Remesa.ActualizarEstadoRemesa(objRemesa)

            If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
                For Each objBulto In objRemesa.Bultos
                    objBulto.UsuarioModificacion = objRemesa.UsuarioModificacion
                    LogicaNegocio.Genesis.Bulto.ActualizarEstadoBulto(objBulto)
                Next
            End If
        End Sub
        ''' <summary>
        ''' Actualizar estado del abono de la remesa.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarEstadoAbonoRemesa(identificadorRemesa As String, codigoEstadoAbono As String, usuario As String,
                                                      soloCuandoNoEsNoAbonados As Boolean,
                                                      ByRef transaccion As DataBaseHelper.Transaccion)

            AccesoDatos.Genesis.Remesa.ActualizarEstadoAbonoRemesa(identificadorRemesa, codigoEstadoAbono, usuario, soloCuandoNoEsNoAbonados, transaccion)
            LogicaNegocio.Genesis.Bulto.ActualizarEstadoAbonoBulto(identificadorRemesa, codigoEstadoAbono, usuario, soloCuandoNoEsNoAbonados, transaccion)

        End Sub

        Public Shared Function HayDiferencaEnLaRemesa(identificadorRemesa As String) As Boolean
            Return AccesoDatos.Genesis.Remesa.HayDiferencaEnLaRemesa(identificadorRemesa)
        End Function

        ''' <summary>
        ''' Actualizar identificador documento de la Remesa
        ''' </summary>
        ''' <param name="objRemesa">Objeto remesa preenchido.</param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarIdentificadorDocumentoDeLaRemesa(objRemesa As Clases.Remesa)
            AccesoDatos.Genesis.Remesa.ActualizarIdentificadorDocumentoDeLaRemesa(objRemesa)
        End Sub

#End Region

#Region "[ELIMINAR]"

        ''' <summary>
        ''' Eliminar la remesa.
        ''' </summary>
        ''' <param name="objRemesa"></param>
        ''' <remarks></remarks>
        Public Shared Sub EliminarRemesa(objRemesa As Clases.Remesa)
            Try
                'Exclui a remesa do documento
                AccesoDatos.GenesisSaldos.DocumentoElemento.Eliminar(objRemesa.IdentificadorDocumento, objRemesa.Identificador)

                'Se a remesa tiver bulto, então exclui os bultos e parciais
                If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
                    For Each objBulto In objRemesa.Bultos
                        objBulto.IdentificadorDocumento = objRemesa.IdentificadorDocumento
                        LogicaNegocio.Genesis.Bulto.EliminarBulto(objBulto)
                    Next
                End If

                AccesoDatos.Genesis.Remesa.EliminarRemesa(objRemesa.Identificador)
            Catch ex As Exception
                Throw
            End Try

        End Sub

        ''' <summary>
        ''' Exclui a remesa bulto e parcial
        ''' </summary>
        ''' <param name="documento"></param>
        ''' <param name="documentoGravado"></param>
        ''' <remarks></remarks>
        Public Shared Sub ExcluirRemesaBultoParcial(documento As Clases.Documento, documentoGravado As Clases.Documento)
            If documento.Elemento IsNot Nothing AndAlso documentoGravado.Elemento IsNot Nothing Then
                Dim remesa = CType(documento.Elemento, Clases.Remesa).Clonar
                Dim remesaGravada = CType(documentoGravado.Elemento, Clases.Remesa)

                'Verifica se o identificador da remesa é diferente do gravado, então exclui a remesa gravada
                If remesaGravada.Identificador <> remesa.Identificador Then
                    remesaGravada.IdentificadorDocumento = documento.Identificador
                    LogicaNegocio.Genesis.Remesa.EliminarRemesa(remesaGravada)
                Else
                    'Verifica a remesa gravada tem bultos e eles existem na remesa que será gravada
                    If remesaGravada.Bultos IsNot Nothing AndAlso remesaGravada.Bultos.Count > 0 Then
                        If remesa.Bultos Is Nothing Then
                            remesa.Bultos = New ObservableCollection(Of Clases.Bulto)
                        End If

                        For Each bultoGravado In remesaGravada.Bultos
                            Dim bulto = remesa.Bultos.Where(Function(b) b.Identificador = bultoGravado.Identificador).FirstOrDefault
                            'se o bulto não existe na remesa então será excluido
                            If bulto Is Nothing Then
                                bulto.IdentificadorDocumento = documento.Identificador
                                LogicaNegocio.Genesis.Bulto.EliminarBulto(bultoGravado)
                            Else
                                'Verifica se as parciais do bulto existem no documento que será gravado.
                                If bultoGravado.Parciales IsNot Nothing AndAlso bultoGravado.Parciales.Count > 0 Then
                                    If bulto.Parciales Is Nothing Then
                                        bulto.Parciales = New ObservableCollection(Of Clases.Parcial)
                                    End If

                                    For Each parcialGravada In bultoGravado.Parciales
                                        If bulto.Parciales.Exists(Function(p) p.Identificador = parcialGravada.Identificador) Then
                                            LogicaNegocio.Genesis.Parcial.EliminarParcial(parcialGravada)
                                        End If
                                    Next
                                End If
                            End If
                        Next
                    End If
                End If
            End If
        End Sub

#End Region

#Region "[MEDOTOS]"

        ''' <summary>
        ''' Validar los campos obrigatórios de la Remesa.
        ''' </summary>
        ''' <param name="objRemesa">Objeto remessa preenchido.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValidarRemesa(objRemesa As Clases.Remesa)

            Dim erros As New System.Text.StringBuilder
            If objRemesa Is Nothing Then
                Throw New Excepcion.CampoObrigatorioException(Traduzir("028_remesa_vazio"))
            End If

            If String.IsNullOrEmpty(objRemesa.IdentificadorDocumento) Then
                erros.AppendLine(Traduzir("028_documento_obrigatorio"))
            End If

            If String.IsNullOrEmpty(objRemesa.Estado) Then
                erros.AppendLine(Traduzir("028_estado_obrigatorio"))
            End If

            If String.IsNullOrEmpty(objRemesa.UsuarioCreacion) Then
                erros.AppendLine(Traduzir("028_usuario_creacion_obrigatorio"))
            End If

            If String.IsNullOrEmpty(objRemesa.UsuarioModificacion) Then
                erros.AppendLine(Traduzir("028_usuario_modification_obrigatorio"))
            End If

            'Validar ATM
            If objRemesa.DatosATM IsNot Nothing AndAlso Not String.IsNullOrEmpty(objRemesa.DatosATM.CodigoCajero) AndAlso _
                objRemesa.Cuenta IsNot Nothing AndAlso objRemesa.Cuenta.PuntoServicio IsNot Nothing AndAlso _
                Not String.IsNullOrEmpty(objRemesa.Cuenta.PuntoServicio.Identificador) AndAlso objRemesa.Cuenta.Sector IsNot Nothing AndAlso _
                objRemesa.Cuenta.Sector.Delegacion IsNot Nothing AndAlso Not String.IsNullOrEmpty(objRemesa.Cuenta.Sector.Delegacion.Codigo) Then

                If Not Cajero.EsDatosATMValidos(objRemesa.DatosATM.CodigoCajero, objRemesa.Cuenta.PuntoServicio.Identificador, objRemesa.Cuenta.Sector.Delegacion.Codigo) Then
                    erros.AppendLine(Traduzir("028_codigo_atm_invalido"))
                End If

            End If

            If erros.Length > 0 Then
                Throw New Excepcion.CampoObrigatorioException(erros.ToString)
            End If
        End Sub

#End Region

    End Class

End Namespace


