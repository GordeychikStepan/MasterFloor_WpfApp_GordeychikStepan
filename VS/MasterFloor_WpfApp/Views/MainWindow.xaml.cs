using System.Windows;
using System.Windows.Input;
using MasterFloor_WpfApp.Models;
using MasterFloor_WpfApp.Services;
using MasterFloor_WpfApp.ViewModel;
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
                .Include(p => p.PartnerProducts).ThenInclude(pp => pp.Product)
                .ToList();

            var viewModels = new List<PartnerViewModel>();

            foreach (var p in partners)
            {
                string typeName = p.PartnerType?.TypeName ?? "(не указан)";
                int totalSales = 0;
                foreach (var pp in p.PartnerProducts)
                {
                    if (pp.Product != null && pp.ProductCount.HasValue)
                    {
                        totalSales += pp.ProductCount.Value;
                    }
                }

                int discount = Service.GetPercent(totalSales);

                viewModels.Add(new PartnerViewModel
                {
                    PartnerId = p.PartnerId,
                    PartnerTypeAndName = typeName + " | " + p.PartnerName,
                    Ceo = p.Ceo,
                    PartnerPhone = "+7 " + p.PartnerPhone,
                    Rating = "Рейтинг: " + (p.Rate?.ToString() ?? "0"),
                    Discount = discount + "%"
                });
            }

            PartnersListView.ItemsSource = viewModels;
        }


        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }

        private void PartnersListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (PartnersListView.SelectedItem is PartnerViewModel item)
            {
                var partner = _context.Partners.Find(item.PartnerId);
                var wnd = new AddEditPartnerWindow(_context, partner);
                if (wnd.ShowDialog() == true)
                    LoadPartners();
            }
        }

        private void AddPartner_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new AddEditPartnerWindow(_context);
            if (wnd.ShowDialog() == true)
                LoadPartners();
        }
    }
}