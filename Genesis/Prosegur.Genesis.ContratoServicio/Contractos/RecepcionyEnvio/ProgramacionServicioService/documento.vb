Namespace RecepcionyEnvio.ProgramacionServicioService

    <System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://service.base.sol.prosegur.com/")> _
    Public Class documento

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property documentoID() As String

        <System.Xml.Serialization.XmlElementAttribute("emisorDocumento", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property emisorDocumento() As Emisor

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property fechaEntradaDocumento() As DateTime

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property fechaEntradaDocumentoSpecified() As Boolean

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property localizacionCodigo() As Integer

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property localizacionCodigoSpecified() As Boolean

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property numeroSerie() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property numero() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property documentoClienteCodigo() As String

        <System.Xml.Serialization.XmlElementAttribute("entidadLV", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property entidadLV() As entidad

        <System.Xml.Serialization.XmlElementAttribute("entidadGE", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property entidadGE() As entidad

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property observacion() As String

        <System.Xml.Serialization.XmlElementAttribute("documentoImporte", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property documentoImporte() As documentoImporte()

        <System.Xml.Serialization.XmlElementAttribute("bultos", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property bultos() As bulto()

        <System.Xml.Serialization.XmlElementAttribute("documentoItens", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property documentoItens() As documentoItem()

    End Class

End Namespace