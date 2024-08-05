Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Clases
    <Serializable()>
    Public Class CuentasContenedor
        Inherits BindableBase
#Region "Variaveis"

        Private _Cuenta As Cuenta
        Private _Divisas As ObservableCollection(Of Divisa)

#End Region

#Region "Propriedades"

        Public Property Cuenta As Cuenta
            Get
                Return _Cuenta
            End Get
            Set(value As Cuenta)
                SetProperty(_Cuenta, value, "Cuenta")
            End Set
        End Property

        Public Property Divisas As ObservableCollection(Of Divisa)
            Get
                Return _Divisas
            End Get
            Set(value As ObservableCollection(Of Divisa))
                SetProperty(_Divisas, value, "Divisas")
            End Set
        End Property

#End Region

    End Class
End Namespace
