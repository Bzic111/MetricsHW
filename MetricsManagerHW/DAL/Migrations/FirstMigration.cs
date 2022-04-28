using FluentMigrator;

namespace MetricsManagerHW.DAL.Migrations;

[Migration(1)]
public class FirstMigration : Migration
{
    public override void Up()
    {
        Create.Table("agents")
              .WithColumn("Id")
              .AsInt64()
              .PrimaryKey()
              .Identity()
              .WithColumn("Adress")
              .AsString()
              .WithColumn("Enabled")
              .AsBoolean();

        Create.Table("cpumetrics")
              .WithColumn("Id")
              .AsInt64()
              .PrimaryKey()
              .Identity()
              .WithColumn("agentId")
              .AsInt64()
              .WithColumn("Value")
              .AsInt32()
              .WithColumn("DateTime")
              .AsString();

        Create.Table("hddmetrics")
              .WithColumn("Id")
              .AsInt64()
              .PrimaryKey()
              .Identity()
              .WithColumn("agentId")
              .AsInt64()
              .WithColumn("Value")
              .AsInt32()
              .WithColumn("DateTime")
              .AsString();

        Create.Table("rammetrics")
              .WithColumn("Id")
              .AsInt64()
              .PrimaryKey()
              .Identity()
              .WithColumn("agentId")
              .AsInt64()
              .WithColumn("Value")
              .AsInt32()
              .WithColumn("DateTime")
              .AsString();

        Create.Table("networkmetrics")
              .WithColumn("Id")
              .AsInt64()
              .PrimaryKey()
              .Identity()
              .WithColumn("agentId")
              .AsInt64()
              .WithColumn("Value")
              .AsInt32()
              .WithColumn("DateTime")
              .AsString();

        Create.Table("dotnetmetrics")
              .WithColumn("Id")
              .AsInt64()
              .PrimaryKey()
              .Identity()
              .WithColumn("agentId")
              .AsInt64()
              .WithColumn("Value")
              .AsInt32()
              .WithColumn("DateTime")
              .AsString();
    }
    public override void Down()
    {
        Delete.Table("agents");
        Delete.Table("cpumetrics");
        Delete.Table("dotnetmetrics");
        Delete.Table("networkmetrics");
        Delete.Table("rammetrics");
        Delete.Table("hddmetrics");
    }
}
