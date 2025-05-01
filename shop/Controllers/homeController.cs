using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using OrderModel.Models;
using ProductModel.Models;

namespace task_beta.Controllers
{
    public class HomeController : Controller
    {

        string connection = "Server=localhost;database=task;user=root;password=1arthur_nyamaa1";

        private List<Product> products = new List<Product>();

        [BindProperty]
        public Order order { get; set; } = new Order();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            products.Clear();

            string sqlCommandProduct = "select * from product";
            using (MySqlConnection connectionCurrent = new MySqlConnection(connection))
            {
                connectionCurrent.Open();
                MySqlCommand commandCurrent = new MySqlCommand(sqlCommandProduct, connectionCurrent);
                using (var sqlSelectProduct = commandCurrent.ExecuteReader())
                {
                    while (sqlSelectProduct.Read())
                    {
                        products.Add(new Product
                        {
                            IdProduct = sqlSelectProduct.GetInt32("ProductId"),
                            NameProduct = sqlSelectProduct.GetString("ProductName"),
                            CostProduct = sqlSelectProduct.GetInt32("ProductCost"),
                            imagePathProduct = sqlSelectProduct.GetString("ProductIMG")
                        });
                    }
                }
            }

            ViewBag.Products = products;
            ViewBag.succesValid = successValid;
            ViewBag.errorValid = errorValid;

            return View();
        }


        private string successValid = "";
        private string errorValid = "";

        [HttpPost]
        public IActionResult Order(Order order)
        {
            if (!ModelState.IsValid)
            {
                errorValid = "Некоторые данные были введены неправильно!";
                return RedirectToAction("Index");
            }

            string sqlCommand = "insert into orderinfo (OrderName, OrderSurname, OrderEmail, OrderAddress, OrderQuantity)" +
                                "values (@NameOrder, @SecondNameOrder,  @EmailOrder, @AddressOrder, @QuantityOrder); ";
            using (MySqlConnection connectionCurrent = new MySqlConnection(connection))
            {
                connectionCurrent.Open();
                MySqlCommand commandCurrent = new MySqlCommand(sqlCommand, connectionCurrent);
                commandCurrent.Parameters.AddWithValue("@NameOrder", order.NameOrder);
                commandCurrent.Parameters.AddWithValue("@SecondNameOrder", order.SecondNameOrder);
                commandCurrent.Parameters.AddWithValue("@EmailOrder", order.EmailOrder);
                commandCurrent.Parameters.AddWithValue("@AddressOrder", order.AddressOrder);
                commandCurrent.Parameters.AddWithValue("@QuantityOrder", order.QuantityOrder);

                commandCurrent.ExecuteNonQuery();
                connectionCurrent.Close();
            }

            successValid = $"Спасибо {order.NameOrder} {order.SecondNameOrder}, заказ выполнен успешно!{Environment.NewLine}" +
               $"Доставим товар по адресу {order.AddressOrder} в количестве {order.QuantityOrder} шт.{Environment.NewLine}" +
               $"Электронный чек придет в ближайшее время на почту {order.EmailOrder}";

            return RedirectToAction("Index");
        }
    }
}
