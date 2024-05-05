using Data;
using Microsoft.Xaml.Behaviors.Layout;
using System;
using System.Windows;
using System.Xml.Linq;

namespace ADO_NET_First
{
    public partial class AddItemDialog : Window
    {
        public AddItemDialog()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                if (string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtType.Text) ||
                    string.IsNullOrWhiteSpace(txtSupplier.Text) ||
                    string.IsNullOrWhiteSpace(txtQuantity.Text) ||
                    string.IsNullOrWhiteSpace(txtCost.Text) ||
                    dpSupplyDate.SelectedDate == null)
                {
                    MessageBox.Show("Please fill in all the fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
                WarehouseItem newItem = new WarehouseItem
                {
                    Name = txtName.Text,
                    Type = txtType.Text,
                    Supplier = txtSupplier.Text,
                    Quantity = int.Parse(txtQuantity.Text),
                    Cost = decimal.Parse(txtCost.Text),
                    SupplyDate = dpSupplyDate.SelectedDate.Value
                };

                
                DialogResult = true;
                Tag = newItem;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            
            DialogResult = false;
        }
    }
}
