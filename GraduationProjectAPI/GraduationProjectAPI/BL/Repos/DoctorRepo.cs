﻿using GraduationProjectAPI.BL.Interfaces;
using GraduationProjectAPI.DAL.Database;
using GraduationProjectAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProjectAPI.BL.Repos
{
    public class DoctorRepo : IDoctor
    {
        private readonly DataContext db;

        public DoctorRepo(DataContext db)
        {
            this.db = db;
        }
        public void Add(Doctor Doctor)
        {
            this.db.Doctors.Add(Doctor);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var doctor = db.Doctors.Where(a=>a.Id==id).Include(a=>a.prescriptions).FirstOrDefault();
            if (doctor.prescriptions.Count() > 0)
            {
                throw new Exception("can not delete Doctor that assign Prescription");
            }
            this.db.Doctors.Remove(doctor);
            db.SaveChanges();
        }

        public IEnumerable<Doctor> GetAll()
        {
            return db.Doctors.ToList();
        }

        public Doctor GetById(int id)
        {
            return db.Doctors.Find(id);
        }

        public void Update(Doctor Doctor)
        {
            db.Entry(Doctor).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
