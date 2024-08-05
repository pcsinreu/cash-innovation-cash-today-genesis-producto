Imports System.Xml.Serialization
Imports System.Xml

Namespace TipoSetor.VerificaTipoSectores
    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificaTipoSectores")> _
    <XmlRoot(Namespace:="urn:VerificaTipoSectores")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _Existe As Boolean

#End Region

#Region "[Propriedades]"

        Public Property Existe() As Boolean
            Get
                Return _Existe
            End Get
            Set(value As Boolean)
                _Existe = value
            End Set
        End Property

#End Region
    End Class
End Namespace
