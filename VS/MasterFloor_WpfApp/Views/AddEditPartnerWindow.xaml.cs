using System;
using System.Windows;
using MasterFloor_WpfApp.Models;
using Microsoft.IdentityModel.Tokens;

namespace MasterFloor_WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEditPartnerWindow.xaml
    /// </summary>
    public partial class AddEditPartnerWindow : Window
    {
        private readonly MasterFloorDbContext _context;
        private Partner _partner;

        public AddEditPartnerWindow(MasterFloorDbContext context, Partner partner = null)
        {
            InitializeComponent();

            _context = context;

            LoadTypes();
            _partner = partner;

            if (_partner != null)
            {
                LoadPartnerData();
                this.Title = "Мастер пол - Редактирование данных партнера";
                WindowTitle_TextBlock.Text = "Редактирование данных партнера";
            }
            else
            {
                this.Title = "Мастер пол - Добавление нового партнера";
                WindowTitle_TextBlock.Text = "Добавление нового партнера";
            }
                
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
            if (NameText.Text.IsNullOrEmpty() || RatingText.Text.IsNullOrEmpty() || 
                AddressText.Text.IsNullOrEmpty() || CeoText.Text.IsNullOrEmpty() || 
                PhoneText.Text.IsNullOrEmpty() || EmailText.Text.IsNullOrEmpty() ||
                TypeCombo.SelectedItem == null)
            {
                MessageBox.Show("Заполните все данные для сохранения.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(RatingText.Text, out int rate) || rate < 0)
            {
                MessageBox.Show("Рейтинг должен быть неотрицательным целым.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_partner == null)
            {
                _partner = new Partner();
                _context.Partners.Add(_partner);
            }

            _partner.PartnerName = NameText.Text;
            _partner.PartnerTypeId = (int?)TypeCombo.SelectedValue;
            _partner.Rate = rate;
            _partner.PartnerAddress = AddressText.Text;
            _partner.Ceo = CeoText.Text;
            _partner.PartnerPhone = PhoneText.Text;
            _partner.PartnerEmail = EmailText.Text;

            _context.SaveChanges();
            DialogResult = true;
        }
    }
}
