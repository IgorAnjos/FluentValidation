using fluentValidation.Domain.ValueObjects;
using System;

namespace fluentValidation
{
    class Program
    {
        static void Main(string[] args)
        {
            var name = new Name("", "");

            foreach (var notification in name.Notifications)
            {
                Console.WriteLine(notification.Message);
            }

            Console.ReadKey();
        }
    }
}
