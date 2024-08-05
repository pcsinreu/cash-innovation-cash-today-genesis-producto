Namespace Clases.Abono
    <Serializable()>
    Public Class DatoBancario
        Public Sub New()

        End Sub
        Public Sub New(codigoTipoCuentaBancaria As String, codigo As String, docto As String, titularidad As String, bolDefecto As Boolean, identificador As String, banco As Cliente, obs As String)
            Me.New()
            Me.Identificador = identificador
            Me.CodigoTipoCuentaBancaria = codigoTipoCuentaBancaria
            Me.CodigoCuentaBancaria = codigo
            Me.CodigoDocumento = docto
            Me.DescripcionTitularidad = titularidad
            Me.BolDefecto = bolDefecto
            Me.Banco = banco
            Me.Observaciones = obs
        End Sub
        Public Property Identificador As String
        Public Property CodigoTipoCuentaBancaria As String
        Public Property CodigoCuentaBancaria As String
        Public Property CodigoDocumento As String
        Public Property DescripcionTitularidad As String
        Public Property Observaciones As String
        Public Property BolDefecto As Boolean
        Public Property Banco As Cliente
    End Class
End Namespace