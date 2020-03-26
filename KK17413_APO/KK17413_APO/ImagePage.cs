using System;
using System.Windows.Forms;


namespace KK17413_APO
{
    class ImagePage
    {
        public ImagePage(   Form form, 
                            MenuStrip menuStrip, 
                            HScrollBar h_scrollbar, 
                            VScrollBar v_scrollbar, 
                            PictureBox picture  
                        )
        {
            this.form = form;
            this.menuStrip = menuStrip;
            this.h_scrollbar = h_scrollbar;
            this.v_scrollbar = v_scrollbar;
            this.picture = picture;
        }


        private Form form;
        private MenuStrip menuStrip;

        private HScrollBar h_scrollbar;
        private VScrollBar v_scrollbar;
        private PictureBox picture;
    }
}
