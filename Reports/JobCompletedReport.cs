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
    internal class JobCompletedReport : IDocument
    {

        private readonly List<TableJobssub> _SubJobs;

        public JobCompletedReport(List<TableJobssub> jobs)
        {
            _SubJobs = jobs;
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

                for (int i = 0; i < _SubJobs.Count; i++)
                {
                    var job = _SubJobs[i];

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
                               if (File.Exists(job.image))
                                   img.Image(job.image, ImageScaling.FitArea);
                               else
                                   img.Text(TaskHive.Strings.NoImage).AlignCenter();
                           });

                        row.RelativeItem().PaddingLeft(10).Column(info =>
                        {
                            info.Item().Text(job.customername).Bold();
                            info.Item().Text($"{TaskHive.Strings.Phone}: {job.phone}");
                            info.Item().Text($"{TaskHive.Strings.Email}: {job.clientmail}");
                            info.Item().Text($"{TaskHive.Strings.nationality}: {job.nationality}");
                            info.Item().Text($"{TaskHive.Strings.age}: {job.age}");
                            info.Item().Text($"{TaskHive.Strings.address}: {job.clientaddress}");
                        });
                    });

                    column.Item().Element(c => ComposeProfileTable(c, job));
                    column.Item().Element(c => ComposeProfileDescription(c, job));
                    column.Item().Element(c => ComposeProfileResult(c, job));

             
                    if (i < _SubJobs.Count - 1)
                        column.Item().PageBreak();
                }
            });
        }
        void ComposeProfileTable(IContainer container, TableJobssub job)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(150);
                    columns.RelativeColumn();
                });

                AddRow(table, TaskHive.Strings.companyname, job.CompanyName);
                AddRow(table, TaskHive.Strings.CompanyAddress, job.companyaddress);
                AddRow(table, TaskHive.Strings.companyphone, job.companyphone);
                AddRow(table, TaskHive.Strings.EmailCompany, job.companyemail);
                AddRow(table, TaskHive.Strings.companywebsite, job.companywebsite);
         

            });
        }
        void ComposeProfileDescription(IContainer container, TableJobssub job)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                });

                AddRow(table, TaskHive.Strings.CustomerDescription,job.jobcomment );


            });
        }

        void ComposeProfileResult(IContainer container, TableJobssub job)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                });

                AddRow(table, TaskHive.Strings.Result, job.Notes2);


            });
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

 


    }




}
