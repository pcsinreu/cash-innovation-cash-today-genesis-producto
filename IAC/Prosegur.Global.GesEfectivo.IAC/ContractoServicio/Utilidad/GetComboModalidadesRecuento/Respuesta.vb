Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboModalidadesRecuento

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 17/03/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboModalidadesRecuento")> _
    <XmlRoot(Namespace:="urn:GetComboModalidadesRecuento")> _
    <Serializable()> _
Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _modalidadesRecuento As ModalidadeRecuentoColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property ModalidadesRecuento() As ModalidadeRecuentoColeccion
            Get
                Return _modalidadesRecuento
            End Get
            Set(value As ModalidadeRecuentoColeccion)
                _modalidadesRecuento = value
            End Set
        End Property

#End Region

    End Class
End Namespace