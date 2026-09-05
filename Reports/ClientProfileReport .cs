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
    internal class ClientProfileReport : IDocument
    {

        private readonly Client _client;

        public ClientProfileReport(Client client)
        {
            _client = client;
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

                page.Content().Element(ComposeContent);
            });

        }


        void ComposeContent(IContainer container)
        {
            container.Column(column =>
            {
                column.Spacing(10);

                // caption
                column.Item().Text(TaskHive.Strings.ClientInfo)
                    .FontSize(16)
                    .Bold()
                    .AlignCenter();

                
                column.Item().Row(row =>
                {
                   
                    row.ConstantItem(120)
                       .Height(140)
                       .Border(1)
                       .AlignCenter()
                       .AlignMiddle()
                       .Element(img =>
                       {
                           if (File.Exists(_client.image))
                               img.Image(_client.image, ImageScaling.FitArea);
                           else
                               img.Text(TaskHive.Strings.NoImage).AlignCenter();
                       });

                
                    row.RelativeItem().PaddingLeft(10).Column(info =>
                    {
                        info.Item().Text(_client.fullname).Bold();
                        info.Item().Text($"{TaskHive.Strings.Phone }: {_client.phone}");
                        info.Item().Text($"{TaskHive.Strings.Email}: {_client.myemail}");
                        info.Item().Text($"{TaskHive.Strings.nationality}: {_client.nationality}");
                    });
                });

           
                column.Item().Element(ComposeProfileTable);
                column.Item().Element(ComposeProfileDescription);
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

                AddRow(table, TaskHive.Strings.fname +" "+ TaskHive.Strings.lname, _client.fullname);
                AddRow(table, TaskHive.Strings.fname, _client.fname);
                AddRow(table, TaskHive.Strings.lname, _client.lname);
                AddRow(table, TaskHive.Strings.Phone, _client.phone);
                AddRow(table, TaskHive.Strings.Email, _client.myemail);
                AddRow(table, TaskHive.Strings.age, _client.age.ToString());
                AddRow(table, TaskHive.Strings.nationality, _client.nationality);
                AddRow(table, TaskHive.Strings.TelegramID, _client.idtelegram);
                AddRow(table, TaskHive.Strings.gender, _client.personeltype);
                AddRow(table, TaskHive.Strings.address, _client.address);

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

                AddRow(table, TaskHive.Strings.description, _client.des);


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
