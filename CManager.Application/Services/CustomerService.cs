using CManager.Domain.Models;
using CManager.Infrastructure.Repos;

namespace CManager.Application.Services;

// implementation of customer service interface that contains business logic for creating and managing customers
public class CustomerService(ICustomerRepo customerRepo) : ICustomerService
{

    private readonly ICustomerRepo _customerRepo = customerRepo;

    // method for creating customer with Id generated in CustomerModel
    public bool CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postalCode, string city)
    {
        CustomerModel customerModel = new()
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            Address = new AddressModel
            {
                StreetAddress = streetAddress,
                PostalCode = postalCode,
                City = city
            }
        };

        try
        {
            var customers = _customerRepo.GetAllCustomers();
            customers.Add(customerModel);
            var result = _customerRepo.SaveCustomers(customers);
            return result;
        }
        catch (Exception)
        {
            return false;
        }
    }

    // method for getting all customers
    public IEnumerable<CustomerModel> GetAllCustomers(out bool hasError)
    {
        hasError = false;
        try
        {
            var customers = _customerRepo.GetAllCustomers();
            return customers;
        }
        catch (Exception)
        {
            hasError = true;
            return [];
        }
    }

    // method for getting customer by Id
    public bool GetCustomerById(Guid id, out CustomerModel? customer)
    {
        customer = null;
        try
        {
            var customers = _customerRepo.GetAllCustomers();
            customer = customers.FirstOrDefault(c => c.Id == id);
            return customer != null;
        }
        catch (Exception)
        {
            return false;
        }
    }

    // method for deleting customer by email
    public bool DeleteCustomerByEmail(string email)
    {
        try
        {
            var customers = _customerRepo.GetAllCustomers();
            var customer = customers.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (customer == null)
                return false;

            customers.Remove(customer);
            return _customerRepo.SaveCustomers(customers);

        }
        catch (Exception)       
        {            
            return false;
        }
    }

}
