using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyManager.Migrations
{
    public partial class AddCurrentBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>("CurrentBalance", "Asset", nullable: true);
            migrationBuilder.Sql(@"UPDATE Asset 
                                    Set CurrentBalance = amountTable.Amount
                                    From Asset
                                    inner join (SELECT table1.AssetId,
   		                            table1.deb - table2.cred AS Amount
                                    FROM  (SELECT AssetId, sum(Amount) deb
	                                FROM Transaction 
	                                Inner Join Category on Transaction.CategoryId = Category.Id 
	                                where Category.Type = 1
	                                group by AssetId) table1 JOIN (SELECT AssetId, sum(Amount) cred
	                                FROM Transaction 
	                                Inner Join Category on Transaction.CategoryId = Category.Id 
	                                where Category.Type = 0
	                                group by AssetId) 
	                                table2 on table1.AssetId = table2.AssetId) amountTable
	                                on Asset.Id = amountTable.AssetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
