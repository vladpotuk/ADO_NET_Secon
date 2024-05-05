using System;
using System.Windows;

namespace ADO_NET_First
{
    public partial class UpdateItemDialog : Window
    {
        public WarehouseItem UpdatedItem { get; private set; }

        public UpdateItemDialog(WarehouseItem item)
        {
            InitializeComponent();
            UpdatedItem = item;
            txtName.Text = UpdatedItem.Name;
            txtType.Text = UpdatedItem.Type;
            txtSupplier.Text = UpdatedItem.Supplier;
            txtQuantity.Text = UpdatedItem.Quantity.ToString();
            txtCost.Text = UpdatedItem.Cost.ToString();
            dpSupplyDate.SelectedDate = UpdatedItem.SupplyDate;
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

                
                UpdatedItem.Name = txtName.Text;
                UpdatedItem.Type = txtType.Text;
                UpdatedItem.Supplier = txtSupplier.Text;
                UpdatedItem.Quantity = int.Parse(txtQuantity.Text);
                UpdatedItem.Cost = decimal.Parse(txtCost.Text);
                UpdatedItem.SupplyDate = dpSupplyDate.SelectedDate.Value;

      
                DialogResult = true;
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
