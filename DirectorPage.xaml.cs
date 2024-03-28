using Microsoft.EntityFrameworkCore;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Travel_agency_Lyapynova.Models;

namespace Travel_agency_Lyapynova
{
    /// <summary>
    /// Логика взаимодействия для Director.xaml
    /// </summary>
    public partial class Director : Page
    {
        List<Employee> employees = new List<Employee>();

        public Director(Employee employee)
        {
            InitializeComponent();
            Welcome(employee);
            LoadEmployee();
        }

        private void Welcome(Employee employee)
        {
            tb_welcome.Text = "Добро пожаловать " + employee.Surname + " " + employee.Name + " " + employee.Patronymic + "!";
        }

        private void LoadEmployee()
        {
            using (var context = new TravelAgentsPr21101LyapynovaContext())
            {
                employees = context.Employees.Include(e => e.Position).ToList();
                employeeListView.ItemsSource = employees;
            }
        }

        private void cb_filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = cb_filter.SelectedItem as ComboBoxItem;

            if (selectedItem != null)
            {
                string selectedOption = selectedItem.Content.ToString();

                if (selectedOption == "По алфавиту(А-Я)")
                {
                    employees = employees.OrderBy(emp => emp.Surname).ToList();
                }
                else if (selectedOption == "По алфавиту(Я-А)")
                {
                    employees = employees.OrderByDescending(emp => emp.Surname).ToList();
                }

                employeeListView.ItemsSource = employees;
            }
        }

        private void btn_poisk_Click(object sender, RoutedEventArgs e)
        {
            string search = tb_poisk.Text.Trim().ToLower();
            List<Employee> searchResults = employees.Where(emp => emp.Name.ToLower().Contains(search) ||
                                                               emp.Surname.ToLower().Contains(search) ||
                                                               emp.Patronymic.ToLower().Contains(search)).ToList();

            employeeListView.ItemsSource = searchResults;
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEmplyee());
        }

        private void employeeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employee selectedEmployee = employeeListView.SelectedItem as Employee;

            if (selectedEmployee != null)
            {
                NavigationService.Navigate(new ProfileEmployee(selectedEmployee.EmployeeId));
                employeeListView.SelectedItem = null;
            }

        }
    }
}
