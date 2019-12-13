Imports MySql.Data.MySqlClient

Module PubVers
    Public session_user As String
    Public session_level As String
    Public session_poli As String
    Public session_NO_RM As String
    Public session_TGL
    Public id_kia As String
    Public conn As MySqlConnection
    Public cmd As MySqlCommand
    Public rd As MySqlDataReader
    Public adapter As MySqlDataAdapter
    Public ds As DataSet
    Public varobat As New List(Of String)()
    Public vardosis As New List(Of String)()
    Public diagicd As New List(Of String)()

    Sub koneksi()
        Try
            Dim str As String = "server=localhost;user id=root;database=sikia"
            conn = New MySqlConnection(str)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Module
