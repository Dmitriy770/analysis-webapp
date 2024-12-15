 using FluentMigrator;

namespace Uis.Infrastructure.Repositories.Users.Migrations;

[Migration(20241512,  TransactionBehavior.None)]
internal sealed class AddUserTable : Migration
{
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("id").AsInt64().PrimaryKey("users_pk").Identity()
            .WithColumn("login").AsString(40).NotNullable()
            .WithColumn("name").AsString(40).NotNullable()
            .WithColumn("avatar_uri").AsString(100).NotNullable()
            .WithColumn("limit").AsInt32().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("users");
    }
}