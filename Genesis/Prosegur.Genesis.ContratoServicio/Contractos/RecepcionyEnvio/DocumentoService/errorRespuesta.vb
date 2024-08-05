Namespace RecepcionyEnvio.DocumentoService

    <System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://service.base.sol.prosegur.com/")> _
    Public Class errorRespuesta

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property errorCodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property errorIdSol() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property errorMensaje() As String

    End Class

End Namespace
