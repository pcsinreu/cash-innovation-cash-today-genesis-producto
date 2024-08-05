Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio.Contractos.Genesis.Pantallas.Delegaciones
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Framework.Dicionario

Namespace Genesis.Pantallas

    Public Class DelegacionesPorFacturacion

        Public Shared Sub RecuperarInformacionesDelegacion(identificadorDelegacion As String,
                                                         ByRef PuntosServicios As List(Of DelegacionFacturacion),
                                                         Usuario As String)

            Try

                Dim ds As DataSet
                Dim spw As SPWrapper = Nothing

                spw = srecuperar_info_delegaciones(identificadorDelegacion, Usuario)
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

                If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                    ' Maquinas
                    If ds.Tables.Contains("resultado") AndAlso ds.Tables("resultado").Rows.Count > 0 Then
                        If PuntosServicios Is Nothing Then PuntosServicios = New List(Of DelegacionFacturacion)
                        For Each rowResultado As DataRow In ds.Tables("resultado").Rows
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowResultado("OID_DELEGACIONXCONFIG_FACTUR"), GetType(String))) Then

                                PuntosServicios.Add(New DelegacionFacturacion With {
                                    .OID_DELEGACIONXCONFIG_FACTUR = Util.AtribuirValorObj(rowResultado("OID_DELEGACIONXCONFIG_FACTUR"), GetType(String)),
                                    .OID_CLIENTE = Util.AtribuirValorObj(rowResultado("OID_CLIENTE"), GetType(String)),
                                    .COD_CLIENTE = Util.AtribuirValorObj(rowResultado("COD_CLIENTE"), GetType(String)),
                                    .DES_CLIENTE = Util.AtribuirValorObj(rowResultado("DES_CLIENTE"), GetType(String)),
                                    .OID_SUBCLIENTE = Util.AtribuirValorObj(rowResultado("OID_SUBCLIENTE"), GetType(String)),
                                    .COD_SUBCLIENTE = Util.AtribuirValorObj(rowResultado("COD_SUBCLIENTE"), GetType(String)),
                                    .DES_SUBCLIENTE = Util.AtribuirValorObj(rowResultado("DES_SUBCLIENTE"), GetType(String)),
                                    .OID_PTO_SERVICIO = Util.AtribuirValorObj(rowResultado("OID_PTO_SERVICIO"), GetType(String)),
                                    .COD_PTO_SERVICIO = Util.AtribuirValorObj(rowResultado("COD_PTO_SERVICIO"), GetType(String)),
                                    .DES_PTO_SERVICIO = Util.AtribuirValorObj(rowResultado("DES_PTO_SERVICIO"), GetType(String))
                                })

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

        Private Shared Function srecuperar_info_delegaciones(oid_delegacion As String, usuario As String) As SPWrapper

            Dim SP As String = String.Format("SAPR_PPANTALLAS_{0}.srecuperar_info_delegaciones", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_delegacion", ProsegurDbType.Objeto_Id, oid_delegacion, , False)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, If(String.IsNullOrEmpty(usuario), "PANTALLA_TRANSACCIONES", usuario), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)

            spw.AgregarParam("par$rc_resultado", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "resultado")

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            Return spw

        End Function

    End Class

End Namespace

