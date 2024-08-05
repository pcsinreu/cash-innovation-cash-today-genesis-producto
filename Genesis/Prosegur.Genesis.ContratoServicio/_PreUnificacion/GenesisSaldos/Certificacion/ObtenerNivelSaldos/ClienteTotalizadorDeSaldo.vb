Imports System.Xml.Serialization

Namespace GenesisSaldos.Certificacion.ObtenerNivelSaldos

    <Serializable()>
    Public Class ClienteTotalizadorDeSaldo

        Public Property OidConfigNivelSaldo As String
        Public Property OidCliente As String
        Public Property CodCliente As String
        Public Property DesCliente As String
        Public Property OidSubcliente As String
        Public Property CodSubcliente As String
        Public Property DesSubcliente As String
        Public Property OidPtoServicio As String
        Public Property CodPtoServicio As String
        Public Property DesPtoServicio As String
        Public Property GmtCreacion As DateTime
        Public Property DesUsuarioCreacion As String
        Public Property GmtModificacion As DateTime
        Public Property DesUsuarioModificacion As String
        Public Property ClienteTotalizaEnClienteTotalizadorSaldo As ClienteTotalizaEnClienteTotalizadorSaldoColeccion

    End Class

End Namespace