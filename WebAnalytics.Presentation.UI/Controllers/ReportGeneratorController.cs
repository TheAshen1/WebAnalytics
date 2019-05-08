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
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                var totalStatistics = _statisticsService.GetTotalStatistics();
                worksheet.Cells[1, 1].Value = "Total users";
                worksheet.Cells[1, 2].Value = totalStatistics.TotalUsersCount;
                worksheet.Cells[2, 1].Value = "Total page views count";
                worksheet.Cells[2, 2].Value = totalStatistics.TotalPageViewsCount;
                worksheet.Cells[3, 1].Value = "Total clicks";
                worksheet.Cells[3, 2].Value = totalStatistics.TotalClicksCount;

                var deviceUsage = _statisticsService.GetDeviceUsageStatistics();
                worksheet.Cells[4, 1].Value = "Device usage statistics";
                for (int i = 0; i < deviceUsage.Count; i++)
                {
                    worksheet.Cells[i + 5, 1].Value = deviceUsage[i].Device;
                    worksheet.Cells[i + 5, 2].Value = deviceUsage[i].Count;
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