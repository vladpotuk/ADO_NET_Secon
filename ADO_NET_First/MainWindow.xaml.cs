using Data;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace ADO_NET_First
{
    public partial class MainWindow : Window
    {
        private DBManager dbManager;
        private ObservableCollection<WarehouseItem> items;

        public MainWindow()
        {
            InitializeComponent();
            dbManager = new DBManager("Data Source=10.0.0.40,1433;User ID=student;Password=1111;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            items = new ObservableCollection<WarehouseItem>(dbManager.SelectAllItems());
            dgItems.ItemsSource = items;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }

        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
         
            AddItemDialog dialog = new AddItemDialog();
            if (dialog.ShowDialog() == true)
            {
                WarehouseItem newItem = dialog.GetNewItem();
                dbManager.InsertItem(newItem);
                RefreshDataGrid();
            }
        }

        private void UpdateItem_Click(object sender, RoutedEventArgs e)
        {
            if (dgItems.SelectedItem != null)
            {
                WarehouseItem selectedItem = dgItems.SelectedItem as WarehouseItem;
              
                UpdateItemDialog dialog = new UpdateItemDialog(selectedItem);
                if (dialog.ShowDialog() == true)
                {
                    WarehouseItem updatedItem = dialog.GetUpdatedItem();
                    dbManager.UpdateItem(updatedItem);
                    RefreshDataGrid();
                }
            }
            else
            {
                MessageBox.Show("Please select an item to update.", "Update Item", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (dgItems.SelectedItem != null)
            {
                WarehouseItem selectedItem = dgItems.SelectedItem as WarehouseItem;
                if (MessageBox.Show($"Are you sure you want to delete item '{selectedItem.Name}'?", "Delete Item", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    dbManager.DeleteItem(selectedItem.ID);
                    RefreshDataGrid();
                }
            }
            else
            {
                MessageBox.Show("Please select an item to delete.", "Delete Item", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
