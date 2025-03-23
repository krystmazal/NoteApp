namespace AppNote
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListView lstNotes;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lstNotes = new System.Windows.Forms.ListView();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstNotes
            // 
            this.lstNotes.BackColor = System.Drawing.SystemColors.Info;
            this.lstNotes.FullRowSelect = true;
            this.lstNotes.HideSelection = false;
            this.lstNotes.Location = new System.Drawing.Point(30, 30);
            this.lstNotes.Name = "lstNotes";
            this.lstNotes.Size = new System.Drawing.Size(454, 200);
            this.lstNotes.TabIndex = 0;
            this.lstNotes.UseCompatibleStateImageBehavior = false;
            this.lstNotes.View = System.Windows.Forms.View.Details;
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(490, 47);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(155, 20);
            this.txtNote.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(490, 73);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(80, 25);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Dodaj";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(490, 104);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(80, 25);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Edytuj";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(490, 135);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 25);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Usuń";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Myanmar Text", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(486, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 24);
            this.label1.TabIndex = 5;
            this.label1.Text = "Treść notatki:";
            // 
            // MainForm
            // 
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(657, 245);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstNotes);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Name = "MainForm";
            this.Text = "Notatnik";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label label1;
    }
}
