Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace GenesisSaldos.Certificacion


    ''' <summary>
    ''' Classe CertificadoComon
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [marcel.espiritosanto] 10/06/2015 - Criado
    ''' </history>
    <Serializable()> _
    Public Class CertificadoComon

#Region "[PROPRIEDADES]"

        Public Property CodigoCertificado As String
        Public Property CodigoEstado As String
        Public Property DescripcionEstado As String
        Public Property CodigoExterno As String
        Public Property FyhCertificado As DateTime
        Public Property Cliente As Clases.Cliente
        Public Property CodigosDelegaciones As List(Of String)
        Public Property CodigosSectores As List(Of String)
        Public Property CodigosSubCanales As List(Of String)

#End Region

    End Class

End Namespace