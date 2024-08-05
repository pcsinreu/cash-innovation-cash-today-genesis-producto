Imports System.Xml.Serialization
Imports System.Xml

Namespace Planta.VerificaExistencia

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 19/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificaExistencia")> _
    <XmlRoot(Namespace:="urn:VerificaExistencia")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIAVEIS]"

        Private _codPlanta As String
        Private _codDelegacion As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodPlanta() As String
            Get
                Return _codPlanta
            End Get
            Set(value As String)
                _codPlanta = value
            End Set
        End Property

        Public Property CodDelegacion() As String
            Get
                Return _codDelegacion
            End Get
            Set(value As String)
                _codDelegacion = value
            End Set
        End Property


#End Region
    End Class
End Namespace
