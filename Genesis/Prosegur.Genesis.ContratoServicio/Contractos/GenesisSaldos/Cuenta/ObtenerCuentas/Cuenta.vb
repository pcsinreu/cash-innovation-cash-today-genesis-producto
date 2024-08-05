Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.Cuenta.ObtenerCuentas

    <XmlType(Namespace:="urn:ObtenerCuentas")> _
    <XmlRoot(Namespace:="urn:ObtenerCuentas")> _
    <Serializable()>
    Public Class DatosCuenta

        ''' <summary>
        ''' Código de Cliente
        ''' </summary>
        Public Property Cliente As Valor

        ''' <summary>
        ''' Código de Sub Cliente.
        ''' </summary>
        Public Property SubCliente As Valor

        ''' <summary>
        ''' Código de Punto de Servicio
        ''' </summary>
        Public Property PuntoServicio As Valor

        ''' <summary>
        ''' Código de la Delegación
        ''' </summary>
        Public Property Delegacion As Valor

        ''' <summary>
        ''' Código de la Planta
        ''' </summary>
        Public Property Planta As Valor

        ''' <summary>
        ''' Código de Sector
        ''' </summary>
        Public Property Sector As Valor

        ''' <summary>
        ''' Código de Canal
        ''' </summary>
        Public Property Canal As Valor

        ''' <summary>
        ''' Código Sub Canal
        ''' </summary>
        Public Property SubCanal As Valor

        Sub New()
            Me.Cliente = New Valor
            Me.SubCliente = New Valor
            Me.PuntoServicio = New Valor
            Me.Canal = New Valor
            Me.SubCanal = New Valor
            Me.Delegacion = New Valor
            Me.Planta = New Valor
            Me.Sector = New Valor
        End Sub

    End Class

End Namespace
