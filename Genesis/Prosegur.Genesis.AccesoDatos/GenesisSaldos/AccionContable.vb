Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio
Imports System.Collections.ObjectModel
Imports System.Text

Namespace GenesisSaldos

    ''' <summary>
    ''' Classe de AccionContable.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 04/09/2013 - Criado
    ''' </history>
    Public Class AccionContable

#Region "[CONSULTAS]"

        ''' <summary>
        ''' Recupera acciones contables ativas
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [victor.ramos] 19/02/2014 - Criado
        ''' </history>
        Public Shared Function ObtenerAccionesContables() As List(Of Clases.AccionContable)

            Dim listaAccionesContables As New List(Of Clases.AccionContable)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AccionContableRecuperar)
            cmd.CommandType = CommandType.Text

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each row In dt.Rows
                    Dim objAccionContable As New Clases.AccionContable
                    With objAccionContable

                        .Identificador = Util.AtribuirValorObj(row("OID_ACCION_CONTABLE"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(row("COD_ACCION_CONTABLE"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(row("DES_ACCION_CONTABLE"), GetType(String))
                        .EstaActivo = Util.AtribuirValorObj(row("BOL_ACTIVO"), GetType(Boolean))
                        .FechaHoraCreacion = Util.AtribuirValorObj(row("GMT_CREACION"), GetType(DateTime))
                        .UsuarioCreacion = Util.AtribuirValorObj(row("DES_USUARIO_CREACION"), GetType(String))
                        .FechaHoraModificacion = Util.AtribuirValorObj(row("GMT_MODIFICACION"), GetType(DateTime))
                        .UsuarioModificacion = Util.AtribuirValorObj(row("DES_USUARIO_MODIFICACION"), GetType(String))

                        .Acciones = ObtenerAccionesTransacciones(.Identificador)

                    End With

                    listaAccionesContables.Add(objAccionContable)
                Next
            End If

            Return listaAccionesContables
        End Function

        ''' <summary>
        ''' Recupera todas las acciones contables que tengan solamente configuraciones para el estado "Aceptado".
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerAccionesContablesSoloConEstadoAceptado() As List(Of Clases.AccionContable)

            Dim listaAccionesContables As New List(Of Clases.AccionContable)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AccionContableRecuperarSoloConEstadoAceptado)
            cmd.CommandType = CommandType.Text

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each row In dt.Rows
                    Dim objAccionContable As New Clases.AccionContable
                    With objAccionContable

                        .Identificador = Util.AtribuirValorObj(row("OID_ACCION_CONTABLE"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(row("COD_ACCION_CONTABLE"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(row("DES_ACCION_CONTABLE"), GetType(String))
                        .EstaActivo = Util.AtribuirValorObj(row("BOL_ACTIVO"), GetType(Boolean))
                        .FechaHoraCreacion = Util.AtribuirValorObj(row("GMT_CREACION"), GetType(DateTime))
                        .UsuarioCreacion = Util.AtribuirValorObj(row("DES_USUARIO_CREACION"), GetType(String))
                        .FechaHoraModificacion = Util.AtribuirValorObj(row("GMT_MODIFICACION"), GetType(DateTime))
                        .UsuarioModificacion = Util.AtribuirValorObj(row("DES_USUARIO_MODIFICACION"), GetType(String))

                        .Acciones = ObtenerAccionesTransacciones(.Identificador)

                    End With

                    listaAccionesContables.Add(objAccionContable)
                Next
            End If

            Return listaAccionesContables
        End Function

        ''' <summary>
        ''' Recupera a accionContable pelo identificador
        ''' </summary>
        ''' <param name="identificador"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [claudioniz.pereira] 04/09/2013 - Criado
        ''' </history>
        Public Shared Function ObtenerAccionContable(identificador As String) As Clases.AccionContable

            Dim objAccionContable As Clases.AccionContable = Nothing
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AccionContableRecuperarPorIdentificador)
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ACCION_CONTABLE", ProsegurDbType.Objeto_Id, identificador))

            cmd.CommandType = CommandType.Text

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objAccionContable = New Clases.AccionContable
                With objAccionContable

                    .Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_ACCION_CONTABLE"), GetType(String))
                    .Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_ACCION_CONTABLE"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_ACCION_CONTABLE"), GetType(String))
                    .EstaActivo = Util.AtribuirValorObj(dt.Rows(0)("BOL_ACTIVO"), GetType(String))
                    .FechaHoraCreacion = Util.AtribuirValorObj(dt.Rows(0)("GMT_CREACION"), GetType(String))
                    .FechaHoraModificacion = Util.AtribuirValorObj(dt.Rows(0)("GMT_MODIFICACION"), GetType(String))
                    .UsuarioCreacion = Util.AtribuirValorObj(dt.Rows(0)("DES_USUARIO_CREACION"), GetType(String))
                    .UsuarioModificacion = Util.AtribuirValorObj(dt.Rows(0)("DES_USUARIO_MODIFICACION"), GetType(String))

                    .Acciones = ObtenerAccionesTransacciones(.Identificador)

                End With

            End If

            Return objAccionContable

        End Function
        Public Shared Function ObtenerAccionContablePorCodigo(codigo As String) As Clases.AccionContable

            Dim objAccionContable As Clases.AccionContable = Nothing
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AccionContableRecuperarPorCodigo)
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ACCION_CONTABLE", ProsegurDbType.Objeto_Id, codigo))

            cmd.CommandType = CommandType.Text

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objAccionContable = New Clases.AccionContable
                With objAccionContable

                    .Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_ACCION_CONTABLE"), GetType(String))
                    .Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_ACCION_CONTABLE"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_ACCION_CONTABLE"), GetType(String))
                    .EstaActivo = Util.AtribuirValorObj(dt.Rows(0)("BOL_ACTIVO"), GetType(String))
                    .FechaHoraCreacion = Util.AtribuirValorObj(dt.Rows(0)("GMT_CREACION"), GetType(String))
                    .FechaHoraModificacion = Util.AtribuirValorObj(dt.Rows(0)("GMT_MODIFICACION"), GetType(String))
                    .UsuarioCreacion = Util.AtribuirValorObj(dt.Rows(0)("DES_USUARIO_CREACION"), GetType(String))
                    .UsuarioModificacion = Util.AtribuirValorObj(dt.Rows(0)("DES_USUARIO_MODIFICACION"), GetType(String))

                    .Acciones = ObtenerAccionesTransacciones(.Identificador)

                End With

            End If

            Return objAccionContable

        End Function
        ''' <summary>
        ''' Verifica se a Ação Contábel está presente em mais de um formulário
        ''' </summary>
        ''' <param name="identificador"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function VerificaAccionContableMasDeUnoFormulario(identificador As String) As Boolean

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AccionContableVerificaMasDeUnoFormulario)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ACCION_CONTABLE", ProsegurDbType.Objeto_Id, identificador))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 1 Then
                Return True
            Else
                Return False
            End If
        End Function
        ''' <summary>
        ''' Insere uma nova accionContable
        ''' </summary>
        ''' <param name="accionContable"></param>
        ''' <remarks></remarks>
        ''' <history>
        ''' [victor.ramos] 15/01/2014 - Criado
        ''' </history>
        Public Shared Sub GuardarAccionContable(accionContable As Clases.AccionContable)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AccionContableInserir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ACCION_CONTABLE", ProsegurDbType.Objeto_Id, accionContable.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ACCION_CONTABLE", ProsegurDbType.Descricao_Longa, accionContable.Codigo))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_ACCION_CONTABLE", ProsegurDbType.Descricao_Longa, accionContable.Descripcion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_ACTIVO", ProsegurDbType.Inteiro_Curto, accionContable.EstaActivo))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, accionContable.UsuarioCreacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, accionContable.UsuarioModificacion))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        End Sub
        Private Shared Function ObtenerAccionesTransacciones(identificadorAccionContable As String) As ObservableCollection(Of Clases.AccionTransaccion)

            Dim objAccionesContables As New ObservableCollection(Of Clases.AccionTransaccion)

            Try

                Dim listaEstadoAccionContable = EstadoAccionContable.ObtenerEstadosAccionContable(identificadorAccionContable)

                'Se encontrou algum estado de accioncontable
                If listaEstadoAccionContable IsNot Nothing AndAlso listaEstadoAccionContable.Count > 0 Then

                    For Each estadoAccion In listaEstadoAccionContable

                        Dim objAccionTransaccion As New Clases.AccionTransaccion

                        'Quando o valor for zero, será ignorado, não será recuperada a accionTransacion
                        If estadoAccion.OrigemDisponible <> Prosegur.Genesis.Comon.Constantes.TipoMovimientoSinMovimiento Then
                            'Quando for OrgemDisponible
                            objAccionTransaccion = Prosegur.Genesis.Comon.Util.AccionTransaccion(estadoAccion.OrigemDisponible, estadoAccion.Codigo, Enumeradores.TipoSitio.Origen, Enumeradores.TipoSaldo.Disponible)
                            objAccionesContables.Add(objAccionTransaccion)
                        End If

                        'Quando o valor for zero, será ignorado, não será recuperada a accionTransacion
                        If estadoAccion.OrigemNoDisponible <> Prosegur.Genesis.Comon.Constantes.TipoMovimientoSinMovimiento Then
                            'Quando for OrgemNoDisponible
                            objAccionTransaccion = Prosegur.Genesis.Comon.Util.AccionTransaccion(estadoAccion.OrigemNoDisponible, estadoAccion.Codigo, Enumeradores.TipoSitio.Origen, Enumeradores.TipoSaldo.NoDisponible)
                            objAccionesContables.Add(objAccionTransaccion)
                        End If

                        'Quando o valor for zero, será ignorado, não será recuperada a accionTransacion
                        If estadoAccion.DestinoDisponible <> Prosegur.Genesis.Comon.Constantes.TipoMovimientoSinMovimiento Then
                            'Quando for DestinoDisponible
                            objAccionTransaccion = Prosegur.Genesis.Comon.Util.AccionTransaccion(estadoAccion.DestinoDisponible, estadoAccion.Codigo, Enumeradores.TipoSitio.Destino, Enumeradores.TipoSaldo.Disponible)
                            objAccionesContables.Add(objAccionTransaccion)
                        End If

                        'Quando o valor for zero, será ignorado, não será recuperada a accionTransacion
                        If estadoAccion.DestinoNoDisponible <> Prosegur.Genesis.Comon.Constantes.TipoMovimientoSinMovimiento Then
                            'Quando for DestinoNoDisponible
                            objAccionTransaccion = Prosegur.Genesis.Comon.Util.AccionTransaccion(estadoAccion.DestinoNoDisponible, estadoAccion.Codigo, Enumeradores.TipoSitio.Destino, Enumeradores.TipoSaldo.NoDisponible)
                            objAccionesContables.Add(objAccionTransaccion)
                        End If
                    Next
                End If

            Catch ex As Excepcion.CampoObrigatorioException
                Throw
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try

            Return objAccionesContables

        End Function

        Public Shared Sub ObtenerAccionesTransacciones_v2(EstadoAccionContable As Clases.EstadoAccionContable,
                                                           ByRef AccionesContables As ObservableCollection(Of Clases.AccionTransaccion))

            Try

                'Se encontrou algum estado de accioncontable
                If EstadoAccionContable IsNot Nothing Then

                    Dim objAccionTransaccion As New Clases.AccionTransaccion

                    'Quando o valor for zero, será ignorado, não será recuperada a accionTransacion
                    If EstadoAccionContable.OrigemDisponible <> Prosegur.Genesis.Comon.Constantes.TipoMovimientoSinMovimiento Then
                        'Quando for OrgemDisponible
                        objAccionTransaccion = Prosegur.Genesis.Comon.Util.AccionTransaccion(EstadoAccionContable.OrigemDisponible, EstadoAccionContable.Codigo, Enumeradores.TipoSitio.Origen, Enumeradores.TipoSaldo.Disponible)
                        AccionesContables.Add(objAccionTransaccion)
                    End If

                    'Quando o valor for zero, será ignorado, não será recuperada a accionTransacion
                    If EstadoAccionContable.OrigemNoDisponible <> Prosegur.Genesis.Comon.Constantes.TipoMovimientoSinMovimiento Then
                        'Quando for OrgemNoDisponible
                        objAccionTransaccion = Prosegur.Genesis.Comon.Util.AccionTransaccion(EstadoAccionContable.OrigemNoDisponible, EstadoAccionContable.Codigo, Enumeradores.TipoSitio.Origen, Enumeradores.TipoSaldo.NoDisponible)
                        AccionesContables.Add(objAccionTransaccion)
                    End If

                    'Quando o valor for zero, será ignorado, não será recuperada a accionTransacion
                    If EstadoAccionContable.DestinoDisponible <> Prosegur.Genesis.Comon.Constantes.TipoMovimientoSinMovimiento Then
                        'Quando for DestinoDisponible
                        objAccionTransaccion = Prosegur.Genesis.Comon.Util.AccionTransaccion(EstadoAccionContable.DestinoDisponible, EstadoAccionContable.Codigo, Enumeradores.TipoSitio.Destino, Enumeradores.TipoSaldo.Disponible)
                        AccionesContables.Add(objAccionTransaccion)
                    End If

                    'Quando o valor for zero, será ignorado, não será recuperada a accionTransacion
                    If EstadoAccionContable.DestinoNoDisponible <> Prosegur.Genesis.Comon.Constantes.TipoMovimientoSinMovimiento Then
                        'Quando for DestinoNoDisponible
                        objAccionTransaccion = Prosegur.Genesis.Comon.Util.AccionTransaccion(EstadoAccionContable.DestinoNoDisponible, EstadoAccionContable.Codigo, Enumeradores.TipoSitio.Destino, Enumeradores.TipoSaldo.NoDisponible)
                        AccionesContables.Add(objAccionTransaccion)
                    End If

                End If

            Catch ex As Excepcion.CampoObrigatorioException
                Throw
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try

        End Sub

#End Region
    End Class
End Namespace



