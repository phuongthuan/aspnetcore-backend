using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class SeedDatabase : Migration
    {
protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Category (Name) VALUES ('Laptop')");
            migrationBuilder.Sql("INSERT INTO Category (Name) VALUES ('Camera')");
            migrationBuilder.Sql("INSERT INTO Category (Name) VALUES ('Mouse')");


            migrationBuilder.Sql("INSERT INTO Product (Name, Quantity, UnitPrice, CategoryId) VALUES ('DELL xps15', 10, 2000, (SELECT ID FROM Category WHERE Name='Laptop'))");
            migrationBuilder.Sql("INSERT INTO Product (Name, Quantity, UnitPrice, CategoryId) VALUES ('Macbook Pro 2017', 10, 2500, (SELECT ID FROM Category WHERE Name='Laptop'))");
            migrationBuilder.Sql("INSERT INTO Product (Name, Quantity, UnitPrice, CategoryId) VALUES ('MSI GE 602PC', 10, 2000, (SELECT ID FROM Category WHERE Name='Laptop'))");


            migrationBuilder.Sql("INSERT INTO Product (Name, Quantity, UnitPrice, CategoryId) VALUES ('Canon EOS M500', 10, 4000, (SELECT ID FROM Category WHERE Name='Camera'))");
            migrationBuilder.Sql("INSERT INTO Product (Name, Quantity, UnitPrice, CategoryId) VALUES ('Nikon NK300', 10, 3000, (SELECT ID FROM Category WHERE Name='Camera'))");
            migrationBuilder.Sql("INSERT INTO Product (Name, Quantity, UnitPrice, CategoryId) VALUES ('Sony SNY 250', 5, 2500, (SELECT ID FROM Category WHERE Name='Camera'))");
            migrationBuilder.Sql("INSERT INTO Product (Name, Quantity, UnitPrice, CategoryId) VALUES ('Canon EOS D5', 3, 5000, (SELECT ID FROM Category WHERE Name='Camera'))");

            migrationBuilder.Sql("INSERT INTO Product (Name, Quantity, UnitPrice, CategoryId) VALUES ('Razer Death Adder 2016', 20, 70, (SELECT ID FROM Category WHERE Name='Mouse'))");
            migrationBuilder.Sql("INSERT INTO Product (Name, Quantity, UnitPrice, CategoryId) VALUES ('Steelseries Rival 700', 30, 100, (SELECT ID FROM Category WHERE Name='Mouse'))");
            migrationBuilder.Sql("INSERT INTO Product (Name, Quantity, UnitPrice, CategoryId) VALUES ('Logitech M900s', 10, 150, (SELECT ID FROM Category WHERE Name='Mouse'))");

            migrationBuilder.Sql("INSERT INTO Customer (FirstName, LastName, Email, Status) VALUES ('Alex', 'Hunter', 'alex@guest.com', 'working')");
            migrationBuilder.Sql("INSERT INTO Customer (FirstName, LastName, Email, Status) VALUES ('Peter', 'Dagger', 'peter@guest.com', 'working')");
            migrationBuilder.Sql("INSERT INTO Customer (FirstName, LastName, Email, Status) VALUES ('Jack', 'Ripper', 'jack@guest.com', 'working')");
            migrationBuilder.Sql("INSERT INTO Customer (FirstName, LastName, Email, Status) VALUES ('Nguyen', 'Phuong Thuan', 'admin@admin.com', 'working')");


            migrationBuilder.Sql("INSERT INTO [Order] (OrderDate, TotalPrice, CustomerID) VALUES ('2017-02-03', 3000, (SELECT ID FROM Customer WHERE FirstName='Alex'))");
            migrationBuilder.Sql("INSERT INTO [Order] (OrderDate, TotalPrice, CustomerID) VALUES ('2017-03-05', 3600, (SELECT ID FROM Customer WHERE FirstName='Peter'))");
            migrationBuilder.Sql("INSERT INTO [Order] (OrderDate, TotalPrice, CustomerID) VALUES ('2017-08-06', 3200, (SELECT ID FROM Customer WHERE FirstName='Jack'))");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Category WHERE Name IN ('Laptop', 'Camera', 'Mouse')");
            migrationBuilder.Sql("DELETE FROM Customer WHERE FirstName IN ('Alex', 'Peter', 'Jack')");
        }
    }
}
