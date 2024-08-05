Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports Prosegur.Genesis
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.ConfigurarDatosBancarios
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports Prosegur.Framework.Dicionario
Imports System.Linq
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon.Clases

Public Class DatoBancario

#Region "[EXCLUIR]"

    Public Shared Sub BajaDatosBancarios(IdentificadorCliente As String, IdentificadorSubCliente As String, IdentificadorPuntoServicio As String, ByRef Transacion As Prosegur.DbHelper.Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' query
        Dim query As New List(Of String)

        If Not String.IsNullOrEmpty(IdentificadorCliente) Then
            query.Add(" OID_CLIENTE = []OID_CLIENTE")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, IdentificadorCliente))
        Else
            query.Add(" OID_CLIENTE IS NULL ")
        End If
        If Not String.IsNullOrEmpty(IdentificadorSubCliente) Then
            query.Add(" OID_SUBCLIENTE = []OID_SUBCLIENTE")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, IdentificadorSubCliente))
        Else
            query.Add(" OID_SUBCLIENTE IS NULL ")

        End If
        If Not String.IsNullOrEmpty(IdentificadorPuntoServicio) Then
            query.Add(" OID_PTO_SERVICIO = []OID_PTO_SERVICIO")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, IdentificadorPuntoServicio))
        Else
            query.Add(" OID_PTO_SERVICIO IS NULL ")
        End If

        ' preparar query
        comando.CommandText = Util.PrepararQuery(String.Format(My.Resources.BajaDatoBancario, String.Join(" AND ", query)))
        comando.CommandType = CommandType.Text

        If Transacion Is Nothing Then
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        Else
            Transacion.AdicionarItemTransacao(comando)
        End If

    End Sub

#End Region

#Region "[INSERIR/ATUALIZAR]"

    Public Shared Sub AprobarRechazar(listaDatosBancariosCambio As List(Of String), usuario As String, accion As String, comentario As String, tester_aprobacion As Boolean, codigoPais As String)
        Dim ds As DataSet = Nothing
        Dim spw As SPWrapper = Nothing
        Try
            spw = armarWrapperAprobarRechazar(listaDatosBancariosCambio, usuario, accion, comentario, tester_aprobacion, codigoPais)
            ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GE, False, Nothing)
            PoblarRespuestaAprobarRechazar(ds)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Shared Function armarWrapperAprobarRechazar(listaDatosBancariosCambio As List(Of String), usuario As String, accion As String, comentario As String, tester_aprobacion As Boolean, codigoPais As String) As SPWrapper
        Dim SP As String = String.Format("SAPR_PDATO_BANCARIO_{0}.saprobar_rechazar", Prosegur.Genesis.Comon.Util.Version)
        Dim spw As New SPWrapper(SP, False)

        spw.AgregarParam("par$aoid_dato_banc_cambio", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$cod_accion", ProsegurDbType.Descricao_Longa, accion, , False)
        spw.AgregarParam("par$des_comentario", ProsegurDbType.Descricao_Longa, comentario, , False)
        spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Curta, usuario, , False)
        spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                Tradutor.CulturaSistema.Name,
                                                                If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
        spw.AgregarParam("par$tester_aprobacion", ProsegurDbType.Logico, tester_aprobacion, , False)
        spw.AgregarParamInfo("par$info_ejecucion")
        spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
        spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)
        spw.AgregarParam("par$cod_pais", ProsegurDbType.Descricao_Curta, codigoPais, , False)

        For Each dato_bancario_cambio In listaDatosBancariosCambio
            spw.Param("par$aoid_dato_banc_cambio").AgregarValorArray(dato_bancario_cambio)
        Next

        Return spw
    End Function

    Private Shared Sub PoblarRespuestaAprobarRechazar(ds As DataSet)
        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

            'Buscamos los mensajes de error y concatenamos la excepcion 
            'Con el mensaje de todas los errores
            Dim mensajeError = ""

            If ds.Tables.Contains("validaciones") AndAlso ds.Tables("validaciones").Rows.Count > 0 Then
                For Each row As DataRow In ds.Tables("validaciones").Rows
                    Dim resultado = Util.AtribuirValorObj(row("CODIGO"), GetType(String))
                    'Busco en los detalles si hay errores para agregar el tipo de resultado al cliente

                    If resultado.Substring(0, 1) <> "0" Then
                        'Lanzar exception
                        If Not String.IsNullOrWhiteSpace(mensajeError) Then
                            mensajeError += vbNewLine
                        End If
                        mensajeError += $"{Util.AtribuirValorObj(row("DESCRIPCION"), GetType(String))}"
                    End If
                Next
            End If

            If Not String.IsNullOrWhiteSpace(mensajeError) Then
                Throw New Excepcion.NegocioExcepcion(mensajeError)
            End If
        End If
    End Sub

    Public Shared Sub SetDatosBancarios(pPeticion As Peticion, Optional ByRef pTransaccion As DataBaseHelper.Transaccion = Nothing)
        'Dim objWrapper As 
        Dim ds As DataSet = Nothing
        Dim spw As SPWrapper = Nothing
        Try
            spw = armarWrapperConfigurarDatosBancarios(pPeticion)

            'ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GE, False, Nothing)
            If pTransaccion IsNot Nothing Then
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GE, False, pTransaccion)
            Else
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GE, False, Nothing)
            End If

            PoblarRespuesta(ds)


        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Shared Function armarWrapperConfigurarDatosBancarios(pPeticion As Peticion) As SPWrapper
        Dim SP As String = String.Format("SAPR_PDATO_BANCARIO_{0}.sconfigurar_dato_bancario", Prosegur.Genesis.Comon.Util.Version)
        Dim spw As New SPWrapper(SP, False)
        Dim codigo_usuario As String = "SCONFIGURAR_DATO_BANCARIO"

        spw.AgregarParam("par$anel_index", ProsegurDbType.Inteiro_Curto, Nothing, , True)
        spw.AgregarParam("par$acod_entidad", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$acod_accion", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$aoid_dato_bancario", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$aoid_cliente", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$aoid_subcliente", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$aoid_pto_servicio", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$acod_banco", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$acod_agencia", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$acod_tipo_cuenta_bancaria", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$acod_cuenta_bancaria", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$acod_documento", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

        spw.AgregarParam("par$ades_titularidad", ProsegurDbType.Descricao_Longa, Nothing, , True)
        spw.AgregarParam("par$acod_divisa", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$ades_observaciones", ProsegurDbType.Descricao_Longa, Nothing, , True)
        spw.AgregarParam("par$abol_defecto", ProsegurDbType.Logico, Nothing, , True)
        spw.AgregarParam("par$abol_activo", ProsegurDbType.Logico, Nothing, , True)
        spw.AgregarParam("par$ades_campo_adicional_1", ProsegurDbType.Descricao_Longa, Nothing, , True)
        spw.AgregarParam("par$ades_campo_adicional_2", ProsegurDbType.Descricao_Longa, Nothing, , True)
        spw.AgregarParam("par$ades_campo_adicional_3", ProsegurDbType.Descricao_Longa, Nothing, , True)
        spw.AgregarParam("par$ades_campo_adicional_4", ProsegurDbType.Descricao_Longa, Nothing, , True)
        spw.AgregarParam("par$ades_campo_adicional_5", ProsegurDbType.Descricao_Longa, Nothing, , True)
        spw.AgregarParam("par$ades_campo_adicional_6", ProsegurDbType.Descricao_Longa, Nothing, , True)
        spw.AgregarParam("par$ades_campo_adicional_7", ProsegurDbType.Descricao_Longa, Nothing, , True)
        spw.AgregarParam("par$ades_campo_adicional_8", ProsegurDbType.Descricao_Longa, Nothing, , True)
        spw.AgregarParam("par$ades_comentario", ProsegurDbType.Descricao_Longa, Nothing, , True)



        spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                Tradutor.CulturaSistema.Name,
                                                                If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
        If pPeticion.Usuario IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(pPeticion.Usuario) Then
            codigo_usuario = pPeticion.Usuario
        End If
        spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Longa, codigo_usuario, , False)
        spw.AgregarParam("par$cod_pais", ProsegurDbType.Identificador_Alfanumerico, pPeticion.CodigoPais, , False)



        spw.AgregarParamInfo("par$info_ejecucion")
        'Se agrega parametro indicando que limpia la tabla SAPR_GTT_TAUXILIAR
        spw.AgregarParam("par$bol_limpiar_temporal", ProsegurDbType.Inteiro_Curto, 1, , False)

        spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
        spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)

        Dim indice As Integer = 0
        For Each dato_bancario In pPeticion.DatosBancarios
            spw.Param("par$anel_index").AgregarValorArray(indice)
            If Not String.IsNullOrEmpty(dato_bancario.IdentificadorPuntoDeServicio) Then
                spw.Param("par$acod_entidad").AgregarValorArray("PUNTOSERVICIO")
            ElseIf Not String.IsNullOrEmpty(dato_bancario.IdentificadorSubCliente) Then
                spw.Param("par$acod_entidad").AgregarValorArray("SUBCLIENTE")
            ElseIf Not String.IsNullOrEmpty(dato_bancario.IdentificadorCliente) Then
                spw.Param("par$acod_entidad").AgregarValorArray("CLIENTE")
            End If

            spw.Param("par$acod_accion").AgregarValorArray(dato_bancario.Accion.ToString().ToUpper())

            If Not String.IsNullOrEmpty(dato_bancario.Identificador) Then
                spw.Param("par$aoid_dato_bancario").AgregarValorArray(dato_bancario.Identificador)
            Else
                spw.Param("par$aoid_dato_bancario").AgregarValorArray(DBNull.Value)
            End If

            AgregarParametroAlWrapper(spw, "par$aoid_cliente", dato_bancario.IdentificadorCliente)
            AgregarParametroAlWrapper(spw, "par$aoid_subcliente", dato_bancario.IdentificadorSubCliente)
            AgregarParametroAlWrapper(spw, "par$aoid_pto_servicio", dato_bancario.IdentificadorPuntoDeServicio)
            AgregarParametroAlWrapper(spw, "par$acod_banco", dato_bancario.CodigoBanco)
            AgregarParametroAlWrapper(spw, "par$acod_tipo_cuenta_bancaria", dato_bancario.Tipo)
            AgregarParametroAlWrapper(spw, "par$acod_cuenta_bancaria", dato_bancario.NumeroCuenta)
            AgregarParametroAlWrapper(spw, "par$acod_documento", dato_bancario.NumeroDocumento)
            AgregarParametroAlWrapper(spw, "par$ades_titularidad", dato_bancario.Titularidad)
            AgregarParametroAlWrapper(spw, "par$acod_divisa", dato_bancario.CodigoDivisa)
            AgregarParametroAlWrapper(spw, "par$ades_observaciones", dato_bancario.Observaciones)

            AgregarParametroAlWrapper(spw, "par$acod_agencia", dato_bancario.CodigoAgencia)

            If dato_bancario.Patron Is Nothing Then
                If dato_bancario.Accion = Genesis.ContractoServicio.Contractos.Integracion.Comon.Enumeradores.AccionABM.Alta Then
                    spw.Param("par$abol_defecto").AgregarValorArray(0)
                Else
                    spw.Param("par$abol_defecto").AgregarValorArray(DBNull.Value)
                End If
            ElseIf dato_bancario.Patron = "1" OrElse dato_bancario.Patron.ToUpper = "TRUE" Then
                spw.Param("par$abol_defecto").AgregarValorArray(1)
            Else
                spw.Param("par$abol_defecto").AgregarValorArray(0)
            End If

            If dato_bancario.Accion = Genesis.ContractoServicio.Contractos.Integracion.Comon.Enumeradores.AccionABM.Baja Then
                spw.Param("par$abol_activo").AgregarValorArray(0)
            Else
                spw.Param("par$abol_activo").AgregarValorArray(1)
            End If

            AgregarParametroAlWrapper(spw, "par$ades_campo_adicional_1", dato_bancario.CampoAdicional1)
            AgregarParametroAlWrapper(spw, "par$ades_campo_adicional_2", dato_bancario.CampoAdicional2)
            AgregarParametroAlWrapper(spw, "par$ades_campo_adicional_3", dato_bancario.CampoAdicional3)
            AgregarParametroAlWrapper(spw, "par$ades_campo_adicional_4", dato_bancario.CampoAdicional4)
            AgregarParametroAlWrapper(spw, "par$ades_campo_adicional_5", dato_bancario.CampoAdicional5)
            AgregarParametroAlWrapper(spw, "par$ades_campo_adicional_6", dato_bancario.CampoAdicional6)
            AgregarParametroAlWrapper(spw, "par$ades_campo_adicional_7", dato_bancario.CampoAdicional7)
            AgregarParametroAlWrapper(spw, "par$ades_campo_adicional_8", dato_bancario.CampoAdicional8)
            AgregarParametroAlWrapper(spw, "par$ades_comentario", dato_bancario.Comentario)

            indice += 1
        Next

        Return spw
    End Function

    ''' <summary>
    ''' Se encarga de agregar parametros en el wrapper considerando los valores vacios
    ''' </summary>
    ''' <param name="spw"> Objeto SPWrapper </param>
    ''' <param name="nombreParametro"> Nombre del parametro para el SP</param>
    ''' <param name="valorParametro"> Valor del parametro para el SP </param>
    Private Shared Sub AgregarParametroAlWrapper(ByRef spw As SPWrapper, ByRef nombreParametro As String, ByRef valorParametro As String)
        If valorParametro Is Nothing Then
            spw.Param(nombreParametro).AgregarValorArray(DBNull.Value)
        ElseIf valorParametro.Equals(String.Empty) Then
            spw.Param(nombreParametro).AgregarValorArray(Comon.Constantes.CONST_VACIO_PARA_BBDD)
        Else
            spw.Param(nombreParametro).AgregarValorArray(valorParametro)
        End If
    End Sub

    Private Shared Sub PoblarRespuesta(ds As DataSet)
        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

            'Buscamos los mensajes de error y concatenamos la excepcion 
            'Con el mensaje de todas los errores
            Dim mensajeError = ""

            If ds.Tables.Contains("validaciones") AndAlso ds.Tables("validaciones").Rows.Count > 0 Then
                For Each row As DataRow In ds.Tables("validaciones").Rows
                    Dim resultado = Util.AtribuirValorObj(row("CODIGO"), GetType(String))
                    'Busco en los detalles si hay errores para agregar el tipo de resultado al cliente

                    If resultado.Substring(0, 1) <> "0" Then
                        'Lanzar exception
                        If Not String.IsNullOrWhiteSpace(mensajeError) Then
                            mensajeError += vbNewLine
                        End If
                        mensajeError += $"{resultado} - {Util.AtribuirValorObj(row("DESCRIPCION"), GetType(String))}"
                    End If
                Next
            End If

            If Not String.IsNullOrWhiteSpace(mensajeError) Then
                Throw New Excepcion.NegocioExcepcion(mensajeError)
            End If
        End If
    End Sub

    Public Shared Sub SetDatosBancarios(objPeticion As List(Of Comon.Clases.DatoBancario),
                                        IdentificadorCliente As String,
                                        IdentificadorSubCliente As String,
                                        IdentificadorPuntoServicio As String,
                                        CodigoUsuario As String,
                                        ByRef Transacion As Prosegur.DbHelper.Transacao)

        For Each objDatoBancario In objPeticion

            ' criar comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DATO_BANCARIO", ProsegurDbType.Objeto_Id, objDatoBancario.Identificador))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_BANCO", ProsegurDbType.Objeto_Id, objDatoBancario.Banco.Identificador))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DIVISA", ProsegurDbType.Objeto_Id, objDatoBancario.Divisa.Identificador))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_CUENTA_BANCARIA", ProsegurDbType.Observacao_Longa, objDatoBancario.CodigoTipoCuentaBancaria.ToUpper))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CUENTA_BANCARIA", ProsegurDbType.Observacao_Longa, objDatoBancario.CodigoCuentaBancaria))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DOCUMENTO", ProsegurDbType.Observacao_Longa, objDatoBancario.CodigoDocumento))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TITULARIDAD", ProsegurDbType.Observacao_Longa, objDatoBancario.DescripcionTitularidad))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_OBSERVACIONES", ProsegurDbType.Observacao_Longa, objDatoBancario.DescripcionObs))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_DEFECTO", ProsegurDbType.Logico, objDatoBancario.bolDefecto))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, True))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data_Hora, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Observacao_Longa, CodigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Observacao_Longa, CodigoUsuario))


            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_AGENCIA", ProsegurDbType.Observacao_Longa, objDatoBancario.CodigoAgencia))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CAMPO_ADICIONAL_1", ProsegurDbType.Observacao_Longa, objDatoBancario.DescripcionAdicionalCampo1))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CAMPO_ADICIONAL_2", ProsegurDbType.Observacao_Longa, objDatoBancario.DescripcionAdicionalCampo2))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CAMPO_ADICIONAL_3", ProsegurDbType.Observacao_Longa, objDatoBancario.DescripcionAdicionalCampo3))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CAMPO_ADICIONAL_4", ProsegurDbType.Observacao_Longa, objDatoBancario.DescripcionAdicionalCampo4))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CAMPO_ADICIONAL_5", ProsegurDbType.Observacao_Longa, objDatoBancario.DescripcionAdicionalCampo5))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CAMPO_ADICIONAL_6", ProsegurDbType.Observacao_Longa, objDatoBancario.DescripcionAdicionalCampo6))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CAMPO_ADICIONAL_7", ProsegurDbType.Observacao_Longa, objDatoBancario.DescripcionAdicionalCampo7))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CAMPO_ADICIONAL_8", ProsegurDbType.Observacao_Longa, objDatoBancario.DescripcionAdicionalCampo8))




            If Not String.IsNullOrEmpty(IdentificadorCliente) Then
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, IdentificadorCliente))
            Else
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, Nothing))
            End If
            If Not String.IsNullOrEmpty(IdentificadorSubCliente) Then
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, IdentificadorSubCliente))
            Else
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, Nothing))
            End If
            If Not String.IsNullOrEmpty(IdentificadorPuntoServicio) Then
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, IdentificadorPuntoServicio))
            Else
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, Nothing))
            End If

            ' preparar query
            comando.CommandText = Util.PrepararQuery(My.Resources.SetDatoBancario.ToString)
            comando.CommandType = CommandType.Text

            If Transacion Is Nothing Then
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
            Else
                Transacion.AdicionarItemTransacao(comando)
            End If

        Next

    End Sub

    Public Shared Sub AlterarBolDefectoDatoBancario(identifiador As String, defecto As Boolean, ByRef Transacion As Prosegur.DbHelper.Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' preparar query
        comando.CommandText = Util.PrepararQuery(My.Resources.AlterarBolDefecto.ToString)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DATO_BANCARIO", ProsegurDbType.Objeto_Id, identifiador))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_DEFECTO", ProsegurDbType.Logico, defecto))

        If Transacion Is Nothing Then
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        Else
            Transacion.AdicionarItemTransacao(comando)
        End If

    End Sub

#End Region

#Region "[CONSULTAR]"

    Public Shared Function GetDatosBancarios(objPeticion As ContractoServicio.DatoBancario.GetDatosBancarios.Peticion) As List(Of Comon.Clases.DatoBancario)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.GetDatoBancario.ToString)

        If Not String.IsNullOrEmpty(objPeticion.IdentificadorCliente) Then
            query.AppendLine(" AND DATB.OID_CLIENTE = []OID_CLIENTE")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, objPeticion.IdentificadorCliente))
        End If
        If Not String.IsNullOrEmpty(objPeticion.IdentificadorSubCliente) Then
            query.AppendLine(" AND DATB.OID_SUBCLIENTE = []OID_SUBCLIENTE")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, objPeticion.IdentificadorSubCliente))
        Else
            query.AppendLine(" AND DATB.OID_SUBCLIENTE IS NULL")
        End If
        If Not String.IsNullOrEmpty(objPeticion.IdentificadorPuntoServicio) Then
            query.AppendLine(" AND DATB.OID_PTO_SERVICIO = []OID_PTO_SERVICIO")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, objPeticion.IdentificadorPuntoServicio))
        Else
            query.AppendLine(" AND DATB.OID_PTO_SERVICIO IS NULL")
        End If
        If Not String.IsNullOrEmpty(objPeticion.IdentificadorBanco) Then
            query.AppendLine(" AND DATB.OID_BANCO = []OID_BANCO")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_BANCO", ProsegurDbType.Objeto_Id, objPeticion.IdentificadorBanco))
        End If
        If Not String.IsNullOrEmpty(objPeticion.IdentificadorDivisa) Then
            query.AppendLine(" AND DATB.OID_DIVISA = []OID_DIVISA")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DIVISA", ProsegurDbType.Objeto_Id, objPeticion.IdentificadorDivisa))
        End If

        ' preparar query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' criar objeto coleccion
        Dim objDatoBancarios As New List(Of Comon.Clases.DatoBancario)

        ' executar query
        Dim dtRetorno As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' caso encontre algum registro
        If dtRetorno IsNot Nothing _
            AndAlso dtRetorno.Rows.Count > 0 Then

            ' percorrer registros encontrados
            For Each dr As DataRow In dtRetorno.Rows

                ' preencher a coleção com objetos
                objDatoBancarios.Add(PopularGetDatosBancarios(dr))

            Next

        End If

        ' retornar coleção
        Return objDatoBancarios

    End Function
    Public Shared Function GetDatosBancariosDefectos(objPeticion As List(Of ContractoServicio.DatoBancario.GetDatosBancarios.Peticion)) As List(Of Comon.Clases.DatoBancario)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.GetDatoBancario.ToString)

        Dim identificadoresClientes As New List(Of String)
        Dim identificadoresSubClientes As New List(Of String)
        Dim identificadoresPtoServicios As New List(Of String)
        Dim identificadoresDivisas As New List(Of String)

        objPeticion.ForEach(Sub(p)
                                identificadoresClientes.Add(p.IdentificadorCliente)
                                identificadoresSubClientes.Add(p.IdentificadorSubCliente)
                                identificadoresPtoServicios.Add(p.IdentificadorPuntoServicio)
                                identificadoresDivisas.Add(p.IdentificadorDivisa)
                            End Sub)

        query.Append(" AND DATB.BOL_DEFECTO = 1 ")

        query.Append(Util.MontarClausulaIn(identificadoresClientes, "OID_CLIENTE", comando, "AND", "DATB"))

        If identificadoresSubClientes.Count > 0 Then
            query.Append(Util.MontarClausulaIn(identificadoresClientes, "OID_SUBCLIENTE", comando, "AND", "DATB"))
        End If

        If identificadoresPtoServicios.Count > 0 Then
            query.Append(Util.MontarClausulaIn(identificadoresClientes, "OID_PTO_SERVICIO", comando, "AND", "DATB"))
        End If

        If identificadoresDivisas.Count > 0 Then
            query.Append(Util.MontarClausulaIn(identificadoresClientes, "OID_DIVISA", comando, "AND", "DATB"))
        End If

        ' preparar query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' criar objeto coleccion
        Dim objDatoBancarios As New List(Of Comon.Clases.DatoBancario)

        ' executar query
        Dim dtRetorno As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' caso encontre algum registro
        If dtRetorno IsNot Nothing _
            AndAlso dtRetorno.Rows.Count > 0 Then

            ' percorrer registros encontrados
            For Each dr As DataRow In dtRetorno.Rows

                ' preencher a coleção com objetos
                objDatoBancarios.Add(PopularGetDatosBancarios(dr))

            Next

        End If

        ' retornar coleção
        Return objDatoBancarios

    End Function

    Private Shared Function PopularGetDatosBancarios(dr As DataRow) As Comon.Clases.DatoBancario

        ' criar objeto
        Dim objDatoBancario As New Comon.Clases.DatoBancario

        Util.AtribuirValorObjeto(objDatoBancario.Identificador, dr("OID_DATO_BANCARIO"), GetType(String))
        If dr("OID_BANCO") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(dr("OID_BANCO").ToString()) Then
            objDatoBancario.Banco = New Comon.Clases.Cliente
            Util.AtribuirValorObjeto(objDatoBancario.Banco.Identificador, dr("OID_BANCO"), GetType(String))
            Util.AtribuirValorObjeto(objDatoBancario.Banco.Codigo, dr("COD_BANCO"), GetType(String))
            Util.AtribuirValorObjeto(objDatoBancario.Banco.Descripcion, dr("DES_BANCO"), GetType(String))
        End If
        If dr("OID_CLIENTE") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(dr("OID_CLIENTE").ToString()) Then
            objDatoBancario.Cliente = New Comon.Clases.Cliente
            Util.AtribuirValorObjeto(objDatoBancario.Cliente.Identificador, dr("OID_CLIENTE"), GetType(String))
        End If
        If dr("OID_SUBCLIENTE") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(dr("OID_SUBCLIENTE").ToString()) Then
            objDatoBancario.SubCliente = New Comon.Clases.SubCliente
            Util.AtribuirValorObjeto(objDatoBancario.SubCliente.Identificador, dr("OID_SUBCLIENTE"), GetType(String))
        End If
        If dr("OID_PTO_SERVICIO") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(dr("OID_PTO_SERVICIO").ToString()) Then
            objDatoBancario.PuntoServicio = New Comon.Clases.PuntoServicio
            Util.AtribuirValorObjeto(objDatoBancario.PuntoServicio.Identificador, dr("OID_PTO_SERVICIO"), GetType(String))
        End If
        If dr("OID_DIVISA") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(dr("OID_DIVISA").ToString()) Then
            objDatoBancario.Divisa = New Comon.Clases.Divisa
            Util.AtribuirValorObjeto(objDatoBancario.Divisa.Identificador, dr("OID_DIVISA"), GetType(String))
            Util.AtribuirValorObjeto(objDatoBancario.Divisa.Descripcion, dr("DES_DIVISA"), GetType(String))
            Util.AtribuirValorObjeto(objDatoBancario.Divisa.CodigoISO, dr("COD_ISO_DIVISA"), GetType(String))
        End If
        Dim tipoCuenta As String = Nothing
        Util.AtribuirValorObjeto(objDatoBancario.CodigoTipoCuentaBancaria, dr("COD_TIPO_CUENTA_BANCARIA"), GetType(String))

        Util.AtribuirValorObjeto(objDatoBancario.CodigoCuentaBancaria, dr("COD_CUENTA_BANCARIA"), GetType(String))
        Util.AtribuirValorObjeto(objDatoBancario.CodigoDocumento, dr("COD_DOCUMENTO"), GetType(String))
        Util.AtribuirValorObjeto(objDatoBancario.DescripcionTitularidad, dr("DES_TITULARIDAD"), GetType(String))
        Util.AtribuirValorObjeto(objDatoBancario.DescripcionObs, dr("DES_OBSERVACIONES"), GetType(String))
        Util.AtribuirValorObjeto(objDatoBancario.bolDefecto, dr("BOL_DEFECTO"), GetType(Boolean))


        If dr("COD_AGENCIA") Is DBNull.Value Then
            objDatoBancario.CodigoAgencia = String.Empty
        Else
            Util.AtribuirValorObjeto(objDatoBancario.CodigoAgencia, dr("COD_AGENCIA"), GetType(String))
        End If

        Util.AtribuirValorObjeto(objDatoBancario.DescripcionAdicionalCampo1, dr("DES_CAMPO_ADICIONAL_1"), GetType(String))
        Util.AtribuirValorObjeto(objDatoBancario.DescripcionAdicionalCampo2, dr("DES_CAMPO_ADICIONAL_2"), GetType(String))
        Util.AtribuirValorObjeto(objDatoBancario.DescripcionAdicionalCampo3, dr("DES_CAMPO_ADICIONAL_3"), GetType(String))
        Util.AtribuirValorObjeto(objDatoBancario.DescripcionAdicionalCampo4, dr("DES_CAMPO_ADICIONAL_4"), GetType(String))
        Util.AtribuirValorObjeto(objDatoBancario.DescripcionAdicionalCampo5, dr("DES_CAMPO_ADICIONAL_5"), GetType(String))
        Util.AtribuirValorObjeto(objDatoBancario.DescripcionAdicionalCampo6, dr("DES_CAMPO_ADICIONAL_6"), GetType(String))
        Util.AtribuirValorObjeto(objDatoBancario.DescripcionAdicionalCampo7, dr("DES_CAMPO_ADICIONAL_7"), GetType(String))
        Util.AtribuirValorObjeto(objDatoBancario.DescripcionAdicionalCampo8, dr("DES_CAMPO_ADICIONAL_8"), GetType(String))
        Util.AtribuirValorObjeto(objDatoBancario.Pendiente, dr("PENDIENTE"), GetType(Boolean))

        ' retornar objeto preenchido
        Return objDatoBancario

    End Function

    Public Shared Function RecuperarDatosBancariosCambio(pPeticion As Contractos.Integracion.RecuperarDatosBancarios.Peticion) As List(Of Comon.Clases.DatoBancarioGrilla)
        Dim ds As DataSet = Nothing
        Dim spw As SPWrapper = Nothing
        Try
            spw = armarWrapperRecuperarDatosBancariosCambio(pPeticion)
            ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GE, False, Nothing)
            Return PoblarRespuestaRecuperarDatosBancariosCambio(ds)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Shared Function armarWrapperRecuperarDatosBancariosCambio(pPeticion As Contractos.Integracion.RecuperarDatosBancarios.Peticion) As SPWrapper

        Dim SP As String = String.Format("SAPR_PDATO_BANCARIO_{0}.SRECUPERAR_DATOS", Prosegur.Genesis.Comon.Util.Version)
        Dim spw As New SPWrapper(SP, False)

        spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Longa, pPeticion.CodigoUsuario, , False)
        spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                Tradutor.CulturaSistema.Name,
                                                                If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)

        spw.AgregarParam("par$cod_pais", ProsegurDbType.Descricao_Longa, pPeticion.CodigoPais, , False)
        spw.AgregarParam("par$acod_estado", ProsegurDbType.Descricao_Curta, Nothing, , True)
        spw.AgregarParam("par$acod_campo", ProsegurDbType.Descricao_Longa, Nothing, , True)

        spw.AgregarParam("par$aoid_cliente", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$aoid_subcliente", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$aoid_pto_servicio", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

        spw.AgregarParam("par$aoid_usu_aprob", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$aoid_usu_modif", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

        spw.AgregarParam("par$rc_cambios", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "cambios")
        spw.AgregarParam("par$rc_datos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "datos")
        spw.AgregarParam("par$rc_aprob", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "aprob")
        spw.AgregarParam("par$cod_tipo_fecha", ProsegurDbType.Descricao_Longa, pPeticion.CodigoTipoFecha, , False)
        spw.AgregarParam("par$fecha_desde", ProsegurDbType.Data_Hora, pPeticion.FechaDesde, , False)
        spw.AgregarParam("par$fecha_hasta", ProsegurDbType.Data_Hora, pPeticion.FechaHasta, , False)

        For Each estado As String In pPeticion.CodigosEstados
            spw.Param("par$acod_estado").AgregarValorArray(estado)
        Next

        For Each campo As String In pPeticion.CodigosCampos
            spw.Param("par$acod_campo").AgregarValorArray(campo)
        Next

        For Each oid_cliente As String In pPeticion.OidClientes
            spw.Param("par$aoid_cliente").AgregarValorArray(oid_cliente)
        Next


        For Each oid_subcliente As String In pPeticion.OidSubClientes
            spw.Param("par$aoid_subcliente").AgregarValorArray(oid_subcliente)
        Next

        For Each oid_pto_servicio As String In pPeticion.OidPuntosServicios
            spw.Param("par$aoid_pto_servicio").AgregarValorArray(oid_pto_servicio)
        Next

        For Each oid_usu_modificacion As String In pPeticion.OidUsuariosModificacion
            spw.Param("par$aoid_usu_modif").AgregarValorArray(oid_usu_modificacion)
        Next

        For Each oid_usu_aprobacion As String In pPeticion.OidUsuariosAprobacion
            spw.Param("par$aoid_usu_aprob").AgregarValorArray(oid_usu_aprobacion)
        Next

        Return spw
    End Function

    Private Shared Function PoblarRespuestaRecuperarDatosBancariosCambio(ds As DataSet) As List(Of Comon.Clases.DatoBancarioGrilla)
        Dim Resultado = New List(Of Comon.Clases.DatoBancarioGrilla)

        If ds.Tables("datos") IsNot Nothing Then
            For Each row In ds.Tables("datos").Rows
                Dim datoBancarioG = New Comon.Clases.DatoBancarioGrilla
                Util.AtribuirValorObjeto(datoBancarioG.OidDatoBancario, row("OID_DATO_BANCARIO"), GetType(String))
                Util.AtribuirValorObjeto(datoBancarioG.Cliente, row("CLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(datoBancarioG.SubCliente, row("SUBCLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(datoBancarioG.PuntoServicio, row("PUNTO"), GetType(String))
                Util.AtribuirValorObjeto(datoBancarioG.Divisa, row("DIVISA"), GetType(String))

                Util.AtribuirValorObjeto(datoBancarioG.CantAprobados, row("CANT_APROBADOS"), GetType(Integer))
                Util.AtribuirValorObjeto(datoBancarioG.CantPendientes, row("CANT_PENDIENTES"), GetType(Integer))
                Util.AtribuirValorObjeto(datoBancarioG.CantRechazados, row("CANT_RECHAZADOS"), GetType(Integer))

                If ds.Tables("cambios") IsNot Nothing Then
                    For Each rowDetalle In ds.Tables("cambios").Rows
                        Dim detalleBancarioG = New Comon.Clases.DatoBancarioGrillaDetalle
                        If rowDetalle("OID_DATO_BANCARIO") = row("OID_DATO_BANCARIO") Then
                            Util.AtribuirValorObjeto(detalleBancarioG.OidDatoBancarioCambio, rowDetalle("OID_DATO_BANCARIO_CAMBIO"), GetType(String))
                            Util.AtribuirValorObjeto(detalleBancarioG.CampoModificado, rowDetalle("CAMPO_TRADUCCION"), GetType(String))

                            Select Case rowDetalle("COD_CAMPO")
                                Case "OID_DIVISA"
                                    Util.AtribuirValorObjeto(detalleBancarioG.ValorActual, row("DIVISA"), GetType(String))
                                Case "OID_BANCO"
                                    Util.AtribuirValorObjeto(detalleBancarioG.ValorActual, row("BANCO"), GetType(String))
                                Case Else
                                    Util.AtribuirValorObjeto(detalleBancarioG.ValorActual, row(rowDetalle("COD_CAMPO")), GetType(String))
                            End Select

                            Util.AtribuirValorObjeto(detalleBancarioG.LoginUsuario, rowDetalle("DES_LOGIN"), GetType(String))
                            Util.AtribuirValorObjeto(detalleBancarioG.ValorModificado, rowDetalle("DES_VALOR"), GetType(String))
                            Util.AtribuirValorObjeto(detalleBancarioG.FechaCambio, rowDetalle("FYH_MODIFICACION"), GetType(Date))
                            Util.AtribuirValorObjeto(detalleBancarioG.UsuarioModificacion, $"{rowDetalle("DES_LOGIN")} - {rowDetalle("DES_NOMBRE")} {rowDetalle("DES_APELLIDO")}", GetType(String))
                            Util.AtribuirValorObjeto(detalleBancarioG.Estado, rowDetalle("COD_ESTADO"), GetType(String))
                            Util.AtribuirValorObjeto(detalleBancarioG.AprobacionesNecesarias, rowDetalle("APROBACIONES_NECESARIAS"), GetType(String))
                            Dim aprobacion = New Comon.Clases.AprobacionesGrillaDetalle
                            If ds.Tables("aprob") IsNot Nothing Then
                                For Each rowAprob In ds.Tables("aprob").Rows

                                    If rowAprob("OID_DATO_BANCARIO_CAMBIO") = rowDetalle("OID_DATO_BANCARIO_CAMBIO") Then
                                        Dim usuario As New Comon.Clases.DatoBancarioAprobador
                                        Util.AtribuirValorObjeto(usuario.Login, rowAprob("LOGIN"), GetType(String))
                                        Util.AtribuirValorObjeto(usuario.Usuario, rowAprob("USUARIO"), GetType(String))
                                        Util.AtribuirValorObjeto(usuario.FechaAprobacion, rowAprob("FYH_APROBACION"), GetType(String))

                                        aprobacion.UsuariosAprobadores.Add(usuario)

                                    End If

                                Next
                                detalleBancarioG.Aprobaciones = aprobacion
                                datoBancarioG.Detalle.Add(detalleBancarioG)

                            End If

                        End If
                    Next
                End If
                If (Not Resultado.Any(Function(a) a.OidDatoBancario = datoBancarioG.OidDatoBancario)) Then
                    Resultado.Add(datoBancarioG)
                End If
            Next


        End If

        Return Resultado
    End Function


    Public Function RecuperarDatoBancarioComparativo(identificador As String) As DatoBancarioComparativo
        Dim ds As DataSet = Nothing
        Dim spw As SPWrapper = Nothing
        Try
            spw = armarWrapperRecuperarDatoBancarioComparativo(identificador)
            ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GE, False, Nothing)
            Return PoblarRespuestaRecuperarDatoBancarioComparativo(ds)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function armarWrapperRecuperarDatoBancarioComparativo(identificador As String)
        Dim SP As String = String.Format("SAPR_PDATO_BANCARIO_{0}.srecuperar_comparativo", Prosegur.Genesis.Comon.Util.Version)
        Dim spw As New SPWrapper(SP, False)

        spw.AgregarParam("par$oid_dato_bancario", ProsegurDbType.Identificador_Alfanumerico, identificador, , False)
        spw.AgregarParam("par$rc_datos_bancarios", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "datos")
        spw.AgregarParam("par$rc_datos_bancarios_cambio", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "cambio")

        Return spw
    End Function

    Private Function PoblarRespuestaRecuperarDatoBancarioComparativo(ds As DataSet) As DatoBancarioComparativo
        'Inicializamos el objeto de respuesta
        Dim respuesta As New DatoBancarioComparativo

        If ds.Tables("datos") IsNot Nothing AndAlso ds.Tables("datos").Rows.Count > 0 Then
            respuesta.DatoBancarioOriginal = New Comon.Clases.DatoBancario With {
                    .Banco = New Comon.Clases.Cliente,
                    .Divisa = New Comon.Clases.Divisa
                }
            For Each row In ds.Tables("datos").Rows
                Util.AtribuirValorObjeto(respuesta.DatoBancarioOriginal.Banco.Descripcion, row("DES_BANCO"), GetType(String))
                Util.AtribuirValorObjeto(respuesta.DatoBancarioOriginal.CodigoDocumento, row("COD_DOCUMENTO"), GetType(String))
                Util.AtribuirValorObjeto(respuesta.DatoBancarioOriginal.CodigoAgencia, row("COD_AGENCIA"), GetType(String))
                Util.AtribuirValorObjeto(respuesta.DatoBancarioOriginal.Divisa.Descripcion, row("DES_DIVISA"), GetType(String))
                Util.AtribuirValorObjeto(respuesta.DatoBancarioOriginal.DescripcionObs, row("DES_OBSERVACIONES"), GetType(String))
                Util.AtribuirValorObjeto(respuesta.DatoBancarioOriginal.bolDefecto, row("BOL_DEFECTO"), GetType(Boolean))
                Util.AtribuirValorObjeto(respuesta.DatoBancarioOriginal.DescripcionTitularidad, row("DES_TITULARIDAD"), GetType(String))
                Util.AtribuirValorObjeto(respuesta.DatoBancarioOriginal.CodigoTipoCuentaBancaria, row("COD_TIPO_CUENTA_BANCARIA"), GetType(String))
                Util.AtribuirValorObjeto(respuesta.DatoBancarioOriginal.CodigoCuentaBancaria, row("COD_CUENTA_BANCARIA"), GetType(String))
                Util.AtribuirValorObjeto(respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo1, row("DES_CAMPO_ADICIONAL_1"), GetType(String))
                Util.AtribuirValorObjeto(respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo2, row("DES_CAMPO_ADICIONAL_2"), GetType(String))
                Util.AtribuirValorObjeto(respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo3, row("DES_CAMPO_ADICIONAL_3"), GetType(String))
                Util.AtribuirValorObjeto(respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo4, row("DES_CAMPO_ADICIONAL_4"), GetType(String))
                Util.AtribuirValorObjeto(respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo5, row("DES_CAMPO_ADICIONAL_5"), GetType(String))
                Util.AtribuirValorObjeto(respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo6, row("DES_CAMPO_ADICIONAL_6"), GetType(String))
                Util.AtribuirValorObjeto(respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo7, row("DES_CAMPO_ADICIONAL_7"), GetType(String))
                Util.AtribuirValorObjeto(respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo8, row("DES_CAMPO_ADICIONAL_8"), GetType(String))
            Next
        End If

        If ds.Tables("cambio") IsNot Nothing AndAlso ds.Tables("cambio").Rows.Count > 0 Then
            respuesta.DatoBancarioCambio = New Comon.Clases.DatoBancario With {
                    .Banco = New Comon.Clases.Cliente,
                    .Divisa = New Comon.Clases.Divisa
                }
            For Each row In ds.Tables("cambio").Rows
                Select Case row("COD_CAMPO").ToString
                    Case "OID_BANCO"
                        respuesta.DatoBancarioCambio.Banco.Descripcion = row("DES_VALOR").ToString()
                        respuesta.DatoBancarioCambio.Banco.Codigo = row("COD_BANCARIO").ToString()
                    Case "OID_DIVISA"
                        respuesta.DatoBancarioCambio.Divisa.Descripcion = row("DES_VALOR").ToString()
                    Case "COD_AGENCIA"
                        respuesta.DatoBancarioCambio.CodigoAgencia = row("DES_VALOR").ToString()
                    Case "COD_TIPO_CUENTA_BANCARIA"
                        respuesta.DatoBancarioCambio.CodigoTipoCuentaBancaria = row("DES_VALOR").ToString()
                    Case "COD_CUENTA_BANCARIA"
                        respuesta.DatoBancarioCambio.CodigoCuentaBancaria = row("DES_VALOR").ToString()
                    Case "COD_DOCUMENTO"
                        respuesta.DatoBancarioCambio.CodigoDocumento = row("DES_VALOR").ToString()
                    Case "DES_TITULARIDAD"
                        respuesta.DatoBancarioCambio.DescripcionTitularidad = row("DES_VALOR").ToString()
                    Case "DES_OBSERVACIONES"
                        respuesta.DatoBancarioCambio.DescripcionObs = row("DES_VALOR").ToString()
                    Case "BOL_DEFECTO"
                        Util.AtribuirValorObjeto(respuesta.DatoBancarioCambio.bolDefecto, row("DES_VALOR"), GetType(Boolean))
                    Case "DES_CAMPO_ADICIONAL_1"
                        respuesta.DatoBancarioCambio.DescripcionAdicionalCampo1 = row("DES_VALOR").ToString()
                    Case "DES_CAMPO_ADICIONAL_2"
                        respuesta.DatoBancarioCambio.DescripcionAdicionalCampo2 = row("DES_VALOR").ToString()
                    Case "DES_CAMPO_ADICIONAL_3"
                        respuesta.DatoBancarioCambio.DescripcionAdicionalCampo3 = row("DES_VALOR").ToString()
                    Case "DES_CAMPO_ADICIONAL_4"
                        respuesta.DatoBancarioCambio.DescripcionAdicionalCampo4 = row("DES_VALOR").ToString()
                    Case "DES_CAMPO_ADICIONAL_5"
                        respuesta.DatoBancarioCambio.DescripcionAdicionalCampo5 = row("DES_VALOR").ToString()
                    Case "DES_CAMPO_ADICIONAL_6"
                        respuesta.DatoBancarioCambio.DescripcionAdicionalCampo6 = row("DES_VALOR").ToString()
                    Case "DES_CAMPO_ADICIONAL_7"
                        respuesta.DatoBancarioCambio.DescripcionAdicionalCampo7 = row("DES_VALOR").ToString()
                    Case "DES_CAMPO_ADICIONAL_8"
                        respuesta.DatoBancarioCambio.DescripcionAdicionalCampo8 = row("DES_VALOR").ToString()
                End Select
            Next
        End If


        Return respuesta
    End Function
#End Region

End Class