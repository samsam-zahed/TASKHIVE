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
    internal class ReportClients : IDocument
    {
        private readonly List<Client> _clients;

        public ReportClients(List<Client> clients)
        {
            _clients = clients;
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
                column.Spacing(15);

                int index = 1;

                foreach (var client in _clients)
                {
                    column.Item().Element(x => ComposeClient(x, client, index));
                    index++;
                }
            });
        }


        void ComposeClient(IContainer container, Client client, int number)
        {
            container.Column(main =>
            {
                
                main.Item().Row(row =>
                {
                  
                    row.ConstantItem(120)
                       .Height(140)
                       .Border(1)
                       .AlignCenter()
                       .AlignMiddle()
                       .Element(img =>
                       {
                           if (!string.IsNullOrEmpty(client.image) && File.Exists(client.image))
                               img.Image(client.image, ImageScaling.FitArea);
                           else
                               img.Text(TaskHive.Strings.NoImage).AlignCenter();
                       });

                  
                    row.RelativeItem().PaddingLeft(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        // Row 1
                        table.Cell().Element(CellTitle).Text(TaskHive.Strings.ID);
                        table.Cell().Element(CellTitle).Text(TaskHive.Strings.fname);
                        table.Cell().Element(CellTitle).Text(TaskHive.Strings.lname);
                        table.Cell().Element(CellTitle).Text(TaskHive.Strings.Phone);

                        table.Cell().Element(CellValue).Text(number.ToString());
                        table.Cell().Element(CellValue).Text(client.fname);
                        table.Cell().Element(CellValue).Text(client.lname);
                        table.Cell().Element(CellValue).Text(client.phone);

                        // Row 2
                        table.Cell().Element(CellTitle).Text(TaskHive.Strings.TelegramID);
                        table.Cell().Element(CellTitle).Text(TaskHive.Strings.age);
                        table.Cell().Element(CellTitle).Text(TaskHive.Strings.nationality);
                        table.Cell().Element(CellTitle).Text(TaskHive.Strings.gender);

                        table.Cell().Element(CellValue).Text(client.idtelegram);
                        table.Cell().Element(CellValue).Text(client.age.ToString());
                        table.Cell().Element(CellValue).Text(client.nationality);
                        table.Cell().Element(CellValue).Text(client.personeltype);

                        table.Cell().ColumnSpan(4)
       .Element(CellTitleLeft)
       .Text(TaskHive.Strings.Email);

                        table.Cell().ColumnSpan(4)
                            .Element(CellValueLeft)
                            .Text(client.myemail ?? "-");

                    });
                });

            
                main.Item().PaddingTop(10).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.Cell().ColumnSpan(4)
                        .Element(CellTitleLeft)
                        .Text(TaskHive.Strings.address);

                    table.Cell().ColumnSpan(4)
                        .Element(CellValueLeft)
                        .Text(client.address ?? "-");
                });

            
                main.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.Cell().ColumnSpan(4)
                        .Element(CellTitleLeft)
                        .Text(TaskHive.Strings.description);

                    table.Cell().ColumnSpan(4)
                        .Element(CellValueLeft)
                        .Text(client.des ?? "-");
                });
            });
        }

        static IContainer CellTitle(IContainer container) =>
            container.Border(1)
                     .Background(Colors.Grey.Lighten3)
                     .Padding(5)
                     .AlignCenter();

        static IContainer CellValue(IContainer container) =>
            container.Border(1)
                     .Padding(5)
                     .AlignCenter();


        static IContainer CellTitleLeft(IContainer container) =>
    container.Border(1)
             .Background(Colors.Grey.Lighten3)
             .Padding(5)
             .AlignLeft();

        static IContainer CellValueLeft(IContainer container) =>
            container.Border(1)
                     .Padding(5)
                     .AlignLeft();
    }
}
