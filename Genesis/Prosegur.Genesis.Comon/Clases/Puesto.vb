Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel

Namespace Clases
    <Serializable()> _
    Public Class Puesto
        Inherits BindableBase

        Private _Codigo As String
        Public Property Codigo As String
            Get
                Return Me._Codigo
            End Get
            Set(value As String)
                SetProperty(Me._Codigo, value, "Codigo")
            End Set
        End Property

        Private _Identificador As String
        Public Property Identificador As String
            Get
                Return Me._Identificador
            End Get
            Set(value As String)
                SetProperty(Me._Identificador, value, "Identificador")
            End Set
        End Property

        Private _Activo As Boolean
        Public Property Activo As Boolean
            Get
                Return Me._Activo
            End Get
            Set(value As Boolean)
                SetProperty(Me._Activo, value, "Activo")
            End Set
        End Property

        Private _CantidadRemesas As Integer
        Public Property CantidadRemesas As Integer
            Get
                Return Me._CantidadRemesas
            End Get
            Set(value As Integer)
                SetProperty(Me._CantidadRemesas, value, "CantidadRemesas")
            End Set
        End Property

        Private _CodigoHost As String
        Public Property CodigoHost As String
            Get
                Return _CodigoHost
            End Get
            Set(value As String)
                _CodigoHost = value
            End Set
        End Property

        Private _CantidadBultos As Integer
        Public Property CantidadBultos As Integer
            Get
                Return Me._CantidadBultos
            End Get
            Set(value As Integer)
                SetProperty(Me._CantidadBultos, value, "CantidadBultos")
            End Set
        End Property

        Private _SaldoPuesto As String
        Public Property SaldoPuesto As String
            Get
                ' Se existem divisas
                If Saldo IsNot Nothing AndAlso Saldo.Divisas IsNot Nothing AndAlso Saldo.Divisas.Count > 0 Then
                    ' Variável que receberá os valores
                    Dim valores As New Text.StringBuilder
                    ' Define os dados da divisa
                    Saldo.Divisas.Foreach(
                        Sub(d)
                            valores.Append(String.Format(IIf(valores.Length > 0, " | {0} {1}", "{0} {1}"),
                                                         d.Descripcion,
                                                         IIf(d.ValoresTotalesEfectivo Is Nothing OrElse d.ValoresTotalesEfectivo.Sum(Function(s) s.Importe) = 0,
                                                           If(d.Denominaciones IsNot Nothing, d.Denominaciones.Sum(Function(s) If(s.ValorDenominacion IsNot Nothing, s.ValorDenominacion.Sum(Function(v) v.Importe), 0)).ToString("N2"), 0),
                                                           If(d.ValoresTotalesEfectivo IsNot Nothing, d.ValoresTotalesEfectivo.Sum(Function(s) s.Importe).ToString("N2"), 0))))
                        End Sub)
                    ' Define os valores da divisa
                    Me._SaldoPuesto = valores.ToString()
                Else
                    Me._SaldoPuesto = String.Empty
                End If
                Return Me._SaldoPuesto
            End Get
            Set(value As String)
                SetProperty(Me._SaldoPuesto, value, "SaldoPuesto")
            End Set
        End Property

        Private _SaldoMedioPago As String
        Public Property SaldoMedioPago As String
            Get
                ' Se existem divisas
                If Saldo IsNot Nothing AndAlso Saldo.Divisas IsNot Nothing AndAlso Saldo.Divisas.Count > 0 Then
                    ' Variável que receberá os valores
                    Dim valores As New Text.StringBuilder
                    ' Define os dados da divisa
                    Saldo.Divisas.Where(Function(mp) mp.MediosPago IsNot Nothing).ToObservableCollection.Foreach(
                        Sub(mp)
                            valores.Append(String.Format(IIf(valores.Length > 0, " | {0} {1}", "{0} {1}"),
                                                         mp.Descripcion, mp.MediosPago.Sum(Function(s) If(s.Valores IsNot Nothing, s.Valores.Sum(Function(v) v.Importe), 0)).ToString("N2"), 0))
                        End Sub)
                    ' Define os valores da divisa
                    Me._SaldoMedioPago = valores.ToString()
                Else
                    Me._SaldoMedioPago = String.Empty
                End If
                Return Me._SaldoMedioPago
            End Get
            Set(value As String)
                SetProperty(Me._SaldoMedioPago, value, "SaldoMedioPago")
            End Set
        End Property

        Private _Saldo As Clases.SaldoSector
        Public Property Saldo As Clases.SaldoSector
            Get
                Return Me._Saldo
            End Get
            Set(value As Clases.SaldoSector)
                SetProperty(Me._Saldo, value, "Saldo")
            End Set
        End Property

    End Class

End Namespace
