Imports System.DirectoryServices.AccountManagement
Imports System.DirectoryServices
Imports eProcurementApps.Models
Imports System.Configuration

Public Class ActiveDirectory
    Public Shared Function GetActiveDirList() As List(Of USER_HELPER)
        Dim lUser_helper As New List(Of USER_HELPER)

        Dim context = New PrincipalContext(ContextType.Domain, Nothing)
        Dim userPrin = New UserPrincipal(context)
        userPrin.Enabled = True
        Dim searcher = New PrincipalSearcher(userPrin)

        For Each result In searcher.FindAll()
            Dim de As DirectoryEntry = TryCast(result.GetUnderlyingObject(), DirectoryEntry)
            If de.Properties("mail").Value IsNot Nothing Then
                Dim user_helper As New USER_HELPER
                user_helper.USER_ID = de.Properties("samAccountName").Value.ToString()
                user_helper.EMAIL = de.Properties("mail").Value.ToString()

                lUser_helper.Add(user_helper)
            End If
        Next

        Return lUser_helper
    End Function

    Public Shared Function GetActiveDir2(user_id As String) As List(Of USER_HELPER)
        Dim yourDomain As New PrincipalContext(ContextType.Domain)
        Dim lUser_helper As New List(Of USER_HELPER)
        If user_id <> "" Then
            Dim domainName As String = ConfigurationSettings.AppSettings("ActiveDirectoryServer")
            Dim connection = "LDAP://" + domainName
            Dim user_id_log As String = CurrentUser.GetCurrentUserId
            Dim password As String = CurrentUser.GetCurrentPassword
            Dim dEntry = New DirectoryEntry(connection, user_id_log, password)
            Dim dssearch = New DirectorySearcher(dEntry)

            dssearch.Filter = "(sAMAccountName=" + user_id + ")"

            Dim sresult As SearchResult
            sresult = dssearch.FindOne()

            If sresult IsNot Nothing Then
                Dim dsresult As New DirectoryEntry
                dsresult = sresult.GetDirectoryEntry()
                Dim user_helper As New USER_HELPER

                user_helper.USER_ID = If(dsresult.Properties("sAMAccountName")(0) Is Nothing, "", dsresult.Properties("sAMAccountName")(0).ToString())
                user_helper.USER_NAME = If(dsresult.Properties("displayName")(0) Is Nothing, "", dsresult.Properties("displayName")(0).ToString())
                user_helper.EMAIL = If(dsresult.Properties("mail")(0) Is Nothing, "", dsresult.Properties("mail")(0).ToString())
                user_helper.DEPARTMENT = If(dsresult.Properties("distinguishedName")(0) Is Nothing, "", dsresult.Properties("distinguishedName")(0).ToString())

                lUser_helper.Add(user_helper)
            End If
        End If


        Return lUser_helper
    End Function

    Public Shared Function GetActiveDir(user_id As String) As List(Of USER_HELPER)
        Dim yourDomain As New PrincipalContext(ContextType.Domain)
        Dim lUser_helper As New List(Of USER_HELPER)
        If user_id <> "" Then
            'Dim connection = ConfigurationManager.ConnectionStrings("eProcurementEntities").ToString()
            Dim domainName As String = ConfigurationSettings.AppSettings("ActiveDirectoryServer")
            Dim connection = "LDAP://" + domainName
            Dim user_id_log As String = CurrentUser.GetCurrentUserId
            Dim password As String = CurrentUser.GetCurrentPassword
            Dim dEntry = New DirectoryEntry(connection, user_id_log, password)
            Dim dssearch = New DirectorySearcher(dEntry)

            dssearch.Filter = "(&(sAMAccountName=" + user_id + "*)" + "(objectCategory=person)" + ")"
            Dim sresult As SearchResultCollection
            sresult = dssearch.FindAll()

            If sresult.Count > 0 Then
                For Each result In sresult
                    Dim dsresult As New DirectoryEntry
                    dsresult = result.GetDirectoryEntry()
                    If dsresult.Properties("mail").Value IsNot Nothing Then
                        Dim user_helper As New USER_HELPER

                        user_helper.USER_ID = If(dsresult.Properties("sAMAccountName")(0) Is Nothing, "", dsresult.Properties("sAMAccountName")(0).ToString())
                        user_helper.USER_NAME = If(dsresult.Properties("displayName")(0) Is Nothing, "", dsresult.Properties("displayName")(0).ToString())
                        user_helper.EMAIL = If(dsresult.Properties("mail")(0) Is Nothing, "", dsresult.Properties("mail")(0).ToString())
                        user_helper.DEPARTMENT = If(dsresult.Properties("distinguishedName")(0) Is Nothing, "", dsresult.Properties("distinguishedName")(0).ToString())

                        lUser_helper.Add(user_helper)
                    End If
                Next
            End If
        End If


        Return lUser_helper
    End Function

    Public Shared Function GetActiveDirByEmail(email As String) As List(Of USER_HELPER)
        Dim yourDomain As New PrincipalContext(ContextType.Domain)
        Dim lUser_helper As New List(Of USER_HELPER)
        If email <> "" Then
            'Dim connection = ConfigurationManager.ConnectionStrings("eProcurementEntities").ToString()
            Dim domainName As String = ConfigurationSettings.AppSettings("ActiveDirectoryServer")
            Dim connection = "LDAP://" + domainName
            Dim user_id_log As String = CurrentUser.GetCurrentUserId
            Dim password As String = CurrentUser.GetCurrentPassword
            Dim dEntry = New DirectoryEntry(connection, user_id_log, password)
            Dim dssearch = New DirectorySearcher(dEntry)
            'Dim dssearch = New DirectorySearcher(connection)

            'dssearch.Filter = "(&(mail=" + "*" + email + "*)" + "(objectCategory=person)" + ")"
            dssearch.Filter = "(&(mail=" + email + "*)" + "(objectCategory=person)" + ")"
            Dim sresult As SearchResultCollection
            sresult = dssearch.FindAll()

            If sresult.Count > 0 Then
                For Each result In sresult
                    Dim dsresult As New DirectoryEntry
                    dsresult = result.GetDirectoryEntry()
                    If dsresult.Properties("mail").Value IsNot Nothing Then
                        Dim user_helper As New USER_HELPER

                        user_helper.USER_ID = If(dsresult.Properties("sAMAccountName")(0) Is Nothing, "", dsresult.Properties("sAMAccountName")(0).ToString())
                        user_helper.USER_NAME = If(dsresult.Properties("displayName")(0) Is Nothing, "", dsresult.Properties("displayName")(0).ToString())
                        user_helper.EMAIL = If(dsresult.Properties("mail")(0) Is Nothing, "", dsresult.Properties("mail")(0).ToString())
                        user_helper.DEPARTMENT = If(dsresult.Properties("distinguishedName")(0) Is Nothing, "", dsresult.Properties("distinguishedName")(0).ToString())

                        lUser_helper.Add(user_helper)
                    End If
                Next
            End If
        End If


        Return lUser_helper
    End Function

    Public Shared Function GetActiveDirOne(user_id As String) As List(Of USER_HELPER)
        Dim yourDomain As New PrincipalContext(ContextType.Domain)
        Dim lUser_helper As New List(Of USER_HELPER)
        If user_id <> "" Then
            Dim domainName As String = ConfigurationSettings.AppSettings("ActiveDirectoryServer")
            Dim connection = "LDAP://" + domainName
            Dim user_id_log As String = CurrentUser.GetCurrentUserId
            Dim password As String = CurrentUser.GetCurrentPassword
            Dim dEntry = New DirectoryEntry(connection, user_id_log, password)
            Dim dssearch = New DirectorySearcher(dEntry)

            dssearch.Filter = "(&(sAMAccountName=" + user_id + ")" + "(objectCategory=person)" + ")"
            Dim sresult As SearchResultCollection
            sresult = dssearch.FindAll()

            If sresult.Count > 0 Then
                For Each result In sresult
                    If IsAccountLocked(result) Then
                        Dim user_helper_is_locked As New USER_HELPER
                        user_helper_is_locked.IS_LOCKED = True
                        user_helper_is_locked.LOCKED_DATE = DateTime.FromFileTime(Long.Parse(result.Properties("lockoutTime")(0)))
                        lUser_helper.Add(user_helper_is_locked)
                        Exit For
                    Else
                        Dim dsresult As New DirectoryEntry
                        dsresult = result.GetDirectoryEntry()

                        If dsresult.Properties("mail").Value IsNot Nothing Then
                            Dim user_helper As New USER_HELPER

                            user_helper.USER_ID = If(dsresult.Properties("sAMAccountName")(0) Is Nothing, "", dsresult.Properties("sAMAccountName")(0).ToString())
                            user_helper.USER_NAME = If(dsresult.Properties("displayName")(0) Is Nothing, "", dsresult.Properties("displayName")(0).ToString())
                            user_helper.EMAIL = If(dsresult.Properties("mail")(0) Is Nothing, "", dsresult.Properties("mail")(0).ToString())
                            user_helper.DEPARTMENT = If(dsresult.Properties("distinguishedName")(0) Is Nothing, "", dsresult.Properties("distinguishedName")(0).ToString())

                            lUser_helper.Add(user_helper)
                        End If
                    End If
                Next
            End If
        End If


        Return lUser_helper
    End Function

    Private Shared Function IsAccountLocked(user As SearchResult) As Boolean
        'if they have a lockoutTime
        If user.Properties.Contains("lockoutTime") Then
            Dim lockout = user.Properties("lockoutTime")(0)

            'check to see if it's not already unlocked
            If lockout <> 0 Then
                Return True
            End If
        End If
        Return False
    End Function


    Public Shared Function IsAuthenticated(user_id As String, password As String) As USER_HELPER
        Dim _path As String = ""
        Dim _filterAttribute As String = ""
        Dim _department As String = ""
        Dim domainName As String = ConfigurationSettings.AppSettings("ActiveDirectoryServer")
        Dim user_helper As New USER_HELPER
        Dim lUser_helper As New List(Of USER_HELPER)
        lUser_helper = GetActiveDirOne(user_id)

        If lUser_helper.Count > 0 Then
            If lUser_helper(0).IS_LOCKED Then
                user_helper.IS_LOCKED = lUser_helper(0).IS_LOCKED
                user_helper.LOCKED_DATE = lUser_helper(0).LOCKED_DATE
            Else
                Dim yourDomain As New PrincipalContext(ContextType.Domain)
                Dim domainAndUsername As String = Convert.ToString(domainName & Convert.ToString("\")) & user_id
                Dim entry As New DirectoryEntry(_path, domainAndUsername, password)
                Try
                    ' Bind to the native AdsObject to force authentication.
                    Dim obj As [Object] = entry.NativeObject
                    Dim search As New DirectorySearcher(entry)
                    search.Filter = (Convert.ToString("(SAMAccountName=") & user_id) + ")"
                    search.PropertiesToLoad.Add("cn")
                    search.PropertiesToLoad.Add("mail")
                    Dim result As SearchResult = search.FindOne()

                    If result IsNot Nothing Then
                        user_helper.USER_ID = user_id
                        user_helper.USER_NAME = DirectCast(result.Properties("cn")(0), [String])
                        user_helper.EMAIL = DirectCast(result.Properties("mail")(0), [String])
                        Dim directoryEntryPath As String() = result.Path.Split(","c)
                        For Each splitedPath In directoryEntryPath
                            Dim eleiments As String() = splitedPath.Split("="c)
                            'If the 1st element of the array is "OU" string then get the 2dn element
                            If eleiments(0).Trim() = "OU" Then
                                If eleiments(1).Trim() <> "Users" Then
                                    user_helper.DEPARTMENT = eleiments(1).Trim()
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                Catch ex As Exception
                    Return user_helper
                End Try
            End If
        End If

        Return user_helper
    End Function


End Class
