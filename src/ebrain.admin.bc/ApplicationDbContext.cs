// ======================================
// Author: Ebrain Team
// Email:  info@ebrain.com.vn
// Copyright (c) 2017 www.ebrain.com.vn
// 
// ==> Contact Us: contact@ebrain.com.vn
// ======================================

using ebrain.admin.bc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ebrain.admin.bc.Models.Interfaces;
using ebrain.admin.bc.Utilities;
namespace ebrain.admin.bc
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public Guid? CurrentUserId { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Examine> Examine { get; set; }
        public DbSet<ClassExamine> ClassExamine { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<BranchSMS> BranchSMS { get; set; }
        public DbSet<BranchZalo> BranchZalo { get; set; }

        public DbSet<ConfigNumberOfCode> ConfigNumberOfCode { get; set; }

        public DbSet<BranchHead> BranchHead { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<ClassOffset> ClassOffset { get; set; }
        public DbSet<ClassEx> ClassEx { get; set; }
        public DbSet<Today> Today { get; set; }
        public DbSet<ClassHead> ClassHead { get; set; }
        public DbSet<Consultant> Consultant { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<DocumentHead> DocumentHeads { get; set; }
        public DbSet<GroupDocument> GroupDocument { get; set; }
        public DbSet<GroupDocumentHead> GroupDocumentHeads { get; set; }
        public DbSet<GrpMaterial> GrpMaterial { get; set; }
        public DbSet<TypeMaterial> TypeMaterial { get; set; }
        public DbSet<GrpSupplier> GrpSupplier { get; set; }
        public DbSet<Profit> Profit { get; set; }
        public DbSet<ProfitDetail> ProfitDetail { get; set; }
        public DbSet<ClassTime> ClassTime { get; set; }
        public DbSet<ClassStudent> ClassStudent { get; set; }
        public DbSet<ClassStatus> ClassStatus { get; set; }

        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<InventoryDetail> InventoryDetail { get; set; }
        public DbSet<Dept> Dept { get; set; }
        public DbSet<DeptDetail> DeptDetail { get; set; }

        public DbSet<IOStock> IOStock { get; set; }
        public DbSet<IOStockDetail> IOStockDetail { get; set; }
        public DbSet<IOType> IOType { get; set; }
        public DbSet<LevelClass> LevelClass { get; set; }
        public DbSet<LevelClassHead> LevelClassHeads { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<MessengerBranch> MessengerBranch { get; set; }
        public DbSet<Messenger> Messenger { get; set; }
        public DbSet<Support> Support { get; set; }
        public DbSet<MessengerRead> MessengerRead { get; set; }
        public DbSet<MaterialHead> MaterialHead { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<PaymentDetail> PaymentDetail { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<PaymentTypeHead> PaymentTypeHeads { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionDetail> PromotionDetails { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<RoomHead> RoomHeads { get; set; }
        public DbSet<ShiftClass> ShiftClass { get; set; }
        public DbSet<SMS> SMS { get; set; }
        public DbSet<ShiftClassHead> ShiftClassHeads { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<StockHead> StockHeads { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentRelationShip> StudentRelationShip { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<StudentStatus> StudentStatus { get; set; }
        public DbSet<AccessRight> AccessRight { get; set; }
        public DbSet<AccessRightPerson> AccessRightPerson { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<FeatureGroup> FeatureGroup { get; set; }//Chuc nang, Danh muc,..
        public DbSet<Feature> Feature { get; set; }//Phan tich hoat dong, Du doan dau tu,..
        public DbSet<UserGroup> UserGroup { get; set; }//admin, sales,..

        public ApplicationDbContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().HasMany(u => u.Claims).WithOne().HasForeignKey(c => c.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUser>().HasMany(u => u.Roles).WithOne().HasForeignKey(r => r.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationRole>().HasMany(r => r.Claims).WithOne().HasForeignKey(c => c.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationRole>().HasMany(r => r.Users).WithOne().HasForeignKey(r => r.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Customer>().Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Entity<Customer>().HasIndex(c => c.Name);
            builder.Entity<Customer>().Property(c => c.Email).HasMaxLength(100);
            builder.Entity<Customer>().Property(c => c.PhoneNumber).IsUnicode(false).HasMaxLength(30);
            builder.Entity<Customer>().Property(c => c.City).HasMaxLength(50);
            builder.Entity<Customer>().ToTable($"App{nameof(this.Customers)}");

            builder.Entity<ProductCategory>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Entity<ProductCategory>().Property(p => p.Description).HasMaxLength(500);
            builder.Entity<ProductCategory>().ToTable($"App{nameof(this.ProductCategories)}");

            builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Entity<Product>().HasIndex(p => p.Name);
            builder.Entity<Product>().Property(p => p.Description).HasMaxLength(500);
            builder.Entity<Product>().Property(p => p.Icon).IsUnicode(false).HasMaxLength(256);
            builder.Entity<Product>().HasOne(p => p.Parent).WithMany(p => p.Children).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Product>().ToTable($"App{nameof(this.Products)}");

            builder.Entity<Order>().Property(o => o.Comments).HasMaxLength(500);
            builder.Entity<Order>().ToTable($"App{nameof(this.Orders)}");

            builder.Entity<OrderDetail>().ToTable($"App{nameof(this.OrderDetails)}");

            builder.Entity<Branch>().ToTable(nameof(this.Branch));
            builder.Entity<BranchSMS>().ToTable(nameof(this.BranchSMS));
            builder.Entity<BranchZalo>().ToTable(nameof(this.BranchZalo));
            builder.Entity<Attendance>().ToTable(nameof(this.Attendance));

            builder.Entity<ClassExamine>().ToTable(nameof(this.ClassExamine));
            builder.Entity<Gender>().ToTable(nameof(this.Gender));
            builder.Entity<Examine>().ToTable(nameof(this.Examine));
            builder.Entity<ConfigNumberOfCode>().ToTable(nameof(this.ConfigNumberOfCode));

            builder.Entity<BranchHead>().ToTable(nameof(this.BranchHead));
            builder.Entity<Class>().ToTable(nameof(this.Class));
            builder.Entity<ClassOffset>().ToTable(nameof(this.ClassOffset));
            builder.Entity<ClassEx>().ToTable(nameof(this.ClassEx));
            builder.Entity<Class>().ToTable(nameof(this.Class));
            builder.Entity<Today>().ToTable(nameof(this.Today));
            builder.Entity<ClassHead>().ToTable(nameof(this.ClassHead));
            builder.Entity<Consultant>().ToTable(nameof(this.Consultant));
            builder.Entity<Document>().ToTable(nameof(this.Document));
            builder.Entity<DocumentHead>().ToTable(nameof(this.DocumentHeads));
            builder.Entity<GroupDocument>().ToTable(nameof(this.GroupDocument));
            builder.Entity<GroupDocumentHead>().Property(p => p.GroupDocumentHeadId).HasColumnName("GrpDocumentHeadId");
            builder.Entity<GroupDocumentHead>().ToTable(nameof(this.GroupDocumentHeads));
            builder.Entity<Inventory>().ToTable(nameof(this.Inventory));
            builder.Entity<InventoryDetail>().ToTable(nameof(this.InventoryDetail));
            builder.Entity<Profit>().ToTable(nameof(this.Profit));
            builder.Entity<ProfitDetail>().ToTable(nameof(this.ProfitDetail));

            builder.Entity<ClassStatus>().ToTable(nameof(this.ClassStatus));
            builder.Entity<ClassStudent>().ToTable(nameof(this.ClassStudent));
            builder.Entity<ClassTime>().ToTable(nameof(this.ClassTime));

            builder.Entity<Dept>().ToTable(nameof(this.Dept));
            builder.Entity<DeptDetail>().ToTable(nameof(this.DeptDetail));
            builder.Entity<IOStock>().ToTable(nameof(this.IOStock));
            builder.Entity<IOStockDetail>().ToTable(nameof(this.IOStockDetail));

            builder.Entity<IOType>().ToTable(nameof(this.IOType));
            builder.Entity<LevelClass>().ToTable(nameof(this.LevelClass));
            builder.Entity<LevelClassHead>().ToTable(nameof(this.LevelClassHeads));
            builder.Entity<Material>().ToTable(nameof(this.Material));
            builder.Entity<MaterialHead>().ToTable(nameof(this.MaterialHead));
            builder.Entity<Payment>().ToTable(nameof(this.Payment));

            builder.Entity<Messenger>().ToTable(nameof(this.Messenger));
            builder.Entity<MessengerBranch>().ToTable(nameof(this.MessengerBranch));
            builder.Entity<MessengerRead>().ToTable(nameof(this.MessengerRead));
            builder.Entity<Support>().ToTable(nameof(this.Support));
            
            builder.Entity<PaymentDetail>().ToTable(nameof(this.PaymentDetail));
            builder.Entity<PaymentType>().ToTable(nameof(this.PaymentType));
            builder.Entity<PaymentTypeHead>().ToTable(nameof(this.PaymentTypeHeads));
            builder.Entity<Promotion>().ToTable(nameof(this.Promotions));
            builder.Entity<PromotionDetail>().ToTable(nameof(this.PromotionDetails));

            builder.Entity<PurchaseOrder>().ToTable(nameof(this.PurchaseOrder));
            builder.Entity<PurchaseOrderDetail>().ToTable(nameof(this.PurchaseOrderDetail));
            builder.Entity<Relationship>().ToTable(nameof(this.Relationships));
            builder.Entity<Room>().ToTable(nameof(this.Room));
            builder.Entity<RoomHead>().ToTable(nameof(this.RoomHeads));

            builder.Entity<SMS>().ToTable(nameof(this.SMS));

            builder.Entity<ShiftClass>().ToTable(nameof(this.ShiftClass));

            builder.Entity<ShiftClassHead>().ToTable(nameof(this.ShiftClassHeads));
            builder.Entity<Stock>().ToTable(nameof(this.Stock));
            builder.Entity<StockHead>().ToTable(nameof(this.StockHeads));
            builder.Entity<Promotion>().ToTable(nameof(this.Promotions));
            builder.Entity<Student>().ToTable(nameof(this.Student));
            builder.Entity<StudentStatus>().ToTable(nameof(this.StudentStatus));

            builder.Entity<StudentRelationShip>().ToTable(nameof(this.StudentRelationShip));
            builder.Entity<Supplier>().ToTable(nameof(this.Supplier));
            builder.Entity<Unit>().ToTable(nameof(this.Unit));
            builder.Entity<AccessRight>().ToTable(nameof(this.AccessRight)).HasKey(x => new { x.FeatureID, x.GroupID });
            builder.Entity<AccessRightPerson>().ToTable(nameof(this.AccessRightPerson)).HasKey(x => new { x.FeatureId, x.UserId });
            builder.Entity<UserRole>().ToTable(nameof(this.UserRole)).HasKey(x => new { x.UserId, x.GroupId });
            builder.Entity<FeatureGroup>().ToTable(nameof(this.FeatureGroup));
            builder.Entity<Feature>().ToTable(nameof(this.Feature));
            builder.Entity<UserGroup>().ToTable(nameof(this.UserGroup));
        }




        public override int SaveChanges()
        {
            UpdateAuditEntities();
            return base.SaveChanges();
        }


        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateAuditEntities();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateAuditEntities();
            return base.SaveChangesAsync(cancellationToken);
        }


        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateAuditEntities();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


        private void UpdateAuditEntities()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));


            foreach (var entry in modifiedEntries)
            {
                var entity = (IAuditableEntity)entry.Entity;
                DateTime now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = now;
                    entity.CreatedBy = CurrentUserId;
                }
                else
                {
                    base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                }

                var lastUpdated = entry.Entity as IHistoricalEntity;
                if (lastUpdated != null)
                {
                    lastUpdated.UpdatedBy = CurrentUserId.HasValue ? CurrentUserId.Value : Guid.NewGuid();
                    lastUpdated.UpdatedDate = now;
                }

                entity.UpdatedDate = now;
                entity.UpdatedBy = CurrentUserId;
            }
        }
    }
}
