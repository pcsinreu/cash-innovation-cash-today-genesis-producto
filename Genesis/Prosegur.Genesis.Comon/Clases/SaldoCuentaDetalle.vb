' ***********************************************************************
'  Module:  SaldoCuenta.vb
'  Author:  CAzevedo
'  Purpose: Definition of the Class SaldoCuentaDetalle
' ***********************************************************************
Namespace Clases

    ''' <summary>
    ''' Classe SaldoCuenta
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class SaldoCuentaDetalle
        Inherits Saldo

#Region "Variaveis"

        Private _Cuenta As Cuenta

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

#End Region

    End Class

End Namespace