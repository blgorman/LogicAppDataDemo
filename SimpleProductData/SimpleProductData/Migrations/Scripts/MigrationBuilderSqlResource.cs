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
                    var text = Encoding.UTF8.GetString(data, 0, data.Length);
                    return mb.Sql(text);
                }
            }
        }
    }
}
