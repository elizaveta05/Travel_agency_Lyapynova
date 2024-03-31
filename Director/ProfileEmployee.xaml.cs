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
using System.ComponentModel.DataAnnotations;
using Travel_agency_Lyapynova.Models;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;

namespace Travel_agency_Lyapynova
{
    /// <summary>
    /// Логика взаимодействия для ProfileEmployee.xaml
    /// </summary>
    public partial class ProfileEmployee : Page
    {
        public Employee employee;
        public ProfileEmployee(int EmployeeId)
        {
            InitializeComponent();
            employee = TravelAgentsPr21101LyapynovaContext.GetContext().Employees.FirstOrDefault(e => e.EmployeeId == EmployeeId);
            LoadEmployee(employee);
        }
        public void LoadEmployee(Employee employee)
        {
            tb_surname.Text = employee.Surname;
            tb_name.Text = employee.Name;
            tb_patronymic.Text = employee.Patronymic;
            tb_number_phone.Text = employee.PhoneNumber;
            cb_position.ItemsSource = TravelAgentsPr21101LyapynovaContext.GetContext().Positions.Select(p => p.NamePositions).ToList();
            cb_position.SelectedIndex = employee.PositionId-1;

            var user = TravelAgentsPr21101LyapynovaContext.GetContext().Users.FirstOrDefault(u => u.UserId == employee.UserId);
            if (user != null)
            {
                tb_login.Text = user.Login;
                tb_password.Text = user.Password;
            }
            else
            {
                MessageBox.Show("Данные о пользователе не найдены", "Ошибка", MessageBoxButton.OK);
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

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этого сотрудника?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                TravelAgentsPr21101LyapynovaContext.GetContext().Employees.Remove(employee);
                TravelAgentsPr21101LyapynovaContext.GetContext().SaveChanges();

                MessageBox.Show("Сотрудник успешно удален.", "Успех", MessageBoxButton.OK);

            }
        }

        private void btn_save_employee_Click(object sender, RoutedEventArgs e)
        {
            employee.Surname = tb_surname.Text;
            employee.Name = tb_name.Text;
            employee.Patronymic = tb_patronymic.Text;
            employee.PhoneNumber = tb_number_phone.Text;
            employee.PositionId = TravelAgentsPr21101LyapynovaContext.GetContext().Positions
                .OrderBy(p => p.PositionId)
                .Select(p => p.PositionId)
                .ToList()[cb_position.SelectedIndex];

            var user = TravelAgentsPr21101LyapynovaContext.GetContext().Users.FirstOrDefault(u => u.UserId == employee.UserId);
            if (user != null)
            {
                user.Login = tb_login.Text; 
                user.Password = tb_password.Text; 
            }
            var validationResults = employee.Validate(new ValidationContext(employee));
            if (validationResults.Any())
            {
                string errorMessage = string.Join("\n", validationResults.Select(r => r.ErrorMessage));
                MessageBox.Show("Ошибка при сохранении данных: " + errorMessage);
                return;
            }
            var validationResults1 = user.Validate(new ValidationContext(user));
            if (validationResults1.Any())
            {
                string errorMessage = string.Join("\n", validationResults1.Select(r => r.ErrorMessage));
                MessageBox.Show("Ошибка при сохранении данных: " + errorMessage);
                return;
            }

            TravelAgentsPr21101LyapynovaContext.GetContext().SaveChanges();

            MessageBox.Show("Данные о сотруднике успешно сохранены.", "Успех", MessageBoxButton.OK);
        }

        private void cb_position_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}