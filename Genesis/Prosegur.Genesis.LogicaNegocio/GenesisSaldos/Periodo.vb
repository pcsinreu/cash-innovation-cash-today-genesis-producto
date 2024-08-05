Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports Prosegur.Genesis.Comunicacion
Imports System.Data

Namespace Integracion
    Public Class Periodo
        Public Shared Sub obtenerDelegAndPlantaDeUnaMAE(deviceID As String, ds As DataSet, ByRef codigoDeleg As String, ByRef codigoPlanta As String)
            If ds IsNot Nothing AndAlso ds.Tables IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables.Contains("maquinas") Then
                Dim dt As DataTable = ds.Tables("maquinas")
                Dim fila As DataRow() = dt.Select(String.Format("cod_sector = '{0}'", deviceID))

                codigoDeleg = Util.AtribuirValorObj(fila(0)("COD_DELEGACION"), GetType(String))
                codigoPlanta = Util.AtribuirValorObj(fila(0)("COD_PLANTA"), GetType(String))
            End If
        End Sub

        Public Shared Sub RecuperarEstadosPeriodos(ByRef pEstadosPeriodos As Dictionary(Of String, String))
            pEstadosPeriodos = AccesoDatos.GenesisSaldos.Periodos.RecuperarEstadosPeriodos()
        End Sub

        Public Shared Sub GenerarPeriodos(identificadorMaquina As String,
                                              codigoUsuario As String,
                                            Optional identificadorLlamada As String = Nothing)

            Dim objTransaccionOracle As DataBaseHelper.Transaccion = New DataBaseHelper.Transaccion()
            'Ponemos en una cola de transaccion de Oracle.
            objTransaccionOracle.IniciarTransaccion = True

            Try
                'CREAR PERIODOS
                AccesoDatos.GenesisSaldos.Periodos.generarPeriodos(identificadorMaquina, codigoUsuario, identificadorLlamada)
            Catch ex As Exception
                DataBaseHelper.AccesoDB.TransactionRollback(objTransaccionOracle)
            End Try
        End Sub

        Public Shared Sub RelacionarDocumentosMAE(deviceID As String,
                                                fechaGestion As Date,
                                                Optional identificadorLlamada As String = Nothing)

            Dim objTransaccionOracle As DataBaseHelper.Transaccion = New DataBaseHelper.Transaccion()
            'Ponemos en una cola de transaccion de Oracle.
            objTransaccionOracle.IniciarTransaccion = True

            Try
                'CREAR PERIODOS
                AccesoDatos.GenesisSaldos.Periodos.RelacionarDocumentosMAE(deviceID, fechaGestion, identificadorLlamada)
            Catch ex As Exception
                DataBaseHelper.AccesoDB.TransactionRollback(objTransaccionOracle)
            End Try
        End Sub
    End Class
End Namespace

