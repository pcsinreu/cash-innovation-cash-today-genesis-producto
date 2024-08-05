Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarTransaccionesFechas

    <Serializable()>
    Public Class Filtro

        Public Property IncluirMediosPago As Boolean
        Public Property DetallarSaldos As Boolean
        Public Property SaldoAMostrar As Comon.Enumeradores.TipoSaldo
        Public Property Certificado As Comon.Enumeradores.TipoCertificado
        Public Property Acreditado As Nullable(Of Comon.Enumeradores.TipoAcreditado)
        Public Property Notificado As Nullable(Of Comon.Enumeradores.TipoNotificado)
        Public Property IncluirSubSectores As Boolean
        Public Property IACs As Boolean
        Public Property IncluirValoresInformativos As Boolean
    End Class

End Namespace