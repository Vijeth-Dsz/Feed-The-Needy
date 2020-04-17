Imports System.Data.SqlClient
Partial Class Login
    Inherits System.Web.UI.Page
    Dim cnn As New SqlConnection
    Dim cmd As New SqlCommand
    Dim dr As SqlDataReader
    Protected Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        cnn = New SqlConnection("Data Source=DESKTOP-K4S3UFL\SQLEXPRESS;Initial Catalog=FTN;Integrated Security=True")
        cnn.Open()
        If tbuser.Text = "feedtheneedyftn@gmail.com" And tbpass.Text = "feedtheneedy@" Then
            Response.Redirect("default3.aspx")
        End If
        cmd = New SqlCommand("Select * from Register where  Email ='" & tbuser.Text & "' and Pass= '" & tbpass.Text & "'", cnn)
        cmd.Connection = cnn
        dr = cmd.ExecuteReader
        Dim st As String = ""

        If dr.HasRows() Then
            While dr.Read()
                st = dr.Item(9).ToString
            End While
            Session("email") = tbuser.Text
            If st.Equals("Donor") Then
                Response.Redirect("Donorpage.aspx")
            ElseIf st.Equals("Recipient") Then
                Response.Redirect("Request.aspx")
            End If
        Else
            MsgBox("User Not Found")
        End If
        dr.Close()
        cnn.Close()
    End Sub
    Protected Sub btnsign_Click(sender As Object, e As EventArgs) Handles btnsign.Click
        Response.Redirect("Register.aspx")
    End Sub
    Protected Sub btnLcan_Click(sender As Object, e As EventArgs) Handles btnLcan.Click
        Response.Redirect("default.aspx")
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        tbuser.Focus()
    End Sub

End Class
