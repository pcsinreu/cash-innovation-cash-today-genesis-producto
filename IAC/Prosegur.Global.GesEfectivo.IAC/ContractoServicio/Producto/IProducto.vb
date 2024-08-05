Public Interface IProducto

    ''' <summary>
    ''' Assinatura do método GetComboMaquinas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 26/01/2009 Created
    ''' </history>
    Function GetProductos(peticion As ContractoServicio.Producto.GetProductos.Peticion) As ContractoServicio.Producto.GetProductos.Respuesta

    ''' <summary>
    ''' Assinatura do método Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 05/02/2010 Created
    ''' </history>
    Function Test() As ContractoServicio.Test.Respuesta

End Interface
