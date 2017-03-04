using System;

namespace Business.DataAccess.Public.Core
{
    public static class Core
    {
        public static string GetDateString(DateTime? date)
        {
            if (date.HasValue)
                {
                    int DateDiff = (DateTime.Now - date.Value).Days;
                    if ((DateDiff < 1) && (DateTime.Now.Date==date.Value.Date))
                    {
                        return "Сегодня в " + date.Value.ToShortTimeString();
                    }
                    else if (DateDiff < 2)
                    {
                        return "Вчера в " + date.Value.ToShortTimeString();
                    }
                    else if (DateDiff < 7)
                    {
                        return date.Value.Date.ToShortDateString();
                    }
                    else if (DateDiff < 30)
                    {
                        return date.Value.Date.ToShortDateString();
                    }
                    else
                    {
                        return date.Value.Date.ToShortDateString();
                    }
                }
                else
                {
                    return "Не известно";
                }
        }
    }
}
