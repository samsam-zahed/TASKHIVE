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
    internal class ReportJobs : IDocument
    {
        private readonly List<TableJobs> _ItemJobs;

        public ReportJobs(List<TableJobs> MyJobs)
        {
            _ItemJobs = MyJobs;
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

                foreach (var items in _ItemJobs)
                {
                    column.Item().Element(x => ComposeClient(x, items, index));
                    index++;
                }
            });
        }

        void ComposeClient(IContainer container, TableJobs items, int number)
        {
            container.Table(table =>
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
                table.Cell().Element(CellTitle).Text(TaskHive.Strings.ServiceUser);
                table.Cell().Element(CellTitle).Text(TaskHive.Strings.date);
                table.Cell().Element(CellTitle).Text(TaskHive.Strings.AssistanceServiceType);

                table.Cell().Element(CellValue).Text(items.id.ToString());
                table.Cell().Element(CellValue).Text(items.customername);
                table.Cell().Element(CellValue).Text(ClassYear.Return_DateName(items.idday));
                table.Cell().Element(CellValue).Text(items.jobcaption);

                // Row 2
                table.Cell().Element(CellTitle).Text(TaskHive.Strings.ServiceStatus);
                table.Cell().Element(CellTitle).Text(TaskHive.Strings.ServiceWorker);
                table.Cell().Element(CellTitle).Text(TaskHive.Strings.Time);
                table.Cell().Element(CellTitle).Text(TaskHive.Strings.ServiceUserID);

                table.Cell().Element(CellValue).Text(items.jobsit);
                table.Cell().Element(CellValue).Text(items.jobduty);
                table.Cell().Element(CellValue).Text(items.jobtime);
                table.Cell().Element(CellValue).Text(items.iduser);

                // Row 3
                table.Cell().ColumnSpan(4)
                    .Element(CellTitleDes)
                    .Text(TaskHive.Strings.description);

                table.Cell().ColumnSpan(4)
                    .Element(CellValueDes)
                    .Text(items.jobcomment ?? "-");
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

        static IContainer CellTitleDes(IContainer container) =>
    container.Border(1)
             .Background(Colors.Grey.Lighten3)
             .Padding(5)
             .AlignLeft();

        static IContainer CellValueDes(IContainer container) =>
     container.Border(1)
              .Padding(5)
              .AlignLeft();

    }
}
