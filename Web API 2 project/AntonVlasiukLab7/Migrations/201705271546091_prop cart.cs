namespace AntonVlasiukLab7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class propcart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Cart_Id", c => c.Int());
            CreateIndex("dbo.Items", "Cart_Id");
            AddForeignKey("dbo.Items", "Cart_Id", "dbo.Carts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "Cart_Id", "dbo.Carts");
            DropIndex("dbo.Items", new[] { "Cart_Id" });
            DropColumn("dbo.Items", "Cart_Id");
        }
    }
}
