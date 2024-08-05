Imports Prosegur.Global.GesEfectivo.IAC.AccesoDatos
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionGrupo
    Implements ContractoServicio.IGrupo

#Region "[MÉTODOS WS]"

    ''' <summary>
    ''' responsable por obtener los datos de todos los ATMs pertenecientes al grupo de la petición.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 13/01/2011 Criado
    ''' </history>
    Public Function GetATMsbyGrupo(Peticion As ContractoServicio.Grupo.GetATMsbyGrupo.Peticion) As ContractoServicio.Grupo.GetATMsbyGrupo.Respuesta Implements ContractoServicio.IGrupo.GetATMsbyGrupo

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Grupo.GetATMsbyGrupo.Respuesta
        Dim dt As DataTable

        Try

            ValidarPeticion(Peticion)

            ' executa busca
            dt = IAC.AccesoDatos.Grupo.GetATMsByGrupo(Peticion.OidGrupo)

            ' converte datatable em uma lista de clientes
            objRespuesta.Clientes = ConverterDtGetATMsbyGrupo(dt)

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
    ''' responsable por obtener los datos de los grupos. 
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 13/01/2011 Criado
    ''' </history>
    Public Function GetGrupos(Peticion As ContractoServicio.Grupo.GetGrupos.Peticion) As ContractoServicio.Grupo.GetGrupos.Respuesta Implements ContractoServicio.IGrupo.GetGrupos

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Grupo.GetGrupos.Respuesta
        Dim dt As DataTable = Nothing

        Try

            If Peticion.BolObtenerTodosGrupos Then

                ' obtener todos los grupos existentes  
                dt = IAC.AccesoDatos.Grupo.GetGrupos()

            Else

                ' obtener apenas los grupos que no poseen cajero
                dt = IAC.AccesoDatos.Grupo.GetGruposSemCajero()

            End If

            If dt IsNot Nothing Then
                objRespuesta.Grupos = ConverterDtGetGrupos(dt)
            End If

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
    ''' responsable por grabar en la base de datos las informaciones del grupo informado en la petición.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 13/01/2011 Criado
    ''' </history>
    Public Function SetGrupo(Peticion As ContractoServicio.Grupo.SetGrupo.Peticion) As ContractoServicio.Grupo.SetGrupo.Respuesta Implements ContractoServicio.IGrupo.SetGrupo

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Grupo.SetGrupo.Respuesta

        Try

            ValidarPeticion(Peticion)

            ' insere grupo
            IAC.AccesoDatos.Grupo.InsertarGrupo(Guid.NewGuid().ToString(), Peticion.CodigoGrupo, Peticion.DescripcionGrupo,
                                                Peticion.CodUsuario, DateTime.Now)

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
    ''' responsable por verificar si ya existe grupo con los datos informados en la peticion.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 13/01/2011 Criado
    ''' </history>
    Public Function VerificarGrupo(Peticion As ContractoServicio.Grupo.VerificarGrupo.Peticion) As ContractoServicio.Grupo.VerificarGrupo.Respuesta Implements ContractoServicio.IGrupo.VerificarGrupo

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Grupo.VerificarGrupo.Respuesta

        Try

            ' inicializa
            objRespuesta.Morfologia = New ContractoServicio.Grupo.VerificarGrupo.Morfologia

            ValidarPeticion(Peticion)

            ' verifica se existe grupo 
            objRespuesta.Morfologia.BolExiste = IAC.AccesoDatos.Grupo.VerificarGrupo(Peticion.CodigoGrupo)

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
    ''' [danielnunes] 18/06/2013 - Criado
    ''' </history>
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IGrupo.Test
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

#End Region

#Region "[VALIDAR]"

    ''' <summary>
    ''' valida a petição da operação VerificarGrupo
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  13/01/2011  criado
    ''' </history>
    Private Sub ValidarPeticion(Peticion As ContractoServicio.Grupo.VerificarGrupo.Peticion)

        If String.IsNullOrEmpty(Peticion.CodigoGrupo) Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("024_msg_codigogrupo"))

        End If

    End Sub

    ''' <summary>
    ''' valida a petição da operação GetATMsbyGrupo
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  13/01/2011  criado
    ''' </history>
    Private Sub ValidarPeticion(Peticion As ContractoServicio.Grupo.GetATMsbyGrupo.Peticion)

        If String.IsNullOrEmpty(Peticion.OidGrupo) Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("024_msg_oidgrupo"))

        End If

    End Sub

    ''' <summary>
    ''' valida a petição da operação SetGrupo
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  13/01/2011  criado
    ''' </history>
    Private Sub ValidarPeticion(Peticion As ContractoServicio.Grupo.SetGrupo.Peticion)

        If String.IsNullOrEmpty(Peticion.CodigoGrupo) Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("024_msg_codigogrupo"))

        End If

        If String.IsNullOrEmpty(Peticion.DescripcionGrupo) Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("024_msg_descripciongrupo"))

        End If

        If String.IsNullOrEmpty(Peticion.CodUsuario) Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("024_msg_codusuario"))

        End If

    End Sub

#End Region

#Region "[CONVERTER DATATABLE]"

    ''' <summary>
    ''' converte um datatable com o resultado da busca do método GetGrupos em uma lista de Grupo.GetGrupos.Grupo
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  13/01/2011  criado
    ''' </history>
    Private Function ConverterDtGetGrupos(dt As DataTable) As List(Of ContractoServicio.Grupo.GetGrupos.Grupo)

        Dim grupos As New List(Of ContractoServicio.Grupo.GetGrupos.Grupo)
        Dim grupo As ContractoServicio.Grupo.GetGrupos.Grupo

        If dt.Rows.Count = 0 Then
            Return grupos
        End If

        For Each row In dt.Rows

            ' cria objeto grupo
            grupo = New ContractoServicio.Grupo.GetGrupos.Grupo

            ' preenche objeto grupo
            With grupo
                .CodigoGrupo = Util.VerificarDBNull(row("cod_grupo"))
                .DescripcionGrupo = Util.VerificarDBNull(row("des_grupo"))
                .OidGrupo = Util.VerificarDBNull(row("oid_grupo"))
            End With

            ' adiciona a lista
            grupos.Add(grupo)

        Next

        Return grupos

    End Function

    ''' <summary>
    ''' converte um datatable com o resultado da busca do método GetATMsbyGrupo em uma lista de Grupo.GetATMsbyGrupo.Cliente
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  13/01/2011  criado
    ''' </history>
    Private Function ConverterDtGetATMsbyGrupo(dt As DataTable) As List(Of ContractoServicio.Grupo.GetATMsbyGrupo.Cliente)

        Dim lista As New List(Of ContractoServicio.Grupo.GetATMsbyGrupo.Cliente)
        Dim cliente As ContractoServicio.Grupo.GetATMsbyGrupo.Cliente
        Dim subCliente As ContractoServicio.Grupo.GetATMsbyGrupo.SubCliente
        Dim ptoServ As ContractoServicio.Grupo.GetATMsbyGrupo.PuntoServicio
        Dim oidCliente As String
        Dim codCliente As String
        Dim codSubcliente As String
        Dim rowsSubclientes As DataRow()
        Dim rowsPtosServicio As DataRow()

        If dt.Rows.Count = 0 Then
            Return lista
        End If

        For Each row In dt.Rows

            ' obtém oid cliente
            oidCliente = row("OID_CLIENTE")
            codCliente = Util.VerificarDBNull(row("COD_CLIENTE"))

            ' verfica se cliente já foi adicionado
            If (From cli In lista Where cli.CodigoCliente = codCliente).FirstOrDefault() IsNot Nothing Then
                Continue For
            End If

            ' cria e preenche objeto cliente
            cliente = New ContractoServicio.Grupo.GetATMsbyGrupo.Cliente

            With cliente
                .CodigoCliente = Util.VerificarDBNull(row("COD_CLIENTE"))
                .DescripcionCliente = Util.VerificarDBNull(row("DES_CLIENTE"))
                .SubClientes = New List(Of ContractoServicio.Grupo.GetATMsbyGrupo.SubCliente)
            End With

            ' adiciona a lista
            lista.Add(cliente)

            ' obtém subclientes
            rowsSubclientes = (From r As DataRow In dt.Rows Where r("OID_CLIENTE") = oidCliente).ToArray()

            ' adiciona subcliente
            For Each rSubcli In rowsSubclientes

                ' obtém codigo subcliente
                codSubcliente = rSubcli("COD_SUBCLIENTE")

                ' verifica se subcliente já foi adicionado
                If (From sc In cliente.SubClientes Where sc.CodigoSubcliente = codSubcliente).FirstOrDefault() IsNot Nothing Then
                    Continue For
                End If

                ' cria e preenche subcliente
                subCliente = New ContractoServicio.Grupo.GetATMsbyGrupo.SubCliente

                With subCliente
                    .CodigoSubcliente = Util.VerificarDBNull(rSubcli("COD_SUBCLIENTE"))
                    .DescripcionSubcliente = Util.VerificarDBNull(rSubcli("DES_SUBCLIENTE"))
                    .PuntosServicio = New List(Of ContractoServicio.Grupo.GetATMsbyGrupo.PuntoServicio)
                End With

                ' adiciona ao cliente
                cliente.SubClientes.Add(subCliente)

                ' obtém subclientes
                rowsPtosServicio = (From r As DataRow In dt.Rows Where r("OID_CLIENTE") = oidCliente AndAlso r("COD_SUBCLIENTE") = codSubcliente).ToArray()

                ' preenche pontos de serviço do subcliente
                For Each rPto In rowsPtosServicio

                    ' cria e preenche subcliente
                    ptoServ = New ContractoServicio.Grupo.GetATMsbyGrupo.PuntoServicio

                    With ptoServ
                        .CodigoCajero = Util.VerificarDBNull(rPto("COD_CAJERO"))
                        .CodigoPuntoServicio = Util.VerificarDBNull(rPto("COD_PTO_SERVICIO"))
                        .DescripcionPuntoServicio = Util.VerificarDBNull(rPto("Des_Pto_Servicio"))
                        .OidCajero = Util.VerificarDBNull(rPto("OID_CAJERO"))
                        .OidPuntoServicio = Util.VerificarDBNull(rPto("OID_PTO_SERVICIO"))
                        .FyhActualizacion = Util.VerificarDBNull(rPto("FYH_ACTUALIZACION"))
                    End With

                    ' adiciona ao subcliente
                    subCliente.PuntosServicio.Add(ptoServ)

                Next

            Next

        Next

        Return lista

    End Function

#End Region

End Class
