Imports System.Web.Mvc
Imports System.Runtime.CompilerServices
Imports System.Linq.Expressions
Imports System.Web.UI.WebControls
Imports System.Web.WebPages
Imports System.Web.Routing
Imports System.Web.Mvc.Html

Imports System.Collections
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Web.Mvc.Properties

Public Module LabelExtensions
    <Extension>
    Public Function SpanFor(Of TModel, TValue)(helper As HtmlHelper(Of TModel), expression As Expression(Of Func(Of TModel, TValue)), Optional htmlAttributes As Object = Nothing) As MvcHtmlString
        Dim valueGetter = expression.Compile()
        Dim value = valueGetter(helper.ViewData.Model)
        'Dim value = "test"

        Dim span As New TagBuilder("span")
        span.MergeAttributes(New RouteValueDictionary(htmlAttributes))
        If value IsNot Nothing Then
            span.SetInnerText(value.ToString())
        End If

        Return MvcHtmlString.Create(span.ToString())
    End Function

    'Public Function EditorFor(Of TModel, TValue)(html As HtmlHelper(Of TModel), expression As Expression(Of Func(Of TModel, TValue)), templateName As String, htmlFieldName As String) As MvcHtmlString
    '    Dim valueGetter = expression.Compile()
    '    Dim value = valueGetter(html.ViewData.Model)
    '    Dim value = "test"

    '    Dim span As New TagBuilder("span")
    '    span.MergeAttributes(New RouteValueDictionary(htmlAttributes))
    '    If value IsNot Nothing Then
    '        span.SetInnerText(value.ToString())
    '    End If

    '    Return MvcHtmlString.Create(span.ToString())
    'End Function


End Module

'' Decompiled with JetBrains decompiler
'' Type: System.Web.Mvc.Html.EditorExtensions
'' Assembly: System.Web.Mvc, Version=5.2.3.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
'' MVID: CC73190B-AB9D-435C-8315-10FF295C572A
'' Assembly location: C:\Users\hamkair\Desktop\System.Web.Mvc.dll

'Imports System.Linq.Expressions
'Imports System.Web.UI.WebControls

'Namespace System.Web.Mvc.Html
'    Public NotInheritable Class EditorExtensions
'        Private Sub New()
'        End Sub
'        <System.Runtime.CompilerServices.Extension>
'        Public Shared Function Editor(html As HtmlHelper, expression As String) As MvcHtmlString
'            Return TemplateHelpers.Template(html, expression, DirectCast(Nothing, String), DirectCast(Nothing, String), DataBoundControlMode.Edit, DirectCast(Nothing, Object))
'        End Function

'        <System.Runtime.CompilerServices.Extension>
'        Public Shared Function Editor(html As HtmlHelper, expression As String, additionalViewData As Object) As MvcHtmlString
'            Return TemplateHelpers.Template(html, expression, DirectCast(Nothing, String), DirectCast(Nothing, String), DataBoundControlMode.Edit, additionalViewData)
'        End Function

'        <System.Runtime.CompilerServices.Extension>
'        Public Shared Function Editor(html As HtmlHelper, expression As String, templateName As String) As MvcHtmlString
'            Return TemplateHelpers.Template(html, expression, templateName, DirectCast(Nothing, String), DataBoundControlMode.Edit, DirectCast(Nothing, Object))
'        End Function

'        <System.Runtime.CompilerServices.Extension>
'        Public Shared Function Editor(html As HtmlHelper, expression As String, templateName As String, additionalViewData As Object) As MvcHtmlString
'            Return TemplateHelpers.Template(html, expression, templateName, DirectCast(Nothing, String), DataBoundControlMode.Edit, additionalViewData)
'        End Function

'        <System.Runtime.CompilerServices.Extension>
'        Public Shared Function Editor(html As HtmlHelper, expression As String, templateName As String, htmlFieldName As String) As MvcHtmlString
'            Return TemplateHelpers.Template(html, expression, templateName, htmlFieldName, DataBoundControlMode.Edit, DirectCast(Nothing, Object))
'        End Function

'        <System.Runtime.CompilerServices.Extension>
'        Public Shared Function Editor(html As HtmlHelper, expression As String, templateName As String, htmlFieldName As String, additionalViewData As Object) As MvcHtmlString
'            Return TemplateHelpers.Template(html, expression, templateName, htmlFieldName, DataBoundControlMode.Edit, additionalViewData)
'        End Function

'        <System.Runtime.CompilerServices.Extension>
'        Public Shared Function EditorFor(Of TModel, TValue)(html As HtmlHelper(Of TModel), expression As Expression(Of Func(Of TModel, TValue))) As MvcHtmlString
'            Return html.TemplateFor(Of TModel, TValue)(expression, DirectCast(Nothing, String), DirectCast(Nothing, String), DataBoundControlMode.Edit, DirectCast(Nothing, Object))
'        End Function

'        <System.Runtime.CompilerServices.Extension>
'        Public Shared Function EditorFor(Of TModel, TValue)(html As HtmlHelper(Of TModel), expression As Expression(Of Func(Of TModel, TValue)), additionalViewData As Object) As MvcHtmlString
'            Return html.SpanFor(Of TModel, TValue)(expression, DirectCast(Nothing, String), DirectCast(Nothing, String), DataBoundControlMode.Edit, additionalViewData)
'        End Function

'        <System.Runtime.CompilerServices.Extension>
'        Public Shared Function EditorFor(Of TModel, TValue)(html As HtmlHelper(Of TModel), expression As Expression(Of Func(Of TModel, TValue)), templateName As String) As MvcHtmlString
'            Return html.TemplateFor(Of TModel, TValue)(expression, templateName, DirectCast(Nothing, String), DataBoundControlMode.Edit, DirectCast(Nothing, Object))
'        End Function

'        <System.Runtime.CompilerServices.Extension>
'        Public Shared Function EditorFor(Of TModel, TValue)(html As HtmlHelper(Of TModel), expression As Expression(Of Func(Of TModel, TValue)), templateName As String, additionalViewData As Object) As MvcHtmlString
'            Return html.TemplateFor(Of TModel, TValue)(expression, templateName, DirectCast(Nothing, String), DataBoundControlMode.Edit, additionalViewData)
'        End Function

'        <System.Runtime.CompilerServices.Extension>
'        Public Shared Function EditorFor(Of TModel, TValue)(html As HtmlHelper(Of TModel), expression As Expression(Of Func(Of TModel, TValue)), templateName As String, htmlFieldName As String) As MvcHtmlString
'            Return html.TemplateFor(Of TModel, TValue)(expression, templateName, htmlFieldName, DataBoundControlMode.Edit, DirectCast(Nothing, Object))
'        End Function

'        <System.Runtime.CompilerServices.Extension>
'        Public Shared Function EditorFor(Of TModel, TValue)(html As HtmlHelper(Of TModel), expression As Expression(Of Func(Of TModel, TValue)), templateName As String, htmlFieldName As String, additionalViewData As Object) As MvcHtmlString
'            Return html.TemplateFor(Of TModel, TValue)(expression, templateName, htmlFieldName, DataBoundControlMode.Edit, additionalViewData)
'        End Function

'        <System.Runtime.CompilerServices.Extension>
'        Public Shared Function EditorForModel(html As HtmlHelper) As MvcHtmlString
'            Return MvcHtmlString.Create(TemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, String.Empty, DirectCast(Nothing, String), DataBoundControlMode.Edit, DirectCast(Nothing, Object)))
'        End Function

'        <System.Runtime.CompilerServices.Extension>
'        Public Shared Function EditorForModel(html As HtmlHelper, additionalViewData As Object) As MvcHtmlString
'            Return MvcHtmlString.Create(TemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, String.Empty, DirectCast(Nothing, String), DataBoundControlMode.Edit, additionalViewData))
'        End Function

'        <System.Runtime.CompilerServices.Extension>
'        Public Shared Function EditorForModel(html As HtmlHelper, templateName As String) As MvcHtmlString
'            Return MvcHtmlString.Create(TemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, String.Empty, templateName, DataBoundControlMode.Edit, DirectCast(Nothing, Object)))
'        End Function

'        <System.Runtime.CompilerServices.Extension>
'        Public Shared Function EditorForModel(html As HtmlHelper, templateName As String, additionalViewData As Object) As MvcHtmlString
'            Return MvcHtmlString.Create(TemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, String.Empty, templateName, DataBoundControlMode.Edit, additionalViewData))
'        End Function

'        <System.Runtime.CompilerServices.Extension>
'        Public Shared Function EditorForModel(html As HtmlHelper, templateName As String, htmlFieldName As String) As MvcHtmlString
'            Return MvcHtmlString.Create(TemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, htmlFieldName, templateName, DataBoundControlMode.Edit, DirectCast(Nothing, Object)))
'        End Function

'        <System.Runtime.CompilerServices.Extension>
'        Public Shared Function EditorForModel(html As HtmlHelper, templateName As String, htmlFieldName As String, additionalViewData As Object) As MvcHtmlString
'            Return MvcHtmlString.Create(TemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, htmlFieldName, templateName, DataBoundControlMode.Edit, additionalViewData))
'        End Function
'    End Class
'End Namespace

''=======================================================
''Service provided by Telerik (www.telerik.com)
''Conversion powered by NRefactory.
''Twitter: @telerik
''Facebook: facebook.com/telerik
''=======================================================


