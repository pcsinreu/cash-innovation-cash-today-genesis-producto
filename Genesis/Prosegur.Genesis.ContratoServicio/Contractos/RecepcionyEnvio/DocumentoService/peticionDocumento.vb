Namespace RecepcionyEnvio.DocumentoService

    <System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://service.base.sol.prosegur.com/")>
    Public Class peticionDocumento
        Inherits basePeticion

        <System.Xml.Serialization.XmlElementAttribute("bultos", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property bultos() As bulto()

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property delegacionCodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property bolSinMovimiento() As Boolean

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property delegacionLocalizacionCodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property documentoCodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property documentoCodigoCliente() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property documentoCodigoSerie() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property documentoId() As String

        <System.Xml.Serialization.XmlElementAttribute("documentoImportes", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property documentoImportes() As documentoImporte()

        <System.Xml.Serialization.XmlElementAttribute("documentoItens", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property documentoItens() As documentoItem()

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property emisorDocumentoCodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property empleoCodigo() As Integer

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property entidadGECodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property entidadLVCodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property estadoCodigo() As Integer

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property fechaEntradaDocumento() As Date

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property fechaServicio() As Date

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property localizacionCodigo() As Integer

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property observacion() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property otId() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property paisCodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property servicioContratadoCodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property canalCodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property subcanalCodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property codigoPuesto() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property codigoUsuario() As String

    End Class

End Namespace
