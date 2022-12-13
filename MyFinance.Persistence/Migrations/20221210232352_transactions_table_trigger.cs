using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFinance.Persistence.Migrations
{
    public partial class transactions_table_trigger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                CREATE OR REPLACE FUNCTION before_insert_transaction() returns TRIGGER
                    LANGUAGE plpgsql AS
                $$
                BEGIN
                    UPDATE public."Accounts" accounts
                    SET "Balance" =
                            CASE
                                WHEN TG_OP = 'INSERT' THEN
                                    CASE
                                        WHEN cat."Type" = 0 THEN accounts."Balance" - NEW."Sum"
                                        WHEN cat."Type" = 1 THEN accounts."Balance" + NEW."Sum"
                                        END
                                WHEN TG_OP = 'UPDATE' THEN
                                    CASE
                                        WHEN cat."Type" = 0 THEN accounts."Balance" - (NEW."Sum" - OLD."Sum")
                                        WHEN cat."Type" = 1 THEN accounts."Balance" + (NEW."Sum" - OLD."Sum")
                                        END
                                END
                
                    FROM public."Categories" cat
                    WHERE NEW."CategoryId" = cat."Id";
                    RETURN NEW;
                END
                $$;
                
                CREATE OR REPLACE TRIGGER before_insert_transaction_trigger
                    AFTER INSERT OR UPDATE OF "Sum"
                    ON public."Transactions"
                    FOR EACH ROW
                EXECUTE FUNCTION before_insert_transaction();
                """);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP FUNCTION before_insert_transaction; DROP TRIGGER before_insert_transaction_trigger ON public.""Transactions"";");
        }
    }
}
