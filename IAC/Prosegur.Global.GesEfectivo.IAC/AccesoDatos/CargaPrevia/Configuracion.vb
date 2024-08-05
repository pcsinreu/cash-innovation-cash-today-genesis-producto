Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Namespace CargaPreviaEletronica

    Public Class Configuracion

        ''' <summary>
        ''' Busca o configuracao de acordo com os parametros passados.
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [adans.klevanskis] 26/03/2013 Criado
        ''' </history>
        Public Shared Function getConfiguraciones(Peticion As Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguraciones.Peticion) As Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguraciones.Configuracion_CPColeccion

            ' criar objeto
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            comando.CommandText = My.Resources.GetConfiguraciones.ToString()

            'Monta a query de acordo com os parametros passados.
            MontaClausulaConfiguraciones(Peticion, comando)

            comando.CommandText = Util.PrepararQuery(comando.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

            Dim Respuesta As Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguraciones.Configuracion_CPColeccion = RetornaColConfiguraciones(dt)

            Return Respuesta

        End Function

        Public Shared Function getConfiguracion(Peticion As Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguracion.Peticion) As Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguracion.Configuracion_CP
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = My.Resources.GetConfiguracion.ToString()

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CONFIGURACION", ProsegurDbType.Descricao_Longa, Peticion.IdentificadorConfiguracion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CONFIGURACION", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodigoConfiguracion))
            comando.CommandText = Util.PrepararQuery(comando.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

            Dim Respuesta As Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguracion.Configuracion_CP = RetornaColConfiguracion(dt)

            Return Respuesta

        End Function

        ''' <summary>
        ''' Monta Clausula Where GetConfiguracion
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <param name="comando"></param>
        ''' <remarks></remarks>
        ''' <history>
        ''' [adans.klevanskis] 26/03/2013 Criado
        ''' </history>
        Private Shared Sub MontaClausulaConfiguraciones(ByRef Peticion As Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguraciones.Peticion,
                                                        ByRef comando As IDbCommand)

            Dim filtros As New System.Text.StringBuilder

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, Peticion.CodigoDelegacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodigoCliente))

            If Not String.IsNullOrEmpty(Peticion.CodigosubCliente) Then
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodigosubCliente))
            Else
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, DBNull.Value))
            End If

            If Not String.IsNullOrEmpty(Peticion.CodigoPuntoServicio) Then
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodigoPuntoServicio))
            Else
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, DBNull.Value))
            End If

            If Not String.IsNullOrEmpty(Peticion.CodigoCanal) Then
                filtros.Append(" AND COD_CANAL = []COD_CANAL ")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CANAL", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodigoCanal))
            End If


            If Not String.IsNullOrEmpty(Peticion.CodigoSubCanal) Then
                filtros.Append(" AND COD_SUBCANAL = []COD_SUBCANAL ")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodigoSubCanal))
            End If


            If Peticion.FormatoArchivo IsNot Nothing Then
                filtros.Append(" AND NEC_FORMATO_ARCHIVO = []NEC_FORMATO_ARCHIVO ")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_FORMATO_ARCHIVO", ProsegurDbType.Inteiro_Curto, Peticion.FormatoArchivo))
            End If


            If Peticion.TipoArchivo IsNot Nothing Then
                filtros.Append(" AND NEC_TIPO_ARCHIVO = []NEC_TIPO_ARCHIVO ")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_TIPO_ARCHIVO", ProsegurDbType.Inteiro_Curto, Peticion.TipoArchivo))
            End If


            If Peticion.Bol_Vigente IsNot Nothing Then
                filtros.Append(" AND BOL_VIGENTE = []BOL_VIGENTE ")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, Peticion.Bol_Vigente))
            End If


            If Not String.IsNullOrEmpty(Peticion.CodigoConfiguracion) Then
                filtros.Append(" AND UPPER(COD_CONFIGURACION) LIKE UPPER([]COD_CONFIGURACION) ")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CONFIGURACION", ProsegurDbType.Identificador_Alfanumerico, "%" & Peticion.CodigoConfiguracion & "%"))
            End If

            If Not String.IsNullOrEmpty(Peticion.DescripcionConfiguracion) Then
                filtros.Append(" AND UPPER(DES_CONFIGURACION) LIKE UPPER([]DES_CONFIGURACION) ")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CONFIGURACION", ProsegurDbType.Descricao_Longa, "%" & Peticion.DescripcionConfiguracion & "%"))
            End If


            comando.CommandText = String.Format(comando.CommandText, If(filtros.Length > 0, filtros.ToString, String.Empty))

        End Sub

        Private Shared Function RetornaColConfiguraciones(dt As DataTable) As Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguraciones.Configuracion_CPColeccion
            Dim objRetornaProceso As New Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguraciones.Configuracion_CPColeccion

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    ' adicionar para objeto
                    objRetornaProceso.Add(PopularConfiguraciones(dr))
                Next
            End If

            Return objRetornaProceso

        End Function

        Private Shared Function PopularConfiguraciones(dr As DataRow) As Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguraciones.Configuracion_CP

            Dim configuracion As New Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguraciones.Configuracion_CP

            Util.AtribuirValorObjeto(configuracion.IdentificadorConfiguracion, dr("OID_CONFIGURACION"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.CodigoConfiguracion, dr("COD_CONFIGURACION"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.DescripcionConfiguracion, dr("DES_CONFIGURACION"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.CodigoCliente, dr("COD_CLIENTE"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.CodigosubCliente, dr("COD_SUBCLIENTE"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.CodigoPuntoServicio, dr("COD_PTO_SERVICIO"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.CodigoCanal, dr("COD_CANAL"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.CodigoSubCanal, dr("COD_SUBCANAL"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.CodigoDelegacion, dr("COD_DELEGACION"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.FormatoArchivo, dr("NEC_FORMATO_ARCHIVO"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.TipoArchivo, dr("NEC_TIPO_ARCHIVO"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.CodigoUsuario, dr("COD_USUARIO"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.Fyh_actualizacion, dr("FYH_ACTUALIZACION"), GetType(DateTime))
            Util.AtribuirValorObjeto(configuracion.BolVigente, dr("BOL_VIGENTE"), GetType(Boolean))

            Return configuracion

        End Function

        Private Shared Function RetornaColConfiguracion(dt As DataTable) As Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguracion.Configuracion_CP


            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Dim objRetornaProceso As New Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguracion.Configuracion_CP

                objRetornaProceso = PopularConfiguracion(dt.Rows(0))

                Return objRetornaProceso

            End If

            Return Nothing

        End Function

        Private Shared Sub PopularConfiguracionXML(ByRef configuracion As Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguracion.Configuracion_CP, str As String)

            Dim config = CType(Util.DeSerializa(str, GetType(Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguracion.Configuracion)),  _
                                   Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguracion.Configuracion)

            configuracion.Comentario = config.Comentario
            configuracion.Declarados = config.Declarados
            configuracion.DescripcionNombreHoja = config.DescripcionNombreHoja
            configuracion.FilaFinal = config.FilaFinal
            configuracion.FilaInicial = config.FilaInicial
            configuracion.IacColeccion = config.IacColeccion
            configuracion.EsFilaTotal = config.esFilaTotal

        End Sub

        Private Shared Function PopularConfiguracion(dr As DataRow) As Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguracion.Configuracion_CP

            Dim configuracion As New Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguracion.Configuracion_CP

            Dim str As String = String.Empty

            Util.AtribuirValorObjeto(str, dr("BIN_CONFIGURACION_CP"), GetType(String))


            PopularConfiguracionXML(configuracion, str)

            Util.AtribuirValorObjeto(configuracion.IdentificadorConfiguracion, dr("OID_CONFIGURACION"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.CodigoConfiguracion, dr("COD_CONFIGURACION"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.DescripcionConfiguracion, dr("DES_CONFIGURACION"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.CodigoCliente, dr("COD_CLIENTE"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.CodigosubCliente, dr("COD_SUBCLIENTE"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.CodigoPuntoServicio, dr("COD_PTO_SERVICIO"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.CodigoCanal, dr("COD_CANAL"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.CodigoSubCanal, dr("COD_SUBCANAL"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.CodigoDelegacion, dr("COD_DELEGACION"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.FormatoArchivo, dr("NEC_FORMATO_ARCHIVO"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.TipoArchivo, dr("NEC_TIPO_ARCHIVO"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.DescripcionObservacion, dr("DES_OBSERVACION"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.CodigoUsuario, dr("COD_USUARIO"), GetType(String))
            Util.AtribuirValorObjeto(configuracion.Fyh_actualizacion, dr("FYH_ACTUALIZACION"), GetType(DateTime))
            Util.AtribuirValorObjeto(configuracion.BolVigente, dr("BOL_VIGENTE"), GetType(Boolean))

            Return configuracion

        End Function

        Private Shared Function ExtraiConfiguracionesCPXML(ByRef Peticion As Integracion.ContractoServicio.CargaPreviaEletronica.SetConfiguraciones.Peticion) As String

            Dim conf As New Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguracion.Configuracion
            conf.Comentario = Peticion.Comentario
            conf.Declarados = Peticion.Declarados
            conf.DescripcionNombreHoja = Peticion.DescripcionNombreHoja
            conf.FilaFinal = Peticion.FilaFinal
            conf.FilaInicial = Peticion.FilaInicial
            conf.IacColeccion = Peticion.IacColeccion
            conf.esFilaTotal = Peticion.EsFilaTotal

            Return Util.Serializa(conf)

        End Function

        Public Shared Function setConfiguracionCP(Peticion As Integracion.ContractoServicio.CargaPreviaEletronica.SetConfiguraciones.Peticion) As Integracion.ContractoServicio.CargaPreviaEletronica.SetConfiguraciones.Respuesta

            Dim objConfiguracion As New Integracion.ContractoServicio.CargaPreviaEletronica.SetConfiguraciones.Respuesta

            Try
                Dim objtransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

                Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
                comando.CommandText = Util.PrepararQuery(My.Resources.SetConfiguracion.ToString())
                comando.CommandType = CommandType.Text


                Dim oidconfiguracion As String = Guid.NewGuid.ToString
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CONFIGURACION", ProsegurDbType.Objeto_Id, oidconfiguracion))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CONFIGURACION", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodigoConfiguracion))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CONFIGURACION", ProsegurDbType.Descricao_Longa, Peticion.DescripcionConfiguracion))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodigoCliente))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodigosubCliente))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Descricao_Longa, Peticion.CodigoPuntoServicio))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CANAL", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodigoCanal))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodigoSubCanal))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, Peticion.CodigoDelegacion))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_FORMATO_ARCHIVO", ProsegurDbType.Inteiro_Curto, Peticion.FormatoArchivo))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_TIPO_ARCHIVO", ProsegurDbType.Inteiro_Curto, Peticion.TipoArchivo))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_OBSERVACION", ProsegurDbType.Identificador_Alfanumerico, Peticion.DescripcionObservacion))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BIN_CONFIGURACION_CP", ProsegurDbType.Observacao_Longa, ExtraiConfiguracionesCPXML(Peticion)))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodigoUsuario))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, Peticion.BolVigente))

                comando.CommandText = Util.PrepararQuery(comando.CommandText)

                objtransacion.AdicionarItemTransacao(comando)

                objtransacion.RealizarTransacao()

            Catch ex As Exception
                Excepcion.Util.Tratar(ex, Traduzir("036_msg_Erro_UKCodConfiguracion"))
            End Try

            Return objConfiguracion

        End Function


    End Class

End Namespace
