using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public class EmployeeStorage : IEmployeeStorage
    {
        private EmployeeContext _db;
        public EmployeeStorage()
        {
            _db = new EmployeeContext();
        }

        //we cant unit   test this as it is interacting with external dependency so for this we need to write integration test
        public void DeleteEmployee(int id)
        {
            var employee = _db.Employees.Find(id);
            if (employee == null) return;
                _db.Employees.Remove(employee);
                _db.SaveChanges();
        }
    }
}
