Imports System.Data.SqlClient
Imports System.Net.Mail
Partial Class Donorpage
    Inherits System.Web.UI.Page
    Dim cnn As New SqlConnection
    Dim cmd As New SqlCommand
    Dim dr As SqlDataReader

    Protected Sub Btndonate_Click(sender As Object, e As EventArgs) Handles Btndonate.Click
        Dim str As String
        str = Session("email")
        cnn = New SqlConnection("Data Source=DESKTOP-K4S3UFL\SQLEXPRESS;Initial Catalog=FTN;Integrated Security=True")
        cnn.Open()
        cmd = New SqlCommand("insert into Consume values('" & str & "','" & ddlFtype.SelectedItem.Text.ToString & "','" & tbftype.Text.ToString & "','" & tbfname.Text.ToString & "','" & tbfef.Text.ToString & "','" & tbmfd.Text.ToString & "','" & tbexp.Text.ToString & "')", cnn)
        cmd.ExecuteNonQuery()
        Label1.Text = "Donation Successfull"
        ddlFtype.Enabled = False
        tbftype.Enabled = False
        tbfname.Enabled = False
        tbfef.Enabled = False
        tbmfd.Enabled = False
        tbexp.Enabled = False
        cnn.Close()

    End Sub
    Protected Sub btnNonD_Click(sender As Object, e As EventArgs) Handles btnNonD.Click
        Dim sex As String
        Dim age As String
        Dim str As String
        str = Session("email")
        If rbnmale.Checked Then
            sex = rbnmale.Text
        ElseIf rbnfemale.Checked Then
            sex = rbnfemale.Text
        Else
            sex = "---"
        End If

        If rblist.Items.Item(0).Selected Then
            age = rblist.SelectedValue
        ElseIf rblist.Items.Item(1).Selected Then
            age = rblist.SelectedValue
        Else
            age = "---"
        End If


        cnn = New SqlConnection("Data Source=DESKTOP-K4S3UFL\SQLEXPRESS;Initial Catalog=FTN;Integrated Security=True")
            cnn.Open()
        cmd = New SqlCommand("insert into Non_Consume values('" & str & "','" & ddlitype.SelectedItem.Text.ToString & "','" & tbitype.Text.ToString & "','" & tbIname.Text.ToString & "','" & sex & "','" & age & "','" & tbqty.Text.ToString & "')", cnn)
        cmd.ExecuteNonQuery()
            Label2.ForeColor = System.Drawing.Color.Lime
            Label2.Text = "Donation Successfull"
            ddlitype.Enabled = False
            tbitype.Enabled = False
            tbIname.Enabled = False
            rbnmale.Enabled = False
            rbnfemale.Enabled = False
            rblist.Enabled = False
            tbqty.Enabled = False
            BtnNONAdd.Focus()
        cnn.Close()


    End Sub
    Protected Sub ddlFtype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFtype.SelectedIndexChanged
        If ddlFtype.SelectedItem.Text = "Others" Then
            RequiredFieldValidator3.Visible = True
            tbftype.Visible = True
            tbftype.Focus()
        End If
        If ddlFtype.SelectedItem.Text = "Veg" Then
            tbftype.Visible = False
            RequiredFieldValidator3.Visible = False
        End If
        If ddlFtype.SelectedItem.Text = "Non-Veg" Then
            tbftype.Visible = False
            RequiredFieldValidator3.Visible = False
        End If
    End Sub
    Protected Sub ddlitype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlitype.SelectedIndexChanged
        If ddlitype.SelectedItem.Text = "Others" Then
            RequiredFieldValidator7.Visible = True
            RequiredFieldValidator12.Visible = False

            tbitype.Visible = True
            tbitype.Focus()
            rbnmale.Enabled = False
            rbnfemale.Enabled = False
            rbnmale.Checked = False
            rbnmale.Checked = False
            rblist.Enabled = False
            rblist.Items.Item(0).Selected = False
            rblist.Items.Item(1).Selected = False
        End If
        If ddlitype.SelectedItem.Text = "Clothes" Then
            RequiredFieldValidator7.Visible = False
            RequiredFieldValidator12.Visible = True
            tbitype.Visible = False
            rbnmale.Enabled = True
            rbnfemale.Enabled = True
            rblist.Enabled = True
        End If
        If ddlitype.SelectedItem.Text = "Stationary" Then
            RequiredFieldValidator7.Visible = False
            RequiredFieldValidator12.Visible = False

            tbitype.Visible = False
            rbnmale.Enabled = False
            rbnfemale.Enabled = False
            rbnmale.Checked = False
            rbnmale.Checked = False
            rblist.Enabled = False
            rblist.Items.Item(0).Selected = False
            rblist.Items.Item(1).Selected = False
        End If
    End Sub
    Protected Sub btnDcancel_Click(sender As Object, e As EventArgs) Handles btnDcancel.Click
        Dim str As String
        str = Session("email")

        Dim from As String
        from = "feedtheneedyftn@gmail.com"
        Dim password, subject, body As String
        password = "feedtheneedy@"
        subject = "Feed The Needy"
        body = "Hello! Thank you for your donation"

        Using mm As New MailMessage(from, str)
            mm.Subject = "Feed The needy"
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

        Response.Redirect("Default.aspx")
    End Sub
    Protected Sub btnnonC_Click(sender As Object, e As EventArgs) Handles btnnonC.Click
        Dim str As String
        str = Session("email")

        Dim from As String
        from = "feedtheneedyftn@gmail.com"
        Dim password, subject, body As String
        password = "feedtheneedy@"
        subject = "Feed The Needy"
        body = "Hello! Thank you for your donation"

        Using mm As New MailMessage(from, str)
            mm.Subject = "Feed The needy"
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

        Response.Redirect("Default.aspx")
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ddlFtype.SelectedIndex = Items.Item(0)
        tbftype.Text = " "
        tbfname.Text = " "
        tbfef.Text = " "
        tbmfd.Text = " "
        tbexp.Text = " "
        Label1.Text = " "
        ddlFtype.Enabled = True
        tbftype.Enabled = True
        tbfname.Enabled = True
        tbfef.Enabled = True
        tbmfd.Enabled = True
        tbexp.Enabled = True
    End Sub
    Protected Sub BtnNONAdd_Click(sender As Object, e As EventArgs) Handles BtnNONAdd.Click
        ddlitype.SelectedIndex = Items.Item(0)
        tbitype.Text = " "
        tbIname.Text = " "
        rbnmale.Checked = False
        rbnfemale.Checked = False
        rblist.Items.Item(0).Selected = False
        rblist.Items.Item(1).Selected = False
        tbqty.Text = " "
        Label2.Text = " "

        ddlitype.Enabled = True
        tbitype.Enabled = True
        tbIname.Enabled = True
        rbnmale.Enabled = True
        rbnfemale.Enabled = True
        rblist.Enabled = True
        tbqty.Enabled = True
    End Sub
End Class
