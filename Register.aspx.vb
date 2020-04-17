Imports System.Data.SqlClient
Imports System.Net.Mail
Partial Class Register
    Inherits System.Web.UI.Page
    Dim cn As New SqlConnection
    Dim cmd As New SqlCommand
    Protected Sub btnregister_Click(sender As Object, e As EventArgs) Handles btnregister.Click
        If tbOtp.Text = "" Then
            lbmsg.Visible = True
            lbmsg.Text = "   *Enter OTP"
        ElseIf tbOtp.Text = Session("OTP") Then
            cn = New SqlConnection("Data Source=DESKTOP-K4S3UFL\SQLEXPRESS;Initial Catalog=FTN;Integrated Security=True")
                cn.Open()
                cmd = New SqlCommand("insert into Register values('" & tbuser.Text & "','" & tbadrs.Text & "','" & tbcity.Text & "','" & ddlState.SelectedItem.Text & "','" & tbpin.Text & "','" & tborg.Text & "'," & tbcontact.Text & ",'" & tbemail.Text & "','" & tbpass.Text & "','" & ddlRtype.SelectedItem.Text & "')", cn)
                cmd.ExecuteNonQuery()
                MsgBox("You Registered Successfully")
                cn.Close()

                cn = New SqlConnection("Data Source=DESKTOP-K4S3UFL\SQLEXPRESS;Initial Catalog=FTN;Integrated Security=True")
                cn.Open()
                cmd = New SqlCommand("insert into Login values('" & tbemail.Text & "','" & tbpass.Text & "')", cn)
            cmd.ExecuteNonQuery()
            cn.Close()

            If ddlRtype.SelectedItem.Text = "Donor" Then
                Response.Redirect("DonorPage.aspx")
            Else
                Response.Redirect("Request.aspx")
            End If
        Else
                lbmsg.Text = "Enter Correct OTP"
        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        tbuser.Text = " "
        tbadrs.Text = " "
        tbcity.Text = " "
        ddlState.SelectedItem.Text = " "
        tbpin.Text = " "
        tbcontact.Text = " "
        tborg.Text = " "
        tbemail.Text = " "
        tbpass.Text = " "

        ddlRtype.SelectedItem.Text = " "
        Response.Redirect("Default.aspx")
    End Sub
    Protected Sub btnOtp_Click(sender As Object, e As EventArgs) Handles btnOtp.Click

        Dim uname As String = tbuser.Text

        Dim r As New Random
        Session("OTP") = (Convert.ToString(r.Next(1000, 2000)))

        Dim email As String
        email = tbemail.Text

        Dim from As String
        from = "feedtheneedyftn@gmail.com"
        Dim password, subject, body As String
        password = "feedtheneedy@"
        subject = "One Time Password"
        body = "Hello " + uname + " Your OTP is:" + Session("OTP")

        Using mm As New MailMessage(from, tbemail.Text)
            mm.Subject = subject
            mm.Body = body
            mm.IsBodyHtml = False
            Dim smtp As New SmtpClient()
            smtp.Host = "smtp.gmail.com"
            smtp.EnableSsl = True
            Dim NetworkCred As New System.Net.NetworkCredential(from, password)
            smtp.UseDefaultCredentials = True
            smtp.Credentials = NetworkCred
            smtp.Port = 587
            smtp.Send(mm)
        End Using
        MsgBox("done")

    End Sub
End Class
