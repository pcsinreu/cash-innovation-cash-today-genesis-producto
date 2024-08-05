Namespace RecepcionyEnvio.ProgramacionServicioService

    <System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://service.base.sol.prosegur.com/")>
    Public Class peticionBuscarProgramacionServicios

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property idSistemaOrigen() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property paisCodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property delegacionGECodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property delegacionLVCodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property operacion() As Integer

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property bolOtNoProgramadas() As Boolean

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property rutaCodigo() As Integer

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property rutaCodigoSpecified() As Boolean

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property fecha() As DateTime

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property fechaSpecified() As Boolean

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property rutaId() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property idiomaCodigo() As String

    End Class

End Namespace
