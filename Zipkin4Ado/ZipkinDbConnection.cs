using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Transactions;

namespace Zipkin4Ado
{
    public class ZipkinDbConnection : DbConnection
    {
        private TimeSpan timerTimeSpan;
        private bool wasPreviouslyUsed;

        public ZipkinDbConnection(DbConnection connection)
            : this(connection, DbProviderFactories.GetFactory(connection))
        { 
        }

        public ZipkinDbConnection(DbConnection connection, DbProviderFactory providerFactory)
            : this(connection, providerFactory, Guid.NewGuid())
        { 
        }

        public ZipkinDbConnection(DbConnection connection, DbProviderFactory providerFactory, Guid connectionId)
        {
            InnerConnection = connection;
            InnerProviderFactory = providerFactory;
            ConnectionId = connectionId;
        }

        public override event StateChangeEventHandler StateChange
        {
            add
            {
                if (InnerConnection != null)
                {
                    InnerConnection.StateChange += value;
                }
            }
            remove
            {
                if (InnerConnection != null)
                {
                    InnerConnection.StateChange -= value;
                }
            }
        }

        public DbProviderFactory InnerProviderFactory { get; set; }

        public DbConnection InnerConnection { get; set; }

        public Guid ConnectionId { get; set; }

        public override string ConnectionString
        {
            get { return InnerConnection.ConnectionString; }
            set { InnerConnection.ConnectionString = value; }
        }

        public override int ConnectionTimeout
        {
            get { return InnerConnection.ConnectionTimeout; }
        }

        public override string Database
        {
            get { return InnerConnection.Database; }
        }

        public override string DataSource
        {
            get { return InnerConnection.DataSource; }
        }

        public override ConnectionState State
        {
            get { return InnerConnection.State; }
        }

        public override string ServerVersion
        {
            get { return InnerConnection.ServerVersion; }
        }

        public override ISite Site
        {
            get { return InnerConnection.Site; }
            set { InnerConnection.Site = value; }
        }

        protected override DbProviderFactory DbProviderFactory
        {
            get { return InnerProviderFactory; }
        }

        public override void ChangeDatabase(string databaseName)
        {
            InnerConnection.ChangeDatabase(databaseName);
        }

        public override void Close()
        {
            InnerConnection.Close(); 
        }

        public override void Open()
        {
            InnerConnection.Open(); 
        }

        public override void EnlistTransaction(Transaction transaction)
        {
            InnerConnection.EnlistTransaction(transaction);
        }
         
        public override DataTable GetSchema()
        {
            return InnerConnection.GetSchema();
        }

        public override DataTable GetSchema(string collectionName)
        {
            return InnerConnection.GetSchema(collectionName);
        }

        public override DataTable GetSchema(string collectionName, string[] restrictionValues)
        {
            return InnerConnection.GetSchema(collectionName, restrictionValues);
        }
         
        protected override DbTransaction BeginDbTransaction(System.Data.IsolationLevel isolationLevel)
        {
            return new ZipkinDbTransaction(InnerConnection.BeginTransaction(isolationLevel), this);
        }

        protected override DbCommand CreateDbCommand()
        {
            return new ZipkinDbCommand(InnerConnection.CreateCommand(), this);
        }

        protected override object GetService(Type service)
        {
            return ((IServiceProvider)InnerConnection).GetService(service);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && InnerConnection != null)
            {
                InnerConnection.Dispose();
            }

            InnerConnection = null;
            InnerProviderFactory = null;
            base.Dispose(disposing);
        }
    }
}
