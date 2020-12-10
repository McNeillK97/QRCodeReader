using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge;
using QR_Scanner;
using AForge.Video.DirectShow;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using ZXing;
using ZXing.Aztec;
using MySql.Data.MySqlClient;
using System.Collections;

namespace QR_Scanner
{    
    public partial class Form1 : Form
    {
        private FilterInfoCollection VideoCaptureDevices;
        private VideoCaptureDevice FinalVideo;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            FinalVideo = new VideoCaptureDevice(VideoCaptureDevices[cmboSource.SelectedIndex].MonikerString);
            FinalVideo.NewFrame += new NewFrameEventHandler(FinalVideo_NewFrame);
            FinalVideo.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (FinalVideo.IsRunning)
                if (FinalVideo.IsRunning)
                    if (FinalVideo.IsRunning)
                    {
                        FinalVideo.Stop();
                    }
        }

        void FinalVideo_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap video = (Bitmap)eventArgs.Frame.Clone();
            picWebCam.Image = video;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            VideoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo VideoCaptureDevice in VideoCaptureDevices)
            {
                cmboSource.Items.Add(VideoCaptureDevice.Name);
            }
            cmboSource.SelectedIndex = 0;
        }

        private void  an(object sender, EventArgs e)
        {
            BarcodeReader Reader = new BarcodeReader();
            Result result = Reader.Decode((Bitmap)picWebCam.Image);

            try
            {
                string decoded = result.ToString().Trim();
                if (decoded != "")
                {
                    MessageBox.Show(decoded);
                    string[] info = decoded.Split('-');
                    sendToServer(info);
                }
            }
            catch (Exception ex)
            {

            }
        }

       
        private void readFromServer()
        {
            MySql.Data.MySqlClient.MySqlConnection connection;
            string server = "remotemysql.com";
            string database = "u02cS5igZL";
            string uid = "u02cS5igZL";
            string password = "HogSr1b0sA";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `box_info`", connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                Queue myBoxQueue = new Queue();
                Box myBox;
                while (reader.Read())
                {
                    string test1 = reader.GetString("id");
                    string test2 = reader.GetString("length");
                    string test3 = reader.GetString("width");
                    string test4 = reader.GetString("height");
                    string test5 = reader.GetString("weight");
                    myBox = new Box(int.Parse(test1), float.Parse(test2), float.Parse(test3), float.Parse(test4), float.Parse(test5));
                    myBoxQueue.Enqueue(myBox);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured. Please try again later.");
            }
            connection.Close();
        }

        private void sendToServer()
        {
            MySql.Data.MySqlClient.MySqlConnection connection;
            string server = "remotemysql.com";
            string database = "u02cS5igZL";
            string uid = "u02cS5igZL";
            string password = "HogSr1b0sA";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO `box_info` (`id`, `length`, `width`, `height`, `weight`) VALUES (NULL, '3', '4', '5', '6');", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured. Please try again later.");
            }
            connection.Close();
        }

        private void sendToServer(string[] i)
        {
            MySql.Data.MySqlClient.MySqlConnection connection;
            string server = "remotemysql.com";
            string database = "CEWfsvQgZu";
            string uid = "CEWfsvQgZu";
            string password = "UdelU1N7ef";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO `box_info` (`id`, `length`, `width`, `height`, `weight`) VALUES (NULL, @length, @width, @height, @weight);", connection);
                cmd.Parameters.AddWithValue("@length", i[0]);
                cmd.Parameters.AddWithValue("@width", i[1]);
                cmd.Parameters.AddWithValue("@height", i[2]);
                cmd.Parameters.AddWithValue("@weight", i[3]);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured. Please try again later.");
            }
            connection.Close();
        }
    }
}
