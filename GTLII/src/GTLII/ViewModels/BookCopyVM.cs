using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTLII.Entities;

namespace GTLII.ViewModels
{
    public class BookCopyVM
    {
        public int Id { get; set; }

        public bool IsAvailable { get; set; }

        public BookCopyVM()
        {
            
        }

        public BookCopyVM(BookCopy copy)
        {
            Id = copy.Id;
            IsAvailable = copy.IsAvailable;
        }

        protected bool Equals(BookCopyVM other)
        {
            return Id == other.Id && IsAvailable == other.IsAvailable;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BookCopyVM) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id * 397) ^ IsAvailable.GetHashCode();
            }
        }
    }

}
