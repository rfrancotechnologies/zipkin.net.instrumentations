using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zipkin4Ado
{
    public class ZipkinDbTransaction : DbTransaction
    {
        private readonly TimeSpan timerTimeSpan;

        public ZipkinDbTransaction(DbTransaction transaction, ZipkinDbConnection connection)
        {
            InnerTransaction = transaction;
            InnerConnection = connection;
            TransactionId = Guid.NewGuid();
        }

        public ZipkinDbConnection InnerConnection { get; set; }

        public DbTransaction InnerTransaction { get; set; }

        public Guid TransactionId { get; set; }

        public override IsolationLevel IsolationLevel
        {
            get { return InnerTransaction.IsolationLevel; }
        }

        protected override DbConnection DbConnection
        {
            get { return InnerConnection; }
        }

        public override void Commit()
        {
            InnerTransaction.Commit();
        }

        public override void Rollback()
        {
            InnerTransaction.Rollback();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                InnerTransaction.Dispose();
            }

            base.Dispose(disposing);
        }

    }
}
