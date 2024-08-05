Imports System.Xml.Serialization
Imports System.Xml

Namespace CodigoAjeno.VerificarIdentificadorXCodigoAjeno

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 16/04/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarIdentificadorXCodigoAjeno")>
    <XmlRoot(Namespace:="urn:VerificarIdentificadorXCodigoAjeno")>
    <Serializable()>
    Public Class Peticion

#Region "[Variáveis]"

        Private _OidCodigoAjeno As String
        Private _CodTipoTablaGenesis As String
        Private _CodIdentificador As String
        Private _CodAjeno As String

#End Region

#Region "[Propriedades]"

        Public Property OidCodigoAjeno() As String
            Get
                Return _OidCodigoAjeno
            End Get
            Set(value As String)
                _OidCodigoAjeno = value
            End Set
        End Property

        Public Property CodTipoTablaGenesis() As String
            Get
                Return _CodTipoTablaGenesis
            End Get
            Set(value As String)
                _CodTipoTablaGenesis = value
            End Set
        End Property

        Public Property CodIdentificador() As String
            Get
                Return _CodIdentificador
            End Get
            Set(value As String)
                _CodIdentificador = value
            End Set
        End Property

        Public Property CodAjeno() As String
            Get
                Return _CodAjeno
            End Get
            Set(value As String)
                _CodAjeno = value
            End Set
        End Property

        Public Property NombreCampoOid As String
        Public Property NombreCampoCod As String
        Public Property NombreCampoDes As String

#End Region

    End Class

End Namespace