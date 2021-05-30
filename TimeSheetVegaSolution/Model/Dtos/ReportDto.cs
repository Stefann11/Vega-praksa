using System.IO;

namespace Model.Dtos
{
    public class ReportDto
    {
        public MemoryStream MemoryStream { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
