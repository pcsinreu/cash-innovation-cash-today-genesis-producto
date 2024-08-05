Namespace RecepcionyEnvio.DocumentoService

    <System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://service.base.sol.prosegur.com/")> _
    Public Class respuesta

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property exito() As Boolean

        <System.Xml.Serialization.XmlElementAttribute("datos", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable:=True)> _
        Public Property datos() As datosEstadoLV()

        <System.Xml.Serialization.XmlElementAttribute("errores", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable:=True)> _
        Public Property errores() As errorRespuesta()

    End Class

End Namespace


