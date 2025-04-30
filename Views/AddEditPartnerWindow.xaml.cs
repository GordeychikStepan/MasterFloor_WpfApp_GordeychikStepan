using System;
using System.Windows;
using MasterFloor_WpfApp.Models;

namespace MasterFloor_WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEditPartnerWindow.xaml
    /// </summary>
    public partial class AddEditPartnerWindow : Window
    {
        private readonly MasterFloorDbContext _context = new MasterFloorDbContext();
        private Partner _partner;

        public AddEditPartnerWindow(Partner partner = null)
        {
            InitializeComponent();

            LoadTypes();
            _partner = partner;
            if (_partner != null)
                LoadPartnerData();
        }

        private void LoadTypes()
        {
            var types = _context.PartnerTypes.ToList();
            TypeCombo.ItemsSource = types;
            TypeCombo.DisplayMemberPath = "TypeName";
            TypeCombo.SelectedValuePath = "PartnerTypeId";
        }

        private void LoadPartnerData()
        {
            NameText.Text = _partner.PartnerName;
            TypeCombo.SelectedValue = _partner.PartnerTypeId;
            RatingText.Text = _partner.Rate?.ToString();
            AddressText.Text = _partner.PartnerAddress;
            CeoText.Text = _partner.Ceo;
            PhoneText.Text = _partner.PartnerPhone;
            EmailText.Text = _partner.PartnerEmail;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
