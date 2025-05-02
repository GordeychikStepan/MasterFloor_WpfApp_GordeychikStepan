using System;

namespace MasterFloor_WpfApp.Services
{
    public class Service
    {
        public static int GetPercent(int totalSales)
        {
            if (totalSales > 300000)
                return 15;
            else if (totalSales >= 50000)
                return 10;
            else if (totalSales >= 10000)
                return 5;
            else
                return 0;
        }
    }
}
