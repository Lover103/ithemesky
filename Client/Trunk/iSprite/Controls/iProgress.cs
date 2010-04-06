using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using Manzana;
using System.IO;

namespace iSprite
{
    internal delegate void CancelHandler(object sender, bool cancel);


    internal partial class iProgress : iUserControl
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
            this.progressbar.Position = 0;
            this.Visible = true;
        }

        public void SetProgress(FileProgressMode mode, ulong totalSize, ulong completeSize, int speed, double timeElapse, string file)
        {
            this.Visible = true;
            if (totalSize > Int32.MaxValue)
            {
                totalSize = Int32.MaxValue;
                completeSize = (ulong)(completeSize * (Int32.MaxValue * 1.0 / totalSize));
            }

            if (completeSize > totalSize)
            {
                completeSize = totalSize;
            }

            double perent = 0;
            if (totalSize > 0)
            {
                perent = completeSize * 1.0 / totalSize;
            }

            if (perent.ToString("P") == this.progressbar.Text)
            {
                return;
            }

            Percent = perent.ToString("P");
            this.Maximum = Convert.ToInt32(totalSize);
            this.Value = Convert.ToInt32(completeSize);
            Application.DoEvents();
            switch (mode)
            {
                case FileProgressMode.PC2iPhone:
                    this.Title = string.Format("Copy file({0}) to iPhone...", Path.GetFileName(file));
                    break;
                case FileProgressMode.iPhone2PC:
                    this.Title = string.Format("Copy file({0}) from iPhone...", Path.GetFileName(file));
                    break;
                case FileProgressMode.Internet2PC:
                    this.Title = string.Format("Download file({0}) from Internet...", Path.GetFileName(file));
                    break;
            }
            if (perent > 0 && timeElapse > 0)
            {
                int timeleft = (int)(timeElapse * ((1 - perent) / perent));
                this.Status = string.Format(
                    "Transmited {0}, Speed {1}, Time Left {2}",
                    Utility.FormatFileSizeFloat(completeSize),
                    Utility.FormatFileSizeFloat((ulong)speed),
                    string.Format("{0:00}:{1:00}",timeleft/60,timeleft%60)
                    );
            }
            else
            {
                this.Status = string.Format(
                    "Transmited {0}, Speed {1}",
                    Utility.FormatFileSizeFloat(completeSize),
                    Utility.FormatFileSizeFloat((ulong)speed)
                    );
            }
        }

        public new bool Visible
        {
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new ThreadInvokeDelegate(
                        delegate()
                        {
                            base.Visible = value;
                        }
                    ));
                }
                else
                {
                    base.Visible = value;
                }
            }
        }

        public string Title
        {
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new ThreadInvokeDelegate(
                        delegate()
                        {
                            this.lblTitle.Text = value;
                        }
                    ));
                }
                else
                {
                    this.lblTitle.Text = value;
                }
            }
        }
        public string Status
        {
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new ThreadInvokeDelegate(
                        delegate()
                        {
                            this.lblStatus.Text = value;
                        }
                    ));
                }
                else
                {
                    this.lblStatus.Text = value;
                }
            }
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
                            this.progressbar.Text = value;
                        }
                    ));
                }
                else
                {
                    this.progressbar.Text = value;
                }
            }
        }

        public int Maximum
        {
            get 
            {
                return this.progressbar.PositionMax;
            }
            set 
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new ThreadInvokeDelegate(
                        delegate()
                        {
                            this.progressbar.PositionMax = value; 
                        }
                    ));
                }
                else
                {
                    this.progressbar.PositionMax = value; 
                }
            }
        }


        public int Minimum
        {
            get
            {
                return this.progressbar.PositionMin;
            }
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new ThreadInvokeDelegate(
                        delegate()
                        {
                            this.progressbar.PositionMin = value;
                        }
                    ));
                }
                else
                {
                    this.progressbar.PositionMin = value;
                }
            }
        }

        public int Value
        {
            get
            {
                return this.progressbar.Position;
            }
            set
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new ThreadInvokeDelegate(
                        delegate()
                        {
                            this.progressbar.Position = value;
                        }
                    ));
                }
                else
                {
                    this.progressbar.Position = value;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            RaiseCancelHandler(this,true);
        }
    }
}
