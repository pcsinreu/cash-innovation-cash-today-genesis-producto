Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Public Class Permiso
    Public Shared Function ObtenerPermisos(unPermiso As Clases.Permiso) As List(Of Clases.Permiso)

        Dim respuesta As List(Of Clases.Permiso)

        Try
            Dim spw As SPWrapper = armarWrapperObtenerPermisos(unPermiso)
            Dim ds As DataSet = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

            respuesta = PoblarRespuestaObtenerPermisos(ds)

        Catch ex As Exception
            Throw ex
        End Try
        Return respuesta
    End Function

    Private Shared Function armarWrapperObtenerPermisos(unPermiso As Clases.Permiso) As SPWrapper
        Dim SP As String = String.Format("SAPR_PUSUARIO_{0}.srecuperar_permisos", Prosegur.Genesis.Comon.Util.Version)
        Dim spw As New SPWrapper(SP, False)

        If unPermiso.Aplicacion IsNot Nothing Then
            spw.AgregarParam("par$oid_aplicacion", ProsegurDbType.Identificador_Alfanumerico, unPermiso.Aplicacion.Identificador, , False)
        Else
            spw.AgregarParam("par$oid_aplicacion", ProsegurDbType.Identificador_Alfanumerico, String.Empty, , False)
        End If

        If unPermiso.Activo Then
            spw.AgregarParam("par$bol_activo", ProsegurDbType.Inteiro_Curto, 1, , False)
        Else
            spw.AgregarParam("par$bol_activo", ProsegurDbType.Inteiro_Curto, 0, , False)
        End If
        spw.AgregarParam("par$rc_permisos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "permisos")

        Return spw
    End Function

    Private Shared Function PoblarRespuestaObtenerPermisos(ds As DataSet) As List(Of Clases.Permiso)
        Dim respuesta As New List(Of Clases.Permiso)

        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            If ds.Tables.Contains("permisos") Then
                Dim dt As DataTable = ds.Tables("permisos")
                For Each fila As DataRow In dt.Rows
                    Dim permiso As New Clases.Permiso With {
                            .Identificador = Util.AtribuirValorObj(fila("OID_PERMISO"), GetType(String)),
                            .Codigo = Util.AtribuirValorObj(fila("COD_PERMISO"), GetType(String)),
                            .Descripcion = Util.AtribuirValorObj(fila("DES_PERMISO"), GetType(String)),
                            .Activo = Util.AtribuirValorObj(fila("BOL_ACTIVO"), GetType(Boolean)),
                            .Aplicacion = New Clases.Aplicacion With {
                                .Identificador = Util.AtribuirValorObj(fila("OID_APLICACION"), GetType(String)),
                                .Codigo = Util.AtribuirValorObj(fila("COD_APLICACION"), GetType(String)),
                                .Descripcion = Util.AtribuirValorObj(fila("DES_APLICACION"), GetType(String))
                            }
                    }
                    respuesta.Add(permiso)
                Next

            End If
        End If

        Return respuesta
    End Function

End Class
