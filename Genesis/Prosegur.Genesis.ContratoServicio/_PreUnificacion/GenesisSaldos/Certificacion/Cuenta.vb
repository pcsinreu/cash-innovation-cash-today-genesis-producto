Imports System.Xml.Serialization

Namespace GenesisSaldos.Certificacion

    ''' <summary>
    ''' Classe Cuenta
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class Cuenta

#Region "[PROPRIEDADES]"

        Public Property CodCliente As String
        Public Property DesCliente As String
        Public Property CodSector As String
        Public Property DesSector As String
        Public Property CodSubcanal As String
        Public Property DesSubcanal As String
        Public Property CodSubcliente As String
        Public Property DesSubcliente As String
        Public Property CodPtoServicio As String
        Public Property DesPtoServicio As String
        Public Property CodTipoCuenta As String
        Public Property CodDelegacion As String
        Public Property DesDelegacion As String
        Public Property SaldosEfectivos As CertificadoSaldoEfectivoColeccion
        Public Property SaldosMedioPagos As CertificadoSaldoMedioPagoColeccion
        Public Property Documentos As DocumentoColeccion

        <XmlIgnore()>
        Public Property IdentificadorCuenta As String

#End Region

    End Class

End Namespace