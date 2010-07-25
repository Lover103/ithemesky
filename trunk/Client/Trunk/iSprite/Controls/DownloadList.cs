using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using MyDownloader.Core;
using System.Collections;
using System.IO;
using MyDownloader.Core.Common;
using System.Web;

namespace iSprite
{
    public delegate void InstallAppHandler(Downloader d);
    public delegate void SaveDownQueueHandler();
    /// <summary>
    /// 下载队列视图
    /// </summary>
    public partial class DownloadList : ListView
    {
        delegate void ActionDownloader(Downloader d, ListViewItem item);
        public event UpdataListViewCountHandler OnUpdateCount;

        Hashtable mapItemToDownload = new Hashtable();
        Hashtable mapDownloadToItem = new Hashtable();
        ListViewItem lastSelection = null;

        int colIndex_File;
        int colIndex_Size;
        int colIndex_Progress;
        int colIndex_Left;
        int colIndex_Rate;
        int colIndex_State;
        int colIndex_Installed;

        #region 更新数量
        /// <summary>
        /// 更新数量
        /// </summary>
        /// <param name="count"></param>
        private void RaiseUpdateCount(int count)
        {
            if (OnUpdateCount != null)
            {
                OnUpdateCount(count);
            }
        }
        #endregion

        public event InstallAppHandler OnDoInstall;
        /// <summary>
        /// 安装软件
        /// </summary>
        /// <param name="d"></param>
        void DoInstall(Downloader d)
        {
            if (OnDoInstall != null)
            {
                OnDoInstall(d);
            }
        }

        public event SaveDownQueueHandler OnSaveDownQueue;
        /// <summary>
        /// 保存下载队列
        /// </summary>
        void SaveDownQueue()
        {
            if (OnSaveDownQueue != null)
            {
                OnSaveDownQueue();
            }
        }

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public DownloadList()
        {
            Initialise();

            //下载任务添加事件
            DownloadManager.Instance.DownloadAdded += new EventHandler<DownloaderEventArgs>(Instance_DownloadAdded);
            //下载任务移除事件
            DownloadManager.Instance.DownloadRemoved += new EventHandler<DownloaderEventArgs>(Instance_DownloadRemoved);
            //下载任务结束事件
            DownloadManager.Instance.DownloadEnded += new EventHandler<DownloaderEventArgs>(Instance_DownloadEnded);

            for (int i = 0; i < DownloadManager.Instance.Downloads.Count; i++)
            {
                AddDownload(DownloadManager.Instance.Downloads[i]);
            }

            //开始批量添加下载任务
            DownloadManager.Instance.BeginAddBatchDownloads += new EventHandler(Instance_BeginAddBatchDownloads);
            //结束批量添加下载任务
            DownloadManager.Instance.EndAddBatchDownloads += new EventHandler(Instance_EndAddBatchDownloads);

            this.GridLines = true;
            this.FullRowSelect = true;
            this.View = View.Details;
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        void Initialise()
        {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 18);//分别是宽和高
            this.SmallImageList = imgList; 

            ColumnHeader columnFile = new ColumnHeader();
            ColumnHeader columnSize = new ColumnHeader();
            ColumnHeader columnProgress = new ColumnHeader();
            ColumnHeader columnLeft = new ColumnHeader();
            ColumnHeader columnRate = new ColumnHeader();
            ColumnHeader columnState = new ColumnHeader();
            ColumnHeader columnInstalled = new ColumnHeader();

            columnFile.Text = "File";
            columnFile.Width = 260;

            columnSize.Text = "Size";
            columnSize.Width = 80;

            columnProgress.Text = "Progress";
            columnProgress.Width = 63;

            columnLeft.Text = "Left";
            columnLeft.Width = 70;

            columnRate.Text = "Rate";
            columnRate.Width = 72;

            columnState.Text = "Down State";
            columnState.Width = 100;

            columnInstalled.Text = "Installed";
            columnInstalled.Width = 80;

            this.Columns.AddRange(
                new ColumnHeader[] 
                {
                    columnFile,
                    columnSize,
                    columnProgress,
                    columnLeft,
                    columnRate,
                    columnState,
                    columnInstalled
                }
            );

            colIndex_File = 0;
            colIndex_Size = 1;
            colIndex_Progress = 2;
            colIndex_Left = 3;
            colIndex_Rate = 4;
            colIndex_State = 5;
            colIndex_Installed = 6;
        }
        #endregion

        #region 添加下载任务
        /// <summary>
        /// 添加下载任务
        /// </summary>
        /// <param name="url"></param>
        /// <param name="savePath"></param>
        /// <param name="fileName"></param>
        /// <param name="state"></param>
        public void AddURL2Download(string url, string savePath, string fileName,InstallState state)
        {
            ResourceLocation location = new ResourceLocation();
            location.URL = url;
            int segments = 5;

            string fullfilename = savePath + fileName;
            Downloader d = DownloadManager.Instance.Add(
                location,
                null,
                fullfilename,
                segments,
                true
                );

            d.InstallCode = state;

            SaveDownQueue();
        }
        #endregion

        #region 下载任务结束事件
        /// <summary>
        /// 下载任务结束事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Instance_DownloadEnded(object sender, DownloaderEventArgs e)
        {
            if (e.Downloader.State == DownloaderState.Finished)
            {
                Downloader d = e.Downloader;
                //完成文件下载
                if (d.InstallCode == InstallState.NeedInstall || d.InstallCode == InstallState.DependInstall)
                {
                    DoInstall(e.Downloader);
                }
            }
            else
            {
            }
            RaiseUpdateCount(this.Items.Count);
        }
        #endregion

        #region 选中的下载任务数量
        /// <summary>
        /// 选中的下载任务数量
        /// </summary>
        public int SelectedCount
        {
            get
            {
                return this.SelectedItems.Count;
            }
        }
        #endregion

        #region 选中的下载任务
        /// <summary>
        /// 选中的下载任务
        /// </summary>
        public Downloader[] SelectedDownloaders
        {
            get
            {
                if (this.SelectedItems.Count > 0)
                {
                    Downloader[] downloaders = new Downloader[this.SelectedItems.Count];
                    for (int i = 0; i < downloaders.Length; i++)
                    {
                        downloaders[i] = mapItemToDownload[this.SelectedItems[i]] as Downloader;
                    }
                    return downloaders;
                }

                return null;
            }
        }
        #endregion

        #region 开始选中的下载任务
        /// <summary>
        /// 开始选中的下载任务
        /// </summary>
        public void StartSelections()
        {
            DownloadsAction(
                delegate(Downloader d, ListViewItem item)
                {
                    d.Start();
                }
            );
        }
        #endregion

        #region 暂停下载
        /// <summary>
        /// 暂停下载
        /// </summary>
        public void PauseSelections()
        {
            DownloadsAction(
                delegate(Downloader d, ListViewItem item)
                {
                    d.Pause();
                }
            );


            SaveDownQueue();
        }
        #endregion

        #region 全部暂停
        /// <summary>
        /// 全部暂停
        /// </summary>
        public void PauseAll()
        {
            DownloadManager.Instance.PauseAll();
            UpdateList();

            SaveDownQueue();
        }
        #endregion

        internal Downloader GetDownloaderByItem(ListViewItem item)
        {
            return (Downloader)mapItemToDownload[item];
        }

        #region 移除选中的下载任务
        /// <summary>
        /// 移除选中的下载任务
        /// </summary>
        public void RemoveSelections()
        {
            if (MessageHelper.ShowConfirm("Are you sure that you want to remove selected downloads") == DialogResult.OK)
            {
                try
                {
                    this.ItemSelectionChanged -= new ListViewItemSelectionChangedEventHandler(LV_ItemSelectionChanged);
                    DownloadManager.Instance.DownloadRemoved -= new EventHandler<DownloaderEventArgs>(Instance_DownloadRemoved);

                    DownloadsAction(
                        delegate(Downloader d, ListViewItem item)
                        {
                            this.Items.RemoveAt(item.Index);
                            DownloadManager.Instance.RemoveDownload(d);
                        }
                    );
                }
                finally
                {
                    this.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(LV_ItemSelectionChanged);
                    LV_ItemSelectionChanged(null, null);

                    DownloadManager.Instance.DownloadRemoved += new EventHandler<DownloaderEventArgs>(Instance_DownloadRemoved);
                }
                SaveDownQueue();
            }
        }
        #endregion

        #region 选中列表
        /// <summary>
        /// 选中列表
        /// </summary>
        public void SelectAll()
        {
            using (DownloadManager.Instance.LockDownloadList(false))
            {
                this.BeginUpdate();
                try
                {
                    this.ItemSelectionChanged -= new ListViewItemSelectionChangedEventHandler(LV_ItemSelectionChanged);

                    for (int i = 0; i < this.Items.Count; i++)
                    {
                        this.Items[i].Selected = true;
                    }
                }
                finally
                {
                    this.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(LV_ItemSelectionChanged);
                    LV_ItemSelectionChanged(null, null);

                    this.EndUpdate();
                }
            }
        }
        #endregion

        #region 移除已完成的下载任务
        /// <summary>
        /// 移除已完成的下载任务
        /// </summary>
        public void RemoveCompleted()
        {
            this.BeginUpdate();
            try
            {
                DownloadManager.Instance.ClearEnded();
                UpdateList();
            }
            finally
            {
                this.EndUpdate();
            }
        }
        #endregion

        #region 响应队列任务
        /// <summary>
        /// 响应队列任务
        /// </summary>
        /// <param name="action"></param>
        private void DownloadsAction(ActionDownloader action)
        {
            if (this.SelectedItems.Count > 0)
            {
                try
                {
                    this.BeginUpdate();

                    this.ItemSelectionChanged -= new ListViewItemSelectionChangedEventHandler(LV_ItemSelectionChanged);

                    for (int i = this.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        ListViewItem item = this.SelectedItems[i];
                        action((Downloader)mapItemToDownload[item], item);
                    }

                    this.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(LV_ItemSelectionChanged);
                    LV_ItemSelectionChanged(null, null);
                }
                finally
                {
                    this.EndUpdate();
                }
            }
        }
        #endregion

        #region 选择项变更事件处理
        /// <summary>
        /// 选择项变更事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LV_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            UpdateUI();
        }
        #endregion

        string GetInstallDisplay(InstallState s)
        {
            switch (s)
            {
                case InstallState.DependInstall:
                case InstallState.NeedInstall:
                case InstallState.Installing:
                case InstallState.NoNeedInstall:
                    return "No";
                case InstallState.InstallFinished:
                    return "Yes";
                case InstallState.Error:
                    return "Error";
                default:
                    return "No";
            }
        }

        #region 更新列表
        /// <summary>
        /// 更新列表
        /// </summary>
        public void UpdateList()
        {
            for (int i = 0; i < this.Items.Count; i++)
            {
                ListViewItem item = this.Items[i];
                if (item == null)
                {
                    return;
                }
                Downloader d = mapItemToDownload[item] as Downloader;
                if (d == null)
                {
                    return;
                }

                DownloaderState state;
                string oldinstallstate = item.SubItems[colIndex_Installed].Text;
                string newinstallstate = GetInstallDisplay(d.InstallCode);

                if (item.Tag == null)
                {
                    state = DownloaderState.Working;
                }
                else
                {
                    state = (DownloaderState)item.Tag;
                }

                if (state != d.State ||
                    state == DownloaderState.Working ||
                    state == DownloaderState.WaitingForReconnect
                    || oldinstallstate != newinstallstate
                    )
                {
                    item.SubItems[colIndex_Size].Text = ByteFormatter.ToString(d.FileSize);
                    item.SubItems[colIndex_Progress].Text = String.Format("{0:0.##}%", d.Progress);
                    item.SubItems[colIndex_Left].Text = TimeSpanFormatter.ToString(d.Left);
                    item.SubItems[colIndex_Rate].Text = String.Format("{0:F2}K", d.Rate / 1024.0);

                    if (d.LastError != null)
                    {
                        item.SubItems[colIndex_State].Text = d.State.ToString() + ", " + d.LastError.Message;
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(d.StatusMessage))
                        {
                            item.SubItems[colIndex_State].Text = d.State.ToString();
                        }
                        else
                        {
                            item.SubItems[colIndex_State].Text = d.State.ToString() + ", " + d.StatusMessage;
                        }
                    }
                    item.SubItems[colIndex_Installed].Text = newinstallstate;
                    item.Tag = d.State;
                }
            }
        }
        private void UpdateUI()
        {
        }
        #endregion

        #region 下载任务添加事件
        /// <summary>
        /// 下载任务添加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Instance_DownloadAdded(object sender, DownloaderEventArgs e)
        {
            if (IsHandleCreated)
            {
                this.BeginInvoke((MethodInvoker)delegate() { AddDownload(e.Downloader); });
            }
            else
            {
                AddDownload(e.Downloader);
            }
        }

        /// <summary>
        /// 添加下载任务
        /// </summary>
        /// <param name="d"></param>
        private void AddDownload(Downloader d)
        {
            d.RestartingSegment += new EventHandler<SegmentEventArgs>(download_RestartingSegment);
            d.SegmentStoped += new EventHandler<SegmentEventArgs>(download_SegmentEnded);
            d.SegmentFailed += new EventHandler<SegmentEventArgs>(download_SegmentFailed);
            d.SegmentStarted += new EventHandler<SegmentEventArgs>(download_SegmentStarted);
            d.InfoReceived += new EventHandler(download_InfoReceived);
            d.SegmentStarting += new EventHandler<SegmentEventArgs>(Downloader_SegmentStarting);

            string ext = Path.GetExtension(d.LocalFile);

            ListViewItem item = new ListViewItem();

            item.Text = Path.GetFileName(d.LocalFile);

            // size
            item.SubItems.Add(ByteFormatter.ToString(d.FileSize));

            // progress
            item.SubItems.Add(String.Format("{0:0.##}%", d.Progress));
            // left
            item.SubItems.Add(TimeSpanFormatter.ToString(d.Left));
            // rate
            item.SubItems.Add("0");

            item.SubItems.Add(d.State.ToString());
            item.SubItems.Add(GetInstallDisplay(d.InstallCode));

            mapDownloadToItem[d] = item;
            mapItemToDownload[item] = d;

            this.Items.Add(item);
            RaiseUpdateCount(this.Items.Count);
        }
        #endregion

        #region 下载任务移除事件
        /// <summary>
        /// 下载任务移除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Instance_DownloadRemoved(object sender, DownloaderEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate()
            {
                ListViewItem item = mapDownloadToItem[e.Downloader] as ListViewItem;

                if (item != null)
                {
                    if (item.Selected)
                    {
                        lastSelection = null;
                    }

                    mapDownloadToItem[e.Downloader] = null;
                    mapItemToDownload[item] = null;

                    item.Remove();
                    RaiseUpdateCount(this.Items.Count);
                }
            }
            );
        }
        #endregion

        #region 开始批量添加下载任务
        /// <summary>
        /// 开始批量添加下载任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Instance_BeginAddBatchDownloads(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)this.BeginUpdate);
        }
        #endregion

        #region 结束批量添加下载任务
        /// <summary>
        /// 结束批量添加下载任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Instance_EndAddBatchDownloads(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)this.EndUpdate);
        }
        #endregion

        #region 其他事件
        void download_InfoReceived(object sender, EventArgs e)
        {
            Downloader d = (Downloader)sender;

            Log(
                d,
                String.Format(
                "Connected to: {2}. File size = {0}, Resume = {1}",
                ByteFormatter.ToString(d.FileSize),
                d.RemoteFileInfo.AcceptRanges,
                d.ResourceLocation.URL),
                LogMode.Information);
        }

        void Downloader_SegmentStarting(object sender, SegmentEventArgs e)
        {
            Log(
                e.Downloader,
                String.Format(
                "Starting segment for {3}, start position = {0}, end position {1}, segment size = {2}",
                ByteFormatter.ToString(e.Segment.InitialStartPosition),
                ByteFormatter.ToString(e.Segment.EndPosition),
                ByteFormatter.ToString(e.Segment.TotalToTransfer),
                e.Downloader.ResourceLocation.URL),
                LogMode.Information);
        }

        void download_SegmentStarted(object sender, SegmentEventArgs e)
        {
            Log(
                e.Downloader,
                String.Format(
                "Started segment for {3}, start position = {0}, end position {1}, segment size = {2}",
                ByteFormatter.ToString(e.Segment.InitialStartPosition),
                ByteFormatter.ToString(e.Segment.EndPosition),
                ByteFormatter.ToString(e.Segment.TotalToTransfer),
                e.Downloader.ResourceLocation.URL),
                LogMode.Information);
        }

        void download_SegmentFailed(object sender, SegmentEventArgs e)
        {
            Log(
                e.Downloader,
                String.Format(
                "Download segment ({0}) failed for {2}, reason = {1}",
                e.Segment.Index,
                e.Segment.LastError.Message,
                e.Downloader.ResourceLocation.URL),
                LogMode.Error);
        }

        void download_SegmentEnded(object sender, SegmentEventArgs e)
        {
            Log(
                e.Downloader,
                String.Format(
                "Download segment ({0}) ended for {1}",
                e.Segment.Index,
                e.Downloader.ResourceLocation.URL),
                LogMode.Information);
        }

        void download_RestartingSegment(object sender, SegmentEventArgs e)
        {
            Log(
                e.Downloader,
                String.Format(
                "Download segment ({0}) is restarting for {1}",
                e.Segment.Index,
                e.Downloader.ResourceLocation.URL),
                LogMode.Information);
        }
        #endregion

        enum LogMode
        {
            Error,
            Information
        }

        void Log(Downloader downloader, string msg, LogMode m)
        {
            try
            {
                this.BeginInvoke(
                    (MethodInvoker)
                  delegate()
                  {
                  }
              );
            }
            catch { }
        }
    }
}
