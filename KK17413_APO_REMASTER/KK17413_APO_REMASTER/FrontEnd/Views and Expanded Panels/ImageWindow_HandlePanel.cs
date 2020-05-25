﻿using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

using KK17413_APO_REMASTER.FrontEnd.Forms_and_Popups;
using KK17413_APO_REMASTER.BackEnd.Factories;
using KK17413_APO_REMASTER.BackEnd;
using System.Data;

namespace KK17413_APO_REMASTER.FrontEnd.Views_and_Expanded_Panels
{
    [System.ComponentModel.DesignerCategory("")]
    public class ImageWindow_HandlePanel : Panel
    {

        public ImageForm_Service SERVICE;

        private Button closeButton;
        private Panel mainButton;
        private Label mbText; // mainButtonText

        // TODO:
        // Issiue: You can scroll the handleContainer when form's not active

        // ##########################################################################
        public ImageWindow_HandlePanel(string filename)
        {
            mbText = new Label();
            mainButton = new Panel();

            mainButton.Width = 72;
            mainButton.Height = 23;

            Size minSize = new Size(mainButton.Width, mainButton.Height);
            Size maxSize = new Size(200, mainButton.Height);
            mbText.MinimumSize = minSize;
            mbText.MaximumSize = maxSize;
            mainButton.MinimumSize = minSize;
            mainButton.MaximumSize = maxSize;
            this.MinimumSize = minSize;
            this.MaximumSize = maxSize;

            mbText.AutoEllipsis = false;
            mbText.AutoSize = true;
            mbText.Text = CalculateText(filename);
            mbText.TextAlign = ContentAlignment.TopLeft;

            mainButton.BorderStyle = BorderStyle.FixedSingle;

            closeButton = new Button();
            closeButton.BackColor = Color.IndianRed;
            closeButton.FlatStyle = FlatStyle.Flat;
            const int closeButtonPadding = 8;
            closeButton.Width = mainButton.Height - closeButtonPadding;
            closeButton.Height = mainButton.Height - closeButtonPadding;
            closeButton.Left = (closeButtonPadding - 1) / 2;
            closeButton.Top = (closeButtonPadding - 1) / 2;
            mbText.Left = 0;
            mbText.Top = (closeButtonPadding - 1) / 2;

            closeButton.Click += closeButton_Click;
            mainButton.Click += button_Click;
            mainButton.DoubleClick += button_DoubleClick;
            mbText.Click += button_Click;
            mbText.DoubleClick += button_DoubleClick;

            mainButton.Controls.Add(closeButton);
            mainButton.Controls.Add(mbText);
            this.Controls.Add(mainButton);

            int buttonWidth = mbText.Width;
            int buttonHeight = mainButton.Height;
            mbText.AutoSize = false;
            mbText.AutoEllipsis = true;

            mbText.Width = buttonWidth;
            mbText.Height = buttonHeight;
            this.Width = buttonWidth;
            this.Height = buttonHeight;
            mainButton.Width = buttonWidth;
            mainButton.Height = buttonHeight;
        }

        // ##########################################################################
        public void Clear()
        {
            closeButton = null;
            mainButton = null;
            mbText = null;
        }

        public void ReloadColorSet(ColorSet ColorSet)
        {
            mbText.ForeColor = ColorSet.GetValue("fontColor");
            mainButton.BackColor = ColorSet.GetValue("bgColorLayer2");
            closeButton.ForeColor = ColorSet.GetValue("fontColor");
        }

        // ##########################################################################
        private void closeButton_Click(object sender, EventArgs e)
        => SERVICE.CloseWindow();

        private void button_Click(object sender, EventArgs e)
        =>SERVICE.ShowWindow();

        private void button_DoubleClick(object sender, EventArgs e)
        => SERVICE.HideAllWindowsExceptOne();

        // ##########################################################################
        private string CalculateText(string filename)
        {
            if (filename == null) return filename;

            var value = new FileInfo(filename);

            return "       " + value.Name;
        }
    }
}
