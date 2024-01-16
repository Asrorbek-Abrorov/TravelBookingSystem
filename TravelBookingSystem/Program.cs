using Spectre.Console;
using TravelBookingSystem.Displays;
using TravelBookingSystem.Displays.other;
using TravelBookingSystem.services;

class Program
{
    static void Main(string[] args)
    {
        var travelPackageRepository = new TravelPackageRepository("../../../../TravelBookingSystem/Datas/travel_packages.txt");
        var customerRepository = new CustomerRepository("../../../../TravelBookingSystem/Datas/customers.txt");
        var bookingRepository = new BookingRepository("../../../../TravelBookingSystem/Datas/bookings.txt");
        var paymentRepository = new PaymentRepository("../../../../TravelBookingSystem/Datas/payments.txt");

        var travelPackageManager = new TravelPackageManager(travelPackageRepository);
        var customerManager = new CustomerManager(customerRepository);
        var bookingManager = new BookingManager(bookingRepository);
        var paymentManager = new PaymentManager(paymentRepository);

        var travelPackageMenu = new TravelPackageMenu(travelPackageManager);
        var customerMenu = new CustomerMenu(customerManager);
        var bookingMenu = new BookingMenu(bookingManager, customerManager);
        var paymentMenu = new PaymentMenu(paymentManager, bookingManager);

        var mainMenu = new MainMenu(
            travelPackageManager,
            customerManager,
            bookingManager,
            paymentManager,
            travelPackageMenu,
            customerMenu,
            bookingMenu,
            paymentMenu);

        mainMenu.Run();

    }
}