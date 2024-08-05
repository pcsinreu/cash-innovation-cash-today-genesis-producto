Imports System.Xml.Serialization

Namespace Clases


    <Serializable> _
       Public Class SaldoSector
        Inherits Saldo

#Region "Variaveis"

        Private _Sector As Sector

#End Region

#Region "Propriedades"

        Public Property Sector As Sector
            Get
                Return _Sector
            End Get
            Set(value As Sector)
                SetProperty(_Sector, value, "Sector")
            End Set
        End Property

#End Region

    End Class

End Namespace