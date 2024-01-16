using Spectre.Console;
using TravelBookingSystem.services;

namespace TravelBookingSystem.Displays.other
{
    public class CustomerMenu
    {
        private readonly CustomerManager customerManager;

        public CustomerMenu(CustomerManager customerManager)
        {
            this.customerManager = customerManager;
        }

        public void Run()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new FigletText("* Customer Menu *")
                        .LeftJustified()
                        .Color(Color.Red1));
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]*** Customer Menu ***[/]?")
                        .PageSize(10)
                        .AddChoices(new[]
                        {
                            "View All Customers",
                            "View Customer by ID",
                            "Add Customer",
                            "Update Customer",
                            "Remove Customer",
                            "Go Back"
                        }));

                switch (choice)
                {
                    case "View All Customers":
                        ViewAllCustomers();
                        break;

                    case "View Customer by ID":
                        ViewCustomerById();
                        break;

                    case "Add Customer":
                        AddCustomer();
                        break;

                    case "Update Customer":
                        UpdateCustomer();
                        break;

                    case "Remove Customer":
                        RemoveCustomer();
                        break;

                    case "Go Back":
                        keepRunning = false;
                        break;
                }
            }
        }

        private void ViewAllCustomers()
        {
            var customers = customerManager.GetAllCustomersAsync().Result;

            if (customers.Count == 0)
            {
                AnsiConsole.WriteLine("No customers found.");
            }
            else
            {
                foreach (var customer in customers)
                {
                    AnsiConsole.WriteLine($"Customer ID: {customer.Id}");
                    AnsiConsole.WriteLine($"Name: {customer.Name}");
                    AnsiConsole.WriteLine($"Contact Details: {customer.ContactDetails}");
                    AnsiConsole.WriteLine($"Payment Information: {customer.PaymentInformation}");
                    AnsiConsole.WriteLine();
                }
            }

            AnsiConsole.WriteLine("Press Enter to continue...");
            Console.ReadKey();
        }

        private void ViewCustomerById()
        {
            int customerId = AnsiConsole.Ask<int>("Enter the customer ID:");

            var customer = customerManager.GetCustomerByIdAsync(customerId).Result;

            if (customer == null)
            {
                AnsiConsole.WriteLine($"Customer with ID {customerId} not found.");
            }
            else
            {
                AnsiConsole.WriteLine($"Customer ID: [bold]{customer.Id}[/]");
                AnsiConsole.WriteLine($"Name: {customer.Name}");
                AnsiConsole.WriteLine($"Contact Details: {customer.ContactDetails}");
                AnsiConsole.WriteLine($"Payment Information: {customer.PaymentInformation}");
            }

            AnsiConsole.WriteLine("Press Enter to continue...");
            Console.ReadKey();
        }

        private void AddCustomer()
        {
            string name = AnsiConsole.Ask<string>("Enter the customer name:");
            string contactDetails = AnsiConsole.Ask<string>("Enter the contact details:");

            if (!IsValidContactDetails(contactDetails))
            {
                AnsiConsole.WriteLine("Invalid contact details. It should be in the format +998xx*******.");
                AnsiConsole.WriteLine("Press Enter to continue...");
                Console.ReadKey();
                return;
            }

            string paymentInformation = AnsiConsole.Ask<string>("Enter the payment information:");

            Customer newCustomer = new Customer
            {
                Name = name,
                ContactDetails = contactDetails,
                PaymentInformation = paymentInformation
            };

            customerManager.AddCustomerAsync(newCustomer).Wait();

            AnsiConsole.WriteLine("Customer added successfully!");
            AnsiConsole.WriteLine("Press Enter to continue...");
            Console.ReadKey();
        }

        private bool IsValidContactDetails(string contactDetails)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(contactDetails, @"^\+998\d{9}$");
        }

        private void UpdateCustomer()
        {
            int customerId = AnsiConsole.Ask<int>("Enter the customer ID:");

            var customer = customerManager.GetCustomerByIdAsync(customerId).Result;

            if (customer == null)
            {
                AnsiConsole.WriteLine($"Customer with ID {customerId} not found.");
            }
            else
            {
                string newName = AnsiConsole.Ask<string>("Enter the new customer name:");
                string newContactDetails = AnsiConsole.Ask<string>("Enter the new contact details:");
                string newPaymentInformation = AnsiConsole.Ask<string>("Enter the new payment information:");

                customer.Name = newName;
                customer.ContactDetails = newContactDetails;
                customer.PaymentInformation = newPaymentInformation;

                customerManager.UpdateCustomerAsync(customer).Wait();

                AnsiConsole.WriteLine("Customer updated successfully!");
            }

            AnsiConsole.WriteLine("Press Enter to continue...");
            Console.ReadKey();
        }

        private void RemoveCustomer()
        {
            int customerId = AnsiConsole.Ask<int>("Enter the customer ID:");

            var customer = customerManager.GetCustomerByIdAsync(customerId).Result;

            if (customer == null)
            {
                AnsiConsole.WriteLine($"Customer with ID {customerId} not found.");
            }
            else
            {
                customerManager.RemoveCustomerAsync(customerId).Wait();

                AnsiConsole.WriteLine("Customer removed successfully!");
            }

            AnsiConsole.WriteLine("Press Enter to continue...");
            Console.ReadKey();
        }
    }
}