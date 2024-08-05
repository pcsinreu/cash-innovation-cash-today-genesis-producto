Imports System.Data.Entity
Imports System.Linq
Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Paginacion
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Contractos.Permisos
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper

''' <summary>
''' Classe de Acesso a dados Sector.
''' </summary>
''' <history>
''' [Thiago Dias] 04/09/2013 - Criado.
'''</history>
Public Class Usuario

    ''' <summary>
    ''' Busca por informações de Sector.
    ''' </summary>
    Public Shared Function PesquisarUsuario(peticion As PeticionHelper, ByRef paramRespuestaPaginacion As ParametrosRespuestaPaginacion) As List(Of Helper.Respuesta)

        Dim query As New StringBuilder
        Dim dtResultado As DataTable

        ' Obtem Query.
        query.Append(My.Resources.BuscaUsuario.ToString())

        ' Realiza Pesquisa.
        dtResultado = HelperBuscaDatos.BuscaDatos(query, peticion, paramRespuestaPaginacion, "DES_LOGIN", "DES_NOMBRE", "DES_LOGIN")

        ' Retorna lista contendo dados de Respuesta Cliente.
        Return HelperBuscaDatos.ListaDatosRespuesta(dtResultado)

    End Function

    Public Shared Function ObtenerUsuarios(peticion As Contractos.Permisos.PeticionRecuperarUsuario, modoDetallado As Boolean) As List(Of Contractos.Permisos.RespuestaRecuperarUsuario)

        Dim respuesta As List(Of Contractos.Permisos.RespuestaRecuperarUsuario)

        Try
            Dim spw As SPWrapper = ArmarWrapperObtenerUsuarios(peticion, modoDetallado)
            Dim ds As DataSet = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

            respuesta = PoblarRespuestaObtenerUsuario(ds, modoDetallado)

        Catch ex As Exception
            Throw ex
        End Try
        Return respuesta

    End Function

    Private Shared Function ArmarWrapperObtenerUsuarios(peticion As Contractos.Permisos.PeticionRecuperarUsuario, modoDetallado As Boolean) As SPWrapper
        Dim SP As String = String.Format("SAPR_PUSUARIO_{0}.srecuperar_usuario", Prosegur.Genesis.Comon.Util.Version)
        Dim spw As New SPWrapper(SP, False)

        spw.AgregarParam("par$cod_pais", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoPais, , False)
        spw.AgregarParam("par$cod_role", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoRole, , False)
        spw.AgregarParam("par$des_login", ProsegurDbType.Identificador_Alfanumerico, peticion.DesLogin, , False)

        If modoDetallado Then
            spw.AgregarParam("par$modo_detallado", ProsegurDbType.Inteiro_Curto, 1, , False)
        Else
            spw.AgregarParam("par$modo_detallado", ProsegurDbType.Inteiro_Curto, 0, , False)
        End If

        spw.AgregarParam("par$rc_usuarios", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "usuarios")

        Return spw
    End Function

    Private Shared Function PoblarRespuestaObtenerUsuario(ds As DataSet, modoDetallado As Boolean) As List(Of Contractos.Permisos.RespuestaRecuperarUsuario)
        Dim respuesta As New List(Of Contractos.Permisos.RespuestaRecuperarUsuario)()

        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            If ds.Tables.Contains("usuarios") Then
                Dim dt As DataTable = ds.Tables("usuarios")
                For Each fila As DataRow In dt.Rows
                    Dim usuario As New RespuestaRecuperarUsuario With {
                        .Identificador = Util.AtribuirValorObj(fila("OID_USUARIO"), GetType(String)),
                        .DesLogin = Util.AtribuirValorObj(fila("DES_LOGIN"), GetType(String)),
                        .Nombre = Util.AtribuirValorObj(fila("DES_NOMBRE"), GetType(String)),
                        .Apellido = Util.AtribuirValorObj(fila("DES_APELLIDO"), GetType(String)),
                        .IdiomaPorDefecto = Util.AtribuirValorObj(fila("DES_IDIOMA_PREFERIDO"), GetType(String)),
                        .Activo = Util.AtribuirValorObj(fila("BOL_ACTIVO_USUARIO"), GetType(Boolean)),
                        .Roles = New List(Of Clases.Rol)(),
                        .RoleXUsuario = New List(Of Clases.RoleXUsuario),
                        .Pais = New Clases.Pais() With {
                            .Identificador = Util.AtribuirValorObj(fila("OID_PAIS"), GetType(String)),
                            .Codigo = Util.AtribuirValorObj(fila("COD_PAIS"), GetType(String)),
                            .Descripcion = Util.AtribuirValorObj(fila("DES_PAIS"), GetType(String))
                        }
                    }

                    Dim role = New Clases.Rol With {
                            .Identificador = Util.AtribuirValorObj(fila("OID_ROLE"), GetType(String)),
                            .Codigo = Util.AtribuirValorObj(fila("COD_ROLE"), GetType(String)),
                            .Descripcion = Util.AtribuirValorObj(fila("DES_ROLE"), GetType(String))
                        }

                    If modoDetallado Then
                        role.Aplicacion = New Clases.Aplicacion With {
                        .Identificador = Util.AtribuirValorObj(fila("OID_APLICACION"), GetType(String)),
                        .Codigo = Util.AtribuirValorObj(fila("COD_APLICACION"), GetType(String)),
                        .Descripcion = Util.AtribuirValorObj(fila("DES_APLICACION"), GetType(String)),
                        .Activo = Util.AtribuirValorObj(fila("BOL_ACTIVO_APLICACION"), GetType(Boolean))
                        }
                    End If

                    'Buscamos si el usuario ya existe para ese Pais
                    Dim usuarioExistente = respuesta.FirstOrDefault(Function(x) x.DesLogin = usuario.DesLogin AndAlso x.Pais.Codigo = usuario.Pais.Codigo)
                    If (usuarioExistente Is Nothing) Then
                        usuario.Roles.Add(role)
                        respuesta.Add(usuario)
                    Else
                        usuarioExistente.Roles.Add(role)
                    End If

                    If modoDetallado Then
                        Dim roleXUsuario = New Clases.RoleXUsuario With {
                            .Identificador = Util.AtribuirValorObj(fila("OID_ROLEXUSUARIO"), GetType(String)),
                            .Role = role,
                            .Pais = usuario.Pais,
                            .Activo = Util.AtribuirValorObj(fila("BOL_ACTIVO_ROLEXUSR"), GetType(Boolean))
                        }

                        'Poblamos property RoleXUsuario
                        Dim buscaUsuario = respuesta.FirstOrDefault(Function(x) x.DesLogin = usuario.DesLogin).RoleXUsuario
                        If buscaUsuario IsNot Nothing AndAlso buscaUsuario.Count > 0 Then
                            Dim rolXUsuarioExistente = buscaUsuario.FirstOrDefault(Function(x) x.Role.Codigo = roleXUsuario.Role.Codigo AndAlso x.Pais.Codigo = roleXUsuario.Pais.Codigo)
                            If rolXUsuarioExistente Is Nothing Then
                                buscaUsuario.Add(roleXUsuario)
                            End If
                        Else
                            usuario.RoleXUsuario.Add(roleXUsuario)
                        End If
                    End If
                Next
            End If
        End If

        Return respuesta
    End Function

    Public Shared Sub GrabarUsuario(ByRef peticion As PeticionGrabarUsuario, ByRef respuesta As RespuestaGrabarUsuario)
        Try
            Dim spw As SPWrapper = ArmarWrapperGrabarUsuarios(peticion)
            Dim ds As DataSet = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
            respuesta = PoblarRespuestaGrabarUsuario(ds)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Shared Function PoblarRespuestaGrabarUsuario(ds As DataSet) As RespuestaGrabarUsuario
        Dim objRespuesta As RespuestaGrabarUsuario = New RespuestaGrabarUsuario
        If ds IsNot Nothing Then
            If ds.Tables("validaciones") IsNot Nothing AndAlso ds.Tables("validaciones").Rows.Count > 0 Then
                If ds.Tables("validaciones").Rows.Count = 1 Then
                    objRespuesta.Codigo = Util.AtribuirValorObj(ds.Tables("validaciones").Rows(0)("Codigo"), GetType(String))
                    objRespuesta.Descripcion = Util.AtribuirValorObj(ds.Tables("validaciones").Rows(0)("Descripcion"), GetType(String))
                Else
                    For Each fila As DataRow In ds.Tables("validaciones").Rows
                        objRespuesta.Codigo = objRespuesta.Codigo + vbNewLine + Util.AtribuirValorObj(ds.Tables("validaciones").Rows(0)("Codigo"), GetType(String))
                        objRespuesta.Descripcion = objRespuesta.Descripcion + vbNewLine + Util.AtribuirValorObj(ds.Tables("validaciones").Rows(0)("Descripcion"), GetType(String))
                    Next
                End If

            End If
        End If

        Return objRespuesta
    End Function

    Private Shared Function ArmarWrapperGrabarUsuarios(peticion As PeticionGrabarUsuario) As SPWrapper
        Dim SP As String = String.Format("SAPR_PUSUARIO_{0}.sconfigurar_usuario", Prosegur.Genesis.Comon.Util.Version)
        Dim spw As New SPWrapper(SP, False)

        spw.AgregarParam("par$cod_accion", ProsegurDbType.Identificador_Alfanumerico, peticion.Accion, , False)
        spw.AgregarParam("par$aoid_role", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$aoid_usuario", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$ades_login", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$ades_nombre", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$ades_apellido", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$ades_idioma", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$aoid_pais", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$abol_activo", ProsegurDbType.Inteiro_Curto, Nothing, , True)

        For Each elemento_usuario In peticion.Usuarios
            If elemento_usuario.RoleXUsuario IsNot Nothing AndAlso elemento_usuario.RoleXUsuario.Count > 0 Then
                For Each elemento_role In elemento_usuario.RoleXUsuario
                    spw.Param("par$aoid_role").AgregarValorArray(elemento_role.Role.Identificador)
                    spw.Param("par$aoid_usuario").AgregarValorArray(elemento_usuario.Identificador)
                    spw.Param("par$ades_login").AgregarValorArray(elemento_usuario.DesLogin)
                    spw.Param("par$ades_nombre").AgregarValorArray(elemento_usuario.Nombre)
                    spw.Param("par$ades_apellido").AgregarValorArray(elemento_usuario.Apellido)
                    spw.Param("par$ades_idioma").AgregarValorArray(elemento_usuario.IdiomaPorDefecto)
                    spw.Param("par$aoid_pais").AgregarValorArray(elemento_role.Pais.Identificador)
                    spw.Param("par$abol_activo").AgregarValorArray(elemento_usuario.Activo)
                Next
            Else
                spw.Param("par$aoid_role").AgregarValorArray(String.Empty)
                spw.Param("par$aoid_usuario").AgregarValorArray(elemento_usuario.Identificador)
                spw.Param("par$ades_login").AgregarValorArray(elemento_usuario.DesLogin)
                spw.Param("par$ades_nombre").AgregarValorArray(elemento_usuario.Nombre)
                spw.Param("par$ades_apellido").AgregarValorArray(elemento_usuario.Apellido)
                spw.Param("par$ades_idioma").AgregarValorArray(elemento_usuario.IdiomaPorDefecto)
                spw.Param("par$aoid_pais").AgregarValorArray(String.Empty)
                spw.Param("par$abol_activo").AgregarValorArray(elemento_usuario.Activo)
            End If

        Next

        spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Curta, peticion.CodigoUsuario, , False)
        spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Curta, Util.GetCultureUser())

        spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")

        Return spw
    End Function
End Class
