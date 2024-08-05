Namespace RecepcionyEnvio.OtService

    <System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://service.base.sol.prosegur.com/")> _
    Public Class mercanciaTransportar

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property cantidadBulto() As Long

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property cantidadBultoSpecified() As Boolean

        <System.Xml.Serialization.XmlElementAttribute("desglose", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable:=True)> _
        Public Property desglose() As desglose()

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property divisaCodigoISO() As String

        <System.Xml.Serialization.XmlElementAttribute("modulos", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable:=True)> _
        Public Property modulos() As modulo()

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property peso() As Decimal

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property pesoSpecified() As Boolean

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property tipoEmbalajeCodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property tipoMercanciaCodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property valor() As Decimal

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property volumen() As Decimal

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property volumenSpecified() As Boolean

    End Class

End Namespace