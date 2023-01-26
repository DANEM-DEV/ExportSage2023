namespace ExportSage
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelErr = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelConnString = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelErr
            // 
            this.labelErr.AutoSize = true;
            this.labelErr.Location = new System.Drawing.Point(12, 40);
            this.labelErr.Name = "labelErr";
            this.labelErr.Size = new System.Drawing.Size(41, 13);
            this.labelErr.TabIndex = 0;
            this.labelErr.Text = "Erreur :";
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(265, 83);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 39);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelConnString
            // 
            this.labelConnString.AutoSize = true;
            this.labelConnString.Location = new System.Drawing.Point(12, 9);
            this.labelConnString.Name = "labelConnString";
            this.labelConnString.Size = new System.Drawing.Size(35, 13);
            this.labelConnString.TabIndex = 2;
            this.labelConnString.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 134);
            this.Controls.Add(this.labelConnString);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelErr);
            this.Name = "Form1";
            this.Text = "Export FRESHFOOD";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelErr;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelConnString;
    }
}

