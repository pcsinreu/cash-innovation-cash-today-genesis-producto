Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.NuevoSalidas.Remesa
Imports Prosegur.Genesis.ContractoServicio
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports System.Windows.Forms
Imports System.Threading.Tasks
Imports System.Collections.Concurrent

Namespace NuevoSalidas

    Public Class Remesa

        ''' <summary>
        ''' Valida se a quantidade de remesas recebida pelo processo RecuperarPedidosSOL permanece a mesma.
        ''' </summary>
        ''' <param name="IdentificadorOT"></param>
        ''' <param name="CantidadRemesas"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ValidarCantidadRemesas(IdentificadorOT As String, CantidadRemesas As Integer) As Boolean

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesas_ValidarCantidadRemesas)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_OT", ProsegurDbType.Objeto_Id, IdentificadorOT))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "CANTIDAD_REMESAS", ProsegurDbType.Inteiro_Longo, CantidadRemesas))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Dim PuedeModificar As Boolean = Util.AtribuirValorObj(dt.Rows(0)("PUEDE_MODIFICAR"), GetType(Boolean))
                Dim HayRemesasProcesadas As Boolean = Util.AtribuirValorObj(dt.Rows(0)("HAY_REMESAS_PROCESADAS"), GetType(Boolean))

                If PuedeModificar AndAlso Not HayRemesasProcesadas Then
                    Return True
                End If

            End If

            Return False
        End Function

        ''' <summary>
        ''' Inserir remesas divididas
        ''' ' OBS: os script estão no código temporariamente, porque o projeto de Acesso a Dados estava checado quando o método foi criado
        ''' </summary>
        ''' <param name="identificadorRemesaPadre"></param>
        ''' <param name="remesas"></param>
        ''' <param name="codigoPuesto"></param>
        ''' <remarks></remarks>
        Public Shared Sub InsertarRemesasHijas(identificadorRemesaPadre As String, remesas As List(Of Clases.Remesa), codigoPuesto As String, codigoDelegacion As String)

            If remesas IsNot Nothing AndAlso remesas.Count > 0 Then

                For Each remesa In remesas

                    Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_InsertarRemesaHija)
                    cmd.CommandType = CommandType.Text

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, remesa.Identificador))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA_PADRE", ProsegurDbType.Objeto_Id, identificadorRemesaPadre))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA_LEGADO", ProsegurDbType.Objeto_Id, remesa.IdentificadorExterno))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_ESTADO_REMESA", ProsegurDbType.Descricao_Curta, remesa.Estado.RecuperarValor))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO_BLOQUEO", ProsegurDbType.Descricao_Longa, remesa.UsuarioBloqueo))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Descricao_Curta, remesa.UsuarioModificacion))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, remesa.FechaHoraModificacion))

                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

                    Dim bulto As Clases.Bulto = remesa.Bultos.FirstOrDefault

                    ' inserir malote
                    NuevoSalidas.Bulto.InsertarBulto(bulto.Identificador, bulto.TipoBulto.Identificador, remesa.Identificador,
                                                     String.Empty, bulto.Estado.RecuperarValor, bulto.Preparado,
                                                     bulto.UsuarioModificacion, bulto.UsuarioBloqueo, bulto.FechaHoraModificacion, Nothing)

                    If remesa.Estado = EstadoRemesa.EnCurso Then

                        ' inserir registro lista de trabalho
                        NuevoSalidas.Remesa.AsignarPuesto(remesa.Identificador, codigoPuesto, remesa.UsuarioModificacion, remesa.FechaHoraModificacion, codigoDelegacion)

                    End If

                Next remesa

                'Altera a remesa para Dividida
                ActualizarEstadoRemesa(identificadorRemesaPadre, _
                                       Enumeradores.EstadoRemesa.Dividido.RecuperarValor(), _
                                       remesas.First.UsuarioModificacion, _
                                       remesas.First.FechaHoraModificacion, False, String.Empty)
            End If

        End Sub

        ''' <summary>
        ''' Borrar remesas divididas
        ''' ' OBS: os script estão no código temporariamente, porque o projeto de Acesso a Dados estava checado quando o método foi criado
        ''' </summary>
        ''' <param name="identificadorRemesaOriginal"></param>
        ''' <param name="Remesas"></param>
        ''' <remarks></remarks>
        Public Shared Sub BorrarRemesas(identificadorRemesaOriginal As String, Remesas As List(Of Clases.Remesa))

            If Remesas IsNot Nothing AndAlso Remesas.Count > 0 Then

                Dim identificadoresRemesasHijas As List(Of String) = (From r In Remesas
                                                                Select r.Identificador).ToList

                BorrarRemesasListaTrabajo(identificadoresRemesasHijas)

                Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

                cmd.CommandText = My.Resources.Salidas_Bulto_RecuperarBultosPorRemesas
                cmd.CommandText = String.Format(cmd.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, identificadoresRemesasHijas, "OID_REMESA", cmd, "WHERE", "B"))
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

                Dim dtBultosRemesas As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

                Dim identificadoresBultos As List(Of String) = Nothing
                If dtBultosRemesas IsNot Nothing AndAlso dtBultosRemesas.Rows.Count > 0 Then
                    identificadoresBultos = New List(Of String)
                    For Each row In dtBultosRemesas.Rows
                        identificadoresBultos.Add(row("OID_BULTO"))
                    Next row
                End If

                ' se as remessas a serem excluidas possuirem bultos
                If identificadoresBultos IsNot Nothing AndAlso identificadoresBultos.Count > 0 Then
                    'Exclui as mercancias dos bultos
                    cmd = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
                    cmd.CommandText = String.Format(My.Resources.Salidas_TipoMercancia_BorrarTipoMercanciasBultos, Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, identificadoresBultos, "OID_BULTO", cmd, "WHERE", "B"))
                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)
                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

                    'Exclui os efectivos dos bultos
                    cmd = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
                    cmd.CommandText = String.Format(My.Resources.Salidas_Bulto_BorrarEfectivosBultos, Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, identificadoresBultos, "OID_BULTO", cmd, "WHERE", "B"))
                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)
                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

                    'Exclui os bultos
                    cmd = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
                    cmd.CommandText = String.Format(My.Resources.Salidas_Bulto_BorrarBultos, Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, identificadoresBultos, "OID_BULTO", cmd, "WHERE", "B"))
                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)
                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

                End If

                cmd = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
                cmd.CommandText = String.Format(My.Resources.Salidas_Remesa_BorrarRemesas, Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, identificadoresRemesasHijas, "OID_REMESA", cmd, "WHERE", "R"))
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

                'Se estiver excluindo mais de uma remesa, então é porque está excluíndo todas as remesas
                If Remesas.Count > 1 Then
                    ' Volta a remesa para o estado pendente
                    ActualizarEstadoRemesa(identificadorRemesaOriginal, _
                                              Enumeradores.EstadoRemesa.Pendiente.RecuperarValor(), _
                                              Remesas.First.UsuarioModificacion, _
                                              Remesas.First.FechaHoraModificacion, False, String.Empty)
                End If

            End If
        End Sub

        Public Shared Sub BorrarRemesasListaTrabajo(identificadoresRemesas As List(Of String))

            If identificadoresRemesas IsNot Nothing AndAlso identificadoresRemesas.Count > 0 Then

                Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

                cmd.CommandText = My.Resources.Salidas_Remesa_BorrarRemesasListaTrabajo
                cmd.CommandText = String.Format(cmd.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, identificadoresRemesas, "OID_REMESA", cmd, "WHERE", "LT"))
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

            End If

        End Sub

        ''' <summary>
        ''' Obter situações remessa.
        ''' </summary>
        ''' <param name="codigoDelegacion">Código da Delegação</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerSituacionRemesas(codigoDelegacion As String) As ObtenerSituacionRemesas.SituacionesRemesa

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_ObtenerSituacionRemesas)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion))

            Return CargarSituacionRemesa(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd))

        End Function

        Private Shared Function CargarSituacionRemesa(dtRetorno As DataTable) As ObtenerSituacionRemesas.SituacionesRemesa

            ' Variável que recebe a situações das remessas
            Dim objSituacionesRemesas As ObtenerSituacionRemesas.SituacionesRemesa = Nothing

            ' Verifica se retornou algum dado
            If dtRetorno IsNot Nothing Then

                ' Cria uma nova instancia para a situação da remessa
                objSituacionesRemesas = New ObtenerSituacionRemesas.SituacionesRemesa

                ' Para cada estado da remessa retornado
                For Each dr As DataRow In dtRetorno.Rows

                    ' Verifica o tipo do estado
                    Select Case dr("COD_ESTADO_REMESA")

                        Case EstadoRemesa.Pendiente.RecuperarValor()

                            objSituacionesRemesas.CantidadRemesasPendientes = dr("CANTIDAD")

                        Case EstadoRemesa.Asignada.RecuperarValor()

                            objSituacionesRemesas.CantidadRemesasAsignadas = dr("CANTIDAD")

                        Case EstadoRemesa.Procesada.RecuperarValor()

                            objSituacionesRemesas.CantidadRemesasProcesadas = dr("CANTIDAD")

                        Case Else

                            objSituacionesRemesas.CantidadRemesasSinBultos = dr("CANTIDAD")

                    End Select

                Next

            End If

            ' Retorna as situações das remesas
            Return objSituacionesRemesas

        End Function

        Public Shared Function RecuperarIdentificadorRemesaConPrecintoBulto(CodigoPrecintoBulto As String) As String

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_RecuperarIdentificadorRemesaConCodigoPrecinto)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_PRECINTO_BULTO", ProsegurDbType.Identificador_Alfanumerico, CodigoPrecintoBulto))

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALIDAS, cmd)
        End Function

        ''' <summary>
        ''' Recupera as remesas
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarRemesas(Peticion As ContractoServicio.NuevoSalidas.Remesa.RecuperarDatosGeneralesRemesa.Peticion) As ObservableCollection(Of Clases.Remesa)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            comando.CommandText = My.Resources.Salidas_Remesa_RecuperarDatos
            comando.CommandType = CommandType.Text

            MontaClausulaRemesas(Peticion, comando)

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, comando.CommandText)

            Return PopularRemesas(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, comando), Peticion.CodigoSubCanalATM, Peticion.CrearConfiguracionNivelSaldo, Peticion.TrabajaPorBulto)

        End Function

        Public Shared Function RecuperarDatosRemesasPadreYHija(Peticion As ContractoServicio.NuevoSalidas.Remesa.RecuperarDatosRemesasPadreYHija.Peticion) As ObservableCollection(Of Clases.Remesa)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            comando.CommandText = My.Resources.Salidas_Remesa_RecuperarDatosRemesaPadreYHija
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "AGRUPA_BULTOS", ProsegurDbType.Logico, Peticion.AgruparBultos))

            If Peticion.IdentificadoresRemesas IsNot Nothing Then
                Dim oidRemesa As String = Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.IdentificadoresRemesas, "OID_REMESA", comando, "WHERE", "R")
                Dim oidRemesaPadre As String = Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.IdentificadoresRemesas, "OID_REMESA_PADRE", comando, "WHERE", "R")
                comando.CommandText = String.Format(comando.CommandText, oidRemesa, oidRemesaPadre)
            End If

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, comando.CommandText)

            Return RemesasPadreYHija(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, comando), Peticion.CodigoSubCanalATM, Peticion.CrearConfiguracionNivelSaldo)


        End Function

        ''' <summary>
        ''' Popula a remesa
        ''' </summary>
        ''' <param name="dt"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function PopularRemesas(dt As DataTable, CodigoSubCanalATM As String, CrearConfiguracionNivelSaldo As Boolean, trabajaPorBulto As Boolean) As ObservableCollection(Of Clases.Remesa)

            Dim objRemesas As ObservableCollection(Of Clases.Remesa) = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Dim objDivisasRemesas As Dictionary(Of String, ObservableCollection(Of Clases.Divisa)) = Nothing
                Dim objDivisasBultos As List(Of Tuple(Of String, Boolean, ObservableCollection(Of Clases.Divisa))) = Nothing
                Dim objObjetosRemesas As Dictionary(Of String, ObservableCollection(Of Clases.Objeto)) = Nothing
                Dim objModulosRemesas As Dictionary(Of String, ObservableCollection(Of Clases.Modulo)) = Nothing
                Dim objClientesSaldo As List(Of Clases.Cliente) = Nothing
                Dim objTiposMercanciaRemesas As Dictionary(Of String, ObservableCollection(Of Clases.TipoMercancia)) = Nothing
                Dim objTiposMercanciaBultos As Dictionary(Of String, ObservableCollection(Of Clases.TipoMercancia)) = Nothing
                Dim objDivisasCompletas As ObservableCollection(Of Clases.Divisa) = Nothing

                Dim IdentificadoresRemesas As List(Of String) = (From dr As DataRow In dt.Rows
                                                                 Select Convert.ToString(Util.AtribuirValorObj(dr("OID_REMESA"), GetType(String))) Distinct).ToList

                Dim IdentificadoresBultos As List(Of String) = (From dr As DataRow In dt.Rows
                                                                Select Convert.ToString(Util.AtribuirValorObj(dr("OID_BULTO"), GetType(String))) Distinct).ToList

                If IdentificadoresRemesas IsNot Nothing AndAlso IdentificadoresRemesas.Count > 0 Then
                    IdentificadoresRemesas.RemoveAll(Function(r) String.IsNullOrEmpty(r))
                End If

                If IdentificadoresBultos IsNot Nothing AndAlso IdentificadoresBultos.Count > 0 Then
                    IdentificadoresBultos.RemoveAll(Function(b) String.IsNullOrEmpty(b))
                End If

                ' armazena os identificadores de bultos que não foram buscados no filtro inicial, porque pode ocorrer na gestão de bultos que algum bulto esteja em outro estado e é necessário neste momento ter todos os bultos da remesa para que seus valores sejam validados para definir se já estão dividos ou não
                Dim identificadorBultosTemporarios As List(Of String) = Nothing
                Dim dtBultoTemporarios As DataTable = Nothing

                ' se trabalha por bulto e tem bultos na lista, buscar outros bultos que não vierem no primeiro filtro, caso tenha
                If trabajaPorBulto AndAlso IdentificadoresBultos IsNot Nothing AndAlso IdentificadoresBultos.Count > 0 Then
                    dtBultoTemporarios = AccesoDatos.Genesis.Bulto.RecuperarBultosRemesas(IdentificadoresRemesas, IdentificadoresBultos)

                    If dtBultoTemporarios IsNot Nothing AndAlso dtBultoTemporarios.Rows.Count > 0 Then

                        identificadorBultosTemporarios = (From dr As DataRow In dtBultoTemporarios.Rows
                                                          Select Convert.ToString(Util.AtribuirValorObj(dr("OID_BULTO"), GetType(String))) Distinct).ToList

                        IdentificadoresBultos.AddRange(identificadorBultosTemporarios)
                        dt.Merge(dtBultoTemporarios)

                    End If

                End If

                Dim objClientes As List(Of Tuple(Of String, String, String, String)) = _
                    (From dr As DataRow In dt.Rows
                    Select New Tuple(Of String, String, String, String)(Util.AtribuirValorObj(dr("COD_CLIENTE_DESTINO"), GetType(String)),
                                                                        Util.AtribuirValorObj(dr("COD_SUBCLIENTE"), GetType(String)),
                                                                        Util.AtribuirValorObj(dr("COD_PUNTO_SERVICIO"), GetType(String)),
                                                                        Util.AtribuirValorObj(dr("COD_CANAL"), GetType(String))) Distinct).ToList

                Dim TfDivisasRemesas As Task = Nothing
                Dim TfObjetos As Task = Nothing
                Dim TfModulos As Task = Nothing
                Dim TfTipoMercanciaRemesa As Task = Nothing
                Dim TfTipoMercanciaBulto As Task = Nothing
                Dim TfDivisasBultos As Task = Nothing
                Dim TfDivisasCompletas As Task = Nothing

                TfDivisasRemesas = New Task(Sub()
                                                objDivisasRemesas = Divisa.RecuperarDivisasRemesas(IdentificadoresRemesas)
                                            End Sub)

                TfDivisasBultos = New Task(Sub()
                                               objDivisasBultos = Divisa.RecuperarDivisasBultosModulos(IdentificadoresBultos)
                                           End Sub)

                TfObjetos = New Task(Sub()
                                         objObjetosRemesas = Objeto.RecuperarObjetosRemesas(IdentificadoresRemesas)
                                     End Sub)

                TfModulos = New Task(Sub()
                                         objModulosRemesas = Modulo.RecuperarModulosRemesas(IdentificadoresRemesas)
                                     End Sub)

                TfTipoMercanciaRemesa = New Task(Sub()
                                                     objTiposMercanciaRemesas = TipoMercancia.RecuperarTiposMercanciaRemesas(IdentificadoresRemesas)
                                                 End Sub)

                TfTipoMercanciaBulto = New Task(Sub()
                                                    objTiposMercanciaBultos = TipoMercancia.RecuperarTiposMercanciaBultos(IdentificadoresBultos)
                                                End Sub)

                TfDivisasCompletas = New Task(Sub()
                                                  objDivisasCompletas = AccesoDatos.Genesis.Divisas.ObtenerDivisas(, , , , True, False, True, True, )
                                              End Sub)


                TfDivisasRemesas.Start()
                TfObjetos.Start()
                TfModulos.Start()
                TfTipoMercanciaRemesa.Start()
                TfTipoMercanciaBulto.Start()
                TfDivisasBultos.Start()
                TfDivisasCompletas.Start()

                Task.WaitAll(New Task() {TfDivisasRemesas, TfDivisasBultos, TfObjetos, TfModulos, TfTipoMercanciaRemesa, TfTipoMercanciaBulto, TfDivisasCompletas})


                Dim objRemesasRetorno As New ConcurrentBag(Of Clases.Remesa)
                Dim obj As New Object

                Parallel.ForEach(IdentificadoresRemesas, Sub(IdRemesa)

                                                             Dim objRemesa As Clases.Remesa = Nothing
                                                             Dim IdentificadorRemesaPadre As String = String.Empty
                                                             Dim CodigoClienteFacturacion As String = String.Empty
                                                             Dim CodigoClienteSaldo As String = String.Empty
                                                             Dim CodigoSubCliente As String = String.Empty
                                                             Dim CodigoPuntoServicio As String = String.Empty
                                                             Dim CodigoCanal As String = String.Empty
                                                             Dim CodigoSubCanal As String = String.Empty
                                                             Dim CodigoSector As String = String.Empty
                                                             Dim DescripcionModeloCajero As String = String.Empty
                                                             Dim DescripcionModeloRed As String = String.Empty
                                                             Dim ModalidadRecogida As Integer = 0
                                                             Dim CodigoCajero As String = String.Empty
                                                             Dim IdentificadorBulto As String = String.Empty
                                                             Dim CodigoPrecinto As String = String.Empty
                                                             Dim objPrecintos As ObservableCollection(Of String) = Nothing
                                                             Dim dtAux As DataTable = dt

                                                             For Each dr In (From dr1 As DataRow In dtAux.Rows Where dr1("OID_REMESA") = IdRemesa)
                                                                 'For Each dr In dt.Rows

                                                                 CodigoClienteFacturacion = Util.AtribuirValorObj(dr("COD_CLIENTE_FACTURACION"), GetType(String))
                                                                 CodigoClienteSaldo = Util.AtribuirValorObj(dr("COD_CLIENTE_SALDO"), GetType(String))
                                                                 CodigoSubCliente = Util.AtribuirValorObj(dr("COD_SUBCLIENTE"), GetType(String))
                                                                 CodigoPuntoServicio = Util.AtribuirValorObj(dr("COD_PUNTO_SERVICIO"), GetType(String))
                                                                 CodigoCanal = Util.AtribuirValorObj(dr("COD_CANAL"), GetType(String))
                                                                 CodigoSubCanal = Util.AtribuirValorObj(dr("COD_SUBCANAL"), GetType(String))
                                                                 CodigoSector = Util.AtribuirValorObj(dr("COD_SECTOR"), GetType(String))
                                                                 DescripcionModeloCajero = Util.AtribuirValorObj(dr("DES_MODELO_CAJERO"), GetType(String))
                                                                 CodigoCajero = Util.AtribuirValorObj(dr("COD_ATM"), GetType(String))
                                                                 DescripcionModeloRed = Util.AtribuirValorObj(dr("DES_RED"), GetType(String))
                                                                 ModalidadRecogida = Util.AtribuirValorObj(dr("NEC_MOD_HAB_REC"), GetType(Integer))
                                                                 IdentificadorRemesaPadre = Util.AtribuirValorObj(dr("OID_REMESA_PADRE"), GetType(String))
                                                                 IdentificadorBulto = Util.AtribuirValorObj(dr("OID_BULTO"), GetType(String))

                                                                 objRemesa = (From objR In objRemesasRetorno Where objR.Identificador = IdRemesa).FirstOrDefault

                                                                 If objRemesa Is Nothing Then

                                                                     objRemesa = New Clases.Remesa

                                                                     objRemesa.Identificador = IdRemesa
                                                                     objRemesa.IdentificadorRemesaPadre = IdentificadorRemesaPadre
                                                                     objRemesa.EsRemesaModificada = Util.AtribuirValorObj(dr("MODIFICADA"), GetType(Boolean))
                                                                     objRemesa.IdentificadorExterno = Util.AtribuirValorObj(dr("OID_REMESA_LEGADO"), GetType(String))
                                                                     objRemesa.IdentificadorOT = Util.AtribuirValorObj(dr("OID_OT"), GetType(String))
                                                                     objRemesa.CodigoSecuencia = Util.AtribuirValorObj(dr("COD_SECUENCIA"), GetType(String))
                                                                     objRemesa.CodigoServicioContratado = Util.AtribuirValorObj(dr("COD_SERVICIO"), GetType(String))
                                                                     objRemesa.CodigoDelegacion = Util.AtribuirValorObj(dr("COD_DELEGACION"), GetType(String))
                                                                     objRemesa.Estado = Extenciones.RecuperarEnum(Of EstadoRemesa)(Util.AtribuirValorObj(dr("COD_ESTADO_REMESA"), GetType(String)))
                                                                     objRemesa.FechaHoraPreparacion = Util.AtribuirValorObj(dr("FYH_INICIO"), GetType(DateTime))
                                                                     objRemesa.FechaHoraInicioArmado = Util.AtribuirValorObj(dr("FYH_INICIO_ARMADO"), GetType(DateTime))
                                                                     objRemesa.FechaHoraFinArmado = Util.AtribuirValorObj(dr("FYH_FIN_ARMADO"), GetType(DateTime))
                                                                     objRemesa.TiposMercancia = TipoMercancia.RecuperarTiposMercanciaRemesa(IdRemesa)
                                                                     objRemesa.UsuarioBloqueo = Util.AtribuirValorObj(dr("COD_USUARIO_BLOQUEO"), GetType(String))
                                                                     objRemesa.EsMedioPago = Util.AtribuirValorObj(dr("BOL_MEDIO_PAGO"), GetType(String))
                                                                     objRemesa.DescripcionContacto1 = Util.AtribuirValorObj(dr("DES_CONTACTO1"), GetType(String))
                                                                     objRemesa.DescripcionContacto2 = Util.AtribuirValorObj(dr("DES_CONTACTO2"), GetType(String))
                                                                     objRemesa.DescripcionContacto3 = Util.AtribuirValorObj(dr("DES_CONTACTO3"), GetType(String))
                                                                     objRemesa.DescripcionContacto4 = Util.AtribuirValorObj(dr("DES_CONTACTO4"), GetType(String))
                                                                     objRemesa.EsRemesaATM = Util.AtribuirValorObj(dr("BOL_ATM"), GetType(Boolean))
                                                                     objRemesa.IdentificadorRemesaModificada = Util.AtribuirValorObj(dr("OID_REMESA_REF"), GetType(String))

                                                                     If objDivisasRemesas IsNot Nothing AndAlso objDivisasRemesas.Count > 0 Then

                                                                         objRemesa.Divisas = (From objRemDiv In objDivisasRemesas
                                                                                              Where objRemDiv.Key = objRemesa.Identificador
                                                                                              Select objRemDiv.Value).FirstOrDefault

                                                                     End If

                                                                     If objObjetosRemesas IsNot Nothing AndAlso objObjetosRemesas.Count > 0 Then

                                                                         objRemesa.Objetos = (From objRemob In objObjetosRemesas
                                                                                              Where objRemob.Key = objRemesa.Identificador
                                                                                              Select objRemob.Value).FirstOrDefault

                                                                     End If

                                                                     If objModulosRemesas IsNot Nothing AndAlso objModulosRemesas.Count > 0 Then

                                                                         objRemesa.Modulos = (From objRemMod In objModulosRemesas
                                                                                              Where objRemMod.Key = objRemesa.Identificador
                                                                                              Select objRemMod.Value).FirstOrDefault

                                                                     End If

                                                                     If objTiposMercanciaRemesas IsNot Nothing AndAlso objTiposMercanciaRemesas.Count > 0 Then

                                                                         objRemesa.TiposMercancia = (From objRemTM In objTiposMercanciaRemesas
                                                                                                    Where objRemTM.Key = objRemesa.Identificador
                                                                                                    Select objRemTM.Value).FirstOrDefault

                                                                     End If

                                                                     'Se possui cliente saldo na tabela de remessa
                                                                     If Not String.IsNullOrEmpty(CodigoClienteSaldo) Then

                                                                         objRemesa.ClienteSaldo = New Clases.Cliente With {.Codigo = CodigoClienteSaldo, _
                                                                                                                                 .Descripcion = Util.AtribuirValorObj(dr("DES_CLIENTE_SALDO"), GetType(String)),
                                                                                                                           .Identificador = Util.AtribuirValorObj(dr("OID_CLIENTE_SALDO"), GetType(String))}

                                                                         If Not String.IsNullOrEmpty(Util.AtribuirValorObj(dr("OID_SUBCLIENTE_SALDO"), GetType(String))) Then
                                                                             objRemesa.ClienteSaldo.SubClientes = New ObjectModel.ObservableCollection(Of Clases.SubCliente)
                                                                             objRemesa.ClienteSaldo.SubClientes.Add(New Clases.SubCliente With {.Identificador = Util.AtribuirValorObj(dr("OID_SUBCLIENTE_SALDO"), GetType(String)),
                                                                                                                                 .Codigo = Util.AtribuirValorObj(dr("COD_SUBCLIENTE_SALDO"), GetType(String)),
                                                                                                                                 .Descripcion = Util.AtribuirValorObj(dr("DES_SUBCLIENTE_SALDO"), GetType(String))})

                                                                             If Not String.IsNullOrEmpty(Util.AtribuirValorObj(dr("OID_PTO_SERVICIO_SALDO"), GetType(String))) Then
                                                                                 objRemesa.ClienteSaldo.SubClientes.First.PuntosServicio = New ObjectModel.ObservableCollection(Of Clases.PuntoServicio)
                                                                                 objRemesa.ClienteSaldo.SubClientes.First.PuntosServicio.Add(New Clases.PuntoServicio With {.Identificador = Util.AtribuirValorObj(dr("OID_PTO_SERVICIO_SALDO"), GetType(String)),
                                                                                                                                                             .Codigo = Util.AtribuirValorObj(dr("COD_PTO_SERVICIO_SALDO"), GetType(String)),
                                                                                                                                                             .Descripcion = Util.AtribuirValorObj(dr("DES_PTO_SERVICIO_SALDO"), GetType(String))})
                                                                             End If
                                                                         End If

                                                                     Else

                                                                         Dim codSubCanal As String = CodigoSubCanal

                                                                         ' se não possuir subCanal
                                                                         If String.IsNullOrEmpty(codSubCanal) Then
                                                                             ' se remessa for de ATM utiliza subcanal configurado no parametro
                                                                             ' senão utiliza o codigo do canal como subCanal
                                                                             If objRemesa.EsRemesaATM Then
                                                                                 codSubCanal = CodigoSubCanalATM
                                                                             Else
                                                                                 codSubCanal = CodigoCanal
                                                                             End If
                                                                         End If


                                                                         objRemesa.ClienteSaldo = Genesis.Cliente.RecuperarClienteTotalizadorSaldo(Util.AtribuirValorObj(dr("COD_CLIENTE_DESTINO"), GetType(String)), _
                                                                                                                                                                                    CodigoSubCliente, _
                                                                                                                                                                                   CodigoPuntoServicio,
                                                                                                                                                                                   codSubCanal)

                                                                     End If
                                                                     If Not String.IsNullOrEmpty(CodigoClienteFacturacion) Then

                                                                         objRemesa.ClienteFacturacion = New Clases.Cliente With {.Codigo = CodigoClienteFacturacion, _
                                                                                                                                 .Descripcion = Util.AtribuirValorObj(dr("DES_CLIENTE_FACTURACION"), GetType(String)),
                                                                                                                                 .Identificador = Util.AtribuirValorObj(dr("OID_CLIENTE_FACTURACION"), GetType(String))}

                                                                     End If

                                                                     objRemesa.Cuenta = New Clases.Cuenta With {.Cliente = New Clases.Cliente With {.Codigo = Util.AtribuirValorObj(dr("COD_CLIENTE_DESTINO"), GetType(String)), _
                                                                                                                                                   .Descripcion = Util.AtribuirValorObj(dr("DES_CLIENTE_DESTINO"), GetType(String)),
                                                                                                                                                    .Identificador = Util.AtribuirValorObj(dr("OID_CLIENTE_DESTINO"), GetType(String))}, _
                                                                                                               .SubCliente = If(Not String.IsNullOrEmpty(CodigoSubCliente),
                                                                                                                                New Clases.SubCliente With {.Codigo = CodigoSubCliente, _
                                                                                                                                                            .Descripcion = Util.AtribuirValorObj(dr("DES_SUBCLIENTE"), GetType(String)),
                                                                                                                                                            .Identificador = Util.AtribuirValorObj(dr("OID_SUBCLIENTE"), GetType(String))}, Nothing), _
                                                                                                               .PuntoServicio = If(Not String.IsNullOrEmpty(CodigoPuntoServicio),
                                                                                                                                New Clases.PuntoServicio With {.Codigo = CodigoPuntoServicio, _
                                                                                                                                                               .Descripcion = Util.AtribuirValorObj(dr("DES_PUNTO_SERVICIO"), GetType(String)),
                                                                                                                                                               .Identificador = Util.AtribuirValorObj(dr("OID_PUNTO_SERVICIO"), GetType(String))}, Nothing), _
                                                                                                               .Canal = If(Not String.IsNullOrEmpty(CodigoCanal), New Clases.Canal With {.Codigo = CodigoCanal}, Nothing), _
                                                                                                               .SubCanal = If(Not String.IsNullOrEmpty(CodigoSubCanal), New Clases.SubCanal With {.Codigo = CodigoSubCanal}, Nothing), _
                                                                                                               .Sector = If(Not String.IsNullOrEmpty(CodigoSector), New Clases.Sector With {.Codigo = CodigoSector}, Nothing)}

                                                                     If objRemesa.ClienteSaldo Is Nothing AndAlso CrearConfiguracionNivelSaldo Then

                                                                         If objRemesa.Cuenta.PuntoServicio IsNot Nothing Then

                                                                             Dim objPuntoServicio As New ObservableCollection(Of Clases.PuntoServicio)
                                                                             Dim objSubCliente As New ObservableCollection(Of Clases.SubCliente)

                                                                             objPuntoServicio.Add(New Clases.PuntoServicio With { _
                                                                                                  .Identificador = objRemesa.Cuenta.PuntoServicio.Identificador,
                                                                                                  .Codigo = objRemesa.Cuenta.PuntoServicio.Codigo,
                                                                                                  .Descripcion = objRemesa.Cuenta.PuntoServicio.Descripcion})

                                                                             objSubCliente.Add(New Clases.SubCliente With { _
                                                                                               .Identificador = objRemesa.Cuenta.SubCliente.Identificador,
                                                                                               .Codigo = objRemesa.Cuenta.SubCliente.Codigo,
                                                                                               .Descripcion = objRemesa.Cuenta.SubCliente.Descripcion,
                                                                                               .PuntosServicio = objPuntoServicio})

                                                                             objRemesa.ClienteSaldo = New Clases.Cliente With
                                                                                                      {.Identificador = objRemesa.Cuenta.Cliente.Identificador,
                                                                                                       .Codigo = objRemesa.Cuenta.Cliente.Codigo,
                                                                                                       .Descripcion = objRemesa.Cuenta.Cliente.Descripcion,
                                                                                                       .SubClientes = objSubCliente
                                                                                                      }
                                                                         End If

                                                                     End If

                                                                     objRemesa.Ruta = Util.AtribuirValorObj(dr("COD_RUTA_REMESA"), GetType(String))
                                                                     objRemesa.Parada = Util.AtribuirValorObj(dr("NEL_PARADA"), GetType(Int64?))
                                                                     objRemesa.CodigoEmpresaTransporte = Util.AtribuirValorObj(dr("COD_EMPRESA_TRANSPORTE"), GetType(String))
                                                                     objRemesa.CodigoCajaCentralizada = Util.AtribuirValorObj(dr("COD_CAJA_CENTRALIZADA"), GetType(String))
                                                                     objRemesa.DescripcionDireccionEntrega = Util.AtribuirValorObj(dr("DES_DIRECCION_ENTREGA"), GetType(String))
                                                                     objRemesa.DescripcionLocalidadEntrega = Util.AtribuirValorObj(dr("DES_LOCALIDAD_ENTREGA"), GetType(String))
                                                                     objRemesa.FechaHoraProceso = Util.AtribuirValorObj(dr("FYH_PROCESO"), GetType(DateTime))
                                                                     objRemesa.FechaServicio = Util.AtribuirValorObj(dr("FEC_SERVICIO"), GetType(Date))
                                                                     objRemesa.FechaHoraSalida = Util.AtribuirValorObj(dr("FYH_SALIDA"), GetType(DateTime))
                                                                     objRemesa.NumeroPedidoLegado = Util.AtribuirValorObj(dr("NEL_PEDIDO_LEGADO"), GetType(Integer))
                                                                     objRemesa.NumeroControleLegado = Util.AtribuirValorObj(dr("NEL_CONTROL_LEGADO"), GetType(String))
                                                                     objRemesa.DescripcionComentario = Util.AtribuirValorObj(dr("DES_COMENTARIO"), GetType(String))
                                                                     objRemesa.UsuarioCreacion = Util.AtribuirValorObj(dr("COD_USUARIO"), GetType(String))
                                                                     objRemesa.UsuarioModificacion = Util.AtribuirValorObj(dr("COD_USUARIO"), GetType(String))
                                                                     objRemesa.FechaHoraCreacion = Util.AtribuirValorObj(dr("FECHA_MODIFICACION_REMESA"), GetType(DateTime))
                                                                     objRemesa.FechaHoraModificacion = Util.AtribuirValorObj(dr("FECHA_MODIFICACION_REMESA"), GetType(DateTime))
                                                                     objRemesa.PuestoResponsable = Util.AtribuirValorObj(dr("COD_PUESTO"), GetType(String))
                                                                     objRemesa.CodigoReciboSalida = Util.AtribuirValorObj(dr("COD_RECIBO_REMESA"), GetType(String))
                                                                     objRemesa.ReciboTransporte = objRemesa.CodigoReciboSalida
                                                                     objRemesa.TrabajaPorBulto = Util.AtribuirValorObj(dr("BOL_TRABAJA_POR_BULTO"), GetType(Boolean))

                                                                     If (Not String.IsNullOrEmpty(DescripcionModeloCajero) OrElse Not String.IsNullOrEmpty(DescripcionModeloRed)) AndAlso ModalidadRecogida > 0 Then

                                                                         objRemesa.DatosATM = New Clases.ATM With {.DescripcionModeloCajero = DescripcionModeloCajero, _
                                                                      .DescripcionRed = DescripcionModeloRed, _
                                                                      .ModalidadRecogida = ModalidadRecogida, _
                                                                      .CodigoCajero = CodigoCajero}

                                                                     End If

                                                                     objRemesasRetorno.Add(objRemesa)

                                                                     objRemesa = (From objR In objRemesasRetorno Where objR.Identificador = IdRemesa).FirstOrDefault

                                                                 End If

                                                                 If Not String.IsNullOrEmpty(IdentificadorBulto) Then

                                                                     If objRemesa.Bultos Is Nothing Then objRemesa.Bultos = New ObservableCollection(Of Clases.Bulto)

                                                                     CodigoPrecinto = Util.AtribuirValorObj(dr("COD_PRECINTO_BULTO"), GetType(String))

                                                                     If Not String.IsNullOrEmpty(CodigoPrecinto) Then
                                                                         objPrecintos = New ObservableCollection(Of String)
                                                                         objPrecintos.Add(CodigoPrecinto)
                                                                     Else
                                                                         objPrecintos = Nothing
                                                                     End If

                                                                     Dim objTipoMercanciaBulto As ObservableCollection(Of Clases.TipoMercancia) = Nothing
                                                                     Dim objDivisasBulto As Tuple(Of String, Boolean, ObservableCollection(Of Clases.Divisa)) = Nothing

                                                                     If objTiposMercanciaBultos IsNot Nothing AndAlso objTiposMercanciaBultos.Count > 0 Then

                                                                         objTipoMercanciaBulto = (From objBulTM In objTiposMercanciaBultos
                                                       Where objBulTM.Key = IdentificadorBulto Select objBulTM.Value).FirstOrDefault

                                                                     End If

                                                                     Dim DivisasBulto As ObservableCollection(Of Clases.Divisa) = Nothing
                                                                     Dim EsModulo As Boolean = False

                                                                     If objDivisasBultos IsNot Nothing AndAlso objDivisasBultos.Count > 0 Then

                                                                         objDivisasBulto = (From objBulDiv In objDivisasBultos
                                                       Where objBulDiv.Item1 = IdentificadorBulto).FirstOrDefault

                                                                         If objDivisasBulto IsNot Nothing Then

                                                                             EsModulo = objDivisasBulto.Item2
                                                                             DivisasBulto = objDivisasBulto.Item3

                                                                         End If

                                                                     End If

                                                                     objRemesa.Bultos.Add(New Clases.Bulto With {.Identificador = IdentificadorBulto,
                                                                                                                 .Precintos = objPrecintos,
                                                                                                                 .Estado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoBulto)(Util.AtribuirValorObj(dr("COD_ESTADO_BULTO"), GetType(String))),
                                                                                                                 .Cuadrado = Util.AtribuirValorObj(dr("BOL_CUADRADO"), GetType(Boolean)),
                                                                                                                 .UsuarioCreacion = Util.AtribuirValorObj(dr("COD_USUARIO"), GetType(String)),
                                                                                                                 .UsuarioModificacion = Util.AtribuirValorObj(dr("COD_USUARIO"), GetType(String)),
                                                                                                                 .FechaHoraCreacion = Util.AtribuirValorObj(dr("FECHA_MODIFICACION_BULTO"), GetType(DateTime)),
                                                                                                                 .FechaHoraModificacion = Util.AtribuirValorObj(dr("FECHA_MODIFICACION_BULTO"), GetType(DateTime)),
                                                                                                                 .AceptaPicos = Util.AtribuirValorObj(dr("BOL_PICOS"), GetType(Boolean)),
                                                                                                                 .Preparado = Util.AtribuirValorObj(dr("BOL_PREPARADO"), GetType(Boolean)),
                                                                                                                 .PuestoResponsable = Util.AtribuirValorObj(dr("COD_PUESTO"), GetType(String)),
                                                                                                                 .TiposMercancia = objTipoMercanciaBulto,
                                                                                                                 .Divisas = DivisasBulto,
                                                                                                                 .EsModulo = EsModulo,
                                                                                                                 .IdentificadorMorfologiaComponente = Util.AtribuirValorObj(dr("OID_MORFOLOGIA_COMPONENTE"), GetType(String)),
                                                                                                                 .UsuarioBloqueo = Util.AtribuirValorObj(dr("COD_USUARIO_BLOQUEO"), GetType(String)),
                                                                                                                 .CodigoBolsa = Util.AtribuirValorObj(dr("COD_BOLSA"), GetType(String)),
                                                                                                                 .CodigoFormato = Util.AtribuirValorObj(dr("COD_FORMATO"), GetType(String)),
                                                                                                                 .TipoBulto = New Clases.TipoBulto With {
                                                                                                                 .Identificador = Util.AtribuirValorObj(dr("OID_TIPO_BULTO"), GetType(String)),
                                                                                                                 .Codigo = Util.AtribuirValorObj(dr("COD_TIPO_BULTO"), GetType(String)),
                                                                                                                 .Descripcion = Util.AtribuirValorObj(dr("DES_TIPO_BULTO"), GetType(String)),
                                                                                                                 .EsCajetin = Util.AtribuirValorObj(dr("BOL_CAJETIN"), GetType(Boolean))}
                                                                                                                })

                                                                 End If

                                                             Next

                                                             objRemesa.ListoTrabajo = objRemesa.ImporteRemesaOk

                                                             ' remove os bultos adicionados para realizar a validação de importe total (bulto x remesa)
                                                             If identificadorBultosTemporarios IsNot Nothing AndAlso identificadorBultosTemporarios.Count > 0 Then
                                                                 objRemesa.Bultos.RemoveAll(Function(x) identificadorBultosTemporarios.Contains(x.Identificador))
                                                             End If

                                                         End Sub)


                If objRemesasRetorno IsNot Nothing Then

                    Dim remOrd = objRemesasRetorno.OrderByDescending(Function(a) a.FechaHoraModificacion).ToArray()

                    objRemesas = New ObservableCollection(Of Clases.Remesa)

                    objRemesas = remOrd.ToObservableCollection
                End If

            End If


            Return objRemesas

        End Function

        ''' <summary>
        ''' Atualiza as denominações da remesa
        ''' </summary>
        ''' <param name="remesa"></param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarDivisasRemesas(ByRef remesa As Clases.Remesa)

            If remesa IsNot Nothing AndAlso remesa.Bultos IsNot Nothing AndAlso remesa.Bultos.Count > 0 Then

                ' Verifica se a remessa possui divisas
                If remesa.Divisas IsNot Nothing Then

                    ' Limpa os valores existentes nas denominações das divisas das remessas
                    remesa.Divisas.Foreach(Sub(div) If div.Denominaciones IsNot Nothing Then div.Denominaciones.Foreach(Sub(den) If den.ValorDenominacion IsNot Nothing Then den.ValorDenominacion.Clear()))

                End If

                ' Para cada malote existente, limpa as divisas dos malotes
                For Each bulto In remesa.Bultos.Where(Function(b) b.Divisas IsNot Nothing AndAlso b.Divisas.Count > 0 AndAlso Not b.EsModulo).ToObservableCollection

                    ' Se não existe divisas na remessa
                    If remesa.Divisas Is Nothing OrElse remesa.Divisas.Count = 0 Then

                        ' A remessa recebe a divida do malote
                        remesa.Divisas = bulto.Divisas.Clonar

                    Else

                        ' Para cada divisa existente no malote
                        For Each div In bulto.Divisas

                            ' Recupera a divisa da remessa de acordo com a divisa do malote
                            Dim divisaRemesa As Clases.Divisa = remesa.Divisas.FirstOrDefault(Function(f) f.CodigoISO = div.CodigoISO)

                            ' Se a remessa não possui a divisa
                            If divisaRemesa Is Nothing Then

                                ' Adiciona a divisa na remessa
                                remesa.Divisas.Add(div.Clonar)

                            Else

                                ' Verifica se existe denominações
                                If div.Denominaciones IsNot Nothing Then

                                    ' Verifica se a divisa da remessa possui denominações
                                    If divisaRemesa.Denominaciones Is Nothing Then

                                        ' Define as denominações da remessa
                                        divisaRemesa.Denominaciones = div.Denominaciones.Clonar

                                    End If

                                    ' Para cada denominação existente
                                    For Each den In div.Denominaciones

                                        ' Verifica se a denominação existe na remessa
                                        Dim denominacionDivRemesa As Clases.Denominacion = divisaRemesa.Denominaciones.FirstOrDefault(Function(f) f.Codigo = den.Codigo)

                                        ' Se a remessa não possui a denominação
                                        If denominacionDivRemesa Is Nothing Then

                                            ' Adiciona a denominação na divisa
                                            divisaRemesa.Denominaciones.Add(den.Clonar)

                                        Else

                                            ' Verifica se a denominação possui valor
                                            If den.ValorDenominacion IsNot Nothing Then

                                                ' Verifica se a denominação da divisa da remessa possui valores
                                                If denominacionDivRemesa.ValorDenominacion Is Nothing Then

                                                    ' Define os valores da denominação da divisa da remessa
                                                    denominacionDivRemesa.ValorDenominacion = den.ValorDenominacion.Clonar

                                                Else

                                                    ' Para cada valor da denominacao existente
                                                    For Each vd In den.ValorDenominacion

                                                        ' Busca a valor da denominacao
                                                        Dim valorDenominacion As Clases.ValorDenominacion = denominacionDivRemesa.ValorDenominacion.FirstOrDefault(Function(f) f.Calidad IsNot Nothing AndAlso vd.Calidad IsNot Nothing AndAlso f.Calidad.Codigo = vd.Calidad.Codigo)

                                                        ' Se nao encontrou o valor
                                                        If valorDenominacion Is Nothing Then
                                                            ' Adiciona o valor da denominacao
                                                            denominacionDivRemesa.ValorDenominacion.Add(vd.Clonar)
                                                        Else
                                                            ' Atualiza os valores da denomincao
                                                            valorDenominacion.Cantidad += vd.Cantidad
                                                            valorDenominacion.Importe += vd.Importe
                                                        End If

                                                    Next vd

                                                End If

                                            End If

                                        End If

                                    Next den

                                End If

                            End If

                        Next div

                    End If

                Next bulto

                Dim objDivisas As ObservableCollection(Of Clases.Divisa) = remesa.Divisas.Clonar

                If objDivisas Is Nothing Then objDivisas = New ObservableCollection(Of Clases.Divisa)

                ' remesa possui somente modulos 
                If remesa.Bultos.Where(Function(b) b.EsModulo).Count = remesa.Bultos.Count Then

                    For Each bulto In remesa.Bultos.FindAll(Function(b) b.Divisas IsNot Nothing AndAlso b.Divisas.Count > 0)

                        For Each divisa In bulto.Divisas
                            If Not objDivisas.Exists(Function(div) div.CodigoISO = divisa.CodigoISO) AndAlso _
                               (remesa.Divisas Is Nothing OrElse
                                (remesa.Divisas IsNot Nothing AndAlso Not remesa.Divisas.Exists(Function(div) div.CodigoISO = divisa.CodigoISO))) Then
                                objDivisas.Add(divisa.Clonar)
                            Else
                                Dim divisaTemp = objDivisas.FirstOrDefault(Function(div) div.CodigoISO = divisa.CodigoISO)

                                If divisaTemp IsNot Nothing AndAlso divisa.Denominaciones IsNot Nothing AndAlso divisa.Denominaciones.Count > 0 Then
                                    divisaTemp.Denominaciones = divisa.Denominaciones.Clonar
                                End If

                            End If
                        Next

                    Next

                    Prosegur.Genesis.Comon.Util.ZerarValoresDivisas(objDivisas)

                    remesa.Divisas = objDivisas
                Else

                    Dim divisasBultos As New ObservableCollection(Of Clases.Divisa)
                    If remesa.Bultos IsNot Nothing AndAlso remesa.Bultos.Count > 0 Then
                        For Each bulto In remesa.Bultos.Where(Function(b) b.EsModulo AndAlso b.Divisas IsNot Nothing AndAlso b.Divisas.Count > 0)
                            divisasBultos.AddRange(bulto.Divisas)
                        Next
                    End If

                    Prosegur.Genesis.Comon.Util.UnificaItemsDivisas(divisasBultos)
                    Prosegur.Genesis.Comon.Util.ZerarValoresDivisas(divisasBultos)

                    If remesa.Divisas IsNot Nothing AndAlso remesa.Divisas.Count > 0 Then
                        For Each divisaBulto In divisasBultos
                            If Not remesa.Divisas.Exists(Function(e) e.Identificador = divisaBulto.Identificador) Then
                                remesa.Divisas.Add(divisaBulto)
                            End If
                        Next
                    Else
                        remesa.Divisas = divisasBultos
                    End If

                End If

            End If

        End Sub

        Private Shared Function ValidarImporteRemesa(IdentificadorRemesa As String) As Boolean

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_ValidarImporte)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, IdentificadorRemesa))

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALIDAS, cmd)
        End Function

        Private Shared Function RemesasPadreYHija(dt As DataTable, CodigoSubCanalATM As String, CrearConfiguracionNivelSaldo As Boolean) As ObservableCollection(Of Clases.Remesa)

            Dim objRemesas As ObservableCollection(Of Clases.Remesa) = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objRemesas = New ObservableCollection(Of Clases.Remesa)

                'Ordena as remesas primeiro pelas remesas pais, e depois as remesas filhas
                For Each dr In dt.Select(String.Empty, "OID_REMESA_PADRE ASC")

                    Dim objDivisas As ObservableCollection(Of Clases.Divisa) = AccesoDatos.Genesis.Divisas.ObtenerDivisas(, , , , True, False, True, True, )

                    Dim CodigoClienteSaldo As String = Util.AtribuirValorObj(dr("COD_CLIENTE_SALDO"), GetType(String))
                    Dim CodigoClienteFacturacion As String = Util.AtribuirValorObj(dr("COD_CLIENTE_FACTURACION"), GetType(String))
                    Dim CodigoSubCliente As String = Util.AtribuirValorObj(dr("COD_SUBCLIENTE"), GetType(String))
                    Dim CodigoPuntoServicio As String = Util.AtribuirValorObj(dr("COD_PUNTO_SERVICIO"), GetType(String))
                    Dim CodigoCanal As String = Util.AtribuirValorObj(dr("COD_CANAL"), GetType(String))
                    Dim CodigoSubCanal As String = Util.AtribuirValorObj(dr("COD_SUBCANAL"), GetType(String))
                    Dim CodigoSector As String = Util.AtribuirValorObj(dr("COD_SECTOR"), GetType(String))
                    Dim IdentificadorBulto As String = Util.AtribuirValorObj(dr("OID_BULTO"), GetType(String))
                    Dim IdentificadorRemesa As String = Util.AtribuirValorObj(dr("OID_REMESA"), GetType(String))

                    Dim Remesa As Clases.Remesa = objRemesas.Where(Function(r) r.Identificador = IdentificadorRemesa).FirstOrDefault
                    If Remesa Is Nothing Then
                        Remesa = New Clases.Remesa

                        Remesa.Identificador = IdentificadorRemesa
                        Remesa.IdentificadorRemesaPadre = Util.AtribuirValorObj(dr("OID_REMESA_PADRE"), GetType(String))
                        Remesa.IdentificadorExterno = Util.AtribuirValorObj(dr("OID_REMESA_LEGADO"), GetType(String))
                        Remesa.IdentificadorOT = Util.AtribuirValorObj(dr("OID_OT"), GetType(String))
                        Remesa.CodigoServicioContratado = Util.AtribuirValorObj(dr("COD_SERVICIO"), GetType(String))
                        Remesa.CodigoSecuencia = Util.AtribuirValorObj(dr("COD_SECUENCIA"), GetType(String))
                        Remesa.CodigoDelegacion = Util.AtribuirValorObj(dr("COD_DELEGACION"), GetType(String))
                        Remesa.Estado = Extenciones.RecuperarEnum(Of EstadoRemesa)(Util.AtribuirValorObj(dr("COD_ESTADO_REMESA"), GetType(String)))
                        Remesa.FechaHoraPreparacion = Util.AtribuirValorObj(dr("FYH_INICIO"), GetType(DateTime))
                        Remesa.FechaHoraInicioArmado = Util.AtribuirValorObj(dr("FYH_INICIO_ARMADO"), GetType(DateTime))
                        Remesa.FechaHoraFinArmado = Util.AtribuirValorObj(dr("FYH_FIN_ARMADO"), GetType(DateTime))
                        Remesa.TiposMercancia = TipoMercancia.RecuperarTiposMercanciaRemesa(Remesa.Identificador)
                        Remesa.Divisas = MesclarDivisas(objDivisas, Divisa.RecuperarDivisasRemesa(Remesa.Identificador))
                        Remesa.Objetos = Objeto.RecuperarObjetos(Remesa.Identificador)
                        Remesa.UsuarioBloqueo = Util.AtribuirValorObj(dr("COD_USUARIO_BLOQUEO"), GetType(String))
                        Remesa.DescripcionContacto1 = Util.AtribuirValorObj(dr("DES_CONTACTO1"), GetType(String))
                        Remesa.DescripcionContacto2 = Util.AtribuirValorObj(dr("DES_CONTACTO2"), GetType(String))
                        Remesa.DescripcionContacto3 = Util.AtribuirValorObj(dr("DES_CONTACTO3"), GetType(String))
                        Remesa.DescripcionContacto4 = Util.AtribuirValorObj(dr("DES_CONTACTO4"), GetType(String))
                        Remesa.EsRemesaATM = Util.AtribuirValorObj(dr("BOL_ATM"), GetType(Boolean))

                        If Not String.IsNullOrEmpty(CodigoClienteFacturacion) Then
                            Remesa.ClienteFacturacion = New Clases.Cliente With {.Codigo = CodigoClienteFacturacion, _
                                                                                 .Descripcion = Util.AtribuirValorObj(dr("DES_CLIENTE_FACTURACION"), GetType(String))
                                                                                }
                        End If

                        'Se possui cliente saldo na tabela de remessa
                        If Not String.IsNullOrEmpty(CodigoClienteSaldo) Then

                            Remesa.ClienteSaldo = New Clases.Cliente With {.Codigo = CodigoClienteSaldo, _
                                                                           .Descripcion = Util.AtribuirValorObj(dr("DES_CLIENTE_SALDO"), GetType(String))}
                        Else

                            Dim codSubCanal As String = CodigoSubCanal

                            ' se não possuir subCanal
                            If String.IsNullOrEmpty(codSubCanal) Then
                                ' se remessa for de ATM utiliza subcanal configurado no parametro
                                ' senão utiliza o codigo do canal como subCanal
                                If Remesa.EsRemesaATM Then
                                    codSubCanal = CodigoSubCanalATM
                                Else
                                    codSubCanal = CodigoCanal
                                End If
                            End If


                            Remesa.ClienteSaldo = Genesis.Cliente.RecuperarClienteTotalizadorSaldo(Util.AtribuirValorObj(dr("COD_CLIENTE_DESTINO"), GetType(String)), _
                                                                                                   CodigoSubCliente, _
                                                                                                   CodigoPuntoServicio,
                                                                                                   codSubCanal)
                        End If

                        Remesa.Cuenta = New Clases.Cuenta With {.Cliente = New Clases.Cliente With {.Codigo = Util.AtribuirValorObj(dr("COD_CLIENTE_DESTINO"), GetType(String)), _
                                                                                                    .Descripcion = Util.AtribuirValorObj(dr("DES_CLIENTE_DESTINO"), GetType(String))}, _
                                                                   .SubCliente = If(Not String.IsNullOrEmpty(CodigoSubCliente),
                                                                                    New Clases.SubCliente With {.Codigo = CodigoSubCliente, _
                                                                                                                .Descripcion = Util.AtribuirValorObj(dr("DES_SUBCLIENTE"), GetType(String))}, Nothing), _
                                                                   .PuntoServicio = If(Not String.IsNullOrEmpty(CodigoPuntoServicio),
                                                                                    New Clases.PuntoServicio With {.Codigo = CodigoPuntoServicio, _
                                                                                                                   .Descripcion = Util.AtribuirValorObj(dr("DES_PUNTO_SERVICIO"), GetType(String))}, Nothing), _
                                                                   .Canal = If(Not String.IsNullOrEmpty(CodigoCanal), New Clases.Canal With {.Codigo = CodigoCanal}, Nothing), _
                                                                   .SubCanal = If(Not String.IsNullOrEmpty(CodigoCanal), New Clases.SubCanal With {.Codigo = CodigoCanal}, Nothing), _
                                                                   .Sector = If(Not String.IsNullOrEmpty(CodigoSector), New Clases.Sector With {.Codigo = CodigoSector}, Nothing)}

                        If Remesa.ClienteSaldo Is Nothing AndAlso CrearConfiguracionNivelSaldo Then
                            If Remesa.Cuenta.PuntoServicio IsNot Nothing Then
                                Remesa.ClienteSaldo = New Clases.Cliente With
                                                         {.Identificador = Remesa.Cuenta.PuntoServicio.Identificador,
                                                          .Codigo = Remesa.Cuenta.PuntoServicio.Codigo,
                                                          .Descripcion = Remesa.Cuenta.PuntoServicio.Descripcion
                                                         }
                            End If
                        End If

                        Remesa.Ruta = Util.AtribuirValorObj(dr("COD_RUTA_REMESA"), GetType(String))
                        Remesa.Parada = Util.AtribuirValorObj(dr("NEL_PARADA"), GetType(Int64?))
                        Remesa.CodigoEmpresaTransporte = Util.AtribuirValorObj(dr("COD_EMPRESA_TRANSPORTE"), GetType(String))
                        Remesa.CodigoCajaCentralizada = Util.AtribuirValorObj(dr("COD_CAJA_CENTRALIZADA"), GetType(String))
                        Remesa.DescripcionDireccionEntrega = Util.AtribuirValorObj(dr("DES_DIRECCION_ENTREGA"), GetType(String))
                        Remesa.DescripcionLocalidadEntrega = Util.AtribuirValorObj(dr("DES_LOCALIDAD_ENTREGA"), GetType(String))
                        Remesa.FechaHoraProceso = Util.AtribuirValorObj(dr("FYH_PROCESO"), GetType(DateTime))
                        Remesa.FechaServicio = Util.AtribuirValorObj(dr("FEC_SERVICIO"), GetType(Date))
                        Remesa.FechaHoraSalida = Util.AtribuirValorObj(dr("FYH_SALIDA"), GetType(DateTime))
                        Remesa.NumeroPedidoLegado = Util.AtribuirValorObj(dr("NEL_PEDIDO_LEGADO"), GetType(Integer))
                        Remesa.NumeroControleLegado = Util.AtribuirValorObj(dr("NEL_CONTROL_LEGADO"), GetType(String))
                        Remesa.DescripcionComentario = Util.AtribuirValorObj(dr("DES_COMENTARIO"), GetType(String))
                        Remesa.UsuarioCreacion = Util.AtribuirValorObj(dr("COD_USUARIO"), GetType(String))
                        Remesa.UsuarioModificacion = Util.AtribuirValorObj(dr("COD_USUARIO"), GetType(String))
                        Remesa.FechaHoraCreacion = Util.AtribuirValorObj(dr("FECHA_MODIFICACION_REMESA"), GetType(DateTime))
                        Remesa.FechaHoraModificacion = Util.AtribuirValorObj(dr("FECHA_MODIFICACION_REMESA"), GetType(DateTime))
                        Remesa.PuestoResponsable = Util.AtribuirValorObj(dr("COD_PUESTO"), GetType(String))
                        Remesa.CodigoReciboSalida = Util.AtribuirValorObj(dr("COD_RECIBO_REMESA"), GetType(String))
                        Remesa.TrabajaPorBulto = Util.AtribuirValorObj(dr("BOL_TRABAJA_POR_BULTO"), GetType(Boolean))

                        'se a remesa ainda não existe na lista, então insere na lista
                        objRemesas.Add(Remesa)
                    End If

                    If Not String.IsNullOrEmpty(IdentificadorBulto) Then

                        If Remesa.Bultos Is Nothing Then Remesa.Bultos = New ObservableCollection(Of Clases.Bulto)

                        Dim CodigoPrecinto As String = Util.AtribuirValorObj(dr("COD_PRECINTO_BULTO"), GetType(String))

                        Remesa.Bultos.Add(New Clases.Bulto With {.Identificador = IdentificadorBulto, _
                                                                 .Precintos = New ObservableCollection(Of String) From {CodigoPrecinto}, _
                                                                 .CodigoBolsa = Util.AtribuirValorObj(dr("COD_BOLSA"), GetType(String)), _
                                                                 .Estado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoBulto)(Util.AtribuirValorObj(dr("COD_ESTADO_BULTO"), GetType(String))), _
                                                                 .Cuadrado = Util.AtribuirValorObj(dr("BOL_CUADRADO"), GetType(Boolean)), _
                                                                 .UsuarioCreacion = Util.AtribuirValorObj(dr("COD_USUARIO"), GetType(String)), _
                                                                 .UsuarioModificacion = Util.AtribuirValorObj(dr("COD_USUARIO"), GetType(String)), _
                                                                 .FechaHoraCreacion = Util.AtribuirValorObj(dr("FECHA_MODIFICACION_BULTO"), GetType(DateTime)), _
                                                                 .FechaHoraModificacion = Util.AtribuirValorObj(dr("FECHA_MODIFICACION_BULTO"), GetType(DateTime)), _
                                                                 .AceptaPicos = Util.AtribuirValorObj(dr("BOL_PICOS"), GetType(Boolean)), _
                                                                 .Preparado = Util.AtribuirValorObj(dr("BOL_PREPARADO"), GetType(Boolean)), _
                                                                 .PuestoResponsable = Util.AtribuirValorObj(dr("COD_PUESTO"), GetType(String)), _
                                                                 .TiposMercancia = TipoMercancia.RecuperarTiposMercanciaBulto(.Identificador), _
                                                                 .Divisas = MesclarDivisas(objDivisas, Divisa.RecuperarDivisasBultoOModulo(.Identificador, False)),
                                                                 .IdentificadorMorfologiaComponente = Util.AtribuirValorObj(dr("OID_MORFOLOGIA_COMPONENTE"), GetType(String)), _
                                                                 .TipoBulto = TipoBulto.ObtenerTipoBulto(Util.AtribuirValorObj(dr("OID_TIPO_BULTO"), GetType(String))), _
                                                                 .CodigoFormato = Util.AtribuirValorObj(dr("COD_FORMATO"), GetType(String)), _
                                                                 .UsuarioBloqueo = Util.AtribuirValorObj(dr("COD_USUARIO_BLOQUEO"), GetType(String))})

                    End If

                    'verfica se é uma remesa filha, se for recupera atualiza a remesa pai
                    If Not String.IsNullOrEmpty(Remesa.IdentificadorRemesaPadre) Then
                        Remesa.RemesaOrigen = objRemesas.Where(Function(r) r.Identificador = Remesa.IdentificadorRemesaPadre).FirstOrDefault
                    End If
                Next

                ' Atualiza as divisas da Remessa
                Prosegur.Genesis.Comon.Util.ActualizarDivisasRemesas(objRemesas)

            End If

            Return objRemesas

        End Function

        ''' <summary>
        ''' Mescla o objeto de divisa que esta completo com o objeto que contem os valores.
        ''' </summary>
        ''' <param name="objDivisasCompletas"></param>
        ''' <param name="objDivisasBanco"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function MesclarDivisas(objDivisasCompletas As ObservableCollection(Of Clases.Divisa), _
                                               objDivisasBanco As ObservableCollection(Of Clases.Divisa)) As ObservableCollection(Of Clases.Divisa)

            Dim objDivisasRetorno As ObservableCollection(Of Clases.Divisa) = Nothing
            Dim objDivisa As Clases.Divisa = Nothing
            Dim objDenominacion As Clases.Denominacion = Nothing

            If objDivisasBanco IsNot Nothing AndAlso objDivisasBanco.Count > 0 AndAlso _
               objDivisasCompletas IsNot Nothing AndAlso objDivisasCompletas.Count > 0 Then

                objDivisasRetorno = New ObservableCollection(Of Clases.Divisa)

                For Each objDiv In objDivisasBanco

                    objDivisa = (From objDivComp In objDivisasCompletas Where objDivComp.CodigoISO = objDiv.CodigoISO).FirstOrDefault.Clonar()

                    If objDivisa IsNot Nothing Then

                        If objDiv.Denominaciones IsNot Nothing AndAlso objDiv.Denominaciones.Count > 0 AndAlso _
                           objDivisa.Denominaciones IsNot Nothing AndAlso objDivisa.Denominaciones.Count > 0 Then

                            objDivisa.Denominaciones.Foreach(Sub(dn) If dn.ValorDenominacion IsNot Nothing Then dn.ValorDenominacion.Clear())

                            For Each objDen In objDiv.Denominaciones

                                objDenominacion = objDivisa.Denominaciones.FindAll(Function(dc) dc.Codigo = objDen.Codigo).FirstOrDefault

                                If objDenominacion IsNot Nothing Then
                                    objDenominacion.ValorDenominacion = objDen.ValorDenominacion
                                End If

                            Next

                        End If

                        objDivisa.ValoresTotalesEfectivo = objDiv.ValoresTotalesEfectivo
                        objDivisa.ValoresTotalesTipoMedioPago = objDiv.ValoresTotalesTipoMedioPago
                        objDivisa.MediosPago = Nothing

                        objDivisasRetorno.Add(objDivisa)

                    End If

                Next

            End If

            Return objDivisasRetorno
        End Function

        ''' <summary>
        ''' Método que insere um efectivo da remessa no banco de dados
        ''' </summary>
        ''' <param name="OidRemesa"></param>
        ''' <param name="OidDivisa"></param>
        ''' <param name="OidDenominacion"></param>
        ''' <param name="NelCantidad"></param>
        ''' <param name="Importe"></param>
        ''' <param name="CodUsuario"></param>
        ''' <param name="FechaActualizacion"></param>
        ''' <history>[maoliveira]	04/06/2014	Creado</history>
        ''' <remarks></remarks>
        Public Shared Sub InsertarEfectivoRemesa(OidRemesa As String, _
                                                OidDivisa As String, _
                                                OidDenominacion As String, _
                                                NelCantidad As Int64, _
                                                Importe As Nullable(Of Decimal), _
                                                CodUsuario As String, _
                                                FechaActualizacion As DateTime)

            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_InsertarEfectivoRemesa)
                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_EFECTIVO_LEGADO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString()))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, OidRemesa))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_DIVISA", ProsegurDbType.Objeto_Id, OidDivisa))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_DENOMINACION", ProsegurDbType.Objeto_Id, OidDenominacion))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "NEL_CANTIDAD", ProsegurDbType.Inteiro_Longo, NelCantidad))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "NEL_IMPORTE_EFECTIVO", ProsegurDbType.Inteiro_Longo, Importe))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodUsuario))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, FechaActualizacion))
            End With

            'Apaga a remesa do banco
            DbHelper.AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        ''' <summary>
        ''' Método que deleta todos os efectivos de uma remessa
        ''' </summary>
        ''' <param name="oidRemesa"></param>
        ''' <history>[cbomtempo]	05/06/2014	Creado</history>
        ''' <remarks></remarks>
        Public Shared Sub BorrarEfectivosRemesa(oidRemesa As String)

            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_BorrarEfectivosRemesa)
                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, oidRemesa))
            End With

            'Apaga a remesa do banco
            DbHelper.AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        ''' <summary>
        ''' Monta a clausula para buscar as remesas
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <param name="comando"></param>
        ''' <remarks></remarks>
        Private Shared Sub MontaClausulaRemesas(objPeticion As ContractoServicio.NuevoSalidas.Remesa.RecuperarDatosGeneralesRemesa.Peticion,
                                                ByRef comando As IDbCommand)

            Dim sbComando As New System.Text.StringBuilder
            Dim sbComandoBulto As System.Text.StringBuilder = Nothing

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "AGRUPA_BULTOS", ProsegurDbType.Logico, objPeticion.AgruparBultos))

            If (objPeticion.IdentificadoresRemesas IsNot Nothing AndAlso objPeticion.IdentificadoresRemesas.Count > 0) OrElse Not String.IsNullOrEmpty(objPeticion.IdentificadorRemesa) OrElse Not String.IsNullOrEmpty(objPeticion.IdentificadorRemesaLegado) _
               OrElse Not String.IsNullOrEmpty(objPeticion.IdentificadorContenedor) OrElse Not String.IsNullOrEmpty(objPeticion.IdentificadorBulto) Then

                'Si OidsRemesas ha sido informado
                If objPeticion.IdentificadoresRemesas IsNot Nothing Then
                    sbComando.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, objPeticion.IdentificadoresRemesas, "OID_REMESA", comando,
                                                       If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "), "REM"))
                End If

                'Si OidRemesas ha sido informado
                If Not String.IsNullOrEmpty(objPeticion.IdentificadorRemesa) Then
                    sbComando.Append(If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                    sbComando.Append(" REM.OID_REMESA = []OID_REMESA ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, objPeticion.IdentificadorRemesa))
                End If

                'Si Codigo Precinto ha sido informado
                If Not String.IsNullOrEmpty(objPeticion.IdentificadorBulto) Then
                    sbComando.Append(If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                    sbComando.Append(" B.OID_BULTO = []OID_BULTO ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_BULTO", ProsegurDbType.Objeto_Id, objPeticion.IdentificadorBulto))
                End If

                'Si Indentificador do Contenedor fue Informado
                If Not String.IsNullOrEmpty(objPeticion.IdentificadorContenedor) Then
                    sbComando.Append(If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                    sbComando.Append(" (B.OID_BULTO = []OID_CONTENEDOR OR REM.OID_REMESA = []OID_CONTENEDOR) ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_CONTENEDOR", ProsegurDbType.Objeto_Id, objPeticion.IdentificadorContenedor))
                End If

                'Si CodCliente OidRemesaLegado ha sido informado
                If Not String.IsNullOrEmpty(objPeticion.IdentificadorRemesaLegado) Then
                    sbComando.Append(If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                    sbComando.Append(" REM.OID_REMESA_LEGADO = []OID_REMESA_LEGADO ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA_LEGADO", ProsegurDbType.Objeto_Id, objPeticion.IdentificadorRemesaLegado))
                End If

            Else


                'Monta a clausula in para recuperar os estados
                If objPeticion.CodigosEstados IsNot Nothing AndAlso objPeticion.CodigosEstados.Count > 0 Then

                    If objPeticion.EsEstadoBulto Then
                        sbComando.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, objPeticion.CodigosEstados, "COD_ESTADO_BULTO", comando,
                                                       If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "), "B"))
                    Else
                        sbComando.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, objPeticion.CodigosEstados, "COD_ESTADO_REMESA", comando,
                                                               If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "), "REM"))
                    End If

                End If

                'Monta a clausula in para recuperar os emissores
                If objPeticion.CodigosEmisores IsNot Nothing AndAlso objPeticion.CodigosEmisores.Count > 0 Then
                    sbComando.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, objPeticion.CodigosEmisores, "COD_EMPRESA_TRANSPORTE", comando,
                                                           If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "), "REM"))

                End If

                'Monta a clausula in para recuperar os tipos de mercancia
                If objPeticion.CodigosTiposMercancia IsNot Nothing AndAlso objPeticion.CodigosTiposMercancia.Count > 0 Then
                    sbComando.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, objPeticion.CodigosTiposMercancia, "COD_TIPO_MERCANCIA", comando,
                                         If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "), "TM"))
                End If

                'Si fecha desde ha sido informada
                If objPeticion.FechaServicioDesde IsNot Nothing Then
                    sbComando.Append(If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                    sbComando.Append(" REM.FEC_SERVICIO >= []FEC_SERVICIO_DESDE ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FEC_SERVICIO_DESDE", ProsegurDbType.Data, objPeticion.FechaServicioDesde))
                End If
                'Si fecha hasta ha sido informada
                If objPeticion.FechaServicioHasta IsNot Nothing Then
                    sbComando.Append(If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                    sbComando.Append(" REM.FEC_SERVICIO <= []FEC_SERVICIO_HASTA ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FEC_SERVICIO_HASTA", ProsegurDbType.Data, objPeticion.FechaServicioHasta))
                End If

                'Si CodigoClienteFacturacion ha sido informado
                If Not String.IsNullOrEmpty(objPeticion.CodigoClienteFacturacion) Then
                    sbComando.Append(If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                    sbComando.Append(" REM.COD_CLIENTE_FACTURACION = []COD_CLIENTE_FACTURACION ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_CLIENTE_FACTURACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoClienteFacturacion))
                End If

                'Si CodigoClienteFacturacion ha sido informado
                If Not String.IsNullOrEmpty(objPeticion.CodigoCanal) Then
                    sbComando.Append(If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                    sbComando.Append(" REM.COD_CANAL = []COD_CANAL ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_CANAL", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoCanal))
                End If

                'Si Codigo Precinto ha sido informado
                If Not String.IsNullOrEmpty(objPeticion.CodigoPrecintoBulto) Then
                    sbComando.Append(If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                    sbComando.Append(" B.COD_PRECINTO_BULTO = []COD_PRECINTO_BULTO ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_PRECINTO_BULTO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoPrecintoBulto))
                End If

                'Si Codigo Precinto ha sido informado
                If objPeticion.EsPreparado IsNot Nothing Then
                    sbComando.Append(If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                    sbComando.Append(" B.BOL_PREPARADO = []BOL_PREPARADO ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "BOL_PREPARADO", ProsegurDbType.Logico, objPeticion.EsPreparado))
                End If

                'Não recuperar remesas com estado igual a dividdia
                sbComando.Append(If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                sbComando.Append(" REM.COD_ESTADO_REMESA <> []COD_ESTADO_REMESA_DIVIDIDA")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_ESTADO_REMESA_DIVIDIDA", ProsegurDbType.Identificador_Alfanumerico, Enumeradores.EstadoRemesa.Dividido.RecuperarValor))

                If objPeticion.EsPreparado.HasValue Then
                    sbComando.Append(If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                    sbComando.Append(" B.BOL_PREPARADO = []BOL_PREPARADO ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "BOL_PREPARADO", ProsegurDbType.Logico, objPeticion.EsPreparado))
                End If

                Select Case objPeticion.BuscarRemesasConRuta

                    Case RemesasConRuta.ConRuta

                        'Si CodCliente CodRutaRemesa ha sido informado
                        If Not String.IsNullOrEmpty(objPeticion.CodigoRutaRemesa) Then

                            sbComando.Append(If(sbComando.ToString().Contains("WHERE"), " AND (", " WHERE ("))
                            Dim blocosRotas As String() = objPeticion.CodigoRutaRemesa.Replace("'", "").Split(",")
                            Dim rotas As String()


                            For Each bloco In blocosRotas

                                rotas = bloco.Split("-")

                                If rotas.Count > 1 Then
                                    sbComando.AppendLine(" REM.COD_RUTA_REMESA BETWEEN '" & rotas(0).Trim() & "' AND '" & rotas(1).Trim() & "' OR")
                                Else
                                    sbComando.AppendLine(" REM.COD_RUTA_REMESA = '" & rotas(0).Trim() & "' OR")
                                End If

                            Next

                            sbComando.Remove((sbComando.Length - 4), 2)
                            sbComando.Append(") ")

                        End If

                    Case RemesasConRuta.SinRuta

                        sbComando.AppendLine(If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                        sbComando.AppendLine(" (REM.COD_RUTA_REMESA IS NULL OR REM.COD_RUTA_REMESA = '') ")

                End Select

                'Si BolObtenerRemesaConRecibo ha sido informado
                If objPeticion.ObtenerRemesaConRecibo Then

                    If Not String.IsNullOrEmpty(objPeticion.CodigoReciboRemesa) Then
                        sbComando.Append(IIf(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                        sbComando.Append(" REM.COD_RECIBO_REMESA = '" & objPeticion.CodigoReciboRemesa.Trim() & "'")
                    Else
                        sbComando.Append(IIf(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                        sbComando.Append(" REM.COD_RECIBO_REMESA IS NOT NULL ")
                    End If

                End If

                If objPeticion.ObtenerRemesaSinRecibo Then
                    sbComando.Append(IIf(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                    sbComando.Append(" REM.COD_RECIBO_REMESA IS NULL ")
                End If

                'Si tipo de bolsa (bulto) ha sido informado
                If objPeticion.CodigosTiposBulto IsNot Nothing AndAlso objPeticion.CodigosTiposBulto.Count > 0 Then

                    sbComando.Append(If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                    sbComando.Append(" EXISTS (SELECT 1 ")
                    sbComando.Append(" FROM GEPR_TBULTO BUL ")
                    sbComando.Append(" INNER JOIN GEPR_TTIPO_BULTO TIP ON BUL.OID_TIPO_BULTO = TIP.OID_TIPO_BULTO ")
                    sbComando.Append(" WHERE BUL.OID_REMESA = REM.OID_REMESA ")
                    sbComando.Append(Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, objPeticion.CodigosTiposBulto, "COD_TIPO_BULTO", comando, " AND ", "TIP"))
                    sbComando.Append(")")
                End If

                If objPeticion.FechaHoraSalidaDesde IsNot Nothing Then
                    sbComando.Append(If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                    sbComando.Append(" REM.FYH_SALIDA >= []FYH_SALIDA_DESDE ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_SALIDA_DESDE", ProsegurDbType.Data_Hora, objPeticion.FechaHoraSalidaDesde))
                End If

                If objPeticion.FechaHoraSalidaHasta IsNot Nothing Then
                    sbComando.Append(If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                    sbComando.Append(" REM.FYH_SALIDA <= []FYH_SALIDA_HASTA ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_SALIDA_HASTA", ProsegurDbType.Data_Hora, objPeticion.FechaHoraSalidaHasta))
                End If

                If Not String.IsNullOrEmpty(objPeticion.CodigoClienteDestino) Then
                    sbComando.Append(IIf(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                    sbComando.Append(" REM.COD_CLIENTE_DESTINO = []COD_CLIENTE_DESTINO ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_CLIENTE_DESTINO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoClienteDestino))
                End If

                If Not String.IsNullOrEmpty(objPeticion.CodigoSubClienteDestino) Then
                    sbComando.Append(IIf(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                    sbComando.Append(" REM.COD_SUBCLIENTE = []COD_SUBCLIENTE ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoSubClienteDestino))
                End If

                If Not String.IsNullOrEmpty(objPeticion.CodigoPuntoServicioDestino) Then
                    sbComando.Append(IIf(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                    sbComando.Append(" REM.COD_PUNTO_SERVICIO = []COD_PUNTO_SERVICIO ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_PUNTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoPuntoServicioDestino))
                End If

                If Not String.IsNullOrEmpty(objPeticion.CodigoATM) Then
                    sbComando.Append(IIf(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                    sbComando.Append(" REM.COD_ATM = []COD_ATM ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_ATM", ProsegurDbType.Descricao_Curta, objPeticion.CodigoATM))
                End If

                If objPeticion.CodigosPuestos IsNot Nothing AndAlso objPeticion.CodigosPuestos.Count > 0 Then

                    sbComando.AppendLine(If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))

                    sbComando.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, objPeticion.CodigosPuestos, "COD_PUESTO", comando,
                                         "(", "PST"))

                    'Se foi selecionado o estado pendente tem que recuperar as remesas que não tem posto asociado.
                    If objPeticion.CodigosEstados IsNot Nothing AndAlso _
                       objPeticion.CodigosEstados.FindAll(Function(e) e = Extenciones.RecuperarValor(Enumeradores.EstadoRemesa.Pendiente) OrElse
                                                                      e = Extenciones.RecuperarValor(Enumeradores.EstadoRemesa.Anulado) OrElse
                                                                      e = Extenciones.RecuperarValor(Enumeradores.EstadoRemesa.Modificada)).Count > 0 Then
                        sbComando.AppendLine(" OR LT.OID_PUESTO IS NULL ")
                    End If

                    sbComando.AppendLine(")")

                End If

                If Not String.IsNullOrEmpty(objPeticion.CodigoDelegacion) Then
                    sbComando.Append(If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                    sbComando.Append(" REM.COD_DELEGACION = []COD_DELEGACION ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoDelegacion))
                End If

                If Not String.IsNullOrEmpty(objPeticion.NumeroControleLegado) Then
                    sbComando.Append(If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
                    sbComando.Append(" REM.NEL_CONTROL_LEGADO = []NEL_CONTROL_LEGADO ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "NEL_CONTROL_LEGADO", ProsegurDbType.Descricao_Longa, objPeticion.NumeroControleLegado))
                End If

                If objPeticion.CodigosSectores IsNot Nothing AndAlso objPeticion.CodigosSectores.Count > 0 Then

                    sbComando.AppendLine(If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))

                    sbComando.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, objPeticion.CodigosSectores, "COD_SECTOR", comando,
                                         "(", "REM"))

                    sbComando.AppendLine(")")

                End If
            End If

            sbComandoBulto = New Text.StringBuilder(sbComando.ToString)

            sbComando.Append(If(sbComando.ToString().Contains("WHERE"), " AND ", " WHERE "))
            sbComando.Append(" NOT EXISTS (SELECT 1 FROM GEPR_TBULTO WHERE OID_REMESA = REM.OID_REMESA) ")

            If objPeticion.BuscarRemesasConBultos = RemesasConBultos.ConBultos Then
                comando.CommandText = String.Format(comando.CommandText, " WHERE 1=0 ", sbComandoBulto)
            ElseIf objPeticion.BuscarRemesasConBultos = RemesasConBultos.SinBultos Then
                comando.CommandText = String.Format(comando.CommandText, sbComando, " WHERE 1=0 ")
            Else
                comando.CommandText = String.Format(comando.CommandText, sbComando, sbComandoBulto)
            End If
        End Sub

        ''' <summary>
        ''' Validar serviços remessa.
        ''' </summary>
        ''' <param name="codigoDelegacion">Código da Delegação</param>
        ''' <param name="OidsRemesas">Identificadores da Remessa</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ValidarServicios(codigoDelegacion As String, OidsRemesas As List(Of String)) As ObservableCollection(Of Clases.Remesa)

            Dim objRemesas As New ObservableCollection(Of Clases.Remesa)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_ValidarServicioRemesa)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion))

            Dim sbComando As New System.Text.StringBuilder
            sbComando.AppendLine(Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, OidsRemesas, "OID_REMESA", cmd, " AND ", "R")))

            cmd.CommandText &= sbComando.ToString

            ' Para cada linha retornada
            For Each dr As DataRow In AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd).Rows

                objRemesas.Add(New Clases.Remesa With
                               {
                                   .Identificador = Util.AtribuirValorObj(dr("OID_REMESA"), GetType(String)),
                                   .Estado = Extenciones.RecuperarEnum(Of EstadoRemesa)(Util.AtribuirValorObj(dr("COD_ESTADO_REMESA"), GetType(String))),
                                   .FechaHoraModificacion = Util.AtribuirValorObj(dr("FYH_ACTUALIZACION"), GetType(DateTime)),
                                   .UsuarioBloqueo = Util.AtribuirValorObj(dr("COD_USUARIO_BLOQUEO"), GetType(String))
                               })

            Next

            Return objRemesas

        End Function

        Shared Sub ActualizarEstado(identificadorRemesa As String, estadoRemesa As String, usuarioActualizacion As String, fechaActualizacion As DateTime, estadoBulto As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_ActualizarEstadoRemesaBulto)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_ESTADO_REMESA", ProsegurDbType.Identificador_Alfanumerico, estadoRemesa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Descricao_Curta, usuarioActualizacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, fechaActualizacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_ESTADO_BULTO", ProsegurDbType.Identificador_Alfanumerico, estadoBulto))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        Public Shared Sub ActualizarEstadoRemesa(IdentificadorRemesa As String, _
                                                 CodigoEstadoRemesa As String, _
                                                 CodigoUsuario As String, _
                                                 FyhActualizacion As DateTime, _
                                                 EsActualizarUsuarioBloqueio As Boolean, _
                                                 CodigoUsuarioBloqueo As String)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            comando.CommandText = My.Resources.Salidas_Remesa_ActualizarEstadoRemesa
            comando.CommandType = CommandType.Text
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, IdentificadorRemesa))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_ESTADO_REMESA", ProsegurDbType.Objeto_Id, CodigoEstadoRemesa))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Descricao_Curta, CodigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, FyhActualizacion))

            Dim parametros As New List(Of String)
            If CodigoEstadoRemesa = Enumeradores.EstadoRemesa.EnCurso.RecuperarValor Then
                parametros.Add(" ,R.FYH_INICIO_ARMADO = []FYH_INICIO_ARMADO ")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_INICIO_ARMADO", ProsegurDbType.Data_Hora, FyhActualizacion))
            ElseIf CodigoEstadoRemesa = Enumeradores.EstadoRemesa.Procesada.RecuperarValor Then
                parametros.Add(" ,R.FYH_FIN_ARMADO = []FYH_FIN_ARMADO ")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_FIN_ARMADO", ProsegurDbType.Data_Hora, FyhActualizacion))
            End If
            Dim strParametros As String = String.Join(",", parametros)

            If EsActualizarUsuarioBloqueio Then
                comando.CommandText = String.Format(comando.CommandText, " ,R.COD_USUARIO_BLOQUEO = []COD_USUARIO_BLOQUEO " + strParametros)
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO_BLOQUEO", ProsegurDbType.Identificador_Alfanumerico, If(String.IsNullOrEmpty(CodigoUsuarioBloqueo), DBNull.Value, CodigoUsuarioBloqueo)))
            Else
                comando.CommandText = String.Format(comando.CommandText, String.Empty + strParametros)
            End If

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, comando.CommandText)

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, comando)

        End Sub

        Public Shared Function ActualizarEstadoRemesaConcorrencia(IdentificadorRemesa As String, _
                                                                  CodigoEstadoRemesa As String, _
                                                                  CodigoUsuario As String, _
                                                                  FyhActualizacion As DateTime, _
                                                                  FyhActualizacionNueva As DateTime, _
                                                                  CodigoUsuarioBloqueo As String) As Int32

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, IdentificadorRemesa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_ESTADO_REMESA", ProsegurDbType.Objeto_Id, CodigoEstadoRemesa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Descricao_Curta, CodigoUsuario))
            'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO_BLOQUEO", ProsegurDbType.Descricao_Curta, If(String.IsNullOrEmpty(CodigoUsuarioBloqueo), DBNull.Value, CodigoUsuarioBloqueo)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, FyhActualizacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION_NUEVA", ProsegurDbType.Data_Hora, FyhActualizacionNueva))

            Dim parametros As New List(Of String)
            If CodigoEstadoRemesa = Enumeradores.EstadoRemesa.EnCurso.RecuperarValor Then
                parametros.Add("R.FYH_INICIO_ARMADO = []FYH_INICIO_ARMADO")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_INICIO_ARMADO", ProsegurDbType.Data_Hora, FyhActualizacionNueva))
            ElseIf CodigoEstadoRemesa = Enumeradores.EstadoRemesa.Procesada.RecuperarValor Then
                parametros.Add("R.FYH_FIN_ARMADO = []FYH_FIN_ARMADO")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_FIN_ARMADO", ProsegurDbType.Data_Hora, FyhActualizacionNueva))
            End If

            If parametros.Count > 0 Then
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, String.Format(My.Resources.Salidas_Remesa_ActualizarEstadoRemesaConcorrencia, "," + String.Join(",", parametros)))
            Else
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, String.Format(My.Resources.Salidas_Remesa_ActualizarEstadoRemesaConcorrencia, ""))
            End If

            Return AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Function

        Public Shared Function ActualizarEstadoRemesa(IdentificadorRemesa As String, _
                                                CodigoEstadoRemesa As String, _
                                                CodigoUsuario As String, _
                                                FyhActualizacion As DateTime, _
                                                FyhInicioArmado As DateTime) As Integer

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_ActualizarEstadoRemesaEArmado)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, IdentificadorRemesa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_ESTADO_REMESA", ProsegurDbType.Objeto_Id, CodigoEstadoRemesa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Descricao_Curta, CodigoUsuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, FyhActualizacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_INICIO_ARMADO", ProsegurDbType.Data_Hora, If(FyhInicioArmado = DateTime.MinValue, Nothing, FyhInicioArmado)))

            Return AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Function

        Public Shared Function ValidarAlterarEstadoRemesaAsignado(identificadorRemesa As String) As Boolean
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_ValidarAlterarEstadoRemesaAsignado)

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALIDAS, cmd)
        End Function

        Shared Sub AsignarPuesto(identificadorRemesa As String, codigoPuesto As String, usuarioActualizacion As String, fechaActualizacion As DateTime, codDelegacion As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_AsignarPuesto)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_LISTA_TRABAJO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Descricao_Curta, usuarioActualizacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, fechaActualizacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, codigoPuesto.ToUpper()))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_DELEGACION", ProsegurDbType.Objeto_Id, codDelegacion))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        Shared Sub ActualizarPuestoAsignado(identificadorRemesa As String, codigoPuesto As String, usuarioActualizacion As String, fechaActualizacion As DateTime, codigoDelegacion As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_ActualizarPuestoAsignado)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, codigoPuesto.ToUpper()))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Descricao_Curta, usuarioActualizacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, fechaActualizacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        ''' <summary>
        ''' 1.	Verifica si se puede modificar el estado de la remesa. Sólo si puede volver al estado asignado una remesa cuyo estado actual esté en ‘EC’ (En Curso). 
        ''' Además, la remesa no puede tener bultos ya procesados o en curso. 'PR', 'SA', 'EN', 'EC'
        ''' </summary>
        ''' <param name="identificadorRemesa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ValidarModificarEstadoRemesa(identificadorRemesa As String) As DataTable

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_ValidarModificarEstadoRemesa)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))

            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

        End Function

        Public Shared Function RecuperarEstadoRemesa(identificadorRemesa As String) As String
            Dim retorno As String = String.Empty
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_RecuperarEstadoRemesa)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))

            Dim dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                retorno = dt.Rows(0)("COD_ESTADO_REMESA").ToString()
            End If

            Return retorno
        End Function

        ''' <summary>
        ''' Função que verifica se uma remesa foi alterada (caso o sistema legado 
        ''' ou outro supervisor tenha modificado os valores da remesa)
        ''' </summary>
        ''' <param name="codDelegacion"></param>
        ''' <param name="oidRemesaLegado"></param>
        ''' <param name="dataAtualizacao"></param>
        ''' <returns></returns>
        ''' <history>[cbomtempo] 29/04/2014	Creado</history>
        ''' <remarks></remarks>
        Public Shared Function VerificarRemesaFueAlterada(codDelegacion As String, _
                                                          oidRemesaLegado As String, _
                                                          dataAtualizacao As Date) As Integer
            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_VerificarRemesaFueAlterada)
                'Adiciona parâmetro à consulta
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codDelegacion))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA_LEGADO", ProsegurDbType.Objeto_Id, oidRemesaLegado))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, dataAtualizacao))
            End With
            'Retorna o registro
            Return DbHelper.AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALIDAS, cmd)
        End Function

        Public Shared Function VerificarRemesaDividida(codDelegacion As String, oidOT As String) As Boolean
            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesas_VerificarRemesaDividida)
                'Adiciona parâmetro à consulta
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codDelegacion))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_OT", ProsegurDbType.Objeto_Id, oidOT))
            End With
            'Retorna o registro
            Return DbHelper.AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALIDAS, cmd) > 0
        End Function

        ''' <summary>
        ''' Método que atualiza o usuário e data de uma remesa com
        ''' data/hora do sistema. Esta remesa é filtrada pelo código da delegação
        ''' e oidRemesaLegado
        ''' </summary>
        ''' <param name="remesa"></param>
        ''' <param name="codUsuario"></param>
        ''' <history>[cbomtempo]	29/04/2014	Creado</history>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarUsuarioData(remesa As Clases.Remesa, _
                                                codUsuario As String)
            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_ActualizarUsuarioDataPorDelegacionRemesaLegado)
                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, remesa.CodigoDelegacion))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA_LEGADO", ProsegurDbType.Objeto_Id, remesa.IdentificadorExterno))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, remesa.FechaHoraModificacion))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codUsuario))
            End With

            'Apaga a remesa do banco
            DbHelper.AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        ''' <summary>
        ''' Função que retorna o oid da remesa pelo oid da remesa legado
        ''' </summary>
        ''' <param name="oidRemesaLegado"></param>
        ''' <returns></returns>
        ''' <history>[cbomtempo]	29/04/2014	Creado</history>
        ''' <remarks></remarks>
        Public Shared Function RecuperarOidRemesa(oidRemesaLegado As String) As String

            'variável de retorno que irá armazenar o oid da remesa recuperado
            Dim strOidRemesa As String = String.Empty

            'Cria comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_RecuperarOidRemesa)
                'Cria o parâmetro para a consulta
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA_LEGADO", ProsegurDbType.Objeto_Id, oidRemesaLegado))
            End With
            'Executa a consulta retornando para um datatable
            Dim dt As DataTable = DbHelper.AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)
            'verifica se foi encontrado o oid da remesa, senão retorna valor vazio
            If dt.Rows.Count > 0 Then
                strOidRemesa = Util.ValidarValor(dt.Rows(0)("OID_REMESA"))
            End If

            Return strOidRemesa
        End Function

        Public Shared Function ObtenerUltimoCodigoReciboRemesa(PrefixoReciboRemesa As String, codigoDelegacion As String) As Integer

            'Cria comando SQL
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With comando
                .CommandType = CommandType.StoredProcedure
                .CommandText = String.Format("UTIL_{0}.FN_GENERAR_COD_RECIBO_REMESA", Prosegur.Genesis.Comon.Util.Version)

                'Cria o parâmetro para a consulta
                .Parameters.Add(Util.CriarParametroOracle("RESULT", ParameterDirection.ReturnValue, DBNull.Value, OracleClient.OracleType.Number))
                .Parameters.Add(Util.CriarParametroOracle("P_COD_PREFIJO", ParameterDirection.Input, PrefixoReciboRemesa, OracleClient.OracleType.VarChar))
                .Parameters.Add(Util.CriarParametroOracle("P_COD_DELEGACION", ParameterDirection.Input, codigoDelegacion, OracleClient.OracleType.VarChar))
            End With

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, comando)

            If comando IsNot Nothing AndAlso comando.Parameters IsNot Nothing AndAlso comando.Parameters("RESULT") IsNot Nothing Then
                'Resgata o valor do parâmetro de saída
                Return comando.Parameters("RESULT").Value.ToString()
            Else
                Return String.Empty
            End If

        End Function

        Public Shared Function ActualizarCodigoReciboRemesa(CodReciboRemesa As String, _
                                                            CodUsuario As String, _
                                                            CodDelegacion As String, _
                                                            OidRemesaLegado As String, _
                                                            FyhActualizacion As DateTime,
                                                            FyhActualizado As DateTime) As Integer

            'Cria comando SQL
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With comando
                .CommandType = CommandType.Text
                .CommandText = My.Resources.Salidas_Remesa_ActualizarCodigoReciboRemesa

                'Cria os parâmetros para gravar os dados
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_RECIBO_REMESA", ProsegurDbType.Identificador_Alfanumerico, CodReciboRemesa))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodUsuario))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUAL", ProsegurDbType.Data_Hora, FyhActualizado))
                'Parâmetros de filtro
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodDelegacion))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA_LEGADO", ProsegurDbType.Objeto_Id, OidRemesaLegado))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, FyhActualizacion))

            End With

            'Executa a consulta retornando o último código
            Return DbHelper.AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, comando)

        End Function

        Public Shared Function RemesasBloqueadasPorOutroUsuario(Remesas As ObservableCollection(Of Clases.Remesa),
                                                                CodigoUsuarioBloqueio As String) As ObservableCollection(Of Clases.Remesa)

            Dim objRemesas As New ObservableCollection(Of Clases.Remesa)

            Dim identificadoresRemesas As List(Of String) = (From r In Remesas Where Not String.IsNullOrEmpty(r.Identificador) Select r.Identificador).ToList

            If identificadoresRemesas IsNot Nothing AndAlso identificadoresRemesas.Count > 0 Then

                Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
                cmd.CommandType = CommandType.Text

                cmd.CommandText = My.Resources.Salidas_Remesa_RecuperarBloqueadasPorOutroUsuario
                cmd.CommandText = String.Format(cmd.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, identificadoresRemesas, "OID_REMESA", cmd, "AND", "REM"))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO_BLOQUEO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuarioBloqueio))

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    ' Para cada linha retornada
                    For Each dr As DataRow In dt.Rows

                        objRemesas.Add(New Clases.Remesa With
                                       {
                                           .Identificador = Util.AtribuirValorObj(dr("OID_REMESA"), GetType(String)),
                                           .IdentificadorExterno = Util.AtribuirValorObj(dr("OID_REMESA_LEGADO"), GetType(String)),
                                           .Estado = Extenciones.RecuperarEnum(Of EstadoRemesa)(Util.AtribuirValorObj(dr("COD_ESTADO_REMESA"), GetType(String))),
                                           .CodigoDelegacion = Util.AtribuirValorObj(dr("COD_DELEGACION"), GetType(String)),
                                          .UsuarioBloqueo = Util.AtribuirValorObj(dr("COD_USUARIO_BLOQUEO"), GetType(String))
                                       })

                    Next

                End If

            End If

            Return objRemesas

        End Function

        Public Shared Sub BloquearRemesa(IdentificadoresRemesa As List(Of String), CodigoUsuarioBloqueio As String)

            'Cria o comando SQL 
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, String.Format(My.Resources.Salidas_Remesa_BloquearRemesa,
                                                                                            Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS,
                                                                                                                  IdentificadoresRemesa,
                                                                                                                  "OID_REMESA",
                                                                                                                  cmd, "AND", "R")))
                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO_BLOQUEO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuarioBloqueio))
            End With

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        Public Shared Sub DesBloquearRemesa(IdentificadorRemesa As String, CodigoUsuarioBloqueio As String)

            'Cria o comando SQL 
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_DesBloquearRemesa)
                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, IdentificadorRemesa))
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO_BLOQUEO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuarioBloqueio))
            End With

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        Public Shared Sub DesBloquearRemesas(IdentificadoresRemesas As List(Of String), CodigoUsuarioBloqueio As String)

            'Cria o comando SQL 
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, String.Format(My.Resources.Salidas_Remesas_DesbloquearRemesas,
                                                                                            Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS,
                                                                                                                  IdentificadoresRemesas,
                                                                                                                  "OID_REMESA", cmd,
                                                                                                                  "AND")))
                'Adiciona parâmetro ao comando
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO_BLOQUEO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuarioBloqueio))
            End With

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        Public Shared Function RecuperarDatosRuta(identificadoresRemesa As List(Of String)) As ObservableCollection(Of Clases.Remesa)

            'Cria o comando SQL 
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            Dim remesas As New ObservableCollection(Of Clases.Remesa)

            cmd.CommandType = CommandType.Text
            cmd.CommandText = My.Resources.Salidas_Remesa_RecuperarDatosRuta

            cmd.CommandText += Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, identificadoresRemesa, "OID_REMESA", cmd, "AND", "R")

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

            Dim dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each row In dt.Rows

                    remesas.Add(New Clases.Remesa With {.Identificador = Util.AtribuirValorObj(row("OID_REMESA"), GetType(String)),
                                                   .Ruta = Util.AtribuirValorObj(row("COD_RUTA_REMESA"), GetType(String)),
                                                   .CodigoSecuencia = Util.AtribuirValorObj(row("COD_SECUENCIA"), GetType(String)),
                                                   .FechaHoraSalida = Util.AtribuirValorObj(row("FYH_SALIDA"), GetType(DateTime)),
                                                   .FechaServicio = Util.AtribuirValorObj(row("FEC_SERVICIO"), GetType(Date))})
                Next
            End If

            Return remesas

        End Function
        Public Shared Sub AnularRemesas(remesa As Clases.Remesa, codigoUsuario As String, fechahoraActualizacion As DateTime)
            'Cria o comando SQL 
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandType = CommandType.Text
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_AnularRemesas)

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, remesa.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA_LEGADO", ProsegurDbType.Objeto_Id, remesa.IdentificadorExterno))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_ESTADO_REMESA", ProsegurDbType.Descricao_Curta, remesa.Estado.RecuperarValor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Descricao_Longa, codigoUsuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, fechahoraActualizacion))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)
        End Sub

        Public Shared Sub ActualizarRemesas(identificadoresRemesas As List(Of String),
                                            CodigoRuta As String,
                                            CodigoSecuencia As String,
                                            FechaSalida As DateTime,
                                            FechaServicio As DateTime,
                                            Comentario As String,
                                            NelControlLegado As String,
                                            NelPedidoLegado As Integer?,
                                            identificadorOT As String,
                                            cantidadSolicitud As Integer)

            If identificadoresRemesas IsNot Nothing AndAlso identificadoresRemesas.Count > 0 Then

                ' se valor do campo ClaveSolicitudGE (SOL) = NelControlLegado (GENESIS) foi alterado, implementa novamente a regra para incremento do campo DES_REF_CLIENTE
                If ValidarClaveSolicitudGECambiada(identificadorOT, NelControlLegado) Then

                    Dim listaRemesas = identificadoresRemesas.Clonar

                    Dim remSecuencia As Integer

                    ' Se a chave de solução é númerico
                    If Not Int32.TryParse(NelControlLegado, remSecuencia) Then
                        remSecuencia = 1
                    End If

                    ' Define a quantidade de remessas solicitadas (incrementando de acordo com a sequencia)
                    Dim cantidadRemesas As Integer = (remSecuencia + cantidadSolicitud) - 1

                    For novoValor = remSecuencia To cantidadRemesas

                        ActualizarDatosRemesas(listaRemesas.First, CodigoRuta, CodigoSecuencia, NelControlLegado, NelPedidoLegado, novoValor, FechaSalida, FechaServicio, Comentario)
                        listaRemesas.Remove(listaRemesas.First)

                    Next

                Else

                    For Each identificadorRemesa In identificadoresRemesas

                        ActualizarDatosRemesas(identificadorRemesa, CodigoRuta, CodigoSecuencia, NelControlLegado, NelPedidoLegado, String.Empty, FechaSalida, FechaServicio, Comentario)

                    Next

                End If

            End If

        End Sub

        Private Shared Sub ActualizarDatosRemesas(identificadorRemesa As String, codigoRuta As String, codigoSecuencia As String, nelControlLegado As String, nelPedidoLegado As Integer?, des_ref_cliente As String, fechaSalida As DateTime, fechaServicio As DateTime, comentario As String)

            'Cria o comando SQL 
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandType = CommandType.Text
            cmd.CommandText = My.Resources.Salidas_Remesa_ActualizarRemesas

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_RUTA_REMESA", ProsegurDbType.Descricao_Curta, codigoRuta))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_SECUENCIA", ProsegurDbType.Descricao_Curta, codigoSecuencia))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "NEL_CONTROL_LEGADO", ProsegurDbType.Descricao_Longa, If(String.IsNullOrEmpty(nelControlLegado), "1", nelControlLegado)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "NEL_PEDIDO_LEGADO", ProsegurDbType.Inteiro_Curto, nelPedidoLegado))

            If Not String.IsNullOrEmpty(des_ref_cliente) Then
                cmd.CommandText = String.Format(cmd.CommandText, ", R.DES_REF_CLIENTE = []DES_REF_CLIENTE ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "DES_REF_CLIENTE", ProsegurDbType.Descricao_Longa, des_ref_cliente))
            Else
                cmd.CommandText = String.Format(cmd.CommandText, String.Empty)
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_SALIDA", ProsegurDbType.Data_Hora, fechaSalida))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FEC_SERVICIO", ProsegurDbType.Data_Hora, fechaServicio))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "DES_COMENTARIO", ProsegurDbType.Descricao_Longa, comentario))

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        Private Shared Function ValidarClaveSolicitudGECambiada(identificadorOT As String, nelControlLegado As String) As Boolean

            'Cria o comando SQL 
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandType = CommandType.Text
            cmd.CommandText = My.Resources.Salidas_Remesa_ValidarClaveSolicitudGeCambiada

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_OT", ProsegurDbType.Objeto_Id, identificadorOT))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "NEL_CONTROL_LEGADO", ProsegurDbType.Descricao_Longa, nelControlLegado))

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALIDAS, cmd) > 0

        End Function

        Public Shared Function ValidarServiciosArmados(identificadorOT As String) As Integer

            'Cria o comando SQL 
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandType = CommandType.Text
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_ValidarServiciosArmados)

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_OT", ProsegurDbType.Objeto_Id, identificadorOT))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt.Rows.Count > 0 Then

                Return dt.Rows.Count

            End If

            Return 0

        End Function


        Public Shared Function RecuperarRemesasPorOT(identificadorOT As String) As DataTable

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_RecuperarRemesasPorOT)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS, "OID_OT", ProsegurDbType.Identificador_Alfanumerico, identificadorOT))

            Return AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS, cmd)

        End Function

        Public Shared Function RecuperarRemesasRefPorOT(identificadorOT As String) As DataTable

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_RecuperarRemesasAnuladas)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS, "OID_OT", ProsegurDbType.Identificador_Alfanumerico, identificadorOT))

            Return AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS, cmd)

        End Function

        Public Shared Function GrabarCodigoReciboTransporte(codigoReciboTransporte As String,
                                                            identificadorRemesa As String,
                                                            codigoUsuario As String,
                                                            fechaActualizacion As Date) As Integer

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, Constantes.SP_SALIDAS_GRABAR_RECIBO)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "P_COD_RECIBO_TRANSPORTE", ProsegurDbType.Descricao_Longa, codigoReciboTransporte))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "P_OID_REMESA", ProsegurDbType.Identificador_Alfanumerico, identificadorRemesa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "P_COD_USUARIO", ProsegurDbType.Descricao_Curta, codigoUsuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "P_FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, fechaActualizacion))
            cmd.Parameters.Add(Util.CriarParametroOracle("P_ACTUALIZADO", ParameterDirection.Output, Nothing, OracleClient.OracleType.Int16, 1000))

            AcessoDados.ExecutarNonQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS, cmd)

            Return Util.AtribuirValorObj(cmd.Parameters("P_ACTUALIZADO").Value, GetType(Int16))

        End Function

        Public Shared Function ValidarCodigoReciboTransporte(remesa As Clases.Remesa) As Integer

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, Constantes.SP_SALIDAS_VALIDAR_RECIBO)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "P_COD_RECIBO_TRANSPORTE", ProsegurDbType.Descricao_Longa, remesa.CodigoReciboSalida))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "P_OID_REMESA", ProsegurDbType.Objeto_Id, remesa.Identificador))
            cmd.Parameters.Add(Util.CriarParametroOracle("P_ACTUALIZADO", ParameterDirection.Output, Nothing, OracleClient.OracleType.Int16, 1000))

            AcessoDados.ExecutarNonQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS, cmd)

            Return Util.AtribuirValorObj(cmd.Parameters("P_ACTUALIZADO").Value, GetType(Int16))

        End Function

        Public Shared Sub BorrarCodigosRecibosTransporte(identificadoresRemesas As List(Of String))

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_Remesa_BorrarCodigoReciboRemesa
            cmd.CommandType = CommandType.Text

            cmd.CommandText = String.Format(cmd.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, identificadoresRemesas, "OID_REMESA", cmd, "WHERE", "REME"))
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)
            AcessoDados.ExecutarNonQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        Public Shared Sub ActualizarReenvioPrecintoLegadoSOL(identificadorRemesa As String,
                                                             fechaActualizacion As DateTime,
                                                             reenvioLegado As Boolean,
                                                             reenvioSOL As Boolean)

            If reenvioLegado Then

                Dim cmdLegado As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS)
                cmdLegado.CommandType = CommandType.Text

                cmdLegado.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_ActualizarBolEnviadoLegado)

                cmdLegado.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "NEL_INTENTOS_LEGADO", ProsegurDbType.Inteiro_Curto, 0))
                cmdLegado.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "BOL_ENVIADO_LEGADO_NUEVO", ProsegurDbType.Inteiro_Curto, 0))
                cmdLegado.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FECHA_MODIFICACION", ProsegurDbType.Data_Hora, fechaActualizacion))
                cmdLegado.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
                cmdLegado.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_ESTADO_REMESA", ProsegurDbType.Descricao_Curta, Enumeradores.EstadoRemesa.Procesada.RecuperarValor))

                AcessoDados.ExecutarNonQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS, cmdLegado)

            End If

            If reenvioSOL Then

                Dim cmdSOL As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS)
                cmdSOL.CommandType = CommandType.Text

                cmdSOL.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_ActualizarBolEnviadoSOL)

                cmdSOL.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "NEL_INTENTOS_SOL", ProsegurDbType.Inteiro_Curto, 0))
                cmdSOL.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "BOL_ENVIADO_SOL_NUEVO", ProsegurDbType.Inteiro_Curto, 0))
                cmdSOL.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FECHA_MODIFICACION", ProsegurDbType.Data_Hora, fechaActualizacion))
                cmdSOL.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
                cmdSOL.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_ESTADO_REMESA", ProsegurDbType.Descricao_Curta, Enumeradores.EstadoRemesa.Procesada.RecuperarValor))

                AcessoDados.ExecutarNonQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS, cmdSOL)

            End If

        End Sub

        Public Shared Sub GrabarErrorEnviadoSOL(identificadorRemesa As String, descripcionError As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_Remesa_GrabarErrorEnviadoSOL
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "DES_ERROR_ENV_SOL", ProsegurDbType.Observacao_Longa, descripcionError))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)
            AcessoDados.ExecutarNonQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        Public Shared Sub GrabarIntentosEnvioSOL(identificadorRemesa As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_Remesa_GrabarIntentosEnvioSOL
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)
            AcessoDados.ExecutarNonQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        Public Shared Sub GrabarIntentosEnvioSOL(identificadorRemesa As String, nelIntentos As Integer, bolEnviadoSOL As Boolean, descripcionError As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_Remesa_GrabarIntentosEnvioSOL_PARAM
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "NEL_INTENTOS_SOL", ProsegurDbType.Inteiro_Curto, nelIntentos))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "bol_enviado_sol", ProsegurDbType.Logico, bolEnviadoSOL))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "DES_ERROR_ENV_SOL", ProsegurDbType.Observacao_Longa, descripcionError))
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)
            AcessoDados.ExecutarNonQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS, cmd)

        End Sub

        Public Shared Function RecuperaRemesasNaoEnviadasSOL(identificadoresOT As List(Of String)) As List(Of String)
            Dim lstIdentificadoresOT As New List(Of String)
            Using comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

                Dim filtros As String = ""
                filtros = Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, identificadoresOT, "OID_OT", comando, "AND", "R")

                If Not String.IsNullOrEmpty(filtros) Then

                    comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, String.Format(My.Resources.Salidas_Remesa_RecuperaRemesasNaoEnviadasSOL, filtros))
                    comando.CommandType = CommandType.Text

                    Dim dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, comando)
                    If dt IsNot Nothing Then
                        For Each dtRow In dt.Rows
                            lstIdentificadoresOT.Add(Util.AtribuirValorObj(dtRow("OID_OT"), GetType(String)))
                        Next
                    End If

                End If


                Return lstIdentificadoresOT
            End Using

        End Function

        Public Shared Function RecuperarValoresRemesa(identificadorRemesa As String) As DataTable
            Using comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

                comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Remesa_RecuperarValoresRemesa)
                comando.CommandType = CommandType.Text

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))

                Dim dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, comando)
                Return dt

            End Using
        End Function

#Region "Dasboard"

        Public Shared Function RetornaCantidadRemesasPorSector(codigoDelegacao As List(Of String), dataInicial As DateTime, codigoSector As List(Of String)) As DataTable
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            Dim query As String = ""

            comando.CommandText = My.Resources.Salidas_Remesa_CantidadRemesasPorSector
            comando.CommandType = CommandType.Text

            If (codigoDelegacao IsNot Nothing AndAlso codigoDelegacao.Count > 0) Then
                If (codigoDelegacao.Count = 1) Then
                    query = " AND REME.COD_DELEGACION = []COD_DELEGACION"
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, codigoDelegacao(0)))
                Else
                    query = Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, codigoDelegacao, "COD_DELEGACION", comando, "AND", "REME")
                End If
            End If

            If (codigoSector IsNot Nothing AndAlso codigoSector.Count > 0) Then
                If (codigoSector.Count = 1) Then
                    query &= " AND REME.COD_SECTOR = []COD_SECTOR"
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SECTOR", ProsegurDbType.Descricao_Curta, codigoSector(0)))
                Else
                    query &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigoSector, "COD_SECTOR", comando, "AND", "REME")
                End If
            End If

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "DATA_INI", ProsegurDbType.Data_Hora, dataInicial))

            comando.CommandText = String.Format(comando.CommandText, query)
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, comando.CommandText)

            Dim dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, comando)
            Return dt
        End Function

        Public Shared Function RetornaCantidadBilletesContadosPorSector(codigoDelegacao As List(Of String), identificadorDivisa As List(Of String), dataInicial As DateTime, dataFinal As DateTime, codigoSector As List(Of String)) As DataTable
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            Dim query As String = ""

            comando.CommandText = My.Resources.Salidas_Remesa_CantidadBilhetesContadosSetor
            comando.CommandType = CommandType.Text

            If (codigoDelegacao IsNot Nothing AndAlso codigoDelegacao.Count > 0) Then
                If (codigoDelegacao.Count = 1) Then
                    query = " AND REME.COD_DELEGACION = []COD_DELEGACION"
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, codigoDelegacao(0)))
                Else
                    query = Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, codigoDelegacao, "COD_DELEGACION", comando, "AND", "REME")
                End If
            End If

            If (identificadorDivisa IsNot Nothing AndAlso identificadorDivisa.Count > 0) Then
                query &= Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, identificadorDivisa, "OID_DIVISA", comando, "AND", "DENO")
            End If

            If (codigoSector IsNot Nothing AndAlso codigoSector.Count > 0) Then
                If (codigoSector.Count = 1) Then
                    query &= " AND REME.COD_SECTOR = []COD_SECTOR"
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SECTOR", ProsegurDbType.Descricao_Curta, codigoSector(0)))
                Else
                    query &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigoSector, "COD_SECTOR", comando, "AND", "REME")
                End If
            End If

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "DATA_INI", ProsegurDbType.Data_Hora, dataInicial))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "DATA_FIN", ProsegurDbType.Data_Hora, dataFinal))

            comando.CommandText = String.Format(comando.CommandText, query)
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, comando.CommandText)

            Dim dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, comando)
            Return dt
        End Function

        Public Shared Function RetornaCantidadContadoPorDenominacion(codigoDelegacao As List(Of String), codigoSector As List(Of String), identificadorDivisa As List(Of String), _
                                                                     dataInicial As DateTime, dataFinal As DateTime, estadoRemesa As EstadoRemesa?) As DataTable
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            Dim query As String = ""

            comando.CommandText = My.Resources.Salidas_Remesa_CantidadContadosDenominacao
            comando.CommandType = CommandType.Text

            If (codigoDelegacao IsNot Nothing AndAlso codigoDelegacao.Count > 0) Then
                If (codigoDelegacao.Count = 1) Then
                    query = " AND REME.COD_DELEGACION = []COD_DELEGACION"
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, codigoDelegacao(0)))
                Else
                    query = Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, codigoDelegacao, "COD_DELEGACION", comando, "AND", "REME")
                End If
            End If

            If (identificadorDivisa IsNot Nothing AndAlso identificadorDivisa.Count > 0) Then
                query &= Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, identificadorDivisa, "OID_DIVISA", comando, "AND", "DENO")
            End If

            If (codigoSector IsNot Nothing AndAlso codigoSector.Count > 0) Then
                If (codigoSector.Count = 1) Then
                    query &= " AND REME.COD_SECTOR = []COD_SECTOR"
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SECTOR", ProsegurDbType.Descricao_Curta, codigoSector(0)))
                Else
                    query &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigoSector, "COD_SECTOR", comando, "AND", "REME")
                End If
            End If

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "DATA_INI", ProsegurDbType.Data_Hora, dataInicial))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "DATA_FIN", ProsegurDbType.Data_Hora, dataFinal))

            If estadoRemesa.HasValue Then
                query &= " AND REME.COD_ESTADO_REMESA = []COD_ESTADO_REMESA"
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_ESTADO_REMESA", ProsegurDbType.Descricao_Curta, estadoRemesa.Value.RecuperarValor))
            End If

            comando.CommandText = String.Format(comando.CommandText, query)
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, comando.CommandText)

            Dim dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, comando)
            Return dt
        End Function

        Public Shared Function RetornaCantidadBilletesDelDia(codigoDelegacao As List(Of String), identificadorDivisa As List(Of String), dataInicial As DateTime, dataFinal As DateTime, codigoSector As List(Of String)) As Integer
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            Dim query As String = ""

            comando.CommandText = My.Resources.Salidas_Remesa_CantidadBilhetesDelDia
            comando.CommandType = CommandType.Text

            If (codigoDelegacao IsNot Nothing AndAlso codigoDelegacao.Count > 0) Then
                If (codigoDelegacao.Count = 1) Then
                    query = " AND REME.COD_DELEGACION = []COD_DELEGACION"
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, codigoDelegacao(0)))
                Else
                    query = Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, codigoDelegacao, "COD_DELEGACION", comando, "AND", "REME")
                End If
            End If

            If (identificadorDivisa IsNot Nothing AndAlso identificadorDivisa.Count > 0) Then
                query &= Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, identificadorDivisa, "OID_DIVISA", comando, "AND", "DENO")
            End If

            If (codigoSector IsNot Nothing AndAlso codigoSector.Count > 0) Then
                If (codigoSector.Count = 1) Then
                    query &= " AND REME.COD_SECTOR = []COD_SECTOR"
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SECTOR", ProsegurDbType.Descricao_Curta, codigoSector(0)))
                Else
                    query &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigoSector, "COD_SECTOR", comando, "AND", "REME")
                End If
            End If

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "DATA_INI", ProsegurDbType.Data_Hora, dataInicial))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "DATA_FIN", ProsegurDbType.Data_Hora, dataFinal))

            comando.CommandText = String.Format(comando.CommandText, query)
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, comando.CommandText)

            Dim dt = AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALIDAS, comando)
            Return dt
        End Function

#End Region

    End Class

End Namespace