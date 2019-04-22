Imports eProcurementApps.Helpers

Public Class HOMEController
    Inherits System.Web.Mvc.Controller

    <AuthorizeLogin()>
    Function Index() As ActionResult
        'Session("USER_ID") = System.Security.Principal.WindowsIdentity.GetCurrent().Name


        Return View()
    End Function

    Function FormElements() As ActionResult
        Return View()
    End Function

    Function Tables() As ActionResult
        Return View()
    End Function

    Function NestableList() As ActionResult
        Return View()
    End Function

    Function UserProfile() As ActionResult
        Return View()
    End Function
    Function TreeViewCheckList() As ActionResult
        Return View()
    End Function
    Function TreeViewFolder() As ActionResult
        Return View()
    End Function
    Function TreeView() As ActionResult
        Return View()
    End Function
    Function Bootstrap() As ActionResult
        Return View()
    End Function
    Function About() As ActionResult
        ViewData("Message") = "Your application description page."

        Return View()
    End Function

    Function Contact() As ActionResult
        ViewData("Message") = "Your contact page."

        Return View()
    End Function


End Class
