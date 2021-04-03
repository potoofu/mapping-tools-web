using MappingToolsWeb.IndexedDB.Records;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MappingToolsWeb.IndexedDB.Api {

    public enum OrderRecordsBy {
        Name,
        Size,
        LastModified
    }

    public class OrderedFileRecords :IOrderedRecords<IFileRecord> {
        public List<IFileRecord> OrderedRecords { get; private set; } = new List<IFileRecord>();

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

        private readonly List<IFileRecord> _records = new List<IFileRecord>();
        private bool _ascending = true;
        private OrderBackupsBy _orderedby = OrderBackupsBy.Name;

        public void Add(IFileRecord record, bool order = true) {
            _records.Add(record);

            if( order ) {
                Order();
            }
        }

        public void Add(IEnumerable<IFileRecord> records, bool order = true) {
            _records.AddRange(records);

            if( order ) {
                Order();
            }
        }

        public void Remove(IFileRecord record, bool order = true) {
            _records.Remove(record);

            if( order ) {
                Order();
            }
        }

        public void Remove(IEnumerable<IFileRecord> records, bool order = true) {
            _records.RemoveAll(o => records.Contains(o));

            if( order ) {
                Order();
            }
        }

        public void Replace(IFileRecord record, bool order = true) {
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
                    OrderBackupsBy.Name => _records.OrderBy(o => o.Name),
                    OrderBackupsBy.Size => _records.OrderBy(o => o.Data.Length),
                    OrderBackupsBy.BackedUpOn => _records.OrderBy(o => o.LastModified),
                    _ => _records.OrderBy(o => o.Name),
                } ).ToList();
            }
            else {
                OrderedRecords = ( _orderedby switch {
                    OrderBackupsBy.Name => _records.OrderByDescending(o => o.Name),
                    OrderBackupsBy.Size => _records.OrderByDescending(o => o.Data.Length),
                    OrderBackupsBy.BackedUpOn => _records.OrderByDescending(o => o.LastModified),
                    _ => _records.OrderByDescending(o => o.Name),
                } ).ToList();
            }
        }
    }
}