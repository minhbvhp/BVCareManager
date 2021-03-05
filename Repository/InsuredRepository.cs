﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BVCareManager.Models;

namespace BVCareManager.Repository
{
    class InsuredRepository
    {
        private BVCareManagerDataContext db = new BVCareManagerDataContext();

        //Query Methods
        public IQueryable<Insured> FindAllInsureds()
        {
            return db.Insureds;
        }

        public Insured GetInsured(string id)
        {
            return db.Insureds.SingleOrDefault(insured => insured.Id == id);
        }

        //Insert/Delete Methods
        public void Add(Insured insured)
        {
            db.Insureds.InsertOnSubmit(insured);
        }

        public void Delete(Insured insured)
        {
            db.Insureds.DeleteOnSubmit(insured);
            db.Policies.DeleteAllOnSubmit(insured.Policies);
        }

        public void Save()
        {
            db.SubmitChanges();
        }

    }
}