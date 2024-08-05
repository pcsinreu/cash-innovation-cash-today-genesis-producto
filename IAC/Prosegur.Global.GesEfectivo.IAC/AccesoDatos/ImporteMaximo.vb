Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ImporteMaximo
Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Data
Imports System.Configuration
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

Public Class ImporteMaximo

#Region "[CONSULTAR]"

    'Public Shared Function GetImporteMaximo(objPeticion As ContractoServicio.ImporteMaximo.GetImporteMaximo.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As GetImporteMaximo.EntidadImporteMaximoColeccion
    '    ' criar comando
    '    Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

    '    ' obter query
    '    Dim query As New StringBuilder
    '    query.Append(My.Resources.GetImporteMaximo.ToString)

    '    'Adiciona Where caso algum parâmetro seja passado
    '    Dim clausulaWhere As New CriterioColecion
    '    Dim strClausulaWhere As String = String.Empty

    '    ' adiciona filtros
    '    If Not String.IsNullOrEmpty(objPeticion.ImporteMaximo.SubCanal.OidSubCanal) Then
    '        clausulaWhere.addCriterio("AND", " IM.OID_PLANTA = []OID_PLANTA ")
    '        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANTA", ProsegurDbType.Identificador_Alfanumerico, objPeticion.ImporteMaximo.Canal.Identificador))
    '    End If
    '    If Not String.IsNullOrEmpty(objPeticion.ImporteMaximo.BolDefecto) Then
    '        clausulaWhere.addCriterio("AND", " IM.OID_SECTOR = []OID_SECTOR ")
    '        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SECTOR", ProsegurDbType.Identificador_Alfanumerico, objPeticion.ImporteMaximo.Sector.OidSector))
    '    End If
    '    If objPeticion.ImporteMaximo.BolDefecto IsNot Nothing Then
    '        clausulaWhere.addCriterio("AND", " IM.BOL_ACTIVO = []BOL_ACTIVO ")
    '        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, objPeticion.ImporteMaximo.BolDefecto))
    '    End If

    '    'Adiciona a clausula Where
    '    If clausulaWhere.Count > 0 Then
    '        strClausulaWhere = Util.MontarClausulaWhere(clausulaWhere)
    '    End If

    '    ' preparar query
    '    comando.CommandText = Util.PrepararQuery(String.Format(query.ToString, strClausulaWhere))

    '    comando.CommandType = CommandType.Text


    '    ' executar query
    '    'Dim dtDivisa As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)
    '    Dim dtImporteMaximo As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, comando, objPeticion.ParametrosPaginacion, parametrosRespuestaPaginacion)


    '    Dim dtDivisaAgrupacion As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

    '    'Verificase o dtDivisaAgrupacion retornou algum registro.
    '    If dtDivisaAgrupacion IsNot Nothing AndAlso dtDivisaAgrupacion.Rows.Count > 0 Then
    '        For Each dr In dtDivisaAgrupacion.Select
    '            If dr("COD_ISO_DIVISA") IsNot DBNull.Value Then
    '                '  objDivisa.CodigoISO = dr("COD_ISO_DIVISA")
    '            End If
    '        Next


    '    End If

    '    Return Nothing


    'End Function


    Public Shared Function PopularGetImportesMaximosRespuesta(dtImporteMaximo As DataRow) As GetImporteMaximo.ImporteMaximoRespuesta

        Dim retorno As New GetImporteMaximo.ImporteMaximoRespuesta

        With retorno

            Util.AtribuirValorObjeto(.CodImporteMaximo, dtImporteMaximo("OID_CLIENTE"), GetType(String))
            Util.AtribuirValorObjeto(.DesImporteMaximo, dtImporteMaximo("OID_DIVISA"), GetType(String))
            Util.AtribuirValorObjeto(.DesImporteMaximo, dtImporteMaximo("OID_SUBCANAL"), GetType(String))
            Util.AtribuirValorObjeto(.DesImporteMaximo, dtImporteMaximo("OID_SECTOR"), GetType(String))
            Util.AtribuirValorObjeto(.DesImporteMaximo, dtImporteMaximo("OID_PLANTA"), GetType(String))
            Util.AtribuirValorObjeto(.DesImporteMaximo, dtImporteMaximo("NUM_IMPORTE_MAXIMO"), GetType(String))
            Util.AtribuirValorObjeto(.BolDefecto, dtImporteMaximo("BOL_ACTIVO"), GetType(Boolean))
            Util.AtribuirValorObjeto(.DesUsuarioCreacion, dtImporteMaximo("DES_USUARIO_CREACION"), GetType(String))
            Util.AtribuirValorObjeto(.DesUsuarioModificacion, dtImporteMaximo("DES_USUARIO_MODIFICACION"), GetType(String))
            Util.AtribuirValorObjeto(.GmtCreacion, dtImporteMaximo("GMT_CREACION"), GetType(Date))
            Util.AtribuirValorObjeto(.GmtModificacion, dtImporteMaximo("GMT_MODIFICACION"), GetType(Date))
            Util.AtribuirValorObjeto(.OidImporteMaximo, dtImporteMaximo("OID_IMPORTE_MAXIMO"), GetType(String))

        End With

        Return retorno

    End Function


    Public Shared Function PopularGetImportesMaximos(dtImporteMaximo As DataRow) As GetImporteMaximo.ImporteMaximo

        Dim retorno As New GetImporteMaximo.ImporteMaximo

        With retorno

            Util.AtribuirValorObjeto(.Cliente.OidCliente, dtImporteMaximo("OID_CLIENTE"), GetType(String))
            Util.AtribuirValorObjeto(.Divisa.Identificador, dtImporteMaximo("OID_DIVISA"), GetType(String))
            Util.AtribuirValorObjeto(.SubCanal.OidSubCanal, dtImporteMaximo("OID_SUBCANAL"), GetType(String))
            Util.AtribuirValorObjeto(.Sector.OidSector, dtImporteMaximo("OID_SECTOR"), GetType(String))
            Util.AtribuirValorObjeto(.SubCanal.OidSubCanal, dtImporteMaximo("OID_PLANTA"), GetType(String))
            Util.AtribuirValorObjeto(.ValorMaximo, dtImporteMaximo("NUM_IMPORTE_MAXIMO"), GetType(String))
            Util.AtribuirValorObjeto(.BolDefecto, dtImporteMaximo("BOL_ACTIVO"), GetType(Boolean))
        End With

        Return retorno

    End Function

    Public Shared Function RecuperaimporteMaximoBase(oidPlanta As String, oidSector As String) As ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase
        Dim objImporteMaximoColeccion As New ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandType = CommandType.Text

        If Not String.IsNullOrEmpty(oidPlanta) Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANTA", ProsegurDbType.Objeto_Id, oidPlanta))
            comando.CommandText = Util.PrepararQuery(My.Resources.GetImporteMaximoPlanta)
        End If
        If Not String.IsNullOrEmpty(oidSector) Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SECTOR", ProsegurDbType.Objeto_Id, oidSector))
            comando.CommandText = Util.PrepararQuery(My.Resources.GetImporteMaximoSector)
        End If
        
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt.Rows.Count > 0 AndAlso _
            dt IsNot Nothing Then

            For Each dr In dt.Rows
                Dim objImporteMaximo As New ContractoServicio.ImporteMaximo.ImporteMaximoBase

                If dr("OID_CLIENTE").ToString <> String.Empty Then
                    Dim objCliente As New ContractoServicio.Utilidad.GetComboClientes.Cliente With {.OidCliente = dr("OID_CLIENTE"), .Codigo = dr("COD_CLIENTE"), .Descripcion = dr("DES_CLIENTE")}
                    objImporteMaximo.Cliente = objCliente
                End If

                If dr("COD_CANAL").ToString <> String.Empty Then
                    Dim objCanal As New ContractoServicio.Utilidad.GetComboCanales.Canal With {.Identificador = dr("OID_CANAL"), .Codigo = dr("COD_CANAL"), .Descripcion = dr("DES_CANAL")}
                    objImporteMaximo.Canal = objCanal
                End If

                If dr("OID_SUBCANAL").ToString <> String.Empty Then
                    Dim objSubCanal As New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal With {.OidSubCanal = dr("OID_SUBCANAL"), .Codigo = dr("COD_SUBCANAL"), .Descripcion = dr("DES_SUBCANAL")}
                    objImporteMaximo.SubCanal = objSubCanal
                End If
                If dr("OID_DIVISA").ToString <> String.Empty Then
                    Dim objDivisa As New ContractoServicio.Utilidad.GetComboDivisas.Divisa With {.Identificador = dr("OID_DIVISA"), .CodigoIso = dr("COD_ISO_DIVISA"), .Descripcion = dr("DES_DIVISA")}
                    objImporteMaximo.Divisa = objDivisa
                End If
                If dr("OID_SECTOR").ToString <> String.Empty Then
                    Dim objSector As New ContractoServicio.Utilidad.GetComboSectores.Sector1 With {.OidSector = dr("OID_SECTOR")}
                    objImporteMaximo.Sector = objSector
                End If

                Util.AtribuirValorObjeto(objImporteMaximo.OidImporteMaximo, dr("OID_IMPORTE_MAXIMO"), GetType(String))
                Util.AtribuirValorObjeto(objImporteMaximo.oidPlanta, dr("OID_PLANTA"), GetType(String))
                Util.AtribuirValorObjeto(objImporteMaximo.ValorMaximo, dr("NUM_IMPORTE_MAXIMO"), GetType(String))
                Util.AtribuirValorObjeto(objImporteMaximo.BolVigente, dr("BOL_ACTIVO"), GetType(Boolean))

                objImporteMaximoColeccion.Add(objImporteMaximo)
            Next
            Return objImporteMaximoColeccion

        End If

        Return Nothing

    End Function


#End Region

    Public Shared Function SetImporteMaximo(ByRef objImporteMaximo As ContractoServicio.ImporteMaximo.SetImporteMaximo.ImporteMaximo, _
                                            Optional ByRef objTransacion As Transacao = Nothing) As Boolean
        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim oidImporteMaximo As String = Nothing

        ' obter query
        Dim query As New StringBuilder
        If String.IsNullOrEmpty(objImporteMaximo.OidImporteMaximo) Then
            query.Append(My.Resources.SetImporteMaximoInsert.ToString)
            oidImporteMaximo = Guid.NewGuid.ToString
            objImporteMaximo.OidImporteMaximo = oidImporteMaximo
        Else
            query.Append(My.Resources.SetImporteMaximoUpdate.ToString)
            oidImporteMaximo = objImporteMaximo.OidImporteMaximo
        End If

        ' setar parametro
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_IMPORTE_MAXIMO", ProsegurDbType.Identificador_Alfanumerico, oidImporteMaximo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objImporteMaximo.Cliente.OidCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DIVISA", ProsegurDbType.Identificador_Alfanumerico, objImporteMaximo.Divisa.Identificador))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, If(objImporteMaximo.SubCanal IsNot Nothing, objImporteMaximo.SubCanal.OidSubCanal, String.Empty)))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SECTOR", ProsegurDbType.Identificador_Alfanumerico, If(objImporteMaximo.Sector IsNot Nothing, objImporteMaximo.Sector.OidSector, String.Empty)))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANTA", ProsegurDbType.Identificador_Alfanumerico, objImporteMaximo.oidPlanta))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NUM_IMPORTE_MAXIMO", ProsegurDbType.Numero_Decimal, objImporteMaximo.ValorMaximo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, objImporteMaximo.BolVigente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data_Hora, objImporteMaximo.GmtCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, objImporteMaximo.DesUsuarioCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, objImporteMaximo.GmtModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, objImporteMaximo.DesUsuarioModificacion))

        ' preparar query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        Dim Atualizados As Integer = 0

        If objTransacion Is Nothing Then
            Atualizados = AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        Else
            objTransacion.AdicionarItemTransacao(comando)
            Return 1
        End If

        Return Atualizados > 0

    End Function

End Class
