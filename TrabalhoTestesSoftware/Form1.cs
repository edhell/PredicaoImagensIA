using Microsoft.VisualBasic;
using System.Drawing.Imaging;
using System.Net;

namespace TrabalhoTestesSoftware
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Ao iniciar APP:
        private async void Form1_Load(object sender, EventArgs e)
        {
            // Inicializa o Banco de dados SQLite:
            DataAccess.InitializeDatabase();

            //DataAccess.AddData("Primeira Entrada de Teste");

            // Pega todas entradas do banco de dados e mostra no RichTextBox:
            //richTextBox1.AppendText("ULTIMAS PREDIÇÕES:" + "\n");
            List<String> entries = DataAccess.GetData();
            foreach (String entry in entries)
            {
                richTextBox1.AppendText(entry + "\n");
            }

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.ImageLocation = "image.jpg";

        }

        // Botão - Abrir imagem
        private void button1_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Em desenvolvimento");

        }

        // Botão - Abrir URL
        private void button3_Click(object sender, EventArgs e)
        {
            var link = Interaction.InputBox("Informe uma URL de imagem válida:", "Informe uma imagem", "");

            //pictureBox1.Load("https://quatrorodas.abril.com.br/wp-content/uploads/2020/09/CHR5309-e1599756335831.jpg");
            //https://escolaeducacao.com.br/wp-content/uploads/2021/11/eletronicos-750x430.jpeg

            if (link.Length > 0)
            {
                // Salva a imagem antiga na pasta
                //makeBackupOlderImage();

                // Faz o download da imagem
                groupBox1.Enabled = false;
                backgroundWorker1.RunWorkerAsync(link);


                // Baixa a imagem da URL:
                /*try
                {
                    pictureBox1.Load(link);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao baixar a imagem: " + ex.Message);
                }*/

            }


        }


        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                // Move a imagem antiga:
                makeBackupOlderImage();

                // Baixa a nova imagem:
                WebClient client = new WebClient();
                client.DownloadFile(e.Argument.ToString(), "image.jpg");

                // Retorna se OK:
                e.Result = "image.jpg";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao baixar a imagem: " + ex.Message);
                e.Result = "";
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Result == "")
            {
                MessageBox.Show("Ocorreu algum erro ao baixar imagem.");
            }
            else
            {
                try
                {
                    //pictureBox1.Image.Save(@"image.jpg", ImageFormat.Jpeg);
                    pictureBox1.ImageLocation = "image.jpg";
                    //groupBox1.Text = "Imagem";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar a imagem: " + ex.Message);
                }
            }

            // Reativa botões:
            groupBox1.Enabled = true;

        }




        // Ao terminar de baixar a imagem:
        private void pictureBox1_LoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            groupBox1.Text = "Imagem";
        }

        private void pictureBox1_LoadProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            /*groupBox1.Text = "Baixando imagem: " + e.ProgressPercentage + "%";

            if (e.ProgressPercentage == 100)
            {
                pictureBox1.Image.Save(@"image.jpg", ImageFormat.Jpeg);
                groupBox1.Text = "Imagem";
            }*/


        }

        // Make a backup of the older image
        private void makeBackupOlderImage()
        {
            string path = @"image.jpg";
            string path2 = Environment.CurrentDirectory + @"\images\" + Guid.NewGuid().ToString() + ".jpg";

            try
            {
                //FileInfo fi = new FileInfo("image.jpg");
                //fi.CopyTo(Environment.CurrentDirectory + @"\images\" + Guid.NewGuid().ToString() + ".jpg", true); // Existing file will be overwritten

                File.Move(path, path2);
            }
            catch (FileNotFoundException ex)
            {
                //MessageBox.Show("Erro ao fazer backup da imagem antiga: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro fazer backup da imagem antiga: " + ex.Message);
            }


        }

        // Botão - Verificar Imagem:
        private void button2_Click(object sender, EventArgs e)
        {
            /* 0 Objetos
            1 Cena
            2 Rostos(Face)
            3 MatchFace
            4 Placa de Carro
            5 Objeto(Pouca luz)
            6 Logotipos
            7 Ações Humanas
            8 Melhorar Imagem*/
            
            try { pictureBox1.Image.Save(@"image.jpg", ImageFormat.Jpeg); }
            catch (Exception) { }
            
            var localImage = "image.jpg";
            var localImage2 = "image.jpg";
            //var text = "";

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    Task<ResponseObject?> task0 = Task.Run(() => { return Detections.detectionObject(localImage); });

                    // Resultado da tarefa:
                    ResponseObject responseTask0 = task0.Result;
                    var text = "";

                    if (responseTask0 != null)
                    {
                        text = "OBJECT PREDICTION: " + responseTask0.predictions.Count() + ": ";

                        foreach (var p in responseTask0.predictions)
                        {
                            text += p.label + "(" + p.confidence.ToString("F") + "%) ";
                        }
                        MessageBox.Show(text);

                        // Add no banco de dados:
                        richTextBox1.Text = "NOW: " + text + "\n" + richTextBox1.Text;
                        DataAccess.AddData(text);
                    }

                    break;
                case 1:
                    Task<ResponseScene?> task1 = Task.Run(() => { return Detections.detectionScene(localImage); });

                    // Resultado da tarefa:
                    ResponseScene responseTask1 = task1.Result;
                    text = "SCENE PREDICTION: " + responseTask1.label + " (" + responseTask1.confidence.ToString("F") + "%)";
                    MessageBox.Show(text);

                    // Add no banco de dados:
                    if (responseTask1 != null) { DataAccess.AddData(text); }

                    break;
                case 2:
                    Task<ResponseFace?> task2 = Task.Run(() => { return Detections.detectionFace(localImage); });

                    // Resultado da tarefa:
                    ResponseFace responseTask2 = task2.Result;

                    if (responseTask2 != null)
                    {
                        text = "FACE PREDICTION: " + responseTask2.predictions.Count() + ": ";

                        foreach (Predict p in responseTask2.predictions)
                        {
                            text += p.label + "(" + p.confidence.ToString("F") + "%) ";
                        }
                        MessageBox.Show(text);

                        // Add no banco de dados:
                        richTextBox1.Text = "NOW: " + text + "\n" + richTextBox1.Text;
                        DataAccess.AddData(text);
                    }

                    break;
                case 3:
                    Task<ResponseMatchFace?> task3 = Task.Run(() => { return Detections.detectionMatchFace(localImage, localImage2); });

                    // Resultado da tarefa:
                    ResponseMatchFace responseTask3 = task3.Result;
                    //text = "FACE MATCH PREDICTION: " + responseTask3.label + " (" + responseTask3.confidence.ToString("F") + "%)";
                    text = "FACE MATCH PREDICTION: " + responseTask3.ToString + " ";
                    MessageBox.Show(text);

                    // Add no banco de dados:
                    if (responseTask3 != null) { DataAccess.AddData(text); }
                    break;
                case 4:
                    Task<ResponsePlate?> task4 = Task.Run(() => { return Detections.detectionPlate(localImage); });

                    // Resultado da tarefa:
                    ResponsePlate responseTask4 = task4.Result;
                    text = "PLATE PREDICTION: " + responseTask4.predictions.Count() + " ";
                    foreach (Predict p in responseTask4.predictions)
                    {
                        text += p.label + "(" + p.confidence.ToString("F") + "%) ";
                    }

                    MessageBox.Show(text);

                    // Add no banco de dados:
                    if (responseTask4 != null)
                    {
                        richTextBox1.Text = "NOW: " + text + "\n" + richTextBox1.Text;
                        DataAccess.AddData(text);
                    }

                    break;
                case 5:
                    Task<ResponseObjectDark?> task5 = Task.Run(() => { return Detections.detectionObjectDark(localImage); });

                    // Resultado da tarefa:
                    ResponseObjectDark responseTask5 = task5.Result;

                    if (responseTask5 != null)
                    {
                        text = "OBJECT DARK PREDICTION: " + responseTask5.predictions.Count() + ": ";

                        foreach (var p in responseTask5.predictions)
                        {
                            text += p.label + "(" + p.confidence.ToString("F") + "%) ";
                        }
                        MessageBox.Show(text);

                        // Add no banco de dados:
                        DataAccess.AddData(text);
                        richTextBox1.Text = "NOW: " + text + "\n" + richTextBox1.Text;
                    }
                    break;
                case 6:
                    Task<ResponseOpenlogo?> task6 = Task.Run(() => { return Detections.detectionOpenlogo(localImage); });

                    // Resultado da tarefa:
                    ResponseOpenlogo responseTask6 = task6.Result;

                    if (responseTask6 != null)
                    {
                        text = "OPEN LOGO PREDICTION: " + responseTask6.predictions.Count() + ": ";

                        foreach (var p in responseTask6.predictions)
                        {
                            text += p.label + "(" + p.confidence.ToString("F") + "%) ";
                        }
                        MessageBox.Show(text);

                        // Add no banco de dados:
                        DataAccess.AddData(text);
                        richTextBox1.Text = "NOW: " + text + "\n" + richTextBox1.Text;
                    }
                    break;
                case 7:
                    Task<ResponseActionNet?> task7 = Task.Run(() => { return Detections.detectionActionNet(localImage); });

                    // Resultado da tarefa:
                    ResponseActionNet responseTask7 = task7.Result;

                    if (responseTask7 != null)
                    {
                        text = "OPEN LOGO PREDICTION: " + responseTask7.predictions.Count() + ": ";

                        foreach (var p in responseTask7.predictions)
                        {
                            text += p.label + "(" + p.confidence.ToString("F") + "%) ";
                        }
                        MessageBox.Show(text);

                        // Add no banco de dados:
                        DataAccess.AddData(text);
                        richTextBox1.Text = "NOW: " + text + "\n" + richTextBox1.Text;
                    }
                    break;

                case 8:
                    Task<ResponseEnhance?> task8 = Task.Run(() => { return Detections.enhanceObject(localImage); });

                    // Resultado da tarefa:
                    ResponseEnhance responseTask8 = task8.Result;

                    if (responseTask8 != null)
                    {
                        text = "ENHANCE IMAGE: " + responseTask8.success + ": " + responseTask8.height + "x" + responseTask8.width;

                        var imagem = base64toImage(responseTask8.base64);

                        pictureBox1.Image = imagem;

                        MessageBox.Show(text);

                        // Add no banco de dados:
                        DataAccess.AddData(text);
                        richTextBox1.Text = "NOW: " + text + "\n" + richTextBox1.Text;
                    }
                    break;

                default:
                    MessageBox.Show("Erro ao verificar imagem.");
                    break;
            }




        }

        private string imageToBase64(string localImage)
        {
            var image = Image.FromFile(localImage);
            using (var m = new MemoryStream())
            {
                image.Save(m, image.RawFormat);
                var imageBytes = m.ToArray();

                // Convert byte[] to Base64 String
                var base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        private Image base64toImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }


        // Botão - Limpar banco de dados
        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Excluir todos dados do banco", "Você tem certeza", buttons: MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataAccess.DellAllData();
            }
        }

    }
}