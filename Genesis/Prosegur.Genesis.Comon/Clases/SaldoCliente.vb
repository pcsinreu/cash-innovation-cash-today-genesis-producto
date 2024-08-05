' ***********************************************************************
'  Module:  SaldoCliente.vb
'  Author:  CAzevedo
'  Purpose: Definition of the Class SaldoCliente
' ***********************************************************************
Namespace Clases

    ''' <summary>
    ''' Classe SaldoCliente
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class SaldoCliente
        Inherits Saldo

#Region "Propriedades"

        Public Cliente As Cliente

#End Region

    End Class

End Namespace