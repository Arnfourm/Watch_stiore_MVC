using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using OrderModel.Models;

namespace task_beta.Controllers
{
    public class watchController : Controller
    {
        string connection = "Server=localhost;database=task;user=root;password=1arthur_nyamaa1";

        private static List<Order> orders = new List<Order>();

        public IActionResult Observer()
        {
            orders.Clear();

            string sqlCommandOrder = "select * from orderinfo";
            using (MySqlConnection connectionCurrent = new MySqlConnection(connection))
            {
                connectionCurrent.Open();
                MySqlCommand commandCurrent = new MySqlCommand(sqlCommandOrder, connectionCurrent);
                using (var sqlSelectProduct = commandCurrent.ExecuteReader())
                {
                    while (sqlSelectProduct.Read())
                    {
                        orders.Add(new Order
                        {
                            IdOrder = sqlSelectProduct.GetInt32("OrderId"),
                            NameOrder = sqlSelectProduct.GetString("OrderName"),
                            SecondNameOrder = sqlSelectProduct.GetString("OrderSurname"),
                            EmailOrder = sqlSelectProduct.GetString("OrderEmail"),
                            AddressOrder = sqlSelectProduct.GetString("OrderAddress"),
                            QuantityOrder = sqlSelectProduct.GetInt32("OrderQuantity"),
                        });
                    }
                }
            }

            ViewBag.Orders = orders;

            return View();
        }
    }
}
