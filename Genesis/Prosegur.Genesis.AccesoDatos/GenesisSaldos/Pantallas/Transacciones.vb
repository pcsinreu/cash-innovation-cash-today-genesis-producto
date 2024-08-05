Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.RecuperarTransacciones
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Framework.Dicionario

Namespace GenesisSaldos.Pantallas

    Public Class Transacciones

        Public Shared Sub RecuperarInformaciones(ByRef Delegaciones As Dictionary(Of String, String),
                                                 ByRef Canales As Dictionary(Of String, String),
                                                 ByRef Tipo_Planificaciones As Dictionary(Of String, String),
                                                 ByRef Tipo_Transacciones As Dictionary(Of String, String),
                                                 ByVal usuario As String)

            Try

                Dim ds As DataSet
                Dim spw As SPWrapper = Nothing

                spw = srecuperar_info_transacciones(usuario)
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

                If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                    ' Delegaciones
                    If ds.Tables.Contains("delegaciones") AndAlso ds.Tables("delegaciones").Rows.Count > 0 Then
                        If Delegaciones Is Nothing Then Delegaciones = New Dictionary(Of String, String)
                        For Each rowDelegacion As DataRow In ds.Tables("delegaciones").Rows
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDelegacion("OID_DELEGACION"), GetType(String))) AndAlso
                                Not Delegaciones.ContainsKey(Util.AtribuirValorObj(rowDelegacion("OID_DELEGACION"), GetType(String))) Then
                                Delegaciones.Add(Util.AtribuirValorObj(rowDelegacion("OID_DELEGACION"), GetType(String)), Util.AtribuirValorObj(rowDelegacion("DES_DELEGACION"), GetType(String)))
                            End If
                        Next
                    End If

                    ' Canales
                    If ds.Tables.Contains("Canales") AndAlso ds.Tables("Canales").Rows.Count > 0 Then
                        If Canales Is Nothing Then Canales = New Dictionary(Of String, String)
                        For Each rowCanal As DataRow In ds.Tables("Canales").Rows
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowCanal("OID_CANAL"), GetType(String))) AndAlso
                                Not Canales.ContainsKey(Util.AtribuirValorObj(rowCanal("OID_CANAL"), GetType(String))) Then
                                Canales.Add(Util.AtribuirValorObj(rowCanal("OID_CANAL"), GetType(String)), Util.AtribuirValorObj(rowCanal("DES_CANAL"), GetType(String)))
                            End If
                        Next
                    End If

                    ' Tipo_Planificaciones
                    If ds.Tables.Contains("tipo_planificacion") AndAlso ds.Tables("tipo_planificacion").Rows.Count > 0 Then
                        If Tipo_Planificaciones Is Nothing Then Tipo_Planificaciones = New Dictionary(Of String, String)
                        For Each rowTipo As DataRow In ds.Tables("tipo_planificacion").Rows
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowTipo("OID_TIPO_PLANIFICACION"), GetType(String))) AndAlso
                                Not Tipo_Planificaciones.ContainsKey(Util.AtribuirValorObj(rowTipo("OID_TIPO_PLANIFICACION"), GetType(String))) Then
                                Tipo_Planificaciones.Add(Util.AtribuirValorObj(rowTipo("OID_TIPO_PLANIFICACION"), GetType(String)), Util.AtribuirValorObj(rowTipo("DES_TIPO_PLANIFICACION"), GetType(String)))
                            End If
                        Next
                    End If

                    ' Tipo_Transacciones
                    If ds.Tables.Contains("tipo_transaccion") AndAlso ds.Tables("tipo_transaccion").Rows.Count > 0 Then
                        If Tipo_Transacciones Is Nothing Then Tipo_Transacciones = New Dictionary(Of String, String)
                        For Each rowTipo As DataRow In ds.Tables("tipo_transaccion").Rows
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowTipo("OID_TIPO_TRANSACCION"), GetType(String))) AndAlso
                                Not Tipo_Transacciones.ContainsKey(Util.AtribuirValorObj(rowTipo("OID_TIPO_TRANSACCION"), GetType(String))) Then
                                Tipo_Transacciones.Add(Util.AtribuirValorObj(rowTipo("OID_TIPO_TRANSACCION"), GetType(String)), Util.AtribuirValorObj(rowTipo("DES_TIPO_TRANSACCION"), GetType(String)))
                            End If
                        Next
                    End If
                End If

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try

        End Sub

        Private Shared Function srecuperar_info_transacciones(ByVal usuario As String) As SPWrapper

            Dim SP As String = String.Format("SAPR_PPANTALLAS_{0}.srecuperar_info_transacciones", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, If(String.IsNullOrEmpty(usuario), "PANTALLA_TRANSACCIONES", usuario), , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$rc_delegaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "delegaciones")
            spw.AgregarParam("par$rc_canales", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "canales")
            spw.AgregarParam("par$rc_tipo_planificacion", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "tipo_planificacion")
            spw.AgregarParam("par$rc_tipo_transaccion", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "tipo_transaccion")

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            Return spw

        End Function

        Public Shared Sub RecuperarInformacionesDinamico(ByRef Maquinas As Dictionary(Of String, String),
                                                         ByRef PuntosServicios As List(Of PtoServicio),
                                                         ByRef Planificaciones As Dictionary(Of String, String),
                                                         Peticion As Peticion,
                                                         Usuario As String)

            Try

                Dim ds As DataSet
                Dim spw As SPWrapper = Nothing

                spw = srecuperar_info_dinamico(Peticion, Usuario)
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

                If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                    ' Maquinas
                    If ds.Tables.Contains("maquinas") AndAlso ds.Tables("maquinas").Rows.Count > 1 Then
                        If Maquinas Is Nothing Then Maquinas = New Dictionary(Of String, String)
                        For Each rowMaquina As DataRow In ds.Tables("maquinas").Rows
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowMaquina("OID_MAQUINA"), GetType(String))) AndAlso
                                Not Maquinas.ContainsKey(Util.AtribuirValorObj(rowMaquina("OID_MAQUINA"), GetType(String))) Then
                                Maquinas.Add(Util.AtribuirValorObj(rowMaquina("OID_MAQUINA"), GetType(String)), Util.AtribuirValorObj(rowMaquina("COD_MAQUINA"), GetType(String)))
                            End If
                        Next

                        ' PuntosServicios
                        If ds.Tables.Contains("ptoservicios") AndAlso ds.Tables("ptoservicios").Rows.Count > 1 Then
                            If PuntosServicios Is Nothing Then PuntosServicios = New List(Of PtoServicio)
                            For Each rowPunto As DataRow In ds.Tables("ptoservicios").Rows
                                If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowPunto("OID_PTO_SERVICIO"), GetType(String))) AndAlso
                                    PuntosServicios.FirstOrDefault(Function(x) x.OID_PTO_SERVICIO = Util.AtribuirValorObj(rowPunto("OID_MAQUINA"), GetType(String))) Is Nothing Then  '  > 20    ContainsKey(Util.AtribuirValorObj(rowPunto("OID_PTO_SERVICIO"), GetType(String))) Then

                                    Dim pto = New PtoServicio

                                    With pto
                                        .DES_PTO_SERVICIO = Util.AtribuirValorObj(rowPunto("DES_PTO_SERVICIO"), GetType(String))
                                        .OID_MAQUINA = Util.AtribuirValorObj(rowPunto("OID_MAQUINA"), GetType(String))
                                        .OID_PTO_SERVICIO = Util.AtribuirValorObj(rowPunto("OID_PTO_SERVICIO"), GetType(String))
                                    End With
                                    PuntosServicios.Add(pto)

                                End If
                            Next
                        Else
                            PuntosServicios = New List(Of PtoServicio)
                        End If

                    Else
                        Maquinas = New Dictionary(Of String, String)
                        PuntosServicios = New List(Of PtoServicio)
                    End If

                    ' Planificaciones
                    If ds.Tables.Contains("planificacion") AndAlso ds.Tables("planificacion").Rows.Count > 1 Then
                        If Planificaciones Is Nothing Then Planificaciones = New Dictionary(Of String, String)
                        For Each rowPlanificacion As DataRow In ds.Tables("planificacion").Rows
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowPlanificacion("OID_PLANIFICACION"), GetType(String))) AndAlso
                                Not Planificaciones.ContainsKey(Util.AtribuirValorObj(rowPlanificacion("OID_PLANIFICACION"), GetType(String))) Then
                                Planificaciones.Add(Util.AtribuirValorObj(rowPlanificacion("OID_PLANIFICACION"), GetType(String)), Util.AtribuirValorObj(rowPlanificacion("DES_PLANIFICACION"), GetType(String)))
                            End If
                        Next
                    Else
                        Planificaciones = New Dictionary(Of String, String)
                    End If
                End If

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try

        End Sub

        Private Shared Function srecuperar_info_dinamico(Peticion As Peticion, usuario As String) As SPWrapper

            Dim SP As String = String.Format("SAPR_PPANTALLAS_{0}.srecuperar_info_dinamico", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oids_delegaciones", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$oids_canales", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$oids_clientes", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$oids_bancos", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$oids_maquinas", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$oids_tipo_plan", ProsegurDbType.Objeto_Id, Nothing, , True)

            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, If(String.IsNullOrEmpty(usuario), "PANTALLA_TRANSACCIONES", usuario), , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$rc_maquinas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "maquinas")
            spw.AgregarParam("par$rc_ptoservicios", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ptoservicios")
            spw.AgregarParam("par$rc_planificacion", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "planificacion")

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            ' Delegaciones
            If Peticion.Delegaciones IsNot Nothing AndAlso Peticion.Delegaciones.Count > 0 Then
                For Each delegacion In Peticion.Delegaciones
                    If Not String.IsNullOrWhiteSpace(delegacion) Then
                        spw.Param("par$oids_delegaciones").AgregarValorArray(delegacion)
                    End If
                Next
            End If

            ' Canales
            If Peticion.Canales IsNot Nothing AndAlso Peticion.Canales.Count > 0 Then
                For Each canal In Peticion.Canales
                    If Not String.IsNullOrWhiteSpace(canal) Then
                        spw.Param("par$oids_canales").AgregarValorArray(canal)
                    End If
                Next
            End If

            ' Clientes
            If Peticion.Clientes IsNot Nothing AndAlso Peticion.Clientes.Count > 0 Then
                For Each cliente In Peticion.Clientes
                    If Not String.IsNullOrWhiteSpace(cliente) Then
                        spw.Param("par$oids_clientes").AgregarValorArray(cliente)
                    End If
                Next
            End If

            ' Maquinas
            If Peticion.Maquinas IsNot Nothing AndAlso Peticion.Maquinas.Count > 0 Then
                For Each maquina In Peticion.Maquinas
                    If Not String.IsNullOrWhiteSpace(maquina) Then
                        spw.Param("par$oids_maquinas").AgregarValorArray(maquina)
                    End If
                Next
            End If

            ' Bancos
            If Peticion.BancosCapital IsNot Nothing AndAlso Peticion.BancosCapital.Count > 0 Then
                For Each banco In Peticion.BancosCapital
                    If Not String.IsNullOrWhiteSpace(banco) Then
                        spw.Param("par$oids_bancos").AgregarValorArray(banco)
                    End If
                Next
            End If

            ' Tipos Planificaciones
            If Peticion.TipoPlanificaciones IsNot Nothing AndAlso Peticion.TipoPlanificaciones.Count > 0 Then
                For Each tipoplan In Peticion.TipoPlanificaciones
                    If Not String.IsNullOrWhiteSpace(tipoplan) Then
                        spw.Param("par$oids_tipo_plan").AgregarValorArray(tipoplan)
                    End If
                Next
            End If

            Return spw

        End Function

    End Class

End Namespace

