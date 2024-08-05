Imports Prosegur.Genesis.Comon.Atributos

Namespace RecepcionyEnvio

    Public Class Enumeradores

        ''' <summary>
        ''' Enumeração dos possíveis estado de uma OT recebida do Sol
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum TipoOT
            <ValorEnum("REC")>
            Recogida
            <ValorEnum("ENT")>
            Entrega
            <ValorEnum("ABA")>
            AbastecimientoATMMAquina
            <ValorEnum("REM")>
            RecogidaATMMaquina
            <ValorEnum("EME")>
            FlotanteEmergencial
        End Enum

        ''' <summary>
        ''' Enumeração das possíveis situações da remessa
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum SituacionRemesa
            <ValorEnum("PESOL")>
            PendienteEnvioSol
            <ValorEnum("PESALDOS")>
            PendienteGravacionSaldos
            <ValorEnum("NOENTREGUE")>
            NoEntregue
        End Enum

        ''' <summary>
        ''' Enumeração das possíveis estados da OT
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum TipoEstado
            <ValorEnum("1")>
            EnEjecucion
        End Enum

        ''' <summary>
        ''' Enumeração das possíveis emplegos da OT
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum TipoEmpleo
            <ValorEnum("2")>
            Defaut
        End Enum

        ''' <summary>
        ''' Enumeração dos possíveis localizações da OT
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum TipoLocalizacion
            <ValorEnum("0")>
            Prosegur
            <ValorEnum("1")>
            EnRuta
            <ValorEnum("2")>
            Cliente
        End Enum

        ''' <summary>
        ''' Enumeração dos possíveis localizações da OT
        ''' </summary>
        ''' <remarks></remarks>
        Public Class Errores

            Public Enum buscarProgramacionServicios
                <ValorEnum("IBPS000")>
                IBPS000
                <ValorEnum("IBPS001")>
                IBPS001
                <ValorEnum("IBPS002")>
                IBPS002
                <ValorEnum("IBPS003")>
                IBPS003
                <ValorEnum("IBPS004")>
                IBPS004
                <ValorEnum("IBPS005")>
                IBPS005
            End Enum

            Public Enum grabarDocumento
                <ValorEnum("IGD000")>
                IGD000
                <ValorEnum("IGD001")>
                IGD001
                <ValorEnum("IGD002")>
                IGD002
                <ValorEnum("IGD003")>
                IGD003
                <ValorEnum("IGD004")>
                IGD004
                <ValorEnum("IGD005")>
                IGD005
                <ValorEnum("IGD006")>
                IGD006
                <ValorEnum("IGD007")>
                IGD007
                <ValorEnum("IGD008")>
                IGD008
                <ValorEnum("IGD009")>
                IGD009
                <ValorEnum("IGD010")>
                IGD010
                <ValorEnum("IGD011")>
                IGD011
                <ValorEnum("IGD012")>
                IGD012
                <ValorEnum("IGD013")>
                IGD013
                <ValorEnum("IGD014")>
                IGD014
                <ValorEnum("IGD015")>
                IGD015
                <ValorEnum("IGD016")>
                IGD016
                <ValorEnum("IGD017")>
                IGD017
                <ValorEnum("IGD018")>
                IGD018
                <ValorEnum("IGD019")>
                IGD019
                <ValorEnum("IGD020")>
                IGD020
                <ValorEnum("IGD021")>
                IGD021
                <ValorEnum("IGD022")>
                IGD022
                <ValorEnum("IGD023")>
                IGD023
                <ValorEnum("IGD024")>
                IGD024
            End Enum

            Public Enum borrarDocumento
                <ValorEnum("IBD000")>
                IBD000
                <ValorEnum("IBD001")>
                IBD001
                <ValorEnum("IBD002")>
                IBD002
                <ValorEnum("IBD003")>
                IBD003
                <ValorEnum("IBD004")>
                IBD004

            end enum

            Public Enum LiberarBoveda
                <ValorEnum("ILB000")>
                ILB000
                <ValorEnum("ILB001")>
                ILB001
                <ValorEnum("ILB002")>
                ILB002
                <ValorEnum("ILB003")>
                ILB003
                <ValorEnum("ILB004")>
                ILB004
            End Enum

        End Class

    End Class

End Namespace