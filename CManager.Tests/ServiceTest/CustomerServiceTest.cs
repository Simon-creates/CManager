using CManager.Application.Services;
using CManager.Domain.Models;
using CManager.Infrastructure.Repos;
using Moq;

namespace CManager.Tests.ServiceTests;

public class CustomerServiceTest
{
    [Fact]
    public void DeleteCustomer_WhenCustomerDoesNotExist_ReturnsFalse()
    {
        //arrange
        var mockCustomerRepo = new Mock<ICustomerRepo>();

        mockCustomerRepo
            .Setup(r => r.GetAllCustomers())
            .Returns(new List<CustomerModel>());

        var service = new CustomerService(mockCustomerRepo.Object);
         
        //act
        var result = service.DeleteCustomerByEmail("test@test.se");

        //assert
        Assert.False(result);
        mockCustomerRepo.Verify(r => r.GetAllCustomers(), Times.Once);
        mockCustomerRepo.Verify(r => r.SaveCustomers(It.IsAny<List<CustomerModel>>()), Times.Never);
    }
}

