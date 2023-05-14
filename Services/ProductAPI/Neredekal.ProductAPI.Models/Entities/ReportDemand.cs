using System;
using Neredekal.Data.Entities;
using Neredekal.ProductAPI.Models.Enums;

namespace Neredekal.ProductAPI.Models.Entities
{
    public class ReportDemand : BaseEntity
    {
        public Guid UserId { get; set; }
        public DateTime DemandDate { get; set; }
        public ReportDemandStatus Status { get; set; }
    }
}