using System;
using System.Windows;
using CustomersAndOrders.Model;
using System.Windows.Input;
using DataStore;

namespace CustomersAndOrders.ViewModel
{
    class AddNewCustomerViewModel:DependencyObject
    {
        public AddNewCustomerViewModel(Customer customer)
        {
            NewCustomer=customer;
            AddNewCustomer = new Command(addCustomer);
        }

        #region Dependency Property
        public string NewCustomerFirstName
        {
            get { return (string)GetValue(NewCustomerFirstNameProperty); }
            set { SetValue(NewCustomerFirstNameProperty, value); }
        }
        public static readonly DependencyProperty NewCustomerFirstNameProperty =
            DependencyProperty.Register("NewCustomerFirstName", typeof(string), typeof(AddNewCustomerViewModel), new PropertyMetadata(""));

        public string NewCustomerAddress
        {
            get { return (string)GetValue(NewCustomerAddressProperty); }
            set { SetValue(NewCustomerAddressProperty, value); }
        }
        public static readonly DependencyProperty NewCustomerAddressProperty =
            DependencyProperty.Register("NewCustomerAddress", typeof(string), typeof(AddNewCustomerViewModel), new PropertyMetadata(""));

        public bool NewCustomerIsVIP
        {
            get { return (bool)GetValue(NewCustomerIsVIPProperty); }
            set { SetValue(NewCustomerIsVIPProperty, value); }
        }
        public static readonly DependencyProperty NewCustomerIsVIPProperty =
            DependencyProperty.Register("NewCustomerIsVIP", typeof(bool), typeof(AddNewCustomerViewModel), new PropertyMetadata(false));

        public ICommand AddNewCustomer
        {
            get { return (ICommand)GetValue(AddNewCustomerProperty); }
            set { SetValue(AddNewCustomerProperty, value); }
        }
        public static readonly DependencyProperty AddNewCustomerProperty =
            DependencyProperty.Register("AddNewCustomer", typeof(ICommand), typeof(AddNewCustomerViewModel), new PropertyMetadata(null));
        #endregion

        private Customer NewCustomer { set; get;}
        
        //Событие завершения ввода данных
        public event Action<bool> OnDataFilled;

        private void addCustomer()
        {
            if (OnDataFilled != null)
            {
                if (string.IsNullOrEmpty(NewCustomerFirstName) || string.IsNullOrWhiteSpace(NewCustomerFirstName))
                {
                    MessageBox.Show("Введите имя клиента");
                    return;
                }
                if (string.IsNullOrEmpty(NewCustomerAddress) || string.IsNullOrWhiteSpace(NewCustomerAddress))
                {
                    MessageBox.Show("Введите адрес клиента");
                    return;
                }
                NewCustomer.FirstName = NewCustomerFirstName;
                NewCustomer.Address = NewCustomerAddress;
                NewCustomer.IsVIP = NewCustomerIsVIP;
                OnDataFilled(true);
            }
        }

    }
}
