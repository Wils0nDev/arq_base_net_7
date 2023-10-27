namespace BaseArchitecture.Domain.Entities
{
    public class LogDb : BaseEntity, IGenerateEntity<LogDb>
    {
        public int IdLogDb { get; set; }
        public int? ErrorNumber { get; set; }
        public int? ErrorSeverity { get; set; }
        public int? ErrorState { get; set; }
        public string ErrorProcedure { get; set; }
        public int? ErrorLine { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime? CreateDateOnly { get; set; }
        public DateTime? CreateDate { get; set; }         
        public LogDb RecoverKey()
        {
            return new LogDb() { IdLogDb = IdLogDb };
        }
    }
}
