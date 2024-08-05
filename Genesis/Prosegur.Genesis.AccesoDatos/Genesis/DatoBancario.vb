Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports Prosegur.Genesis
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper

Public Class DatoBancario

#Region "[EXCLUIR]"

    Public Shared Sub BajaDatosBancarios(IdentificadorCliente As String, IdentificadorSubCliente As String, IdentificadorPuntoServicio As String, ByRef Transacion As Prosegur.DbHelper.Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

        ' query
        Dim query As New List(Of String)

        If Not String.IsNullOrEmpty(IdentificadorCliente) Then
            query.Add(" OID_CLIENTE = []OID_CLIENTE")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Objeto_Id, IdentificadorCliente))
        End If
        If Not String.IsNullOrEmpty(IdentificadorSubCliente) Then
            query.Add(" OID_SUBCLIENTE = []OID_SUBCLIENTE")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, IdentificadorSubCliente))
        End If
        If Not String.IsNullOrEmpty(IdentificadorPuntoServicio) Then
            query.Add(" OID_PTO_SERVICIO = []OID_PTO_SERVICIO")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, IdentificadorPuntoServicio))
        End If

        ' preparar query

        comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.BajaDatoBancario, String.Join(" AND ", query)))
        comando.CommandType = CommandType.Text

        If Transacion Is Nothing Then
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, comando)
        Else
            Transacion.AdicionarItemTransacao(comando)
        End If

    End Sub

#End Region

#Region "[INSERIR/ATUALIZAR]"

    Public Shared Sub SetDatosBancarios(objPeticion As List(Of Prosegur.Genesis.Comon.Clases.DatoBancario),
                                        IdentificadorCliente As String,
                                        IdentificadorSubCliente As String,
                                        IdentificadorPuntoServicio As String,
                                        CodigoUsuario As String,
                                        ByRef Transacion As Prosegur.DbHelper.Transacao)

        For Each objDatoBancario In objPeticion

            ' criar comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DATO_BANCARIO", ProsegurDbType.Objeto_Id, objDatoBancario.Identificador))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BANCO", ProsegurDbType.Objeto_Id, objDatoBancario.Banco.Identificador))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Objeto_Id, objDatoBancario.Divisa.Identificador))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_CUENTA_BANCARIA", ProsegurDbType.Observacao_Longa, objDatoBancario.CodigoTipoCuentaBancaria.ToUpper))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CUENTA_BANCARIA", ProsegurDbType.Observacao_Longa, objDatoBancario.CodigoCuentaBancaria))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DOCUMENTO", ProsegurDbType.Observacao_Longa, objDatoBancario.CodigoDocumento))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_TITULARIDAD", ProsegurDbType.Observacao_Longa, objDatoBancario.DescripcionTitularidad))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_OBSERVACIONES", ProsegurDbType.Observacao_Longa, objDatoBancario.DescripcionObs))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_DEFECTO", ProsegurDbType.Logico, objDatoBancario.bolDefecto))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_ACTIVO", ProsegurDbType.Logico, True))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_CREACION", ProsegurDbType.Data_Hora, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Observacao_Longa, CodigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Observacao_Longa, CodigoUsuario))

            If Not String.IsNullOrEmpty(IdentificadorCliente) Then
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Objeto_Id, IdentificadorCliente))
            Else
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Objeto_Id, Nothing))
            End If
            If Not String.IsNullOrEmpty(IdentificadorSubCliente) Then
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, IdentificadorSubCliente))
            Else
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, Nothing))
            End If
            If Not String.IsNullOrEmpty(IdentificadorPuntoServicio) Then
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, IdentificadorPuntoServicio))
            Else
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, Nothing))
            End If



            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_AGENCIA", ProsegurDbType.Observacao_Longa, objDatoBancario.CodigoAgencia))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_CAMPO_ADICIONAL_1", ProsegurDbType.Observacao_Longa, objDatoBancario.DescripcionAdicionalCampo1))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_CAMPO_ADICIONAL_2", ProsegurDbType.Observacao_Longa, objDatoBancario.DescripcionAdicionalCampo2))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_CAMPO_ADICIONAL_3", ProsegurDbType.Observacao_Longa, objDatoBancario.DescripcionAdicionalCampo3))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_CAMPO_ADICIONAL_4", ProsegurDbType.Observacao_Longa, objDatoBancario.DescripcionAdicionalCampo4))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_CAMPO_ADICIONAL_5", ProsegurDbType.Observacao_Longa, objDatoBancario.DescripcionAdicionalCampo5))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_CAMPO_ADICIONAL_6", ProsegurDbType.Observacao_Longa, objDatoBancario.DescripcionAdicionalCampo6))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_CAMPO_ADICIONAL_7", ProsegurDbType.Observacao_Longa, objDatoBancario.DescripcionAdicionalCampo7))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_CAMPO_ADICIONAL_8", ProsegurDbType.Observacao_Longa, objDatoBancario.DescripcionAdicionalCampo8))


            ' preparar query
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.SetDatoBancario.ToString)
            comando.CommandType = CommandType.Text

            If Transacion Is Nothing Then
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, comando)
            Else
                Transacion.AdicionarItemTransacao(comando)
            End If

        Next

    End Sub

    Public Shared Sub AlterarBolDefectoDatoBancario(identifiador As String, defecto As Boolean, ByRef Transacion As Prosegur.DbHelper.Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

        ' preparar query
        comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AlterarBolDefecto.ToString)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DATO_BANCARIO", ProsegurDbType.Objeto_Id, identifiador))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_DEFECTO", ProsegurDbType.Logico, defecto))

        If Transacion Is Nothing Then
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, comando)
        Else
            Transacion.AdicionarItemTransacao(comando)
        End If

    End Sub
    Public Shared Sub SetComentario(peticion As Prosegur.Genesis.ContractoServicio.DatoBancario.SetComentario.Peticion)

        Try
            Dim spw As SPWrapper = Nothing
            spw = armarWrapperSetComentario(peticion)


            AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
        Catch ex As Exception
            'no hace nada
        End Try
    End Sub

    Private Shared Function armarWrapperSetComentario(peticion As Prosegur.Genesis.ContractoServicio.DatoBancario.SetComentario.Peticion) As SPWrapper
        Dim SP As String = String.Format("SAPR_PDATO_BANCARIO_{0}.sins_dato_banc_comentario", Prosegur.Genesis.Comon.Util.Version)
        Dim spw As New SPWrapper(SP, True)

        spw.AgregarParam("par$oid_dato_banc_cambio", ProsegurDbType.Identificador_Alfanumerico, peticion.Oid_DatoBancario_Cambio, , False)
        spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Curta, peticion.Cod_Usuario, , False)
        spw.AgregarParam("par$des_tabla", ProsegurDbType.Descricao_Longa, peticion.Des_Tabla, , False)
        spw.AgregarParam("par$oid_tabla", ProsegurDbType.Identificador_Alfanumerico, peticion.Oid_Tabla, , False)
        spw.AgregarParam("par$des_comentario", ProsegurDbType.Descricao_Longa, peticion.Des_Comentario, , False)
        spw.AgregarParam("par$fyh_comentario", ProsegurDbType.Data_Hora, peticion.Fecha, , False)
        If peticion.Bol_HacerCommit Then
            spw.AgregarParam("par$bol_commit", ProsegurDbType.Inteiro_Curto, 1, , False)
        Else
            spw.AgregarParam("par$bol_commit", ProsegurDbType.Inteiro_Curto, 0, , False)
        End If


        spw.AgregarParam("par$oid_identificador", ProsegurDbType.Identificador_Alfanumerico, peticion.Identificador, ParameterDirection.Output, False)

        Return spw
    End Function

    ''' <summary>
    ''' Se encarga de devolver los comentarios relacionados a un cambio de DatoBancario
    ''' </summary>
    ''' <param name="oid_dato_bancario_cambio"></param>
    ''' <returns></returns>
    Public Shared Function GetComentarios(oid_dato_bancario_cambio As String) As ContractoServicio.DatoBancario.GetComentario.Respuesta
        Dim respuesta = New ContractoServicio.DatoBancario.GetComentario.Respuesta

        Try
            Dim spw As SPWrapper = armarWrapperGetComentario(oid_dato_bancario_cambio)
            Dim ds As DataSet = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

            respuesta = PoblarRespuestaGetComentarios(ds)

        Catch ex As Exception
            Throw ex
        End Try
        Return respuesta
    End Function

    Private Shared Function PoblarRespuestaGetComentarios(ds As DataSet) As ContractoServicio.DatoBancario.GetComentario.Respuesta
        Dim respuesta As New ContractoServicio.DatoBancario.GetComentario.Respuesta

        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            If ds.Tables.Contains("comentarios_aprobacion") Then
                Dim dt As DataTable = ds.Tables("comentarios_aprobacion")
                Dim mensajeAprobacion As ContractoServicio.DatoBancario.GetComentario.MensajeAprobacion
                For Each fila As DataRow In dt.Rows
                    mensajeAprobacion = New ContractoServicio.DatoBancario.GetComentario.MensajeAprobacion()

                    mensajeAprobacion.Comentario = Util.AtribuirValorObj(fila("DES_COMENTARIO"), GetType(String))
                    mensajeAprobacion.Estado = Util.AtribuirValorObj(fila("BOL_APROBADO"), GetType(Integer))
                    mensajeAprobacion.Fecha = Util.AtribuirValorObj(fila("FYH_APROBACION"), GetType(DateTime))
                    mensajeAprobacion.Usuario_Aprobacion = String.Format("{0} - {1}, {2}", Util.AtribuirValorObj(fila("DES_LOGIN"), GetType(String)), Util.AtribuirValorObj(fila("DES_APELLIDO"), GetType(String)), Util.AtribuirValorObj(fila("DES_NOMBRE"), GetType(String)))

                    respuesta.MensajesAprobacion.Add(mensajeAprobacion)
                Next

            End If

            If ds.Tables.Contains("comentarios_modificacion") Then
                Dim dt As DataTable = ds.Tables("comentarios_modificacion")
                Dim mensajeModificacion As ContractoServicio.DatoBancario.GetComentario.MensajeModificacion
                For Each fila As DataRow In dt.Rows
                    mensajeModificacion = New ContractoServicio.DatoBancario.GetComentario.MensajeModificacion

                    mensajeModificacion.Comentario = Util.AtribuirValorObj(fila("DES_COMENTARIO"), GetType(String))
                    mensajeModificacion.Fecha = Util.AtribuirValorObj(fila("FYH_MODIFICACION"), GetType(DateTime))
                    mensajeModificacion.Usuario_Modificacion = String.Format("{0} - {1}, {2}", Util.AtribuirValorObj(fila("DES_LOGIN"), GetType(String)), Util.AtribuirValorObj(fila("DES_APELLIDO"), GetType(String)), Util.AtribuirValorObj(fila("DES_NOMBRE"), GetType(String)))


                    respuesta.MensajesModificacion.Add(mensajeModificacion)
                Next
            End If


        End If

        Return respuesta
    End Function

    Private Shared Function armarWrapperGetComentario(oid_dato_bancario_cambio As String) As SPWrapper
        Dim SP As String = String.Format("SAPR_PDATO_BANCARIO_{0}.srecuperar_comentarios", Prosegur.Genesis.Comon.Util.Version)
        Dim spw As New SPWrapper(SP, False)

        spw.AgregarParam("par$oid_dato_banc_cambio", ProsegurDbType.Identificador_Alfanumerico, oid_dato_bancario_cambio, , False)
        spw.AgregarParam("par$rc_comentarios_aprob", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "comentarios_aprobacion")
        spw.AgregarParam("par$rc_comentarios_modif", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "comentarios_modificacion")

        Return spw
    End Function

#End Region

#Region "[CONSULTAR]"




#End Region
End Class