Imports System.Collections.ObjectModel

Namespace Clases

    ' ***********************************************************************
    '  Modulo:  Bulto.vb
    '  Descripción: Clase definición Bulto
    ' ***********************************************************************
    <Serializable()>
    Public Class Bulto
        Inherits Elemento

#Region "[VARIAVIES]"

        Private _FechaProcessoLegado As DateTime
        Private _Parciales As New ObservableCollection(Of Parcial)
        Private _TiposMercancia As ObservableCollection(Of TipoMercancia)
        Private _PrecintosRemesa As New ObservableCollection(Of String)
        Private _CantidadParciales As Long = 0
        Private _ReciboTransporte As String
        Private _CodigoBolsa As String
        Private _FechaHoraTransporte As DateTime
        Private _TipoUbicacion As Enumeradores.TipoUbicacion
        Private _GrupoTerminosIACParciales As GrupoTerminosIAC
        Private _TipoFormato As TipoFormato
        Private _TipoServicio As TipoServicio
        Private _Documento As Documento
        Private _BancoIngreso As Cliente
        Private _BancoIngresoEsBancoMadre As Boolean
        Private _FechaHoraInicioConteo As DateTime
        Private _FechaHoraFinConteo As DateTime
        Private _Estado As Enumeradores.EstadoBulto
        Private _Preparado As Boolean
        Private _Cuadrado As Boolean
        Private _AceptaPicos As Boolean
        Private _TipoBulto As Clases.TipoBulto
        Private _IdentificadorMorfologiaComponente As String
        Private _EsModulo As Boolean
        Private _ConfiguracionNivelSaldos As Enumeradores.ConfiguracionNivelSaldos
        Private _CodigoFormato As String
        Private _TipoObjeto As Enumeradores.TipoObjeto
        Private _CodigoClienteSaldo As String
        Private _DescripcionClienteSaldo As String
        Private _TrabajaPorBulto As Boolean
        Private _SoloLectura As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property AceptaPicos As Boolean
            Get
                Return _AceptaPicos
            End Get
            Set(value As Boolean)
                SetProperty(_AceptaPicos, value, "AceptaPicos")
            End Set
        End Property

        Public Property Cuadrado As Boolean
            Get
                Return _Cuadrado
            End Get
            Set(value As Boolean)
                SetProperty(_Cuadrado, value, "Cuadrado")
            End Set
        End Property

        Public Property Preparado As Boolean
            Get
                Return _Preparado
            End Get
            Set(value As Boolean)
                SetProperty(_Preparado, value, "Preparado")
            End Set
        End Property

        Public Property TipoBulto As Clases.TipoBulto
            Get
                Return _TipoBulto
            End Get
            Set(value As Clases.TipoBulto)
                SetProperty(_TipoBulto, value, "TipoBulto")
            End Set
        End Property

        Public Property FechaProcessoLegado As DateTime
            Get
                Return _FechaProcessoLegado
            End Get
            Set(value As DateTime)
                SetProperty(_FechaProcessoLegado, value, "FechaProcessoLegado")
            End Set
        End Property

        Public Property BancoIngresoEsBancoMadre As Boolean
            Get
                Return _BancoIngresoEsBancoMadre
            End Get
            Set(value As Boolean)
                SetProperty(_BancoIngresoEsBancoMadre, value, "BancoIngresoEsBancoMadre")
            End Set
        End Property

        Public Property BancoIngreso As Cliente
            Get
                Return _BancoIngreso
            End Get
            Set(value As Cliente)
                SetProperty(_BancoIngreso, value, "BancoIngreso")
            End Set
        End Property

        Public Property CodigoBolsa As String
            Get
                Return _CodigoBolsa
            End Get
            Set(value As String)
                SetProperty(_CodigoBolsa, value, "CodigoBolsa")
            End Set
        End Property

        Public Property Estado As Enumeradores.EstadoBulto
            Get
                Return _Estado
            End Get
            Set(value As Enumeradores.EstadoBulto)
                SetProperty(_Estado, value, "Estado")
            End Set
        End Property

        Public Property FechaHoraFinConteo As DateTime
            Get
                Return _FechaHoraFinConteo
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraFinConteo, value, "FechaHoraFinConteo")
            End Set
        End Property

        Public Property FechaHoraInicioConteo As DateTime
            Get
                Return _FechaHoraInicioConteo
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraInicioConteo, value, "FechaHoraInicioConteo")
            End Set
        End Property

        Public Property Documento As Documento
            Get
                Return _Documento
            End Get
            Set(value As Documento)
                SetProperty(_Documento, value, "Documento")
            End Set
        End Property

        Public Property FechaHoraTransporte As DateTime
            Get
                Return _FechaHoraTransporte
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraTransporte, value, "FechaHoraTransporte")
            End Set
        End Property

        Public Property GrupoTerminosIACParciales As GrupoTerminosIAC
            Get
                Return _GrupoTerminosIACParciales
            End Get
            Set(value As GrupoTerminosIAC)
                SetProperty(_GrupoTerminosIACParciales, value, "GrupoTerminosIACParciales")
            End Set
        End Property

        Public Property ReciboTransporte As String
            Get
                Return _ReciboTransporte
            End Get
            Set(value As String)
                SetProperty(_ReciboTransporte, value, "ReciboTransporte")
            End Set
        End Property

        Public Property CantidadParciales As Long
            Get
                ' Se a quantidade de parciais não foi informada
                If _CantidadParciales = 0 Then
                    ' Verifica se existem parciais no malote
                    If Me.Parciales IsNot Nothing AndAlso Me.Parciales.Count > 0 Then
                        ' Atualiza a quantidade de parciais de acordo com os parciais existentes
                        _CantidadParciales = Me.Parciales.Count
                    End If
                End If
                ' Retorna a quantidade de Parciais
                Return _CantidadParciales
            End Get
            Set(value As Long)
                SetProperty(_CantidadParciales, value, "CantidadParciales")
            End Set
        End Property

        Public Property Parciales As ObservableCollection(Of Parcial)
            Get
                Return _Parciales
            End Get
            Set(value As ObservableCollection(Of Parcial))
                SetProperty(_Parciales, value, "Parciales")
            End Set
        End Property

        Public Property TipoFormato As TipoFormato
            Get
                Return _TipoFormato
            End Get
            Set(value As TipoFormato)
                SetProperty(_TipoFormato, value, "TipoFormato")
            End Set
        End Property

        Public Property PrecintosRemesa As ObservableCollection(Of String)
            Get
                Return _PrecintosRemesa
            End Get
            Set(value As ObservableCollection(Of String))
                SetProperty(_PrecintosRemesa, value, "PrecintosRemesa")
            End Set
        End Property

        Public Property TiposMercancia As ObservableCollection(Of TipoMercancia)
            Get
                Return _TiposMercancia
            End Get
            Set(value As ObservableCollection(Of TipoMercancia))
                SetProperty(_TiposMercancia, value, "TipoMercancia")
            End Set
        End Property

        Public Property TipoServicio As TipoServicio
            Get
                Return _TipoServicio
            End Get
            Set(value As TipoServicio)
                SetProperty(_TipoServicio, value, "TipoServicio")
            End Set
        End Property

        Public Property TipoUbicacion As Enumeradores.TipoUbicacion
            Get
                Return _TipoUbicacion
            End Get
            Set(value As Enumeradores.TipoUbicacion)
                SetProperty(_TipoUbicacion, value, "TipoUbicacion")
            End Set
        End Property

        Public Property IdentificadorMorfologiaComponente As String
            Get
                Return _IdentificadorMorfologiaComponente
            End Get
            Set(value As String)
                SetProperty(_IdentificadorMorfologiaComponente, value, "IdentificadorMorfologiaComponente")
            End Set
        End Property

        Public Property EsModulo As Boolean
            Get
                Return _EsModulo
            End Get
            Set(value As Boolean)
                SetProperty(_EsModulo, value, "EsModulo")
            End Set
        End Property

        Public Property ConfiguracionNivelSaldos() As Enumeradores.ConfiguracionNivelSaldos
            Get
                Return _ConfiguracionNivelSaldos
            End Get
            Set(value As Enumeradores.ConfiguracionNivelSaldos)
                SetProperty(_ConfiguracionNivelSaldos, value, "ConfiguracionNivelSaldos")
            End Set
        End Property

        Public Property CodigoFormato() As String
            Get
                Return _CodigoFormato
            End Get
            Set(value As String)
                SetProperty(_CodigoFormato, value, "CodigoFormato")
            End Set
        End Property

        Public Property TipoObjeto() As Enumeradores.TipoObjeto
            Get
                Return _TipoObjeto
            End Get
            Set(value As Enumeradores.TipoObjeto)
                SetProperty(_TipoObjeto, value, "TipoObjeto")
            End Set
        End Property

        Public Property CodigoClienteSaldo() As String
            Get
                Return _CodigoClienteSaldo
            End Get
            Set(value As String)
                SetProperty(_CodigoClienteSaldo, value, "CodigoClienteSaldo")
            End Set
        End Property

        Public Property DescripcionClienteSaldo() As String
            Get
                Return _DescripcionClienteSaldo
            End Get
            Set(value As String)
                SetProperty(_DescripcionClienteSaldo, value, "DescripcionClienteSaldo")
            End Set
        End Property

        Public Property TrabajaPorBulto As Boolean
            Get
                Return _TrabajaPorBulto
            End Get
            Set(value As Boolean)
                SetProperty(_TrabajaPorBulto, value, "TrabajaPorBulto")
            End Set
        End Property

        Public Property SoloLectura As Boolean
            Get
                Return _SoloLectura
            End Get
            Set(value As Boolean)
                SetProperty(_SoloLectura, value, "SoloLectura")
            End Set
        End Property

#End Region

        Sub New()
            Identificador = System.Guid.NewGuid().ToString()
        End Sub

    End Class

End Namespace
