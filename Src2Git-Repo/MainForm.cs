using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Src2Git_Repo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //1.URL拚接   
            string url = @"http://127.0.0.1/api/v4/groups";

            //2.通過接口url,獲取接口返回信息
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("PRIVATE-TOKEN", "LLZSFygxb6ZopFK78JvD");
            string responStr = HttpUtils.Get(url, header);
            MessageBox.Show(responStr);

            //4.檢查是否獲取站點信息成功
            try
            {
                Output.WriteLine(responStr);
                List<Group> groups = JsonConvert.DeserializeObject<List<Group>>(responStr);
                foreach (Group group in groups)
                {
                    MessageBox.Show(group.ToString());
                }
            }
            catch (Exception ex)
            {
                Output.WriteLine(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //1.URL拚接   
            string url = @"http://127.0.0.1/api/v4/projects";

            //2.通過接口url,獲取接口返回信息
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("PRIVATE-TOKEN", "LLZSFygxb6ZopFK78JvD");
            PorjectVo projectVo = new PorjectVo();
            projectVo.name = "test";
            projectVo.namespace_id = 3;
            projectVo.visibility = "internal";
            string jsonStr = JsonConvert.SerializeObject(projectVo);
            string responStr = HttpUtils.Post(url, jsonStr, "", header);
            MessageBox.Show(responStr);

            //4.檢查是否獲取站點信息成功
            try
            {
                Output.WriteLine(responStr);
            }
            catch (Exception ex)
            {
                Output.WriteLine(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String srcStr = "E:\\source";
            String dstStr = "E:\\destination";

            Process process = new Process();
            process.StartInfo.Arguments = string.Format("/C robocopy /mir {0} {1}", srcStr, dstStr);
            process.StartInfo.FileName = "CMD.EXE"; // 要啓動的應用程式名稱
            process.StartInfo.CreateNoWindow = true; // 是否以冇有窗體的模式創建應用程式，預設為false，即有窗體，如為true，即隱藏窗體。在這裏不設定該值也可以
            process.StartInfo.UseShellExecute = false; // 要重定嚮 IO流，Process對象必須將 UseShellExecute屬性設定為false
            process.StartInfo.RedirectStandardOutput = true; // 標準輸出流的重定嚮
            process.Start();
            while (!process.StandardOutput.EndOfStream)
            {
                Output.WriteLine(process.StandardOutput.ReadLine());
            }
            process.WaitForExit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String pathStr = "E:\\source\\gg乾.txt";
            String compressionLevel = "Fastest";
            String destinationPath = "E:\\destination\\測試.zip";

            Process process = new Process();
            process.StartInfo.Arguments = string.Format("/C powershell.exe Compress-Archive -Path '{0}' -CompressionLevel '{1}' -DestinationPath '{2}'"
                , pathStr, compressionLevel, destinationPath);
            process.StartInfo.FileName = "CMD.EXE"; // 要啓動的應用程式名稱
            process.StartInfo.CreateNoWindow = true; // 是否以冇有窗體的模式創建應用程式，預設為false，即有窗體，如為true，即隱藏窗體。在這裏不設定該值也可以
            process.StartInfo.UseShellExecute = false; // 要重定嚮 IO流，Process對象必須將 UseShellExecute屬性設定為false
            process.StartInfo.RedirectStandardOutput = true; // 標準輸出流的重定嚮
            process.Start();
            while (!process.StandardOutput.EndOfStream)
            {
                Output.WriteLine(process.StandardOutput.ReadLine());
            }
            process.WaitForExit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string batFileName = @"E:\\GitHub\\Src2Git-Repo\\Src2Git-Repo\\bin\\Debug\\bat" + @"\" + Guid.NewGuid() + ".bat";

            using (StreamWriter batFile = new StreamWriter(batFileName))
            {
                batFile.WriteLine($"YOUR COMMAND");
                batFile.WriteLine($"YOUR COMMAND");
                batFile.WriteLine($"YOUR COMMAND");
            }

            ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe", "/c " + batFileName);
            processStartInfo.CreateNoWindow = true; // 是否以冇有窗體的模式創建應用程式，預設為false，即有窗體，如為true，即隱藏窗體。在這裏不設定該值也可以
            processStartInfo.UseShellExecute = false; // 要重定嚮 IO流，Process對象必須將 UseShellExecute屬性設定為false
            processStartInfo.RedirectStandardOutput = true; // 標準輸出流的重定嚮

            Process p = new Process();
            p.StartInfo = processStartInfo;
            p.Start();
            while (!p.StandardOutput.EndOfStream)
            {
                Output.WriteLine(p.StandardOutput.ReadLine());
            }
            p.WaitForExit();

            File.Delete(batFileName);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            //1.URL拚接   
            string url = @"http://127.0.0.1/api/v4/projects";

            //2.通過接口url,獲取接口返回信息
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("PRIVATE-TOKEN", txtConfigToken.Text);
            PorjectVo projectVo = new PorjectVo();
            projectVo.name = txtName.Text;
            projectVo.namespace_id = 3;
            projectVo.visibility = "internal";
            string jsonStr = JsonConvert.SerializeObject(projectVo);
            string responStr = HttpUtils.Post(url, jsonStr, "", header);
            MessageBox.Show(responStr);



            //4.檢查是否獲取站點信息成功
            try
            {
                ProjectInfo projectInfo = JsonConvert.DeserializeObject<ProjectInfo>(responStr);
                txtHttpUrl2Repo.Text = projectInfo.http_url_to_repo;
                Output.WriteLine(responStr);
            }
            catch (Exception ex)
            {
                Output.WriteLine(ex.ToString());
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            txtDstDir.Text = System.AppDomain.CurrentDomain.BaseDirectory + "tmp";
        }

        private void btnDstDir_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    //string[] files = Directory.GetFiles(fbd.SelectedPath);

                    //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");

                    txtDstDir.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnSrcDir_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    //string[] files = Directory.GetFiles(fbd.SelectedPath);

                    //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");

                    txtSrcDir.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnCompressPathList_Click(object sender, EventArgs e)
        {


            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select file";
            dialog.InitialDirectory = txtSrcDir.Text;
            if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
            {
                txtCompressPathList.Text = dialog.FileName;
                txtZipName.Text = Path.ChangeExtension(Path.GetFileNameWithoutExtension(txtCompressPathList.Text), ".zip"); ;

                DateTime modifyTime = File.GetLastWriteTime(txtCompressPathList.Text);
                txtComment.Text = "(ver." + modifyTime.ToString("yyyyMMddHHmm") + ")";
            }
        }

        private void btnPush_Click(object sender, EventArgs e)
        {
            String dstDirStr = txtDstDir.Text;
            String httpUrl2RepoStr = txtHttpUrl2Repo.Text.Replace("http://", "http://oauth2:" + txtConfigToken.Text + "@"); ;
            String nameStr = txtName.Text;
            String srcDirStr = txtSrcDir.Text;
            String zipNameStr = txtZipName.Text;
            String compressPathListStr = txtCompressPathList.Text;
            String commentStr = txtComment.Text;

            String userStr = txtConfigName.Text;
            String mailStr = txtConfigMail.Text;

            // 參數檢查
            if (String.IsNullOrWhiteSpace(dstDirStr))
            {
                MessageBox.Show("複製目的地目錄不得為空");
            }
            if (String.IsNullOrWhiteSpace(httpUrl2RepoStr))
            {
                MessageBox.Show("git地址不得為空");
            }
            if (String.IsNullOrWhiteSpace(nameStr))
            {
                MessageBox.Show("專案名稱不得為空");
            }
            if (String.IsNullOrWhiteSpace(srcDirStr))
            {
                MessageBox.Show("複製來源目錄不得為空");
            }
            if (String.IsNullOrWhiteSpace(zipNameStr))
            {
                MessageBox.Show("壓縮檔名稱不得為空");
            }
            if (String.IsNullOrWhiteSpace(compressPathListStr))
            {
                MessageBox.Show("壓縮文件清單不得為空");
            }
            if (String.IsNullOrWhiteSpace(commentStr))
            {
                MessageBox.Show("comment 不得為空");
            }

            // 檢查 httpUrl2RepoStr 是否存在


            Process process = new Process();
            process.StartInfo.Arguments = string.Format("-NoProfile -ExecutionPolicy ByPass -Command &\"'{0}' -DstDir '{1}' -HttpUrl2Repo '{2}' -Name '{3}' -SrcDir '{4}' -ZipName '{5}' -CompressPathList '{6}' -Comment '{7}' -UserName '{8}' -Mail '{9}'\""
                , System.AppDomain.CurrentDomain.BaseDirectory + "GitZip.ps1",
                dstDirStr, httpUrl2RepoStr, nameStr, srcDirStr, zipNameStr, compressPathListStr, commentStr, userStr, mailStr);
            Output.WriteLine(process.StartInfo.Arguments);
            process.StartInfo.FileName = "powershell.exe"; // 要啓動的應用程式名稱
            process.StartInfo.CreateNoWindow = true; // 是否以冇有窗體的模式創建應用程式，預設為false，即有窗體，如為true，即隱藏窗體。在這裏不設定該值也可以
            process.StartInfo.UseShellExecute = false; // 要重定嚮 IO流，Process對象必須將 UseShellExecute屬性設定為false
            process.StartInfo.RedirectStandardOutput = true; // 標準輸出流的重定嚮
            process.Start();
            while (!process.StandardOutput.EndOfStream)
            {
                Output.WriteLine(process.StandardOutput.ReadLine());
            }
            process.WaitForExit();
        }

        private void btnConfigLoad_Click(object sender, EventArgs e)
        {


            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select file";
            dialog.InitialDirectory = txtSrcDir.Text;
            if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
            {
                txtConfig.Text = dialog.FileName;

                ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                fileMap.ExeConfigFilename = dialog.FileName;
                Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

                txtConfigToken.Text = configuration.AppSettings.Settings["token"].Value;
                txtConfigName.Text = configuration.AppSettings.Settings["name"].Value;
                txtConfigMail.Text = configuration.AppSettings.Settings["email"].Value;
            }




        }

        private void btnConfigSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "All files (*.*)|*.*";//設定文件類型
            sfd.FileName = "保存";//設定預設文件名
            sfd.DefaultExt = "config";//設定預設格式（可以不設）
            sfd.AddExtension = true;//設定自動在文件名中添加擴展名
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                txtConfig.Text = sfd.FileName;
                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                {
                    System.Text.StringBuilder sb = new StringBuilder();
                    sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                    sb.AppendLine("<configuration>");
                    sb.AppendLine("</configuration>");

                    sw.WriteLineAsync(sb.ToString());
                }
            }

            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = txtConfig.Text;
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            configuration.AppSettings.Settings.Add("token", txtConfigToken.Text);
            configuration.AppSettings.Settings.Add("name", txtConfigName.Text);
            configuration.AppSettings.Settings.Add("email", txtConfigMail.Text);
            configuration.Save(ConfigurationSaveMode.Minimal);

            MessageBox.Show("ok");
        }
    }
}
