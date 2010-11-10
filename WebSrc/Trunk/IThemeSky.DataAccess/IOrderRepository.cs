using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IThemeSky.Model;

namespace IThemeSky.DataAccess
{
    public interface IOrderRepository
    {
        List<ThemeDownloadHistory> GetPaidThemeDownloadHistory(int themeId);

        bool AddOrder(UserOrder order);

        bool UpdateOrderFromIPN(IThemeSky.Model.UserOrder order);

        bool UpdateOrderStatus(string orderNumber, string payerMail, int status, string description);

        UserOrder GetOrder(int themeId, string orderNumber);
    }
}
