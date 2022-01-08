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
using AutoLotModelFINAL1;
using System.Data.Entity;
using System.Data;

namespace SALONFINAL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing,
    }

    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        AutoLotEntitiesModelFinal ctx = new AutoLotEntitiesModelFinal();
        CollectionViewSource customerVSource =new CollectionViewSource();
        CollectionViewSource servicesVSource = new CollectionViewSource();
        CollectionViewSource employeesVSource = new CollectionViewSource();
        CollectionViewSource employeeshairstyleVSource = new CollectionViewSource();

        public int indexTB = 0;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            indexTB = TabControl.TabIndex;
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            System.Windows.Data.CollectionViewSource customersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customersViewSource")));
            customerVSource.Source = ctx.Customers.Local;
            ctx.Customers.Load();
            // Load data by setting the CollectionViewSource.Source property:
            // customersViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource servicesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("servicesViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // servicesViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource employeesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("employeesViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // employeesViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource hairstyleViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("hairstyleViewSource")));
            employeeshairstyleVSource.Source = ctx.Hairstyle.Local;
            ctx.Hairstyle.Load();
            ctx.services.Load();
            cmbEmployees.ItemsSource = ctx.Employees.Local;
            cmbEmployees.DisplayMemberPath = "employees1";
            cmbEmployees.SelectedValuePath = "id";

            cmbServices.ItemsSource = ctx.services.Local;
            cmbServices.DisplayMemberPath = "Description";
            cmbServices.SelectedValuePath = "id service";
              

            // Load data by setting the CollectionViewSource.Source property:
            // hairstyleViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource appoitmentViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("appoitmentViewSource")));

            // Load data by setting the CollectionViewSource.Source property:
            // appoitmentViewSource.Source = [generic data source]

        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            switch (TabControl.SelectedIndex)
            {
                case 0:
                    SaveCustomers();
                    break;
                case 1:
                    SaveServices();
                    break;
                case 2:
                    SaveEmployees();
                    break;
            }
            
        }
        private void btnEdit0_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            switch (TabControl.SelectedIndex)
            {
                case 0:
                    SaveCustomers();
                    break;
                case 1:
                    SaveServices();
                    break;
                case 2:
                    SaveEmployees();
                    break;
            }
        }
        private void btnDelete0_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
            switch (TabControl.SelectedIndex)
            {
                case 0:
                    SaveCustomers();
                    break;
                case 1:
                    SaveServices();
                    break;
                case 2:
                    SaveEmployees();
                    break;
            }
        }
        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            
            customerVSource.View.MoveCurrentToPrevious();
            try
            {
                if (indexTB > 0 && indexTB < 5)
                    TabControl.TabIndex = indexTB;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            customerVSource.View.MoveCurrentToNext();
            try
            {
                if (indexTB > 0 && indexTB < 5)
                    TabControl.TabIndex = indexTB;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SaveCustomers()
        {
            Customers customer = null;
            if (action == ActionState.New)
            {
                try
                {
                    customer = new Customers()
                    {
                        First_Name = first_NameTextBox.Text.Trim(),
                    };
                    ctx.Customers.Add(customer);
                    customerVSource.View.Refresh();
                    ctx.SaveChanges();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                if (action == ActionState.Edit)
            {
                try
                {
                    customer = (Customers)customersDataGrid.SelectedItem;
                    customer.First_Name = first_NameTextBox.Text.Trim();
                    customer.Last_Name = last_NameTextBox.Text.Trim();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    customer = (Customers)customersDataGrid.SelectedItem;
                    ctx.Customers.Remove(customer);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                customerVSource.View.Refresh();
            }
        }
        private void btnPrev1_Click(object sender, RoutedEventArgs e)
        {
            servicesVSource.View.MoveCurrentToPrevious();
            try
            {
                if (indexTB > 0 && indexTB < 5)
                    TabControl.TabIndex = indexTB;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnNext1_Click(object sender, RoutedEventArgs e)
        {
            servicesVSource.View.MoveCurrentToNext();
            try
            {
                if (indexTB > 0 && indexTB < 5)
                    TabControl.TabIndex = indexTB;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SaveServices()
        {
            services services = null;
            if (action == ActionState.New)
            {
                try
                {
                    services = new services()
                    {
                        Description = descriptionTextBox.Text.Trim(),
                    };
                    ctx.services.Add(services);
                    servicesVSource.View.Refresh();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Edit)
            {
                try
                {
                    services = (services)servicesDataGrid.SelectedItem;
                    services.Description = descriptionTextBox.Text.Trim();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    services = (services)servicesDataGrid.SelectedItem;
                    ctx.services.Remove(services);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                servicesVSource.View.Refresh();
            }
        }
        private void btnPrev2_Click(object sender, RoutedEventArgs e)
        {
            employeesVSource.View.MoveCurrentToPrevious();
            try
            {
                if (indexTB > 0 && indexTB < 5)
                    TabControl.TabIndex = indexTB;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNext2_Click(object sender, RoutedEventArgs e)
        {
            employeesVSource.View.MoveCurrentToNext();
            try
            {
                if (indexTB > 0 && indexTB < 5)
                    TabControl.TabIndex = indexTB;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void SaveEmployees()
        {
            Employees employees = null;
            if (action == ActionState.New)
            {
                try
                {
                    employees = new Employees()
                    {
                        description = descriptionTextBox1.Text.Trim(),
                        employees1 = employees1TextBox.Text.Trim(),
                    };
                    ctx.Employees.Add(employees);
                    employeesVSource.View.Refresh();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                    if (action == ActionState.Edit)
            {
                try
                {
                    employees = (Employees)employeesDataGrid.SelectedItem;
                    employees.description = descriptionTextBox1.Text.Trim();
                    employees.employees1 = employees1TextBox.Text.Trim();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    employees = (Employees)employeesDataGrid.SelectedItem;
                    ctx.Employees.Remove(employees);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                employeesVSource.View.Refresh();
            }
        }
        private void gbOperations_Click(object sender, RoutedEventArgs e)
        {
            Button SelectedButton = (Button)e.OriginalSource;
            Panel panel = (Panel)SelectedButton.Parent;
            foreach (Button B in panel.Children.OfType<Button>())
            {
                if (B != SelectedButton)
                    B.IsEnabled = false;
            }
            gbActions.IsEnabled = true;
        }
        private void ReInitialize()
        {

            Panel panel = gbOperations.Content as Panel;
            foreach (Button B in panel.Children.OfType<Button>())
            {
                B.IsEnabled = true;
            }
            gbActions.IsEnabled = false;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReInitialize();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = TabControl.SelectedItem as TabItem;

            switch (ti.Header)
            {
                case "Customers":
                    SaveCustomers();
                    break;
                case "Services":
                    SaveServices();
                    break;
                case "Employees":
                    SaveEmployees();
                    break;
               //case "Hairstyle":
                    //SaveHairstyle();
                   // break;
              //  case "Appoitment":
                   // SaveAppoitment();
                   // break;
            }
            ReInitialize();
        }

    }
}
