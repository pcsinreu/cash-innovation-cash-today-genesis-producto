Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace GenesisSaldos.Certificacion


    ''' <summary>
    ''' Classe Certificado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 27/05/2013 - Criado
    ''' </history>
    <Serializable()> _
    Public Class Certificado
        Inherits CertificadoComon

#Region "[PROPRIEDADES]"

        Public Property IdentificadorCertificadoAnterior As String
        Public Property Cuentas As CuentaColeccion
        Public Property IdentificadorCertificado As String
        Public Property ConfigReporte As Clases.ConfiguracionReporte
        Public Property Plantas As List(Of Clases.Planta)
        Public Property SubCanales As List(Of Clases.SubCanal)

#End Region

    End Class

End Namespace