Imports System.Data.SqlClient

Partial Class Editprofiledetail
    Inherits System.Web.UI.Page
    Dim cnn As New SqlConnection
    Dim cmd As New SqlCommand
    Dim dr As SqlDataReader
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        cnn = New SqlConnection("Data Source=DESKTOP-K4S3UFL\SQLEXPRESS;Initial Catalog=FTN;Integrated Security=True")
        cnn.Open()
        Dim str As String
        str = Session("email")

        cmd = New SqlCommand("Select * from Register where  Email ='" & str & "'", cnn)
        cmd.Connection = cnn
        dr = cmd.ExecuteReader

        If dr.HasRows Then
            str = Session("email")

            While dr.Read()
                tbuser.Text = dr.Item(0)
                tbadrs.Text = dr.Item(1)
                tbcity.Text = dr.Item(2)
                tbstate.Text = dr.Item(3)
                tbpin.Text = dr.Item(4)
                tborg.Text = dr.Item(5)
                tbcontact.Text = dr.Item(6)
                tbemail.Text = dr.Item(7)
                tbPassword.Text = dr.Item(8)
                tbType.Text = dr.Item(9)
            End While
        Else
            MsgBox("Inavlid Email or Password")
        End If
        dr.Close()
        cnn.Close()
    End Sub
    Protected Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        cnn = New SqlConnection("Data Source=DESKTOP-K4S3UFL\SQLEXPRESS;Initial Catalog=FTN;Integrated Security=True")
        cnn.Open()
        cmd = New SqlCommand("update Register set username= '" & tbuser.Text & "', address= '" & tbadrs.Text & "',City ='" & tbcity.Text & "',State ='" & tbstate.Text & "',pin ='" & tbpin.Text & "',orgname ='" & tborg.Text & "',phno ='" & tbcontact.Text & "',Email ='" & tbemail.Text & "',Pass ='" & tbPassword.Text & "',utype ='" & tbType.Text & "' where Email='" & tbemail.Text & "'", cnn)
        cmd.Connection = cnn
        cmd.ExecuteNonQuery()
        MsgBox("Profile Updated Successfully")
        cnn.Close()
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If tbType.Text = "Donor" Then
            Response.Redirect("DonorPage.aspx")
        Else
            Response.Redirect("Request.aspx")
        End If
    End Sub
End Class
