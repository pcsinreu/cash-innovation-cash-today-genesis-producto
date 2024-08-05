Imports System.Runtime.Serialization
Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon.Atributos

Namespace Contractos.Integracion.Comon

    <Serializable()>
    Public Class Enumeradores

        Public Enum Accion
            EnCurso = 0
            Confirmado = 1
            Aceptado = 2
        End Enum

        Public Enum TipoSaldo
            NoDisponible = 0
            Disponible = 1
            Ambos = 2
        End Enum

        Public Enum TipoCertificado
            NoCertificado = 0
            Certificado = 1
            Ambos = 2
        End Enum

        Public Enum TipoAcreditado
            NoAcreditado = 0
            Acreditado = 1
            Ambos = 2
        End Enum

        Public Enum TipoNotificado
            NoNotificado = 0
            Notificado = 1
            Ambos = 2
        End Enum

        Public Enum TipoNivel
            Cliente = 0
            SubCliente = 1
            Punto = 2
        End Enum

        Public Enum AccionABM
            <XmlEnumAttribute("Alta")>
            Alta = 0
            <XmlEnumAttribute("Modificar")>
            Modificar = 1
            <XmlEnumAttribute("Baja")>
            Baja = 2
        End Enum

        Public Enum AccionAB
            <EnumMember(Value:="Alta")>
            Alta = 0
            <EnumMember(Value:="Baja")>
            Baja = 2
        End Enum

        Public Enum TipoResultado
            Exito = 0
            Alerta = 1
            ErrorNegocio = 2
            ErrorAplicacion = 3
        End Enum


        Public Enum TipoCliente

            <ValorEnum("0")>
            [DEFAULT] = 0
            <ValorEnum("1")>
            BANCO = 1
        End Enum

        Public Enum AccionesModificarPeriodo
            Desbloquear = 1
        End Enum
    End Class

End Namespace

