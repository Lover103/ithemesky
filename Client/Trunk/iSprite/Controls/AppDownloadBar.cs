using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace iSprite
{
    public partial class AppDownloadBar : UserControl
    {
        public event AppDownloadBarEventHandler OnDoAction;
        public AppDownloadBar()
        {
            InitializeComponent();

            this.toolbtnStart.Click += new EventHandler
                (
                    delegate(object sender, EventArgs e)
                    {
                        DoAction(ActionOption.Start);
                    }
                );

            this.toolbtnPause.Click += new EventHandler
                (
                    delegate(object sender, EventArgs e)
                    {
                        DoAction(ActionOption.Pause);
                    }
                );

            this.toolbtnRemove.Click += new EventHandler
                (
                    delegate(object sender, EventArgs e)
                    {
                        DoAction(ActionOption.Remove);
                    }
                );
        }

        void DoAction(ActionOption option)
        {
            if (OnDoAction != null)
            {
                OnDoAction(option);
            }
        }
    }

    public enum ActionOption
    {
        Start,
        Remove,
        Pause
    }

    /// <summary>
    /// 申明委托
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public delegate void AppDownloadBarEventHandler(ActionOption option);
}
