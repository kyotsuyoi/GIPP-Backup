using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using MySql.Data;

namespace GIPPBackup
{
    public partial class frmBackup : Form
    {

        MySql.Data.MySqlClient.MySqlConnection connection;
        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
        string vTime;
        Thread t; //Thread que atualiza a lblFiles durante o backup.
        Thread u; //Thread que verifica se o Backup foi efetuado.
        Thread v; //Thread que verifica se o Backup do MySQL foi concluido.
        Boolean vError; //Ferifica se o Backup falhou antes de tentar atualizar as informações de data no banco de dados.
        Boolean vWait = true; //Aguarda backup do banco antes do backup de arquivos.

        public frmBackup()
        {
            InitializeComponent();

            try
            {   // Abrir o arquivo para autenticar o servidor.
                using (StreamReader sr = new StreamReader(@"C:\Program Files (x86)\GIPP\autentication"))
                {
                    // Ler o conteudo do arquivo para uma string.
                    String line = sr.ReadToEnd();
                    connection = new MySql.Data.MySqlClient.MySqlConnection(line);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Arquivo não pode ser lido: " + ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(); //Cria e configura o icone de notificação do Backup.
            this.notifyIcon.ContextMenuStrip.Items.Add("Mostrar");
            this.notifyIcon.ContextMenuStrip.Items.Add("Ocultar");
            this.notifyIcon.ContextMenuStrip.Items.Add("Fechar");
            this.notifyIcon.ContextMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.notifyIcon_MenuClick);
            
            cmd.Connection = connection;

            try
            {
                connection.Open();
                cmd.CommandText= "SELECT data_last_backup FROM system_checks WHERE id = '1'";
                lblBakData.Text= (string)cmd.ExecuteScalar();
                cmd.CommandText = "SELECT system_last_backup FROM system_checks WHERE id = '1'";
                lblBakFiles.Text = (string)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro! " + ex.Message);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }

            lblFiles.Text = "Verificando backup...";

            vError = false;
            u = new Thread(uThreadProcess);
            cmbBkpTime.Text = "24"; //A Thread 'u' será iniciada dentro de cmbBkpTime_SelectedIndexChanged.
            vTime = "24";

        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible=true;
        }

        private void notifyIcon_MenuClick(object sander, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Mostrar")
            {
                this.Visible=true;
            }
            if (e.ClickedItem.Text == "Ocultar")
            {
                this.Visible = false;
            }
            if (e.ClickedItem.Text == "Fechar")
            {
                const string message = "Desativar o Backup?";
                const string caption = "Fechando";
                
                if (MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    u.Abort();
                    base.Dispose(true);
                }
            }
        }

        private void frmBackup_Load(object sender, EventArgs e)
        {
            try
            {   // Abrir o arquivo para ler.
                using (StreamReader sr = new StreamReader(@"C:\Program Files (x86)\GIPP Backup\bakpath.txt"))
                {
                    // Ler o arquivo para uma string.
                    String line = sr.ReadToEnd();

                    txtBakPath.Text = line;
                }
                using (StreamReader sr = new StreamReader(@"C:\Program Files (x86)\GIPP Backup\filespath.txt"))
                {
                    // Ler o arquivo para uma string.
                    String line = sr.ReadToEnd();
                    
                    txtFilesPath.Text = line;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Arquivo não pode ser lido: " + ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {   // Abrir o arquivo para escrever.
                using (StreamWriter sr = new StreamWriter(@"C:\Program Files (x86)\GIPP Backup\bakpath.txt"))
                {
                    // Grava o texto para um arquivo.
                    sr.Write("");
                    sr.Write(txtBakPath.Text);
                }
                using (StreamWriter sr = new StreamWriter(@"C:\Program Files (x86)\GIPP Backup\filespath.txt"))
                {
                    sr.Write("");
                    sr.Write(txtFilesPath.Text);
                }
                MessageBox.Show("Caminhos atualizados!", "Informação!",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Arquivo não pode ser lido: " + ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFileBkp_Click(object sender, EventArgs e)
        {
            t = new Thread(tThreadProcess);
            t.Start();
        }

        private void tThreadProcess()
        {
            try
            {
                string today = DateTime.Today.ToShortDateString();
                today = today.Replace("/", "-");
                string sourcePath = @"" + txtFilesPath.Text;
                string targetPath = @"" + txtBakPath.Text + @"GIPP_Backup_" + today;

                if (!System.IO.Directory.Exists(targetPath))
                {
                    System.IO.Directory.CreateDirectory(targetPath);
                }

                DirectoryCopy(sourcePath, targetPath, true);

                if (vError == false)
                {
                    this.lblFiles.BeginInvoke((MethodInvoker)delegate () { this.lblFiles.Text = "Backup Completo!"; }); //Permite o acesso aos objetos da Thread principal sem risco de Deadlocks.
                    today = today.Replace("-", "/");
                    CreateMySqlCommand("UPDATE `pegpese`.`system_checks` SET `system_last_backup`='" + today + "' WHERE `id`='1'", connection);
                    this.lblBakFiles.BeginInvoke((MethodInvoker)delegate () { this.lblBakFiles.Text = today; });
                }
                else
                {
                    this.lblFiles.BeginInvoke((MethodInvoker)delegate () { this.lblFiles.Text = "Backup falhou!"; });
                }
                vError = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.lblFiles.BeginInvoke((MethodInvoker)delegate () { this.lblFiles.Text = "Backup falhou!"; });
            }
            
        }

        private void uThreadProcess()
        {
            try
            {
                Thread.Sleep(10000); //Evita que esta Thread tente acessar lblFiles da Thread principal antes que lblFiles seja criado.
                v = new Thread(vThreadProcess);
                v.Start();

                int i = 10;
                while (i > 0)
                {
                    this.lblFiles.BeginInvoke((MethodInvoker)delegate () { this.lblFiles.Text = "Verificando backup... " + i.ToString(); });
                    Thread.Sleep(1000);
                    i -= 1;
                }

                while (true) //Este Busy Waiting mantem o Thread de teste rodando constantemente para saber se é hora de um novo backup.
                {
                    DateTime dt = Convert.ToDateTime(CreateMySqlSelect("SELECT system_last_backup FROM system_checks WHERE id = '1'", connection));
                    if (DateTime.Today.Date > dt)
                    {
                        v = new Thread(vThreadProcess);
                        v.Start();
                        this.lblFiles.BeginInvoke((MethodInvoker)delegate () { vWait = true; });
                        
                        while (vWait == true)
                        {
                            Thread.Sleep(1000);
                        }

                        t = new Thread(tThreadProcess);
                        t.Start();
                    }
                    if (DateTime.Today.Date == dt)
                    {
                        this.lblFiles.BeginInvoke((MethodInvoker)delegate () { this.lblFiles.Text = "Backup de hoje já foi concluido!"; });
                    }
                    int vTimeI = Convert.ToInt16(vTime);

                    vTimeI = (vTimeI * 60 * 60 * 1000);
                    //vTimeI=(10000);
                    Thread.Sleep(vTimeI); //A Thread dorme por um certo periodo determinado pelo cmbBkpTime.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRO!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void vThreadProcess()
        {
            try
            {
                string today = DateTime.Today.ToShortDateString();
                today = today.Replace("/", "-");
                string vCommand = "";
                System.Diagnostics.Process.Start(@"C:\Program Files (x86)\GIPP Backup\mysql.bat", vCommand);
                Thread.Sleep(5000);

                FileInfo file = new FileInfo(@"C:\GIPP\GIPP\MySQL\" + today + ".sql");

                if (!file.Exists)
                {
                    throw new DirectoryNotFoundException(
                        "Arquivo de backup de dados não foi criado");
                }
                else
                {
                    today = today.Replace("-", "/");
                    CreateMySqlCommand("UPDATE `pegpese`.`system_checks` SET `data_last_backup`='" + today + "' WHERE `id`='1'", connection);
                    this.lblBakData.BeginInvoke((MethodInvoker)delegate () { this.lblBakData.Text = today; });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.lblFiles.BeginInvoke((MethodInvoker)delegate () { vWait = false; });
        }

        public void CreateMySqlCommand(string myExecuteQuery, MySql.Data.MySqlClient.MySqlConnection myConnection)
        {
            try
            {
            MySql.Data.MySqlClient.MySqlCommand myCommand = new MySql.Data.MySqlClient.MySqlCommand(myExecuteQuery, myConnection);
            myCommand.Connection.Open();
            myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public string CreateMySqlSelect(string myExecuteQuery, MySql.Data.MySqlClient.MySqlConnection myConnection)
        {
            try
            {
                connection.Open();
                cmd.CommandText = myExecuteQuery;
                cmd.Connection = myConnection;
                return (string)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro! " + ex.Message);
                return "";
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            try
            {
                // Get the subdirectories for the specified directory.
                DirectoryInfo dir = new DirectoryInfo(sourceDirName);

                if (!dir.Exists)
                {
                    throw new DirectoryNotFoundException(
                        "Source directory does not exist or could not be found: "
                        + sourceDirName);
                }

                DirectoryInfo[] dirs = dir.GetDirectories();
                // If the destination directory doesn't exist, create it.
                if (!Directory.Exists(destDirName))
                {
                    Directory.CreateDirectory(destDirName);
                }

                // Get the files in the directory and copy them to the new location.
                FileInfo[] files = dir.GetFiles();

                foreach (FileInfo file in files)
                {
                    string temppath = Path.Combine(destDirName, file.Name);
                    file.CopyTo(temppath, false);
                    if (this.lblFiles.InvokeRequired)
                    {
                        this.lblFiles.BeginInvoke((MethodInvoker)delegate () { this.lblFiles.Text = file.ToString(); });
                    }
                    else
                    {
                        this.lblFiles.Text = "";
                    }
                }

                // If copying subdirectories, copy them and their contents to new location.
                if (copySubDirs)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        string temppath = Path.Combine(destDirName, subdir.Name);
                        DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                        this.lblFiles.BeginInvoke((MethodInvoker)delegate () { this.lblFiles.Text = subdir.ToString(); });
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message,"ERRO",MessageBoxButtons.OK,MessageBoxIcon.Error);
                vError = true;
            }
        }

        private void cmbBkpTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblFiles.Text = "Aguarde...";
            try
            {
                u.Abort();
                u = new Thread(uThreadProcess);
                u.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDataBkp_Click(object sender, EventArgs e)
        {
            v = new Thread(vThreadProcess);
            v.Start();
        }
    }
}
