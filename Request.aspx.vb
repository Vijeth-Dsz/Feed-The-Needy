Imports System.Data.SqlClient
Imports System.Net.Mail

Partial Class Request
    Inherits System.Web.UI.Page
    Dim cnn As New SqlConnection
    Dim cmd As New SqlCommand
    Dim sex As String
    Dim da As New SqlDataAdapter
    Dim dt As New Data.DataTable
    Dim dt1 As New Data.DataTable
    Protected Sub Btnreq_Click(sender As Object, e As EventArgs) Handles Btnreq.Click
        btncons.Visible = True
        btnnnoncon.Visible = True

        Panel1.Visible = True
        Panel2.Visible = True
    End Sub
    Protected Sub btncons_Click(sender As Object, e As EventArgs) Handles btncons.Click
        Panel1.Visible = True
    End Sub
    Protected Sub btnnnoncon_Click(sender As Object, e As EventArgs) Handles btnnnoncon.Click
        Panel2.Visible = True
    End Sub
    Protected Sub ddlFtype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFtype.SelectedIndexChanged
        If ddlFtype.SelectedItem.Text = "Others" Then
            tbfoodtye.Visible = True
            tbfoodtye.Focus()
        End If
    End Sub

    Protected Sub ddlitype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlitype.SelectedIndexChanged
        If ddlitype.SelectedItem.Text = "Others" Then
            tbitype.Visible = True
            tbitype.Focus()
        End If
    End Sub
    Protected Sub btnFrecieve_Click(sender As Object, e As EventArgs) Handles btnFrecieve.Click
        Dim str As String
        str = Session("email")
        cnn = New SqlConnection("Data Source=DESKTOP-K4S3UFL\SQLEXPRESS;Initial Catalog=FTN;Integrated Security=True")
        cnn.Open()

        If tbfef.Text > GridView3.SelectedRow.Cells(6).Text Or tbfef.Text <= 0 Then
            Label1.Visible = True
            Label1.ForeColor = System.Drawing.Color.Red
            Label1.Text = "*Enter Correct Quantity"
        Else
            cmd = New SqlCommand("insert into R_Consume values('" & str & "','" & tbFID.Text.ToString & "','" & ddlFtype.SelectedItem.Text.ToString & "','" & tbfoodtye.Text.ToString & "','" & tbfname.Text.ToString & "','" & tbfef.Text.ToString & "','" & tbmfd.Text.ToString & "','" & tbexp.Text.ToString & "')", cnn)
            cmd.ExecuteNonQuery()
            Label1.Visible = True
            Label1.ForeColor = System.Drawing.Color.Green
            Label1.Text = "Your Request Accepted"

            Dim fef As String
            fef = GridView3.SelectedRow.Cells(6).Text - tbfef.Text
            If fef > 0 Then
                cmd = New SqlCommand("update Consume set Food_Enough_For='" & fef & "' where Food_ID='" & tbFID.Text.ToString & "'", cnn)
                cmd.ExecuteNonQuery()
            Else
                cmd = New SqlCommand("delete from Consume where Food_ID='" & tbFID.Text.ToString & "'", cnn)
                cmd.ExecuteNonQuery()
            End If
        End If
        cnn.Close()


        cnn = New SqlConnection("Data Source=DESKTOP-K4S3UFL\SQLEXPRESS;Initial Catalog=FTN;Integrated Security=True")
        cnn.Open()

        da = New SqlDataAdapter("select * from Consume", cnn)
        dt.Clear()
        da.Fill(dt)
        GridView3.DataSource = dt
        GridView3.DataBind()
        cnn.Close()
    End Sub
    Protected Sub Btncancel_Click(sender As Object, e As EventArgs) Handles Btncancel.Click
        Dim str As String
        str = Session("email")

        Dim from As String
        from = "feedtheneedyftn@gmail.com"
        Dim password, subject, body As String
        password = "feedtheneedy@"
        subject = "Feed The Needy"
        body = "Hello! Thank you for your participation"

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
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        cnn = New SqlConnection("Data Source=DESKTOP-K4S3UFL\SQLEXPRESS;Initial Catalog=FTN;Integrated Security=True")
        cnn.Open()

        da = New SqlDataAdapter("select * from Consume", cnn)
        dt.Clear()
        da.Fill(dt)
        GridView3.DataSource = dt
        GridView3.DataBind()
        cnn.Close()

        cnn = New SqlConnection("Data Source=DESKTOP-K4S3UFL\SQLEXPRESS;Initial Catalog=FTN;Integrated Security=True")
        cnn.Open()

        da = New SqlDataAdapter("select * from Non_Consume", cnn)
        dt1.Clear()
        da.Fill(dt1)
        GridView4.DataSource = dt1
        GridView4.DataBind()
        cnn.Close()

    End Sub

    Protected Sub GridView3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView3.SelectedIndexChanged
        cnn = New SqlConnection("Data Source=DESKTOP-K4S3UFL\SQLEXPRESS;Initial Catalog=FTN;Integrated Security=True")
        cnn.Open()
        tbFID.Text = GridView3.SelectedRow.Cells(2).Text
        ddlFtype.SelectedItem.Text = GridView3.SelectedRow.Cells(3).Text
        tbfoodtye.Text = GridView3.SelectedRow.Cells(4).Text
        tbfname.Text = GridView3.SelectedRow.Cells(5).Text
        tbfef.Text = GridView3.SelectedRow.Cells(6).Text
        tbmfd.Text = GridView3.SelectedRow.Cells(7).Text
        tbexp.Text = GridView3.SelectedRow.Cells(8).Text
        cnn.Close()
    End Sub
    Protected Sub GridView4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView4.SelectedIndexChanged
        cnn = New SqlConnection("Data Source=DESKTOP-K4S3UFL\SQLEXPRESS;Initial Catalog=FTN;Integrated Security=True")
        cnn.Open()

        tbItId.Text = GridView4.SelectedRow.Cells(2).Text
        ddlitype.SelectedItem.Text = GridView4.SelectedRow.Cells(3).Text
        tbitype.Text = GridView4.SelectedRow.Cells(4).Text
        tbIname.Text = GridView4.SelectedRow.Cells(5).Text

        If GridView4.SelectedRow.Cells(6).Text = "Male" Then
            rbnmale.Checked = True
        ElseIf GridView4.SelectedRow.Cells(6).Text = "Female" Then
            rbnfemale.Checked = True
        Else
            rbnmale.Checked = False
            rbnfemale.Checked = False
        End If

        If GridView4.SelectedRow.Cells(7).Text = "Adult" Then
            rblist.Items.Item(0).Selected = True

        ElseIf GridView4.SelectedRow.Cells(7).Text = "Child" Then
            rblist.Items.Item(1).Selected = True
        Else
            rblist.Items.Item(0).Selected = False
            rblist.Items.Item(1).Selected = False
        End If

        tbqty.Text = GridView4.SelectedRow.Cells(8).Text

        cnn.Close()
    End Sub
    Protected Sub Btnrcv_Click(sender As Object, e As EventArgs) Handles Btnrcv.Click
        Dim str As String
        str = Session("email")
        If rbnmale.Checked Then
            sex = rbnmale.Text
        ElseIf rbnfemale.Checked Then
            sex = rbnfemale.Text
        End If

        cnn = New SqlConnection("Data Source=DESKTOP-K4S3UFL\SQLEXPRESS;Initial Catalog=FTN;Integrated Security=True")
        cnn.Open()
        If tbqty.Text > GridView4.SelectedRow.Cells(8).Text Or tbqty.Text <= 0 Then
            Label2.Visible = True
            Label2.ForeColor = System.Drawing.Color.Red
            Label2.Text = "*Enter Correct Quantity"
        Else
            cmd = New SqlCommand("insert into R_Non_Consume values('" & str & "','" & tbItId.Text.ToString & "','" & ddlitype.SelectedItem.Text.ToString & "','" & tbitype.Text.ToString & "','" & tbIname.Text.ToString & "','" & sex & "','" & rblist.SelectedItem.Text.ToString & "','" & tbqty.Text.ToString & "')", cnn)

            cmd.ExecuteNonQuery()
            Label2.Visible = True
            Label2.ForeColor = System.Drawing.Color.Green
            Label2.Text = "Your Request Accepted"

            Dim qty As String
            qty = GridView4.SelectedRow.Cells(8).Text - tbqty.Text
            If qty > 0 Then
                cmd = New SqlCommand("update Non_Consume set Quantity='" & qty & "' where Item_ID='" & tbItId.Text.ToString & "'", cnn)
                cmd.ExecuteNonQuery()
            Else
                cmd = New SqlCommand("delete from Non_Consume where Item_ID='" & tbItId.Text.ToString & "'", cnn)
                cmd.ExecuteNonQuery()
            End If
        End If
        cnn.Close()

        cnn = New SqlConnection("Data Source=DESKTOP-K4S3UFL\SQLEXPRESS;Initial Catalog=FTN;Integrated Security=True")
        cnn.Open()

        da = New SqlDataAdapter("select * from Non_Consume", cnn)
        dt1.Clear()
        da.Fill(dt1)
        GridView4.DataSource = dt1
        GridView4.DataBind()
        cnn.Close()

    End Sub
End Class
