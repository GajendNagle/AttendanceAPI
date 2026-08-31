using System.Security.Cryptography;
using System.Text;

namespace PMPoshanWithAngular.Server.Helper
{
    public static class GuidUpsertHelper
    {
        public static void ValidateGuidNotEmpty(Guid guid, string fieldName)
        {
            if (guid == Guid.Empty)
            {
                throw new ArgumentException($"{fieldName} is required.");
            }
        }

        /// <summary>
        /// Converts document string ids to Guid.
        /// Full UUID strings pass through; short ids (e.g. b1b2c3d4, p123, guid1) map to a stable Guid.
        /// </summary>
        public static Guid FromDocumentString(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{fieldName} is required.");
            }

            var trimmed = value.Trim();
            if (Guid.TryParse(trimmed, out var guid) && guid != Guid.Empty)
            {
                return guid;
            }
            else
            {
                var hash = MD5.HashData(Encoding.UTF8.GetBytes(trimmed));
                return new Guid(hash);
            }
        }

        public static Guid ParsePathGuid(string value, string fieldName)
        {
            return FromDocumentString(value, fieldName);
        }

        public static Guid ParsePathGuid(Guid value, string fieldName)
        {
            ValidateGuidNotEmpty(value, fieldName);
            return value;
        }

        public static async Task InsertOrUpdateAsync<TEntity>(
            NHibernate.ISession session,
            TEntity? existing,
            Func<TEntity> createForInsert,
            Action<TEntity> applyUpdate) where TEntity : class
        {
            if (existing == null)
            {
                await session.SaveAsync(createForInsert());
            }
            else
            {
                applyUpdate(existing);
                await session.UpdateAsync(existing);
            }
        }
    }
}
