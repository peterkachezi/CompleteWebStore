using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.Owin.Security;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Web;
using Unity;
using Unity.Injection;
using WebStore.Controllers;
using WebStore.Data.Services.BranchModule;
using WebStore.Data.Services.BrandModule;
using WebStore.Data.Services.CompanyModule;
using WebStore.Data.Services.CountryModule;
using WebStore.Data.Services.CountyModule;
using WebStore.Data.Services.CustomerOrderModule;
using WebStore.Data.Services.CustomersModule;
using WebStore.Data.Services.DeliveryFeeModule;
using WebStore.Data.Services.ElectronicsModule;
using WebStore.Data.Services.EmployeeModule;
using WebStore.Data.Services.ExpenseModule;
using WebStore.Data.Services.ExpenseTypeModule;
using WebStore.Data.Services.FurnitureModule;
using WebStore.Data.Services.MpesaModule;
using WebStore.Data.Services.PaymentModeModule;
using WebStore.Data.Services.PaymentModule;
using WebStore.Data.Services.ProductCategoryModule;

using WebStore.Data.Services.ProductPackingModule;
using WebStore.Data.Services.SalesModule;
using WebStore.Data.Services.Services.ProductModule;
using WebStore.Data.Services.SMSModule;
using WebStore.Data.Services.SupplierModule;
using WebStore.Data.Services.UOMModule;
using WebStore.Data.Services.UserModule;
using WebStore.Data.Services.VehicleModule;
using WebStore.Helpers;
using WebStore.Models;
using WebStore.Services;

namespace WebStore
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        [Obsolete]
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        [Obsolete]
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        [Obsolete]
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterType<AccountController>(new InjectionConstructor());

            container.RegisterType<Microsoft.AspNet.Identity.IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            container.RegisterType(typeof(Microsoft.AspNet.Identity.IUserStore<>), typeof(UserStore<>));

            container.RegisterType<ISupplierService, SupplierService>();
            container.RegisterType<ICountryService, CountryService>();

            container.RegisterType<IProductCategoryService, ProductCategoryService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IBrandSerivce, BrandSerivce>();
            container.RegisterType<IBranchService, BranchService>();
            container.RegisterType<ICountyService, CountyService>();
            container.RegisterType<IExpenseTypeService, ExpenseTypeService>();
            container.RegisterType<IExpenseService, ExpenseService>();
            container.RegisterType<IUnitOfMeasureService, UnitOfMeasureService>();
            container.RegisterType<ISalesService, SalesService>();
            container.RegisterType<IPaymentModeService, PaymentModeService>();
            container.RegisterType<ICompanyService, CompanyService>();
            container.RegisterType<IProductPackingService, ProductPackingService>();
            container.RegisterType<ICustomersService, CustomersService>();
            container.RegisterType<ISMSService, SMSService>();
            container.RegisterType<IEmployeeService, EmployeeService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IPaymentService, PaymentService>();

            container.RegisterType<IMpesaService, MpesaService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IMailService, MailService>();

            container.RegisterType<IVehicleService, VehicleService>();
            container.RegisterType<IFurnitureService, FurnitureService>();
            container.RegisterType<IElectronicService, ElectronicService>();
            container.RegisterType<ICustomerOrderService, CustomerOrderService>();
            container.RegisterType<IdeliveryFeeService, DeliveryFeeService>();

            container.RegisterType<IUserClaimsPrincipalFactory<ApplicationUser>, CustomUserClaimsPrincipalFactory>();




        }
    }
}