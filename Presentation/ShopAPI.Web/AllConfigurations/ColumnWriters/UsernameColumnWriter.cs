using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace ShopAPI.Web.AllConfigurations.ColumnWriters
{
    public class UsernameColumnWriter : ColumnWriterBase
    {
        public UsernameColumnWriter() : base(NpgsqlDbType.Varchar)
        {
        }


        public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
        {
            var(username,value) = logEvent.Properties.FirstOrDefault(x => x.Key == "user_name");

            return value?.ToString() ?? null;
        }



    }
}
