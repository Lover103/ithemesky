using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IThemeSky.WebControls
{
    //[DefaultProperty("FlashUpLoad")]
    //[ToolboxData("<{0}:FlashUpLoad runat=server></{0}:FlashUpLoad>")]
    public class FlashUpLoad : Control
    {

        [Category("Behavior")]
        [Description("Flash组件位置")]
        [DefaultValue("/Resources/FlashUpLoad.swf")]
        public string FlashPath
        {
            get
            {
                object o = ViewState["FlashPath"];
                if (o == null)
                    return "";
                return o.ToString();
            }
            set { ViewState["FlashPath"] = value; }
        }

        [Category("Behavior")]
        [Description("上传文件的ASP.NET处理页面")]
        [DefaultValue("")]
        public string UploadPage
        {
            get
            {
                object o = ViewState["UploadPage"];
                if (o == null)
                    return "";
                return o.ToString();
            }
            set { ViewState["UploadPage"] = value; }
        }

        [Category("Behavior")]
        [Description("上传页参数")]
        [DefaultValue("")]
        public string QueryParameters
        {
            get
            {
                object o = ViewState["QueryParameters"];
                if (o == null)
                    return "";
                return o.ToString();
            }
            set { ViewState["QueryParameters"] = value; }
        }

        [Category("Behavior")]
        [Description("文件上传后JavaScript调用的设置参数的方法.")]
        [DefaultValue("")]
        public string OnUploadComplete
        {
            get
            {
                object o = ViewState["OnUploadComplete"];
                if (o == null)
                    return "";
                return o.ToString();
            }
            set { ViewState["OnUploadComplete"] = value; }
        }

        [Category("Behavior")]
        [Description("设置允许上传的最大文件大小")]
        public decimal UploadFileSizeLimit
        {
            get
            {
                object o = ViewState["UploadFileSizeLimit"];
                if (o == null)
                    return 0;
                return (decimal)o;
            }
            set { ViewState["UploadFileSizeLimit"] = value; }
        }

        [Category("Behavior")]
        [Description("上传文件的总大小")]
        public decimal TotalUploadSizeLimit
        {
            get
            {
                object o = ViewState["TotalUploadSizeLimit"];
                if (o == null)
                    return 0;
                return (decimal)o;
            }
            set { ViewState["TotalUploadSizeLimit"] = value; }
        }

        [Category("Behavior")]
        [Description("上传文件的类型描述 (如. Images (*.JPG;*.JPEG;*.JPE;*.GIF;*.PNG;))")]
        [DefaultValue("")]
        public string FileTypeDescription
        {
            get
            {
                object o = ViewState["FileTypeDescription"];
                if (o == null)
                    return "";
                return o.ToString();
            }
            set { ViewState["FileTypeDescription"] = value; }
        }

        [Category("Behavior")]
        [Description("上传文件的类型(如. *.jpg; *.jpeg; *.jpe; *.gif; *.png;)")]
        [DefaultValue("")]
        public string FileTypes
        {
            get
            {
                object o = ViewState["FileTypes"];
                if (o == null)
                    return "";
                return o.ToString();
            }
            set { ViewState["FileTypes"] = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Page.Form.Enctype = "multipart/form-data";
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);

            ////生成Flash在页面的嵌入代码并设置其参数值
            //string url = Page.ClientScript.GetWebResourceUrl(this.GetType(), FLASH_SWF);
            string obj = string.Format("<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\"" +
                        " codebase=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0\"" +
                        " width=\"575\" height=\"375\" id=\"fileUpload\" align=\"middle\">" +
                        "<param name=\"allowScriptAccess\" value=\"sameDomain\" />" +
                        "<param name=\"movie\" value=\"{0}\" />" +
                        "<param name=\"quality\" value=\"high\" />" +
                        "<param name=\"wmode\" value=\"transparent\" />" +
                        "<param name=\"FlashVars\" value='{3}{4}{5}{6}{7}&uploadPage={1}?{2}'/>" +
                        "<embed src=\"{0}\"" +
                        " FlashVars='{3}{4}{5}{6}{7}&uploadPage={1}?{2}'" +
                        " quality=\"high\" wmode=\"transparent\" width=\"575\" height=\"375\" " +
                        " name=\"fileUpload\" align=\"middle\" allowScriptAccess=\"sameDomain\" " +
                        " type=\"application/x-shockwave-flash\" " +
                        " pluginspage=\"http://www.macromedia.com/go/getflashplayer\" />" +
                        "</object>",
                        FlashPath,
                        ResolveUrl(UploadPage),
                        HttpContext.Current.Server.UrlEncode(QueryParameters),
                        string.IsNullOrEmpty(OnUploadComplete) ? "" : "&completeFunction=" + OnUploadComplete,
                        string.IsNullOrEmpty(FileTypes) ? "" : "&fileTypes=" + HttpContext.Current.Server.UrlEncode(FileTypes),
                        string.IsNullOrEmpty(FileTypeDescription) ? "" : "&fileTypeDescription=" + HttpContext.Current.Server.UrlEncode(FileTypeDescription),
                        TotalUploadSizeLimit > 0 ? "&totalUploadSize=" + TotalUploadSizeLimit : "",
                        UploadFileSizeLimit > 0 ? "&fileSizeLimit=" + UploadFileSizeLimit : ""
                        );
            writer.Write(obj);
        }
    }
}
