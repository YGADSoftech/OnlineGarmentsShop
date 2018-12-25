namespace EntitiesLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aaa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CartItems", "size_Name", c => c.String(nullable: false, maxLength: 200, unicode: false));
            AlterColumn("dbo.CartItems", "color_name", c => c.String(nullable: false, maxLength: 200, unicode: false));
            AlterColumn("dbo.Orders", "OrderID", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Orders", "orderstate", c => c.String(nullable: false, maxLength: 30, unicode: false));
            AlterColumn("dbo.Orders", "paymentType", c => c.String(maxLength: 30, unicode: false));
            DropColumn("dbo.CartItems", "size_Number");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CartItems", "size_Number", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "paymentType", c => c.String());
            AlterColumn("dbo.Orders", "orderstate", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "OrderID", c => c.String(nullable: false, maxLength: 200, unicode: false));
            AlterColumn("dbo.CartItems", "color_name", c => c.String(nullable: false));
            DropColumn("dbo.CartItems", "size_Name");
        }
    }
}
