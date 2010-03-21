using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace iSprite
{
    public delegate void CancelHandler(object sender, bool cancel);


    public partial class iProgress : UserControl
    {

        /// <summary>
        /// 跨线程调用委托
        /// </summary>
        private delegate void ThreadInvokeDelegate();

        internal event CancelHandler OnCancel;

        private void RaiseCancelHandler(object sender, bool cancel)
        {
            if (OnCancel != null)
            {
                OnCancel(sender, cancel);
            }
        }

        public iProgress()
        {
            InitializeComponent();
            this.progressbar.Value = 0;
            this.Visible = true;
        }

        public void SetProgress(ulong total, ulong currentValue)
        {
            this.Visible = true;
            if (total > Int32.MaxValue)
            {
                total = Int32.MaxValue;
                currentValue = (ulong)(currentValue * (Int32.MaxValue * 1.0 / total));
            }

            if (currentValue > total)
            {
                currentValue = total;
            }

            double perent = 0;
            if (total > 0)
            {
                perent = currentValue * 1.0 / total;
            }

            if (perent.ToString("P") == this.lblpercent.Text)
            {
                return;
            }

            Percent = perent.ToString("P");
            this.Maximum = Convert.ToInt32(total);
            this.Value = Convert.ToInt32(currentValue);
            Application.DoEvents();
            
        }

        public string Percent
        {
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new ThreadInvokeDelegate(
                        delegate()
                        {
                            this.lblpercent.Text = value;
                        }
                    ));
                }
                else
                {
                    this.lblpercent.Text = value;
                }
            }
        }

        public int Maximum
        {
            get 
            {
                return this.progressbar.Maximum;
            }
            set 
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new ThreadInvokeDelegate(
                        delegate()
                        {
                            this.progressbar.Maximum = value; 
                        }
                    ));
                }
                else
                {
                    this.progressbar.Maximum = value; 
                }
            }
        }


        public int Minimum
        {
            get
            {
                return this.progressbar.Minimum;
            }
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new ThreadInvokeDelegate(
                        delegate()
                        {
                            this.progressbar.Minimum = value;
                        }
                    ));
                }
                else
                {
                    this.progressbar.Minimum = value;
                }
            }
        }

        public int Value
        {
            get
            {
                return this.progressbar.Value;
            }
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new ThreadInvokeDelegate(
                        delegate()
                        {
                            this.progressbar.Value = value;
                        }
                    ));
                }
                else
                {
                    this.progressbar.Value = value;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            RaiseCancelHandler(this,true);
        }
    }
}
