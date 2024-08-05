Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper

Namespace Genesis
    Public Class Limites

        Public Shared Function ObtenerLimites(oidPlanificacion As String, oidMaquina As String, oidPuntoServicio As String) As List(Of Clases.Limite)
            Dim respuesta = New List(Of Clases.Limite)

            Try
                Dim spw As SPWrapper = armarWrapperObtenerLimites(oidPlanificacion, oidMaquina, oidPuntoServicio)

                Dim ds As DataSet = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

                respuesta = PoblarRespuestaObtenerLimites(ds)

            Catch ex As Exception
                Throw ex
            End Try
            Return respuesta
        End Function

        Private Shared Function armarWrapperObtenerLimites(oidPlanificacion As String, oidMaquina As String, oidPuntoServicio As String) As SPWrapper
            Dim SP As String = String.Format("SAPR_PLIMITE_{0}.srecuperar_datos", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_planificacion", ProsegurDbType.Identificador_Alfanumerico, oidPlanificacion, , False)
            spw.AgregarParam("par$oid_maquina", ProsegurDbType.Identificador_Alfanumerico, oidMaquina, , False)
            spw.AgregarParam("par$oid_pto_servicio", ProsegurDbType.Identificador_Alfanumerico, oidPuntoServicio, , False)

            spw.AgregarParam("par$rc_datos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "datos")

            Return spw
        End Function

        Private Shared Function PoblarRespuestaObtenerLimites(ds As DataSet) As List(Of Clases.Limite)
            Dim respuesta As New List(Of Clases.Limite)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                If ds.Tables.Contains("datos") Then
                    Dim dt As DataTable = ds.Tables("datos")
                    For Each fila As DataRow In dt.Rows
                        Dim limite = New Clases.Limite With {.Divisa = New Clases.Divisa}

                        limite.Identificador = Util.AtribuirValorObj(fila("OID_LIMITE"), GetType(String))
                        limite.NumLimite = Util.AtribuirValorObj(fila("NUM_LIMITE"), GetType(Decimal))

                        limite.Divisa.Identificador = Util.AtribuirValorObj(fila("OID_DIVISA"), GetType(String))
                        limite.Divisa.CodigoISO = Util.AtribuirValorObj(fila("COD_ISO_DIVISA"), GetType(String))
                        limite.Divisa.Descripcion = Util.AtribuirValorObj(fila("DES_DIVISA"), GetType(String))
                        limite.Divisa.CodigoSimbolo = Util.AtribuirValorObj(fila("COD_SIMBOLO"), GetType(String))

                        respuesta.Add(limite)
                    Next
                End If
            End If

            Return respuesta
        End Function
    End Class
End Namespace
