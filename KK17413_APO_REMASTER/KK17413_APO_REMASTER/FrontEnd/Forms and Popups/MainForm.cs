using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using KK17413_APO_REMASTER.FrontEnd.Toolbox_Tools_Expanded;
using KK17413_APO_REMASTER.BackEnd;
using KK17413_APO_REMASTER.BackEnd.Factories;


namespace KK17413_APO_REMASTER.FrontEnd.Forms_and_Popups
{
    [System.ComponentModel.DesignerCategory("")]
    public partial class MainForm //: i_Form //: AdjustedForm
    {
        public AdjustedForm Form;

        // #################################################################################################
        public Taskbar taskbar;
        public Panel dragNdropContainer;
        public Label dragNdropText1;
        public Label dragNdropText2;

        public FlowLayoutPanel pageHandlersContainer;
        public MenuStrip menuStrip;

        public ToolStripMenuItem file_tsmi;
        public ToolStripMenuItem open_tsmi;
        public ToolStripMenuItem project_tsmi;
        public ToolStripMenuItem settings_tsmi;
        public ToolStripMenuItem language_tsmi;
        public ToolStripMenuItem colorTheme_tsmi;
        public List<ToolStripMenuItem> Language_tsmis;
        public List<ToolStripMenuItem> Color_tsmis;
        // *tsmi - Tool Strip Menu Item

        // Kontakt ze światem:
        Program PROGRAM;


        // #################################################################################################
        
        public void AssignProgramReference(Program program)
        {
            this.PROGRAM = program;
        }
        /*
        public void DetachPageHandle(ImageForm_Handle pageHandle)
        {
            pageHandlersContainer.Controls.Remove(pageHandle);
        }
        //*/
        public void AssignEventHandlers()
        {
            // Assigning EventHandlers:
            this.Form.Resize += new EventHandler(mainForm_Resize);

            //dragNdropContainer.DragDrop += dragNdropContainer_DragDrop;
            dragNdropContainer.DragEnter += dragNdropContainer_DragEnter;

            //open_tsmi.Click += new EventHandler(open_tsmi_Click);
            //project_tsmi.Click += new EventHandler(project_tsmi_Click);

            pageHandlersContainer.MouseMove += MouseFix_MouseMove;
            menuStrip.MouseMove += MouseFix_MouseMove;
            dragNdropContainer.MouseMove += MouseFix_MouseMove;
        }


        // #################################################################################################
        public void Init_Language_tsmis(List<string> LanguageKeys)
        {
            foreach (var key in LanguageKeys)
            {
                ToolStripMenuItem tmp_tsmi = new ToolStripMenuItem()
                {
                    Name = key,
                    Text = key
                };
                language_tsmi.DropDownItems.Add(tmp_tsmi);
                Language_tsmis.Add(tmp_tsmi);
            }

            foreach (var obj in Language_tsmis)
                obj.Click += Language_tsmis_Click;
        }        
        public void Init_ColorSet_tsmis(List<string> ColorSetKeys)
        {
            foreach (var key in ColorSetKeys)
            {
                ToolStripMenuItem tmp_tsmi = new ToolStripMenuItem()
                {
                    Name = key,
                    Text = key
                };
                colorTheme_tsmi.DropDownItems.Add(tmp_tsmi);
                Color_tsmis.Add(tmp_tsmi);
            }

            foreach (var obj in Color_tsmis)
                obj.Click += Color_tsmis_Click;
        }

        public void ReloadLanguage(Language LanguageSet)
        {
            file_tsmi.Text = LanguageSet.GetValue("file_tsmi");
            open_tsmi.Text = LanguageSet.GetValue("open_tsmi");
            project_tsmi.Text = LanguageSet.GetValue("project_tsmi");
            settings_tsmi.Text = LanguageSet.GetValue("settings_tsmi");
            language_tsmi.Text = LanguageSet.GetValue("language_tsmi");
            colorTheme_tsmi.Text = LanguageSet.GetValue("colorTheme_tsmi");
        }

        public void ReloadColorSet(ColorSet ColorSet)
        {
            // This Form Layout:
            this.Form.Workspace.BackColor = ColorSet.GetValue("bgColorLayer2");
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

            open_tsmi.ForeColor = ColorSet.GetValue("fontColor");
            open_tsmi.BackColor = ColorSet.GetValue("bgColorLayer1");
            language_tsmi.ForeColor = ColorSet.GetValue("fontColor");
            language_tsmi.BackColor = ColorSet.GetValue("bgColorLayer1");
            colorTheme_tsmi.ForeColor = ColorSet.GetValue("fontColor");
            colorTheme_tsmi.BackColor = ColorSet.GetValue("bgColorLayer1");
            foreach (var obj in Language_tsmis)
            {
                obj.ForeColor = ColorSet.GetValue("fontColor");
                obj.BackColor = ColorSet.GetValue("bgColorLayer1");
            }
            foreach (var obj in Color_tsmis)
            {
                obj.ForeColor = ColorSet.GetValue("fontColor");
                obj.BackColor = ColorSet.GetValue("bgColorLayer1");
            }
        }

        public void ResizeItems()
        {
            dragNdropText1.Top = (dragNdropContainer.Height / 2) - (dragNdropText1.Height / 2);
            dragNdropText2.Top = dragNdropText1.Top + dragNdropText1.Height;

            dragNdropText1.Left = (dragNdropContainer.Width / 2) - dragNdropText1.Width / 2;
            dragNdropText2.Left = (dragNdropContainer.Width / 2) - dragNdropText2.Width / 2;
        }

        /*
        private void CreateImageWorkspace(string filename = null)
        {
            // Create new ImagePage:
            ImageForm newPage = new ImageForm_Builder().GetResult(filename);

            // Create new PageHandle:
            ImageForm_Handle newPageHandle = new ImageForm_Handle(this, newPage, filename);

            // Assign new page handle to the new image page:
            newPage.PageHandle = newPageHandle;

            // Assign new page handle to the MainForm:
            pageHandlersContainer.Controls.Add(newPageHandle);

            // Add new page to the list:
            ProgramSettings.Pages.Add(newPage);
        }
        //*/

        // #################################################################################################
        public void mainForm_Resize(object sender, EventArgs e)
        {
            ResizeItems();
        }

        private void MouseFix_MouseMove(object sender, MouseEventArgs e)
        {
            Form.MouseFix();
        }

        // #################################################################################################
        /*
        public void open_tsmi_Click(object sender, EventArgs e)
        {
            foreach (string value in ProgramSettings.FileVerification.BrowseFiles())
                CreateImageWorkspace(value);
        }

        public void project_tsmi_Click(object sender, EventArgs e)
        {
            CreateImageWorkspace();
        }
        //*/
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
        private void dragNdropContainer_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        /*
        private void dragNdropContainer_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            foreach (string value in files)
                CreateImageWorkspace(value);
        }
        //*/
        // #################################################################################################
    }
}
