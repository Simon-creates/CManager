using CManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CManager.Application.Services;

// Interface for CustomerService
public interface ICustomerService
{
    bool CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postalCode, string city);

    IEnumerable<CustomerModel> GetAllCustomers(out bool hasError);

    bool GetCustomerById(Guid id, out CustomerModel? customer);

    bool DeleteCustomerByEmail(string email);

}
