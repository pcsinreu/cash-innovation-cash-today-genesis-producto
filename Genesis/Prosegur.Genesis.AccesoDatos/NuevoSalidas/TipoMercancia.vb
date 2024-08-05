Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.NuevoSalidas.Remesa
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.Framework.Dicionario

Namespace NuevoSalidas

    Public Class TipoMercancia

#Region "[CONSTANTES]"

        Public Const C_OID_TIPO_MERCANCIA As String = "OID_TIPO_MERCANCIA"
        Public Const C_COD_TIPO_MERCANCIA As String = "COD_TIPO_MERCANCIA"
        Public Const C_DES_TIPO_MERCANCIA As String = "DES_TIPO_MERCANCIA"
        Public Const C_BIN_TIPO_MERCANCIA As String = "BIN_TIPO_MERCANCIA"
        Public Const C_COD_COMBINACION As String = "COD_COMBINACION"

#End Region

#Region "[CONSULTAR]"

        ''' <summary>
        ''' Recupera os tipos de mercancia da remesa.
        ''' </summary>
        ''' <param name="IdentificadoresBulto"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarTiposMercanciaBultos(IdentificadoresBulto As List(Of String)) As Dictionary(Of String, ObservableCollection(Of Clases.TipoMercancia))

            If IdentificadoresBulto Is Nothing OrElse IdentificadoresBulto.Count = 0 Then
                Return Nothing
            End If

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_TipoMercancia_RecuperarMercanciasBultos
            cmd.CommandType = CommandType.Text

            Dim Query As New System.Text.StringBuilder

            Query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, IdentificadoresBulto, "OID_BULTO", cmd, "WHERE", "BTM"))

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, String.Format(cmd.CommandText, Query))
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)
            Dim objTiposMercancia As Dictionary(Of String, ObservableCollection(Of Clases.TipoMercancia)) = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objTiposMercancia = New Dictionary(Of String, ObservableCollection(Of Clases.TipoMercancia))
                Dim objTipoMercancia As KeyValuePair(Of String, ObservableCollection(Of Clases.TipoMercancia))
                Dim IdentificadorBulto As String = String.Empty

                For Each dr In dt.Rows

                    IdentificadorBulto = Util.AtribuirValorObj(dr("OID_BULTO"), GetType(String))

                    objTipoMercancia = (From tm In objTiposMercancia Where tm.Key = IdentificadorBulto).FirstOrDefault

                    If String.IsNullOrEmpty(objTipoMercancia.Key) Then

                        objTiposMercancia.Add(IdentificadorBulto, New ObservableCollection(Of Clases.TipoMercancia))

                        objTipoMercancia = (From tm In objTiposMercancia Where tm.Key = IdentificadorBulto).FirstOrDefault

                    End If

                    objTipoMercancia.Value.Add(New Clases.TipoMercancia With {.Codigo = Util.AtribuirValorObj(dr("COD_TIPO_MERCANCIA"), GetType(String)), _
                                                                         .Descripcion = Util.AtribuirValorObj(dr("DES_TIPO_MERCANCIA"), GetType(String)), _
                                                                         .Identificador = Util.AtribuirValorObj(dr("OID_TIPO_MERCANCIA"), GetType(String))})

                Next

            End If

            Return objTiposMercancia
        End Function

        ''' <summary>
        ''' Recupera os tipos de mercancia do bulto.
        ''' </summary>
        ''' <param name="IdentificadorBulto"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarTiposMercanciaBulto(IdentificadorBulto As String) As ObservableCollection(Of Clases.TipoMercancia)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_TipoMercancia_RecuperarMercanciasBulto)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, IdentificadorBulto))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)
            Dim objTiposMercancia As ObservableCollection(Of Clases.TipoMercancia) = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objTiposMercancia = New ObservableCollection(Of Clases.TipoMercancia)()

                For Each dr In dt.Rows

                    objTiposMercancia.Add(New Clases.TipoMercancia With {.Codigo = Util.AtribuirValorObj(dr("COD_TIPO_MERCANCIA"), GetType(String)), _
                                                                         .Descripcion = Util.AtribuirValorObj(dr("DES_TIPO_MERCANCIA"), GetType(String)), _
                                                                         .Identificador = Util.AtribuirValorObj(dr("OID_TIPO_MERCANCIA"), GetType(String))})

                Next

            End If

            Return objTiposMercancia
        End Function

        ''' <summary>
        ''' Recupera os tipos de mercancia da remesa.
        ''' </summary>
        ''' <param name="IdentificadoresRemesas"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarTiposMercanciaRemesas(IdentificadoresRemesas As List(Of String)) As Dictionary(Of String, ObservableCollection(Of Clases.TipoMercancia))

            If IdentificadoresRemesas Is Nothing OrElse IdentificadoresRemesas.Count = 0 Then
                Return Nothing
            End If

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_TipoMercancia_RecuperarMercanciaRemesas
            cmd.CommandType = CommandType.Text

            Dim Query As New System.Text.StringBuilder

            Query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, IdentificadoresRemesas, "OID_REMESA", cmd, "WHERE", "RTM"))

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, String.Format(cmd.CommandText, Query))
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)
            Dim objTiposMercancia As Dictionary(Of String, ObservableCollection(Of Clases.TipoMercancia)) = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objTiposMercancia = New Dictionary(Of String, ObservableCollection(Of Clases.TipoMercancia))
                Dim objTipoMercancia As KeyValuePair(Of String, ObservableCollection(Of Clases.TipoMercancia))
                Dim IdentificadorRemesa As String = String.Empty

                For Each dr In dt.Rows

                    IdentificadorRemesa = Util.AtribuirValorObj(dr("OID_REMESA"), GetType(String))

                    objTipoMercancia = (From tm In objTiposMercancia Where tm.Key = IdentificadorRemesa).FirstOrDefault

                    If String.IsNullOrEmpty(objTipoMercancia.Key) Then

                        objTiposMercancia.Add(IdentificadorRemesa, New ObservableCollection(Of Clases.TipoMercancia))

                        objTipoMercancia = (From tm In objTiposMercancia Where tm.Key = IdentificadorRemesa).FirstOrDefault

                    End If

                    objTipoMercancia.Value.Add(New Clases.TipoMercancia With {.Codigo = Util.AtribuirValorObj(dr("COD_TIPO_MERCANCIA"), GetType(String)), _
                                                                         .Descripcion = Util.AtribuirValorObj(dr("DES_TIPO_MERCANCIA"), GetType(String)), _
                                                                         .Identificador = Util.AtribuirValorObj(dr("OID_TIPO_MERCANCIA"), GetType(String))})

                Next

            End If

            Return objTiposMercancia
        End Function

        ''' <summary>
        ''' Recupera os tipos de mercancia da remesa.
        ''' </summary>
        ''' <param name="IdentificadorRemesa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarTiposMercanciaRemesa(IdentificadorRemesa As String) As ObservableCollection(Of Clases.TipoMercancia)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_TipoMercancia_RecuperarMercanciasRemesa)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, IdentificadorRemesa))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)
            Dim objTiposMercancia As ObservableCollection(Of Clases.TipoMercancia) = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objTiposMercancia = New ObservableCollection(Of Clases.TipoMercancia)()

                For Each dr In dt.Rows

                    objTiposMercancia.Add(New Clases.TipoMercancia With {.Codigo = Util.AtribuirValorObj(dr("COD_TIPO_MERCANCIA"), GetType(String)), _
                                                                         .Descripcion = Util.AtribuirValorObj(dr("DES_TIPO_MERCANCIA"), GetType(String)), _
                                                                         .Identificador = Util.AtribuirValorObj(dr("OID_TIPO_MERCANCIA"), GetType(String))})

                Next

            End If

            Return objTiposMercancia
        End Function

        Public Shared Function ObtenerTiposMercancia() As ObservableCollection(Of Clases.TipoMercancia)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_TipoMercancia_ObtenerTiposMercancia.ToString()
            cmd.CommandType = CommandType.Text

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim TiposMercancia As New ObservableCollection(Of Clases.TipoMercancia)

                For Each row In dt.Rows

                    TiposMercancia.Add(New Clases.TipoMercancia With {.Codigo = Util.AtribuirValorObj(row("COD_TIPO_MERCANCIA"), GetType(String)), _
                                                                      .Descripcion = Util.AtribuirValorObj(row("DES_TIPO_MERCANCIA"), GetType(String)), _
                                                                      .Identificador = Util.AtribuirValorObj(row("OID_TIPO_MERCANCIA"), GetType(String)), _
                                                                      .Imagen = Util.AtribuirValorObj(row("BIN_TIPO_MERCANCIA"), GetType(Byte()))})

                Next row

                Return TiposMercancia

            End If

            Return Nothing

        End Function

        Public Shared Function ObtenerTiposMercanciaPorCodigos(CodigosTiposMercancia As List(Of String)) As ObservableCollection(Of Clases.TipoMercancia)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_TipoMercancia_ObtenerTiposMercanciaPorCodigo.ToString()
            cmd.CommandType = CommandType.Text

            cmd.CommandText = String.Format(cmd.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, CodigosTiposMercancia, "COD_TIPO_MERCANCIA", cmd, "WHERE", "TM"))
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim TiposMercancia As New ObservableCollection(Of Clases.TipoMercancia)

                For Each row In dt.Rows

                    TiposMercancia.Add(New Clases.TipoMercancia With {.Codigo = Util.AtribuirValorObj(row("COD_TIPO_MERCANCIA"), GetType(String)), _
                                                                      .Descripcion = Util.AtribuirValorObj(row("DES_TIPO_MERCANCIA"), GetType(String)), _
                                                                      .Identificador = Util.AtribuirValorObj(row("OID_TIPO_MERCANCIA"), GetType(String)), _
                                                                      .Imagen = Util.AtribuirValorObj(row("BIN_TIPO_MERCANCIA"), GetType(Byte()))})

                Next row

                Return TiposMercancia

            End If

            Return Nothing

        End Function

        Public Shared Function ObtenerTiposMercancia(Optional CodTipoMercancia As String = Nothing, Optional CodCombinacion As String = Nothing) As ObservableCollection(Of Clases.TipoMercancia)

            ' criar objeto
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            comando.CommandText = My.Resources.Salidas_TipoMercancia_ObtenerTiposMercanciaPorCodigo.ToString()

            Dim filtros As String = " WHERE 1=1 "

            If CodTipoMercancia IsNot Nothing AndAlso Not String.IsNullOrEmpty(CodTipoMercancia) Then
                filtros &= " AND UPPER(COD_TIPO_MERCANCIA) = UPPER('" & CodTipoMercancia.Replace("'", "").Trim() & "') "
            End If

            If CodCombinacion IsNot Nothing AndAlso Not String.IsNullOrEmpty(CodCombinacion) Then
                filtros &= " AND UPPER(COD_COMBINACION) = UPPER('" & CodCombinacion.Replace("'", "").Trim() & "') "
            End If

            comando.CommandText = String.Format(comando.CommandText, filtros)

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, comando.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, comando)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim TiposMercancia As New ObservableCollection(Of Clases.TipoMercancia)

                For Each row In dt.Rows

                    TiposMercancia.Add(New Clases.TipoMercancia With {.Codigo = Util.AtribuirValorObj(row("COD_TIPO_MERCANCIA"), GetType(String)), _
                                                                      .Descripcion = Util.AtribuirValorObj(row("DES_TIPO_MERCANCIA"), GetType(String)), _
                                                                      .Identificador = Util.AtribuirValorObj(row("OID_TIPO_MERCANCIA"), GetType(String)), _
                                                                      .Imagen = Util.AtribuirValorObj(row("BIN_TIPO_MERCANCIA"), GetType(Byte()))})

                Next row

                Return TiposMercancia

            End If

            Return Nothing

        End Function

#End Region

#Region "[INSERTAR]"

        Public Shared Sub InsertarTipoMercanciaRemesa(OidTipoMercancia As String, OidRemesa As String)

            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            With cmd
                .CommandType = CommandType.Text
                .CommandText = My.Resources.Salidas_TipoMercancia_InsertarTipoMercanciaRemesa
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_TIPO_MERCANCIA", ProsegurDbType.Objeto_Id, OidTipoMercancia))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, OidRemesa))
            End With

            'executa o sql e retorna um valor
            DbHelper.AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        Public Shared Sub InsertarTipoMercanciaBulto(OidTipoMercancia As String, OidBulto As String)

            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            With cmd
                .CommandType = CommandType.Text
                .CommandText = My.Resources.Salidas_TipoMercancia_InsertarTipoMercanciaBulto
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_TIPO_MERCANCIA", ProsegurDbType.Objeto_Id, OidTipoMercancia))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, OidBulto))
            End With

            'executa o sql e retorna um valor
            DbHelper.AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

#End Region

#Region "[Calcular Tipo Mercancia]"

        ''' <summary>
        ''' Método responsável por calcular os tipos de mercancia para a remesa e bultos
        ''' </summary>
        ''' <param name="CodigoDelegacion"></param>
        ''' <param name="IdentificadorRemesaLegado"></param>
        ''' <param name="CodigosTipoMercanciaRemesa"></param>
        ''' <remarks></remarks>
        Public Shared Sub CalcularTipoMercancia(CodigoDelegacion As String,
                                                IdentificadorRemesaLegado As String,
                                                parametrosAgruparBultos As Boolean,
                                                trabajaPorBulto As Boolean,
                                                Optional CodigosTipoMercanciaRemesa As List(Of String) = Nothing)


            Dim objRemesas As ObservableCollection(Of Clases.Remesa) = NuevoSalidas.Remesa.RecuperarRemesas( _
                                             New ContractoServicio.NuevoSalidas.Remesa.RecuperarDatosGeneralesRemesa.Peticion _
                                              With {
                                                  .IdentificadorRemesaLegado = IdentificadorRemesaLegado,
                                                  .CodigoDelegacion = CodigoDelegacion,
                                                  .AgruparBultos = parametrosAgruparBultos,
                                                  .CodigosTiposMercancia = CodigosTipoMercanciaRemesa,
                                                  .TrabajaPorBulto = trabajaPorBulto
                                              })

            ' variável responsável por dizer se deve ou não executar
            ' os comandos ao final do método
            Dim executarComandos As Boolean = False

            If objRemesas IsNot Nothing AndAlso objRemesas.Count > 0 Then
                For Each objRemesa In objRemesas

                    ' apaga os tipos de mercancia da remesa e dos bultos antes de 
                    ' re-calcular
                    BorrarTipoMercanciasRemesaBultos(objRemesa)

                    ' Si el parámetro BOL_ATM = true: el código mercancía de la remesa y de sus bultos 
                    ' serán del tipo ATM. Gravar registro del tipo mercancía en las tablas correspondientes, 
                    ' o sea, gravar un registro del tipo mercancía para la remesa (GEPR_TREMESA_TIPO_MERCANCIA) 
                    ' y gravar un registro del tipo mercancía para cada bulto perteneciente a remesa (GEPR_TBULTO_TIPO_MERCANCIA).
                    If objRemesa.EsRemesaATM Then

                        ' recupera o tipo de mercancia ATM
                        'Respuesta = New ObtenerTiposMercancia.Respuesta With {.TiposMercancia = TipoMercancia.ObtenerTiposMercancia()}
                        Dim TiposMercancia As ObservableCollection(Of Clases.TipoMercancia) = TipoMercancia.ObtenerTiposMercancia(Constantes.CONST_TIPO_MERCANCIA_ATM)

                        ' valida se o tipo existe na base de dados
                        If TiposMercancia IsNot Nothing AndAlso TiposMercancia.Count > 0 Then

                            ' adiciona o tipo mercancia no nível de remesa
                            TipoMercancia.InsertarTipoMercanciaRemesa(TiposMercancia(0).Identificador, objRemesa.Identificador)

                            ' verifica se existem bultos na remesa
                            If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
                                ' percorre os bultos (se houver)
                                For Each bulto In objRemesa.Bultos
                                    ' adiciona o tipo mercancia no nível de bulto
                                    TipoMercancia.InsertarTipoMercanciaBulto(TiposMercancia(0).Identificador, bulto.Identificador)
                                Next
                            End If

                        Else

                            ' Si no encontrar en la base de datos el código de tipo de mercancía ATM, deverá regresar un error
                            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Tradutor.Traduzir("002_srv_msg_remesatipomercanciainvalido"), Constantes.CONST_TIPO_MERCANCIA_ATM))

                        End If

                    Else

                        ' Si el sistema legado no enviar el código tipo  mercancía: a través de la división en 
                        ' bultos de la remesa obtener el tipo de mercancía, o sea, para cada bulto verificar 
                        ' su tipo de efectivo y validar si existe algún tipo de mercancía correspondiente. 
                        ' Caso el bulto tenga más de un tipo de efectivo, validar si existe un tipo mercancía 
                        ' con la combinación de los efectivos. Por ejemplo, si en la división un determinado bulto 
                        ' contiene efectivos del tipo billete (GEPR_TDENOMINACION.bol_billete = true), moneda 
                        ' (GEPR_TDENOMINACION.bol_billete = false) y extranjero (si la divisa es diferente de la 
                        ' divisa local), entonces validar si existe en la BBDD un tipo mercancía con la combinación 
                        ' billete/moneda/extranjero (GEPR_TTIPO_MERCANCIA.COD_COMBINACION), caso exista deberá grabar 
                        ' registro del tipo mercancía en las tablas correspondientes, o sea, gravar un registro del 
                        ' tipo mercancía para la remesa (GEPR_TREMESA_TIPO_MERCANCIA) y gravar un registro del tipo 
                        ' mercancía para el bulto (GEPR_TBULTO_TIPO_MERCANCIA). Caso no exista un tipo mercancía con 
                        ' la combinación, para cada tipo de efectivo del bulto, grabar un registro del tipo de mercancía 
                        ' para la remesa y un tipo mercancía para el bulto. 
                        If CodigosTipoMercanciaRemesa Is Nothing OrElse CodigosTipoMercanciaRemesa.Count = 0 Then

                            AtribuirMercanciasRemesaBultos(objRemesa)

                        Else

                            ' Si el sistema legado enviar el código tipo mercancía y lo mismo existir en la BBDD 
                            ' (GEPR_TTIPO_MERCANCIA): en este caso, el código mercancía de la remesa y de sus bultos 
                            ' serán del tipo informado por el legado. Gravar registro del tipo mercancía en las tablas
                            ' correspondientes, o sea, gravar un registro del tipo mercancía para la remesa 
                            ' (GEPR_TREMESA_TIPO_MERCANCIA) y gravar un registro del tipo mercancía para cada bulto 
                            ' perteneciente a remesa (GEPR_TBULTO_TIPO_MERCANCIA).
                            Dim TiposMercancia As ObservableCollection(Of Clases.TipoMercancia) = TipoMercancia.ObtenerTiposMercanciaPorCodigos(CodigosTipoMercanciaRemesa)

                            ' verifica se o código existe no banco de dados
                            If TiposMercancia IsNot Nothing AndAlso TiposMercancia.Count > 0 Then

                                For Each tpMercancia In TiposMercancia
                                    ' insere o tipo mercancia no nível de remesa
                                    TipoMercancia.InsertarTipoMercanciaRemesa(tpMercancia.Identificador, objRemesa.Identificador)

                                    ' verifica se existem bultos
                                    If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
                                        ' percorre os bultos
                                        For Each bulto In objRemesa.Bultos
                                            ' insere o tipo mercancia no nível de bulto
                                            TipoMercancia.InsertarTipoMercanciaBulto(tpMercancia.Identificador, bulto.Identificador)
                                        Next
                                    End If

                                Next

                            Else

                                ' Si el sistema legado enviar el código tipo mercancía y lo mismo no existir en la BBDD 
                                ' (GEPR_TTIPO_MERCANCIA): regresar un mensaje de error informando que el código tipo mercancía 
                                ' enviado no existe en la BBDD.
                                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Tradutor.Traduzir("002_srv_msg_remesatipomercanciainvalido"), String.Join("', '", CodigosTipoMercanciaRemesa.ToArray())))

                            End If

                        End If

                    End If

                Next

            End If

        End Sub

        ''' <summary>
        ''' Atribui a remesa e bultos os tipos mercancia de acordo com o desglose
        ''' </summary>
        ''' <param name="objRemesa"></param>
        ''' <remarks></remarks>
        Private Shared Sub AtribuirMercanciasRemesaBultos(objRemesa As Clases.Remesa)

            Dim codigoDivisaLocal As String = String.Empty

            ' verifica se a divisa local foi configurada no arquivo de configuração
            If Configuration.ConfigurationManager.AppSettings("MercanciaCodigoDivisaLocal") IsNot Nothing AndAlso Not String.IsNullOrEmpty(Configuration.ConfigurationManager.AppSettings("MercanciaCodigoDivisaLocal")) Then
                codigoDivisaLocal = Configuration.ConfigurationManager.AppSettings("MercanciaCodigoDivisaLocal")
            End If

            Dim remesaMercancias As New List(Of String)
            Dim bultosMercancias As New List(Of String)
            Dim oidsMercanciaRemesa As New List(Of String)
            Dim oidsMercanciaBulto As New List(Of String)

            ' verifica se o bulto tem billetes
            If ((From b In objRemesa.Bultos Where b.Divisas IsNot Nothing AndAlso b.Divisas.Count > 0).Count > 0) Then
                'Para remesa sin ser del tipo modulo:

                ' percorre os bultos
                For Each bulto In objRemesa.Bultos.Where(Function(b) b.Divisas IsNot Nothing AndAlso b.Divisas.Count > 0)

                    ' verifica se o bulto tem moedas extrangeiras
                    If bulto.EsModulo AndAlso Not bultosMercancias.Contains(Constantes.CONST_TIPO_MERCANCIA_MODULO) Then
                        bultosMercancias.Add(Constantes.CONST_TIPO_MERCANCIA_MODULO)
                    End If

                    ' percorre os efectivos presentes no bulto
                    For Each efectivo In bulto.Divisas

                        ' verifica se o bulto tem moedas extrangeiras
                        If Not codigoDivisaLocal.Trim().ToUpper() = efectivo.CodigoISO.Trim().ToUpper() AndAlso Not bultosMercancias.Contains(Constantes.CONST_TIPO_MERCANCIA_EXTRANJERO) Then
                            bultosMercancias.Add(Constantes.CONST_TIPO_MERCANCIA_EXTRANJERO)
                        End If

                        ' percorre os detalhes do efectivo atual
                        For Each efectivoDetalle In efectivo.Denominaciones.FindAll(Function(v) v.ValorDenominacion IsNot Nothing AndAlso v.ValorDenominacion.Count > 0)

                            ' verifica se o bulto tem billetes
                            If efectivoDetalle.EsBillete AndAlso Not bultosMercancias.Contains(Constantes.CONST_TIPO_MERCANCIA_BILLETE) Then
                                bultosMercancias.Add(Constantes.CONST_TIPO_MERCANCIA_BILLETE)
                            End If

                            ' verifica se o bulto tem moedas
                            If Not efectivoDetalle.EsBillete AndAlso Not bultosMercancias.Contains(Constantes.CONST_TIPO_MERCANCIA_MONEDA) Then
                                bultosMercancias.Add(Constantes.CONST_TIPO_MERCANCIA_MONEDA)
                            End If

                        Next

                    Next

                    Dim oidCombinacao As String = String.Empty

                    ' se tem mais que uma mercancia, deve buscar por uma combinação
                    If bultosMercancias.Count > 1 Then


                        Dim combinacao As String = String.Empty

                        ' formata a combinação de códigos de mercancia
                        For Each mercancia In bultosMercancias
                            combinacao &= mercancia & "|"
                        Next

                        combinacao = combinacao.Substring(0, (combinacao.Length - 1))

                        ' faz a pesquisa de acordo com a combinação
                        Dim tiposMercacia As ObservableCollection(Of Clases.TipoMercancia) = TipoMercancia.ObtenerTiposMercancia(Nothing, combinacao)

                        ' se encontrou algum tipo mercancia com a combinação informada,
                        ' define o oid
                        If tiposMercacia IsNot Nothing AndAlso tiposMercacia.Count > 0 Then
                            oidCombinacao = tiposMercacia.First.Identificador
                        End If

                    End If

                    If Not String.IsNullOrEmpty(oidCombinacao) Then
                        oidsMercanciaBulto.Add(oidCombinacao)
                    Else
                        For Each mercancia In bultosMercancias

                            ' faz a pesquisa de acordo com a combinação
                            Dim tiposMercacia As ObservableCollection(Of Clases.TipoMercancia) = TipoMercancia.ObtenerTiposMercancia(mercancia)

                            ' se encontrou algum tipo mercancia com a combinação informada,
                            ' define o oid
                            If tiposMercacia IsNot Nothing AndAlso tiposMercacia.Count > 0 Then
                                oidsMercanciaBulto.Add(tiposMercacia.FirstOrDefault.Identificador)
                            End If

                        Next
                    End If

                    ' percorre os tipos mercancia que deverão ser inseridos no bulto
                    For Each oidMercancia In oidsMercanciaBulto

                        ' insere no bulto atual o tipo mercancia
                        TipoMercancia.InsertarTipoMercanciaBulto(oidMercancia, bulto.Identificador)

                        ' verifica se o tipo mercancia já existe ou não na coleção que deverá 
                        ' ser inserida no nível de remesa
                        If Not oidsMercanciaRemesa.Contains(oidMercancia) Then
                            oidsMercanciaRemesa.Add(oidMercancia)
                        End If

                    Next

                    oidsMercanciaBulto.Clear()

                Next

                ' percorre a coleção de tipo mercancia que deverá ser inserida na remessa
                For Each oidMercancia In oidsMercanciaRemesa

                    ' insere na remesa o tipo mercancia
                    TipoMercancia.InsertarTipoMercanciaRemesa(oidMercancia, objRemesa.Identificador)

                Next

                oidsMercanciaRemesa.Clear()

            End If

        End Sub

        ''' <summary>
        ''' Apaga os tipos de mercancia da remesa e dos bultos
        ''' </summary>
        ''' <param name="remesa"></param>
        ''' <remarks></remarks>
        Private Shared Sub BorrarTipoMercanciasRemesaBultos(remesa As Clases.Remesa)

            ' verifica se o objeto existe
            If remesa IsNot Nothing Then

                ' verifica se existem bultos
                If remesa.Bultos IsNot Nothing Then

                    ' percorre os bultos
                    For Each b In remesa.Bultos

                        ' apaga os tipos mercancia do bulto
                        BorrarTipoMercanciasBulto(b.Identificador)

                    Next

                End If

                ' apaga os tipos mercancia da remesa
                BorrarTipoMercanciasRemesa(remesa.Identificador)

            End If

        End Sub

        ''' <summary>
        ''' Apaga os tipos mercancias da remesa
        ''' </summary>
        ''' <param name="oidRemesa"></param>
        ''' <remarks></remarks>
        Private Shared Sub BorrarTipoMercanciasRemesa(oidRemesa As String)

            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            With cmd
                .CommandType = CommandType.Text
                .CommandText = My.Resources.Salidas_TipoMercancia_BorrarTipoMercanciasRemesa
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Identificador_Alfanumerico, oidRemesa))
            End With

            'executa o sql e retorna um valor
            DbHelper.AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        ''' <summary>
        ''' Apaga os tipos mercancia do bulto
        ''' </summary>
        ''' <param name="oidBulto"></param>
        ''' <remarks></remarks>
        Private Shared Sub BorrarTipoMercanciasBulto(oidBulto As String)

            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            With cmd
                .CommandType = CommandType.Text
                .CommandText = My.Resources.Salidas_TipoMercancia_BorrarTipoMercanciasBulto
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Identificador_Alfanumerico, oidBulto))
            End With

            'executa o sql e retorna um valor
            DbHelper.AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

#End Region


    End Class

End Namespace
