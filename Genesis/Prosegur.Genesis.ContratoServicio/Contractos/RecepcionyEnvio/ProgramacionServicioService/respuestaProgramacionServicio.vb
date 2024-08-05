Namespace RecepcionyEnvio.ProgramacionServicioService

    <System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://service.base.sol.prosegur.com/")> _
    Public Class RespuestaBuscarProgramacionServicios
        Inherits Respuesta

        <System.Xml.Serialization.XmlElementAttribute("ruta", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable:=True)> _
        Public Property rutas() As Ruta()

        <System.Xml.Serialization.XmlElementAttribute("ot", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property ots() As Ot()

    End Class

End Namespace


