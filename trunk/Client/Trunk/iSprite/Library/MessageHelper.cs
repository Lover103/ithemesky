using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace iSprite
{
    internal class MessageHelper
    {
        MessageHelper()
        { 
        }

        public static DialogResult ShowError(string message)
        {
            return ShowError("Error", message);
        }

        public static DialogResult ShowError(string title, string message)
        {
            return iSpriteMessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult ShowInfo(string message)
        {
            return ShowInfo("Information", message);
        }
        public static DialogResult ShowInfo(string title, string message)
        {
            return iSpriteMessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        public static DialogResult ShowConfirm(string message)
        {
            return ShowConfirm("Confirm", message);
        }
        public static DialogResult ShowConfirm(string title, string message)
        {
            return iSpriteMessageBox.Show(message, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }

        public static DialogResult ShowWarning(string message)
        {
            return ShowWarning("Error", message);
        }

        public static DialogResult ShowWarning(string title, string message)
        {
            return iSpriteMessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
