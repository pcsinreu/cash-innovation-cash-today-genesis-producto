﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Prosegur.Global.GesEfectivo.Reportes.AccesoDatos.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to SELECT COD_ISO_DIVISA
        '''FROM GEPR_TDIVISA_POSIBLE
        '''WHERE OID_BULTO = []OID_BULTO AND ROWNUM = 1
        '''ORDER BY NEC_NUM_ORDEN.
        '''</summary>
        Friend ReadOnly Property BultoRetornarDivisaLocal() As String
            Get
                Return ResourceManager.GetString("BultoRetornarDivisaLocal", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Select sum(nec_parciales) as nec_parciales
        '''From gepr_tbulto
        '''Where oid_remesa = []P_OID_REMESA.
        '''</summary>
        Friend ReadOnly Property BuscaNecDeclaradosRemesa() As String
            Get
                Return ResourceManager.GetString("BuscaNecDeclaradosRemesa", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to SELECT 
        '''	OID_ITEM_PROCESO, 
        '''	COD_PROCESO, 
        '''	OBS_PARAMETROS, 
        '''	COD_ESTADO, 
        '''	NEL_INTENTOS_ENVIO, 
        '''	COD_USUARIO, 
        '''	FYH_CREACION, 
        '''	FYH_ACTUALIZACION, 
        '''	COD_DELEGACION
        '''FROM GEPR_TITEM_PROCESO
        '''WHERE 
        '''	COD_PROCESO = []COD_PROCESO AND 
        '''	OBS_PARAMETROS = []OBS_PARAMETROS AND
        '''	COD_ESTADO = []COD_ESTADO AND
        '''	COD_DELEGACION = []COD_DELEGACION.
        '''</summary>
        Friend ReadOnly Property BuscarItemProcesoPorParametro() As String
            Get
                Return ResourceManager.GetString("BuscarItemProcesoPorParametro", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to 	SELECT 
        '''		OID_ITEM_PROCESO, 
        '''		COD_PROCESO, 
        '''		OBS_PARAMETROS, 
        '''		COD_ESTADO, 
        '''		NEL_INTENTOS_ENVIO, 
        '''		COD_USUARIO, 
        '''		FYH_CREACION, 
        '''		FYH_ACTUALIZACION, 
        '''		COD_DELEGACION,
        '''		DES_ERROR
        '''	FROM GEPR_TITEM_PROCESO
        '''	WHERE 
        '''		COD_DELEGACION = []COD_DELEGACION
        '''		AND COD_PROCESO = []COD_PROCESO
        '''		AND FYH_CREACION &gt;= []FYH_CREACION
        '''	ORDER BY FYH_CREACION DESC.
        '''</summary>
        Friend ReadOnly Property BuscarUltimoItemProceso() As String
            Get
                Return ResourceManager.GetString("BuscarUltimoItemProceso", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to SELECT 
        '''	CTP.COD_CARACTERISTICA, 
        '''	CTP.DES_CARACTERISTICA, 
        '''	CTP.COD_CARACTERISTICA_CONTEO 
        '''FROM 
        '''	GEPR_TCARAC_POR_TIPO_PROCESADO CTP
        '''	INNER JOIN GEPR_TTIPO_PROCESADO TP ON TP.OID_TIPO_PROCESADO = CTP.OID_TIPO_PROCESADO
        '''WHERE
        '''	TP.OID_BULTO = []OID_BULTO	.
        '''</summary>
        Friend ReadOnly Property CaracteristicaSelecionar() As String
            Get
                Return ResourceManager.GetString("CaracteristicaSelecionar", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to INSERT INTO GEPR_TITEM_PROCESO
        '''	(OID_ITEM_PROCESO, 
        '''	COD_PROCESO, 
        '''	OBS_PARAMETROS, 
        '''	COD_ESTADO, 
        '''	NEL_INTENTOS_ENVIO, 
        '''	COD_USUARIO, 
        '''	FYH_CREACION, 
        '''	FYH_ACTUALIZACION, 
        '''	COD_DELEGACION)
        '''VALUES
        '''	([]OID_ITEM_PROCESO, 
        '''	[]COD_PROCESO, 
        '''	[]OBS_PARAMETROS, 
        '''	[]COD_ESTADO, 
        '''	[]NEL_INTENTOS_ENVIO, 
        '''	[]COD_USUARIO, 
        '''	[]FYH_CREACION, 
        '''	[]FYH_ACTUALIZACION, 
        '''	[]COD_DELEGACION).
        '''</summary>
        Friend ReadOnly Property GuardarItemProcesoConteo() As String
            Get
                Return ResourceManager.GetString("GuardarItemProcesoConteo", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to SELECT R.OID_REMESA,
        '''       R.COD_PRECINTO AS PRECINTO_REMESA,
        '''       B.COD_TRANSPORTE,
        '''       R.FYH_FIN_CONTEO,
        '''       B.DES_CLIENTE,
        '''       B.DES_SUBCLIENTE,
        '''       B.DES_PUNTO_SERVICIO,
        '''	   B.FEC_TRANSPORTE
        '''FROM GEPR_TREMESA R
        '''INNER JOIN GEPR_TBULTO B ON B.OID_REMESA = R.OID_REMESA
        '''WHERE R.COD_ESTADO IN (&apos;PR&apos;,&apos;EN&apos;,&apos;SA&apos;,&apos;NEN&apos;,&apos;NSA&apos;,&apos;MS&apos;,&apos;REN&apos;,&apos;RSA&apos;).
        '''</summary>
        Friend ReadOnly Property InformacionesContejeSelecionar() As String
            Get
                Return ResourceManager.GetString("InformacionesContejeSelecionar", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to INSERT INTO GEPR_TLOG_ERROR
        '''  (OID_LOG_ERROR,
        '''   DES_ERROR,
        '''   DES_OTRO,
        '''   FYH_ERROR,
        '''   COD_USUARIO,
        '''   COD_DELEGACION)
        '''VALUES
        '''  ([]OID_LOG_ERROR,
        '''   []DES_ERROR,
        '''   []DES_OTRO,
        '''   []FYH_ERROR,
        '''   []COD_USUARIO,
        '''   []COD_DELEGACION).
        '''</summary>
        Friend ReadOnly Property InserirLog() As String
            Get
                Return ResourceManager.GetString("InserirLog", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to SELECT 
        '''	TBILLETAJEXSUC.RECUENTO, 
        '''	TBILLETAJEXSUC.FECHA, 
        '''	TBILLETAJEXSUC.LETRA, 
        '''	TBILLETAJEXSUC.F22, 
        '''	TBILLETAJEXSUC.OID_REMESA_ORI, 
        '''	TBILLETAJEXSUC.COD_SUBCLIENTE, 
        '''	TBILLETAJEXSUC.ESTACION, 
        '''	TBILLETAJEXSUC.DES_ESTACION, 
        '''	TBILLETAJEXSUC.MEDIO_PAGO, 
        '''	TBILLETAJEXSUC.DIVISA, 
        '''	TBILLETAJEXSUC.DES_DIVISA, 
        '''	TBILLETAJEXSUC.UNIDAD, 
        '''	TBILLETAJEXSUC.MUTIPLICADOR, 
        '''	TBILLETAJEXSUC.ES_BILLETE, 
        '''	SUM(TBILLETAJEXSUC.CANTIDAD) AS CANTIDAD, 
        '''	SUM(TBILLETAJEXSUC.VALOR) AS VALOR,
        '''	TBILLETAJEXSUC.C [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property ListadoBilletajeSucursalCSV() As String
            Get
                Return ResourceManager.GetString("ListadoBilletajeSucursalCSV", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to SELECT
        '''  SUM(
        '''	CASE 
        '''      WHEN TREGMOV.NUM_IMPORTE_EFECTIVO IS NOT NULL OR TREGMOV.NUM_IMPORTE_EFECTIVO &lt;&gt; 0 THEN
        '''       TREGMOV.NUM_IMPORTE_EFECTIVO
        '''			WHEN	GEPR_TDECL_TDENO01.NUM_IMPORTE_EFECTIVO_DENO=0
        '''	OR	GEPR_TDECL_TDENO01.NUM_IMPORTE_EFECTIVO_DENO IS NULL THEN
        '''     (TDECL.num_importe_efectivo)
        '''			ELSE
        '''     (GEPR_TDECL_TDENO01.NUM_IMPORTE_EFECTIVO_DENO)
        ''' END)  AS IMPORTE_DECLARADO_EFETIVO,
        '''	SUM(
        '''		CASE 
        '''		  WHEN TREGMOV.Num_Importe_Cheque IS NOT NULL OR TREGMOV.Num_Importe_Cheque &lt;&gt; 0 
        '''	 [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property ListadoBilletajeSucursalDivisaPDF() As String
            Get
                Return ResourceManager.GetString("ListadoBilletajeSucursalDivisaPDF", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to SELECT 
        '''	TBILLETAJEXSUC.SUCURSAL AS ESTACION, 
        '''	TBILLETAJEXSUC.DESC_SUCURSAL AS DES_ESTACION, 
        '''	TBILLETAJEXSUC.DIVISA AS DIVISA, 
        '''	TBILLETAJEXSUC.DES_DIVISA AS DES_DIVISA, 
        '''	TBILLETAJEXSUC.COD_TIPO AS COD_TIPO, 
        '''	TBILLETAJEXSUC.DES_TIPO AS DES_TIPO, 
        '''	TBILLETAJEXSUC.ES_BILLETE AS ES_BILLETE, 
        '''	TBILLETAJEXSUC.UNIDAD_MOEDA AS UNIDAD_MOEDA,
        '''	SUM(TBILLETAJEXSUC.UNIDADES) AS UNIDADES, 
        '''	SUM(TBILLETAJEXSUC.VALOR_RECONTADO) AS VALOR_RECONTADO, 
        '''	TBILLETAJEXSUC.DES_MEDIO_PAGO AS DES_MEDIO_PAGO, 
        '''	TBILLE [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property ListadoBilletajeSucursalPDF() As String
            Get
                Return ResourceManager.GetString("ListadoBilletajeSucursalPDF", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to SELECT
        '''  SUM(
        '''  CASE
        '''    WHEN  
        '''    (select 
        '''		1 as Respaldos
        '''     from 
        '''		GEPR_TBULTO B 
        '''		inner join gepr_ttipo_procesado tp on B.oid_bulto = tp.oid_bulto
        '''        inner join gepr_tcarac_por_tipo_procesado ctpr on tp.oid_tipo_procesado = ctpr.oid_tipo_procesado 
        '''        and ctpr.cod_caracteristica = &apos;CodCarac004&apos; 
        '''     where b.oid_bulto not in (
        '''        Select 
        '''			tp2.oid_bulto
        '''        from 
        '''			gepr_ttipo_procesado tp2 
        '''			inner join gepr_tcarac_por_tipo_procesado ctpr2 on tp2.oid_tipo_proc [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property ListadoBilletajeSucursalRespaldoPDF() As String
            Get
                Return ResourceManager.GetString("ListadoBilletajeSucursalRespaldoPDF", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to SELECT 
        '''        DPD.OID_DOC_PARTE_DIFERENCIAS, 
        '''        DPD.OID_REMESA,         
        '''        DPD.NEL_NUMERO_DOCUMENTO,         
        '''        B.DES_CLIENTE,
        '''        B.DES_SUBCLIENTE,
        '''        B.DES_PUNTO_SERVICIO,                                
        '''        R.COD_PRECINTO,        
        '''        B.COD_TRANSPORTE,
        '''        B.FEC_TRANSPORTE,
        '''        DPD.FYH_CONTEO, 
        '''        DPD.FYH_ACTUALIZACION,
        '''        CASE WHEN DPD.BIN_DOCUMENTO_GENERAL IS NOT NULL THEN 1 ELSE 0 END HAY_GENERAL,
        '''        CASE WHEN DPD.BIN_DOCUMENTO_C [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property ListadoParteDiferencias() As String
            Get
                Return ResourceManager.GetString("ListadoParteDiferencias", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to SELECT &apos;00&apos; TIPO_REGISTRO,
        '''       BUL.DES_BOOK_PROCESO,
        '''       BUL.FEC_TRANSPORTE,
        '''       RE.FYH_FIN_CONTEO,
        '''       &apos;A&apos; LETRA_RECIBO_TRANSPORTE,
        '''       BUL.COD_TRANSPORTE,
        '''       RE.COD_ESTADO,
        '''       RE.OID_REMESA
        '''FROM GEPR_TBULTO BUL
        '''INNER JOIN GEPR_TREMESA RE
        '''        ON BUL.OID_REMESA = RE.OID_REMESA
        '''WHERE BUL.COD_CLIENTE = :COD_CLIENTE
        '''  AND RE.FYH_FIN_CONTEO BETWEEN :FECHA_DESDE_FINCONTEO AND :FECHA_HASTA_FINCONTEO
        '''  AND RE.COD_DELEGACION = :COD_DELEGACION
        '''  AND RE.COD_ESTADO = &apos;PR&apos; 
        '''ORD [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property ListadoReciboF22() As String
            Get
                Return ResourceManager.GetString("ListadoReciboF22", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to SELECT	 
        '''	TBILLETAJEXSUC.RECUENTO,
        '''	TBILLETAJEXSUC.FECHA,
        '''	TBILLETAJEXSUC.LETRA,
        '''	TBILLETAJEXSUC.F22,
        '''    TBILLETAJEXSUC.OID_REMESA_ORI,
        '''	TBILLETAJEXSUC.COD_SUBCLIENTE,
        '''	TBILLETAJEXSUC.ESTACION ,
        '''	TBILLETAJEXSUC.DES_ESTACION,
        '''	TBILLETAJEXSUC.MEDIO_PAGO,
        '''	TBILLETAJEXSUC.DIVISA,
        '''	TBILLETAJEXSUC.DES_DIVISA,
        '''	TBILLETAJEXSUC.UNIDAD,
        '''	TBILLETAJEXSUC.MUTIPLICADOR,
        '''    TBILLETAJEXSUC.ES_BILLETE,
        '''	SUM(TBILLETAJEXSUC.CANTIDAD) CANTIDAD,
        '''	SUM(TBILLETAJEXSUC.VALOR) VALOR
        '''FROM	
        '''( 
        '''	-- MEDIOS DE PAGO
        ''' [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property ListadoReciboRespaldo() As String
            Get
                Return ResourceManager.GetString("ListadoReciboRespaldo", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to SELECT 
        '''        DPD.OID_DOC_PARTE_DIFERENCIAS
        '''		[DOCUMENTOS]        
        '''FROM 
        '''        GEPR_TDOC_PARTE_DIFERENCIAS DPD
        '''WHERE
        '''        DPD.OID_DOC_PARTE_DIFERENCIAS = []OID_DOC_PARTE_DIFERENCIAS
        '''		[FILTROS].
        '''</summary>
        Friend ReadOnly Property RecuperarDocumentos() As String
            Get
                Return ResourceManager.GetString("RecuperarDocumentos", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to SELECT
        '''        OID_INVENTARIO
        '''        ,COD_INVENTARIO
        '''        ,GMT_CREACION
        '''        ,DES_USUARIO_CREACION
        '''FROM RPGE_Yinventario
        '''WHERE OID_SECTOR =[]OID_SECTOR
        '''AND GMT_CREACION BETWEEN []DATA_INICIAL AND []DATA_FINAL
        '''.
        '''</summary>
        Friend ReadOnly Property RecuperarInventarios() As String
            Get
                Return ResourceManager.GetString("RecuperarInventarios", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to SELECT
        '''          TS.COD_SECTOR
        '''          ,TS.DES_SECTOR
        '''          ,TS.OID_SECTOR  
        '''          from RPGE_YSECTOR TS
        '''            LEFT JOIN RPGE_YSECTOR TSS on TS.oid_sector_padre = tss.oid_sector
        '''            INNER JOIN RPGE_YPLANTA TP ON TS.OID_PLANTA = TP.OID_PLANTA
        '''            INNER JOIN RPGE_YTIPO_SECTOR TTS ON TTS.OID_TIPO_SECTOR = TS.OID_TIPO_SECTOR
        '''            INNER JOIN RPGE_YDELEGACION DEL ON DEL.OID_DELEGACION = TP.OID_DELEGACION
        '''            WHERE DEL.COD_DELEGACION =[]COD_DELEGACION
        '''        [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property RecuperarSectorPorDelegacion() As String
            Get
                Return ResourceManager.GetString("RecuperarSectorPorDelegacion", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to SELECT
        '''	OID_REGISTRO_MOVIMIENTO
        '''FROM 
        '''	GEPR_TREGISTRO_MOVIMIENTO
        '''WHERE 
        '''	ROWNUM=1.
        '''</summary>
        Friend ReadOnly Property Test() As String
            Get
                Return ResourceManager.GetString("Test", resourceCulture)
            End Get
        End Property
    End Module
End Namespace