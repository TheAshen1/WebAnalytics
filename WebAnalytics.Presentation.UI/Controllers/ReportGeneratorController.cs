using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using WebAnalytics.Services.Interfaces;

namespace WebAnalytics.Presentation.UI.Controllers
{
    public class ReportGeneratorController : Controller
    {
        private readonly IStatisticsService _statisticsService;

        public ReportGeneratorController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        public ActionResult TotalStatistics()
        {
            byte[] fileContents;

            using (var package = new ExcelPackage())
            {
                var row = 1;
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                var totalStatistics = _statisticsService.GetTotalStatistics();
                worksheet.Cells[row, 1].Value = "Total users";
                worksheet.Cells[row++, 2].Value = totalStatistics.TotalUsersCount;
                worksheet.Cells[row, 1].Value = "Total page views count";
                worksheet.Cells[row++, 2].Value = totalStatistics.TotalPageViewsCount;
                worksheet.Cells[row, 1].Value = "Total clicks";
                worksheet.Cells[row++, 2].Value = totalStatistics.TotalClicksCount;
                row++;
                var deviceUsage = _statisticsService.GetDeviceUsageStatistics();
                worksheet.Cells[row, 1].Value = "Device usage statistics";
                worksheet.Cells[row++, 1].Style.Font.Bold = true;
                for (int i = 0; i < deviceUsage.Count; i++)
                {
                    worksheet.Cells[row, 1].Value = deviceUsage[i].Device;
                    worksheet.Cells[row++, 2].Value = deviceUsage[i].Count;
                }
                row++;
                var pageViewStatistics = _statisticsService.GetPageViewStatistics();
                worksheet.Cells[row, 1].Value = "Page view statistics";
                worksheet.Cells[row++, 1].Style.Font.Bold = true;
                for (int i = 0; i < pageViewStatistics.Count; i++)
                {
                    worksheet.Cells[row, 1].Value = pageViewStatistics[i].Url;
                    worksheet.Cells[row++, 2].Value = pageViewStatistics[i].Count;
                }
                row++;
                var clickStatistics = _statisticsService.GetClickStatistics();
                worksheet.Cells[row, 1].Value = "Click statistics";
                worksheet.Cells[row++, 1].Style.Font.Bold = true;
                for (int i = 0; i < pageViewStatistics.Count; i++)
                {
                    worksheet.Cells[row, 1].Value = clickStatistics[i].Description;
                    worksheet.Cells[row++, 2].Value = clickStatistics[i].Count;
                }

                fileContents = package.GetAsByteArray();
            }

            if (fileContents == null || fileContents.Length == 0)
            {
                return NotFound();
            }

            return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "test.xlsx"
            );
        }
    }
}