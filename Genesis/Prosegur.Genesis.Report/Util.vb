Imports System.Xml

Public Class Util
    Public Shared Function LerXMLConfiguracion(pathXML) As List(Of ConfiguracionGeneral)
        Dim configuracoes As New List(Of ConfiguracionGeneral)

        Return configuracoes
    End Function

    Public Shared Function GravarXMLConfiguracion(pathXML As String, ListaConfiguraciones As List(Of ConfiguracionGeneral)) As Boolean
        Dim sucesso As Boolean = True
        ' Cria um novo arquivo.
        Dim xmlw As New XmlTextWriter(pathXML, System.Text.Encoding.UTF8)
        xmlw.Formatting = Formatting.Indented
        xmlw.WriteStartDocument()

        ' Criar um elemento geral 
        xmlw.WriteStartElement("Configuraciones")

        For Each config As ConfiguracionGeneral In ListaConfiguraciones
            With xmlw
                .WriteStartElement("ConfiguracionGeneral")
                .WriteElementString("Report", config.Report)
                .WriteElementString("ID", config.ID)
                .WriteElementString("FormatoSalida", config.FormatoSalida)
                .WriteElementString("NombreArchivo", config.NombreArchivo)
                .WriteElementString("ExtencionArchivo", config.ExtencionArchivo)
                .WriteElementString("Separador", config.Separador)
                .WriteEndElement()
            End With
        Next

        xmlw.WriteEndElement() ' <- ConfiguracionGeneral 
        xmlw.WriteEndDocument()

        ' Fecha o documento XML 
        xmlw.Flush()
        xmlw.Close()

        Return sucesso
    End Function

End Class
