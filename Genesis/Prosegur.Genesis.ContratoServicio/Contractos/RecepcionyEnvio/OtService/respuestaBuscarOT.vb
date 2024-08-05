Namespace RecepcionyEnvio.OtService

    <System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://service.base.sol.prosegur.com/")> _
    Public Class respuestaBuscarOT
        Inherits respuesta

        <System.Xml.Serialization.XmlElementAttribute("ot", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property ot() As ot()

    End Class

End Namespace