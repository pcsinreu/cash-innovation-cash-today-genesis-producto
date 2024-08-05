''' <summary>
''' Constantes acceso datos
''' </summary>
''' <remarks></remarks>
''' <history>
''' [kasantos] 20/04/2013 Criado
''' </history>
Public Class Constantes

    ' conexão gestão de efetivo
    Public Const CONEXAO_GE As String = "GENESIS"

    Public Shared MapeoEntidadesCodigoAjeno As New List(Of MapeoEntidadeCodigoAjeno)({
        New MapeoEntidadeCodigoAjeno With {.CodTipoTablaGenesis = "CLIENTE", .Entidade = "GEPR_TCLIENTE", .OidTablaGenesis = "OID_CLIENTE", .CodTablaGenesis = "COD_CLIENTE", .DesTablaGenesis = "DES_CLIENTE"},
        New MapeoEntidadeCodigoAjeno With {.CodTipoTablaGenesis = "SUBCLIENTE", .Entidade = "GEPR_TSUBCLIENTE", .OidTablaGenesis = "OID_SUBCLIENTE", .CodTablaGenesis = "COD_SUBCLIENTE", .DesTablaGenesis = "DES_SUBCLIENTE"},
        New MapeoEntidadeCodigoAjeno With {.CodTipoTablaGenesis = "PUNTO_SERVICIO", .Entidade = "GEPR_TPUNTO_SERVICIO", .OidTablaGenesis = "OID_PTO_SERVICIO", .CodTablaGenesis = "COD_PTO_SERVICIO", .DesTablaGenesis = "DES_PTO_SERVICIO"},
        New MapeoEntidadeCodigoAjeno With {.CodTipoTablaGenesis = "PLANTA", .Entidade = "GEPR_TPLANTA", .OidTablaGenesis = "OID_PLANTA", .CodTablaGenesis = "COD_PLANTA", .DesTablaGenesis = "DES_PLANTA"},
        New MapeoEntidadeCodigoAjeno With {.CodTipoTablaGenesis = "CANAL", .Entidade = "GEPR_TCANAL", .OidTablaGenesis = "OID_CANAL", .CodTablaGenesis = "COD_CANAL", .DesTablaGenesis = "DES_CANAL"},
        New MapeoEntidadeCodigoAjeno With {.CodTipoTablaGenesis = "SUBCANAL", .Entidade = "GEPR_TSUBCANAL", .OidTablaGenesis = "OID_SUBCANAL", .CodTablaGenesis = "COD_SUBCANAL", .DesTablaGenesis = "DES_SUBCANAL"},
        New MapeoEntidadeCodigoAjeno With {.CodTipoTablaGenesis = "TIPO_SECTOR", .Entidade = "GEPR_TTIPO_SECTOR", .OidTablaGenesis = "OID_TIPO_SECTOR", .CodTablaGenesis = "COD_TIPO_SECTOR", .DesTablaGenesis = "DES_TIPO_SECTOR"},
        New MapeoEntidadeCodigoAjeno With {.CodTipoTablaGenesis = "SECTOR", .Entidade = "GEPR_TSECTOR", .OidTablaGenesis = "OID_SECTOR", .CodTablaGenesis = "COD_SECTOR", .DesTablaGenesis = "DES_SECTOR"},
        New MapeoEntidadeCodigoAjeno With {.CodTipoTablaGenesis = "GRUPO_CLIENTE", .Entidade = "GEPR_TGRUPO_CLIENTE", .OidTablaGenesis = "OID_GRUPO_CLIENTE", .CodTablaGenesis = "COD_GRUPO_CLIENTE", .DesTablaGenesis = "DES_GRUPO_CLIENTE"},
        New MapeoEntidadeCodigoAjeno With {.CodTipoTablaGenesis = "DELEGACION", .Entidade = "GEPR_TDELEGACION", .OidTablaGenesis = "OID_DELEGACION", .CodTablaGenesis = "COD_DELEGACION", .DesTablaGenesis = "DES_DELEGACION"},
        New MapeoEntidadeCodigoAjeno With {.CodTipoTablaGenesis = "DIVISA", .Entidade = "GEPR_TDIVISA", .OidTablaGenesis = "OID_DIVISA", .CodTablaGenesis = "COD_DIVISA", .DesTablaGenesis = "DES_DIVISA"},
        New MapeoEntidadeCodigoAjeno With {.CodTipoTablaGenesis = "DENOMINACION", .Entidade = "GEPR_TDENOMINACION", .OidTablaGenesis = "OID_DENOMINACION", .CodTablaGenesis = "COD_DENOMINACION", .DesTablaGenesis = "DES_DENOMINACION"},
        New MapeoEntidadeCodigoAjeno With {.CodTipoTablaGenesis = "MAQUINA", .Entidade = "SAPR_TMAQUINA", .OidTablaGenesis = "OID_MAQUINA", .CodTablaGenesis = "COD_IDENTIFICACION", .DesTablaGenesis = ""}
    })

    Public Const ConstanteCliente As String = "GEPR_TCLIENTE"
    Public Const ConstanteSubcliente As String = "GEPR_TSUBCLIENTE"
    Public Const ConstantePuntoServicio As String = "GEPR_TPUNTO_SERVICIO"
    Public Const ConstantePlanta As String = "GEPR_TPLANTA"
    Public Const ConstanteCanal As String = "GEPR_TCANAL"
    Public Const ConstanteSubcanal As String = "GEPR_TSUBCANAL"
    Public Const ConstanteTipoSector As String = "GEPR_TTIPO_SECTOR"
    Public Const ConstanteSector As String = "GEPR_TSECTOR"
    Public Const ConstanteGrupoCliente As String = "GEPR_TGRUPO_CLIENTE"
    Public Const ConstanteDelegacion As String = "GEPR_TDELEGACION"
    Public Const ConstanteDivisa As String = "GEPR_TDIVISA"
    Public Const ConstantePROFAT As String = "PROFAT"
    Public Const ConstanteDenominacion As String = "GEPR_TDENOMINACION"
    Public Const ConstanteMaquina As String = "SAPR_TMAQUINA"

End Class