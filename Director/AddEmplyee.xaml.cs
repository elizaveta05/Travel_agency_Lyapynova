using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Travel_agency_Lyapynova.Models;

namespace Travel_agency_Lyapynova
{
    /// <summary>
    /// Логика взаимодействия для AddEmplyee.xaml
    /// </summary>
    public partial class AddEmplyee : Page
    {
        public AddEmplyee()
        {
            InitializeComponent();
            cb_position.ItemsSource = TravelAgentsPr21101LyapynovaContext.GetContext().Positions.Select(p => p.NamePositions).ToList();
        }

        private void btn_save_employee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee employee = new Employee();
                employee.Surname = tb_surname.Text;
                employee.Name = tb_name.Text;
                employee.PhoneNumber = tb_number_phone.Text;

                employee.Patronymic = tb_patronymic.Text;
                employee.PositionId = TravelAgentsPr21101LyapynovaContext.GetContext().Positions
                .OrderBy(p => p.PositionId)
                .Select(p => p.PositionId)
                .ToList()[cb_position.SelectedIndex];

                User user = new User();
                user.Login = tb_login.Text;
                user.Password = tb_password.Text;

                var validationResults1 = employee.Validate(new ValidationContext(employee));
                if (validationResults1.Any())
                {
                    string errorMessage = string.Join("\n", validationResults1.Select(r => r.ErrorMessage));
                    MessageBox.Show("Ошибка при сохранении данных сотрудника: " + errorMessage);
                    return;
                }

                var validationResults2 = user.Validate(new ValidationContext(user));
                if (validationResults2.Any())
                {
                    string errorMessage = string.Join("\n", validationResults2.Select(r => r.ErrorMessage));
                    MessageBox.Show("Ошибка при сохранении данных пользователя: " + errorMessage);
                    return;
                }

                TravelAgentsPr21101LyapynovaContext.GetContext().Users.Add(user);
                TravelAgentsPr21101LyapynovaContext.GetContext().SaveChanges();

                int userId = user.UserId;
                employee.UserId = userId;

                TravelAgentsPr21101LyapynovaContext.GetContext().Employees.Add(employee);
                TravelAgentsPr21101LyapynovaContext.GetContext().SaveChanges();

                MessageBox.Show("Сотрудник успешно добавлен.", "Успех", MessageBoxButton.OK);
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK);
            }
        }

        private void btn_save_image_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png; *.jpg; *.jpeg; *.bmp)|*.png; *.jpg; *.jpeg; *.bmp|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                image.Source = new BitmapImage(new Uri(imagePath));
            }
        }
    }
}
