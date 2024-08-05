Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

' ***********************************************************************
'  Module:  Saldo.vb
'  Author:  CAzevedo
'  Purpose: Definition of the Class Saldo
' ***********************************************************************
Namespace Clases

    ''' <summary>
    ''' Classe Saldo
    ''' </summary>
    ''' <remarks></remarks>
    <XmlRoot("Saldo", Namespace:="Prosegur.Genesis.Comon.Clases")> _
    <XmlInclude(GetType(SaldoCuenta))> _
    <XmlInclude(GetType(SaldoSector))> _
    <Serializable> _
    Public MustInherit Class Saldo
        Inherits BindableBase

#Region "Variaveis"

        Private _Divisas As ObservableCollection(Of Divisa)
        Private _Elementos As ObservableCollection(Of Elemento)
#End Region

#Region "Propriedades"

        Public Property Divisas As ObservableCollection(Of Divisa)
            Get
                Return _Divisas
            End Get
            Set(value As ObservableCollection(Of Divisa))
                SetProperty(_Divisas, value, "Divisas")
            End Set
        End Property

        Public Property Elementos As ObservableCollection(Of Elemento)
            Get
                Return _Elementos
            End Get
            Set(value As ObservableCollection(Of Elemento))
                _Elementos = value
                SetProperty(_Elementos, value, "Elementos")
            End Set
        End Property

#End Region

    End Class

End Namespace