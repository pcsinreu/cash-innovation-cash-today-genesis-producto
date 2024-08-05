Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon

Namespace Genesis

    ''' <summary>
    ''' Classe TipoContenedor
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TipoContenedor

        ''' <summary>
        ''' Retorna os tipos de contenedor
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerTipoContenedor() As List(Of Clases.TipoContenedor)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = My.Resources.TipoContenedorObtener
            cmd.CommandType = CommandType.Text


            Dim objTiposContenedor As List(Of Clases.TipoContenedor) = Nothing
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objTiposContenedor = New List(Of Clases.TipoContenedor)

                For Each dr In dt.Rows

                    objTiposContenedor.Add(New Clases.TipoContenedor With { _
                                           .AceptaMezcla = Util.AtribuirValorObj(dr("BOL_ACEPTA_MEZCLA"), GetType(Boolean)), _
                                           .AceptaPico = Util.AtribuirValorObj(dr("BOL_ACEPTA_PICO"), GetType(Boolean)), _
                                           .Codigo = Util.AtribuirValorObj(dr("COD_TIPO_CONTENEDOR"), GetType(String)), _
                                           .Descripcion = Util.AtribuirValorObj(dr("DES_TIPO_CONTENEDOR"), GetType(String)), _
                                           .EstaActivo = Util.AtribuirValorObj(dr("BOL_ACTIVO"), GetType(Boolean)), _
                                           .FechaHoraCreacion = Util.AtribuirValorObj(dr("GMT_CREACION"), GetType(DateTime)), _
                                           .FechaHoraModificacion = Util.AtribuirValorObj(dr("GMT_MODIFICACION"), GetType(DateTime)), _
                                           .Identificador = Util.AtribuirValorObj(dr("OID_TIPO_CONTENEDOR"), GetType(String)), _
                                           .UsuarioCreacion = Util.AtribuirValorObj(dr("DES_USUARIO_CREACION"), GetType(String)), _
                                           .UsuarioModificacion = Util.AtribuirValorObj(dr("DES_USUARIO_MODIFICACION"), GetType(String)), _
                                           .ValorMaximoImporte = Util.AtribuirValorObj(dr("NUM_MAXIMO_IMPORTE"), GetType(Decimal))})
                Next

            End If

            Return objTiposContenedor
        End Function

        ''' <summary>
        ''' Retorna o identificador e a descrição dos tipos de contenedor
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerTipoContenedorSencillo() As List(Of Clases.TipoContenedor)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = My.Resources.TipoContenedorObtenerSencillo
            cmd.CommandType = CommandType.Text


            Dim objTiposContenedor As List(Of Clases.TipoContenedor) = Nothing
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objTiposContenedor = New List(Of Clases.TipoContenedor)

                For Each dr In dt.Rows

                    objTiposContenedor.Add(New Clases.TipoContenedor With { _
                                           .Descripcion = Util.AtribuirValorObj(dr("DES_TIPO_CONTENEDOR"), GetType(String)), _
                                           .Identificador = Util.AtribuirValorObj(dr("OID_TIPO_CONTENEDOR"), GetType(String)), _
                                           .esDefecto = Util.AtribuirValorObj(dr("BOL_DEFECTO"), GetType(String))})
                Next

            End If

            Return objTiposContenedor
        End Function

    End Class

End Namespace