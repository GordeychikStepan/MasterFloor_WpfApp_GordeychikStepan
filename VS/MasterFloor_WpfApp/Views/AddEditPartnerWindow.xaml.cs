using System;
using System.Text.RegularExpressions;
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
            // 1) Проверяем обязательные поля
            if (NameText.Text.IsNullOrEmpty() || 
                RatingText.Text.IsNullOrEmpty() || 
                AddressText.Text.IsNullOrEmpty() || 
                CeoText.Text.IsNullOrEmpty() || 
                PhoneText.Text.IsNullOrEmpty() || 
                EmailText.Text.IsNullOrEmpty() ||
                TypeCombo.SelectedItem == null)
            {
                MessageBox.Show("Заполните все данные для сохранения.", "Внимание", 
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            // 2) Проверяем, что рейтинг — целое число и в разумных пределах (0–10)
            if (!int.TryParse(RatingText.Text, out int Rate) || Rate < 0 || Rate > 10)
            {
                MessageBox.Show("Рейтинг должен быть целым числом от 0 до 10.", "Внимание",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 3) Проверяем ФИО: три слова, каждое с заглавной буквы
            // Русские буквы: учитываем Ё/ё; если нужны латинские — добавьте A-Z a-z
            var FioPattern = @"^[А-ЯЁ][а-яё]+ [А-ЯЁ][а-яё]+ [А-ЯЁ][а-яё]+$";
            if (!Regex.IsMatch(CeoText.Text.Trim(), FioPattern))
            {
                MessageBox.Show(
                    "ФИО должно состоять из трёх слов, каждое — с заглавной буквы, например: «Иванов Иван Иванович».",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 4) Проверяем формат телефона
            var PhonePattern = @"^\d{3} \d{3} \d{2} \d{2}$";
            if (!Regex.IsMatch(PhoneText.Text, PhonePattern))
            {
                MessageBox.Show("Телефон должен быть в формате «XXX XXX XX XX».", "Внимание", 
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 5) Проверяем формат email
            var EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(EmailText.Text, EmailPattern))
            {
                MessageBox.Show("Некорректный формат электронной почты.", "Внимание",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Сохранение
            try
            {
                if (_partner == null)
                {
                    _partner = new Partner();
                    _context.Partners.Add(_partner);
                    MessageBox.Show("Новый партнер успешно добавлен.", "Информация",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Изменения успешно сохранены.", "Информация",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                }

                _partner.PartnerName = NameText.Text;
                _partner.PartnerTypeId = (int?)TypeCombo.SelectedValue;
                _partner.Rate = Rate;
                _partner.PartnerAddress = AddressText.Text;
                _partner.Ceo = CeoText.Text;
                _partner.PartnerPhone = PhoneText.Text;
                _partner.PartnerEmail = EmailText.Text;

                _context.SaveChanges();

                DialogResult = true;
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
