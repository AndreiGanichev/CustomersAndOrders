   M S B A M L   `   `   `  ����M  ICustomersAndOrders, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:CustomersAndOrdersCustomersAndOrders  5   	   A%clr-namespace:CustomersAndOrders.ViewCustomersAndOrders.View  5   	   K*clr-namespace:CustomersAndOrders.ViewModelCustomersAndOrders.ViewModel  5   	   #    CustomersAndOrders.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:CustomersAndOrders     5   	   ;	localView%clr-namespace:CustomersAndOrders.View     5   	   ElocalViewModel*clr-namespace:CustomersAndOrders.ViewModel     5   	     � Title$  CustomersAndOrders��5
   	   $	��350��6$   $	��610��61   $	��610��6=   $	��610��6L    � DataContext 5      6  0CustomersAndOrders.ViewModel.MainWindowViewModel  5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��300 �6   6-   �� 5      $	��300 �6   6-   .+�5   
   �� $��Center=�6   $��16(�6-   .��6;   Клиенты6D   �� 5   
   $��Center=�6   $��16(�6-   $��1��6;   .��6K   Заказы6S   &   System.Windows.Controls.DataGrid  5   
   $��10,30,10,50q�6      AutoGenerateColumns .  ��6(   �� *	Customers+ �� SelectedItem 6J   �� *SelectedCustomer+ �� UpdateSourceTrigger$ PropertyChanged=� � 	Resources 5      *  $System.Windows.Controls.DataGridCell �� 
TargetType �� Triggers	 �� Binding
 �� Path �� Value �� Property �� Value��%�   '        �� 5      "  6    5      �� 5      	 6&   �� $
 IsVIP� True6E   .a�5      ��  � ��
 �z���6;   5      5      5          Columns 5"      0  *System.Windows.Controls.DataGridTextColumn  5#      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� *Id+6U     5$      $ Имя��6%   $	 100 62    6>   �� *	FirstName+6^     5%      $ 
Адрес��6%   $
 auto 64    6A   �� *	Address+6_   5'      �� 5)   
   $��60��6   $��25��6   $��10,20,220,10q�6(   $
��Left=�6^   $��Bottom=�6y   ��6>   �� *AddCustomer+.��6�   Добавить6�   �� 5*   
   $��60��6   $��25��6   $��80,20,100,10q�6(   $
��Left=�6a   $��Bottom=�6|   ��6>   �� *DeleteCustomer+.��6�   Удалить6�   �� 5+   
   $	��100��6   $��25��6   $��150,20,10,10q�6)   $��Right=�6_   $��Bottom=�6{   ��6?   �� *CreateOrder+.��6�   Создать заказ6�     5-   
   $��10,30,10,50q�6    .  $��1��6c   ��6(   �� *Orders+ 6s   �� *SelectedOrder+ 5.        5/      $ Заказ №��6%   $ 60 66    6A   �� *Number+6^     50      $ Описание��6%   $
 auto 67    6D   �� *Description+6f   52      �� 53   
   $	��100��6   $��25��6   $��10,20,10,10q�6)   $��1��6\   $��Right=�6l   $��Bottom=�6�   ��6>   �� *	EditOrder+.��6�   Редактировать6�   �� 54   
   $��60��6   $��25��6   $��10,20,120,10q�6(   $��1��6^   $��Right=�6n   $��Bottom=�6�   ��6>   �� *DeleteOrder+.��6�   Удалить6�   55      56      