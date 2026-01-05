using CManager.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Presentation.ConsoleApp.Controllers;

public class MenuController
{
    private readonly ICustomerService _customerService;

    public MenuController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public void ShowMenu()
    {

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Customer Manager");
            Console.WriteLine("1. Create Customer");
            Console.WriteLine("2. View All Customers");
            Console.WriteLine("3. View Specific Customer");
            Console.WriteLine("0. Exit");
            Console.Write("Choose option: ");


            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CreateCustomer();
                    break;

                case "2":
                    ViewAllCustomers();
                    break;

                case "3":
                    ViewSpecificCustomer();
                    break;

                case "0":
                    return;

                default:
                    OutputDialog("Invalid option! Press any key to continue...");
                    break;
            }
        }

    }

    private void CreateCustomer()
    {
        Console.Clear();
        Console.WriteLine("Create Customer");

        Console.Write("First Name: ");
        var firstName = Console.ReadLine()!;

        Console.Write("Last Name: ");
        var lastName = Console.ReadLine()!;

        Console.Write("Email: ");
        var email = Console.ReadLine()!;

        Console.Write("PhoneNumber: ");
        var phoneNumber = Console.ReadLine()!;

        Console.Write("Street Address: ");
        var streetAddress = Console.ReadLine()!;

        Console.Write("Postal Code: ");
        var postalCode = Console.ReadLine()!;

        Console.Write("City: ");
        var city = Console.ReadLine()!;


        var result = _customerService.CreateCustomer(firstName, lastName, email, phoneNumber, streetAddress, postalCode, city);

        if (result)
        {
            Console.WriteLine("Customer created");
            Console.WriteLine($"Name: {firstName} {lastName}");
        }
        else
        {
            Console.WriteLine("Something went wrong. Please try again");
        }
        OutputDialog("Press any key to continue...");

    }


    private void ViewAllCustomers()
    {
        Console.Clear();
        Console.WriteLine("All Customers");

        var customers = _customerService.GetAllCustomers(out bool hasError);

        if (hasError)
        {
            Console.WriteLine("Something went wrong. Please try again later");
        }

        if (!customers.Any())
        {
            Console.WriteLine("No customers found");
        }
        else
        {
            foreach (var customer in customers)
            {
                Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine($"Phone: {customer.PhoneNumber}");
                Console.WriteLine($"Address: {customer.Address.StreetAddress} {customer.Address.PostalCode} {customer.Address.City}");
                Console.WriteLine($"ID: {customer.Id}");
                Console.WriteLine();
            }
        }

        OutputDialog("Press any key to continue...");
    }

    private void ViewSpecificCustomer()

    {      
                
        Console.Clear();
        Console.WriteLine("View Specific Customer");
        Console.Write("Enter Customer ID: ");
        var input = Console.ReadLine()!;
                
        if (!Guid.TryParse(input, out Guid customerId))
        {
            OutputDialog("Invalid Customer ID format. Press any key to continue...");
            return;
        }
        
        bool found = _customerService.GetCustomerById(customerId, out var customer);

        if (!found)       
        
        {
            OutputDialog("Customer not found. Press any key to continue...");
            return;
        }
        if (found)

        Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
        Console.WriteLine($"Email: {customer.Email}");
        Console.WriteLine($"Phone: {customer.PhoneNumber}");
        Console.WriteLine($"Address: {customer.Address.StreetAddress} {customer.Address.PostalCode} {customer.Address.City}");
        Console.WriteLine($"ID: {customer.Id}");
        

        OutputDialog("Press any key to continue..."); 
        
    }


    private void OutputDialog(string message)
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }


}
