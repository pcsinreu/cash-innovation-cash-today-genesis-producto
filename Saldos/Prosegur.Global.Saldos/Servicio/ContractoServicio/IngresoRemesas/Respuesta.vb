Imports System.Xml.Serialization
Imports System.Xml

Namespace IngresoRemesas

    ''' <summary>
    ''' Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 27/07/2009 Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:IngresoRemesas")> _
    <XmlRoot(Namespace:="urn:IngresoRemesas")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _RemesasOK As RemesasOk
        Private _RemesasError As RemesasError

#End Region

#Region "[PROPRIEDADES]"

        Public Property RemesasOK() As RemesasOk
            Get
                Return _RemesasOK
            End Get
            Set(value As RemesasOk)
                _RemesasOK = value
            End Set
        End Property

        Public Property RemesasError() As RemesasError
            Get
                Return _RemesasError
            End Get
            Set(value As RemesasError)
                _RemesasError = value
            End Set
        End Property

#End Region

    End Class
End Namespace