
using Northwind.Business.Abstract;
using Northwind.Business.Concrete;
using Northwind.Business.DependencyResolvers.ninject;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.DataAccess.Concrete.NHibermate;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Northwind.WebFormsUI
{
    public partial  class Ürünler : Form
    {
        public Ürünler()
        {
            InitializeComponent();
            _productService = InstanceFactory.GetInstance<IProductService>();
            _categoryService = InstanceFactory.GetInstance<ICategoryService>();
        }

        private IProductService _productService;
        private ICategoryService _categoryService;

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadCategories();
        }

        private void LoadCategories()
        {
            cbxCategory.DataSource = _categoryService.GetAll();
            cbxCategory.DisplayMember = "CategoryName";
            cbxCategory.ValueMember = "CategoryId";

            cbxCategoryId.DataSource = _categoryService.GetAll();
            cbxCategoryId.DisplayMember = "CategoryName";
            cbxCategoryId.ValueMember = "CategoryId";

            cbxCategoryIdUpdate.DataSource = _categoryService.GetAll();
            cbxCategoryIdUpdate.DisplayMember = "CategoryName";
            cbxCategoryIdUpdate.ValueMember = "CategoryId";
        }

        private void LoadProducts()
        {
            dgwProduct.DataSource = _productService.GetAll();
        }

        private void LoadProducts(string searchIndex)
        {
        dgwProduct.DataSource=_productService.GetAll().Where(x=>x.ProductName.Contains(searchIndex)).ToList();
        }

        private static void SayiEngelle(KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private static void HarfEngelle(KeyPressEventArgs e)
        {
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
           
        }


        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgwProduct.DataSource = _productService.GetProductsByCategory(Convert.ToInt32(cbxCategory.SelectedValue));
            }
            catch 
            {
               
            }
            
        }

        private void tbxProductName_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbxProductName.Text))
            { 
                dgwProduct.DataSource = _productService.GetProductsByProductName(tbxProductName.Text);
            }
            else 
            {
                LoadProducts();
            }
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        { 
            try
            {
                _productService.Add(new Product  
                {
                    CategoryId = Convert.ToInt32(cbxCategoryId.SelectedValue),
                    ProductName = tbxProductName2.Text,
                    QuantityPerUnit = tbxQuantityPerUnit.Text,
                    UnitPrice = Convert.ToDecimal(tbxUnitPrice.Text),
                    UnitsInStock = Convert.ToInt16(tbxStock.Text),
                });
                MessageBox.Show("Ürün Eklendi");
                TbxNull2();
                LoadProducts();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                _productService.Update(new Product
                {
                    ProductId = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value),
                    ProductName = tbxProductNameUpdate.Text,
                    CategoryId = Convert.ToInt32(cbxCategoryIdUpdate.SelectedValue),
                    UnitsInStock = Convert.ToInt16(tbxUnitInStockUpdate.Text),
                    QuantityPerUnit = tbxQuantityPerUnitUpdate.Text,
                    UnitPrice = Convert.ToDecimal(tbxUnitPriceUpdate.Text),
                });
                MessageBox.Show("Ürün güncellendi!!");
                TbxNull();
                LoadProducts();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
               
        }
        //Güncelleme tarafının textboxları kaydetikten sonra sıfırlama methodu.
        private void TbxNull()
        {
            tbxProductNameUpdate.Clear();
            tbxUnitPriceUpdate.Clear();
            tbxUnitInStockUpdate.Clear();
            tbxQuantityPerUnitUpdate.Clear();
        }
        //Ekleme tarafının textboxları kaydetikten sonra sıfırlama methodu.
        private void TbxNull2()
        {
            tbxProductName2.Clear();
            tbxUnitPrice.Clear();
            tbxStock.Clear();
            tbxQuantityPerUnit.Clear();
        }

        private void dgwProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgwProduct.CurrentRow;
            tbxProductNameUpdate.Text = row.Cells[1].Value.ToString();
            cbxCategoryIdUpdate.SelectedValue = row.Cells[2].Value;
            tbxUnitPriceUpdate.Text = row.Cells[3].Value.ToString();
            tbxQuantityPerUnitUpdate.Text = row.Cells[4].Value.ToString();
            tbxUnitInStockUpdate.Text = row.Cells[5].Value.ToString();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        { 
            try
            {
                if (dgwProduct.CurrentRow != null)
                {
                    _productService.Delete(new Entities.Concrete.Product());
                }
                MessageBox.Show("Ürün silindi!!");
                LoadProducts();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            } 
        }
        //Ekleme operasyonundaki harf ve sayı engelleme
        private void tbxProductName2_KeyPress(object sender, KeyPressEventArgs e)
        {
            SayiEngelle(e);
        }

        private void tbxUnitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            HarfEngelle(e);
        }

        private void tbxStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            HarfEngelle(e);
        }

        private void tbxQuantityPerUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            HarfEngelle(e);
        }

        //Güncelleme operasyonundaki harf ve sayı engelleme
        private void tbxProductNameUpdate_KeyPress(object sender, KeyPressEventArgs e)
        {
            SayiEngelle(e);
        }

        private void tbxUnitPriceUpdate_KeyPress(object sender, KeyPressEventArgs e)
        {
            HarfEngelle(e);
        }

        private void tbxUnitInStockUpdate_KeyPress(object sender, KeyPressEventArgs e)
        {
            HarfEngelle(e);
        }

        private void tbxQuantityPerUnitUpdate_KeyPress(object sender, KeyPressEventArgs e)
        {
            HarfEngelle(e);
        }
        //isme göre aramada sayi engelleme
        private void tbxProductName_KeyPress(object sender, KeyPressEventArgs e)
        {
            SayiEngelle(e);
            if ((int)e.KeyChar == 32)
            {
                e.Handled = true;
            }
        }
    }
}
