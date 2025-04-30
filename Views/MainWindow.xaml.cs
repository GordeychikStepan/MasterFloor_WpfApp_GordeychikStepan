using System.Windows;
using System.Windows.Input;
using MasterFloor_WpfApp.Models;
using MasterFloor_WpfApp.Views;
using Microsoft.EntityFrameworkCore;

namespace MasterFloor_WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MasterFloorDbContext _context = new MasterFloorDbContext();

        public MainWindow()
        {
            InitializeComponent();
            LoadPartners();
        }

        // Загружает список партнеров из базы данных, создает модель отображения
        private void LoadPartners()
        {
            var partners = _context.Partners
                .Include(p => p.PartnerType)
                .Include(p => p.PartnerProducts)
                    .ThenInclude(pp => pp.Product)
                .ToList();

            var partnerViewModels = partners.Select(p => new
            {
                PartnerTypeAndName = $"{p.PartnerType?.TypeName} | {p.PartnerName}",
                p.Ceo,
                PartnerPhone = $"+7 {p.PartnerPhone}",
                Rating = $"Рейтинг: {p.Rate}",
                Discount = $"{CalculateDiscount(p)}%"
            }).ToList();

            PartnersListView.ItemsSource = partnerViewModels;
        }

        // Рассчитывает индивидуальную скидку партнера на основании общей суммы продаж.
        private int CalculateDiscount(Partner partner)
        {
            // Суммируем количество товаров
            var totalSales = partner.PartnerProducts
                .Where(pp => pp.Product != null)
                .Sum(pp => pp.ProductCount ?? 0);

            // Логика расчета скидки по порогам
            if (totalSales > 300000) return 15;
            if (totalSales >= 50000) return 10;
            if (totalSales >= 10000) return 5;
            return 0;
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }

        private void PartnersListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            /*if (PartnersListView.SelectedItem is dynamic item)
            {
                int id = item.PartnerId;
                var partner = _context.Partners.Find(id);
                var wnd = new AddEditPartnerWindow(partner);
                if (wnd.ShowDialog() == true)
                {
                    LoadPartners();
                }
            }*/
        }
    }
}