Namespace NuevoSalidas.Recibo.TransporteF22.Parametros

    ''' <summary>
    ''' Entidad Remesa
    ''' </summary>
    ''' <history>[jviana] 23/08/2010 Creado</history>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class Remesa

#Region "[VARIÁVEIS]"

        Private _CodUsuario As String
        Private _CodDelegacion As String
        Private _DesDelegacion As String
        Private _CodSector As String
        Private _DesSector As String
        Private _FechaServicio As Date
        Private _NelPedidoLegado As Integer
        Private _NelParada As Integer
        Private _NelControlLegado As String
        Private _CodReciboRemesa As String
        Private _CodClienteFacturacion As String
        Private _DesClienteFacturacion As String
        Private _CodSubcliente As String
        Private _DesSubcliente As String
        Private _DesDireccionEntrega As String
        Private _DesLocalidadEntrega As String
        Private _DesConfiguracionPlanta As String
        Private _DesCondicionesTransporte As String
        Private _RazonSocialClienteServicoF22 As String
        Private _RazonSocialEmpresaF22 As String
        Private _PlantaConfeccionRemitoF22 As String
        Private _LocalidadRemitoF22 As String
        Private _Copias As List(Of String)
        Private _DescripcionClienteDestinoArgentina As String
        Private _Efectivos As Recibo.TransporteF22.Parametros.EfectivoColeccion
        Private _Bultos As Recibo.TransporteF22.Parametros.BultoColeccion
        Private _CantidadBultosTotal As Integer

        ' adicionado por [marcel.espiritosanto] 13/03/2013 referente a demanda: 0008250: PT46603: ARG-SALIDAS-CRECIMIENTOCLIENTES 
        Private _DesClienteDestino As String

        Private _CodClienteSaldo As String
        Private _DesClienteSaldo As String

        Private _DesContacto1 As String
        Private _DesContacto2 As String
        Private _DesContacto3 As String
        Private _DesContacto4 As String

        Private _CodATM As String

#End Region

#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' Propriedad CodUsuario
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodUsuario() As String
            Get
                Return _CodUsuario
            End Get
            Set(value As String)
                _CodUsuario = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodPlanta
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodDelegacion() As String
            Get
                Return _CodDelegacion
            End Get
            Set(value As String)
                _CodDelegacion = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad DesPlanta
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property DesDelegacion() As String
            Get
                Return _DesDelegacion
            End Get
            Set(value As String)
                _DesDelegacion = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodSector
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodSector() As String
            Get
                Return _CodSector
            End Get
            Set(value As String)
                _CodSector = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad DesSector
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property DesSector() As String
            Get
                Return _DesSector
            End Get
            Set(value As String)
                _DesSector = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad FechaServicio
        ''' </summary>
        ''' <value>Date</value>
        ''' <returns>Date</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property FechaServicio() As Date
            Get
                Return _FechaServicio
            End Get
            Set(value As Date)
                _FechaServicio = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad NelPedidoLegado
        ''' </summary>
        ''' <value>Integer</value>
        ''' <returns>Integer</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property NelPedidoLegado() As Integer
            Get
                Return _NelPedidoLegado
            End Get
            Set(value As Integer)
                _NelPedidoLegado = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad NelParada
        ''' </summary>
        ''' <value>Integer</value>
        ''' <returns>Integer</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property NelParada() As Integer
            Get
                Return _NelParada
            End Get
            Set(value As Integer)
                _NelParada = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad NelControlLegado
        ''' </summary>
        ''' <value>Integer</value>
        ''' <returns>Integer</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property NelControlLegado() As String
            Get
                Return _NelControlLegado
            End Get
            Set(value As String)
                _NelControlLegado = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodReciboRemesa
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodReciboRemesa() As String
            Get
                Return _CodReciboRemesa
            End Get
            Set(value As String)
                _CodReciboRemesa = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodClienteFacturacion
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodClienteFacturacion() As String
            Get
                Return _CodClienteFacturacion
            End Get
            Set(value As String)
                _CodClienteFacturacion = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad DesClienteFacturacion
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property DesClienteFacturacion() As String
            Get
                Return _DesClienteFacturacion
            End Get
            Set(value As String)
                _DesClienteFacturacion = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodSubcliente
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodSubcliente() As String
            Get
                Return _CodSubcliente
            End Get
            Set(value As String)
                _CodSubcliente = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad DesSubcliente
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property DesSubcliente() As String
            Get
                Return _DesSubcliente
            End Get
            Set(value As String)
                _DesSubcliente = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad DesDireccionEntrega
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property DesDireccionEntrega() As String
            Get
                Return _DesDireccionEntrega
            End Get
            Set(value As String)
                _DesDireccionEntrega = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad DesLocalidadEntrega
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property DesLocalidadEntrega() As String
            Get
                Return _DesLocalidadEntrega
            End Get
            Set(value As String)
                _DesLocalidadEntrega = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad DesConfiguracionPlanta
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property DesConfiguracionPlanta() As String
            Get
                Return _DesConfiguracionPlanta
            End Get
            Set(value As String)
                _DesConfiguracionPlanta = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad DesCondicionesTransporte
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property DesCondicionesTransporte() As String
            Get
                Return _DesCondicionesTransporte
            End Get
            Set(value As String)
                _DesCondicionesTransporte = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad Copias
        ''' </summary>
        ''' <value>List(Of String)</value>
        ''' <returns>List(Of String)</returns>
        ''' <history>[jorge.viana]	24/08/2010	Creado</history>
        ''' <remarks></remarks>
        Public Property Copias() As List(Of String)
            Get
                Return _Copias
            End Get
            Set(value As List(Of String))
                _Copias = value
            End Set
        End Property

        Public Property DescripcionClienteDestinoArgentina As String
            Get
                Return _DescripcionClienteDestinoArgentina
            End Get
            Set(value As String)
                _DescripcionClienteDestinoArgentina = value
            End Set
        End Property

        Public Property RazonSocialClienteServicoF22 As String
            Get
                Return _RazonSocialClienteServicoF22
            End Get
            Set(value As String)
                _RazonSocialClienteServicoF22 = value
            End Set
        End Property

        Public Property RazonSocialEmpresaF22 As String
            Get
                Return _RazonSocialEmpresaF22
            End Get
            Set(value As String)
                _RazonSocialEmpresaF22 = value
            End Set
        End Property

        Public Property PlantaConfeccionRemitoF22 As String
            Get
                Return _PlantaConfeccionRemitoF22
            End Get
            Set(value As String)
                _PlantaConfeccionRemitoF22 = value
            End Set
        End Property

        Public Property LocalidadRemitoF22 As String
            Get
                Return _LocalidadRemitoF22
            End Get
            Set(value As String)
                _LocalidadRemitoF22 = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad Efectivos
        ''' </summary>
        ''' <value>Recibo.TransporteF22.Parametros.EfectivoColeccion</value>
        ''' <returns>Recibo.TransporteF22.Parametros.EfectivoColeccion</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property Efectivos() As Recibo.TransporteF22.Parametros.EfectivoColeccion
            Get
                Return _Efectivos
            End Get
            Set(value As Recibo.TransporteF22.Parametros.EfectivoColeccion)
                _Efectivos = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad Bultos
        ''' </summary>
        ''' <value>Recibo.TransporteF22.Parametros.BultoColeccion</value>
        ''' <returns>Recibo.TransporteF22.Parametros.BultoColeccion</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property Bultos() As Recibo.TransporteF22.Parametros.BultoColeccion
            Get
                Return _Bultos
            End Get
            Set(value As Recibo.TransporteF22.Parametros.BultoColeccion)
                _Bultos = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad DesClienteDestino
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <history>
        ''' [marcel.espiritosanto] 13/03/2013 referente a demanda: 0008250: PT46603: ARG-SALIDAS-CRECIMIENTOCLIENTES 
        ''' </history>
        ''' <remarks></remarks>
        Public Property DesClienteDestino() As String
            Get
                Return _DesClienteDestino
            End Get
            Set(value As String)
                _DesClienteDestino = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CantidadBultosTotal
        ''' Armazena a quantidade de bultos total da remessa. Em alguns casos os bultos podem ser filtrados para exibição,
        ''' assim quando faz um Count() nos bultos não mostra a quantidade real de bulto e sim apenas os filtrados
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <history>
        ''' [victor.ramos] 11/06/2015 referente a demanda: 3638 - Salidas - ReporteF22 - Ajustar Cantidad de Bultos que aparece en el Reporte F22, cuando hay módulos en la remesa.
        ''' </history>
        ''' <remarks></remarks>
        Public Property CantidadBultosTotal() As Integer
            Get
                Return _CantidadBultosTotal
            End Get
            Set(value As Integer)
                _CantidadBultosTotal = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodClienteSaldo
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Public Property CodClienteSaldo() As String
            Get
                Return _CodClienteSaldo
            End Get
            Set(value As String)
                _CodClienteSaldo = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad DesClienteSaldo
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Public Property DesClienteSaldo() As String
            Get
                Return _DesClienteSaldo
            End Get
            Set(value As String)
                _DesClienteSaldo = value
            End Set
        End Property

        Public Property DesContacto1() As String
            Get
                Return _DesContacto1
            End Get
            Set(value As String)
                _DesContacto1 = value
            End Set
        End Property

        Public Property DesContacto2() As String
            Get
                Return _DesContacto2
            End Get
            Set(value As String)
                _DesContacto2 = value
            End Set
        End Property

        Public Property DesContacto3() As String
            Get
                Return _DesContacto3
            End Get
            Set(value As String)
                _DesContacto3 = value
            End Set
        End Property

        Public Property DesContacto4() As String
            Get
                Return _DesContacto4
            End Get
            Set(value As String)
                _DesContacto4 = value
            End Set
        End Property

        Public Property CodATM() As String
            Get
                Return _CodATM
            End Get
            Set(value As String)
                _CodATM = value
            End Set
        End Property

        Public Property TipoReciboRemesa As TipoReciboRemesa
        Public Property TipoServicio As String
        Public Property FechaHoraPreparacion As DateTime?
        Public Property DesPuntoServicio As String
        Public Property CodPuntoServicio As String
        Public Property DescripcionComentario As String

        Public Property TerminosIAC As List(Of Termino)

#End Region

    End Class

End Namespace