Namespace RecepcionyEnvio.ProgramacionServicioService

    <System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://service.base.sol.prosegur.com/")> _
    Public Class documentoItem

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property tipoMercanciaCodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property divisaCodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property cantidad() As Integer

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property cantidadSpecified() As Boolean

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property valor() As Decimal

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property valorSpecified() As Boolean

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property codTipoEmbalaje() As String

        <System.Xml.Serialization.XmlElementAttribute("desglose", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property desgloses() As desglose()

        <System.Xml.Serialization.XmlElementAttribute("bulto", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property bulto() As bulto

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property tipoMercanciaDescripcion() As String

    End Class

End Namespace
