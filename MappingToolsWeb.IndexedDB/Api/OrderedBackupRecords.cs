using MappingToolsWeb.IndexedDB.Records;
using System.Collections.Generic;
using System.Linq;

namespace MappingToolsWeb.IndexedDB.Api {

    public enum OrderBackupsBy {
        Name,
        Size,
        BackedUpOn
    }

    public class OrderedBackupRecords :IOrderedRecords<IBackupRecord> {
        public List<IBackupRecord> OrderedRecords { get; private set; } = new List<IBackupRecord>();

        public bool IsAscending {
            get { return _ascending; }
            set {
                _ascending = value;
                Order();
            }
        }

        public OrderBackupsBy OrderedBy {
            get {
                return _orderedby;
            }
            set {
                _orderedby = value;
                Order();
            }
        }

        private readonly List<IBackupRecord> _records = new List<IBackupRecord>();
        private bool _ascending = true;
        private OrderBackupsBy _orderedby = OrderBackupsBy.Name;

        public void Add(IBackupRecord record, bool order = true) {
            _records.Add(record);

            if( order ) {
                Order();
            }
        }

        public void Add(IEnumerable<IBackupRecord> records, bool order = true) {
            _records.AddRange(records);

            if( order ) {
                Order();
            }
        }

        public void Remove(IBackupRecord record, bool order = true) {
            _records.Remove(record);

            if( order ) {
                Order();
            }
        }

        public void Remove(IEnumerable<IBackupRecord> records, bool order = true) {
            _records.RemoveAll(o => records.Contains(o));

            if( order ) {
                Order();
            }
        }

        public void Replace(IBackupRecord record, bool order = true) {
            var index = _records.IndexOf(record);

            if( index >= 0 ) {
                _records[index] = record;
            }

            if( order ) {
                Order();
            }
        }

        public void Clear() {
            _records.Clear();

            Order();
        }

        public void Order() {
            if( _ascending ) {
                OrderedRecords = ( _orderedby switch {
                    OrderBackupsBy.Name => _records.OrderBy(o => o.File.Name),
                    OrderBackupsBy.Size => _records.OrderBy(o => o.File.Data.Length),
                    OrderBackupsBy.BackedUpOn => _records.OrderBy(o => o.BackedUpOn),
                    _ => _records.OrderBy(o => o.File.Name),
                } ).ToList();
            }
            else {
                OrderedRecords = ( _orderedby switch {
                    OrderBackupsBy.Name => _records.OrderByDescending(o => o.File.Name),
                    OrderBackupsBy.Size => _records.OrderByDescending(o => o.File.Data.Length),
                    OrderBackupsBy.BackedUpOn => _records.OrderByDescending(o => o.BackedUpOn),
                    _ => _records.OrderByDescending(o => o.File.Name),
                } ).ToList();
            }
        }
    }
}