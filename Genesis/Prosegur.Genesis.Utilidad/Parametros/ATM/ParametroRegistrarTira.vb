<Serializable()>
Public Class ParametroRegistrarTira
    Implements IParametroModulo

    ''' <summary>
    ''' Módulo que originou a requisição de uma tela.
    ''' </summary>
    Public Property Aplicacion As Prosegur.Genesis.Comon.Enumeradores.Aplicacion Implements IParametroModulo.Aplicacion

    Public Property PantallaOrigen As System.IntPtr Implements IParametroModulo.PantallaOrigen

    Public Property CodigoCliente As String

    Public Property CodigoSubcliente As String

    Public Property CodigoPuntoServicio As String

    Public Property IdentificadorTira As String

    Public Property Componente As Prosegur.Genesis.Comon.Enumeradores.ATM.Componente

End Class
