using BD.DATA.Entity;
using System;
using System.Collections.Generic;

namespace DB.SERVICE
{
    public interface IBloodGroupService
    {
        IEnumerable<BloodGroup> GetBloodGroups();
        BloodGroup GetBloodGroup(int id);
        void InsertBloodGroup(BloodGroup BloodGroup);
        void UpdateBloodGroup(BloodGroup BloodGroup);
        void DeleteBloodGroup(int id);
    }
}
