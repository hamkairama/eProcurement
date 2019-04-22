Imports System.Web.Optimization

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Protected Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
    End Sub

    'Protected Sub Application_Error(sender As Object, e As EventArgs)
    '    Dim exception As Exception = Server.GetLastError()
    '    Dim httpException As HttpException = TryCast(exception, HttpException)
    'End Sub
End Class
