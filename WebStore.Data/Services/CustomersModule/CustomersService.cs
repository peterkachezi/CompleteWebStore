using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.CustomerModule;
using WebStore.Data.EDMX;


namespace WebStore.Data.Services.CustomersModule
{
    public class CustomersService : ICustomersService
    {
        private WinkadStoreEntities context;

        public CustomersService(WinkadStoreEntities context)
        {
            this.context = context;
        }
        public async Task<CustomerDTO> AddCustomer(CustomerDTO customerDTO)
        {
            try
            {

                string customer_number = CustomerNumber.GenerateUniqueNumber();

                customerDTO.CustomerNumber = "CUS" + "" + customer_number;

                var c = new Customer
                {
                    Id = Guid.NewGuid(),

                    FirstName = customerDTO.FirstName.Substring(0, 1).ToUpper() + customerDTO.FirstName.Substring(1).ToLower().Trim(),

                    MiddleName = customerDTO.MiddleName.Substring(0, 1).ToUpper() + customerDTO.MiddleName.Substring(1).ToLower().Trim(),

                    LastName = customerDTO.LastName.Substring(0, 1).ToUpper() + customerDTO.LastName.Substring(1).ToLower().Trim(),

                    Email = customerDTO.Email.ToLower().Trim(),

                    PhoneNumber = customerDTO.PhoneNumber,

                    CustomerNumber = customerDTO.CustomerNumber,

                    CreatedBy = customerDTO.CreatedBy,

                    CreateDate = DateTime.Now,

                    Apartment = customerDTO.Apartment,

                    DeliveryLocation = customerDTO.DeliveryLocation,
                };

                context.Customers.Add(c);

                await context.SaveChangesAsync();

                return customerDTO;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<bool> DeleteCustomer(Guid Id)
        {
            try
            {
                bool result = false;

                var s = await context.Customers.FindAsync(Id);

                if (s != null)
                {
                    context.Customers.Remove(s);

                    await context.SaveChangesAsync();

                    return true;
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public async Task<CustomerDTO> Update(CustomerDTO customerDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.Customers.FindAsync(customerDTO.Id);
                    {
                        s.FirstName = customerDTO.FirstName.Substring(0, 1).ToUpper() + customerDTO.FirstName.Substring(1).ToLower().Trim();

                        s.MiddleName = customerDTO.MiddleName.Substring(0, 1).ToUpper() + customerDTO.MiddleName.Substring(1).ToLower().Trim();

                        s.LastName = customerDTO.LastName.Substring(0, 1).ToUpper() + customerDTO.LastName.Substring(1).ToLower().Trim();

                        s.Email = customerDTO.Email.ToLower().Trim();

                        s.PhoneNumber = customerDTO.PhoneNumber;

                    }

                    await context.SaveChangesAsync();

                    transaction.Commit();
                }

                return customerDTO;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

                return null;
            }
        }


        public async Task<List<CustomerDTO>> GetAllCustomers()
        {
            try
            {
                var customer = await context.Customers.ToListAsync();

                var customers = new List<CustomerDTO>();

                foreach (var item in customer)
                {
                    var data = new CustomerDTO
                    {
                        Id = item.Id,

                        FirstName = item.FirstName,

                        MiddleName = item.MiddleName,

                        LastName = item.LastName,

                        Email = item.Email,

                        PhoneNumber = item.PhoneNumber,

                        CustomerNumber = item.CustomerNumber,

                        CreatedBy = item.CreatedBy ==null ? "" : item.CreatedBy,

                        CreatedByFirstName = item.AspNetUser.FirstName ==null ? "" : item.AspNetUser.FirstName,

                        CreatedByLastName = item.AspNetUser.LastName ==null ? "" : item.AspNetUser.LastName,

                        CreateDate = item.CreateDate
                    };

                    customers.Add(data);
                }

                return customers;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<CustomerDTO> GetCustomerById(Guid Id)
        {
            try
            {
                var customer = await context.Customers.FindAsync(Id);

                return new CustomerDTO
                {
                    Id = customer.Id,

                    FirstName = customer.FirstName,

                    MiddleName = customer.MiddleName,

                    LastName = customer.LastName,

                    Email = customer.Email,

                    PhoneNumber = customer.PhoneNumber,

                    CustomerNumber = customer.CustomerNumber,
                       
                    CreatedBy = customer.CreatedBy == null ? "" : customer.CreatedBy,

                    CreatedByFirstName = customer.AspNetUser.FirstName == null ? "" : customer.AspNetUser.FirstName,

                    CreatedByLastName = customer.AspNetUser.LastName == null ? "" : customer.AspNetUser.LastName,

                    CreateDate = customer.CreateDate
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
