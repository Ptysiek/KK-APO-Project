using System;
using System.Windows.Forms;


namespace KK17413_APO.Forms_and_Pages
{
    class PageHandle
    {
        private Button button;
        private ImagePage pageRef;


        // ##########################################################################
        public PageHandle(ImagePage pageRef)
        {
            button = new Button();
            this.pageRef = pageRef;

            button.Click += button_Click;
            button.DoubleClick += button_DoubleClick;
        }


        // ##########################################################################
        private void button_Click(object sender, EventArgs e)
        {
            //pageRef.
        }

        private void button_DoubleClick(object sender, EventArgs e)
        {
            
        }


        // ##########################################################################       
    }
}
