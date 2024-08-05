Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Text
Namespace GenesisSaldos
    Public Class FiltroFormulario

        ''' <summary>
        ''' Recupera todos os filtros fomulario
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperaFiltrosFormulario() As List(Of Clases.FiltroFormulario)

            Dim listaFiltroFormulario As New List(Of Clases.FiltroFormulario)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim dt As DataTable

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.FiltroFormularioRecuperar)
            cmd.CommandType = CommandType.Text

            dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each row In dt.Rows
                    Dim filtroFormulario As New Clases.FiltroFormulario
                    With filtroFormulario
                        .Identificador = Util.AtribuirValorObj(row("OID_FILTRO_FORMULARIO"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(row("DES_FILTRO_FORMULARIO"), GetType(String))
                        .SoloDisponible = Util.AtribuirValorObj(row("BOL_SOLO_DISPONIBLE"), GetType(Boolean))
                        .ConValor = Util.AtribuirValorObj(row("BOL_CON_VALOR"), GetType(Boolean))
                        .ConBulto = Util.AtribuirValorObj(row("BOL_CON_BULTO"), GetType(Boolean))
                        .SoloReenvio = Util.AtribuirValorObj(row("BOL_SOLO_REENVIO"), GetType(Boolean))
                        .SoloSustitucion = Util.AtribuirValorObj(row("BOL_SOLO_SUSTITUCION"), GetType(Boolean))
                        .ConFechaEspecifica = Util.AtribuirValorObj(row("BOL_CON_FECHA_ESPECIFICA"), GetType(Boolean))
                        .NecDiasBusquedaInicio = Util.AtribuirValorObj(row("NEC_DIAS_BUSQUEDA_INICIO"), GetType(Integer))
                        .NecDiasBusquedaFim = Util.AtribuirValorObj(row("NEC_DIAS_BUSQUEDA_FIN"), GetType(Integer))
                        .CodigoMigracion = Util.AtribuirValorObj(row("COD_MIGRACION"), GetType(String))
                        .EsActivo = Util.AtribuirValorObj(row("BOL_ACTIVO"), GetType(Boolean))
                        .FechaCreacion = Util.AtribuirValorObj(row("GMT_CREACION"), GetType(DateTime))
                        .UsuarioCriacion = Util.AtribuirValorObj(row("DES_USUARIO_CREACION"), GetType(String))
                        .FechaModificacion = Util.AtribuirValorObj(row("GMT_MODIFICACION"), GetType(DateTime))
                        .UsuarioModificacion = Util.AtribuirValorObj(row("DES_USUARIO_MODIFICACION"), GetType(String))

                    End With
                    listaFiltroFormulario.Add(filtroFormulario)
                Next
            End If

            Return listaFiltroFormulario
        End Function

    End Class
End Namespace

