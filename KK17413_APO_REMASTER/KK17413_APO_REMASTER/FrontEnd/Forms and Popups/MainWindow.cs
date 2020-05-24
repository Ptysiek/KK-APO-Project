using System;
using System.Windows.Forms;
using System.Collections.Generic;

using KK17413_APO_REMASTER.FrontEnd.Toolbox_Tools_Expanded;
using KK17413_APO_REMASTER.BackEnd;
using KK17413_APO_REMASTER.BackEnd.Factories;
using KK17413_APO_REMASTER.FrontEnd.Views_and_Expanded_Panels;

namespace KK17413_APO_REMASTER.FrontEnd.Forms_and_Popups
{
    [System.ComponentModel.DesignerCategory("")]
    public partial class MainWindow
    {
        public Program PROGRAM;
        public AdjustedForm Form;

        // #################################################################################################
        public Taskbar taskbar;
        public Panel dragNdropContainer;
        public Label dragNdropText1;
        public Label dragNdropText2;

        public FlowLayoutPanel pageHandlersContainer;
        public MenuStrip menuStrip;

        public List<ToolStripMenuItem> Menu_tsmis;
        public List<ToolStripMenuItem> Language_tsmis;
        public List<ToolStripMenuItem> Color_tsmis;
        // *tsmi - Tool Strip Menu Item


        // #################################################################################################
        
        public void DetachPageHandle(ImageWindow_HandlePanel pageHandle)
        {
            pageHandlersContainer.Controls.Remove(pageHandle);
        }        
        
        public void ReloadLanguage(Language LanguageSet)
        {
            foreach (var tsmi in Menu_tsmis)            
                tsmi.Text = LanguageSet.GetValue(tsmi.Name);            
        }
        
        public void ReloadColorSet(ColorSet ColorSet)
        {
            // Image_HandlePanel:
            foreach (ImageWindow_HandlePanel panel in pageHandlersContainer.Controls)
            {
                panel.ReloadColorSet(ColorSet);
            }

            // This Form Layout:
            Form.Workspace.BackColor = ColorSet.GetValue("bgColorLayer2");
            taskbar.ForeColor = ColorSet.GetValue("fontColor");
            taskbar.BackColor = ColorSet.GetValue("bgColorLayer2");
            taskbar.IconChangeColor(ColorSet.GetValue("detailColor2"));

            dragNdropText1.ForeColor = ColorSet.GetValue("detailColor2");
            dragNdropText2.ForeColor = ColorSet.GetValue("detailColor2");
            pageHandlersContainer.BackColor = ColorSet.GetValue("bgColorLayer2");
            dragNdropContainer.BackColor = ColorSet.GetValue("bgColorLayer3");

            // MenuStrip:
            menuStrip.ForeColor = ColorSet.GetValue("fontColor");
            menuStrip.BackColor = ColorSet.GetValue("bgColorLayer1");

            foreach (var tsmi in Menu_tsmis)
            {
                tsmi.ForeColor = ColorSet.GetValue("fontColor");
                tsmi.BackColor = ColorSet.GetValue("bgColorLayer1");
            }

            foreach (var tsmi in Language_tsmis)
            {
                tsmi.ForeColor = ColorSet.GetValue("fontColor");
                tsmi.BackColor = ColorSet.GetValue("bgColorLayer1");
            }
            foreach (var tsmi in Color_tsmis)
            {
                tsmi.ForeColor = ColorSet.GetValue("fontColor");
                tsmi.BackColor = ColorSet.GetValue("bgColorLayer1");
            }
        }
        
        public void ResizeItems()
        {
            dragNdropText1.Top = (dragNdropContainer.Height / 2) - (dragNdropText1.Height / 2);
            dragNdropText2.Top = dragNdropText1.Top + dragNdropText1.Height;

            dragNdropText1.Left = (dragNdropContainer.Width / 2) - dragNdropText1.Width / 2;
            dragNdropText2.Left = (dragNdropContainer.Width / 2) - dragNdropText2.Width / 2;
        }

        // #################################################################################################
        public void mainForm_Resize(object sender, EventArgs e)
        => ResizeItems();        

        public void MouseFix_MouseMove(object sender, MouseEventArgs e)
        => Form.MouseFix();        

        public void open_tsmi_Click(object sender, EventArgs e)
        => PROGRAM.BrowseFiles();        

        public void project_tsmi_Click(object sender, EventArgs e)
        => PROGRAM.Build_ImageWindow();

        // #################################################################################################
        public void dragNdropContainer_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
        
        public void dragNdropContainer_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            PROGRAM.TestFiles(files);
        }
        
        // #################################################################################################
        public void Language_tsmis_Click(object sender, EventArgs e)
        {
            foreach (var obj in Language_tsmis)
            {
                if (sender.Equals(obj))
                {
                    PROGRAM.SetLanguage(obj.Name);
                    return;                    
                }
            }
        }
        
        public void Color_tsmis_Click(object sender, EventArgs e)
        {
            foreach (var obj in Color_tsmis)
            {
                if (sender.Equals(obj))
                {
                    PROGRAM.SetColorSet(obj.Name);
                    return;
                }
            }
        }
        
        // #################################################################################################
    }
}
