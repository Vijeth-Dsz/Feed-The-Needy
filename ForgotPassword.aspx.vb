Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Net.NetworkCredential
Imports System.Net

Partial Class ForgotPassword
    Inherits System.Web.UI.Page
    Dim cnn As New SqlConnection
    Dim cmd As New SqlCommand
    Dim dr As SqlDataReader
    Dim msg As New MailMessage

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        Dim pass As String = ""
        cnn = New SqlConnection("Data Source=DESKTOP-K4S3UFL\SQLEXPRESS;Initial Catalog=FTN;Integrated Security=True")
        cnn.Open()
        cmd = New SqlCommand("Select * from Register where  Email ='" & txtEmail.Text & "'", cnn)
        cmd.Connection = cnn
        cmd.Parameters.AddWithValue("Email", txtEmail.Text)

        dr = cmd.ExecuteReader()
        If txtEmail.Text = "" Then
            lbltxt.Text = "  *Enter your registered email"
        Else
            If dr.HasRows() Then
                While dr.Read()
                    pass = dr.Item(8).ToString
                End While
            End If

            cnn.Close()
            If String.IsNullOrEmpty(pass) Then
                lbltxt.Text = "Null or Empty"
            Else
                msg.From = New MailAddress("feedtheneedyftn@gmail.com")

                msg.To.Add(txtEmail.Text)
                msg.Subject = "Recover Your Password"
                msg.Body = ("Your Password is: " + pass)
                msg.IsBodyHtml = True

                Dim smt As New SmtpClient
                smt.Host = "smtp.gmail.com"

                Dim ntwd As New NetworkCredential
                ntwd.UserName = "feedtheneedyftn@gmail.com"
                ntwd.Password = "feedtheneedy@"
                smt.UseDefaultCredentials = True
                smt.Credentials = ntwd
                smt.Port = 587
                smt.EnableSsl = True
                smt.Send(msg)
                MsgBox("Password sent to Email successfully")
                Response.Redirect("Login.aspx")
            End If
        End If
    End Sub
End Class
