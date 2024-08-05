Imports Prosegur.Genesis.DataBaseHelper
Imports System.Web

Public Class ModeloDD
    Public Const SP_Accion = "gepr_pmod_modelo_DD_###VERSION###.sobtener_accion"
    Public Const Conexion_Genesis = "GENESIS"

    Public Shared Function ProcesarAccion(acciones As Dictionary(Of String, String)) As DataSet
        Dim res As String = ""
        Dim req As HttpRequest = Nothing
        Dim ds2 As DataSet = New DataSet()

        If acciones.Count > 0 Then
            'Dim ds As DataSet = ObtenerDefinicionAccion(accion)
            Dim ds As DataSet = ObtenerDefinicionAccion(acciones.Item("Accion"))
            'If ds IsNot Nothing AndAlso ds.Tables.Count = 4 Then
            If ds IsNot Nothing AndAlso ds.Tables.Count = 7 Then
                Dim tAccion As DataTable = ds.Tables(0)
                Dim tScripts As DataTable = ds.Tables(1)
                Dim tXsls As DataTable = ds.Tables(2)
                Dim tParams As DataTable = ds.Tables(3)
                Dim tHtmlJs As DataTable = ds.Tables(4)
                Dim tFiltros As DataTable = ds.Tables(5)
                Dim tResultados As DataTable = ds.Tables(6)

                Dim dthtml As DataTable = New DataTable("HTML")
                dthtml.Columns.Add("innerHtml", GetType(String))


                If tAccion.Rows.Count > 0 Then

                    Dim modo = tAccion(0).Item("MODO")

                    ds2 = EjecutarSctips(tScripts, tParams, acciones)

                    If modo = 1 Then

                        res = ProcesarXSLs(ds2, tXsls)

                        dthtml.Rows.Add(res)

                        ds2.Tables.Add(dthtml)
                    Else
                        ds2.Tables.Add(tHtmlJs.Copy)
                        ds2.Tables.Add(tFiltros.Copy)
                        ds2.Tables.Add(tResultados.Copy)
                    End If

                End If

            Else
                Throw New Exception("No se pudo recuperar la definición de la acción")
            End If
        End If

        Return ds2

    End Function


    Public Shared Function ProcesarAccion(accion As String) As DataSet


        Dim data = accion.Split(New Char() {"&"}).Select(Function(x) x.Split(New Char() {"="})).ToDictionary(Function(p) p(0), Function(q) q(1))


        Dim req As HttpRequest = Nothing
        Dim ds2 As DataSet = New DataSet()

        If accion <> "" Then
            'Dim ds As DataSet = ObtenerDefinicionAccion(accion)
            Dim ds As DataSet = ObtenerDefinicionAccion(data.Item("Accion"))
            If ds IsNot Nothing AndAlso ds.Tables.Count = 4 Then
                Dim tAccion As DataTable = ds.Tables(0)
                Dim tScripts As DataTable = ds.Tables(1)
                Dim tXsls As DataTable = ds.Tables(2)
                Dim tParams As DataTable = ds.Tables(3)
                If tAccion.Rows.Count > 0 Then
                    accion = CStr(tAccion.Rows(0).Item("COD_ACCION"))

                    ds2 = EjecutarSctips(tScripts, tParams, data)
                    ds2.DataSetName = accion


                End If

            Else
                Throw New Exception("No se pudo recuperar la definición de la acción")
            End If
        End If

        Return ds2
    End Function

    Public Shared Function ObtenerDefinicionAccion(Accion As String) As DataSet
        Dim ds As DataSet = Nothing
        Dim spw As New SPWrapper(SPaccion)

        spw.AgregarParam(New ParamWrapper("par$cod_accion", ParamWrapper.ParamTypes.String, Accion))
        spw.AgregarParam(New ParamWrapper("par$rc_accion", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "Accion"))
        spw.AgregarParam(New ParamWrapper("par$rc_scripts", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "Scripts"))
        spw.AgregarParam(New ParamWrapper("par$rc_xsls", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "Xsls"))
        spw.AgregarParam(New ParamWrapper("par$rc_parametros", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "Parametros"))
        spw.AgregarParam(New ParamWrapper("par$rc_htmljs", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "HtmlJs"))
        spw.AgregarParam(New ParamWrapper("par$rc_filtro", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "PathFiltros"))
        spw.AgregarParam(New ParamWrapper("par$rc_resultados", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "Resultados"))


        ds = AccesoDB.EjecutarSP(spw, Conexion_Genesis, False)

        Return ds
    End Function

    Public Shared Function SPaccion() As String
        Dim sp As String = SP_Accion.Replace("###VERSION###", Prosegur.Genesis.Comon.Util.Version)
        Return sp
    End Function


    'Private Shared Function EjecutarSctips(tScripts As DataTable, tParams As DataTable, req As HttpRequest) As DataSet
    Private Shared Function EjecutarSctips(tScripts As DataTable, tParams As DataTable, req As Dictionary(Of String, String)) As DataSet
        Dim lSPw As List(Of SPWapperConn) = PrepararScripts(tScripts, tParams, req)
        Dim ds As DataSet = Nothing

        If lSPw.Count > 0 Then
            Try
                'TODO: implementar transacciones
                'TODO: acumular tablas de resultado - por ahora sirve solo para un script, porque se queda con las del último solamente

                For Each s As SPWapperConn In lSPw
                    ds = AccesoDB.EjecutarSP(s.spw, s.con, False)
                Next

            Catch ex As Exception
                Throw New Exception("Se produjo un error al ejecutar los scipts", ex)
            End Try
        End If

        Return ds
    End Function

    'Public Shared Function PrepararScripts(tScripts As DataTable, tParams As DataTable, req As HttpRequest) As List(Of SPWapperConn)
    Public Shared Function PrepararScripts(tScripts As DataTable, tParams As DataTable, req As Dictionary(Of String, String)) As List(Of SPWapperConn)
        Dim lSPw As New List(Of SPWapperConn)
        For Each r As DataRow In tScripts.Rows
            Dim script As String = CStr(r.Item("OBS_SCRIPT"))
            Dim iScript As String = CStr(r.Item("NEC_SCRIPT"))
            Dim conexion As String = CStr(r.Item("COD_CONEXION"))
            Dim spw As New SPWrapper(script.Replace("###VERSION###", Prosegur.Genesis.Comon.Util.Version))

            Dim p() As DataRow = tParams.Select("NEC_SCRIPT = '" & iScript & "'")
            If p IsNot Nothing AndAlso p.Count > 0 Then
                Try
                    For Each pp In p
                        Dim ParamSP As String = CStr(pp.Item("COD_PARAMETRO_SCRIPT"))
                        Dim tipo As String = CStr(pp.Item("COD_TIPO_DATO"))
                        Dim ElemReq As String = ""
                        If Not IsDBNull(pp.Item("COD_ELEMENTO_REQUEST")) Then
                            ElemReq = CStr(pp.Item("COD_ELEMENTO_REQUEST"))
                        End If
                        Dim MetReq As String = ""
                        If Not IsDBNull(pp.Item("COD_METODO_REQUEST")) Then
                            MetReq = CStr(pp.Item("COD_METODO_REQUEST"))
                        End If

                        Dim valor As String = ""
                        If req IsNot Nothing AndAlso req.ContainsKey(ElemReq) = True Then
                            valor = req.Item(ElemReq)
                        End If
                        Dim pw As ParamWrapper = Nothing
                        Select Case tipo.ToLower
                            Case "integer"
                                pw = New ParamWrapper(ParamSP, ParamWrapper.ParamTypes.Integer, If(valor Is Nothing OrElse valor = String.Empty, DBNull.Value, CInt(valor)))
                            Case "long"
                                pw = New ParamWrapper(ParamSP, ParamWrapper.ParamTypes.Long, Int64.Parse(valor))
                            Case "decimal"
                                pw = New ParamWrapper(ParamSP, ParamWrapper.ParamTypes.Decimal, CDbl(valor))
                            Case "boolean"
                                pw = New ParamWrapper(ParamSP, ParamWrapper.ParamTypes.Boolean, CBool(valor))
                            Case "string"
                                pw = New ParamWrapper(ParamSP, ParamWrapper.ParamTypes.String, CStr(valor))
                            Case "date"
                                pw = New ParamWrapper(ParamSP, ParamWrapper.ParamTypes.Date, Date.Parse(valor))
                            Case "timestamp"
                                pw = New ParamWrapper(ParamSP, ParamWrapper.ParamTypes.Timestamp, If(valor Is Nothing OrElse valor = String.Empty, DBNull.Value, DateTime.Parse(valor)))
                            Case "refcursor"
                                pw = New ParamWrapper(ParamSP, ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, ElemReq)
                        End Select
                        spw.AgregarParam(pw)
                    Next

                Catch ex As Exception
                    Throw New Exception("Se produjo un error al preparar los parámetros", ex)
                End Try

            End If

            lSPw.Add(New SPWapperConn(spw, conexion))

        Next

        Return lSPw
    End Function

    Private Shared Function ProcesarXSLs(ResultadosScripts As DataSet, tXsls As DataTable) As String
        Dim sXML As String = XML.DSaXML(ResultadosScripts)
        Dim ArchXSL As String = ""
        Dim PathRelativo As String = ""
        Try
            For Each r As DataRow In tXsls.Rows
                ArchXSL = CStr(r.Item("DES_NOMBRE"))
                PathRelativo = CStr(r.Item("DES_PATH_RELATIVO"))
                sXML = XML.Transformar(sXML, PathRelativo, ArchXSL)
            Next

        Catch ex As Exception
            Throw New Exception("Se produjo un error al procesar las transformaciones (" & ArchXSL & ")", ex)
        End Try

        Return sXML
    End Function

End Class


Public Class SPWapperConn
    Public spw As SPWrapper
    Public con As String
    Public Sub New(_SPw As SPWrapper, _con As String)
        spw = _SPw
        con = _con
    End Sub
End Class

