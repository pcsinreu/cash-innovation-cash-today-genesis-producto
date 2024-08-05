Imports System.Xml.Serialization
Imports System.Xml
Imports System.Data

Namespace RecuperarTransaccionNoMigrada

    ''' <summary>
    ''' TransaccionMigracion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [marcel.espiritosanto] - 21/05/2013 - Creado
    ''' </history>
    <XmlType(Namespace:="urn:RecuperarTransaccionNoMigrada")> _
    <XmlRoot(Namespace:="urn:RecuperarTransaccionNoMigrada")> _
    <Serializable()> _
    Public Class TransaccionMigracion

#Region "Propriedades"

        Public Property Oid_Transaccion_Migracion As String
        Public Property Cod_Documento As String
        Public Property Cod_Cliente As String
        Public Property Especie_Descripcion As String
        Public Property Num_Importe As Decimal
        Public Property Nel_Cantidad As Integer
        Public Property Bol_Disponible As Boolean
        Public Property Cod_Estado_Documento As String
        Public Property Cod_Tipo_Movimiento As String
        Public Property Cod_Nivel_Detalle As String
        Public Property Cod_Tipo_Efectivo_Total As String
        Public Property Fyh_Transaccion As DateTime

#End Region

#Region "Construtores"

        Sub New()

        End Sub

        Sub New(row As DataRow)
            Util.AtribuirValorObjeto(Oid_Transaccion_Migracion, row("OID_TRANSACCION_MIGRACION"), GetType(String))
            Util.AtribuirValorObjeto(Cod_Documento, row("COD_DOCUMENTO"), GetType(String))
            Util.AtribuirValorObjeto(Cod_Cliente, row("COD_CLIENTE"), GetType(String))
            Util.AtribuirValorObjeto(Especie_Descripcion, row("DESCRIPCION"), GetType(String))
            Util.AtribuirValorObjeto(Num_Importe, row("NUM_IMPORTE"), GetType(Decimal))
            Util.AtribuirValorObjeto(Nel_Cantidad, row("NEL_CANTIDAD"), GetType(Integer))
            Util.AtribuirValorObjeto(Bol_Disponible, row("BOL_DISPONIBLE"), GetType(Int16))
            Util.AtribuirValorObjeto(Cod_Estado_Documento, row("COD_ESTADO_DOCUMENTO"), GetType(String))
            Util.AtribuirValorObjeto(Cod_Tipo_Movimiento, row("COD_TIPO_MOVIMIENTO"), GetType(String))
            Util.AtribuirValorObjeto(Cod_Nivel_Detalle, row("COD_NIVEL_DETALLE"), GetType(String))
            Util.AtribuirValorObjeto(Cod_Tipo_Efectivo_Total, row("COD_TIPO_EFECTIVO_TOTAL"), GetType(String))
            Util.AtribuirValorObjeto(Fyh_Transaccion, row("FYH_TRANSACCION"), GetType(DateTime))
        End Sub

#End Region

    End Class

End Namespace