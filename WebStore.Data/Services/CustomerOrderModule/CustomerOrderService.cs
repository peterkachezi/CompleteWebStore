using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.CustomerModule;
using WebStore.Data.DTOs.CustomerOrderModule;
using WebStore.Data.EDMX;

namespace WebStore.Data.Services.CustomerOrderModule
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly WinkadStoreEntities context;

        public CustomerOrderService(WinkadStoreEntities context)
        {
            this.context = context;
        }
        public async Task<CustomerOrderDTO> Create(CustomerOrderDTO customerOrderDTO)
        {
            try
            {

                string customer_number = CustomerNumber.GenerateUniqueNumber();

                customerOrderDTO.CustomerNumber = "CUS" + "" + customer_number;

                var customerID = Guid.NewGuid();

                customerOrderDTO.CustomerId = customerID;

                var c = new Customer
                {
                    Id = customerOrderDTO.CustomerId,

                    FirstName = customerOrderDTO.FirstName.Substring(0, 1).ToUpper() + customerOrderDTO.FirstName.Substring(1).ToLower().Trim(),

                    MiddleName = customerOrderDTO.MiddleName.Substring(0, 1).ToUpper() + customerOrderDTO.MiddleName.Substring(1).ToLower().Trim(),

                    LastName = customerOrderDTO.LastName.Substring(0, 1).ToUpper() + customerOrderDTO.LastName.Substring(1).ToLower().Trim(),

                    Email = customerOrderDTO.Email.ToLower().Trim(),

                    PhoneNumber = customerOrderDTO.PhoneNumber,

                    CustomerNumber = customerOrderDTO.CustomerNumber,

                    CreatedBy = customerOrderDTO.CreatedBy,

                    Apartment = customerOrderDTO.Apartment,

                    DeliveryLocation = customerOrderDTO.DeliveryLocation,

                    CreateDate = DateTime.Now,
                };

                context.Customers.Add(c);

                string order_number = CustomerNumber.GenerateUniqueNumber();

                var orderNumber = ("ORN" + order_number);

                customerOrderDTO.OrderNumber = orderNumber;


                var s = new CustomerOrder
                {
                    Id = Guid.NewGuid(),

                    ProductId = customerOrderDTO.ProductId,

                    OrderNumber = customerOrderDTO.OrderNumber,

                    OrderDate = DateTime.Now,

                    OrderNotes = customerOrderDTO.OrderNotes,

                    CustomerId = customerOrderDTO.CustomerId,

                    Quantity = customerOrderDTO.Quantity,

                };

                context.CustomerOrders.Add(s);

                await context.SaveChangesAsync();

                return customerOrderDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                bool result = false;

                var s = await context.CustomerOrders.FindAsync(Id);

                if (s != null)
                {
                    context.CustomerOrders.Remove(s);

                };

                await context.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public async Task<List<CustomerOrderDTO>> GetAll()
        {
            try
            {
                var order = await context.CustomerOrders.ToListAsync();

                var orders = new List<CustomerOrderDTO>();

                foreach (var item in order)
                {
                    var data = new CustomerOrderDTO
                    {
                        Id = item.Id,

                        ProductId = item.ProductId,

                        OrderNumber = item.OrderNumber,

                        OrderDate = item.OrderDate,

                        OrderNotes = item.OrderNotes,

                        FirstName = item.Customer.FirstName,

                        MiddleName = item.Customer.MiddleName,

                        LastName = item.Customer.LastName,

                        PhoneNumber = item.Customer.PhoneNumber,

                        Email = item.Customer.Email,

                        ProductName = item.Product.Name + " " + item.Product.ProductPackaging.Name + " " + item.Product.ProductPackaging.Unit,

                        SellingPrice = item.Product.SellingPrice,

                        ProductCode = item.Product.ProductCode,

                        Quantity=item.Quantity,

                    };
                    orders.Add(data);
                }
                return orders;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }

        public async Task<CustomerOrderDTO> GetById(Guid Id)
        {
            try
            {
                var order = await context.CustomerOrders.FindAsync(Id);

                return new CustomerOrderDTO
                {
                    Id = order.Id,

                    ProductId = order.ProductId,

                    OrderNumber = order.OrderNumber,

                    OrderDate = order.OrderDate,

                    OrderNotes = order.OrderNotes,

                    FirstName = order.Customer.FirstName,

                    MiddleName = order.Customer.MiddleName,

                    LastName = order.Customer.LastName,

                    PhoneNumber = order.Customer.PhoneNumber,

                    Email = order.Customer.Email,

                    ProductName = order.Product.Name + " " + order.Product.ProductPackaging.Name + " " + order.Product.ProductPackaging.Unit,

                    SellingPrice = order.Product.SellingPrice,

                    ProductCode = order.Product.ProductCode,

                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<CustomerOrderDTO> GetByOrderNumber(string OrderNumber)
        {
            try
            {
                var order = await context.CustomerOrders.Where(x => x.OrderNumber == OrderNumber).FirstOrDefaultAsync();

                return new CustomerOrderDTO
                {
                    Id = order.Id,

                    ProductId = order.ProductId,

                    OrderNumber = order.OrderNumber,

                    OrderDate = order.OrderDate,

                    OrderNotes = order.OrderNotes,

                    FirstName = order.Customer.FirstName,

                    MiddleName = order.Customer.MiddleName,

                    LastName = order.Customer.LastName,

                    PhoneNumber = order.Customer.PhoneNumber,

                    Email = order.Customer.Email,

                    ProductName = order.Product.Name + " " + order.Product.ProductPackaging.Name + " " + order.Product.ProductPackaging.Unit,

                    SellingPrice = order.Product.SellingPrice,

                    ProductCode = order.Product.ProductCode,
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public Task<CustomerOrderDTO> Update(CustomerOrderDTO customerOrderDTO)
        {
            throw new NotImplementedException();
        }
    }
}
