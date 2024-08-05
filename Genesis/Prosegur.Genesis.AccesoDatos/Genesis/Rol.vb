Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio.Contractos.Permisos
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper

Public Class Rol
    Public Shared Function ObtenerRoles(unRol As Clases.Rol, modoDetallado As Boolean) As List(Of Clases.Rol)

        Dim respuesta As List(Of Clases.Rol)

        Try
            Dim spw As SPWrapper = ArmarWrapperObtenerRoles(unRol, modoDetallado)
            Dim ds As DataSet = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

            respuesta = PoblarRespuestaObtenerRoles(ds, modoDetallado)

        Catch ex As Exception
            Throw ex
        End Try
        Return respuesta
    End Function

    Public Shared Function Grabar(unaPeticion As PeticionGrabarRol) As RespuestaGrabarRol
        Dim respuesta As RespuestaGrabarRol

        Try
            Dim spw As SPWrapper = ArmarWrapperGrabarRol(unaPeticion)
            Dim ds As DataSet = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

            respuesta = PoblarRespuestaGrabarRol(ds)

        Catch ex As Exception
            Throw ex
        End Try

        Return respuesta
    End Function

    Private Shared Function PoblarRespuestaGrabarRol(ds As DataSet) As RespuestaGrabarRol
        Dim objRespuesta As New RespuestaGrabarRol
        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            If ds.Tables.Contains("validaciones") Then
                If ds.Tables("validaciones").Rows.Count > 0 Then

                    objRespuesta.Codigo = String.Empty

                    If ds.Tables("validaciones").Rows.Count = 1 Then
                        objRespuesta.Codigo = Util.AtribuirValorObj(ds.Tables("validaciones").Rows(0)("CODIGO"), GetType(String))
                        objRespuesta.Descripcion = Util.AtribuirValorObj(ds.Tables("validaciones").Rows(0)("DESCRIPCION"), GetType(String))
                    Else
                        Dim strBuilder As New StringBuilder
                        For Each fila As DataRow In ds.Tables("validaciones").Rows
                            If Util.AtribuirValorObj(fila("TIPO_MENSAJE"), GetType(String)) <> Enumeradores.Mensajes.Tipo.Exito Then
                                strBuilder.AppendLine(Util.AtribuirValorObj(fila("DESCRIPCION"), GetType(String)))
                            End If
                        Next
                        objRespuesta.Descripcion = strBuilder.ToString()
                    End If

                End If
            End If
        End If

        Return objRespuesta
    End Function

    Private Shared Function ArmarWrapperGrabarRol(unaPeticion As Prosegur.Genesis.ContractoServicio.Contractos.Permisos.PeticionGrabarRol) As SPWrapper
        Dim SP As String = String.Format("SAPR_PUSUARIO_{0}.sconfigurar_role", Prosegur.Genesis.Comon.Util.Version)
        Dim spw As New SPWrapper(SP, False)

        spw.AgregarParam("par$cod_accion", ProsegurDbType.Descricao_Curta, unaPeticion.AccionGrabar, , False)
        spw.AgregarParam("par$cod_role", ProsegurDbType.Descricao_Curta, unaPeticion.Rol.Codigo, , False)
        spw.AgregarParam("par$des_role", ProsegurDbType.Descricao_Longa, unaPeticion.Rol.Descripcion, , False)


        If unaPeticion.Rol.Activo Then
            spw.AgregarParam("par$bol_activo", ProsegurDbType.Inteiro_Curto, 1, , False)
        Else
            spw.AgregarParam("par$bol_activo", ProsegurDbType.Inteiro_Curto, 0, , False)
        End If


        spw.AgregarParam("par$aoid_permiso", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

        For Each objPermiso In unaPeticion.Rol.Permisos
            spw.Param("par$aoid_permiso").AgregarValorArray(objPermiso.Identificador)
        Next

        spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Longa, unaPeticion.CodigoUsuario, , False)
        spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, Util.GetCultureUser())

        spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")

        Return spw
    End Function

    Private Shared Function ArmarWrapperObtenerRoles(unRol As Clases.Rol, modoDetallado As Boolean) As SPWrapper
        Dim SP As String = String.Format("SAPR_PUSUARIO_{0}.srecuperar_role", Prosegur.Genesis.Comon.Util.Version)
        Dim spw As New SPWrapper(SP, False)

        spw.AgregarParam("par$cod_role", ProsegurDbType.Identificador_Alfanumerico, unRol.Codigo, , False)

        If unRol.Aplicacion IsNot Nothing Then
            spw.AgregarParam("par$oid_aplicacion", ProsegurDbType.Identificador_Alfanumerico, unRol.Aplicacion.Identificador, , False)
        Else
            spw.AgregarParam("par$oid_aplicacion", ProsegurDbType.Identificador_Alfanumerico, String.Empty, , False)
        End If

        If unRol.Activo Then
            spw.AgregarParam("par$bol_activo", ProsegurDbType.Inteiro_Curto, 1, , False)
        Else
            spw.AgregarParam("par$bol_activo", ProsegurDbType.Inteiro_Curto, 0, , False)
        End If
        If modoDetallado Then
            spw.AgregarParam("par$modo_detallado", ProsegurDbType.Inteiro_Curto, 1, , False)
        Else
            spw.AgregarParam("par$modo_detallado", ProsegurDbType.Inteiro_Curto, 0, , False)
        End If

        spw.AgregarParam("par$rc_roles", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "roles")
        spw.AgregarParam("par$rc_permisos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "permisos")

        Return spw
    End Function

    Private Shared Function PoblarRespuestaObtenerRoles(ds As DataSet, modoDetallado As Boolean) As List(Of Clases.Rol)
        Dim respuesta As New List(Of Clases.Rol)

        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

            If Not modoDetallado Then
                If ds.Tables.Contains("roles") Then
                    Dim dt As DataTable = ds.Tables("roles")
                    For Each fila As DataRow In dt.Rows
                        Dim rol As New Clases.Rol With {
                                .Identificador = Util.AtribuirValorObj(fila("OID_ROLE"), GetType(String)),
                                .Codigo = Util.AtribuirValorObj(fila("COD_ROLE"), GetType(String)),
                                .Descripcion = Util.AtribuirValorObj(fila("DES_ROLE"), GetType(String)),
                                .Activo = Util.AtribuirValorObj(fila("BOL_ACTIVO"), GetType(Boolean)),
                                .Aplicacion = New Clases.Aplicacion With {
                                    .Identificador = Util.AtribuirValorObj(fila("OID_APLICACION"), GetType(String)),
                                    .Codigo = Util.AtribuirValorObj(fila("COD_APLICACION"), GetType(String)),
                                    .Descripcion = Util.AtribuirValorObj(fila("DES_APLICACION"), GetType(String))
                                }
                        }
                        respuesta.Add(rol)
                    Next
                End If
            Else
                If ds.Tables.Contains("roles") Then
                    Dim dt As DataTable = ds.Tables("roles")
                    For Each fila As DataRow In dt.Rows
                        Dim rol As New Clases.Rol With {
                                .Identificador = Util.AtribuirValorObj(fila("OID_ROLE"), GetType(String)),
                                .Codigo = Util.AtribuirValorObj(fila("COD_ROLE"), GetType(String)),
                                .Descripcion = Util.AtribuirValorObj(fila("DES_ROLE"), GetType(String)),
                                .Activo = Util.AtribuirValorObj(fila("BOL_ACTIVO"), GetType(Boolean)),
                                .Permisos = New List(Of Clases.Permiso)
                        }

                        If ds.Tables.Contains("permisos") Then
                            'Filtro las filas en base al rol que se está recorriendo
                            For Each filaPermisos As DataRow In ds.Tables("permisos").Select($"OID_ROLE = '{rol.Identificador}'")
                                Dim permiso = New Clases.Permiso With {
                                        .Identificador = Util.AtribuirValorObj(filaPermisos("OID_PERMISO"), GetType(String)),
                                        .Codigo = Util.AtribuirValorObj(filaPermisos("COD_PERMISO"), GetType(String)),
                                        .Descripcion = Util.AtribuirValorObj(filaPermisos("DES_PERMISO"), GetType(String)),
                                        .Aplicacion = New Clases.Aplicacion With {
                                                .Identificador = Util.AtribuirValorObj(filaPermisos("OID_APLICACION"), GetType(String)),
                                                .Codigo = Util.AtribuirValorObj(filaPermisos("COD_APLICACION"), GetType(String)),
                                                .Descripcion = Util.AtribuirValorObj(filaPermisos("DES_APLICACION"), GetType(String))
                                        }
                                }
                                rol.Permisos.Add(permiso)
                            Next
                        End If

                        respuesta.Add(rol)
                    Next
                End If
            End If

        End If

        Return respuesta
    End Function

End Class
