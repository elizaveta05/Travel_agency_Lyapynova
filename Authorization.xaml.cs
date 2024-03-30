using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
        }
        private void btn_authorization_Click(object sender, RoutedEventArgs e)
        {
            string login = tb_login.Text.Trim();
            string password = tb_password.Password.Trim();
            var user = TravelAgentsPr21101LyapynovaContext.GetContext().Users
                .FirstOrDefault(u => u.Login == login && u.Password == password);
            if (user == null)
            {

                MessageBox.Show("Пользователь не найдег");
                Clear();
            }
            else
            {
                Role_definition(user);
            }

        }
        public void Role_definition(User user)
        {
            try
            {
                var employee = TravelAgentsPr21101LyapynovaContext.GetContext().Employees.FirstOrDefault(e => e.UserId == user.UserId);
                var supplier = TravelAgentsPr21101LyapynovaContext.GetContext().Suppliers.FirstOrDefault(s => s.UserId == user.UserId);

                if (employee != null)
                {
                    switch (employee.PositionId)
                    {
                        case 2:
                            NavigationService.Navigate(new MenegerPage());
                            break;
                        case 3:
                            NavigationService.Navigate(new Director(employee));
                            break;
                        default:
                            MessageBox.Show("В данный момент для вашей должности недоступно использование приложения");
                            break;
                    }
                }
                else if (supplier != null)
                {
                    NavigationService.Navigate(new SupplierPage(supplier));
                }
                else
                {
                    MessageBox.Show("Сотрудник или поставщик не найден");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
                Clear();
            }
        }
        private void Clear()
        {
            tb_login.Clear();
            tb_password.Clear();
        }
    }
}
