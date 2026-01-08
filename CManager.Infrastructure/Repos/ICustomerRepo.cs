using CManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Infrastructure.Repos;

// Interface for customer repository
public interface ICustomerRepo
{
    List<CustomerModel> GetAllCustomers();
    bool SaveCustomers(List<CustomerModel> customers);
}
