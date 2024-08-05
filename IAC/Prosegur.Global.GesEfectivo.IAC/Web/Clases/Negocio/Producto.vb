Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Namespace Negocio

    ''' <summary>
    ''' Classe de negócio Producto
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  04/02/2011 criado
    ''' </history>
    <Serializable()> _
    Public Class Producto
        Inherits BaseEntidade

#Region "[VARIÁVEIS]"

        Private _oidProducto As String
        Private _desProducto As String
        Private _codProducto As String
        Private _desClaseBillete As String
        Private _numFactorCorreccion As Double
        Private _bolManual As Boolean
        Private _bolVigente As Boolean
        Private _codUsuario As String
        Private _fyhActualizacion As DateTime

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidProducto As String
            Get
                Return _oidProducto
            End Get
            Set(value As String)
                _oidProducto = value
            End Set
        End Property

        Public Property DesProducto As String
            Get
                Return _desProducto
            End Get
            Set(value As String)
                _desProducto = value
            End Set
        End Property

        Public Property CodProducto As String
            Get
                Return _codProducto
            End Get
            Set(value As String)
                _codProducto = value
            End Set
        End Property

        Public Property DesClaseBillete As String
            Get
                Return _desClaseBillete
            End Get
            Set(value As String)
                _desClaseBillete = value
            End Set
        End Property

        Public Property NumFactorCorreccion As Double
            Get
                Return _numFactorCorreccion
            End Get
            Set(value As Double)
                _numFactorCorreccion = value
            End Set
        End Property

        Public Property BolManual As Boolean
            Get
                Return _bolManual
            End Get
            Set(value As Boolean)
                _bolManual = value
            End Set
        End Property

        Public Property BolVigente As Boolean
            Get
                Return _bolVigente
            End Get
            Set(value As Boolean)
                _bolVigente = value
            End Set
        End Property

        Public Property CodUsuario As String
            Get
                Return _codUsuario
            End Get
            Set(value As String)
                _codUsuario = value
            End Set
        End Property

        Public Property FyhActualizacion As DateTime
            Get
                Return _fyhActualizacion
            End Get
            Set(value As DateTime)
                _fyhActualizacion = value
            End Set
        End Property

#End Region

#Region "[CONSTRUTORES]"

        Public Sub New()


        End Sub

        Public Sub New(Oid As String, Codigo As String, Descripcion As String)

            _oidProducto = Oid
            _desProducto = Descripcion
            _codProducto = Codigo

        End Sub

#End Region

#Region "[MÉTODOS]"

        ''' <summary>
        ''' Obtém valores para preencher uma combo 
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 04/02/2011 - Criado 
        ''' </history>
        Public Function ObtenerCombo() As List(Of Producto)

            Dim objProxy As New Comunicacion.ProxyUtilidad
            Dim objRespuesta As IAC.ContractoServicio.Utilidad.GetComboProductos.Respuesta

            ' obtém morfologias
            objRespuesta = objProxy.GetComboProductos()

            ' seta resposta para que os erros sejam tratados na tela
            MyBase.Respuesta = objRespuesta

            If objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

                ' se não houve erros, retorna morfologias
                Return ConvertToList(objRespuesta.Productos)

            Else

                ' se houve erros, retorna lista vazia
                Return New List(Of Producto)

            End If

        End Function


        Public Function ConvertToList(Productos As ContractoServicio.Utilidad.GetComboProductos.ProductoColeccion) As List(Of Producto)

            Dim lista As New List(Of Producto)
            Dim item As Producto

            If Productos Is Nothing Then
                Return lista
            End If

            For Each prod In Productos

                item = New Producto

                With item
                    .CodProducto = prod.Codigo
                    .DesProducto = prod.Descripcion
                    .DesClaseBillete = prod.DescripcionClaseBillete
                End With

                lista.Add(item)

            Next

            Return lista

        End Function

#End Region

    End Class

End Namespace
