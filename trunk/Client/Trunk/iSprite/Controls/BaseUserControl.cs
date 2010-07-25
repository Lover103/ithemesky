using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace iSprite
{
    internal partial class BaseUserControl : UserControl
    {
        #region 变量定义
        internal event SetNodeCountHandler OnSetNodeCount;
        protected AppHelper m_appHelper;
        internal event MessageHandler OnMessage;
        public event UpdataCatalogCountHandler OnUpdataCatalogCount;
        protected iPhoneFileDevice m_iPhoneDevice;
        protected ContextMenuStrip m_ctxTools;
        #endregion

        #region 设置节点数量
        /// <summary>
        /// 设置节点数量
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="count"></param>
        /// <param name="selectNode"></param>
        protected void SetNodeCount(string nodeName, int count, bool selectNode)
        {
            if (null != OnSetNodeCount)
            {
                OnSetNodeCount(nodeName, count, selectNode);
            }
        }
        #endregion

        #region 更新类别数量
        /// <summary>
        /// 更新类别数量
        /// </summary>
        protected void UpdataCatalogCount()
        {
            if (null != OnUpdataCatalogCount)
            {
                OnUpdataCatalogCount();
            }
        }
        #endregion

        #region 消息处理
        /// <summary>
        /// 消息处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Message"></param>
        /// <param name="messageType"></param>
        protected void RaiseMessageHandler(object sender, string Message, MessageTypeOption messageType)
        {
            if (OnMessage != null)
            {
                OnMessage(sender, Message, messageType);
            }
        }
        #endregion

        protected void AddContextMenu(Control listView, ToolStrip toolStrip, EventHandler OnClick)
        {
            m_ctxTools = new ContextMenuStrip();
            ToolStripButton btn;
            foreach (ToolStripItem item in toolStrip.Items)
            {
                if (item is ToolStripButton)
                {
                    btn = new ToolStripButton(item.Text);
                    btn.Click += OnClick;
                    item.Click += OnClick;
                    m_ctxTools.Items.Add(btn);
                    m_ctxTools.Items.Add(new ToolStripSeparator());
                }
            }
            btn = new ToolStripButton("Cancel");
            btn.Click += OnClick;
            m_ctxTools.Items.Add(btn);
            listView.ContextMenuStrip = m_ctxTools;
        }
    }
}
