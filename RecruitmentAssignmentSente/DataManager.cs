using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RecruitmentAssignment_SenteTests")]

namespace RecruitmentAssignmentSente
{
    internal class DataManager
    {
        public XSD.Structure EmployeeHierarchy { private get; set; }

        internal Dictionary<int, DAO.Employee> Employees;
        internal Dictionary<int, int?> EmployeesBosses;

        public void BuildEmployeeDictionaries()
        {
            Employees = new Dictionary<int, DAO.Employee>();
            EmployeesBosses = new Dictionary<int, int?>();

            foreach (var item in EmployeeHierarchy.Employees)
            {
                EmployeesBosses.Add(item.id, null);

                var currentEmployee = new DAO.Employee
                {
                    Id = item.id,
                    Boss = null,
                    Level = 0,
                    Subordinates = new List<DAO.Employee>(),
                    MonetaryCommision = 0
                };

                Employees.Add(item.id, currentEmployee);
                BuildEmployeeDictionaries(item, currentEmployee);
            }
        }

        void BuildEmployeeDictionaries(XSD.Employee xsdEmployee, DAO.Employee employee = null)
        {
            if (xsdEmployee.Employees != null)
                foreach (var item in xsdEmployee.Employees)
                {
                    EmployeesBosses.Add(item.id, xsdEmployee.id);

                    var currentEmployee = new DAO.Employee
                    {
                        Id = item.id,
                        Boss = employee,
                        Level = employee.Level + 1,
                        Subordinates = new List<DAO.Employee>(),
                        MonetaryCommision = 0
                    };

                    Employees.Add(item.id, currentEmployee);
                    employee.Subordinates.Add(currentEmployee);
                    BuildEmployeeDictionaries(item, currentEmployee);
                }
        }

        int[] GetAllBossesOfEmployeeSorted(int id)
        {
            var bosses = new List<int>();
            int? currentId = id;

            while (true)
            {
                currentId = EmployeesBosses[(int)currentId];
                if (currentId != null)
                {
                    bosses.Add((int)currentId);
                }
                else
                {
                    break;
                }
            }

            bosses.Sort();
            return bosses.ToArray();
        }

        void CalculateTransfer(int id, int amount)
        {
            int[] bosses = GetAllBossesOfEmployeeSorted(id);
            int currentAmount = amount;

            for (int i = 0; i < bosses.Length - 1; i++)
            {
                var monetaryCommision = (int)Math.Floor(currentAmount / 2f);
                currentAmount -= monetaryCommision;
                Employees[bosses[i]].MonetaryCommision += currentAmount;
            }

            Employees[bosses[bosses.Length - 1]].MonetaryCommision += currentAmount;
        }

        public void CalculateTransfers(XSD.Transfers transfers)
        {
            foreach (var transfer in transfers.Transfer)
            {
                CalculateTransfer(transfer.From, transfer.Amount);
            }
        }

        public int GetAllSubordinatesWithoutTheirSubordinates(int id)
        {
            int value = 0;

            foreach (var subordinate in Employees[id].Subordinates)
            {
                value += GetAllSubordinatesWithoutTheirSubordinates(subordinate);
            }

            return value;
        }

        int GetAllSubordinatesWithoutTheirSubordinates(DAO.Employee employee)
        {
            int value = 0;

            if (employee.Subordinates.Count == 0)
                return 1;

            foreach (var subordinate in employee.Subordinates)
            {
                value += GetAllSubordinatesWithoutTheirSubordinates(subordinate);
            }

            return value;
        }

        public DAO.Employee[] GetAllEmployeesSorted()
            => Employees.Values.OrderBy(x => x.Id).ToArray();
    }
}
