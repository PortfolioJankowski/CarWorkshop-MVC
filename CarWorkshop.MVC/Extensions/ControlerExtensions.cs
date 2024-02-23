using CarWorkshop.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CarWorkshop.MVC.Extensions
{
    public static class ControlerExtensions
    {
        public static void SetNotification(this Controller controller, string type, string message)
        {
            var notofication = new Notification(type, message);
            //serializujemy ją tutaj, żeby deserializować ją na widoku
            controller.TempData["Notification"] = JsonConvert.SerializeObject(notofication);
        }
    }
}
