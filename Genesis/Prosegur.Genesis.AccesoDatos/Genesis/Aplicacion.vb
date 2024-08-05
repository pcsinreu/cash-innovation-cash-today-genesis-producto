Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper

Namespace Genesis
    Public Class Aplicacion
        Public Shared Function ObtenerAplicaciones(unaAplicacion As Clases.Aplicacion) As List(Of Clases.Aplicacion)

            Dim respuesta As List(Of Clases.Aplicacion)

            Try
                Dim spw As SPWrapper = armarWrapperObtenerAplicacines(unaAplicacion)
                Dim ds As DataSet = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

                respuesta = PoblarRespuestaObtenerAplicaciones(ds)

            Catch ex As Exception
                Throw ex
            End Try
            Return respuesta
        End Function

        Private Shared Function armarWrapperObtenerAplicacines(unaAplicacion As Clases.Aplicacion) As SPWrapper
            Dim SP As String = String.Format("SAPR_PUSUARIO_{0}.srecuperar_aplicaciones", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$cod_aplicacion", ProsegurDbType.Identificador_Alfanumerico, unaAplicacion.Codigo, , False)
            spw.AgregarParam("par$des_aplicacion", ProsegurDbType.Identificador_Alfanumerico, unaAplicacion.Descripcion, , False)
            If unaAplicacion.Activo Then
                spw.AgregarParam("par$bol_activo", ProsegurDbType.Inteiro_Curto, 1, , False)
            Else
                spw.AgregarParam("par$bol_activo", ProsegurDbType.Inteiro_Curto, 0, , False)
            End If
            spw.AgregarParam("par$rc_aplicaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "aplicaciones")


            Return spw
        End Function

        Private Shared Function PoblarRespuestaObtenerAplicaciones(ds As DataSet) As List(Of Clases.Aplicacion)
            Dim respuesta As New List(Of Clases.Aplicacion)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                If ds.Tables.Contains("aplicaciones") Then
                    Dim dt As DataTable = ds.Tables("aplicaciones")
                    For Each fila As DataRow In dt.Rows
                        Dim aplicacion As New Clases.Aplicacion With {
                            .Identificador = Util.AtribuirValorObj(fila("OID_APLICACION"), GetType(String)),
                            .Codigo = Util.AtribuirValorObj(fila("COD_APLICACION"), GetType(String)),
                            .Descripcion = Util.AtribuirValorObj(fila("DES_APLICACION"), GetType(String)),
                            .Activo = Util.AtribuirValorObj(fila("BOL_ACTIVO"), GetType(Boolean))
                        }

                        respuesta.Add(aplicacion)
                    Next

                End If
            End If

            Return respuesta
        End Function

    End Class
End Namespace
