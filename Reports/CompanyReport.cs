using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using TaskHive.Classes;
using System.IO;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace TaskHive.Reports
{
    internal class CompanyReport : IDocument
    {

        private readonly Companies _client;

        public CompanyReport(Companies MyCompany)
        {
            _client = MyCompany;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(25);
                page.DefaultTextStyle(x =>
                    x.FontSize(10)
                     .FontFamily("Arial"));

                page.Content().Element(ComposeProfileTable);
            });

        }



        void ComposeProfileTable(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(150);
                    columns.RelativeColumn();
                });

                AddRow(table, TaskHive.Strings.companyname, _client.CompanyName);
                AddRow(table, TaskHive.Strings.CompanyAddress, _client.Address);
                AddRow(table, TaskHive.Strings.Phone, _client.Phone);
                AddRow(table, TaskHive.Strings.Email, _client.Email);
                AddRow(table, TaskHive.Strings.ServiceType, _client.ServiceType);
                AddRow(table, TaskHive.Strings.RegistrationDate, _client.CreatedAt);

            });
        }

        void ComposeProfileDescription(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                });

                AddRow(table, TaskHive.Strings.description, _client.Notes);


            });
        }

        void addrowdes(TableDescriptor table, string title, string value) {

            table.Cell().Element(CellTitleDes).Text(title);
            table.Cell().Element(CellValueDes).Text(value ?? "-");

        }
        void AddRow(TableDescriptor table, string title, string value)
        {
            table.Cell().Element(CellTitle).Text(title);
            table.Cell().Element(CellValue).Text(value ?? "-");


        }

        static IContainer CellTitle(IContainer container) =>
            container.Border(1)
                     .Background(Colors.Grey.Lighten3)
                     .Padding(5)
                     .AlignLeft();

        static IContainer CellValue(IContainer container) =>
            container.Border(1)
                     .Padding(5)
                     .AlignLeft();

        static IContainer CellTitleDes(IContainer container) =>
    container.Border(1)
             .Background(Colors.Grey.Lighten3)
             .Padding(5)
             .AlignCenter();

        static IContainer CellValueDes(IContainer container) =>
            container.Border(1)
                     .Padding(5)
                     .AlignLeft()
                     .Extend()
                         ;


    }




}
