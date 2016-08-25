namespace GIPPBackup
{
    partial class frmBackup
    {
        
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            //if (disposing && (components != null))
            //{
            //     components.Dispose();
            // }
            //base.Dispose(disposing);
            this.Visible = false;
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBackup));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBakData = new System.Windows.Forms.Label();
            this.lblBakFiles = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFilesPath = new System.Windows.Forms.TextBox();
            this.txtBakPath = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDataBkp = new System.Windows.Forms.Button();
            this.btnFileBkp = new System.Windows.Forms.Button();
            this.lblFiles = new System.Windows.Forms.Label();
            this.cmbBkpTime = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "GIPP Backup";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data do ultimo backup de dados:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Data do ultimo backup de arquivos:";
            // 
            // lblBakData
            // 
            this.lblBakData.AutoSize = true;
            this.lblBakData.Location = new System.Drawing.Point(195, 13);
            this.lblBakData.Name = "lblBakData";
            this.lblBakData.Size = new System.Drawing.Size(30, 13);
            this.lblBakData.TabIndex = 3;
            this.lblBakData.Text = "Data";
            // 
            // lblBakFiles
            // 
            this.lblBakFiles.AutoSize = true;
            this.lblBakFiles.Location = new System.Drawing.Point(195, 36);
            this.lblBakFiles.Name = "lblBakFiles";
            this.lblBakFiles.Size = new System.Drawing.Size(30, 13);
            this.lblBakFiles.TabIndex = 4;
            this.lblBakFiles.Text = "Data";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(184, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Caminho para copiar arquivos/dados:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(185, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Caminho para gravar arquivos/dados:";
            // 
            // txtFilesPath
            // 
            this.txtFilesPath.Location = new System.Drawing.Point(198, 61);
            this.txtFilesPath.Name = "txtFilesPath";
            this.txtFilesPath.Size = new System.Drawing.Size(192, 20);
            this.txtFilesPath.TabIndex = 7;
            // 
            // txtBakPath
            // 
            this.txtBakPath.Location = new System.Drawing.Point(198, 87);
            this.txtBakPath.Name = "txtBakPath";
            this.txtBakPath.Size = new System.Drawing.Size(192, 20);
            this.txtBakPath.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(295, 113);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Gravar caminhos";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDataBkp
            // 
            this.btnDataBkp.Location = new System.Drawing.Point(295, 8);
            this.btnDataBkp.Name = "btnDataBkp";
            this.btnDataBkp.Size = new System.Drawing.Size(95, 23);
            this.btnDataBkp.TabIndex = 10;
            this.btnDataBkp.Text = "Gerar Backup";
            this.btnDataBkp.UseVisualStyleBackColor = true;
            this.btnDataBkp.Click += new System.EventHandler(this.btnDataBkp_Click);
            // 
            // btnFileBkp
            // 
            this.btnFileBkp.Location = new System.Drawing.Point(295, 32);
            this.btnFileBkp.Name = "btnFileBkp";
            this.btnFileBkp.Size = new System.Drawing.Size(95, 23);
            this.btnFileBkp.TabIndex = 11;
            this.btnFileBkp.Text = "Gerar Backup";
            this.btnFileBkp.UseVisualStyleBackColor = true;
            this.btnFileBkp.Click += new System.EventHandler(this.btnFileBkp_Click);
            // 
            // lblFiles
            // 
            this.lblFiles.AutoSize = true;
            this.lblFiles.Location = new System.Drawing.Point(13, 153);
            this.lblFiles.Name = "lblFiles";
            this.lblFiles.Size = new System.Drawing.Size(13, 13);
            this.lblFiles.TabIndex = 12;
            this.lblFiles.Text = "_";
            // 
            // cmbBkpTime
            // 
            this.cmbBkpTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBkpTime.FormattingEnabled = true;
            this.cmbBkpTime.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "8",
            "12",
            "24",
            "48",
            "96",
            "360",
            "720"});
            this.cmbBkpTime.Location = new System.Drawing.Point(198, 113);
            this.cmbBkpTime.Name = "cmbBkpTime";
            this.cmbBkpTime.Size = new System.Drawing.Size(82, 21);
            this.cmbBkpTime.TabIndex = 13;
            this.cmbBkpTime.SelectedIndexChanged += new System.EventHandler(this.cmbBkpTime_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Próximo backup automatico (horas):";
            // 
            // frmBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 184);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbBkpTime);
            this.Controls.Add(this.lblFiles);
            this.Controls.Add(this.btnFileBkp);
            this.Controls.Add(this.btnDataBkp);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtBakPath);
            this.Controls.Add(this.txtFilesPath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblBakFiles);
            this.Controls.Add(this.lblBakData);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmBackup";
            this.Text = "GIPP Backup";
            this.Load += new System.EventHandler(this.frmBackup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void ContextMenuStrip_ItemClicked(object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblBakData;
        private System.Windows.Forms.Label lblBakFiles;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFilesPath;
        private System.Windows.Forms.TextBox txtBakPath;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDataBkp;
        private System.Windows.Forms.Button btnFileBkp;
        private System.Windows.Forms.Label lblFiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbBkpTime;
    }
}

