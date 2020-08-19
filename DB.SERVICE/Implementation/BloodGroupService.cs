using BD.DATA.Entity;
using BD.REPO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB.SERVICE.Implementation
{
    public class BloodGroupService : IBloodGroupService
    {
        private readonly IRepository<BloodGroup> BloodGroupRepository;
        public BloodGroupService(IRepository<BloodGroup> BloodGroupRepository)
        {
            this.BloodGroupRepository = BloodGroupRepository;
        }

        public void DeleteBloodGroup(int id)
        {
            BloodGroup BloodGroup = BloodGroupRepository.Get(id);
            BloodGroupRepository.Remove(BloodGroup);

        }

        public BloodGroup GetBloodGroup(int id)
        {
            return BloodGroupRepository.Get(id);
        }
        public IEnumerable<BloodGroup> GetBloodGroups()
        {
            return BloodGroupRepository.GetAll();
        }

        public void InsertBloodGroup(BloodGroup BloodGroup)
        {
            BloodGroupRepository.Insert(BloodGroup);
        }

        public void UpdateBloodGroup(BloodGroup BloodGroup)
        {
            BloodGroupRepository.Update(BloodGroup);
        }
    }
}
