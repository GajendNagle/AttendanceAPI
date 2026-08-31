using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using NHibernate.Linq;
using PMPoshanWithAngular.Server.Data.Models;
using ISession = NHibernate.ISession;

namespace PMPoshanWithAngular.Server.Helper
{
    public static class DocApiHelper
    {
        public static string MapGender(string code) => code.ToUpperInvariant() switch
        {
            "M" => "Male",
            "F" => "Female",
            "O" => "Other",
            _ => throw new ArgumentException("Invalid gender code.")
        };

        public static bool TryParseDocDate(string value, out DateTime date)
        {
            string[] formats = {
                "dd/MM/yyyy",
                "dd-MM-yyyy",
                "yyyy-MM-dd",
                "yyyy-MM-dd HH:mm:ss",
                "yyyy-MM-ddTHH:mm:ss",
                "yyyy-MM-ddTHH:mm:ss.fffZ",
                "yyyy-MM-ddTHH:mm:ssZ"
            };

            if (DateTime.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return true;
            }

            return DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
        }

        public static Guid CreateEmbeddingGuid(Guid photoGuid, int detectorId, int embeddingTypeId)
        {
            var input = $"{photoGuid:N}-{detectorId}-{embeddingTypeId}";
            var hash = MD5.HashData(Encoding.UTF8.GetBytes(input));
            return new Guid(hash);
        }

        public static void ValidateEmbeddingType(int embeddingType)
        {
            if (embeddingType <= 0)
                throw new ArgumentException("Invalid embedding type. Must be a positive integer (e.g. 1).");
        }

        public static void ValidatePhotoTypeShortcode(string? photoTypeShortcode)
        {
            if (string.IsNullOrWhiteSpace(photoTypeShortcode)
                || photoTypeShortcode.Trim().Length != 1)
            {
                throw new ArgumentException("Invalid photo type. Use a single character shortcode (e.g. F, S).");
            }
        }

        public static async Task<int> ResolvePhotoTypeIdAsync(ISession session, string? photoTypeShortcode)
        {
            if (photoTypeShortcode != null)
            {
                var trimmed = photoTypeShortcode.Trim().ToUpperInvariant();
                if (trimmed == "1" || trimmed == "FRONT") photoTypeShortcode = "F";
                else if (trimmed == "2" || trimmed == "LEFT") photoTypeShortcode = "L";
                else if (trimmed == "3" || trimmed == "RIGHT") photoTypeShortcode = "R";
                else if (trimmed == "4" || trimmed == "SIDE" || trimmed == "STANDARD") photoTypeShortcode = "S";
            }

            ValidatePhotoTypeShortcode(photoTypeShortcode);

            var shortcode = photoTypeShortcode!.Trim().ToUpperInvariant();
            var photoType = await session.Query<PhotoType>()
                .FirstOrDefaultAsync(p => p.Shortcode == shortcode);

            if (photoType == null)
            {
                // Seed common shortcodes if missing (e.g. after truncate).
                var name = shortcode switch
                {
                    "F" => "Front",
                    "S" => "Side",
                    "L" => "Left",
                    "R" => "Right",
                    _ => $"Type {shortcode}"
                };

                photoType = new PhotoType
                {
                    Name = name,
                    Shortcode = shortcode
                };
                await session.SaveAsync(photoType);
                await session.FlushAsync();
            }

            return photoType.PhotoTypeId;
        }

        public static async Task EnsureDetectorExistsAsync(ISession session, int detectorId)
        {
            if (!await session.Query<Detector>().AnyAsync(d => d.DetectorId == detectorId))
            {
                await session.SaveAsync(new Detector
                {
                    DetectorId = detectorId,
                    DetectorName = $"Detector {detectorId}"
                });
                await session.FlushAsync();
            }
        }

        public static async Task EnsureEmbeddingTypeExistsAsync(ISession session, int embeddingTypeId)
        {
            ValidateEmbeddingType(embeddingTypeId);

            if (!await session.Query<EmbeddingType>().AnyAsync(e => e.EmbeddingTypeId == embeddingTypeId))
            {
                // Seed missing type (table may be empty after truncate, or client uses a new id).
                await session.SaveAsync(new EmbeddingType
                {
                    EmbeddingTypeId = embeddingTypeId,
                    EmbeddingName = $"Type {embeddingTypeId}"
                });
                await session.FlushAsync();
            }
        }
    }
}
