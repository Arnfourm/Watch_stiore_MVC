using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using OrderModel.Models;

namespace task_beta.Controllers
{
    public class deleteController : Controller
    {
        string connection = "Server=localhost;database=task;user=root;password=1arthur_nyamaa1";

        public Order order = new Order();

        [HttpGet]
        public IActionResult Deleter()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleterPost(Order order)
        {
            string sqlCommand = "DELETE FROM orderinfo WHERE OrderId = @idOrder";

            using (MySqlConnection connectionCurrent = new MySqlConnection(connection))
            {
                connectionCurrent.Open();
                MySqlCommand commandCurrent = new MySqlCommand(sqlCommand, connectionCurrent);
                commandCurrent.Parameters.AddWithValue("@idOrder", order.IdOrder);

                commandCurrent.ExecuteNonQuery();
                connectionCurrent.Close();
            }

            return RedirectToAction("Deleter", "Delete");
        }
    }
}
