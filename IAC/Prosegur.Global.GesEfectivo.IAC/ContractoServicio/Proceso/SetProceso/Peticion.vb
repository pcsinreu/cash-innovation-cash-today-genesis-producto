Imports System.Xml.Serialization
Imports System.Xml

Namespace Proceso.SetProceso

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 17/03/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetProceso")> _
    <XmlRoot(Namespace:="urn:SetProceso")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIÁVEIS]"

        'Proceso
        Private _Proceso As Proceso
        'Informação
        Private _CodigoUsuario As String


#End Region

#Region "[PROPRIEDADES]"

        'Proceso
        Public Property Proceso() As Proceso
            Get
                Return _Proceso
            End Get
            Set(value As Proceso)
                _Proceso = value
            End Set
        End Property

        'CodigoUsuario
        Public Property CodigoUsuario() As String
            Get
                Return _CodigoUsuario
            End Get
            Set(value As String)
                _CodigoUsuario = value
            End Set
        End Property

#End Region

    End Class

End Namespace