Imports System.Security.Cryptography
Imports System.Text

Public Class Hashing
    Public Shared Function CreateSalt() As String
        Dim rng As New RNGCryptoServiceProvider()
        Dim buff As Byte() = New Byte(31) {}
        rng.GetBytes(buff)

        Return Convert.ToBase64String(buff)
    End Function

    Public Shared Function CreatePasswordHash(password As String) As String
        'string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha1");
        Dim sha1 As New SHA1Managed()
        Dim hash As Byte() = sha1.ComputeHash(Encoding.UTF8.GetBytes(password))
        Dim sb As New StringBuilder(hash.Length * 2)

        For Each b As Byte In hash
            ' can be "x2" if you want lowercase
            sb.Append(b.ToString("X2"))
        Next
        Return sb.ToString()
    End Function

    Public Shared Function CreatePasswordAndSaltHash(hashpassword As String, salt As String) As String
        Dim pwdAndSalt As String = [String].Concat(hashpassword, salt)
        Dim sha1 As New SHA1Managed()
        Dim hash As Byte() = sha1.ComputeHash(Encoding.UTF8.GetBytes(pwdAndSalt))
        Dim sb As New StringBuilder(hash.Length * 2)

        For Each b As Byte In hash
            ' can be "x2" if you want lowercase
            sb.Append(b.ToString("X2"))
        Next
        Return sb.ToString()
    End Function
End Class
