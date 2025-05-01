using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using OrderModel.Models;

namespace task_beta.Controllers
{
    public class editController : Controller
    {
        string connection = "Server=localhost;database=task;user=root;password=1arthur_nyamaa1";
        
        public Order order { get; set; } = new Order();

        [HttpGet]
        public IActionResult Editor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditorPost(Order order)
        {
            string sqlCommand = "UPDATE orderinfo " +
                                 "SET OrderName = @NameOrder, " +
                                    "OrderSurname = @SecondNameOrder," +
                                    "OrderEmail =  @EmailOrder, " +
                                    "OrderAddress = @AddressOrder," +
                                    "OrderQuantity = @QuantityOrder " +
                                "WHERE OrderId = @idOrder";
            using (MySqlConnection connectionCurrent = new MySqlConnection(connection))
            {
                connectionCurrent.Open();
                MySqlCommand commandCurrent = new MySqlCommand(sqlCommand, connectionCurrent);
                commandCurrent.Parameters.AddWithValue("@idOrder", order.IdOrder);
                commandCurrent.Parameters.AddWithValue("@NameOrder", order.NameOrder);
                commandCurrent.Parameters.AddWithValue("@SecondNameOrder", order.SecondNameOrder);
                commandCurrent.Parameters.AddWithValue("@EmailOrder", order.EmailOrder);
                commandCurrent.Parameters.AddWithValue("@AddressOrder", order.AddressOrder);
                commandCurrent.Parameters.AddWithValue("@QuantityOrder", order.QuantityOrder);

                commandCurrent.ExecuteNonQuery();
                connectionCurrent.Close();
            }

            return RedirectToAction("Editor", "Edit");
        }
    }
}
