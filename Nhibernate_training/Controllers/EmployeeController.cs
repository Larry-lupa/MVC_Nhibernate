using System.Linq;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Linq;
using Nhibernate_training.Models;

namespace Nhibernate_training.Controllers
{
    public class EmployeeController : Controller
    {

        public ActionResult Index()
        {
            using (ISession session = OpenNhibernateSession.OpenSession())
            {
                var employees = session.Query<Employee>().ToList();
                return View(employees);
            }
        }

        public ActionResult Details(int id = 0)
        {
            using (ISession session = OpenNhibernateSession.OpenSession())
            {
                var employee = session.Get<Employee>(id);
                return View(employee);
            }
        }

        //
        // GET: /Associates/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Associates/Create
        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            using (ISession session = OpenNhibernateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(emp);
                    transaction.Commit();
                }
            }

            return View(emp);
        }

        //
        // GET: /Associates/Edit/5
        public ActionResult Edit(int id = 0)
        {
            using (ISession session = OpenNhibernateSession.OpenSession())
            {
                var employee = session.Get<Employee>(id);
                return View(employee);
            }
        }

        //
        // POST: /Associates/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, Employee emp)
        {
            using (ISession session = OpenNhibernateSession.OpenSession())
            {
                var employee = session.Get<Employee>(id);
                employee.FirstName = emp.FirstName;
                employee.Designation = emp.Designation;
                employee.Role = emp.Role;
                employee.Gender = emp.Gender;
                employee.Salary = emp.Salary;
                employee.City = emp.City;
                employee.State = emp.State;
                employee.Zip = emp.Zip;

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(employee);
                    transaction.Commit();
                }
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Associates/Delete/5
        public ActionResult Delete(int id = 0)
        {
            using (ISession session = OpenNhibernateSession.OpenSession())
            {
                var employee = session.Get<Employee>(id);
                return View(employee);
            }
        }

        //
        // POST: /Associates/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, Employee emp)
        {
            using (ISession session = OpenNhibernateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(emp);
                    transaction.Commit();
                }
            }
            return RedirectToAction("Index");
        }
    }
}