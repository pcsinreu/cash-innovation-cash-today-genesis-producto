Namespace RecepcionyEnvio.ProgramacionServicioService

    <System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://service.base.sol.prosegur.com/")> _
    Public Class puntoServicio

        <System.Xml.Serialization.XmlElementAttribute("centro", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property centro() As centro

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property codigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property codigoAjeno() As String

        <System.Xml.Serialization.XmlElementAttribute("codigoTercero", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property codigoTercero() As codigoTercero()

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property descripcion As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property descripcionCompleta() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property direccion() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property id() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property necOrderOT() As Integer

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property necOrderOTSpecified() As Boolean

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property tipoCodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property tipoDescripcion() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property prosegurBol As Boolean

    End Class

End Namespace

