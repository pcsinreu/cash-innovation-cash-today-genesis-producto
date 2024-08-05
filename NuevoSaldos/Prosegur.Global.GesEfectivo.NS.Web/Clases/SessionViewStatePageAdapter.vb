Public Class SessionViewStatePageAdapter
    Inherits System.Web.UI.Adapters.PageAdapter

    Public Overrides Function GetStatePersister() As System.Web.UI.PageStatePersister
        Return New SessionPageStatePersister(Me.Page)
    End Function

End Class
