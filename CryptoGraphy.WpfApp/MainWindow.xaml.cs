using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CryptoGraphy.Services;
using CryptoGraphy.Models;
using Notification.Wpf;
using System.Threading.Tasks;

namespace CryptoGraphy.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _FilePath = string.Empty;
        private string _SavePath = string.Empty;
        private readonly NotificationManager _notificationManager = new NotificationManager();
        public MainWindow()
        {
            InitializeComponent();
        }

        #region UI Opration

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;


                    IsMaximized = true;

                }
            }
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

        private void btnMaximizeWindow_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void btnCloseApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private  void BtnConvertSingle_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtValueToConvert.Text))
            {

                CryptoGraphyLogicOpration logicOpration = new CryptoGraphyLogicOpration();
                var result = logicOpration.ConvertSingleLineToHashes(txtValueToConvert.Text.Trim());

                if (result != null)
                {
                    HashesDataGrid.ItemsSource = result;
                    var content = new NotificationContent { Title = "INFO", Message = $"{txtValueToConvert.Text} \n has been Converted to Hashes", Type = NotificationType.Information };
                    _notificationManager.Show(content, "WindowArea");
                }
                else
                {
                    HashesDataGrid.ItemsSource = null;
                    var content = new NotificationContent { Title = "Error", Message = $"{txtValueToConvert.Text} \n can't Convert at the moment.", Type = NotificationType.Error };
                    _notificationManager.Show(content, "WindowArea");

                }

            }
        }

        private void txtValueToConvert_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CryptoGraphyLogicOpration logicOpration = new CryptoGraphyLogicOpration();
                var result = logicOpration.ConvertSingleLineToHashes(txtValueToConvert.Text.Trim());
                if (result != null)
                {
                    HashesDataGrid.ItemsSource = result;
                    var content = new NotificationContent { Title = "INFO", Message = $"{txtValueToConvert.Text} \n has been Converted to Hashes", Type = NotificationType.Information };
                    _notificationManager.Show(content, "WindowArea");
                }
                else
                {
                    HashesDataGrid.ItemsSource = null;
                    var content = new NotificationContent { Title = "Error", Message = $"{txtValueToConvert.Text} \n can't Convert at the moment.", Type = NotificationType.Error };
                    _notificationManager.Show(content, "WindowArea");

                }

            }
            else if (e.Key == Key.Escape)
            {
                txtValueToConvert.Text = string.Empty;
                HashesDataGrid.ItemsSource = null;
            }

        }

        private async void btnConveretBatch_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrEmpty(_FilePath))
            {

                //Get Location TO save
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "EXCEL|*.xlsx";
                if (saveFileDialog.ShowDialog() == true)
                {
                    _SavePath = saveFileDialog.FileName;
                    CryptoGraphyLogicOpration excelLogicOpration = new CryptoGraphyLogicOpration();
                    var content1 = new NotificationContent { Title = "INFO", Message = "Opration Started,Please Wait...", Type = NotificationType.Information};
                    _notificationManager.Show(content1, "WindowArea");
                    //Async
                    var response = await Task.Run(() => excelLogicOpration.SaveToExcelFile(_FilePath, _SavePath));

                    if (response.IsSuccessfull)
                    {

                        var content = new NotificationContent { Title = "INFO", Message = response.ResponseMessage, Type = NotificationType.Success };
                        _notificationManager.Show(content, "WindowArea");

                    }
                    else
                    {
                        var content = new NotificationContent { Title = "Error", Message = response.ResponseMessage, Type = NotificationType.Error };
                        _notificationManager.Show(content, "WindowArea");

                    }

                }
            }
            else
            {
                var content = new NotificationContent { Title = "Warning", Message = "Select  File To Convert", Type = NotificationType.Warning };
                _notificationManager.Show(content, "WindowArea");
            }

        }

        private void CopytoClipboard_Click(object sender, RoutedEventArgs e)
        {
            var selecteditem = HashesDataGrid.SelectedItem as HashResultModel;
            if (selecteditem != null)
            {
                string hashedValue = selecteditem.HashedValue.ToString();
                Clipboard.SetText(hashedValue);
                var content = new NotificationContent { Title = "INFO", Message = $"{hashedValue} \n has been copied to clipboard",Type=NotificationType.Information};
                _notificationManager.Show(content, "WindowArea");
               
            }
        }

        #endregion

        #region Menu Buttons Behaviors

        private async void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.ValidateNames = true;
            openFileDialog.DereferenceLinks = false;
            openFileDialog.Filter = "TEXT|*.txt";
            bool? response = openFileDialog.ShowDialog();
            if (response == true)
            {
                string filePath = openFileDialog.FileName;
                if (string.IsNullOrEmpty(filePath) || filePath.Contains(".lnk") || Path.GetExtension(filePath) !=".txt")
                {
                    
                    var content = new NotificationContent { Title = "Warning", Message = "Please select a valid Text File", Type = NotificationType.Warning };
                    _notificationManager.Show(content, "WindowArea");
                    return;
                }

                //Async
                var lineCount = await Task.Run(() => TextFileService.GetLinesCount(filePath));
                //var lineCount = TextFileService.GetLinesCount(filePath);
                tbLinescount.Text = $"Lines Count:{lineCount}";
                tbFileName.Text = Path.GetFileName(filePath);
                _FilePath = filePath;
                var content2 = new NotificationContent { Title = "INFO", Message =$"File Name : {Path.GetFileName(filePath)} \n Line Count :{lineCount}", Type = NotificationType.Information };
                _notificationManager.Show(content2, "WindowArea");


            }
        }

        private async void rectangleDropFile_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                //GetFile Extension
                var ext = Path.GetExtension(files[0]);
                if (ext != ".txt" || string.IsNullOrEmpty(files[0]) || files[0].Contains(".lnk"))
                {
                    var content = new NotificationContent { Title = "Warning", Message = "Please select a valid Text File", Type = NotificationType.Warning };
                    _notificationManager.Show(content, "WindowArea");
                    return;

                }
                _FilePath = files[0];
                string filename = Path.GetFileName(files[0]);
                var lineCount = await Task.Run(() => TextFileService.GetLinesCount(files[0]));
                tbFileName.Text = $"{filename}";
                tbLinescount.Text = $"Lines Count:{lineCount}";
                var content2 = new NotificationContent { Title = "INFO", Message = $"File Name : {Path.GetFileName(files[0])} \n Line Count :{lineCount}", Type = NotificationType.Information };
                _notificationManager.Show(content2, "WindowArea");

            }



        }


        #endregion

        
    }
}
