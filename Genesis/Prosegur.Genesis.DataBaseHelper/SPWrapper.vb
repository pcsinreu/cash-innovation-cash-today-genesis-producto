Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Atributos
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper

Public Class SPWrapper
    Dim p_Sp As String = ""
    Dim p_Params As New List(Of ParamWrapper)
    Dim p_RegistrarEjecucionExterna As Boolean = False
    Dim p_SP_upd_Ejecucion As String
    Dim p_Param_Cod_Ejec_out As String
    Dim p_Param_Cod_Ejec_in As String
    Dim p_Param_Duracion As String
    Dim p_Param_Cod_Transac As String
    Dim p_Param_Cod_Result As String
    Dim p_SPW_Reg_transac_ext As SPWrapper = Nothing
    Dim p_NoQuery As Boolean = False
    Dim p_Tipo As System.Data.CommandType = CommandType.StoredProcedure


    Public Enum CodTransaccion
        <ValorEnum("I")>
        Interna
        <ValorEnum("E")>
        Externa
    End Enum

    Public Enum CodResultado
        <ValorEnum("S")>
        Success
        <ValorEnum("E")>
        [Error]
        <ValorEnum("C")>
        Commit
        <ValorEnum("R")>
        Rollback
    End Enum

    ''' <summary>
    ''' Versão do Assembly. Retorna um valor com 8 posições.
    ''' </summary>
    ''' <remarks></remarks>


    Public Sub New(SP As String, Optional NoQuery As Boolean = False, Optional Tipo As System.Data.CommandType = CommandType.StoredProcedure)

        Dim strVersao = Comon.Util.Version
#If DEBUG Then
        Dim objParametrosDebug As Comon.ParametrosDebug = Comon.Util.RecuperarParametrosDebug
        If objParametrosDebug IsNot Nothing AndAlso Not String.IsNullOrEmpty(objParametrosDebug.VersaoProcedure) Then

            SP = SP.Replace("_" & strVersao, "_" & objParametrosDebug.VersaoProcedure)
        End If
#End If

        If Tipo = CommandType.StoredProcedure AndAlso Not SP.Contains("supd_tlog_ejecucion_trn_ex") Then
            p_Sp = String.Format("gepr_putilidades_{0}.sinicializar_sesion;", strVersao) & SP
        Else
            p_Sp = SP
        End If

        p_NoQuery = NoQuery
        p_Tipo = Tipo

    End Sub

    Public ReadOnly Property Tipo As System.Data.CommandType
        Get
            Return p_Tipo
        End Get
    End Property

    Public Property SP As String
        Get
            Return p_Sp
        End Get

        Set(value As String)
            p_Sp = value
        End Set
    End Property

    Public ReadOnly Property NoQuery As Boolean
        Get
            Return p_NoQuery
        End Get
    End Property

    Public ReadOnly Property Params As List(Of ParamWrapper)
        Get
            Return p_Params
        End Get
    End Property

    Public Function Param(Nombre As String) As ParamWrapper
        Return p_Params.Find(Function(t) t.Nombre = Nombre)
    End Function

    Public Function AgregarParam(Param As ParamWrapper) As ParamWrapper
        If p_Params.Find(Function(t) t.Nombre = Param.Nombre) IsNot Nothing Then
            Throw New Exception("Parámetro ya existente")
        Else
            p_Params.Add(Param)
            Return Param
        End If
    End Function

    Public Function AgregarParam(Nombre As String, Tipo As ProsegurDbType, Valor As Object, Optional Direccion As ParameterDirection = ParameterDirection.Input, Optional EsArray As Boolean = False) As ParamWrapper
        If p_Params.Find(Function(t) t.Nombre = Nombre) IsNot Nothing Then
            Throw New Exception("Parámetro ya existente")
        Else
            Dim pw As New ParamWrapper(Nombre, Tipo, Valor, Direccion, EsArray) ', p_Params)
            p_Params.Add(pw)
            Return pw
        End If
    End Function

    Public Function AgregarParam(Nombre As String, Tipo As ParamTypes, Valor As Object, Optional Direccion As ParameterDirection = ParameterDirection.Input, Optional EsArray As Boolean = False, Optional NombreTabla As String = "") As ParamWrapper
        If p_Params.Find(Function(t) t.Nombre = Nombre) IsNot Nothing Then
            Throw New Exception("Parámetro ya existente")
        Else
            Dim pw As New ParamWrapper(Nombre, Tipo, Valor, Direccion, EsArray, NombreTabla) ', p_Params)
            p_Params.Add(pw)
            Return pw
        End If
    End Function


    Public Function Info() As String
        Dim s As String = ""
        s += "Stored procedure: " & p_Sp & vbCrLf
        s += "Parametros: " & p_Params.Count & vbCrLf

        Dim pArr As Integer = 0
        Dim pVals As Integer = 0
        Dim pValsA As Integer = 0

        For Each p As ParamWrapper In p_Params
            If p.EsArray Then
                pArr += 1
                pValsA += p.ListaValores.Count
            Else
                pVals += 1
            End If
        Next

        s += "Arrays asociativos: " & pArr & vbCrLf
        s += "Total valores: " & pVals + pValsA & vbCrLf

        Return s
    End Function

    Public Sub AgregarParamInfo(Nombre As String)
        Me.AgregarParam(Nombre, ParamTypes.String, Me.Info, , False)
    End Sub

    Public Sub RegistrarEjecucionExterna(SP_upd_Ejecucion As String,
                                           Param_Cod_Ejec_out As String,
                                           Param_Cod_Ejec_in As String,
                                           Param_Duracion As String,
                                           Param_Trasaccion As String,
                                           Param_Resultado As String)
        p_SP_upd_Ejecucion = SP_upd_Ejecucion
        p_Param_Cod_Ejec_out = Param_Cod_Ejec_out
        p_Param_Cod_Ejec_in = Param_Cod_Ejec_in
        p_Param_Duracion = Param_Duracion
        p_Param_Cod_Transac = Param_Trasaccion
        p_Param_Cod_Result = Param_Resultado
        If p_SP_upd_Ejecucion <> "" AndAlso p_Param_Cod_Ejec_out <> "" AndAlso p_Param_Cod_Ejec_in <> "" AndAlso p_Param_Duracion <> "" _
            AndAlso p_Param_Cod_Transac <> "" AndAlso Param_Resultado <> "" Then
            p_RegistrarEjecucionExterna = True
            p_SPW_Reg_transac_ext = New SPWrapper(p_SP_upd_Ejecucion)
            p_SPW_Reg_transac_ext.AgregarParam(p_Param_Cod_Ejec_in, ProsegurDbType.Inteiro_Longo, Nothing)
            p_SPW_Reg_transac_ext.AgregarParam(p_Param_Cod_Transac, ProsegurDbType.Identificador_Alfanumerico, Nothing)
            p_SPW_Reg_transac_ext.AgregarParam(p_Param_Duracion, ProsegurDbType.Numero_Decimal, Nothing)
            p_SPW_Reg_transac_ext.AgregarParam(p_Param_Cod_Result, ProsegurDbType.Identificador_Alfanumerico, Nothing)
        End If
    End Sub

    Public ReadOnly Property RegistraEjecucionExterna As Boolean
        Get
            Return p_RegistrarEjecucionExterna
        End Get
    End Property

    Public Sub AsignarRegistroEjecucionExterna(Duracion As Single, Transaccion As CodTransaccion, Resultado As CodResultado)
        If p_SPW_Reg_transac_ext IsNot Nothing AndAlso Param(p_Param_Cod_Ejec_out) IsNot Nothing Then
            p_SPW_Reg_transac_ext.Param(p_Param_Cod_Ejec_in).AsignarValor(Me.Param(p_Param_Cod_Ejec_out).Valor)
            p_SPW_Reg_transac_ext.Param(p_Param_Duracion).AsignarValor(Duracion)
            p_SPW_Reg_transac_ext.Param(p_Param_Cod_Transac).AsignarValor(Transaccion.RecuperarValor)
            p_SPW_Reg_transac_ext.Param(p_Param_Cod_Result).AsignarValor(Resultado.RecuperarValor)
        End If
    End Sub

    Public ReadOnly Property SpRegTransacExt As SPWrapper
        Get
            Return p_SPW_Reg_transac_ext
        End Get
    End Property

    Public ReadOnly Property ParamOutCodEjecucion As String
        Get
            Return p_Param_Cod_Ejec_out
        End Get
    End Property

    Public Sub ConservarValoresSalida(Comando As Oracle.DataAccess.Client.OracleCommand)
        For Each p As ParamWrapper In p_Params
            If Not p.EsArray Then
                If p.Direccion <> ParameterDirection.Input AndAlso p.Tipo <> ParamTypes.RefCursor Then
                    If Comando.Parameters(p.Nombre) IsNot Nothing Then
                        p.AsignarValor(Comando.Parameters(p.Nombre).Value)
                    End If
                End If
            End If
        Next
    End Sub

End Class
