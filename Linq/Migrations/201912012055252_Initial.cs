namespace Linq.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pessoas",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Codigo);
            
            CreateTable(
                "dbo.Veiculos",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        CodigoCliente = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.Pessoas", t => t.CodigoCliente, cascadeDelete: true)
                .Index(t => t.CodigoCliente);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Veiculos", "CodigoCliente", "dbo.Pessoas");
            DropIndex("dbo.Veiculos", new[] { "CodigoCliente" });
            DropTable("dbo.Veiculos");
            DropTable("dbo.Pessoas");
        }
    }
}
