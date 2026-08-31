namespace PMPoshanWithAngular.Server.Data.Models
{
    public class Embedding
    {
        public virtual Guid EmbeddingGuid { get; set; }
        public virtual Guid StudentPhotoGuid { get; set; }
        public virtual int DetectorId { get; set; }
        public virtual int EmbeddingTypeId { get; set; }
        public virtual byte[] EmbeddingData { get; set; } = Array.Empty<byte>();
        public virtual bool IsSynchronized { get; set; }
    }
}
