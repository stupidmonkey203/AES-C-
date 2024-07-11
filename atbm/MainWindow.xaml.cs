using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace atbm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            string inputData = txtInputData.Text;
            string encryptionKey = txtEncryptionKey.Text;

            if (!string.IsNullOrEmpty(inputData) && !string.IsNullOrEmpty(encryptionKey))
            {
                try
                {
                    string encryptedData = AES.Encrypt(inputData, encryptionKey);
                    txtEncryptedData.Text = encryptedData;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please enter input data and encryption key.");
            }
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            string inputData = txtEncryptedData.Text;
            string encryptionKey = txtEncryptionKey.Text;

            if (!string.IsNullOrEmpty(inputData) && !string.IsNullOrEmpty(encryptionKey))
            {
                try
                {
                    string outputData = AES.DecryptAES(inputData, encryptionKey);
                    txtDecryptedData.Text = outputData;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please enter input data and encryption key.");
            }
        }

        private void RandomKey_Click(object sender, RoutedEventArgs e)
        {
            // Khởi tạo một mảng byte để lưu trữ khóa mã hóa
            byte[] key = new byte[16]; // 128-bit = 16 bytes

            // Sử dụng RNGCryptoServiceProvider để tạo số ngẫu nhiên
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                // Điền mảng byte với số ngẫu nhiên
                rng.GetBytes(key);
            }

            // Chuyển đổi khóa sang dạng Base64 để hiển thị trong TextBox
            string keyBase64 = Convert.ToBase64String(key);

            // Gán giá trị khóa cho TextBox
            txtEncryptionKey.Text = keyBase64;

        }

        private void Chuyen_Click(object sender, RoutedEventArgs e)
        {
            txtEncryptedData_Decrypt.Text = txtEncryptedData.Text.Trim();
            txtDecryptionKey.Text = txtEncryptionKey.Text.Trim();
        }

        private void Xoa_Click(object sender, RoutedEventArgs e)
        {
            txtInputData.Text = "";
            txtEncryptionKey.Text = "";
            txtEncryptedData.Text = "";
            txtEncryptedData_Decrypt.Text = "";
            txtDecryptionKey.Text = "";
            txtDecryptedData.Text = "";
        }

        private void ReadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                // Đọc nội dung của tệp tin
                string fileContent = File.ReadAllText(filePath);

                if (String.IsNullOrEmpty(fileContent))
                {
                    MessageBox.Show("File rỗng, vui lòng chọn file khác");
                }
                else
                {
                    // Hiển thị nội dung trong TextBox
                    txtInputData.Text = fileContent;
                }
            }
        }
    }
}

 