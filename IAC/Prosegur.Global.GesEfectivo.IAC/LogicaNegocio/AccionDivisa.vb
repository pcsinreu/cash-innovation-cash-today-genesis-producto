Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Divisa
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' Classe AccionDivisa
''' </summary>
''' <remarks></remarks>
''' <history>
''' [octavio.piramo] 27/01/2009 Criado
''' </history>
Public Class AccionDivisa
    Implements ContractoServicio.IDivisa

    ''' <summary>
    ''' Obtém as divisas e suas denominaciones
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 28/01/2009 Criado
    ''' </history>
    Public Function getDenominacionesByDivisa(Peticion As ContractoServicio.Divisa.GetDenominacionesByDivisa.Peticion) As ContractoServicio.Divisa.GetDenominacionesByDivisa.Respuesta Implements ContractoServicio.IDivisa.getDenominacionesByDivisa

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Divisa.GetDenominacionesByDivisa.Respuesta

        Try
            ' obter divisas com as denominaciones
            objRespuesta.Divisas = AccesoDatos.Divisa.getDenominacionesByDivisaObterDivisa(Peticion)

            'Obtener codigos ajenos divisas
            Dim objAccionAjeno As New IAC.LogicaNegocio.AccionCodigoAjeno
            If objRespuesta.Divisas IsNot Nothing AndAlso objRespuesta.Divisas.Count > 0 Then

                For Each objDivisa As ContractoServicio.Divisa.Divisa In objRespuesta.Divisas

                    Dim peticionCodigoAjeno = New ContractoServicio.CodigoAjeno.GetCodigosAjenos.Peticion
                    peticionCodigoAjeno.CodigosAjeno = New ContractoServicio.CodigoAjeno.GetCodigosAjenos.CodigoAjeno
                    peticionCodigoAjeno.ParametrosPaginacion = New Comon.Paginacion.ParametrosPeticionPaginacion
                    peticionCodigoAjeno.ParametrosPaginacion.RegistrosPorPagina = 10
                    peticionCodigoAjeno.CodigosAjeno.CodTipoTablaGenesis = "GEPR_TDIVISA"
                    peticionCodigoAjeno.CodigosAjeno.OidTablaGenesis = objDivisa.Identificador

                    Dim CodAjenoRespuesta = objAccionAjeno.GetCodigosAjenos(peticionCodigoAjeno)

                    If CodAjenoRespuesta.CodigoError = 0 AndAlso CodAjenoRespuesta.EntidadCodigosAjenos IsNot Nothing AndAlso CodAjenoRespuesta.EntidadCodigosAjenos.Count > 0 Then

                        objDivisa.CodigosAjenos = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase

                        For Each objCodAjeno As ContractoServicio.CodigoAjeno.GetCodigosAjenos.CodigoAjenoRespuesta In CodAjenoRespuesta.EntidadCodigosAjenos(0).CodigosAjenos
                            Dim CodigoAjenoBase = New ContractoServicio.CodigoAjeno.CodigoAjenoBase
                            With CodigoAjenoBase
                                .BolActivo = objCodAjeno.BolActivo
                                .BolDefecto = objCodAjeno.BolDefecto
                                .BolMigrado = objCodAjeno.BolMigrado
                                .CodAjeno = objCodAjeno.CodAjeno
                                .CodIdentificador = objCodAjeno.CodIdentificador
                                .DesAjeno = objCodAjeno.DesAjeno
                                .OidCodigoAjeno = objCodAjeno.OidCodigoAjeno
                            End With

                            objDivisa.CodigosAjenos.Add(CodigoAjenoBase)
                        Next
                    End If
                Next

            End If

            ' preparar codigos e mensagens do respuesta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Obtém as divisas através do codigo, descrição. Caso nenhum seja informado, todos registros serão retornados.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 28/01/2009 Criado
    ''' </history>
    Public Function getDivisas(Peticion As ContractoServicio.Divisa.GetDivisas.Peticion) As ContractoServicio.Divisa.GetDivisas.Respuesta Implements ContractoServicio.IDivisa.getDivisas

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Divisa.GetDivisas.Respuesta

        Try

            ' obter divisas
            objRespuesta.Divisas = AccesoDatos.Divisa.getDivisas(Peticion)

            ' preparar codigos e mensagens do respuesta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Obtém as divisas através do codigo, descrição. Caso nenhum seja informado, todos registros serão retornados. Este método realiza a paginação dos registro na base de dados.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 06/02/2013 Criado
    ''' </history>
    Public Function GetDivisasPaginacion(Peticion As ContractoServicio.Divisa.GetDivisasPaginacion.Peticion) As ContractoServicio.Divisa.GetDivisasPaginacion.Respuesta Implements ContractoServicio.IDivisa.GetDivisasPaginacion

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Divisa.GetDivisasPaginacion.Respuesta

        Try

            ' obter divisas
            objRespuesta.Divisas = AccesoDatos.Divisa.GetDivisasPaginacion(Peticion, objRespuesta.ParametrosPaginacion)

            ' preparar codigos e mensagens do respuesta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Efetua inserção, alteração ou baixa no banco dos objetos recebidos
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 28/01/2009 Criado
    ''' </history>
    Public Function setDivisaDenominaciones(Peticion As ContractoServicio.Divisa.SetDivisasDenominaciones.Peticion) As ContractoServicio.Divisa.SetDivisasDenominaciones.Respuesta Implements ContractoServicio.IDivisa.setDivisaDenominaciones

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Divisa.SetDivisasDenominaciones.Respuesta
        objRespuesta.RespuestaDivisas = New ContractoServicio.Divisa.SetDivisasDenominaciones.RespuestaDivisaColeccion

        ' criar flag que controle se houve erro
        Dim temErro As Boolean = False

        ' percorrer as divisas existentes na peticion
        For Each objDivisa As ContractoServicio.Divisa.Divisa In Peticion.Divisas

            ' criar objeto respuesta divisa
            Dim objRespuestaDivisa As New ContractoServicio.Divisa.SetDivisasDenominaciones.RespuestaDivisa
            objRespuestaDivisa.CodigoIso = objDivisa.CodigoIso
            objRespuestaDivisa.Descripcion = objDivisa.Descripcion

            Try

                ' verificar se codigo divisa foi enviado
                If String.IsNullOrEmpty(objDivisa.CodigoIso) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("002_msg_DivisaCodigoVazio"))
                End If


                ' obter o oid da divisa
                Dim oidDivisa As String = AccesoDatos.Divisa.ObterOidDivisa(objDivisa.CodigoIso)

                'Verifica se o codigo de acesso da divisa existe.
                If AccesoDatos.Divisa.VerificarCodAccesoDivisaExiste(objDivisa.CodigoAccesoDivisa, oidDivisa) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("002_msg_CodigoAccesoExiste"))
                End If

                ' verifica se a divisa já existe
                If oidDivisa <> String.Empty Then

                    ' se a divisa não for vigente e se estiver sendo utilizada por alguma entidade
                    If Not objDivisa.Vigente AndAlso AccesoDatos.Divisa.VerificarEntidadesVigentesComDivisa(objDivisa.CodigoIso) Then
                        ' lançar erro avisando que existe entidades vigentes que utilizam a divisa
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("002_msg_DivisaComEntidadeVigente"))
                    Else
                        ' efetuar alteração da divisa e suas denominaciones
                        AccesoDatos.Divisa.ActualizarDivisa(objDivisa, Peticion.CodigoUsuario, oidDivisa)

                        If Peticion.CodigoAjeno IsNot Nothing AndAlso Peticion.CodigoAjeno.Count > 0 Then
                            'Grava os Codigos Ajenos
                            DefinirCodigoAjeno(oidDivisa, Peticion.CodigoAjeno, Peticion.CodigoUsuario, IAC.ContractoServicio.Constantes.COD_DIVISA, ContractoServicio.Constantes.COD_DIVISA)
                        End If
                        If Peticion.Divisas(0).Denominaciones IsNot Nothing AndAlso Peticion.Divisas(0).Denominaciones.Count > 0 Then
                            For Each denominacion In Peticion.Divisas(0).Denominaciones
                                If denominacion.CodigosAjenos IsNot Nothing AndAlso denominacion.CodigosAjenos.Count > 0 Then
                                    DefinirCodigoAjeno(denominacion.OidDenominacion, denominacion.CodigosAjenos, Peticion.CodigoUsuario, IAC.ContractoServicio.Constantes.COD_DENOMINACION, ContractoServicio.Constantes.COD_DENOMINACION)
                                End If
                            Next
                        End If
                    End If

                Else

                    ' verificar se a descrição divisa foi enviada
                    If String.IsNullOrEmpty(objDivisa.Descripcion) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("002_msg_DivisaDescripcionVazio"))
                    End If

                    ' efetuar inserção da divisa e suas denominaciones
                    AccesoDatos.Divisa.AltaDivisa(objDivisa, Peticion.CodigoUsuario)
                    If Peticion.CodigoAjeno IsNot Nothing Then
                        'Grava os Codigos Ajenos
                        DefinirCodigoAjeno(objDivisa.Identificador, Peticion.CodigoAjeno, Peticion.CodigoUsuario, IAC.ContractoServicio.Constantes.COD_DIVISA, ContractoServicio.Constantes.COD_DIVISA)
                    End If

                    If Peticion.Divisas(0).Denominaciones IsNot Nothing AndAlso Peticion.Divisas(0).Denominaciones.Count > 0 Then
                        For Each denominacion In Peticion.Divisas(0).Denominaciones
                            If denominacion.CodigosAjenos IsNot Nothing AndAlso denominacion.CodigosAjenos.Count > 0 Then
                                DefinirCodigoAjeno(denominacion.OidDenominacion, denominacion.CodigosAjenos, Peticion.CodigoUsuario, IAC.ContractoServicio.Constantes.COD_DENOMINACION, ContractoServicio.Constantes.COD_DENOMINACION)
                            End If
                        Next
                    End If
                End If


                objRespuestaDivisa.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuestaDivisa.MensajeError = String.Empty

            Catch ex As Excepcion.NegocioExcepcion

                'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
                objRespuestaDivisa.CodigoError = ex.Codigo
                objRespuestaDivisa.MensajeError = ex.Descricao
                temErro = False

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)

                'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
                objRespuestaDivisa.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuestaDivisa.MensajeError = ex.ToString()
                temErro = True

            Finally

                ' adicionar objeto respuesta
                objRespuesta.RespuestaDivisas.Add(objRespuestaDivisa)

            End Try

        Next

        ' caso tenha acontecido algum erro
        If temErro Then
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = Traduzir("002_msg_ErroCollecionDivisas")
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        Else
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty
        End If

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Verifica se o código denominacion existe na base de dados
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 27/01/2009 Criado
    ''' </history>
    Public Function VerificarCodigoDenominacion(Peticion As ContractoServicio.Divisa.VerificarCodigoDenominacion.Peticion) As ContractoServicio.Divisa.VerificarCodigoDenominacion.Respuesta Implements ContractoServicio.IDivisa.VerificarCodigoDenominacion

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Divisa.VerificarCodigoDenominacion.Respuesta

        Try

            ' executar verificação no banco
            objRespuesta.Existe = (AccesoDatos.Denominacion.VerificarCodigoDenominacion(Peticion) OrElse
                                   AccesoDatos.MedioPago.VerificarSeHayMediosPagosConElCodigo(Peticion.Codigo))

            ' preparar codigos e mensagens do respuesta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Verifica se o código divisa existe na base de dados
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 27/01/2009 Criado
    ''' </history>
    Public Function VerificarCodigoDivisa(Peticion As ContractoServicio.Divisa.VerificarCodigoDivisa.Peticion) As ContractoServicio.Divisa.VerificarCodigoDivisa.Respuesta Implements ContractoServicio.IDivisa.VerificarCodigoDivisa

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Divisa.VerificarCodigoDivisa.Respuesta

        Try

            ' executar verificação no banco
            objRespuesta.Existe = AccesoDatos.Divisa.VerificarCodigoDivisa(Peticion)

            ' preparar codigos e mensagens do respuesta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Verifica se já existe uma divisa com a descrição informada
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 27/01/2009 Criado
    ''' </history>
    Public Function VerificarDescripcionDivisa(Peticion As ContractoServicio.Divisa.VerificarDescripcionDivisa.Peticion) As ContractoServicio.Divisa.VerificarDescripcionDivisa.Respuesta Implements ContractoServicio.IDivisa.VerificarDescripcionDivisa

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Divisa.VerificarDescripcionDivisa.Respuesta

        Try

            ' executar verificação no banco
            objRespuesta.Existe = AccesoDatos.Divisa.VerificarDescripcionDivisa(Peticion)

            ' preparar codigos e mensagens do respuesta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Metodo Teste
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 04/02/2010 - Criado
    ''' </history>
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IDivisa.Test
        Dim objRespuesta As New ContractoServicio.Test.Respuesta

        Try

            AccesoDatos.Test.TestarConexao()

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = Traduzir("021_SemErro")

        Catch ex As Excepcion.NegocioExcepcion

            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao


        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta
    End Function

#Region "CODIGO AJENO"

    Private Shared Sub DefinirCodigoAjeno(oidDelegacion As String, codigoAjenoColeccion As IAC.ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase, codigoUsuario As String, codigoTablaAjeno As String, codigoTipoTabla As String)

        Dim objAccionAjeno As New IAC.LogicaNegocio.AccionCodigoAjeno

        If codigoAjenoColeccion.Count > 0 Then
            Dim objCodigoAjeno As New IAC.ContractoServicio.CodigoAjeno.SetCodigosAjenos.Peticion
            objCodigoAjeno.CodigosAjenos = New IAC.ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjenoColeccion


            For Each objItem In codigoAjenoColeccion
                Dim objItemCast As New IAC.ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjeno
                Dim codigotablaGenesis As String = String.Empty

                objItemCast.OidCodigoAjeno = objItem.OidCodigoAjeno
                objItemCast.BolActivo = objItem.BolActivo
                objItemCast.BolDefecto = objItem.BolDefecto
                objItemCast.CodAjeno = objItem.CodAjeno
                objItemCast.CodIdentificador = objItem.CodIdentificador
                objItemCast.DesAjeno = objItem.DesAjeno
                If String.IsNullOrEmpty(objItemCast.DesUsuarioCreacion) Then
                    objItemCast.DesUsuarioCreacion = codigoUsuario
                    objItemCast.GmtCreacion = DateTime.Now
                End If
                objItemCast.DesUsuarioModificacion = codigoUsuario
                objItemCast.GmtModificacion = DateTime.Now
                objItemCast.OidTablaGenesis = oidDelegacion
                codigotablaGenesis = codigoTablaAjeno

                objItemCast.CodTipoTablaGenesis = (From item In IAC.AccesoDatos.Constantes.MapeoEntidadesCodigoAjeno
                                                   Where item.CodTipoTablaGenesis = codigoTipoTabla
                                                   Select item.Entidade).FirstOrDefault()

                objCodigoAjeno.CodigosAjenos.Add(objItemCast)

            Next

            objAccionAjeno.SetCodigosAjenos(objCodigoAjeno)

        End If

    End Sub

#End Region
End Class