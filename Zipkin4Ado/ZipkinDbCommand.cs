using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Zipkin;

namespace Zipkin4Ado
{
    public class ZipkinDbCommand : DbCommand
    {
        public ZipkinDbCommand(DbCommand innerCommand)
        {
            InnerCommand = innerCommand; 
        }

        public ZipkinDbCommand(DbCommand innerCommand, ZipkinDbConnection connection) 
            : this(innerCommand)
        {
            InnerConnection = connection;
        }

        public DbCommand InnerCommand { get; set; }

        public ZipkinDbConnection InnerConnection { get; set; } 

        public override string CommandText
        {
            get { return InnerCommand.CommandText; }
            set { InnerCommand.CommandText = value; }
        }

        public override int CommandTimeout
        {
            get { return InnerCommand.CommandTimeout; }
            set { InnerCommand.CommandTimeout = value; }
        }

        public override CommandType CommandType
        {
            get { return InnerCommand.CommandType; }
            set { InnerCommand.CommandType = value; }
        }

        public override bool DesignTimeVisible
        {
            get { return InnerCommand.DesignTimeVisible; }
            set { InnerCommand.DesignTimeVisible = value; }
        }

        public override ISite Site
        {
            get { return InnerCommand.Site; }
            set { InnerCommand.Site = value; }
        } 

        public override UpdateRowSource UpdatedRowSource
        {
            get { return InnerCommand.UpdatedRowSource; }
            set { InnerCommand.UpdatedRowSource = value; }
        }

        public bool BindByName
        {
            get
            {
                var property = InnerCommand.GetType().GetProperty("BindByName");
                if (property == null)
                {
                    return false;
                }

                return (bool)property.GetValue(InnerCommand, null);
            }

            set
            {
                var property = InnerCommand.GetType().GetProperty("BindByName");
                if (property != null)
                {
                    property.SetValue(InnerCommand, value, null);
                } 
            }
        }

        public DbCommand Inner
        {
            get { return InnerCommand; }
        } 

        protected override DbParameterCollection DbParameterCollection
        {
            get { return InnerCommand.Parameters; }
        }

        protected override DbConnection DbConnection
        {
            get
            {
                return InnerConnection;
            }

            set
            {
                InnerConnection = value as ZipkinDbConnection;
                if (InnerConnection != null)
                {
                    InnerCommand.Connection = InnerConnection.InnerConnection;
                }
                else
                { 
                    InnerConnection = new ZipkinDbConnection(value);
                    InnerCommand.Connection = InnerConnection.InnerConnection; 
                }
            }
        }

        protected override DbTransaction DbTransaction
        {
            get
            {
                return InnerCommand.Transaction == null ? null : new ZipkinDbTransaction(InnerCommand.Transaction, InnerConnection);
            }

            set
            {
                var transaction = value as ZipkinDbTransaction;
                InnerCommand.Transaction = (transaction != null) ? transaction.InnerTransaction : value;
            }
        }

        public override void Cancel()
        {
            InnerCommand.Cancel();
        }

        public override void Prepare()
        {
            InnerCommand.Prepare();
        }

        public override int ExecuteNonQuery()
        {
            int num;
            var commandId = Guid.NewGuid();

            using (var trace = new LocalTrace(this.CommandText).AnnotateWith(PredefinedTag.SqlQuery, this.CommandText)) 
            {
                try
                {
                    num = InnerCommand.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    trace.AnnotateWith(PredefinedTag.Error, exception.Message);
                    throw;
                }
            }

            return num;
        }

        public override object ExecuteScalar()
        {
            object result;
            var commandId = Guid.NewGuid();

            using (var trace = new LocalTrace(this.CommandText).AnnotateWith(PredefinedTag.SqlQuery, this.CommandText))
            {
                try
                {
                    result = InnerCommand.ExecuteScalar();
                }
                catch (Exception exception)
                {
                    trace.AnnotateWith(PredefinedTag.Error, exception.Message);
                    throw;
                }
            }


            return result;
        }


        public override async Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)
        {
            object result;
            var commandId = Guid.NewGuid();

            using (var trace = new LocalTrace(this.CommandText).AnnotateWith(PredefinedTag.SqlQuery, this.CommandText))
            {
                try
                {
                    result = await InnerCommand.ExecuteScalarAsync(cancellationToken);
                }
                catch (Exception exception)
                {
                    trace.AnnotateWith(PredefinedTag.Error, exception.Message);
                    throw;
                }
            }
            return result;
        }

        public override async Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
        {
            int num;
            var commandId = Guid.NewGuid();

            using (var trace = new LocalTrace(this.CommandText).AnnotateWith(PredefinedTag.SqlQuery, this.CommandText))
            {
                try
                {
                    num = await InnerCommand.ExecuteNonQueryAsync(cancellationToken);
                }
                catch (Exception exception)
                {
                    trace.AnnotateWith(PredefinedTag.Error, exception.Message);
                    throw;
                }
            }

            return num;
        }

        protected override async Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
        {
            DbDataReader reader;
            var commandId = Guid.NewGuid();

            using (var trace = new LocalTrace(this.CommandText).AnnotateWith(PredefinedTag.SqlQuery, this.CommandText))
            {
                try
                {
                    reader = await InnerCommand.ExecuteReaderAsync(behavior, cancellationToken);
                }
                catch (Exception exception)
                {
                    trace.AnnotateWith(PredefinedTag.Error, exception.Message);
                    throw;
                }
            }

            return reader;
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            DbDataReader reader;
            var commandId = Guid.NewGuid();

            using (var trace = new LocalTrace(this.CommandText).AnnotateWith(PredefinedTag.SqlQuery, this.CommandText))
            {
                try
                {
                    reader = InnerCommand.ExecuteReader(behavior);
                }
                catch (Exception exception)
                {
                    trace.AnnotateWith(PredefinedTag.Error, exception.Message);
                    throw;
                }
            }

            return reader;
        }

        protected override DbParameter CreateDbParameter()
        {
            return InnerCommand.CreateParameter();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && InnerCommand != null)
            {
                InnerCommand.Dispose();
            }

            InnerCommand = null;
            InnerConnection = null;
            base.Dispose(disposing);
        }
    }
}
