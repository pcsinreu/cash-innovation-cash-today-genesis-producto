Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetConfigNivel

    ''' <summary>
    ''' Coleção de CofigNivelMovColeccion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 20/05/2013 Criado
    ''' </history>
    <Serializable()> _
    Public Class ConfigNivelMovColeccion
        Inherits List(Of ConfigNivelMov)

    End Class

End Namespace