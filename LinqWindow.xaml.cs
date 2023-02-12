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
using System.Windows.Shapes;

namespace Sales
{
    /// <summary>
    /// Interaction logic for LinqWindow.xaml
    /// </summary>
    public partial class LinqWindow : Window
    {
        private LinqContext.DataContext context;
        public LinqWindow()
        {
            InitializeComponent();
            try
            {
                context = new(App.ConnectinString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SimpleN_click(object sender, RoutedEventArgs e)
        {
            var query = context.Products.OrderBy(p => p.Name);
            textBlock1.Text = "";
            foreach (var item in query)
            {
                textBlock1.Text += item.Price + " " + item.Name + "\n";
            }
        }
        private void SimpleRP_click(object sender, RoutedEventArgs e)
        {
            var query = context.Products.OrderByDescending(p => p.Price);
            textBlock1.Text = "";
            foreach (var item in query)
            {
                textBlock1.Text += item.Price + " " + item.Name + "\n";
            }
        }
        private void SimpleTC_click(object sender, RoutedEventArgs e)
        {
            var query = context.Products.Where(p => p.Price < 200).OrderBy(prop => prop.Price);
            //var query = context
            textBlock1.Text = "";
            foreach (var item in query)
            {
                textBlock1.Text += item.Price + " " + item.Name + "\n";
            }
        }
        private void SimpleP_click(object sender, RoutedEventArgs e)
        {
            var query = context.Products.OrderBy(p => p.Price);
            textBlock1.Text = "";
            foreach (var item in query)
            {
                textBlock1.Text += item.Price + " " + item.Name + "\n";
            }
        }
        private void Join_click(object sender, RoutedEventArgs e)
        {
            var query = from m in context.Managers
                        join d in context.Departments on m.IdMainDep equals d.Id
                        select new
                        {
                            Manager = m.Surname + " " + m.Name,
                            Department = d.Name
                        };
            var query2 = context.Managers.Join(context.Departments, m => m.IdMainDep, d => d.Id,(m,d) => new { Manager = m.Surname + " " + m.Name,Deparment = d.Name});
            textBlock1.Text = "";
            foreach (var item in query)
            {
                textBlock1.Text += item.Manager + " - " + query.Count() + "Total " +  "\n";
            }
            textBlock1.Text += "\n" + query.Count() + " Total";
        }

    }
}
