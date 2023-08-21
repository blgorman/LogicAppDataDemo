using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection;
using System.Text;

namespace SimpleProductData.Migrations.Scripts
{
    public static class MigrationBuilderSqlResource
    {
        public static OperationBuilder<SqlOperation> SqlResource(this MigrationBuilder mb, string relativeFileName)
        {
            using (var stream = Assembly.GetAssembly(typeof(MigrationBuilderSqlResource))?.GetManifestResourceStream(relativeFileName))
            {
                if (stream == null)
                {
                    throw new System.Exception($"Could not find resource {relativeFileName}");
                }

                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    var data = ms.ToArray();
                    var text = GetStringExcludeBOMPreamble(Encoding.UTF8, data);
                    return mb.Sql(text);
                }
            }
        }

        public static string GetStringExcludeBOMPreamble(this Encoding encoding, byte[] bytes)
        {
            var preamble = encoding.GetPreamble();

            if (preamble?.Length > 0 && bytes.Length >= preamble.Length && bytes.Take(preamble.Length).SequenceEqual(preamble))
            {
                return encoding.GetString(bytes, preamble.Length, bytes.Length - preamble.Length);
            }
            else
            {
                return encoding.GetString(bytes);
            }
        }
    }
}
