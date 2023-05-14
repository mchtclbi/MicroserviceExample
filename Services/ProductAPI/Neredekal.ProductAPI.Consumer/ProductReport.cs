using Neredekal.Data.Concretes;
using Neredekal.Data.Interfaces;
using Neredekal.ProductAPI.Models.Entities;
using Neredekal.ProductAPI.Models.Enums;

namespace Neredekal.ProductAPI.Consumer
{
    public class ProductReport
    {
        private readonly IMongoRepository<ReportDemand> _repository;

        public ProductReport()
        {
            _repository = new MongoRepository<ReportDemand>();
        }

        public void SendReportProgress(string id)
        {
            var reportDemandId = Guid.Parse(id);

            var report = _repository.Get(q => q.Id == reportDemandId && q.Status == ReportDemandStatus.Preparing);
            if (report.Status == ReportDemandStatus.Preparing)
            {
                SendReport(report);
                UpdateReportStatus(report, ReportDemandStatus.Completed);
            }
        }

        private void SendReport(ReportDemand report)
        {
            // send report
            return;
        }
        
        private void UpdateReportStatus(ReportDemand report, ReportDemandStatus status)
        {
            report.Status = status;
            _repository.Update(report);
        }
    }
}