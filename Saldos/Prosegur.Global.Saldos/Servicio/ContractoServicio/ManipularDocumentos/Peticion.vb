Imports System.Xml.Serialization
Imports System.Xml

Namespace ManipularDocumentos

    ''' <summary>
    ''' ManipularDocumentos Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ManipularDocumentos")> _
    <XmlRoot(Namespace:="urn:ManipularDocumentos")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIÁVEIS]"

        Private _Documentos As Documentos
        Private _Reglas As Reglas

#End Region

#Region "[PROPRIEDADES]"

        Public Property Documentos() As Documentos
            Get
                Return _Documentos
            End Get
            Set(value As Documentos)
                _Documentos = value
            End Set
        End Property

        Public Property Reglas() As Reglas
            Get
                Return _Reglas
            End Get
            Set(value As Reglas)
                _Reglas = value
            End Set
        End Property

#End Region

    End Class

End Namespace