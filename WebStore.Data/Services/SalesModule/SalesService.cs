using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.SalesDetailsModule;
using WebStore.Data.DTOs.SalesModule;
using WebStore.Data.EDMX;
using static WebStore.Data.Utils.Enumerations;

namespace WebStore.Data.Services.SalesModule
{
    public class SalesService : ISalesService
    {
        private readonly WinkadStoreEntities context;

        public SalesService(WinkadStoreEntities context)
        {
            this.context = context;
        }
        public async Task<SalesDTO> AddSalesOrder(SalesDTO salesOrederDTO)
        {
            try
            {

                string order_number = CustomerNumber.GenerateUniqueNumber();

                var orderNumber = ("ORN" + order_number);

                salesOrederDTO.OrderNumber = orderNumber;

                salesOrederDTO.Id = Guid.NewGuid();

                salesOrederDTO.OrderDate = DateTime.Now;


                var s = new Sale
                {
                    Id = salesOrederDTO.Id,

                    CustomerId = salesOrederDTO.CustomerId,

                    OrderDate = salesOrederDTO.OrderDate,

                    CreatedBy = salesOrederDTO.CreatedBy,

                    TotalAmount = salesOrederDTO.TotalAmount,

                    OrderNumber = salesOrederDTO.OrderNumber,

                    CashPaid = salesOrederDTO.CashPaid,

                    Change = salesOrederDTO.Change,
                };

                context.Sales.Add(s);

                var SalesOrderDetailList = new List<SalesDetail>();


                foreach (var item in salesOrederDTO.ListOfSalesDetails)
                {
                    var orderlist = new SalesDetail();
                    {
                        orderlist.Id = Guid.NewGuid();
                        ;
                        orderlist.OrderId = salesOrederDTO.Id;

                        orderlist.ProductId = item.ProductId;

                        orderlist.Quantity = item.Quantity;

                        orderlist.SellingPrice = item.SellingPrice;

                        orderlist.Discount = item.Discount;

                        orderlist.Total = item.Total;

                        orderlist.OrderNumber = salesOrederDTO.OrderNumber;

                        orderlist.CreateDate = salesOrederDTO.OrderDate;

                        orderlist.CreatedBy = salesOrederDTO.CreatedBy;

                        orderlist.PaymentStatus = 1;

                    };

                    context.SalesDetails.Add(orderlist);

                    SalesOrderDetailList.Add(orderlist);
                }

                var result = await context.SaveChangesAsync() > 0;

                if (result == true)
                {
                    foreach (var item in SalesOrderDetailList)
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            var k = context.Products.Where(x => x.Id == item.ProductId).FirstOrDefault();
                            {
                                k.Quantity = k.Quantity - item.Quantity;

                                context.SaveChanges();

                                transaction.Commit();
                            }
                        }

                    }
                }

                return salesOrederDTO;




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }
        public async Task<List<SalesDTO>> GetCustomerOrders()
        {
            try
            {
                var order = await context.Sales.Where(x=>x.CustomerId !=null).ToListAsync();

                var orders = new List<SalesDTO>();

                foreach (var item in order)
                {
                    var data = new SalesDTO
                    {
                        Id = item.Id,

                        CustomerId = item.CustomerId,

                        OrderDate = item.OrderDate,

                        CreatedBy = item.CreatedBy,

                        TotalAmount = item.TotalAmount,

                        Change = item.Change,

                        OrderNumber = item.OrderNumber,

                        CustomerName = item.Customer.FirstName + " " + item.Customer.MiddleName + " " + item.Customer.LastName,

                        CreatedByName = item.AspNetUser.FirstName + " " + item.AspNetUser.LastName,

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
         public async Task<List<SalesDetailsDTO>> GetSalesOrdersDetails()
        {
            try
            {

                var order = await context.SalesDetails.OrderByDescending(x => x.CreateDate).ToListAsync();

                var orders = new List<SalesDetailsDTO>();

                foreach (var item in order)
                {
                    var data = new SalesDetailsDTO
                    {
                        Id = item.Id,

                        OrderId = item.OrderId,

                        ProductId = item.ProductId,

                        //CustomerId = item.Sale.CustomerId,

                        //CustomerName = item.Sale.Customer.FirstName + " " + item.Sale.Customer.MiddleName + " " + item.Sale.Customer.LastName,

                        CreateByName = item.AspNetUser.FirstName + " " + item.AspNetUser.LastName,

                        Quantity = item.Quantity,

                        SellingPrice = item.SellingPrice,

                        Discount = item.Discount,

                        Total = item.Total,

                        OrderNumber = item.OrderNumber,

                        CreateDate = item.CreateDate,

                        CreatedBy = item.CreatedBy,

                        ProductName = item.Product.Name + " " + item.Product.ProductPackaging.Name + " " + item.Product.ProductPackaging.Unit,

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
        public async Task<List<SalesDetailsDTO>> GetSalesOrdersDetailsByOrderId(Guid Id)
        {
            try
            {
                var order = await context.SalesDetails.Where(x => x.OrderId == Id).OrderByDescending(x => x.CreateDate).ToListAsync();

                var orders = new List<SalesDetailsDTO>();

                foreach (var item in order)
                {
                    var data = new SalesDetailsDTO
                    {
                        Id = item.Id,

                        OrderId = item.OrderId,

                        ProductId = item.ProductId,

                        CustomerId = item.Sale.CustomerId,

                        CustomerName = item.Sale.Customer.FirstName + " " + item.Sale.Customer.MiddleName + " " + item.Sale.Customer.LastName,

                        CreateByName = item.AspNetUser.FirstName + " " + item.AspNetUser.LastName,

                        Quantity = item.Quantity,

                        SellingPrice = item.SellingPrice,

                        Discount = item.Discount,

                        Total = item.Total,

                        OrderNumber = item.OrderNumber,

                        CreateDate = item.CreateDate,

                        CreatedBy = item.CreatedBy,

                        ProductName = item.Product.Name + " " + item.Product.ProductPackaging.Name + " " + item.Product.ProductPackaging.Unit,

                        PaymentStatus=item.PaymentStatus,

                        PaymentStatusDescription = GetDescription((PaymentStatus)item.PaymentStatus),

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
        public async Task<List<SalesDetailsDTO>> GetSalesOrdersDetailsByOrderNumber(string OrderNumber)
        {
            try
            {
                var order = await context.SalesDetails.Where(x => x.OrderNumber == OrderNumber).ToListAsync();

                var orders = new List<SalesDetailsDTO>();

                foreach (var item in order)
                {
                    var data = new SalesDetailsDTO
                    {
                        Id = item.Id,

                        OrderId = item.OrderId,

                        ProductId = item.ProductId,

                        //CustomerId = item.Sale.CustomerId,

                        //CustomerName = item.Sale.Customer.FirstName + " " + item.Sale.Customer.MiddleName + " " + item.Sale.Customer.LastName,

                        ProductName = item.Product.Name + " " + item.Product.ProductPackaging.Name + " " + item.Product.ProductPackaging.Unit,

                        CreateByName = item.Sale.AspNetUser.FirstName + " " + item.Sale.AspNetUser.LastName,

                        Quantity = item.Quantity,

                        SellingPrice = item.SellingPrice,

                        Discount = item.Discount,

                        OrderTotalAmount = item.Sale.TotalAmount,

                        Total = item.Total,

                        OrderNumber = item.Sale.OrderNumber,

                        CreateDate = item.Sale.OrderDate,

                        CreatedBy = item.CreatedBy,

                        Change = item.Sale.Change,

                        CashPaid = item.Sale.CashPaid,

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
        public async Task<SalesDetailsDTO> GetSalesOrdersDetailsById(Guid Id)
        {
            try
            {

                var order = await context.SalesDetails.FindAsync(Id);

                return new SalesDetailsDTO
                {
                    Id = order.Id,

                    OrderId = order.OrderId,

                    ProductId = order.ProductId,

                    CustomerId = order.Sale.CustomerId,

                    CustomerName = order.Sale.Customer.FirstName + " " + order.Sale.Customer.MiddleName + " " + order.Sale.Customer.LastName,

                    //ProductName = order.Product.Name,

                    CreateByName = order.AspNetUser.FirstName + " " + order.AspNetUser.LastName,

                    Quantity = order.Quantity,

                    SellingPrice = order.SellingPrice,

                    Discount = order.Discount,

                    Total = order.Total,

                    OrderNumber = order.OrderNumber,

                    CreateDate = order.CreateDate,

                    CreatedBy = order.CreatedBy,

                };


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        private static object SyncObj = new object();
        static Dictionary<Enum, string> _enumDescriptionCache = new Dictionary<Enum, string>();
        public static string GetDescription(Enum value)
        {
            if (value == null) return string.Empty;

            lock (SyncObj)
            {
                if (!_enumDescriptionCache.ContainsKey(value))
                {
                    var description = (from m in value.GetType().GetMember(value.ToString())
                                       let attr = (DescriptionAttribute)m.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault()
                                       select attr == null ? value.ToString() : attr.Description).FirstOrDefault();

                    _enumDescriptionCache.Add(value, description);
                }
            }

            return _enumDescriptionCache[value];
        }

    }
}

