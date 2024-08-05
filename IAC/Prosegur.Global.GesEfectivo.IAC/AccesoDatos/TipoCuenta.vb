Imports Prosegur.DbHelper
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboTiposCuenta
Imports Prosegur.Framework.Dicionario.Tradutor

Public Class TipoCuenta
    Public Shared Function GetComboTiposCuentas() As TipoDeCuentaColeccion
        ' Crear objeto colección de tipo de cuentas
        Dim objTiposDeCuentas As New ContractoServicio.Utilidad.GetComboTiposCuenta.TipoDeCuentaColeccion

        ' Crear comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' Obtener consulta
        comando.CommandText = Util.PrepararQuery(My.Resources.getComboTiposDeCuenta.ToString)
        comando.CommandType = CommandType.Text

        ' Ejecutar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' Si encontró algún registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            Dim indice As Integer = 0
            'Dim primerElemento As New TipoDeCuenta()
            'primerElemento.Indice = 0
            'primerElemento.Descripcion = "gen_ddl_selecione" 'Indica el primer elemento para que "seleccione" en el combo
            'objTiposDeCuentas.Add(primerElemento)

            'indice = indice + 1
            ' Recorre todos los registros
            For Each dr As DataRow In dtQuery.Rows

                ' Adicionamos un nuevo tipo a la colección
                objTiposDeCuentas.Add(PoblarTiposDeCuentas(dr, indice))

                indice += 1

            Next

        End If

        ' retornar colección de Tipos de Cuentas
        Return objTiposDeCuentas

    End Function

    Private Shared Function PoblarTiposDeCuentas(dr As DataRow, indice As Integer) As TipoDeCuenta
        Dim objTipoDeCuenta As New TipoDeCuenta

        objTipoDeCuenta.Descripcion = dr("COD_TIPO_CUENTA_BANCARIA").ToString()
        objTipoDeCuenta.Indice = indice

        Return objTipoDeCuenta

    End Function
End Class
