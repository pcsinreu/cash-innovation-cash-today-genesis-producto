Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases.Abono

Namespace Clases.Transferencias

    ''' <summary>
    ''' Classe que representa o Filtro de Elemento e Valor do abono
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Class FiltroConsultaValoresAbono
        Inherits BindableBase

        Public Sub New()

            'Bancos = New List(Of AbonoInformacion)
            Clientes = New List(Of AbonoInformacion)
            SubClientes = New List(Of AbonoInformacion)
            PuntosServicio = New List(Of AbonoInformacion)
            Sectores = New List(Of AbonoInformacion)
            Canales = New List(Of AbonoInformacion)
            SubCanales = New List(Of AbonoInformacion)
            IdentificadoresDivisas = New List(Of String)
            IdentificadoresValores = New List(Of String)
            IdentificadoresElementosSeleccionados = New List(Of String)
            Me.ValoresElegiveis = New List(Of OpcionElegivel)()
            Me.DivisasElegiveis = New List(Of OpcionElegivel)()

        End Sub

        Public Property NumeroExterno As String
        Public Property Precinto As String
        Public Property IdentificadoresElementosSeleccionados As List(Of String)
        Public Property Sectores As List(Of AbonoInformacion)
        Public Property ConsiderarTodosLosNiveles As Boolean
        Public Property Clientes As List(Of AbonoInformacion)
        Public Property SubClientes As List(Of AbonoInformacion)
        Public Property PuntosServicio As List(Of AbonoInformacion)
        'Public Property Bancos As List(Of AbonoInformacion)
        Public Property ClientesConSoloUnDatoBancario As Boolean

        Public Property ValoresCeroYNegativos As Boolean

        Public Property Canales As List(Of AbonoInformacion)
        Public Property SubCanales As List(Of AbonoInformacion)
        Public Property IdentificadoresValores As List(Of String)
        Public Property IdentificadoresDivisas As List(Of String)
        'Public Property TipoValor As Clases.Abono.TipoValorAbono
        Public Property TipoAbono As Enumeradores.TipoAbono
        Public Property ValoresElegiveis As List(Of OpcionElegivel)
        Public Property DivisasElegiveis As List(Of OpcionElegivel)
        Public Property ObtenerParaCalcularDiferencas As Boolean
        Public Property IdentificadorDelegacion As String
    End Class

    <Serializable()>
    Public Class OpcionElegivel
        Public Sub New()

        End Sub
        Public Sub New(opcion, identificador)
            Me.Opcion = opcion
            Me.Identificador = identificador
        End Sub
        Public Property Elegivel As Boolean
        Public Property Opcion As String
        Public Property Identificador As String
    End Class
End Namespace
