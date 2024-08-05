Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Canal
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.AccesoDatos
Imports Prosegur.Genesis

Public Class AccionUtilidad
    Implements ContractoServicio.IUtilidad

    ''' <summary>
    ''' Função busca todas as maquinas cadastradas.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 27/01/2009 Created
    ''' </history>  
    Public Function GetComboMaquinas() As ContractoServicio.Utilidad.GetComboMaquinas.Respuesta Implements ContractoServicio.IUtilidad.GetComboMaquinas

        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboMaquinas.Respuesta

        Try

            objRespuesta.Descripcion = AccesoDatos.Maquina.GetComboMaquinas()
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Função busca todas os paises.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 13/02/2012 Alterado
    ''' </history>
    Public Function GetComboPais() As ContractoServicio.Utilidad.GetComboPais.Respuesta Implements ContractoServicio.IUtilidad.GetComboPais

        Dim ObjRespuesta As New ContractoServicio.Utilidad.GetComboPais.Respuesta

        Try
            ObjRespuesta.Pais = AccesoDatos.Pais.GetComboPais()
            ObjRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            ObjRespuesta.MensajeError = String.Empty
        Catch ex As Excepcion.NegocioExcepcion
            ObjRespuesta.CodigoError = ex.Codigo
            ObjRespuesta.MensajeError = ex.Descricao
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ObjRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            ObjRespuesta.MensajeError = ex.ToString()
            ObjRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        End Try

        Return ObjRespuesta
    End Function

    ''' <summary>
    ''' Obtém todos os formatos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    Public Function GetComboFormatos() As ContractoServicio.Utilidad.GetComboFormatos.Respuesta Implements ContractoServicio.IUtilidad.GetComboFormatos

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboFormatos.Respuesta

        Try
            ' obter formatos
            objRespuesta.Formatos = AccesoDatos.Formato.GetComboFormatos()
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Obtém todas as mascaras
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    Public Function GetComboMascaras(objPeticion As ContractoServicio.Utilidad.GetComboMascaras.Peticion) As ContractoServicio.Utilidad.GetComboMascaras.Respuesta Implements ContractoServicio.IUtilidad.GetComboMascaras

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboMascaras.Respuesta

        Try

            ' obter as mascaras
            objRespuesta.Mascaras = AccesoDatos.Mascara.GetComboMascaras(objPeticion)

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Obtém todos os algoritmos
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    Public Function GetComboAlgoritmos(objPeticion As ContractoServicio.Utilidad.GetComboAlgoritmos.Peticion) As ContractoServicio.Utilidad.GetComboAlgoritmos.Respuesta Implements ContractoServicio.IUtilidad.GetComboAlgoritmos

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboAlgoritmos.Respuesta

        Try

            ' obter terminos
            objRespuesta.Algoritmos = AccesoDatos.Algoritmo.GetComboAlgoritmos(objPeticion)

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Obtém todos os terminos
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    Public Function GetComboTerminosIAC(objPeticion As ContractoServicio.Utilidad.GetComboTerminosIAC.Peticion) As ContractoServicio.Utilidad.GetComboTerminosIAC.Respuesta Implements ContractoServicio.IUtilidad.GetComboTerminosIAC

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboTerminosIAC.Respuesta

        Try

            ' obter terminos
            objRespuesta.Terminos = AccesoDatos.TerminoIac.GetComboTerminosIAC(objPeticion)

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Obtém os medio pago pelo tipo e divisa
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Public Function GetComboMediosPagoByTipoAndDivisa(objPeticion As ContractoServicio.Utilidad.GetComboMediosPagoByTipoAndDivisa.Peticion) As ContractoServicio.Utilidad.GetComboMediosPagoByTipoAndDivisa.Respuesta Implements ContractoServicio.IUtilidad.GetComboMediosPagoByTipoAndDivisa

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboMediosPagoByTipoAndDivisa.Respuesta

        Try

            ' verificar se codigo tipo medio pago foi enviado
            If String.IsNullOrEmpty(objPeticion.CodigoTipoMedioPago) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("012_msg_codtipo_medio"))
            End If

            ' verificar se a descrição existe
            objRespuesta.MediosPago = AccesoDatos.MedioPago.GetComboMediosPagoByTipoAndDivisa(objPeticion)

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
    ''' Obtém os tipos medio pago através de um código divisa
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Public Function GetComboTiposMedioPagoByDivisa(objPeticion As ContractoServicio.Utilidad.GetComboTiposMedioPagoByDivisa.Peticion) As ContractoServicio.Utilidad.GetComboTiposMedioPagoByDivisa.Respuesta Implements ContractoServicio.IUtilidad.GetComboTiposMedioPagoByDivisa

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboTiposMedioPagoByDivisa.Respuesta

        Try

            ' verificar se a descrição existe
            objRespuesta.TiposMedioPago = AccesoDatos.TipoMedioPago.GetComboTiposMediosPagoByDivisa(objPeticion)

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
    ''' Obtém as divisas vigentes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Public Function GetComboDivisas() As ContractoServicio.Utilidad.GetComboDivisas.Respuesta Implements ContractoServicio.IUtilidad.GetComboDivisas

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboDivisas.Respuesta

        Try

            ' verificar se a descrição existe
            objRespuesta.Divisas = AccesoDatos.Divisa.GetComboDivisas()

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
    ''' Obtém combo divisa através do codigo tipo medio pago
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Public Function GetComboDivisasByTipoMedioPago(objPeticion As ContractoServicio.Utilidad.GetComboDivisasByTipoMedioPago.Peticion) As ContractoServicio.Utilidad.GetComboDivisasByTipoMedioPago.Respuesta Implements ContractoServicio.IUtilidad.GetComboDivisasByTipoMedioPago

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboDivisasByTipoMedioPago.Respuesta

        Try

            ' verificar se a descrição existe
            objRespuesta.Divisas = AccesoDatos.Divisa.getComboDivisasByTipoMedioPago(objPeticion)

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
    ''' Obtém divisas com tipo médio pago e medio pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 06/02/2009 Criado
    ''' </history>
    Public Function GetDivisasMedioPago() As ContractoServicio.Utilidad.GetDivisasMedioPago.Respuesta Implements ContractoServicio.IUtilidad.GetDivisasMedioPago

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Utilidad.GetDivisasMedioPago.Respuesta

        Try

            ' obter divisas
            objRespuesta.Divisas = AccesoDatos.Divisa.GetDivisaMedioPago()

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
    ''' Obtém a lista de clientes
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Public Function GetComboClientes(objPeticion As ContractoServicio.Utilidad.GetComboClientes.Peticion) As ContractoServicio.Utilidad.GetComboClientes.Respuesta Implements ContractoServicio.IUtilidad.GetComboClientes

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboClientes.Respuesta

        Try

            ' obter divisas
            objRespuesta.Clientes = AccesoDatos.Cliente.getComboClientes(objPeticion, objRespuesta.ParametrosPaginacion)

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
    ''' Obtém os tipos medio pago 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 25/02/2009 Criado
    ''' </history>
    Public Function GetComboTiposMedioPago() As ContractoServicio.Utilidad.GetComboTiposMedioPago.Respuesta Implements ContractoServicio.IUtilidad.GetComboTiposMedioPago
        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboTiposMedioPago.Respuesta

        Try

            ' verificar se a descrição existe
            objRespuesta.TiposMedioPago = AccesoDatos.TipoMedioPago.GetComboTiposMediosPago

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
    ''' Obtiene los tipos de cuentas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [achimuris] 03/05/2021 creado
    ''' </history>
    Public Function GetComboTiposCuentas() As ContractoServicio.Utilidad.GetComboTiposCuenta.Respuesta Implements ContractoServicio.IUtilidad.GetComboTiposCuenta
        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboTiposCuenta.Respuesta

        Try

            ' verificar se a descrição existe
            objRespuesta.TiposDeCuentas = AccesoDatos.TipoCuenta.GetComboTiposCuentas

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
    ''' Obtem os subclientes.
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Public Function GetComboSubclientesByCliente(objPeticion As ContractoServicio.Utilidad.GetComboSubclientesByCliente.Peticion) As ContractoServicio.Utilidad.GetComboSubclientesByCliente.Respuesta Implements ContractoServicio.IUtilidad.GetComboSubclientesByCliente

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboSubclientesByCliente.Respuesta

        Try

            ' verificar se pelo menos um codigo do cliente foi enviado
            If objPeticion.CodigosClientes Is Nothing OrElse objPeticion.CodigosClientes.Count = 0 OrElse String.IsNullOrEmpty(objPeticion.CodigosClientes(0)) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("012_msg_codCliente"))
            End If

            'chama o metodo getcombosubclientesbycliente no acesso dados
            objRespuesta.Clientes = AccesoDatos.SubCliente.GetComboSubclientesByCliente(objPeticion)

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
    ''' Obtem os punto de servicio.
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Public Function GetComboPuntosServiciosBySubcliente(objPeticion As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Peticion) As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Respuesta Implements ContractoServicio.IUtilidad.GetComboPuntosServiciosByClienteSubcliente


        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Respuesta

        Try

            ' verificar se codigo do cliente foi enviado
            If String.IsNullOrEmpty(objPeticion.CodigoCliente) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("012_msg_codCliente"))
            End If

            'chama o metodo getcombosubclientesbycliente no acesso dados
            objRespuesta.Cliente = AccesoDatos.PuntoServicio.GetComboPuntosServiciosBySubcliente(objPeticion)

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

    Public Function GetComboPuntosServiciosByClientesSubClientes(objPeticion As ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Peticion) As ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Respuesta Implements ContractoServicio.IUtilidad.GetComboPuntosServiciosByClientesSubclientes

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Respuesta

        Try

            ' verificar se codigo do cliente foi enviado
            If objPeticion.Clientes Is Nothing OrElse objPeticion.Clientes.Count = 0 Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("012_msg_codCliente"))
            End If

            'chama o metodo getcombosubclientesbycliente no acesso dados
            objRespuesta.Clientes = AccesoDatos.PuntoServicio.GetComboPuntosServiciosByClientesSubClientes(objPeticion)

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
    ''' Obtem todos os canais vigentes.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Public Function GetComboCanales() As ContractoServicio.Utilidad.GetComboCanales.Respuesta Implements ContractoServicio.IUtilidad.GetComboCanales

        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboCanales.Respuesta

        Try

            'chama o metodo getcombosubclientesbycliente no acesso dados
            objRespuesta.Canales = AccesoDatos.Canal.GetComboCanales()

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
    ''' Obtem os subcanais dos canais informados.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Public Function GetComboSubcanalesByCanal(objPeticion As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Peticion) As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Respuesta Implements ContractoServicio.IUtilidad.GetComboSubcanalesByCanal

        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Respuesta

        Try


            'chama o metodo getcombosubclientesbycliente no acesso dados
            objRespuesta.Canales = AccesoDatos.SubCanal.GetComboSubcanalesByCanal(objPeticion)

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
    ''' Obtem todos os produtos vigentes.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' [anselmo.gois] 17/03/2009 - Criado
    Public Function GetComboProductos() As ContractoServicio.Utilidad.GetComboProductos.Respuesta Implements ContractoServicio.IUtilidad.GetComboProductos

        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboProductos.Respuesta

        Try

            'chama o metodo getcombosubclientesbycliente no acesso dados
            objRespuesta.Productos = AccesoDatos.Producto.GetComboProductos()

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
    ''' Obtém os códigos de todas as delegacoes utilizando distinct e order by.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 13/03/2009 Criado
    ''' </history>
    Public Function GetComboDelegaciones() As ContractoServicio.Utilidad.GetComboDelegaciones.Respuesta Implements ContractoServicio.IUtilidad.GetComboDelegaciones

        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboDelegaciones.Respuesta

        Try

            'chama o metodo getcombosubclientesbycliente no acesso dados
            objRespuesta.Delegaciones = AccesoDatos.Delegacion.ObterTodasDelegacoes

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
    ''' Obtém todas as delegacoes por pais utilizando distinct e order by.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gccosta] 09/04/2012 Criado
    ''' </history>
    Public Function GetComboDelegacionesPorPais(objPeticion As ContractoServicio.Utilidad.GetComboDelegacionesPorPais.Peticion) As ContractoServicio.Utilidad.GetComboDelegacionesPorPais.Respuesta Implements ContractoServicio.IUtilidad.GetComboDelegacionesPorPais
        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboDelegacionesPorPais.Respuesta

        Try

            Dim DelegacionVO As DelegacionVO = Nothing

            DelegacionVO = Delegacion.ObterOIDDelegacionyCodigoPais(objPeticion.CodDelegacion)
            Dim CodPais As String = If(DelegacionVO IsNot Nothing, DelegacionVO.CodigoPais, String.Empty)

            If Not String.IsNullOrEmpty(CodPais) Then

                'chama o metodo getcombosubclientesbycliente no acesso dados
                objRespuesta.Delegaciones = AccesoDatos.Delegacion.ObterDatosDelegacionPorPais(CodPais)

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
    ''' Obtem todas as modalidades de recuento vigentes.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois]      17/03/2009 - Criado
    ''' </history>
    Public Function GetComboModalidadesRecuento() As ContractoServicio.Utilidad.GetComboModalidadesRecuento.Respuesta Implements ContractoServicio.IUtilidad.GetComboModalidadesRecuento

        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboModalidadesRecuento.Respuesta

        Try

            'chama o metodo GetComboModalidadeRecuento no acesso dados
            objRespuesta.ModalidadesRecuento = AccesoDatos.TiposProcesado.GetComboModalidadeRecuento()

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
    ''' obtem todas as informações adicionais vigentes.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 17/03/2009 - Criado
    ''' </history>
    Public Function GetComboInformacionAdicional() As ContractoServicio.Utilidad.GetComboInformacionAdicional.Respuesta Implements ContractoServicio.IUtilidad.GetComboInformacionAdicional

        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboInformacionAdicional.Respuesta

        Try

            'chama o metodo getcomboInformacionAdicional no acesso dados
            objRespuesta.Iacs = AccesoDatos.Iac.GetComboInformacionAdicional()

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
    ''' obtem todas as informações adicionais vigentes.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [victor.ramos] 27/05/2014 - Criado
    ''' </history>
    Public Function GetComboInformacionAdicional(Peticion As ContractoServicio.Utilidad.GetComboInformacionAdicionalConFiltros.Peticion) As ContractoServicio.Utilidad.GetComboInformacionAdicionalConFiltros.Respuesta Implements ContractoServicio.IUtilidad.GetComboInformacionAdicionalConFiltros

        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboInformacionAdicionalConFiltros.Respuesta

        Try

            'chama o metodo getcomboInformacionAdicional no acesso dados
            objRespuesta.Iacs = AccesoDatos.Iac.GetComboInformacionAdicional(Peticion)

            'preparar codigos e mensagens do respuesta
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
    ''' obtem todas as agrupações vigentes.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 17/03/2009 - Criado
    ''' </history>
    Public Function GetListaAgrupaciones() As ContractoServicio.Utilidad.GetListaAgrupaciones.Respuesta Implements ContractoServicio.IUtilidad.GetListaAgrupaciones

        Dim objRespuesta As New ContractoServicio.Utilidad.GetListaAgrupaciones.Respuesta

        Try

            'chama o metodo GetListaAgrupaciones no acesso dados
            objRespuesta.Agrupaciones = AccesoDatos.Agrupacion.GetListaAgrupaciones()

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
    ''' Verifica que el código informado en el mensaje de entrada no existe en la base de datos.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.botmempo]      15/05/2009 - Criado
    ''' </history>
    Public Function VerificarCodigoCaracteristica(objPeticion As ContractoServicio.Utilidad.VerificarCodigoCaracteristica.Peticion) As ContractoServicio.Utilidad.VerificarCodigoCaracteristica.Respuesta Implements ContractoServicio.IUtilidad.VerificarCodigoCaracteristica

        Dim objRespuesta As New ContractoServicio.Utilidad.VerificarCodigoCaracteristica.Respuesta

        Try

            ' verificar se codigo tipo medio pago foi enviado
            If String.IsNullOrEmpty(objPeticion.Codigo) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("012_msg_codCaracteristica"))
            End If

            'chama o metodo VerificarCodigoCaracteristica no acesso dados
            objRespuesta.Existe = AccesoDatos.Caracteristica.VerificarCodigoCaracteristica(objPeticion.Codigo)

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
    ''' Verifica que el código de conteo  informado en el mensaje de entrada no existe en la base de datos.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.botmempo]      15/05/2009 - Criado
    ''' </history>
    Public Function VerificarCodigoConteoCaracteristica(objPeticion As ContractoServicio.Utilidad.VerificarCodigoConteoCaracteristica.Peticion) As ContractoServicio.Utilidad.VerificarCodigoConteoCaracteristica.Respuesta Implements ContractoServicio.IUtilidad.VerificarCodigoConteoCaracteristica

        Dim objRespuesta As New ContractoServicio.Utilidad.VerificarCodigoConteoCaracteristica.Respuesta

        Try

            ' verificar se codigo tipo medio pago foi enviado
            If String.IsNullOrEmpty(objPeticion.Codigo) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("012_msg_codConteoCaracteristica"))
            End If

            'chama o metodo VerificarCodigoConteoCaracteristica no acesso dados
            objRespuesta.Existe = AccesoDatos.Caracteristica.VerificarCodigoConteoCaracteristica(objPeticion.Codigo)

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
    ''' Verifica que la descripción de la caracteristica  informada en el mensaje de entrada no existe en la base de datos.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.botmempo]      15/05/2009 - Criado
    ''' </history>
    Public Function VerificarDescripcionCaracteristica(objPeticion As ContractoServicio.Utilidad.VerificarDescripcionCaracteristica.Peticion) As ContractoServicio.Utilidad.VerificarDescripcionCaracteristica.Respuesta Implements ContractoServicio.IUtilidad.VerificarDescripcionCaracteristica

        Dim objRespuesta As New ContractoServicio.Utilidad.VerificarDescripcionCaracteristica.Respuesta

        Try

            ' verificar se codigo tipo medio pago foi enviado
            If String.IsNullOrEmpty(objPeticion.Descripcion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("012_msg_desCaracteristica"))
            End If

            'chama o metodo VerificarDescripcionCaracteristica no acesso dados
            objRespuesta.Existe = AccesoDatos.Caracteristica.VerificarDescripcionCaracteristica(objPeticion.Descripcion)

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
    ''' Recupera las caracteristicas de los tipos de procesado visgentes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo]      15/05/2009 - Criado
    ''' </history>
    Public Function GetComboCaracteristicas() As ContractoServicio.Utilidad.GetComboCaracteristicas.Respuesta Implements ContractoServicio.IUtilidad.GetComboCaracteristicas
        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboCaracteristicas.Respuesta

        Try

            'chama o metodo getcombosubclientesbycliente no acesso dados
            objRespuesta.Caracteristicas = AccesoDatos.Caracteristica.GetComboCaracteristicas()

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
    ''' Recupera todos los modelos de cajeros existentes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]      11/02/2011  Criado
    ''' </history>
    Public Function GetComboRedes() As ContractoServicio.Utilidad.GetComboRedes.Respuesta Implements ContractoServicio.IUtilidad.GetComboRedes

        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboRedes.Respuesta
        Dim red As ContractoServicio.Utilidad.GetComboRedes.Red

        Try

            ' obtém redes
            Dim dt As DataTable = IAC.AccesoDatos.Red.GetComboRedes()

            ' inicializa
            objRespuesta.Redes = New List(Of ContractoServicio.Utilidad.GetComboRedes.Red)

            If dt.Rows.Count > 0 Then

                For Each row In dt.Rows

                    ' cria e preenche objeto rede
                    red = New ContractoServicio.Utilidad.GetComboRedes.Red

                    With red
                        .OidRed = row("OID_RED")
                        .CodigoRed = row("COD_RED")
                        .DescripcionRed = row("DES_RED")
                    End With

                    ' adiciona a lista
                    objRespuesta.Redes.Add(red)

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
    ''' Recupera todos los modelos de cajeros existentes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]      11/02/2011  Criado
    ''' </history>
    Public Function GetComboModelosCajero() As ContractoServicio.Utilidad.GetComboModelosCajero.Respuesta Implements ContractoServicio.IUtilidad.GetComboModelosCajero

        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboModelosCajero.Respuesta
        Dim modelo As ContractoServicio.Utilidad.GetComboModelosCajero.ModeloCajero

        Try

            ' obtém redes
            Dim dt As DataTable = IAC.AccesoDatos.ModeloCajero.GetComboModelosCajero()

            ' inicializa
            objRespuesta.ModelosCajero = New List(Of ContractoServicio.Utilidad.GetComboModelosCajero.ModeloCajero)

            If dt.Rows.Count > 0 Then

                For Each row In dt.Rows

                    ' cria e preenche objeto rede
                    modelo = New ContractoServicio.Utilidad.GetComboModelosCajero.ModeloCajero

                    With modelo
                        .OidModeloCajero = row("OID_MODELO_CAJERO")
                        .CodigoModeloCajero = row("COD_MODELO_CAJERO")
                        .DescripcionModeloCajero = row("DES_MODELO_CAJERO")
                    End With

                    ' adiciona a lista
                    objRespuesta.ModelosCajero.Add(modelo)

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
    ''' Recupera todos los niveles pelas permisos
    ''' </summary>
    Public Function getComboNivelesParametros(objPeticion As ContractoServicio.Utilidad.GetComboNivelesParametros.Peticion) As ContractoServicio.Utilidad.GetComboNivelesParametros.Respuesta Implements ContractoServicio.IUtilidad.GetComboNivelesParametros
        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboNivelesParametros.Respuesta

        Try
            'If objPeticion Is Nothing OrElse ((objPeticion.Permisos Is Nothing OrElse objPeticion.Permisos.Count = 0)) Then
            '    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("026_msg_permisos_no_encontrados"))
            'End If
            'chama o metodo getcombosubclientesbycliente no acesso dados
            objRespuesta.NivelesParametros = AccesoDatos.Nivel.GetComboNivelesParametros(objPeticion)
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
    ''' Recupera todos los Aplicaciones pelas permisos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana]      18/08/2011  Criado
    ''' </history>
    Public Function getComboAplicaciones() As ContractoServicio.Utilidad.getComboAplicaciones.Respuesta Implements ContractoServicio.IUtilidad.GetComboAplicaciones
        Dim objRespuesta As New ContractoServicio.Utilidad.getComboAplicaciones.Respuesta

        Try

            'chama o metodo getcombosubclientesbycliente no acesso dados
            objRespuesta.Aplicaciones = AccesoDatos.Aplicacion.GetComboAplicaciones()
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
    ''' Recupera las características tipo sector activas.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 01/03/2013  Criado
    ''' </history>
    Public Function GetComboCaractTipoSector() As ContractoServicio.Utilidad.GetComboCaractTipoSector.Respuesta Implements ContractoServicio.IUtilidad.GetComboCaractTipoSector

        'Cria objeto de Respuesta
        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboCaractTipoSector.Respuesta

        Try
            objRespuesta.Caracteristicas = AccesoDatos.TipoSetor.GetComboCaractTipoSector()
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
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 05/02/2010 - Criado
    ''' </history>
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IUtilidad.Test
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

    ''' <summary>
    '''  Configura Nivel Saldo
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 20/05/2013 - Criado
    ''' </history>
    Public Function GetConfigNivelSaldo(Peticion As ContractoServicio.Utilidad.GetConfigNivel.Peticion) As ContractoServicio.Utilidad.GetConfigNivel.Respuesta Implements ContractoServicio.IUtilidad.GetConfigNivelSaldo

        ' Cria objeto de respuesta
        Dim objRespuesta As New ContractoServicio.Utilidad.GetConfigNivel.Respuesta

        Try

            If Peticion.ParametrosPaginacion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "ParametrosPaginacion"))
            ElseIf (Peticion.ParametrosPaginacion.RealizarPaginacion AndAlso Peticion.ParametrosPaginacion.RegistrosPorPagina = 0) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "RegistrosPorPagina"))
            End If

            If String.IsNullOrEmpty(Peticion.CodCliente) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "codCliente"))
            End If

            objRespuesta.ConfigNivelMovs = AccesoDatos.ConfigNivelSaldo.getConfigNivelSaldo(Peticion, objRespuesta.ParametrosPaginacion)

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

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

    ''' <summary>
    '''  Configura Tipo SubCliente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Function GetComboTiposSubCliente() As ContractoServicio.Utilidad.GetComboTiposSubCliente.Respuesta Implements ContractoServicio.IUtilidad.GetComboTiposSubCliente

        ' Cria objeto de respuesta
        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboTiposSubCliente.Respuesta

        Try

            'chama o metodo getcombosubclientesbycliente no acesso dados
            objRespuesta.TiposSubCliente = AccesoDatos.TipoSubCliente.GetComboTiposSubCliente()

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
    '''  Configura Tipo Punto Servicio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Function GetComboTiposPuntoServicio() As ContractoServicio.Utilidad.GetComboTiposPuntoServicio.Respuesta Implements ContractoServicio.IUtilidad.GetComboTiposPuntoServicio

        ' Cria objeto de respuesta
        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboTiposPuntoServicio.Respuesta

        Try

            'chama o metodo getcombosubclientesbycliente no acesso dados
            objRespuesta.TiposPuntoServicio = AccesoDatos.TipoPuntoServicio.GetComboTiposPuntoServicio()

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
    '''  Configura Tipo Procedencia
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Function GetComboTiposProcedencia() As ContractoServicio.Utilidad.GetComboTiposProcedencia.Respuesta Implements ContractoServicio.IUtilidad.GetComboTiposProcedencia

        ' Cria objeto de respuesta
        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboTiposProcedencia.Respuesta

        Try

            'chama o metodo getcombosubclientesbycliente no acesso dados
            objRespuesta.TiposProcedencia = AccesoDatos.TipoProcedencia.GetComboTiposProcedencia()

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


    Public Function VerificarCodigoCliente(objPeticion As ContractoServicio.Utilidad.VerificarCodigoCliente.Peticion) As ContractoServicio.Utilidad.VerificarCodigoCliente.Respuesta Implements ContractoServicio.IUtilidad.VerificarCodigoCliente
        Dim objRespuesta As New ContractoServicio.Utilidad.VerificarCodigoCliente.Respuesta

        Try

            ' verificar se codigo cliente que foi enviado
            If String.IsNullOrEmpty(objPeticion.codCliente) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("055_msg_codigo_obrigatorio"))
            End If

            'chama o metodo VerificarCodigoCliente no acesso dados
            objRespuesta.Existe = AccesoDatos.Cliente.VerificarCodigoCliente(objPeticion.codCliente)

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

    Public Function VerificarCodigoPtoServicio(objPeticion As ContractoServicio.Utilidad.VerificarCodigoPtoServicio.Peticion) As ContractoServicio.Utilidad.VerificarCodigoPtoServicio.Respuesta Implements ContractoServicio.IUtilidad.VerificarCodigoPtoServicio
        Dim objRespuesta As New ContractoServicio.Utilidad.VerificarCodigoPtoServicio.Respuesta

        Try
            ' verificar se codigo cliente que foi enviado
            If String.IsNullOrEmpty(objPeticion.CodCliente) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("055_msg_codigo_obrigatorio"))
            End If

            ' verificar se codigo subcliente que foi enviado
            If String.IsNullOrEmpty(objPeticion.CodSubCliente) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("057_msg_codigo_obrigatorio"))
            End If

            ' verificar se codigo ponto de serviço que foi enviado
            If String.IsNullOrEmpty(objPeticion.CodPtoServicio) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("056_msg_codigo_obrigatorio"))
            End If

            'chama o metodo VerificarCodigoPtoServicio no acesso dados
            objRespuesta.Existe = AccesoDatos.PuntoServicio.VerificarCodigoPtoServicio(objPeticion.CodCliente, objPeticion.CodSubCliente, objPeticion.CodPtoServicio)

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

    Public Function VerificarCodigoSubCliente(objPeticion As ContractoServicio.Utilidad.VerificarCodigoSubCliente.Peticion) As ContractoServicio.Utilidad.VerificarCodigoSubCliente.Respuesta Implements ContractoServicio.IUtilidad.VerificarCodigoSubCliente
        Dim objRespuesta As New ContractoServicio.Utilidad.VerificarCodigoSubCliente.Respuesta

        Try

            ' verificar se codigo do cliente que foi enviado
            If String.IsNullOrEmpty(objPeticion.CodCliente) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("055_msg_codigo_obrigatorio"))
            End If

            ' verificar se codigo do subcliente que foi enviado
            If String.IsNullOrEmpty(objPeticion.CodSubCliente) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("057_msg_codigo_obrigatorio"))
            End If

            'chama o metodo VerificarCodigoSubCliente no acesso dados
            objRespuesta.Existe = AccesoDatos.SubCliente.VerificarCodigoSubCliente(objPeticion.CodCliente, objPeticion.CodSubCliente)

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

    Public Function VerificarDescripcionCliente(objPeticion As ContractoServicio.Utilidad.VerificarDescripcionCliente.Peticion) As ContractoServicio.Utilidad.VerificarDescripcionCliente.Respuesta Implements ContractoServicio.IUtilidad.VerificarDescripcionCliente
        Dim objRespuesta As New ContractoServicio.Utilidad.VerificarDescripcionCliente.Respuesta
        Try

            ' verificar se descrição do cliente que foi enviado
            If String.IsNullOrEmpty(objPeticion.DesCliente) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("055_msg_descricao_obrigatorio"))
            End If

            'chama o metodo VerificarDescripcionCliente no acesso dados
            objRespuesta.Existe = AccesoDatos.Cliente.VerificarDescripcionCliente(objPeticion.DesCliente)

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

    Public Function VerificarDescripcionPtoServicio(objPeticion As ContractoServicio.Utilidad.VerificarDescripcionPtoServicio.Peticion) As ContractoServicio.Utilidad.VerificarDescripcionPtoServicio.Respuesta Implements ContractoServicio.IUtilidad.VerificarDescripcionPtoServicio
        Dim objRespuesta As New ContractoServicio.Utilidad.VerificarDescripcionPtoServicio.Respuesta
        Try

            ' verificar se codigo do cliente foi enviado
            If String.IsNullOrEmpty(objPeticion.CodCliente) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("055_msg_codigo_obrigatorio"))
            End If

            ' verificar se codigo do subcliente foi enviado
            If String.IsNullOrEmpty(objPeticion.CodSubCliente) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("057_msg_codigo_obrigatorio"))
            End If

            ' verificar se descrição do ponto de sevico que foi enviado
            If String.IsNullOrEmpty(objPeticion.DesPtoServicio) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("056_msg_descricao_obrigatorio"))
            End If

            'chama o metodo VerificarDescripcionPtoServicio no acesso dados
            objRespuesta.Existe = AccesoDatos.PuntoServicio.VerificarDescripcionPtoServicio(objPeticion.CodCliente, objPeticion.CodSubCliente, objPeticion.DesPtoServicio)

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

    Public Function VerificarDescripcionSubCliente(objPeticion As ContractoServicio.Utilidad.VerificarDescripcionSubCliente.Peticion) As ContractoServicio.Utilidad.VerificarDescripcionSubCliente.Respuesta Implements ContractoServicio.IUtilidad.VerificarDescripcionSubCliente
        Dim objRespuesta As New ContractoServicio.Utilidad.VerificarDescripcionSubCliente.Respuesta
        Try

            ' verificar se codigo cliente que foi enviado
            If String.IsNullOrEmpty(objPeticion.CodCliente) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("055_msg_codigo_obrigatorio"))
            End If

            ' verificar se codigo subcliente que foi enviado
            If String.IsNullOrEmpty(objPeticion.DesSubCliente) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("057_msg_descricao_obrigatorio"))
            End If

            'chama o metodo VerificarDescripcionSubCliente no acesso dados
            objRespuesta.Existe = AccesoDatos.SubCliente.VerificarDescripcionSubCliente(objPeticion.CodCliente, objPeticion.DesSubCliente)

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
    ''' Verifica se o codigo acesso da divisa existe.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function VerificarCodigoAccesoDivisa(Peticion As ContractoServicio.Utilidad.VerificarCodigoAccesoDivisa.Peticion) As ContractoServicio.Utilidad.VerificarCodigoAccesoDivisa.Respuesta Implements ContractoServicio.IUtilidad.VerificarCodigoAccesoDivisa

        Dim objRespuesta As New ContractoServicio.Utilidad.VerificarCodigoAccesoDivisa.Respuesta

        Try

            'Verifica se o código acesso existe.
            objRespuesta.Existe = AccesoDatos.Divisa.VerificarCodAccesoDivisaExiste(Peticion.CodigoAcceso, String.Empty)

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
    ''' verifica se o codigo de acesso da denominação ja existe.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function VerificarCodigoAccesoDenominacion(Peticion As ContractoServicio.Utilidad.VerificarCodigoAccesoDenominacion.Peticion) As ContractoServicio.Utilidad.VerificarCodigoAccesoDenominacion.Respuesta Implements ContractoServicio.IUtilidad.VerificarCodigoAccesoDenominacion

        Dim objRespuesta As New ContractoServicio.Utilidad.VerificarCodigoAccesoDenominacion.Respuesta

        Try

            Dim OidDivisa As String = AccesoDatos.Divisa.ObterOidDivisa(Peticion.CodigoDivisa)

            'Verifica se o código acesso existe.
            objRespuesta.Existe = AccesoDatos.Denominacion.VerificarCodAccesoDenominacionExiste(Peticion.CodigoAcceso, String.Empty, OidDivisa)

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
    ''' Verifica se o codigo acesso do medio pago existe.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function VerificarCodigoAccesoMedioPago(Peticion As ContractoServicio.Utilidad.VerificarCodigoAccesoMedioPago.Peticion) As ContractoServicio.Utilidad.VerificarCodigoAccesoMedioPago.Respuesta Implements ContractoServicio.IUtilidad.VerificarCodigoAccesoMedioPago

        Dim objRespuesta As New ContractoServicio.Utilidad.VerificarCodigoAccesoMedioPago.Respuesta

        Try

            Dim OidDivisa As String = AccesoDatos.Divisa.ObterOidDivisa(Peticion.CodigoDivisa)

            'Verifica se o código acesso existe.
            objRespuesta.Existe = AccesoDatos.MedioPago.VerificarCodAccesoMedioPagoExiste(Peticion.CodigoAcceso, String.Empty, OidDivisa)

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
    ''' Obtém a lista de clientes
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Public Function GetComboSectores(objPeticion As ContractoServicio.Utilidad.GetComboSectores.Peticion) As ContractoServicio.Utilidad.GetComboSectores.Respuesta Implements ContractoServicio.IUtilidad.GetComboSectores

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboSectores.Respuesta

        Try

            ' obter divisas
            objRespuesta.Sectores = AccesoDatos.Sector.getComboSectores(objPeticion, objRespuesta.ParametrosPaginacion)

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
    ''' Obtiene los tipos de periodos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' </history>
    Public Function GetComboTiposPeriodos() As ContractoServicio.Utilidad.GetComboTiposPeriodo.Respuesta Implements ContractoServicio.IUtilidad.GetComboTiposPeriodo
        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboTiposPeriodo.Respuesta

        Try

            ' verificar se a descrição existe
            objRespuesta.TiposDePeriodos = AccesoDatos.TipoPeriodo.GetComboTiposPeriodos

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

End Class