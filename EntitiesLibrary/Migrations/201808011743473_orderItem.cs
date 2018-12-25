namespace EntitiesLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        size_Number = c.Int(nullable: false),
                        color_name = c.String(nullable: false),
                        Products_Id = c.Int(nullable: false),
                        Orders_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Products_Id, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Orders_Id)
                .Index(t => t.Products_Id)
                .Index(t => t.Orders_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        Price = c.Single(nullable: false),
                        Description = c.String(maxLength: 100, unicode: false),
                        LaunchDate = c.DateTime(),
                        fabric_Id = c.Int(),
                        subCategory_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fabrics", t => t.fabric_Id)
                .ForeignKey("dbo.SubCategories", t => t.subCategory_Id, cascadeDelete: true)
                .Index(t => t.fabric_Id)
                .Index(t => t.subCategory_Id);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Fabrics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Caption = c.String(maxLength: 100, unicode: false),
                        Priority = c.Int(nullable: false),
                        Url = c.String(nullable: false, maxLength: 300, unicode: false),
                        Products_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Products_Id)
                .Index(t => t.Products_Id);
            
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        ImageUrl = c.String(maxLength: 300, unicode: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        ImageUrl = c.String(maxLength: 300, unicode: false),
                        department_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.department_Id, cascadeDelete: true)
                .Index(t => t.department_Id);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        ImageUrl = c.String(maxLength: 300, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Province_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provinces", t => t.Province_Id, cascadeDelete: true)
                .Index(t => t.Province_Id);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Country_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id, cascadeDelete: true)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        address = c.String(nullable: false, maxLength: 200, unicode: false),
                        OrderID = c.String(nullable: false, maxLength: 200, unicode: false),
                        orderstate = c.String(nullable: false),
                        OrderDate = c.DateTime(),
                        paymentType = c.String(),
                        TotalAmount = c.Single(nullable: false),
                        user_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.user_Id, cascadeDelete: true)
                .Index(t => t.user_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 20, unicode: false),
                        Password = c.String(nullable: false, maxLength: 20, unicode: false),
                        ContactNumber = c.String(maxLength: 20, unicode: false),
                        Email = c.String(maxLength: 255, unicode: false),
                        ImageUrl = c.String(maxLength: 255, unicode: false),
                        BirthDate = c.DateTime(),
                        LastName = c.String(maxLength: 100, unicode: false),
                        SecurityQuestion = c.String(maxLength: 255, unicode: false),
                        SecurityAnswer = c.String(maxLength: 255, unicode: false),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        StreetAddress = c.String(nullable: false, maxLength: 255, unicode: false),
                        City_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.City_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.City_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductsColors",
                c => new
                    {
                        Products_Id = c.Int(nullable: false),
                        Color_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Products_Id, t.Color_Id })
                .ForeignKey("dbo.Products", t => t.Products_Id, cascadeDelete: true)
                .ForeignKey("dbo.Colors", t => t.Color_Id, cascadeDelete: true)
                .Index(t => t.Products_Id)
                .Index(t => t.Color_Id);
            
            CreateTable(
                "dbo.ProductsSizes",
                c => new
                    {
                        Products_Id = c.Int(nullable: false),
                        Size_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Products_Id, t.Size_Id })
                .ForeignKey("dbo.Products", t => t.Products_Id, cascadeDelete: true)
                .ForeignKey("dbo.Sizes", t => t.Size_Id, cascadeDelete: true)
                .Index(t => t.Products_Id)
                .Index(t => t.Size_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "user_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Addresses", "Id", "dbo.Users");
            DropForeignKey("dbo.Addresses", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.CartItems", "Orders_Id", "dbo.Orders");
            DropForeignKey("dbo.Cities", "Province_Id", "dbo.Provinces");
            DropForeignKey("dbo.Provinces", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.CartItems", "Products_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "subCategory_Id", "dbo.SubCategories");
            DropForeignKey("dbo.SubCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Categories", "department_Id", "dbo.Departments");
            DropForeignKey("dbo.ProductsSizes", "Size_Id", "dbo.Sizes");
            DropForeignKey("dbo.ProductsSizes", "Products_Id", "dbo.Products");
            DropForeignKey("dbo.ProductImages", "Products_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "fabric_Id", "dbo.Fabrics");
            DropForeignKey("dbo.ProductsColors", "Color_Id", "dbo.Colors");
            DropForeignKey("dbo.ProductsColors", "Products_Id", "dbo.Products");
            DropIndex("dbo.ProductsSizes", new[] { "Size_Id" });
            DropIndex("dbo.ProductsSizes", new[] { "Products_Id" });
            DropIndex("dbo.ProductsColors", new[] { "Color_Id" });
            DropIndex("dbo.ProductsColors", new[] { "Products_Id" });
            DropIndex("dbo.Addresses", new[] { "City_Id" });
            DropIndex("dbo.Addresses", new[] { "Id" });
            DropIndex("dbo.Users", new[] { "Role_Id" });
            DropIndex("dbo.Orders", new[] { "user_Id" });
            DropIndex("dbo.Provinces", new[] { "Country_Id" });
            DropIndex("dbo.Cities", new[] { "Province_Id" });
            DropIndex("dbo.Categories", new[] { "department_Id" });
            DropIndex("dbo.SubCategories", new[] { "Category_Id" });
            DropIndex("dbo.ProductImages", new[] { "Products_Id" });
            DropIndex("dbo.Products", new[] { "subCategory_Id" });
            DropIndex("dbo.Products", new[] { "fabric_Id" });
            DropIndex("dbo.CartItems", new[] { "Orders_Id" });
            DropIndex("dbo.CartItems", new[] { "Products_Id" });
            DropTable("dbo.ProductsSizes");
            DropTable("dbo.ProductsColors");
            DropTable("dbo.Roles");
            DropTable("dbo.Addresses");
            DropTable("dbo.Users");
            DropTable("dbo.Orders");
            DropTable("dbo.Countries");
            DropTable("dbo.Provinces");
            DropTable("dbo.Cities");
            DropTable("dbo.Departments");
            DropTable("dbo.Categories");
            DropTable("dbo.SubCategories");
            DropTable("dbo.Sizes");
            DropTable("dbo.ProductImages");
            DropTable("dbo.Fabrics");
            DropTable("dbo.Colors");
            DropTable("dbo.Products");
            DropTable("dbo.CartItems");
        }
    }
}
