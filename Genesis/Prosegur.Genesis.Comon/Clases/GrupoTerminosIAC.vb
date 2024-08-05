Imports System.Collections.ObjectModel

Namespace Clases

    <Serializable()>
    Public NotInheritable Class GrupoTerminosIAC
        Inherits BindableBase

#Region "[VARIAVEIS]"
        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _Observacion As String
        Private _CopiarDeclarados As Boolean
        Private _EstaActivo As Boolean
        Private _EsInvisible As Boolean
        Private _CodigoUsuario As String
        Private _FechaHoraActualizacion As String
        Private _TerminosIAC As ObservableCollection(Of TerminoIAC)

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodigoUsuario As String
            Get
                Return _CodigoUsuario
            End Get
            Set(value As String)
                SetProperty(_CodigoUsuario, value, "CodigoUsuario")
            End Set
        End Property
        Public Property CopiarDeclarados As Boolean
            Get
                Return _CopiarDeclarados
            End Get
            Set(value As Boolean)
                SetProperty(_CopiarDeclarados, value, "CopiarDeclarados")
            End Set
        End Property

        Public Property EsInvisible As Boolean
            Get
                Return _EsInvisible
            End Get
            Set(value As Boolean)
                SetProperty(_EsInvisible, value, "EsInvisible")
            End Set
        End Property
        Public Property EstaActivo As Boolean
            Get
                Return _EstaActivo
            End Get
            Set(value As Boolean)
                SetProperty(_EstaActivo, value, "EstaActivo")
            End Set
        End Property
        Public Property FechaHoraActualizacion As String
            Get
                Return _FechaHoraActualizacion
            End Get
            Set(value As String)
                SetProperty(_FechaHoraActualizacion, value, "FechaHoraActualizacion")
            End Set
        End Property
        Public Property Observacion As String
            Get
                Return _Observacion
            End Get
            Set(value As String)
                SetProperty(_Observacion, value, "Observacion")
            End Set
        End Property

        Public Property Codigo As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                SetProperty(_Codigo, value, "Codigo")
            End Set
        End Property

        Public Property Descripcion As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                SetProperty(_Descripcion, value, "Descripcion")
            End Set
        End Property

        Public Property Identificador As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                SetProperty(_Identificador, value, "Identificador")
            End Set
        End Property

        Public Property TerminosIAC As ObservableCollection(Of TerminoIAC)
            Get
                Return _TerminosIAC
            End Get
            Set(value As ObservableCollection(Of TerminoIAC))
                SetProperty(_TerminosIAC, value, "TerminosIAC")
            End Set
        End Property

#End Region

#Region "[METODOS]"

        Public Function RecuperarValorTerminosIAC(codigoTermino As String) As Object

            ' Verifica se existe a coleção de terminos
            If TerminosIAC IsNot Nothing Then

                ' Recupera o termino
                Dim objTermino As TerminoIAC = TerminosIAC.Where(Function(t) t.Codigo = codigoTermino).FirstOrDefault()

                ' Se encontou o termino
                If objTermino IsNot Nothing Then

                    ' Retorna o valor do termino
                    Return objTermino.Valor

                End If

            End If

            Return Nothing

        End Function

        Public Sub GrabarTerminosIAC(codigoTermino As String, valorTermino As String, Optional identificadorTermino As String = "")

            ' Se não existe terminos
            If TerminosIAC Is Nothing Then
                ' Cria uma nova lista de terminos
                TerminosIAC = New ObservableCollection(Of TerminoIAC)
            End If

            ' Verifica se existe a coleção de terminos
            If TerminosIAC IsNot Nothing Then

                ' Recupera o termino
                Dim objTermino As TerminoIAC = TerminosIAC.Where(Function(t) t.Codigo = codigoTermino).FirstOrDefault()

                ' Se encontou o termino
                If objTermino IsNot Nothing Then

                    If String.IsNullOrEmpty(objTermino.Identificador) Then
                        objTermino.Identificador = identificadorTermino
                    End If
                    ' Retorna o valor do termino
                    objTermino.Valor = valorTermino

                Else
                    ' Adiciona o termino
                    TerminosIAC.Add(New Comon.Clases.TerminoIAC With {.Codigo = codigoTermino, .Valor = valorTermino, .Identificador = identificadorTermino})
                End If

            End If

        End Sub

#End Region

    End Class

End Namespace
