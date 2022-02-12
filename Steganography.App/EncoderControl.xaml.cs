// SPDX-License-Identifier: GPL-3.0-or-later
//
// Copyright 2022 Matthias Lübben <ml81@gmx.de>
//

using Microsoft.Win32;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Steganography.App
{
    /// <summary>
    /// Interaktionslogik für EncoderControl.xaml
    /// </summary>
    public partial class EncoderControl : UserControl
    {
        private readonly BitmapSteganographyEncoder encoder = new BitmapSteganographyEncoder();

        public EncoderControl()
        {
            InitializeComponent();

            inputImageTextBox.Text = ImageDefaults.GetDefaultEncoderInputFile();
            outputImageTextBox.Text = ImageDefaults.GetDefaultDecoderInputFile();
        }

        private void encodeButton_Click(object sender, RoutedEventArgs e)
        {
            var inputFile = inputImageTextBox.Text;
            var outputFile = outputImageTextBox.Text;

            if (!System.IO.File.Exists(inputFile))
            {
                MessageBox.Show("Input file doesn't exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (System.IO.File.Exists(outputFile))
            {
                MessageBox.Show("Output file already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var text = messageTextBox.Text;
            if (text == "")
            {
                MessageBox.Show("Message can't be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var data = Encoding.UTF8.GetBytes(text);

            var inputBitmap = new Bitmap(inputFile);
            var outputBitmap = encoder.Encode(data, inputBitmap);

            outputBitmap.Save(outputFile, ImageFormat.Png);

            MessageBox.Show("Image encoded.", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void browseInputImageButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                inputImageTextBox.Text = dlg.FileName;

                if (outputImageTextBox.Text == "")
                {

                    string directoryName = System.IO.Path.GetDirectoryName(dlg.FileName);
                    string baseName = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName);
                    string outputFile = System.IO.Path.Combine(directoryName, baseName + ".encoded.png");
                    outputImageTextBox.Text = outputFile;
                }
            }
        }

        private void browseOutputImageButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog();
            if (dlg.ShowDialog() == true)
            {
                outputImageTextBox.Text = dlg.FileName;
            }
        }
    }
}
