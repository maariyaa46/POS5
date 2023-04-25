using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace POS5.Models
{

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Item> tbl_Item { get; set; }
        public DbSet<Customer> tbl_Customer { get; set; }
        public DbSet<Brand> tbl_Brand { get; set; }
        public DbSet<Designation> tbl_Designation { get; set; }
        public DbSet<Category> tbl_Category { get; set; }
        public DbSet<Sales> tbl_Sales { get; set; }
        public DbSet<Supplier> tbl_Supplier{ get; set; }
        public DbSet<Stocks> tbl_Stocks { get; set; }
        public DbSet<SalesMaster> tbl_SalesMaster { get; set; }
        public DbSet<SaleDetail> tbl_SaleDetail { get; set; }
        public DbSet<Pmaster> tbl_Pmaster { get; set; }
        public DbSet<PDetail> tbl_PDetail { get; set; }
        public DbSet<SaleDetailReturn> tbl_SaleDetailReturn { get; set; }
        public DbSet<SaleMasterReturn> tbl_SaleMasterReturn { get; set; }
        public DbSet<PmasterR> tbl_PmasterR { get; set; }
        public DbSet<PdetailR> tbl_PdetailR { get; set; }
        public DbSet<queryModel.ItemLedger> tbl_ItemLedger { get; set; }
        public DbSet<queryModel.PurchaseReport> tbl_PurchaseReport { get; set; }
        public DbSet<queryModel.Table> tbl_Table { get; set; }
        public DbSet<queryModel.Invoice> tbl_Invoice { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}