using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace AppNote
{
    public partial class MainForm : Form
    {
        private string _username;

        public MainForm(string username)
        {
            InitializeComponent();
            _username = username;
            LoadNotes();
        }

        private void LoadNotes()
        {
            lstNotes.Items.Clear();
            using (var conn = new SQLiteConnection("Data Source=notes.db;Version=3;"))
            {
                conn.Open();
                string query = "SELECT Id, Content FROM Notes WHERE UserId = (SELECT Id FROM Users WHERE Username = @username)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", _username);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["Id"].ToString());
                            item.SubItems.Add(reader["Content"].ToString());
                            lstNotes.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string note = txtNote.Text.Trim();
            if (string.IsNullOrEmpty(note)) return;

            using (var conn = new SQLiteConnection("Data Source=notes.db;Version=3;"))
            {
                conn.Open();
                string query = "INSERT INTO Notes (UserId, Content) VALUES ((SELECT Id FROM Users WHERE Username = @username), @content)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", _username);
                    cmd.Parameters.AddWithValue("@content", note);
                    cmd.ExecuteNonQuery();
                }
            }

            txtNote.Clear();
            LoadNotes();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lstNotes.SelectedItems.Count == 0)
            {
                MessageBox.Show("Wybierz notatkę do edycji.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newContent = txtNote.Text.Trim();
            if (string.IsNullOrEmpty(newContent))
            {
                MessageBox.Show("Treść notatki nie może być pusta.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int noteId = int.Parse(lstNotes.SelectedItems[0].Text); // Pobierz ID wybranej notatki

            using (var conn = new SQLiteConnection("Data Source=notes.db;Version=3;"))
            {
                conn.Open();
                string query = "UPDATE Notes SET Content = @content WHERE Id = @id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@content", newContent);
                    cmd.Parameters.AddWithValue("@id", noteId);
                    cmd.ExecuteNonQuery();
                }
            }

            txtNote.Clear();
            LoadNotes();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstNotes.SelectedItems.Count == 0)
            {
                MessageBox.Show("Wybierz notatkę do usunięcia.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int noteId = int.Parse(lstNotes.SelectedItems[0].Text);

            using (var conn = new SQLiteConnection("Data Source=notes.db;Version=3;"))
            {
                conn.Open();
                string query = "DELETE FROM Notes WHERE Id = @id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", noteId);
                    cmd.ExecuteNonQuery();
                }
            }

            txtNote.Clear();
            LoadNotes();
        }

        private void lstNotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstNotes.SelectedItems.Count > 0)
            {
                txtNote.Text = lstNotes.SelectedItems[0].SubItems[1].Text;
            }
        }
    }
}
