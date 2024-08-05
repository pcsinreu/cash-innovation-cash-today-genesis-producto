Imports System
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.IO
Imports System.Xml

Namespace Extenciones

    ' Define a SOAP Extension that traces the SOAP request and SOAP response
    ' for the Web service method the SOAP extension is applied to.
    Public Class GenerarCertificadoSoapExtension
        Inherits SoapExtension

        Private oldStream As Stream
        Private newStream As Stream
        Private m_filename As String

        ' Save the Stream representing the SOAP request or SOAP response into
        ' a local memory buffer.
        Public Overrides Function ChainStream(stream As Stream) As Stream
            oldStream = stream
            newStream = New MemoryStream()
            Return newStream
        End Function

        ' When the SOAP extension is accessed for the first time, the XML Web
        ' service method it is applied to is accessed to store the file
        ' name passed in, using the corresponding SoapExtensionAttribute.
        Public Overloads Overrides Function GetInitializer(methodInfo As  _
            LogicalMethodInfo, _
          attribute As SoapExtensionAttribute) As Object
            Return CType(attribute, GenerarCertificadoSoapExtensionAttribute).Filename
        End Function

        ' The SOAP extension was configured to run using a configuration file
        ' instead of an attribute applied to a specific Web service
        ' method.  Return a file name based on the class implementing the Web
        ' Service's type.
        Public Overloads Overrides Function GetInitializer(WebServiceType As  _
          Type) As Object
            ' Return a file name to log the trace information to, based on the
            ' type.
            Return "C:\Temp\" + WebServiceType.FullName + ".log"
        End Function

        ' Receive the file name stored by GetInitializer and store it in a
        ' member variable for this specific instance.
        Public Overrides Sub Initialize(initializer As Object)
            m_filename = CStr(initializer)
        End Sub

        ' If the SoapMessageStage is such that the SoapRequest or SoapResponse
        ' is still in the SOAP format to be sent or received over the network,
        ' save it out to file.
        Public Overrides Sub ProcessMessage(message As SoapMessage)
            Select Case message.Stage
                Case SoapMessageStage.BeforeSerialize
                Case SoapMessageStage.AfterSerialize
                    WriteOutput(message)
                Case SoapMessageStage.BeforeDeserialize
                    WriteInput(message)
                Case SoapMessageStage.AfterDeserialize
                Case Else
                    Throw New Exception("invalid stage")
            End Select
        End Sub

        ' Write the SOAP message out to a file.
        Public Sub WriteOutput(message As SoapMessage)
            newStream.Position = 0

            'Copy(newStream, oldStream)

            'newStream.Position = 0

            Dim xDocument = New XmlDocument()
            xDocument.Load(newStream)

            Dim xmlTimeZone = GetElement(xDocument, "TimeZone")

            If xmlTimeZone IsNot Nothing AndAlso Not String.IsNullOrEmpty(xmlTimeZone.InnerText) Then

                Dim timezone As String = xmlTimeZone.InnerText

                '***************** FecCer *****************        
                Dim xmlFecCer = GetElement(xDocument, "FecCer")
                ConverteDateTimeZone(xmlFecCer, timezone)

                '***************** Documentos *****************        
                Dim xmlDocList As XmlNodeList = xDocument.DocumentElement.GetElementsByTagName("Doc")

                If xmlDocList IsNot Nothing AndAlso xmlDocList.Count > 0 Then
                    For Each xmlDoc As XmlNode In xmlDocList

                        Dim xmlFecGes = xmlDoc.Attributes("FecGes")
                        ConverteDateTimeZone(xmlFecGes, timezone)

                        Dim xmlFecRea = xmlDoc.Attributes("FecRea")
                        ConverteDateTimeZone(xmlFecRea, timezone)

                    Next
                End If

            End If

            If xDocument.FirstChild.NodeType = XmlNodeType.XmlDeclaration Then
                xDocument.RemoveChild(xDocument.FirstChild)
            End If

            xDocument.Save(oldStream)

        End Sub

        Public Function GetElement(xDocument As XmlDocument, element As String) As XmlElement

            Dim lista = xDocument.DocumentElement.GetElementsByTagName(element)
            If lista IsNot Nothing AndAlso lista.Count > 0 Then
                Return lista(0)
            End If

            Return Nothing

        End Function

        Public Sub ConverteDateTimeZone(ByRef xmlDateTime As XmlNode, timeZone As String)
            If xmlDateTime IsNot Nothing AndAlso (Not String.IsNullOrEmpty(xmlDateTime.Value) OrElse Not String.IsNullOrEmpty(xmlDateTime.InnerText)) Then

                If Not String.IsNullOrEmpty(xmlDateTime.Value) Then
                    Dim dtFechaHora As DateTimeOffset = DateTime.MinValue
                    DateTimeOffset.TryParse(xmlDateTime.Value, dtFechaHora)

                    If dtFechaHora <> DateTime.MinValue Then
                        xmlDateTime.Value = dtFechaHora.ToString("yyyy-MM-ddTHH:mm:ss.ffffff") + timeZone
                    End If
                End If

                If Not String.IsNullOrEmpty(xmlDateTime.InnerText) Then
                    Dim dtFechaHora As DateTimeOffset = DateTime.MinValue
                    DateTimeOffset.TryParse(xmlDateTime.InnerText, dtFechaHora)

                    If dtFechaHora <> DateTime.MinValue Then
                        xmlDateTime.InnerText = dtFechaHora.ToString("yyyy-MM-ddTHH:mm:ss.ffffff") + timeZone
                    End If
                End If

            End If
        End Sub

        ' Write the SOAP message out to a file.
        Public Sub WriteInput(message As SoapMessage)
            Copy(oldStream, newStream)
            newStream.Position = 0
        End Sub

        Sub Copy(fromStream As Stream, toStream As Stream)
            Dim reader As New StreamReader(fromStream)
            Dim writer As New StreamWriter(toStream)
            writer.WriteLine(reader.ReadToEnd())
            writer.Flush()
        End Sub
    End Class

    ' Create a SoapExtensionAttribute for our SOAP Extension that can be
    ' applied to a Web service method.
    <AttributeUsage(AttributeTargets.Method)> _
    Public Class GenerarCertificadoSoapExtensionAttribute
        Inherits SoapExtensionAttribute

        Private m_filename As String = "c:\log.txt"
        Private m_priority As Integer

        Public Overrides ReadOnly Property ExtensionType() As Type
            Get
                Return GetType(GenerarCertificadoSoapExtension)
            End Get
        End Property

        Public Overrides Property Priority() As Integer
            Get
                Return m_priority
            End Get
            Set(value As Integer)
                m_priority = value
            End Set
        End Property

        Public Property Filename() As String
            Get
                Return m_filename
            End Get
            Set(value As String)
                m_filename = value
            End Set
        End Property
    End Class

End Namespace