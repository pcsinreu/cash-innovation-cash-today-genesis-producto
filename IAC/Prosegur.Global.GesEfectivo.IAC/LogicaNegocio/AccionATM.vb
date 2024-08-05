Imports Prosegur.Global.GesEfectivo.IAC.AccesoDatos
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionATM
    Implements ContractoServicio.IATM

#Region "[MÉTODOS WS]"

    ''' <summary>
    ''' Esta operación es responsable por obtener los detalles de un ATM de acuerdo con los parámetros de entrada. 
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 10/01/2011 Criado
    ''' </history>
    Public Function GetATMDetail(Peticion As ContractoServicio.ATM.GetATMDetail.Peticion) As ContractoServicio.ATM.GetATMDetail.Respuesta Implements ContractoServicio.IATM.GetATMDetail

        ' criar objeto respuesta
        Dim Respuesta As New ContractoServicio.ATM.GetATMDetail.Respuesta

        Try

            If Not String.IsNullOrEmpty(Peticion.OidCajero) Then

                ' obtener las morfologías y procesos pertenecientes al ATM
                Dim dt As DataTable = IAC.AccesoDatos.Cajero.GetATMDetail(Peticion.OidCajero, String.Empty)

                ' converte o resultado no objeto de retorno
                Respuesta.ATM = ConverterDataTableGetATMDetail(dt, String.Empty)

            ElseIf Not String.IsNullOrEmpty(Peticion.OidGrupo) Then

                ' entre todos los ATMs pertenecientes al grupo, obtener apenas los datos de un determinado ATM, 
                ' pues, los ATMs tienen morfologías y procesos iguales
                Dim dt As DataTable = IAC.AccesoDatos.Cajero.GetATMDetail(String.Empty, Peticion.OidGrupo)
                Dim oidATM As String = String.Empty

                ' obtém o primeiro ATM (pois os ATMs tem morfologías e processos iguais)
                If dt.Rows.Count > 0 Then
                    oidATM = dt.Rows(0)("OID_CAJERO")
                End If

                ' converte o resultado no objeto de retorno
                Respuesta.ATM = ConverterDataTableGetATMDetail(dt, oidATM)

            End If

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            Respuesta.CodigoError = ex.Codigo
            Respuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            Respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            Respuesta.MensajeError = ex.ToString()
            Respuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return Respuesta

    End Function


    ''' <summary>
    ''' Esta operación es responsable por mantener los datos del ATM, y de sus morfologías y procesos.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 07/01/2011 Criado
    ''' </history>
    Public Function SetATM(Peticion As ContractoServicio.ATM.SetATM.Peticion) As ContractoServicio.ATM.SetATM.Respuesta Implements ContractoServicio.IATM.SetATM

        ' criar objeto respuesta
        Dim Respuesta As New ContractoServicio.ATM.SetATM.Respuesta
        Dim fechaAtual As DateTime
        Dim acao As Enumeraciones.Acao
        Dim transacion As Prosegur.DbHelper.Transacao

        Try

            ' define acao (somente dos cajeros)
            If Peticion.BolBorrar Then
                acao = Enumeraciones.Acao.Baja
            ElseIf String.IsNullOrEmpty(Peticion.Cajeros(0).OidCajero) Then
                acao = Enumeraciones.Acao.Alta
            Else
                acao = Enumeraciones.Acao.Modificacion
            End If

            ' valida petição
            ValidarPeticion(Peticion, acao)

            ' seta data de atualização
            fechaAtual = DateTime.Now

            ' cria transação
            transacion = New Prosegur.DbHelper.Transacao(AccesoDatos.Constantes.CONEXAO_GE)

            ' insere/atualiza/exclui cajeros
            For Each cajero In Peticion.Cajeros

                Select Case acao

                    Case Enumeraciones.Acao.Alta

                        InsertarATM(cajero, Peticion.OidModeloCajero, Peticion.OidRed, Peticion.OidGrupo,
                                    Peticion.CodigoDelegacion, Peticion.BolRegistroTira, Peticion.CodUsuario, fechaAtual, transacion)

                        InsertarATMXMorfologia(cajero, Peticion.Morfologias, Peticion.CodUsuario, fechaAtual, transacion)

                    Case Enumeraciones.Acao.Baja

                        IAC.AccesoDatos.Cajero.EliminarATM(cajero.OidCajero, Peticion.CodUsuario, fechaAtual, transacion)

                    Case Enumeraciones.Acao.Modificacion

                        ActualizarATM(cajero, Peticion.OidModeloCajero, Peticion.OidRed, Peticion.OidGrupo, Peticion.CodigoDelegacion,
                                      Peticion.BolRegistroTira, Peticion.CodUsuario, fechaAtual, Peticion.Morfologias, transacion)

                End Select

            Next

            ' executa petições da operação SetProceso
            EjecutarSetProceso(Peticion.Procesos, transacion)

            ' se não ocorreu erros, executa transação
            transacion.RealizarTransacao()

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            Respuesta.CodigoError = ex.Codigo
            Respuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            Respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            Respuesta.MensajeError = ex.ToString()
            Respuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return Respuesta

    End Function

    ''' <summary>
    ''' Esta operación es responsable por obtener los datos de los ATM’s de acuerdo con los parámetros de entrada.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 07/01/2011 Criado
    ''' </history>
    Public Function GetATMs(Peticion As ContractoServicio.GetATMs.Peticion) As ContractoServicio.GetATMs.Respuesta Implements ContractoServicio.IATM.GetATMs

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.GetATMs.Respuesta
        Dim codCliente As String = String.Empty
        Dim codsSubclientes As New List(Of String)
        Dim codsPtosServ As New List(Of String)
        Dim dt As DataTable

        Try

            ValidarPeticion(Peticion)

            If Peticion.Cliente IsNot Nothing Then
                ' obtém código do cliente
                codCliente = Peticion.Cliente.CodigoCliente
            End If

            If Peticion.SubClientes IsNot Nothing AndAlso Peticion.SubClientes.Count > 0 Then
                ' obtém a lista de códigos dos subclientes
                codsSubclientes = (From sc In Peticion.SubClientes Select sc.CodigoSubcliente).ToList()
            End If

            If Peticion.PuntoServicio IsNot Nothing AndAlso Peticion.PuntoServicio.Count > 0 Then
                ' obtém a lista de códigos dos subclientes
                codsPtosServ = (From sc In Peticion.PuntoServicio Select sc.CodigoPuntoServicio).ToList()
            End If

            ' realiza a busca
            With Peticion
                dt = IAC.AccesoDatos.Cajero.GetATMs(.CodigoDelegacion, .CodigoCajero, .CodigoRed, .CodigoModeloCajero, .CodigoGrupo, .BolVigente,
                                                 codCliente, codsSubclientes, codsPtosServ)
            End With

            ' convert o resultado para objeto do tipo lista de ATMs
            objRespuesta.ATMs = ConverterDataTableGetATMs(dt)

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
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IATM.Test

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
    ''' valida dados da petição da operação GetATMs
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  07/01/2011  criado
    ''' </history>
    Private Sub ValidarPeticion(Peticion As ContractoServicio.GetATMs.Peticion)

        ' codigoDelegacion é obrigatório
        If String.IsNullOrEmpty(Peticion.CodigoDelegacion) Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("023_msg_codigodelegacion"))

        End If

        If Peticion.SubClientes IsNot Nothing AndAlso Peticion.SubClientes.Count > 0 Then

            ' qdo existir subclientes, o codigoSubcliente é obrigatório

            ' obtém subclientse cujo código não foi preenchido
            Dim subclientesInvalidos = (From subC In Peticion.SubClientes _
                                        Where String.IsNullOrEmpty(subC.CodigoSubcliente)).FirstOrDefault()

            If subclientesInvalidos IsNot Nothing Then

                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("023_msg_codigosubcliente"))

            End If

        End If

        ' qdo existir pontos de serviço, o codigoPuntoServicio é obrigatório

        If Peticion.PuntoServicio IsNot Nothing AndAlso Peticion.PuntoServicio.Count > 0 Then

            ' qdo existir ptos servicio, o codigo é obrigatório

            ' obtém ptos cujo código não foi preenchido
            Dim ptosInvalidos = (From pto In Peticion.PuntoServicio _
                                        Where String.IsNullOrEmpty(pto.CodigoPuntoServicio)).FirstOrDefault()

            If ptosInvalidos IsNot Nothing Then

                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("023_msg_codigoptoservicio"))

            End If

        End If

    End Sub



    ''' <summary>
    ''' valida dados da petição 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  07/01/2011  criado
    ''' </history>
    Private Sub ValidarPeticion(Peticion As ContractoServicio.ATM.SetATM.Peticion, _
                                Acao As Enumeraciones.Acao)

        ' é obrigatório pelo menos 1 cajero
        If Peticion.Cajeros Is Nothing OrElse Peticion.Cajeros.Count = 0 Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("023_msg_atm"))

        End If

        ' é obrigatório pelo menos 1 proceso
        If Peticion.Procesos Is Nothing OrElse Peticion.Procesos.Count = 0 Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("023_msg_proceso"))

        End If

        For Each morf In Peticion.Morfologias

            ' se existir morfologia, oid morfologia é obrigatório
            If String.IsNullOrEmpty(morf.OidMorfologia) Then

                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("023_msg_oidmorfologia"))

            End If

            ' se existir morfologia, fecha início é obrigatório
            If morf.FechaInicio = Nothing Then

                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("023_msg_fecinicio"))

            End If

        Next

        For Each caj In Peticion.Cajeros

            ' se codigo cajero for vazio, cod pto serviço é obrigatório
            If Acao <> Enumeraciones.Acao.Baja AndAlso _
                String.IsNullOrEmpty(caj.CodigoCajero) AndAlso String.IsNullOrEmpty(caj.CodigoPuntoServicio) Then

                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("023_msg_codcaj_codpto"))

            End If

            If Acao = Enumeraciones.Acao.Modificacion OrElse Acao = Enumeraciones.Acao.Baja Then

                ' oid cajero é obrigatório
                If String.IsNullOrEmpty(caj.OidCajero) Then

                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("023_msg_oidcajero"))

                End If

            End If

        Next

    End Sub

#End Region

#Region "[CONVERTER DATATABLE]"


    ''' <summary>
    ''' Converte um datatable para uma lista de ATMs
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  07/01/2011  criado
    ''' </history>
    Private Function ConverterDataTableGetATMs(dt As DataTable) As List(Of ContractoServicio.GetATMs.ATM)

        Dim listaATMs As New List(Of ContractoServicio.GetATMs.ATM)
        Dim atm As New ContractoServicio.GetATMs.ATM
        Dim morfologia As ContractoServicio.GetATMs.Morfologia
        Dim rowsMorfologias As DataRow()

        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            Return listaATMs
        End If

        For Each row In dt.Rows

            If row("OID_CAJERO") = atm.OidCajero Then
                ' passa para o próximo até ser um ATM diferente do que já foi processado
                Continue For
            End If

            ' cria ATM
            atm = New ContractoServicio.GetATMs.ATM

            With atm
                .BolRegistroTira = Util.VerificarDBNull(row("BOL_REGISTRO_TIRA"))
                .BolVigente = Util.VerificarDBNull(row("BOL_VIGENTE"))
                .CodigoCajero = Util.VerificarDBNull(row("COD_CAJERO"))
                .CodigoCliente = Util.VerificarDBNull(row("COD_CLIENTE"))
                .CodigoGrupo = Util.VerificarDBNull(row("COD_GRUPO"))
                .CodigoModeloCajero = Util.VerificarDBNull(row("COD_MODELO_CAJERO"))
                .CodigoPuntoServicio = Util.VerificarDBNull(row("COD_PTO_SERVICIO"))
                .CodigoRed = Util.VerificarDBNull(row("COD_RED"))
                .CodigoSubcliente = Util.VerificarDBNull(row("COD_SUBCLIENTE"))
                .DescripcionCliente = Util.VerificarDBNull(row("DES_CLIENTE"))
                .DescripcionGrupo = Util.VerificarDBNull(row("DES_GRUPO"))
                .DescripcionModeloCajero = Util.VerificarDBNull(row("DES_MODELO_CAJERO"))
                .DescripcionPuntoServicio = Util.VerificarDBNull(row("DES_PTO_SERVICIO"))
                .DescripcionRed = Util.VerificarDBNull(row("DES_RED"))
                .DescripcionSubcliente = Util.VerificarDBNull(row("DES_SUBCLIENTE"))
                .FyhActualizacion = Util.VerificarDBNull(row("FYH_ACTUALIZACION"))
                .OidCajero = Util.VerificarDBNull(row("OID_CAJERO"))
                .OidGrupo = Util.VerificarDBNull(row("OID_GRUPO"))
            End With

            ' obtém somente as morfologias vigentes do ATM
            rowsMorfologias = (From r As DataRow In dt.Rows Where r("OID_CAJERO") = atm.OidCajero AndAlso Not IsDBNull(r("OID_MORFOLOGIA"))).ToArray()

            If rowsMorfologias IsNot Nothing AndAlso rowsMorfologias.Count > 0 Then

                ' cria morfologia
                morfologia = New ContractoServicio.GetATMs.Morfologia

                With morfologia
                    .DescripcionMorfologia = Util.VerificarDBNull(rowsMorfologias(0)("DES_MORFOLOGIA"))
                    .OidMorfologia = Util.VerificarDBNull(rowsMorfologias(0)("OID_MORFOLOGIA"))
                    .NecModalidadRecogida = Util.VerificarDBNull(rowsMorfologias(0)("NEC_MODALIDAD_RECOGIDA"))
                End With

                ' adiciona morfologia ao ATM
                atm.Morfologia = morfologia

            End If

            ' adiciona a lista
            listaATMs.Add(atm)

        Next

        Return listaATMs

    End Function



    ''' <summary>
    ''' converte um datatable com o resultado da busca do método GetATMDetail e GetATMDetailXGrupo num objeto GetATMDetail.ATM
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  11/01/2011  criado
    ''' </history>
    Private Function ConverterDataTableGetATMDetail(dt As DataTable, OidATM As String) As ContractoServicio.ATM.GetATMDetail.ATM

        Dim atm As New ContractoServicio.ATM.GetATMDetail.ATM
        Dim morfologia As New ContractoServicio.ATM.GetATMDetail.Morfologia
        Dim oidCajeroXMorfologia As String
        Dim dtDados As DataTable
        Dim oidsCajeroXMorf As New List(Of String)

        If dt.Rows.Count = 0 Then
            Return atm
        End If

        ' preenche objeto ATM
        With atm
            .CodCajero = Util.VerificarDBNull(dt.Rows(0)("COD_CAJERO"))
            .BolRegistroTira = Util.VerificarDBNull(dt.Rows(0)("BOL_REGISTRO_TIRA"))
            .OidGrupo = Util.VerificarDBNull(dt.Rows(0)("OID_GRUPO"))
            .CodigoGrupo = Util.VerificarDBNull(dt.Rows(0)("COD_GRUPO"))
            .DescripcionGrupo = Util.VerificarDBNull(dt.Rows(0)("DES_GRUPO"))
            .OidModeloCajero = Util.VerificarDBNull(dt.Rows(0)("OID_MODELO_CAJERO"))
            .CodigoModeloCajero = Util.VerificarDBNull(dt.Rows(0)("COD_MODELO_CAJERO"))
            .DescripcionModeloCajero = Util.VerificarDBNull(dt.Rows(0)("DES_MODELO_CAJERO"))
            .OidRede = Util.VerificarDBNull(dt.Rows(0)("OID_RED"))
            .CodigoRed = Util.VerificarDBNull(dt.Rows(0)("COD_RED"))
            .DescripcionRed = Util.VerificarDBNull(dt.Rows(0)("DES_RED"))
            .FyhActualizacion = Util.VerificarDBNull(dt.Rows(0)("FYH_ACTUALIZACION"))
            .Morfologias = New List(Of ContractoServicio.ATM.GetATMDetail.Morfologia)
            .Procesos = New List(Of ContractoServicio.ATM.GetATMDetail.Proceso)
        End With

        ' se foi informado oidATM, inclui apenas as morfologias do ATM informado
        If String.IsNullOrEmpty(OidATM) Then

            dtDados = dt

        Else

            ' copia estrutura da tabela
            dtDados = dt.Clone()

            ' filtra apenas os dados do ATM informado
            For Each row In (From r As DataRow In dt.Rows Where r("OID_CAJERO") = OidATM)
                dtDados.Rows.Add(row.ItemArray)
            Next

        End If

        ' preenche morfologias
        For Each row In dtDados.Rows

            ' obtém oid corrente
            oidCajeroXMorfologia = Util.VerificarDBNull(row("OID_CAJEROXMORFOLOGIA"))

            ' verifica se morfologia já foi adicionada
            If (From oid In oidsCajeroXMorf Where oid = oidCajeroXMorfologia).FirstOrDefault() Is Nothing Then

                ' se ainda não foi, adiciona
                atm.Morfologias.Add(CriarMorfologia(Util.VerificarDBNull(row("OID_MORFOLOGIA")), _
                                                    Util.VerificarDBNull(row("COD_MORFOLOGIA")), _
                                                    Util.VerificarDBNull(row("DES_MORFOLOGIA")), _
                                                    Util.VerificarDBNull(row("FEC_INICIO"))))

                ' adiciona a lista de oids adicionados
                oidsCajeroXMorf.Add(oidCajeroXMorfologia)

            End If

        Next

        ' obtém processos
        atm.Procesos = ObtenerProceso(dtDados.Rows)

        Return atm

    End Function


#End Region

    ''' <summary>
    ''' Insere os dados do ATM
    ''' </summary>
    ''' <param name="BolRegistro"></param>
    ''' <param name="Cajero"></param>
    ''' <param name="CodDelegacion"></param>
    ''' <param name="CodUsuario"></param>
    ''' <param name="FechaAtual"></param>
    ''' <param name="OidGrupo"></param>
    ''' <param name="OidModeloCajero"></param>
    ''' <param name="OidRed"></param>
    ''' <param name="Transacion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  11/01/2011  criado
    ''' </history>
    Private Sub InsertarATM(ByRef Cajero As ContractoServicio.ATM.SetATM.Cajero, OidModeloCajero As String, _
                            OidRed As String, OidGrupo As String, CodDelegacion As String, _
                            BolRegistro As Boolean, CodUsuario As String, FechaAtual As DateTime, _
                            ByRef Transacion As Prosegur.DbHelper.Transacao)

        Dim CodCajero As String = Cajero.CodigoCajero
        Dim OidPuntoServicio As String

        ' seta identificador cajero
        Cajero.OidCajero = Guid.NewGuid().ToString()

        ' obtém oid pto serv
        OidPuntoServicio = ObtenerOidPtoServicio(Cajero.CodigoCliente, Cajero.CodigoSubcliente, Cajero.CodigoPuntoServicio)

        IAC.AccesoDatos.Cajero.InsertarATM(Cajero.OidCajero, OidModeloCajero, OidRed, OidPuntoServicio, OidGrupo, CodCajero, _
                                        CodDelegacion, BolRegistro, True, CodUsuario, FechaAtual, Transacion)

    End Sub

    ''' <summary>
    ''' Insere uma associação CajeroXMorfologia
    ''' </summary>
    ''' <param name="Cajero"></param>
    ''' <param name="Morfologias"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  11/01/2011  criado
    ''' </history>
    Private Sub InsertarATMXMorfologia(Cajero As ContractoServicio.ATM.SetATM.Cajero, Morfologias As List(Of ContractoServicio.ATM.SetATM.Morfologia), _
                                       CodUsuario As String, FechaAtual As DateTime, _
                                       ByRef Transacion As Prosegur.DbHelper.Transacao)

        For Each morf In Morfologias

            IAC.AccesoDatos.CajeroXMorfologia.InsertarCajeroXMorfologia(Guid.NewGuid().ToString(), morf.OidMorfologia, Cajero.OidCajero, _
                                                                        morf.FechaInicio, CodUsuario, FechaAtual, Transacion)

        Next

    End Sub

    ''' <summary>
    ''' Atualiza os ATMs da petição e suas morfologias
    ''' </summary>
    ''' <param name="Cajero"></param>
    ''' <param name="Transacion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  12/01/2011  criado
    ''' </history>
    Private Sub ActualizarATM(Cajero As ContractoServicio.ATM.SetATM.Cajero, OidModeloCajero As String, _
                              OidRed As String, OidGrupo As String, CodDelegacion As String, _
                              BolRegistro As Boolean, CodUsuario As String, FechaAtual As DateTime, _
                              Morfologias As List(Of ContractoServicio.ATM.SetATM.Morfologia), _
                              ByRef Transacion As Prosegur.DbHelper.Transacao)

        ' obtém última data de atualização do ATM
        Dim fyhUltimaActualiz As DateTime = IAC.AccesoDatos.Cajero.GetFyhActulizacion(Cajero.OidCajero)

        ' verifica se atm já foi atualizado por outro usuário
        If Cajero.FyhActualizacion <> fyhUltimaActualiz Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("023_msg_concorrencia_atm"))

        End If

        ' obtém oid pto serv
        Dim OidPuntoServicio As String = ObtenerOidPtoServicio(Cajero.CodigoCliente, Cajero.CodigoSubcliente, Cajero.CodigoPuntoServicio)

        ' atualiza ATM
        IAC.AccesoDatos.Cajero.ActualizarATM(Cajero.OidCajero, OidModeloCajero, OidRed, OidPuntoServicio, OidGrupo, _
                                             Cajero.CodigoCajero, CodDelegacion, BolRegistro, CodUsuario, FechaAtual, Transacion)

        ' exclui morfologias do ATM
        IAC.AccesoDatos.CajeroXMorfologia.EliminarMorfologias(Cajero.OidCajero, Transacion)

        ' insere morfologias do ATM
        InsertarATMXMorfologia(Cajero, Morfologias, CodUsuario, FechaAtual, Transacion)

    End Sub

    ''' <summary>
    ''' retorna um objeto GetATMDetail.Morfologia preenchido
    ''' </summary>
    ''' <param name="OidMorfologia"></param>
    ''' <param name="CodMorfologia"></param>
    ''' <param name="DesMorfologia"></param>
    ''' <param name="FecInicio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  11/01/2011  criado
    ''' </history>
    Private Function CriarMorfologia(OidMorfologia As String, CodMorfologia As String, DesMorfologia As String, _
                                     FecInicio As DateTime) As ContractoServicio.ATM.GetATMDetail.Morfologia

        Dim Morfologia As New ContractoServicio.ATM.GetATMDetail.Morfologia

        With Morfologia
            .CodigoMorfologia = CodMorfologia
            .DescripcionMorfologia = DesMorfologia
            .FechaInicio = FecInicio
            .OidMorfologia = OidMorfologia
        End With

        Return Morfologia

    End Function

    ''' <summary>
    ''' Obtém detalhes dos processos dos oids informados
    ''' </summary>
    ''' <param name="Rows"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  11/01/2011  criado
    ''' </history>
    Private Function ObtenerProceso(Rows As DataRowCollection) As List(Of ContractoServicio.ATM.GetATMDetail.Proceso)

        Dim accionProceso As New AccionProceso()
        Dim peticion As New ContractoServicio.Proceso.GetProcesoDetail.Peticion
        Dim respuesta As New ContractoServicio.Proceso.GetProcesoDetail.Respuesta
        Dim peticionProceso As ContractoServicio.Proceso.GetProcesoDetail.PeticionProceso
        Dim proceso As ContractoServicio.ATM.GetATMDetail.Proceso
        Dim listaProcesos As New List(Of ContractoServicio.ATM.GetATMDetail.Proceso)
        Dim oidProceso As String = String.Empty

        ' inicializa
        peticion.PeticionProcesos = New ContractoServicio.Proceso.GetProcesoDetail.PeticionProcesoColeccion

        ' obtém dados dos processos
        For Each row In (From rProc As DataRow In Rows Where Not IsDBNull(rProc("OID_PROCESO"))).Distinct().ToList()

            oidProceso = row("OID_PROCESO")

            If (From p In listaProcesos Where p.OidProceso = oidProceso).FirstOrDefault() IsNot Nothing Then

                Continue For

            End If

            ' inicializa 
            proceso = New ContractoServicio.ATM.GetATMDetail.Proceso
            proceso.RespuestaProcesoDetail = New ContractoServicio.Proceso.GetProcesoDetail.Respuesta

            ' preenche petição
            peticionProceso = New ContractoServicio.Proceso.GetProcesoDetail.PeticionProceso

            peticionProceso.CodigoCliente = row("COD_CLIENTE")
            peticionProceso.CodigoDelegacion = row("COD_DELEGACION")
            peticionProceso.CodigoSubcanal = row("COD_SUBCANAL")
            peticionProceso.CodigoSubcliente = row("COD_SUBCLIENTE")
            peticionProceso.CodigoPuntoServicio = row("COD_PTO_SERVICIO")
            peticionProceso.IdentificadorProceso = oidProceso
            peticion.PeticionProcesos.Insert(0, peticionProceso)

            ' obtém dados do proceso
            proceso.RespuestaProcesoDetail = accionProceso.GetProcesoDetail(peticion)

            ' preenche oid do proceso
            proceso.OidProceso = row("OID_PROCESO")

            ' adiciona a lista
            listaProcesos.Add(proceso)

        Next

        Return listaProcesos

    End Function

    ''' <summary>
    ''' Executa operação SetProcessos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  12/01/2011  criado
    ''' </history>
    Private Sub EjecutarSetProceso(Procesos As List(Of ContractoServicio.ATM.SetATM.Proceso), ByRef transacion As Prosegur.DbHelper.Transacao)

        ' executa setProceso
        Dim accion As New AccionProceso
        Dim respuestaProceso As ContractoServicio.Proceso.SetProceso.Respuesta

        For Each proc In Procesos

            respuestaProceso = accion.SetProceso(proc.PeticionProceso, transacion)

            ' verifica se ocorreu um erro na execução da operação 
            If respuestaProceso.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                ' se ocorreu, gera exceção repassando o código e mensagem do erro
                Throw New Excepcion.NegocioExcepcion(respuestaProceso.CodigoError, respuestaProceso.MensajeError)
            End If

        Next

    End Sub

    Private Function ObtenerOidPtoServicio(CodCliente As String, CodSubcliente As String, CodPtoServicio As String) As String

        Dim oidCliente As String = IAC.AccesoDatos.Cliente.BuscarOidCliente(CodCliente)

        Dim oidSubcliente As String = IAC.AccesoDatos.SubCliente.BuscarOidSubCliente(CodSubcliente, oidCliente)

        Return IAC.AccesoDatos.PuntoServicio.BuscaOidPuntoServicio(CodPtoServicio, oidSubcliente)

    End Function

End Class
