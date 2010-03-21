namespace iSprite.ThirdControl.FarsiLibrary.BaseClasses
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Windows.Forms.VisualStyles;

    [ToolboxItem(false)]
    public class BaseStyledPanel : ContainerControl
    {
        private static ToolStripProfessionalRenderer renderer = new ToolStripProfessionalRenderer();

        public event EventHandler ThemeChanged;

        public BaseStyledPanel()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //base.SetStyle(0x20000, true);--cxb
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnSystemColorsChanged(EventArgs e)
        {
            base.OnSystemColorsChanged(e);
            this.UpdateRenderer();
            base.Invalidate();
        }

        protected virtual void OnThemeChanged(EventArgs e)
        {
            if (this.ThemeChanged != null)
            {
                this.ThemeChanged(this, e);
            }
        }

        private void UpdateRenderer()
        {
            if (!this.UseThemes)
            {
                renderer.ColorTable.UseSystemColors=true;
            }
            else
            {
                renderer.ColorTable.UseSystemColors = false;
            }
        }

        [Browsable(false)]
        public ToolStripProfessionalRenderer ToolStripRenderer
        {
            get
            {
                return renderer;
            }
        }

        [DefaultValue(true), Browsable(false)]
        public bool UseThemes
        {
            get
            {
                return (
                    (VisualStyleRenderer.IsSupported && VisualStyleInformation.IsSupportedByOS) && Application.RenderWithVisualStyles);           }
        }
    }
}
