Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Interfaces.Helper

Namespace Clases

    <Serializable()>
    Public Class TotalizadorSaldo
        Inherits BindableBase

#Region "Variaveis"
        Private _bolDefecto As Boolean
        Private _cliente As Cliente
        Private _subCliente As SubCliente
        Private _puntoServicio As PuntoServicio
        Private _subCanales As List(Of SubCanal)
        Private _identificadorNivelSaldo As String
        Private _identificadorNivelMovimiento As String
#End Region

#Region "Propriedades"
        Public Property bolDefecto As Boolean
            Get
                Return _bolDefecto
            End Get
            Set(value As Boolean)
                SetProperty(_bolDefecto, value, "BolDefecto")
            End Set
        End Property

        Public Property Cliente As Cliente
            Get
                Return _cliente
            End Get
            Set(value As Cliente)
                SetProperty(_cliente, value, "Cliente")
            End Set
        End Property

        Public Property SubCliente As SubCliente
            Get
                Return _subCliente
            End Get
            Set(value As SubCliente)
                SetProperty(_subCliente, value, "SubCliente")
            End Set
        End Property

        Public Property PuntoServicio As PuntoServicio
            Get
                Return _puntoServicio
            End Get
            Set(value As PuntoServicio)
                SetProperty(_puntoServicio, value, "PuntoServicio")
            End Set
        End Property

        Public Property SubCanales As List(Of SubCanal)
            Get
                Return _subCanales
            End Get
            Set(value As List(Of SubCanal))
                SetProperty(_subCanales, value, "SubCanales")
            End Set
        End Property

        Public Property IdentificadorNivelSaldo As String
            Get
                Return _identificadorNivelSaldo
            End Get
            Set(value As String)
                SetProperty(_identificadorNivelSaldo, value, "IdentificadorNivelSaldo")
            End Set
        End Property

        Public Property IdentificadorNivelMovimiento As String
            Get
                Return _identificadorNivelMovimiento
            End Get
            Set(value As String)
                SetProperty(_identificadorNivelMovimiento, value, "IdentificadorNivelMovimiento")
            End Set
        End Property

        Public NivelSaldo As Enumeradores.NivelSaldo

#End Region

        Public Overrides Function Equals(obj As Object) As Boolean
            Dim totalizador = TryCast(obj, TotalizadorSaldo)
            If (totalizador Is Nothing) Then Return False

            'Nivel Cliente
            If (Me.Cliente Is Nothing) Then Return False
            If (totalizador.Cliente Is Nothing) Then Return False
            If (Me.SubCliente Is Nothing AndAlso totalizador.SubCliente Is Nothing) Then
                Return Me.Cliente.Identificador.Equals(totalizador.Cliente.Identificador)
            End If

            'Nivel SubCliente
            If (Me.SubCliente Is Nothing) Then Return False
            If (totalizador.SubCliente Is Nothing) Then Return False
            If (Me.PuntoServicio Is Nothing AndAlso totalizador.PuntoServicio Is Nothing) Then
                Return (Me.Cliente.Identificador.Equals(totalizador.Cliente.Identificador) AndAlso
                        Me.SubCliente.Identificador.Equals(totalizador.SubCliente.Identificador))
            End If

            'Nivel PuntoServicio
            If (Me.PuntoServicio Is Nothing) Then Return False
            If (totalizador.PuntoServicio Is Nothing) Then Return False
            Return (Me.Cliente.Identificador.Equals(totalizador.Cliente.Identificador) AndAlso
                    Me.SubCliente.Identificador.Equals(totalizador.SubCliente.Identificador) AndAlso
                    Me.PuntoServicio.Identificador.Equals(totalizador.PuntoServicio.Identificador))
        End Function
    End Class

End Namespace

