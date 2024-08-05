' ***********************************************************************
'  Module:  SaldoCuenta.vb
'  Author:  CAzevedo
'  Purpose: Definition of the Class SaldoCuenta
' ***********************************************************************
Namespace Clases

    ''' <summary>
    ''' Classe SaldoCuenta
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class SaldoCuenta
        Inherits Saldo

#Region "Variaveis"

        Private _Cuenta As Cuenta

        Private _SaldoCuentaDetalle As List(Of SaldoCuentaDetalle)
        Private _CodigosComprobantes As List(Of String)
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

        Public Property SaldoCuentaDetalle As List(Of SaldoCuentaDetalle)
            Get
                Return _SaldoCuentaDetalle
            End Get
            Set(value As List(Of SaldoCuentaDetalle))
                SetProperty(_SaldoCuentaDetalle, value, "SaldoCuentaDetalle")
            End Set
        End Property

        Public Property CodigosComprobantes As List(Of String)
            Get
                Return _CodigosComprobantes
            End Get
            Set(value As List(Of String))
                SetProperty(_CodigosComprobantes, value, "CodigosComprobantes")
            End Set
        End Property
#End Region

    End Class

End Namespace