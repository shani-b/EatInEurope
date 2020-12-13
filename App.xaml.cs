using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EatInEurope
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DBConnect restaurants = new DBConnect();
            //MessageBox.Show(restaurants.OpenConnection().ToString());
            bool cond= restaurants.Insert("('shani','xxx')", "t_owners");
            MessageBox.Show(cond.ToString());
            //List<string> cond = restaurants.Check_existing("shani", "xxx", "t_owners");
            //MessageBox.Show("result:" + cond[0].ToString());
            List<string>[] cond2 = restaurants.Select("t_restaurants", "Price_Range LIKE '$'", "Rating", "DESC" );
            //MessageBox.Show("result:" + cond2.ToString());
            cond = restaurants.Delete("t_owners", "user_name = 'shani'");
            MessageBox.Show("result:" + cond.ToString());

        }
    }
}
