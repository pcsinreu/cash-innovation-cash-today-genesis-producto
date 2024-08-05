Imports Prosegur.Framework.Dicionario.Tradutor

Namespace Negocio

    ''' <summary>
    ''' Classe de negócio Termino Medio Pago
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  08/02/2011 criado
    ''' </history>
    <Serializable()> _
    Public Class TerminoMedioPago
        Inherits BaseEntidade

#Region "[VARIÁVEIS]"

        Private _oidTermino As String
        Private _codigoTermino As String
        Private _descripcionTermino As String
        Private _esObligatorio As Boolean
        Private _bolVigente As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidTermino As String
            Get
                Return _oidTermino
            End Get
            Set(value As String)
                _oidTermino = value
            End Set
        End Property

        Public Property BolVigente As Boolean
            Get
                Return _bolVigente
            End Get
            Set(value As Boolean)
                _bolVigente = value
            End Set
        End Property

        Public Property CodigoTermino() As String
            Get
                Return _CodigoTermino
            End Get
            Set(value As String)
                _CodigoTermino = value
            End Set
        End Property

        Public Property DescripcionTermino() As String
            Get
                Return _DescripcionTermino
            End Get
            Set(value As String)
                _DescripcionTermino = value
            End Set
        End Property

        Public Property EsObligatorio() As Boolean
            Get
                Return _EsObligatorio
            End Get
            Set(value As Boolean)
                _EsObligatorio = value
            End Set
        End Property

#End Region

#Region "[CONSTRUTORES]"

        Public Sub New()


        End Sub

        Public Sub New(ObjTermino As Integracion.ContractoServicio.GetMorfologiaDetail.TerminoMedioPago)

            _oidTermino = ObjTermino.OidTermino
            _codigoTermino = ObjTermino.CodTermino
            _descripcionTermino = ObjTermino.DesTermino
            _esObligatorio = ObjTermino.BolEsObligatorio
            _bolVigente = ObjTermino.BolVigente

        End Sub

        Public Sub New(ObjTermino As ContractoServicio.Proceso.GetProcesoDetail.TerminoMedioPago)

            _codigoTermino = ObjTermino.Codigo
            _descripcionTermino = ObjTermino.Descripcion
            _esObligatorio = ObjTermino.EsObligatorioTerminoMedioPago

        End Sub

#End Region

#Region "[MÉTODOS]"

        

#End Region

    End Class

End Namespace
