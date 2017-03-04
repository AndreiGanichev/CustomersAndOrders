using System;
using System.Windows;
using System.Windows.Input;
using DataStore;
using CustomersAndOrders.Model;

namespace CustomersAndOrders.ViewModel
{
    class OrderFormViewModel:DependencyObject
    {
        public OrderFormViewModel(Order newOrder, string title)
        {
            Order = newOrder;
            Title = title;
            NewOrderDescription = Order.Description;
            SaveOrderData = new Command(saveOrderData);
        }
        #region Dependency Property
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(OrderFormViewModel), new PropertyMetadata(""));

        public string NewOrderDescription
        {
            get { return (string)GetValue(NewOrderDescriptionProperty); }
            set { SetValue(NewOrderDescriptionProperty, value); }
        }
        public static readonly DependencyProperty NewOrderDescriptionProperty =
            DependencyProperty.Register("NewOrderDescription", typeof(string), typeof(OrderFormViewModel), new PropertyMetadata(""));

        public ICommand SaveOrderData
        {
            get { return (ICommand)GetValue(SaveOrderDataProperty); }
            set { SetValue(SaveOrderDataProperty, value); }
        }
        public static readonly DependencyProperty SaveOrderDataProperty =
            DependencyProperty.Register("SaveOrderData", typeof(ICommand), typeof(OrderFormViewModel), new PropertyMetadata(null));
        #endregion

        Order Order;

        //Событие завершения ввода данных
        public event Action<bool> OnDataFilled;

        private void saveOrderData()
        {
            if (OnDataFilled != null)
            {
                if (string.IsNullOrEmpty(NewOrderDescription) || string.IsNullOrWhiteSpace(NewOrderDescription))
                {
                    MessageBox.Show("Введите описание заказа");
                    return;
                }
                Order.Description = NewOrderDescription;
                OnDataFilled(true);
            }
        }
    }
}
