Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperarDatosDocumento

    <XmlType(Namespace:="urn:RecuperarDatosDocumento")> _
    <XmlRoot(Namespace:="urn:RecuperarDatosDocumento")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIAVEIS]"

        Private _IdDocumento As Integer
        Private _IdGrupo As Integer
        Private _IdPSSector As String
        Private _EstadoComprobante As Enumeradores.eEstadoComprobante
        Private _VistaDestinatario As Enumeradores.eVistaDestinatario = Enumeradores.eVistaDestinatario.No

#End Region

#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' Código único de identificação do documento
        ''' </summary>
        ''' <history>
        ''' [vinicius.gama] Criado em 14/07/09
        ''' </history>
        Public Property IdDocumento() As Integer
            Get
                Return _IdDocumento
            End Get
            Set(value As Integer)
                _IdDocumento = value
            End Set
        End Property

        ''' <summary>
        ''' Código único de identificação do documento grupo
        ''' </summary>
        ''' <history>
        ''' [vinicius.gama] Criado em 14/07/09
        ''' </history>
        Public Property IdGrupo() As Integer
            Get
                Return _IdGrupo
            End Get
            Set(value As Integer)
                _IdGrupo = value
            End Set
        End Property

        ''' <summary>
        ''' Código de identificação do setor
        ''' </summary>
        ''' <history>
        ''' [vinicius.gama] Criado em 14/07/09
        ''' </history>
        Public Property IdPSSector() As String
            Get
                Return _IdPSSector
            End Get
            Set(value As String)
                _IdPSSector = value
            End Set
        End Property

        ''' <summary>
        ''' 1 – En Proceso
        ''' 2 – Impreso
        ''' 3 – Aceptados
        ''' 4 – Rechazados
        ''' 5 – Baja
        ''' </summary>
        ''' <history>
        ''' [vinicius.gama] Criado em 14/07/09
        ''' </history>
        Public Property EstadoComprobante() As Enumeradores.eEstadoComprobante
            Get
                Return _EstadoComprobante
            End Get
            Set(value As Enumeradores.eEstadoComprobante)
                _EstadoComprobante = value
            End Set
        End Property

        ''' <summary>
        ''' 0 – No (Origem) => Default
        ''' 1 – Si  (Destino)
        ''' </summary>
        ''' <history>
        ''' [vinicius.gama] Criado em 14/07/09
        ''' </history>
        Public Property VistaDestinatario() As Enumeradores.eVistaDestinatario
            Get
                Return _VistaDestinatario
            End Get
            Set(value As Enumeradores.eVistaDestinatario)
                _EstadoComprobante = value
            End Set
        End Property

#End Region

    End Class

End Namespace