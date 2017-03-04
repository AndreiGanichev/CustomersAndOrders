using System;
using System.Collections.ObjectModel;
using DataStore;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CustomersAndOrders.Model;
using CustomersAndOrders.View;
using System.ComponentModel;
using System.Windows.Data;

namespace CustomersAndOrders.ViewModel
{
    class MainWindowViewModel:DependencyObject
    {
        public MainWindowViewModel()
        {
            Database.SetInitializer<CustomerDbContext>(new CustomerDbInitializer());
            db = new CustomerDbContext("CustomerStoreConnection");
            Customers = new ObservableCollection<Customer>(db.Customers);
            var orderList = db.Orders.ToArray();
            Orders = CollectionViewSource.GetDefaultView(orderList);
            Orders.Filter = filterOrdersAccordingToSelectedCustomer;

            AddCustomer = new Command(addNewCustomer);
            DeleteCustomer = new Command(deleteCustomer);
            CreateOrder = new Command(createOrder);
            EditOrder = new Command(editOrder);
            DeleteOrder = new Command(deleteOrder);

            db.Dispose();
        }

        #region DependencyProperties
        public ObservableCollection<Customer> Customers
        {
            get { return (ObservableCollection<Customer>)GetValue(CustomersProperty); }
            set { SetValue(CustomersProperty, value); }
        }
        public static readonly DependencyProperty CustomersProperty =
            DependencyProperty.Register("Customers", typeof(ObservableCollection<Customer>), typeof(MainWindowViewModel), new PropertyMetadata(null));

        public ICollectionView Orders
        {
            get { return (ICollectionView)GetValue(OrdersProperty); }
            set { SetValue(OrdersProperty, value); }
        }
        public static readonly DependencyProperty OrdersProperty =
            DependencyProperty.Register("Orders", typeof(ICollectionView), typeof(MainWindowViewModel), new PropertyMetadata(null));

        public Customer SelectedCustomer
        {
            get { return (Customer)GetValue(SelectedCustomerProperty); }
            set { SetValue(SelectedCustomerProperty, value); }
        }
        public static readonly DependencyProperty SelectedCustomerProperty =
            DependencyProperty.Register("SelectedCustomer", typeof(Customer), typeof(MainWindowViewModel), new PropertyMetadata(null,OnSelectedCustomerChanged));

        public Order SelectedOrder
        {
            get { return (Order)GetValue(SelectedOrderProperty); }
            set { SetValue(SelectedOrderProperty, value); }
        }
        public static readonly DependencyProperty SelectedOrderProperty =
            DependencyProperty.Register("SelectedOrder", typeof(Order), typeof(MainWindowViewModel), new PropertyMetadata(null));
        
        #region Commands
        public ICommand AddCustomer
        {
            get { return (ICommand)GetValue(AddCustomerProperty); }
            set { SetValue(AddCustomerProperty, value); }
        }
        public static readonly DependencyProperty AddCustomerProperty =
            DependencyProperty.Register("AddCustomer", typeof(ICommand), typeof(MainWindowViewModel), new PropertyMetadata(null));

        public ICommand DeleteCustomer
        {
            get { return (ICommand)GetValue(DeleteCustomerProperty); }
            set { SetValue(DeleteCustomerProperty, value); }
        }
        public static readonly DependencyProperty DeleteCustomerProperty =
            DependencyProperty.Register("DeleteCustomer", typeof(ICommand), typeof(MainWindowViewModel), new PropertyMetadata(null));

        public ICommand CreateOrder
        {
            get { return (ICommand)GetValue(CreateOrderProperty); }
            set { SetValue(CreateOrderProperty, value); }
        }
        public static readonly DependencyProperty CreateOrderProperty =
            DependencyProperty.Register("CreateOrder", typeof(ICommand), typeof(MainWindowViewModel), new PropertyMetadata(null));

        public ICommand EditOrder
        {
            get { return (ICommand)GetValue(EditOrderProperty); }
            set { SetValue(EditOrderProperty, value); }
        }
        public static readonly DependencyProperty EditOrderProperty =
            DependencyProperty.Register("EditOrder", typeof(ICommand), typeof(MainWindowViewModel), new PropertyMetadata(null));

        public ICommand DeleteOrder
        {
            get { return (ICommand)GetValue(DeleteOrderProperty); }
            set { SetValue(DeleteOrderProperty, value); }
        }
        public static readonly DependencyProperty DeleteOrderProperty =
            DependencyProperty.Register("DeleteOrder", typeof(ICommand), typeof(MainWindowViewModel), new PropertyMetadata(null));
        #endregion
        #endregion

        private enum UpdatingData { Customer, Order };
        CustomerDbContext db;

        private void addNewCustomer()
        {
            db = new CustomerDbContext("CustomerStoreConnection");
            Customer newCustomer = new Customer();
            AddCustomerWindow addCustomerWindow = new AddCustomerWindow();
            AddNewCustomerViewModel addCustomerViewModel = new AddNewCustomerViewModel(newCustomer);
            addCustomerWindow.DataContext = addCustomerViewModel;

            addCustomerViewModel.OnDataFilled += (closeResult) =>
            {
                if (closeResult == true)
                {
                    try
                    {
                        db.Customers.Add(newCustomer);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
                addCustomerWindow.Close();
                updateDataGrid(UpdatingData.Customer);       
            };
            addCustomerWindow.ShowDialog();
        }
        private void deleteCustomer()
        {
            db = new CustomerDbContext("CustomerStoreConnection");
            if (SelectedCustomer != null)
            {
                if (SelectedCustomer.Orders != null)
                {
                    db.Orders.RemoveRange(db.Orders.Where(o=>o.CustomerId==SelectedCustomer.Id));
                }
                Customer delitingCustomer = db.Customers.Where(c => c.Id == SelectedCustomer.Id).First();
                db.Customers.Remove(delitingCustomer);
                db.SaveChanges();
                updateDataGrid(UpdatingData.Customer);
                updateDataGrid(UpdatingData.Order);
            }
            else
            {
                MessageBox.Show("Выберите клиента");
            }
        }
        private void createOrder()
        {
            db = new CustomerDbContext("CustomerStoreConnection");
            if(SelectedCustomer==null)
            {
                MessageBox.Show("Выберите клиента, для которого хотите добавить заказ");
                return;
            }
            Order newOrder = new Order() { CustomerId=SelectedCustomer.Id};
            OrderFormWindow orderFormWindow = new OrderFormWindow();
            OrderFormViewModel orderFormViewModel = new OrderFormViewModel(newOrder,"Новый заказ");
            orderFormWindow.DataContext = orderFormViewModel;

            orderFormViewModel.OnDataFilled += (closeResult) =>
            {
                if (closeResult == true)
                {
                    try
                    {
                        db.Orders.Add(newOrder);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
                
                updateDataGrid(UpdatingData.Order);
                orderFormWindow.Close();
            };
            orderFormWindow.ShowDialog();
        }
        private void editOrder()
        {
            db = new CustomerDbContext("CustomerStoreConnection");
            if (SelectedOrder == null)
            {
                MessageBox.Show("Выберите заказ для редактирвоания");
                return;
            }
            Order editingOrder = SelectedOrder;
            OrderFormWindow orderFormWindow = new OrderFormWindow();
            OrderFormViewModel orderFormViewModel = new OrderFormViewModel(editingOrder, "Редактирование");
            orderFormWindow.DataContext = orderFormViewModel;

            orderFormViewModel.OnDataFilled += (closeResult) =>
            {
                if (closeResult == true)
                {
                    try
                    {
                        db.Entry<Order>(editingOrder).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
                orderFormWindow.Close();
                updateDataGrid(UpdatingData.Order);
            };
            orderFormWindow.ShowDialog();
        }
        private void deleteOrder()
        {
            db = new CustomerDbContext("CustomerStoreConnection");
            if (SelectedOrder != null)
            {
                Order delitingOrder = db.Orders.Where(o => o.Number == SelectedOrder.Number).First();
                db.Orders.Remove(delitingOrder);
                db.SaveChanges();
                updateDataGrid(UpdatingData.Order);
            }
            else
            {
                MessageBox.Show("Выберите заказ для удаления");
                return;
            }
        }

        private async void updateDataGrid(UpdatingData data)
        {
            try
            {
                switch (data)
                {
                    case UpdatingData.Customer:
                        Customers = new ObservableCollection<Customer>(await Task.Run(() => db.Customers.ToList()));
                        break;
                    case UpdatingData.Order:
                        var orderList = await Task.Run(() => db.Orders.ToList());
                        Orders = CollectionViewSource.GetDefaultView(orderList);
                        break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private static void OnSelectedCustomerChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var current = o as MainWindowViewModel;
            if (current != null)
            {
                current.Orders.Filter = null;
                current.Orders.Filter = current.filterOrdersAccordingToSelectedCustomer;
            }
        }
        private bool filterOrdersAccordingToSelectedCustomer(object obj)
        {
            Order currentOrder = obj as Order;
            if (SelectedCustomer == null||currentOrder.CustomerId == SelectedCustomer.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
