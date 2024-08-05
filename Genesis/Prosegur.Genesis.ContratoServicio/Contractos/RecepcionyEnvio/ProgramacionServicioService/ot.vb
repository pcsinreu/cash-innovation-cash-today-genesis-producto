Namespace RecepcionyEnvio.ProgramacionServicioService

    <System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://service.base.sol.prosegur.com/")> _
    Public Class Ot

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property otId() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property secuencia() As Integer

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property secuenciaSpecified() As Boolean

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property claveSolicitudGE() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property claveSolicitudLV() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property servicioCodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property tipoOtCodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property tipoOtDescripcion() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property tipoOtDescripcionCorta() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property tipoServicioCodigo() As Integer

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property tipoServicioCodigoSpecified() As Boolean

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property tipoServicioDescripcion() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property entidadFacturacionGECodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property entidadFacturacionGEDescripcion() As String

        <System.Xml.Serialization.XmlElementAttribute("entidadFacturacionGECodigoTercero", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property entidadFacturacionGECodigoTercero() As codigoTercero()

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property entidadFacturacionLVCodigo() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property entidadDebitoDescripcion() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property entidadFacturacionLVDescripcion() As String
        <System.Xml.Serialization.XmlElementAttribute("entidadDebitoCodigoTercero", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property entidadDebitoCodigoTercero() As codigoTercero()

        <System.Xml.Serialization.XmlElementAttribute("entidadFacturacionLVCodigoTercero", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property entidadFacturacionLVCodigoTercero() As codigoTercero()

        <System.Xml.Serialization.XmlElementAttribute("puntoServicioOrigen", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property puntoServicioOrigen() As puntoServicio

        <System.Xml.Serialization.XmlElementAttribute("puntoServicioDestino", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property puntoServicioDestino() As puntoServicio

        <System.Xml.Serialization.XmlElementAttribute("documentos", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property documentos() As documento()

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property rutaId() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property bolAnulada() As Boolean

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property bolEnvioProcesoGE() As Boolean

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property codigoCanalProducto() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property descripcionCanalProducto() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property codigoSubCanalProducto() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property descripcionSubCanalProducto() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property fechaHoraInicioCarga() As DateTime

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property codigoProducto() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property descripcionProducto() As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property estadoLVCodigo As String

        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property estadoLVDescripcion As String

    End Class

End Namespace

