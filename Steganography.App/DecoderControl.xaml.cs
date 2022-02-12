// SPDX-License-Identifier: GPL-3.0-or-later
//
// Copyright 2022 Matthias Lübben <ml81@gmx.de>
//

using Microsoft.Win32;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Steganography.App
{
    /// <summary>
    /// Interaktionslogik für DecoderControl.xaml
    /// </summary>
    public partial class DecoderControl : UserControl
    {
        private readonly BitmapSteganographyDecoder decoder = new BitmapSteganographyDecoder();

        public DecoderControl()
        {
            InitializeComponent();

            inputImageTextBox.Text = ImageDefaults.GetDefaultDecoderInputFile();
        }

        private void decodeButton_Click(object sender, RoutedEventArgs e)
        {
            var inputFile = inputImageTextBox.Text;
            
            if (!System.IO.File.Exists(inputFile))
            {
                MessageBox.Show("Input file doesn't exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            var inputBitmap = new Bitmap(inputFile);

            byte[] data = decoder.Decode(inputBitmap);

            string text = Encoding.UTF8.GetString(data);

            messageTextBox.Text = text;

            MessageBox.Show("Image decoded.", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void browseInputImageButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                inputImageTextBox.Text = dlg.FileName;
            }
        }
    }
}
