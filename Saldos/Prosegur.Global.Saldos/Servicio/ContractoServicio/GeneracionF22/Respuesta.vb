Imports System.Xml.Serialization
Imports System.Xml

Namespace GeneracionF22OLD

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [abueno] 13/07/2010 - Criado
    ''' </history>    
    <XmlType(Namespace:="urn:GeneracionF22")> _
    <XmlRoot(Namespace:="urn:GeneracionF22")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[INICIALIZAÇÃO]"

        Sub New()
            Me.InfoRemesas = New InfoRemesaColeccion
        End Sub

#End Region


#Region "[VARIAVEIS]"

        Private _InfoRemesas As InfoRemesaColeccion

#End Region

#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' Propriedad InfoRemesa
        ''' </summary>
        ''' <value>InfoRemesaColeccion</value>
        ''' <returns>InfoRemesaColeccion</returns>
        ''' <history>[abueno] 13/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property InfoRemesas() As InfoRemesaColeccion
            Get
                Return _InfoRemesas
            End Get
            Set(value As InfoRemesaColeccion)
                _InfoRemesas = value
            End Set
        End Property


#End Region

    End Class

End Namespace