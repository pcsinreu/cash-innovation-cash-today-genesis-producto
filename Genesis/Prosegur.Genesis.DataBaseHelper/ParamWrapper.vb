Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Extenciones

Imports System.Data

Public Class ParamWrapper
    Private p_Valor As Object = Nothing
    Private p_Tipo As ParamTypes ' ProsegurDbType
    Private p_Nombre As String = ""
    Private p_Direccion As ParameterDirection = ParameterDirection.Input
    Private p_EsArray As Boolean = False
    Private p_Lista As List(Of Object) = Nothing
    Private p_NombreTabla As String = ""


    Public Enum ParamTypes
        [Blob] = 0
        [Integer] = 1
        [Long] = 2
        [Decimal] = 3
        [Boolean] = 4
        [String] = 5
        [Date] = 6
        [Timestamp] = 7
        [RefCursor] = 8
        [Observacion] = 9
    End Enum

    Public Sub New(Nombre As String, Tipo As ProsegurDbType, Valor As Object, Optional Direccion As ParameterDirection = ParameterDirection.Input, Optional EsArray As Boolean = False)
        Dim t As ParamTypes
        Select Case Tipo
            Case ProsegurDbType.Inteiro_Curto, ProsegurDbType.Identificador_Numerico
                t = ParamTypes.Integer
            Case ProsegurDbType.Inteiro_Longo
                t = ParamTypes.Long
            Case ProsegurDbType.Numero_Decimal
                t = ParamTypes.Decimal
            Case ProsegurDbType.Descricao_Curta, ProsegurDbType.Descricao_Longa, ProsegurDbType.Objeto_Id, ProsegurDbType.Identificador_Alfanumerico, ProsegurDbType.Observacao_Curta, ProsegurDbType.Observacao_Longa

                t = ParamTypes.String

                If Valor IsNot Nothing Then
                    Dim str As String = Valor.ToString
                    Valor = If(Not String.IsNullOrEmpty(str), str.ReplaceCaracteresOracle, String.Empty)
                End If

            Case ProsegurDbType.Data, ProsegurDbType.Data_Hora, ProsegurDbType.Hora
                t = ParamTypes.Date
            Case ParamTypes.Timestamp
                t = ParamTypes.Timestamp
            Case ProsegurDbType.Logico
                t = ParamTypes.Boolean
            Case ProsegurDbType.Binario
                t = ParamTypes.Blob
            Case Else
                t = ParamTypes.String
        End Select

        Inicializar(Nombre, t, Valor, Direccion, EsArray)
    End Sub

    Public Sub New(Nombre As String, Tipo As ParamTypes, Valor As Object, Optional Direccion As ParameterDirection = ParameterDirection.Input, Optional EsArray As Boolean = False, Optional NombreTabla As String = "")
        Inicializar(Nombre, Tipo, Valor, Direccion, EsArray, NombreTabla)
    End Sub

    Private Sub Inicializar(Nombre As String, Tipo As ParamTypes, Valor As Object, Optional Direccion As ParameterDirection = ParameterDirection.Input, Optional EsArray As Boolean = False, Optional NombreTabla As String = "")
        If Tipo = ParamTypes.RefCursor AndAlso EsArray Then
            Throw New Exception("Tipo inválido para array asociativo")
        Else
            p_Nombre = Nombre
            p_Tipo = Tipo
            p_Valor = Valor
            p_EsArray = EsArray
            p_Direccion = Direccion
            If p_EsArray Then
                p_Lista = New List(Of Object)
            End If
            p_NombreTabla = NombreTabla.Trim
        End If
    End Sub

    Public Sub AgregarValorArray(Valor As Object)
        If p_Lista IsNot Nothing Then

            If TypeOf Valor Is String Then
                If Valor IsNot Nothing Then
                    Dim str As String = Valor.ToString
                    p_Lista.Add(If(Not String.IsNullOrEmpty(str), str.ReplaceCaracteresOracle, String.Empty))
                End If
            Else
                p_Lista.Add(Valor)
            End If

        End If
    End Sub

    Public ReadOnly Property Nombre As String
        Get
            Return p_Nombre
        End Get
    End Property

    Public ReadOnly Property Tipo As ParamTypes
        Get
            Return p_Tipo
        End Get
    End Property

    Public ReadOnly Property NombreTabla As String
        Get
            Return p_NombreTabla
        End Get
    End Property

    Public ReadOnly Property Valor As Object
        Get
            If p_EsArray Then
                Return p_Lista.ToArray
            Else
                Return p_Valor
            End If
        End Get
    End Property

    Public Sub AsignarValor(Valor As Object)
        If Not p_EsArray Then
            p_Valor = Valor
        End If
    End Sub

    Public ReadOnly Property Direccion As ParameterDirection
        Get
            Return p_Direccion
        End Get
    End Property

    Public ReadOnly Property EsArray As Boolean
        Get
            Return p_EsArray
        End Get
    End Property

    Public ReadOnly Property ListaValores As List(Of Object)
        Get
            Return p_Lista
        End Get
    End Property

End Class
