namespace Resume.Models
{
    public class Work
    {
        public int Id { get; set; }

        public int StotageFileId { get; set; }

        public StorageFile StorageFile { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Comment { get; set; }
    }
}